namespace CRUDWebAPIMySQL.Entities
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Firstname { get; set; } = null!;

        public string Lastname { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime EnrollmentDate { get; set; }
    }
}
