using System.Text.Json;
using Microsoft.AspNetCore.Hosting;

namespace Service.Helpers;

public class LocalizationHelper
{
    private readonly Dictionary<string, Dictionary<string, string>> _labels;

    public LocalizationHelper(IHostingEnvironment env)
    {
        var path = Path.Combine(env.WebRootPath, "assets", "json", "labels.json");
        var json = File.ReadAllText(path);
        _labels = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(json)!;
    }

    public string GetLabel(string lang, string key)
    {
        return _labels.TryGetValue(lang, out var dict) && dict.TryGetValue(key, out var label)
            ? label
            : key;
    }
}