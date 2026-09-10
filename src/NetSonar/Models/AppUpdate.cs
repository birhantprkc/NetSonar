namespace NetSonar.Avalonia.Models;

public sealed record AppUpdate(Octokit.Release Release, int ReleaseAhead, string Changelog);