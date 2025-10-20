using System.Data;

namespace Repository.Common;

internal static class Helper
{
    private const string Value = "@";

    /// <summary>
    /// Retrieves the value of an output parameter from <see cref="DynamicParameters"/>.
    /// Returns the default value of <typeparamref name="T"/> if the parameter is not found or null.
    /// </summary>
    /// <returns>The value of the output parameter as <typeparamref name="T"/> or default.</returns>
    internal static T? GetOutputValueOrDefault<T>(DynamicParameters parameters, string name)
    {
        var paramName = name.StartsWith(Value) ? name[1..] : name;

        if (parameters.ParameterNames.Contains(paramName))
        {
            var value = parameters.Get<object>(paramName);
            if (value != null && value != DBNull.Value)
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }

        return default;
    }

    /// <summary>
    /// Maps a CLR <see cref="Type"/> to its corresponding <see cref="DbType"/>.
    /// </summary>
    /// <returns>The equivalent <see cref="DbType"/>.</returns>
    internal static DbType GetDbType(Type type)
    {
        if (type == typeof(int) || type == typeof(int?)) return DbType.Int32;
        if (type == typeof(long) || type == typeof(long?)) return DbType.Int64;
        if (type == typeof(short) || type == typeof(short?)) return DbType.Int16;
        if (type == typeof(bool) || type == typeof(bool?)) return DbType.Boolean;
        if (type == typeof(string)) return DbType.String;
        if (type == typeof(DateTime) || type == typeof(DateTime?)) return DbType.DateTime;
        if (type == typeof(decimal) || type == typeof(decimal?)) return DbType.Decimal;
        if (type == typeof(double) || type == typeof(double?)) return DbType.Double;

        throw new ArgumentException($"Unsupported type: {type.Name}");
    }
}
