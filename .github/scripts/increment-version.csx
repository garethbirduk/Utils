using System;
using System.Linq;

if (Args == null || Args.Count() < 1)
{
    Console.WriteLine("Usage: dotnet script increment-version.csx <current_version>");
}
else
{
    string inputVersion = Args[0];
    string[] parts = inputVersion.Split('.');

    // Validate input format
    if (parts.Count() != 3 || !parts.All(x => int.TryParse(x, out _)))
    {
        Console.WriteLine("Invalid version format. Please provide a version number in the format 'major.minor.patch'.");
    }
    else
    {
        // Increment the patch version
        int major = int.Parse(parts[0]);
        int minor = int.Parse(parts[1]);
        int patch = int.Parse(parts[2]) + 1;

        Console.WriteLine($"{major}.{minor}.{patch}");
    }
}
