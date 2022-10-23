namespace ArticlesManager.Domain;

using System.Reflection;

public static class Permissions
{
    // Permissions marker - do not delete this comment
    public const string CanDeleteRolePermission = nameof(CanDeleteRolePermission);
    public const string CanUpdateRolePermission = nameof(CanUpdateRolePermission);
    public const string CanAddRolePermission = nameof(CanAddRolePermission);
    public const string CanReadRolePermissions = nameof(CanReadRolePermissions);
    
    public static List<string> List()
    {
        return typeof(Permissions)
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
            .Select(x => (string)x.GetRawConstantValue())
            .ToList();
    }
}
