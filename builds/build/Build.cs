using Serilog;
using StageKit.Fallout;
using StageKit.Runtime;

namespace build;

public class Build : StageKitBuild
{
    public Build()
    {
        PackagingTypes =
        [
            ApplicationPackagingType.Portable,
            ApplicationPackagingType.WindowsInstaller,
            ApplicationPackagingType.LinuxAppImage,
            ApplicationPackagingType.LinuxDeb,
            ApplicationPackagingType.LinuxRpm,
            ApplicationPackagingType.LinuxArchPackage,
            ApplicationPackagingType.MacOSAppBundle,
            ApplicationPackagingType.MacOSPkg
        ];

        UnixFilePermissions.Add("binaries/speedtest/speedtest", "755");

        BeforePublishRid = context =>
            Log.Information("Publishing {Rid} to {Path}",
                context.RuntimeIdentifier, context.PublishPath);

        AfterPublishRid = context =>
        {
            Log.Information("Published {Rid} to {Path}",
                context.RuntimeIdentifier, context.PublishPath);
        };
    }

    /// <inheritdoc />
    protected override LinuxAppBundleOptions CreateLinuxAppBundleOptions()
    {
        var options = base.CreateLinuxAppBundleOptions();
        options.SnapStagePackages.Add("libfontconfig1");
        options.AppRunScriptBeforeExec = $$"""
                                           function help() {
                                               cat <<'EOF'
                                            _   _      _    _____
                                           | \ | |    | |  / ____|
                                           |  \| | ___| |_| (___   ___  _ __   __ _ _ __
                                           | . ` |/ _ \ __|\___ \ / _ \| '_ \ / _` | '__|
                                           | |\  |  __/ |_ ____) | (_) | | | | (_| | |
                                           |_| \_|\___|\__|_____/ \___/|_| |_|\__,_|_|

                                           --------------------------------------------------------------------------
                                              All the great {{SoftwareName}} functionality inside an AppImage package.
                                           --------------------------------------------------------------------------
                                           (This package uses the AppImage software packaging technology for Linux
                                            ['One App == One File'] for easy availability of the newest {{SoftwareName}}
                                            releases across all major Linux distributions.)

                                           Usage: --help, -h
                                                  # This message

                                                  --appimage-extract
                                                  # Unpack this AppImage into a local sub-directory
                                                  # [currently named 'squashfs-root']

                                                  --appimage-help
                                                  # Show available AppImage options
                                                  
                                                  --portable [level]
                                                  # Run in portable mode, configurations are saved near the executable. 
                                                  # Use level to specify the directory level, e.g. `0` for the current directory, `1` for the parent directory, etc.
                                                  
                                                  --profile-path <path>
                                                  # Specify the path to the profile file.
                                                  
                                                  # Note: Both `--portable` and `--profile-path` can be used together
                                                  # but `--profile-path` will take precedence if both are specified.
                                           EOF
                                           }

                                           if [[ "${1:-}" == "--help" || "${1:-}" == "-h" ]]; then
                                               help
                                               exit 0
                                           fi

                                           """;

        return options;
    }

    public static int Main() => Execute<Build>(x => x.Compile);
}