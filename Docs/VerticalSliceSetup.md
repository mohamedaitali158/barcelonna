# Playable Vertical Slice Setup (Current Critical Step)

## Goal
Create one fully playable day loop:
- One small house map
- One playable character
- HUD visible
- Scavenge -> Shelter transition
- End shelter and start next day

## Scene Wiring
1. Add `GamePhaseController` to `GameSystems` object.
2. Add `ScavengeCountdownTimer` to `GameSystems` object.
3. Add `VerticalSliceDayController` and assign:
   - `phaseController`
   - `timer`
   - `playerInventory`
   - `stressSystem`
   - `fungusSystem`
4. Add `VerticalSliceHUD` and connect TMP text labels for phase/timer/day.
5. Player prefab:
   - `CharacterController`
   - `PlayerMovementController`
   - `FacialEmotionController`
6. Place pickup prefabs with `ResourcePickup` + collider set to trigger.

## Validation Checklist (Play Mode)
- No console errors.
- Stable FPS in house map.
- Timer counts from 60 to 0.
- Auto switch to shelter phase at timeout.
- Pickups increase inventory values.
- Day increments after ending shelter.

## Next immediate upgrade
- Add phase transition cinematic stingers (camera + audio).
- Hook event queue to dialogue UI.
- Add layered tension music and panic stingers.
