# Character Systems

### Playable Races

| Race | Lore | Traits | Passive Bonuses | Customization | Starting Zone |
| --- | --- | --- | --- | --- | --- |
| Human | Adaptive heirs of Valoria's fractured courts. | Versatile, diplomatic | +2% reputation gain, +1 talent point every 20 levels | body, ancestry, noble/common marks, scars | Valorian Marches |
| Elf | Long-lived stewards of Sylthar's living cities. | Arcane, graceful | +3% mana regen, +2% nature resistance | ears, luminous tattoos, hair vines, eye glow | Sylthar Canopy |
| Dark Elf | Survivors of Umbrath's shadow refuge. | Stealth, intrigue | +3% shadow resistance, +2% critical damage from stealth | ash skin, eye flame, house sigils | Umbral Spires |
| Dwarf | Clan-bound masters of Khaz'Gar stone and forge. | Durable, industrious | +4% durability, +5 crafting skill when refining metals | beards, clan brands, prosthetics | Ironroot Hold |
| Orc | Nomadic oath-clans seeking honor after exile. | Ferocity, endurance | +3% stamina, +2% crowd-control resistance | tusks, warpaint, trophies | Redstep Barrens |
| Dragonborn | Volcanic forgeborn descendants of ancient drakes. | Elemental, honorable | +3% fire resistance, breath racial cooldown | scales, horns, crest, ember glow | Drak'Thor Caldera |

### Attributes

| Attribute | Primary Effects |
| --- | --- |
| Strength | melee damage, block stability, heavy armor scaling |
| Dexterity | attack speed, dodge distance, ranged damage, crit chance |
| Intelligence | spell power, mana pool, summon effectiveness |
| Vitality | health, physical resistance, stamina recovery |
| Wisdom | healing, cooldown recovery, mana regen, companion command efficiency |
| Luck | drop quality, critical crafting, proc chance, rare event discovery |

#### Scaling Formulas

- Health = `100 + Level * 18 + Vitality * 12`
- Mana = `50 + Level * 8 + Intelligence * 8 + Wisdom * 4`
- Stamina = `75 + Level * 6 + Dexterity * 3 + Vitality * 5`
- PhysicalDamage = `WeaponDamage * (1 + Strength * 0.006 + Dexterity * 0.002)`
- SpellDamage = `SpellPower * (1 + Intelligence * 0.007 + Wisdom * 0.002)`
- Healing = `BaseHeal * (1 + Wisdom * 0.007 + Intelligence * 0.002)`
- CriticalChance = `min(0.5, BaseCrit + Dexterity * 0.0008 + Luck * 0.0006)`

#### Caps and Interactions

Soft cap at 250 rating per attribute, hard cap at 400 before temporary buffs. Past soft cap, each point gives 50% of normal value. Strength and Vitality improve guard break resistance; Intelligence and Wisdom improve ultimate generation; Dexterity and Luck improve combo finishers and crafting discovery.
