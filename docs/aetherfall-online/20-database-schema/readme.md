# Database Schema

PostgreSQL schema uses UUID primary keys, optimistic concurrency versions, audit timestamps, and append-only ledgers for economy-critical flows.

```sql
CREATE TABLE accounts (
    id UUID PRIMARY KEY,
    email_hash TEXT NOT NULL UNIQUE,
    created_at TIMESTAMPTZ NOT NULL,
    last_login_at TIMESTAMPTZ
);

CREATE TABLE characters (
    id UUID PRIMARY KEY,
    account_id UUID NOT NULL REFERENCES accounts(id),
    name TEXT NOT NULL UNIQUE,
    race TEXT NOT NULL,
    class TEXT NOT NULL,
    level INT NOT NULL DEFAULT 1,
    experience BIGINT NOT NULL DEFAULT 0,
    continent TEXT NOT NULL,
    zone TEXT NOT NULL,
    position JSONB NOT NULL,
    attributes JSONB NOT NULL,
    version BIGINT NOT NULL DEFAULT 0
);

CREATE TABLE items (
    id UUID PRIMARY KEY,
    owner_character_id UUID REFERENCES characters(id),
    template_id TEXT NOT NULL,
    rarity TEXT NOT NULL,
    durability INT NOT NULL,
    affixes JSONB NOT NULL,
    sockets JSONB NOT NULL,
    bound_state TEXT NOT NULL,
    version BIGINT NOT NULL DEFAULT 0
);

CREATE TABLE inventories (
    character_id UUID PRIMARY KEY REFERENCES characters(id),
    slots JSONB NOT NULL,
    weight_used NUMERIC NOT NULL,
    gold BIGINT NOT NULL DEFAULT 0,
    version BIGINT NOT NULL DEFAULT 0
);

CREATE TABLE quests (
    character_id UUID NOT NULL REFERENCES characters(id),
    quest_id TEXT NOT NULL,
    state TEXT NOT NULL,
    progress JSONB NOT NULL,
    choices JSONB NOT NULL,
    PRIMARY KEY (character_id, quest_id)
);

CREATE TABLE faction_reputation (
    character_id UUID NOT NULL REFERENCES characters(id),
    faction_id TEXT NOT NULL,
    reputation INT NOT NULL,
    rank TEXT NOT NULL,
    PRIMARY KEY (character_id, faction_id)
);

CREATE TABLE companions (
    id UUID PRIMARY KEY,
    character_id UUID NOT NULL REFERENCES characters(id),
    companion_template_id TEXT NOT NULL,
    level INT NOT NULL,
    loyalty INT NOT NULL,
    traits JSONB NOT NULL,
    memories JSONB NOT NULL,
    skill_trees JSONB NOT NULL,
    version BIGINT NOT NULL DEFAULT 0
);

CREATE TABLE guilds (
    id UUID PRIMARY KEY,
    name TEXT NOT NULL UNIQUE,
    level INT NOT NULL DEFAULT 1,
    treasury BIGINT NOT NULL DEFAULT 0,
    settings JSONB NOT NULL
);

CREATE TABLE guild_members (
    guild_id UUID NOT NULL REFERENCES guilds(id),
    character_id UUID NOT NULL REFERENCES characters(id),
    rank TEXT NOT NULL,
    permissions JSONB NOT NULL,
    PRIMARY KEY (guild_id, character_id)
);

CREATE TABLE auction_listings (
    id UUID PRIMARY KEY,
    seller_character_id UUID NOT NULL REFERENCES characters(id),
    item_id UUID NOT NULL REFERENCES items(id),
    buyout_price BIGINT,
    bid_price BIGINT, -- minimum opening bid
    current_bid BIGINT, -- latest accepted bid amount
    current_bidder_character_id UUID REFERENCES characters(id), -- current highest bidder
    expires_at TIMESTAMPTZ NOT NULL,
    status TEXT NOT NULL,
    version BIGINT NOT NULL DEFAULT 0
);

CREATE TABLE economy_ledger (
    id UUID PRIMARY KEY,
    actor_character_id UUID REFERENCES characters(id), -- nullable for system-generated events such as tax collection or auction expiry
    event_type TEXT NOT NULL,
    amount BIGINT NOT NULL,
    metadata JSONB NOT NULL,
    created_at TIMESTAMPTZ NOT NULL
);

CREATE UNIQUE INDEX auction_listings_active_item_idx
    ON auction_listings (item_id)
    WHERE status = 'active';

CREATE INDEX economy_ledger_actor_idx
    ON economy_ledger (actor_character_id);

CREATE INDEX economy_ledger_created_at_idx
    ON economy_ledger (created_at);
```
