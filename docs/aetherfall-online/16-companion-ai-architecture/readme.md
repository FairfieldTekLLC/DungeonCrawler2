# Companion AI Architecture

Companion AI combines behavior trees, utility AI, finite state machines, emotional systems, and relationship systems.

### Decision Inputs

Threat level, health/mana/stamina, player commands, enemy roles, terrain, status effects, personality, relationship trust, recent memories, tactical opportunities, current quest context, and party composition.

### AI Layers

1. **Perception:** server-authoritative awareness and local prediction for responsiveness.
2. **Blackboard:** shared tactical facts and memory references.
3. **Utility Scoring:** ranks actions by survival, damage, healing, control, loyalty, courage, and opportunity.
4. **Behavior Tree:** executes chosen high-level tactic.
5. **State Machine:** controls animation-safe states such as Idle, Follow, Engage, Retreat, Revive, Interact.
6. **Emotion Model:** modifies thresholds based on fear, trust, anger, and morale.
7. **Memory Resolver:** records significant events and exposes them to dialogue and loyalty systems.
