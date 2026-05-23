# Unity HDRP Scene Setup

## Scenes
- `MainMenu.unity`: Animated menu, tutorial popup, settings, key rebinding entry.
- `ScavengeDistrict.unity`: 60-second run with dynamic debris and loot spawn points.
- `Shelter.unity`: Management hub with interactable stations.
- `Cinematics.unity`: Ending sequences.

## Lighting & Atmosphere
- HDRP directional light with physically based sky.
- Volumetric fog enabled (anisotropy for ash haze).
- Local volumetric dust volumes in shelter.
- Screen-space global illumination for realistic bounce.

## Post-Processing Profile
- Filmic tonemapper
- Bloom (threshold tuned for emergency lights)
- Vignette (subtle during normal gameplay, stronger in panic events)
- Color adjustments for sickly green fungus zones
- Motion blur and depth of field for cinematic beats

## Animation Controller Setup
- `Survivor_Base.controller` layers:
  1. Locomotion blend tree (walk/run/crouch)
  2. Upper-body overrides (carry, aim, reload)
  3. Facial expression additive layer (fear, anger, grief, relief)
- Parameters: `Speed`, `IsCrouching`, `ThreatLevel`, `Stress`, `HasWeapon`, `FaceEmotionIndex`
- Use Timeline for scripted evacuation and true-ending reveal.

## Camera
- Cinemachine virtual cameras:
  - Exploration follow cam
  - Shelter dialogue cam
  - Panic handheld cam with impulse-based shake
