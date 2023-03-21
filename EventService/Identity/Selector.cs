namespace EventService.Identity;

/// <summary>
/// 
/// </summary>
public static class Selector
{
    /// <summary>
    /// 
    /// </summary>

    public static Func<HttpContext, string> ForwardReferenceToken(string introspectionScheme = "introspection")
    {
        string Select(HttpContext context)
        {
            var (scheme, credential) = GetSchemeAndCredential(context);

            if (scheme.Equals("Bearer", StringComparison.OrdinalIgnoreCase) &&
                !credential.Contains("."))
            {
                return introspectionScheme;
            }

            return null!;
        }

        return Select;
    }
    /// <summary>
    /// 
    /// </summary>
    public static (string, string) GetSchemeAndCredential(HttpContext context)
    {
        var header = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(header))
        {
            return ("", "");
        }

        var parts = header.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length != 2 ? ("", "") : (parts[0], parts[1]);
    }
}