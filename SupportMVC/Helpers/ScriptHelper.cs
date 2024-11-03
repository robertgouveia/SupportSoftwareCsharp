using System.Text.RegularExpressions;

namespace SupportMVC.Helpers;

public static class ScriptHelper
{
    public static string[] GetVariables(string content)
    {
        var matches = Regex.Matches(content, @"@(\w+)");
        var variables = matches.Cast<Match>().Select(m => m.Groups[1].Value).Distinct().ToArray();

        return variables;
    }

    public static string SetVariables(string sql, string[] variables)
        => variables.Aggregate(sql, (current, variable) => current.Replace($"@{variable}", variable));
}