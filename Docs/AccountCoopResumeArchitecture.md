# Account, Rebinding, Resume, and Co-op Expansion

## New Systems
- Input rebinding:
  - `InputRebindingManager`
  - `ControlProfileSaveSystem`
  - `DynamicPromptUpdater`
- Account switching/linking:
  - `AccountManager`
  - `ProfileSelectionUI`
  - `AccountLinkSystem`
- Resume continuity:
  - `ResumeGameSystem`
  - `CheckpointManager`
  - `RuntimeStateSnapshot`
- Co-op foundation:
  - `CoopSessionManager`
  - `CoopPlayerSynchronizer`
  - `SharedShelterState`
  - `NetworkSimulationAuthority`

## Setup Steps
1. Add all managers to persistent GameSystems object.
2. Wire `InputRebindingManager` with gameplay `InputActionAsset`.
3. Wire `AccountManager` with `AuthenticationManager` + `CloudSaveManager`.
4. Add profile selection canvas and bind `ProfileSelectionUI` label.
5. Wire `ResumeGameSystem` with `SaveSystem`, `GamePhaseController`, and `CheckpointManager`.
6. Wire co-op managers; connect transport adapter later (NGO/Mirror/Photon).

## Certification/Platform Notes
- Bindings are saved per platform key to avoid cross-platform conflicts.
- Resume snapshots use `persistentDataPath` for console-safe storage.
- Co-op authority path keeps host-authoritative writes for anti-duplication baseline.

## Multiplayer Safety Baseline
- Shared inventory action validation.
- Move speed delta validation.
- Rare event trigger de-duplication via hidden flag counters.
- Host migration signal + graceful disconnect event hooks.
