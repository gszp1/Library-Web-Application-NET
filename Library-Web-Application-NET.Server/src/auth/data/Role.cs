using Library_Web_Application_NET.Server.src.model;

namespace Library_Web_Application_NET.Server.src.auth.data
{
    public enum Role
    {
        User,
        Admin
    }

    static class RoleHandler
    {
        public static List<Permission> GetPermissions(string role)
        {
            switch (role)
            {
                case "User":
                    return
                    [
                        Permission.User_Create,
                        Permission.User_Update,
                        Permission.User_Delete,
                        Permission.User_Read
                    ];
                case "Admin":
                    return
                    [
                        Permission.Admin_Create,
                        Permission.Admin_Update,
                        Permission.Admin_Delete,
                        Permission.Admin_Read,
                        Permission.User_Read,
                        Permission.User_Update,
                        Permission.User_Delete,
                        Permission.User_Create
                    ];
                default:
                    return [];
            }
        }
    }
}
