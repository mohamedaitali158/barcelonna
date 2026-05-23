# Playable Vertical Slice Setup (Polish Pass)

## Immediate Focus
Do not add many new systems now.
Focus on polish:
- movement feel
- camera feel
- audio feel
- cinematic transitions
- behavioral horror feel

## Cinematic Transitions to add now
Between:
- Day Start
- Scavenge -> Shelter
- Expedition Return

Use:
- fade overlay
- camera cuts
- sirens
- breathing
- bunker door sounds

## Scene Wiring
1. Add `GamePhaseController` on `GameSystems`.
2. Add `ScavengeCountdownTimer` and `VerticalSliceDayController`.
3. Add `CinematicTransitionController`:
   - assign full-screen `fadeOverlay`
   - assign `scavengeCamera` and `shelterCamera`
4. Add `CinematicTransitionAudio`:
   - assign `AudioSource`
   - assign siren / breathing / bunker door clips
5. Add `DynamicShelterDecaySystem`:
   - optional `HumidityFungusSystem` reference
   - optional shelter main `Light` reference
6. Add `CharacterBehavioralDecaySystem`:
   - assign `DynamicShelterDecaySystem`
   - assign `FamilyStressSystem`
   - set tracked character ids
7. Ensure `GameBootstrap` references transition controllers.

## Saveable World State v2+
Persist narrative state with chunk-style JSON:
- `meta` chunk: version, slotId, timestamp
- `simulation` chunk: relationships, diseases, rebellions, fungus, events, dead characters, hidden flags

Current improvements:
- slot-based save files (`ashfall_save_v2_<slot>.json`)
- versioning (`meta.version`)
- async save path (`SaveAsync`) for smoother runtime behavior

## Dynamic Shelter + Behavioral Decay
As days pass:
- lighting quality declines
- bunker noise rises
- humidity/fungus spread intensifies
- sleep quality worsens
- camera tension increases
- characters talk less
- characters trust each other less
- characters move slower
- characters isolate themselves
- characters may show nervous laughter spikes
