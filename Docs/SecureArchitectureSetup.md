# Secure Authentication + Save Architecture Setup

## Unity Packages (required)
- `com.unity.services.core`
- `com.unity.services.authentication`
- `com.unity.services.cloudsave`

## Scripts Added
- `Authentication/AuthenticationManager.cs`
- `Cloud/CloudSaveManager.cs`
- `Security/SaveEncryptionUtility.cs`
- `Security/IntegrityValidationSystem.cs`
- `Profile/SecurePlayerProfile.cs`

## Integration Notes
1. Add `AuthenticationManager` to a persistent systems object.
2. Add `IntegrityValidationSystem` and reference it from `SaveSystem`.
3. Add `CloudSaveManager` and link `AuthenticationManager` + `SaveSystem`.
4. Add `HiddenGlobalFlags` and assign it in `SaveSystem`.
5. Use `GameBootstrap` example wiring for auto-login + cloud merge at startup.

## Security Behavior
- Local saves are AES-encrypted + checksum-protected.
- Modified/corrupted saves are rejected and fallback to backup slot.
- Backup save is auto-created before each write.
- Integrity checks detect impossible values and log suspicious changes.
