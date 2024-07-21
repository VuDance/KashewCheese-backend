namespace KashewCheese.API.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizePermissionAttribute : Attribute
    {
        public string Permission { get; }

        public AuthorizePermissionAttribute(string permission)
        {
            Permission = permission;
        }
    }
}
