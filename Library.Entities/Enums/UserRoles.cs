namespace Library.Entities.Enums
{
    public class UserRoles
    {
        public enum Roles
        {
            Admin,
            ProjectManager,
            Developer,
            Submitter,
            User
        }

        public const Roles default_role = Roles.User;
    }
}