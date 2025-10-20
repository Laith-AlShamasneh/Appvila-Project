namespace Service.Helpers;

internal static class CommonProperties
{

    /// <summary>
    /// Represents folder categories for storing data.
    /// </summary>
    internal enum FolderPathName
    {
        Shared
    }

    /// <summary>
    /// A dictionary mapping folder categories to their corresponding folder paths.
    /// </summary>
    internal static Dictionary<FolderPathName, string> FolderPathNameDictionary = new()
    {
        { FolderPathName.Shared , "shared" }
    };
}
