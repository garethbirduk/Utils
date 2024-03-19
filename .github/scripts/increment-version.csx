using System;
using System.Linq;
using System.Text.RegularExpressions;

var debug = true;
var patch = -1;
if (Args != null && Args.Count() > 0 && Args[0] != null)
{
    Console.WriteLine(Args[0]);
    patch = int.Parse(Args[0]);
}

var projectFilePath = System.IO.Path.Combine("..", "..", "Utils", "Utils.csproj");
//var projectFilePath = System.IO.Path.Combine("Utils", "Utils.csproj");
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
        string incrementedVersion = UpdatePatch(version, patch);
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

// Function to increment the patch number of a version string
static string UpdatePatch(string version, int patch)
{
    Console.WriteLine(patch.ToString());
    string[] parts = version.Split('.');
    if (patch < 0)
    {
        patch = int.Parse(parts[2]);
        patch++;
    }
    parts[2] = patch.ToString();
    return string.Join(".", parts);
}
