# Ashfall - Foundation-First Unity HDRP Prototype

This repository now follows a **foundation-first approach** to avoid a messy all-at-once implementation.

## Implemented First (Phase 1)
- Player movement (`CharacterController`-based)
- Third-person camera orbit
- Player inventory
- 60-second scavenging countdown
- Resource pickup/collection loop

## Clean Structure
- `UnityProject/Assets/Scripts/Player`
- `UnityProject/Assets/Scripts/Camera`
- `UnityProject/Assets/Scripts/Inventory`
- `UnityProject/Assets/Scripts/Resources`
- `UnityProject/Assets/Scripts/Gameplay`
- `UnityProject/Assets/Animations`
- `UnityProject/Assets/UI`
- `UnityProject/Assets/Audio`
- `UnityProject/Assets/Prefabs`

## Next
Implement systems one by one (Fungus, Rebellion, Expedition, Dialogue) after validating this base.

See detailed phased plan: `Docs/ImplementationPlan.md`.
