# Ashfall - Cinematic Survival Prototype (Unity HDRP)

A production-ready **architecture scaffold** for a 3D cinematic survival game inspired by time-pressure scavenging and shelter management, with original systems and atmosphere.

## Engine Choice
- **Unity 6 / 2022+ with HDRP**

## Implemented Foundation
- 60-second scavenging phase controller
- Shelter survival phase entry
- Inventory, stress, humidity/fungus, rebellion, expedition systems
- Random + hidden event roll system
- Dynamic music snapshot switching
- Save/load JSON system
- UI menu/tutorial controller hooks

## Project Structure
```
UnityProject/
  Assets/
    Scenes/
    Scripts/
      Core/
      Gameplay/
      Systems/
      AI/
      UI/
      Audio/
      Save/
      Data/
    ScriptableObjects/
    Prefabs/
    Animations/
    Materials/
    Textures/
    PostProcessing/
    Settings/
    Input/
Docs/
```

## Gameplay Systems Included
- Rare rebellion probability model
- Family stress tracking per member
- Humidity and toxic fungus progression with cleaner mitigation
- Expedition loadout validation (map/gas mask/ammo)
- Hidden rare event support for ultra-rare ending chains

## Next Steps in Unity Editor
1. Create scenes listed in `Docs/SceneAndAnimationSetup.md`.
2. Create ScriptableObject item definitions for all required items.
3. Bind manager references in a bootstrap GameObject.
4. Build animation controllers and facial blendshape animator layer.
5. Configure HDRP volume profile and quality tiers.
