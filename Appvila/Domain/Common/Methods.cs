namespace Domain.Common;

public static class Methods
{
    private const int DefaultLang = (int)Lang.En;

    /// <summary>
    /// Validates the provided language string and returns the corresponding language code.
    /// </summary>
    /// <returns>The validated language code if valid; otherwise, the default language code.</returns>
    public static int ValidateLanguage(string? lang)
    {
        if (string.IsNullOrEmpty(lang))
            return DefaultLang;

        if (int.TryParse(lang, out int numericLang))
            return Enum.IsDefined(typeof(Lang), numericLang) ? numericLang : DefaultLang;

        return DefaultLang;
    }

    /// <summary>
    /// Retrieves a localized message based on the specified language and message type.
    /// </summary>
    /// <returns>The localized message string corresponding to the specified language and message type.</returns>
    public static string GetMessages(string? lang, MessageType messageType)
    {
        int language = ValidateLanguage(lang);

        if (Messages.TryGetValue(messageType, out var langMessages) &&
            langMessages.TryGetValue(language, out var message))
        {
            return message;
        }

        return Messages[MessageType.SystemProblem][DefaultLang];
    }

    /// <summary>
    /// A dictionary containing localized messages for different message types.
    /// </summary>
    /// <remarks>
    /// The dictionary maps each <see cref="MessageType"/> to another dictionary, 
    /// which stores translations of the message in different languages (English and Arabic).
    /// </remarks>
    private static readonly Dictionary<MessageType, Dictionary<int, string>> Messages = new()
    {
        { MessageType.SaveSuccessfully, new() { { (int)Lang.Ar, "تم حفظ المعلومات" }, { (int)Lang.En, "Successfully saved" } } },
        { MessageType.SaveFailed, new() { { (int)Lang.Ar, "حدثت مشكلة في النظام , لم يتم حفظ المعلومات" }, { (int)Lang.En, "There is a problem in the system, the data was not saved" } } },
        { MessageType.DeleteSuccessfully, new() { { (int)Lang.Ar, "تم حذف المعلومات" }, { (int)Lang.En, "Successfully deleted" } } },
        { MessageType.DeleteFailed, new() { { (int)Lang.Ar, "حدثت مشكلة في النظام , لم يتم حذف المعلومات" }, { (int)Lang.En, "There is a problem in the system, the data was not deleted" } } },
        { MessageType.RetrieveSuccessfully, new() { { (int)Lang.Ar, "تم استرجاع المعلومات" }, { (int)Lang.En, "Successfully queried data" } } },
        { MessageType.RetrieveFailed, new() { { (int)Lang.Ar, "حدثت مشكلة في النظام , لم يتم استرجاع المعلومات" }, { (int)Lang.En, "There is a problem in the system, the data was not retrieved" } } },
        { MessageType.ActiveSuccessfully, new() { { (int)Lang.Ar, "تم تغيير حالة التفعيل بنجاح" }, { (int)Lang.En, "The activation status has been changed successfully" } } },
        { MessageType.ActiveFailed, new() { { (int)Lang.Ar, "حدثت مشكلة في النظام , لم يتم تغيير حالة التفعيل" }, { (int)Lang.En, "A problem occurred in the system, the activation status was not changed" } } },
        { MessageType.NoDataFound, new() { { (int)Lang.Ar, "لا يوجد معلومات" }, { (int)Lang.En, "Information not found" } } },
        { MessageType.SystemProblem, new() { { (int)Lang.Ar, "حدثت مشكلة في النظام, الرجاء مراجعة مدير النظام" }, { (int)Lang.En, "There is a problem in the system, please ask your admin" } } },
    };
}
