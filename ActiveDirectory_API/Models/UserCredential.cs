namespace ActiveDirectory_API.Models
{
    public class UserCredential
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class User
    {
        public string Name { get; set; }
        public string E_ID { get; set; }
        public string Username { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Branch { get; set; }
        public string Company { get; set; }
    }

    public class Designation
    {
        public string Name { get; set; }
    }

    public class Department
    {
        public string Name { get; set; }
    }

    public class Branch
    {
        public string Name { get; set; }
    }
}
