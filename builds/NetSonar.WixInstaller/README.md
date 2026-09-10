# Installer

`NetSonar.WixInstaller.wixproj` was scaffolded by the Fallout `GenerateWindowsWixInstaller` target. The publish
pipeline builds it once per Windows runtime identifier and passes every value it needs:

| Property | Source |
| --- | --- |
| `PublishDirectory` | The staged, published payload for the runtime identifier |
| `ApplicationName` | `SoftwareName` |
| `ApplicationExecutableName` | `SoftwareExecutableFileNameWithoutExtension` |
| `BuildVersion` | `SoftwareVersion` |
| `OutputName` | The release asset name |
| `Platform` | `x64` or `arm64` |

`Company`, `Copyright`, `RepositoryUrl`, and `ApplicationIcon` come from the repository's
`Directory.Build.props`.

## Before the first release

1. Add this project to the solution so the pipeline discovers it.
2. Replace `Resources/License.rtf` with the real license text.
3. Replace the placeholder artwork in `Resources/`.
4. Keep the generated `UpgradeCode` values stable; changing one makes Windows treat future
   installers as a different product instead of an upgrade.

## Installer artwork

| Property | Image size | Layout |
| --- | --- | --- |
| `InstallerDialogImage` | 493 × 312 pixels | Welcome and completion background. Place artwork in the leftmost 164 pixels; keep the right side clear for wizard text. |
| `InstallerBannerImage` | 493 × 58 pixels | Header on subsequent pages, including installation options. Place the logo at the right edge and keep the left side clear for titles. |

Use BMP or PNG images. Each property is optional; omitting it retains the corresponding WiX default,
and a configured missing file fails the build.

See the [WiX artwork documentation](https://docs.firegiant.com/wix/tools/wixext/wixui/#replacing-the-default-bitmaps).
