using Microsoft.AspNetCore.Http;
using Service.Constants;
using System.Buffers;

namespace Service.Helpers;

internal static class StorageUtility
{
    private const int LargeFileBufferSize = 1048576; // 1MB
    private const int SmallFileBufferSize = 81920;   // 80KB

    /// <summary>
    /// Saves a file (small or large) to a specified folder. Optionally allows custom file name. Returns folder name.
    /// </summary>
    internal static async Task<string> SaveFileAsync(string rootPath, string folder, IFormFile form, FileUploadType fileType, string? customFileName = null)
    {
        string[] allowedExtensions = GetAllowedExtensions(fileType);
        ValidateFileExtension(form.FileName, allowedExtensions);

        string folderName, filePath;

        if (!string.IsNullOrWhiteSpace(customFileName))
        {
            folderName = NormalizePath(folder);
            string directoryPath = Path.Combine(rootPath, folderName);
            EnsureDirectoryExists(directoryPath);

            string extension = Path.GetExtension(form.FileName);
            string safeName = SanitizeFileName(customFileName);
            filePath = Path.Combine(directoryPath, $"{safeName}{extension}");
        }
        else
        {
            (folderName, filePath) = GetSafeFilePath(rootPath, folder, form.FileName);
        }

        try
        {
            if (IsLargeFile(form))
            {
                await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None,
                                                             LargeFileBufferSize, FileOptions.Asynchronous | FileOptions.SequentialScan);
                await using var inputStream = form.OpenReadStream();
                using var bufferOwner = MemoryPool<byte>.Shared.Rent(LargeFileBufferSize);
                var buffer = bufferOwner.Memory;

                int bytesRead;
                while ((bytesRead = await inputStream.ReadAsync(buffer[..buffer.Length])) > 0)
                {
                    await fileStream.WriteAsync(buffer[..bytesRead]);
                }
            }
            else
            {
                await using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None,
                                                             SmallFileBufferSize, FileOptions.Asynchronous | FileOptions.SequentialScan);
                await form.CopyToAsync(fileStream);
            }

            return folderName;
        }
        catch (Exception ex)
        {
            throw new IOException($"Failed to save file '{form.FileName}' to disk.", ex);
        }
    }

    /// <summary>
    /// Returns list of file URLs under specified relative path.
    /// </summary>
    internal static IList<string> GetFileUrlsFromFolder(string rootPath, string baseUrl, string relativeFolderPath)
    {
        if (string.IsNullOrWhiteSpace(rootPath) || string.IsNullOrWhiteSpace(baseUrl) || string.IsNullOrWhiteSpace(relativeFolderPath))
            return [];

        string fullPath = Path.Combine(rootPath, relativeFolderPath);

        if (!Directory.Exists(fullPath))
            return [];

        return [.. Directory.GetFiles(fullPath)
            .Where(file => (File.GetAttributes(file) & FileAttributes.Directory) == 0)
            .Select(file => $"{baseUrl.TrimEnd('/')}/{NormalizePath(relativeFolderPath.Trim('/'))}/{Path.GetFileName(file)}")];
    }

    /// <summary>
    /// Returns the URL of a single file (by name) from a folder, or null if not found.
    /// </summary>
    internal static string? GetSingleFileUrlFromFolder(string rootPath, string baseUrl, string relativeFolderPath, string fileName)
    {
        if (string.IsNullOrWhiteSpace(rootPath) || string.IsNullOrWhiteSpace(baseUrl) ||
            string.IsNullOrWhiteSpace(relativeFolderPath) || string.IsNullOrWhiteSpace(fileName))
            return null;

        string fullFolderPath = Path.Combine(rootPath, relativeFolderPath);

        if (!Directory.Exists(fullFolderPath))
            return null;

        string? filePath = Directory.GetFiles(fullFolderPath)
            .FirstOrDefault(f => string.Equals(Path.GetFileName(f), fileName, StringComparison.OrdinalIgnoreCase));

        if (filePath == null)
            return null;

        string normalizedUrl = $"{baseUrl.TrimEnd('/')}/{NormalizePath(relativeFolderPath.Trim('/'))}/{fileName}";
        return normalizedUrl;
    }

    /// <summary>
    /// Validates file extension against a list of allowed extensions.
    /// </summary>
    internal static void ValidateFileExtension(string fileName, string[] allowedExtensions)
        {
            if (allowedExtensions == null || allowedExtensions.Length == 0)
                throw new InvalidOperationException("Allowed extensions must be specified.");

            string extension = Path.GetExtension(fileName).ToLowerInvariant();

            if (string.IsNullOrWhiteSpace(extension) || !allowedExtensions.Contains(extension))
                throw new InvalidOperationException($"Extension '{extension}' is not allowed.");
        }

    /// <summary>
    /// Gets allowed file extensions based on file upload type.
    /// </summary>
    internal static string[] GetAllowedExtensions(FileUploadType type) => type switch
    {
        FileUploadType.Images => [".png", ".jpg", ".jpeg"],
        FileUploadType.Audios => [".png", ".jpg", ".jpeg"],
        FileUploadType.Videos => [".xlsx"],
        _ => throw new ArgumentOutOfRangeException(nameof(type), $"Unknown file upload type: {type}")
    };

    // ------------------------- Helpers ------------------------- //

    private static (string folderName, string filePath) GetSafeFilePath(string rootPath, string folder, string originalFileName)
    {
        string folderName = Guid.NewGuid().ToString();
        string directoryPath = Path.Combine(rootPath, folder, folderName);
        EnsureDirectoryExists(directoryPath);

        string extension = Path.GetExtension(originalFileName);
        string fileName = $"{Guid.NewGuid()}{extension}";
        string filePath = Path.Combine(directoryPath, fileName);

        return (folderName, filePath);
    }

    internal static (string folderName, string fullFolderPath) GetSafeFolderPath(string rootPath, string folder)
    {
        string folderName = Guid.NewGuid().ToString();
        string fullFolderPath = Path.Combine(rootPath, folder, folderName);
        EnsureDirectoryExists(fullFolderPath);
        return (folderName, fullFolderPath);
    }

    private static void EnsureDirectoryExists(string directoryPath)
    {
        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);
    }

    private static bool IsLargeFile(IFormFile form) => form.Length > 10 * 1024 * 1024;

    private static string NormalizePath(string path) =>
        path.Replace("\\", "/");

    private static string SanitizeFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        return new string([.. fileName.Where(c => !invalidChars.Contains(c))]);
    }
}

