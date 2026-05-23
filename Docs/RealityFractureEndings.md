# Reality Fracture Endings (Impossible Ending Framework)

## Design Intent
These are near-mythic endings with layered hidden conditions.
They should feel uncertain, unsettling, and theory-friendly.

## Core Rule
Not RNG-only. Each ending requires:
1. Long-term behavioral constraints
2. Hidden global flags over many days
3. Specific narrative/event choices
4. Final microscopic trigger chance

## Hidden Global Flags (examples)
- `never_used_gun`
- `laughed_during_death`
- `ignored_radio_signal`
- `saw_shadow_event`
- `trusted_rebel`
- `wall_noise_count`
- `no_music_days`

## Implemented Resolver
`RareEndingResolver` evaluates chains for:
- `WallBreathing`
- `FalseRescue`
- `LaughingShelter`
- `Observer`

Then applies final gate chance (`finalFractureChance`) after all chain checks pass.

## Tone Direction
Do not explicitly inform players these endings exist.
Use subtle hints in:
- audio anomalies
- rare events
- unsettling dialogue
- environmental art cues
