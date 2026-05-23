# Ashfall Scalable Cross-Platform Architecture

## Package Requirements
- `com.unity.localization`
- `com.unity.services.core`
- `com.unity.services.authentication`
- `com.unity.services.cloudsave`
- `com.unity.inputsystem`

## Localization Setup
1. Install Localization package.
2. Create locales: Arabic, English, French, Spanish, Japanese, Chinese, Russian.
3. Add String Tables for UI, subtitles, dialogue, events, endings, tutorial.
4. Add `LocalizationManager` to bootstrap object and call `InitializeAsync`.
5. For Arabic RTL:
   - use TextMeshPro RTL-compatible pipeline and right-aligned layouts.
   - subscribe to `OnLanguageChanged(..., isRtl)` to flip layout groups.

## Platform Adaptation
- `PlatformManager`: runtime platform detection.
- `AdaptiveControlPromptSystem`: auto prompt switching by active device.
- `DevicePerformanceManager`: applies quality + FPS + dynamic resolution profiles.

## Input System
- `CrossPlatformInputManager` auto-detects keyboard/mouse, touch, gamepad.
- Touch UI root toggles automatically on mobile usage.
- Keep rebinding flow on Input System actions (existing notes in Input/RebindingNotes.md).

## Security Integration Guide
- `SecurityManager` creates device fingerprint, offline secure mode, and central logging.
- `RuntimeIntegritySystem`: assembly hash + anti-debug checks.
- `TamperDetectionSystem`: runtime tamper hooks and basic speedhack heuristic.
- `SecureSaveValidator`: pre-save gate.
- `SaveSystem` remains encrypted/checksummed with backups and integrity validation.

## Console-Friendly Notes
- Use `Application.persistentDataPath` only for save storage.
- Keep platform services (achievements/cloud) behind manager abstraction.
- Avoid editor-only and unsupported APIs in runtime code paths.

## Multiplayer-Ready Foundation
- Keep authoritative simulation separate from input/view.
- Add server-authoritative adapter later in `Networking` namespace.
- Reuse integrity events for telemetry and trust scoring.

## Performance Targets
- High-end PC/Console: 4K cinematic profile, target 60 FPS.
- Console baseline: 60 FPS with dynamic resolution.
- Mobile: adaptive 30–60 FPS with lower quality tiers.
- Steam Deck: medium profile with balanced fog/shadow cost.
