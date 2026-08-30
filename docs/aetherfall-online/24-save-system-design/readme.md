# Save System Design

Client saves only settings, graphics, keybinds, accessibility options, cached manifests, and safe UI layout data. All gameplay state is server authoritative. Backend services persist through transactional repositories, event ledgers, version columns, and periodic snapshots. Recovery uses last known consistent snapshot plus replayable ledger events for economy and inventory.
