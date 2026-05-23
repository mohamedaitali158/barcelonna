# Behavioral Polish Pass (Micro Animations + Silence Design)

## Goal
Amplify psychological horror without adding new large systems.
Use existing behavioral decay outputs to drive animation and sound.

## Micro Behavioral Animations
Add `MicroBehaviorAnimator` to each survivor and map animator float params:
- `TouchFace`
- `LookBack`
- `HandTremor`
- `FastBreath`
- `FootFidget`
- `LongStare`

Behavioral signals used:
- social trust decay
- isolation need
- nervous laughter spikes

## Silence Design
Add `SilenceTensionAudioController` and assign sources:
- music bed
- breathing loop
- bunker ambience
- water drip loop
- nervous laugh stinger

As talkativeness drops:
- music volume decreases
- breathing/ambience/drips increase
- rare nervous laughter stinger may fire

## Usage Notes
- Do not fill every moment with music.
- Keep long silent stretches after tragic events.
- Reserve laughter stingers for high tension windows.
