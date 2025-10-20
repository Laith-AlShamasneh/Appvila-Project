namespace Service.Constants;

internal enum FileUploadType
{
    Images,
    Audios,
    Videos
}

/// <summary>
/// A class that holds service-level constants.
/// </summary>
internal class ServiceConstants
{
    private static string _baseUrl = string.Empty;
    private static string _assetsPath = string.Empty;

    internal static string BaseUrl
    {
        get => _baseUrl;
        set => _baseUrl = value ?? throw new ArgumentNullException(nameof(value), "BaseUrl cannot be null.");
    }

    internal static string AssetsPath
    {
        get => _assetsPath;
        set => _assetsPath = value ?? throw new ArgumentNullException(nameof(value), "AssetPath cannot be null.");
    }
}