using Serilog;
using StageKit.Fallout;
using StageKit.Runtime;

namespace build;

public class Build : StageKitBuild
{
    public Build()
    {
        BeforePublishRid = context =>
            Log.Information("Publishing {Rid} to {Path}",
                context.RuntimeIdentifier, context.PublishPath);

        PackagingTypes =
        [
            ApplicationPackagingType.Portable,
            ApplicationPackagingType.WindowsInstaller,
            ApplicationPackagingType.LinuxAppImage,
            ApplicationPackagingType.LinuxDeb,
            ApplicationPackagingType.LinuxRpm,
            ApplicationPackagingType.LinuxArchPackage,
            ApplicationPackagingType.MacOSAppBundle
        ];
    }

    /// <inheritdoc />
    protected override LinuxAppBundleOptions CreateLinuxAppBundleOptions()
    {
        var options = base.CreateLinuxAppBundleOptions();
        options.SnapStagePackages.Add("libfontconfig1");
        options.AppRunScriptBeforeExec = $$"""
                                           function help() {
                                               echo ' _   _      _    _____                        '
                                               echo '| \ | |    | |  / ____|                       '
                                               echo '|  \| | ___| |_| (___   ___  _ __   __ _ _ __ '
                                               echo '| . ` |/ _ \ __|\___ \ / _ \| '_ \ / _` | '__|'
                                               echo '| |\  |  __/ |_ ____) | (_) | | | | (_| | |   '
                                               echo '|_| \_|\___|\__|_____/ \___/|_| |_|\__,_|_|   '
                                               
                                               echo "
                                            --------------------------------------------------------------------------
                                               All the great {{SoftwareName}} functionality inside an AppImage package.
                                            --------------------------------------------------------------------------
                                            (This package uses the AppImage software packaging technology for Linux
                                             ['One App == One File'] for easy availability of the newest {{SoftwareName}}
                                             releases across all major Linux distributions.)
                                            Usage:  --help, -h
                                            ------     # This message
                                                    --appimage-extract
                                                       # Unpack this AppImage into a local sub-directory [currently named 'squashfs-root']
                                                    --appimage-help
                                                       # Show available AppImage options
                                           "
                                           }

                                           if [ "$1" == "--help" -o "$1" == "-h" ]; then
                                               help
                                               exit $?
                                           fi

                                           """;

        return options;
    }

    public static int Main() => Execute<Build>(x => x.Compile);
}