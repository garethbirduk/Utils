using System;
using System.Linq;
using System.Text.RegularExpressions;

var debug = false;
var local = false;

var name = "unknown";
var update = Update.Unknown;

if (Args != null)
{
    if (Args.Count() > 0 && Args[0] != null)
    {
        name = Args[0];
        Console.WriteLine(name);
    }

    if (name.Contains("breaking", StringComparison.OrdinalIgnoreCase))
        update = Update.Major;

    if (name.Contains("feature", StringComparison.OrdinalIgnoreCase))
        update = Update.Minor;

    if (name.Contains("bug", StringComparison.OrdinalIgnoreCase))
        update = Update.Patch;
}

var projectFilePath = System.IO.Path.Combine("Utils", "Utils.csproj");
if (local)
    projectFilePath = System.IO.Path.Combine("..", "..", "Utils", "Utils.csproj");

Console.WriteLine($"projectFilePath: {projectFilePath}");

// Read all lines from the project file
string[] lines = File.ReadAllLines(projectFilePath);

// Define the pattern
string pattern = @"^(.*<Version>)(\d+\.\d+\.\d+)(<\/Version>.*)$";

if (debug)
    foreach (var line in lines)
        Console.WriteLine(line);

var updated = false;

// Iterate over the list of strings and find matches
for (var i = 0; i < lines.Count(); i++)
{
    var line = lines[i];
    Match match = Regex.Match(line, pattern);

    // If a match is found, replace the version number with an incremented patch number
    if (match.Success)
    {
        string version = match.Groups[2].Value;
        string incrementedVersion = UpdatePatch(version, update);
        string replacedLine = match.Groups[1].Value + incrementedVersion + match.Groups[3].Value;
        Console.WriteLine("Original: " + line);
        Console.WriteLine("Replaced: " + replacedLine);

        lines[i] = replacedLine;
        updated = true;
    }
}

if (updated)
    File.WriteAllLines(projectFilePath, lines);

if (debug)
    foreach (var line in File.ReadAllLines(projectFilePath))
        Console.WriteLine(line);

static string UpdatePatch(string version, Update update)
{
    Console.WriteLine(update.ToString());
    var parts = version.Split('.').Select(x => int.Parse(x)).ToList();

    switch (update)
    {
        case Update.Major:
            parts[0]++;
            parts[1] = 0;
            parts[2] = 0;
            break;

        case Update.Minor:
            parts[1]++;
            parts[2] = 0;
            break;

        case Update.Patch:
            parts[2]++;
            break;

        case Update.Unknown:
            parts[2]++;
            break;
    }
    return string.Join(".", parts);
}

enum Update
{
    Unknown,
    Patch,
    Minor,
    Major
}