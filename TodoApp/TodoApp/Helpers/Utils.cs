using System.Configuration;
using System.Text.RegularExpressions;

namespace TodoApp.Helpers;

static public class Utils
{
    static public string ReadSetting(string key)
    {
        try
        {
            var appSettings = ConfigurationManager.AppSettings;
            return appSettings[key] ?? "";
        }
        catch (ConfigurationErrorsException)
        {
            return "";
        }
    }

    public static List<(string Title, string Description)> ExtractTasks(this string input)
    {
        List<(string Title, string Description)> tasks = new List<(string Title, string Description)>();

        // Define the regular expression pattern to match the titles and descriptions
        string pattern = @"\d+\.\s+Title:\s+(.+?)\s+Description:\s+(.+?)(?=\s*\d+\.|$)";

        // Create a regular expression object and find matches in the input string
        MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.Singleline);

        // Process each match and extract the title and description
        foreach (Match match in matches)
        {
            if (match.Groups.Count == 3)
            {
                string title = match.Groups[1].Value.Trim();
                string description = match.Groups[2].Value.Trim();
                tasks.Add((title, description));
            }
        }

        return tasks;
    }
}
