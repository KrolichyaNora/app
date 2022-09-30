namespace app
{
    internal class User
    {
        // { get; set; } is necessary for JSON serializing / deserializing!
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string hash { get; set; }
        public int role { get; set; }

        public User() { 
        
        }

        public User(string login, string password)
        {
            id = 0;
            this.login = login;
            this.password = password;
            hash = "";
            role = 0;
        }

        public static implicit operator bool(User obj)
        {
            if (obj == null) return false;
            if (obj.id == -1 || obj.id == 0) return false;
            return true;
        }

        public static implicit operator string(User obj)
        {
            string pass = obj.password != null && obj.password != "" ? obj.password : obj.hash;
            return $"User {obj.id} ({obj.login}) - pass {pass} - role {obj.role}";
        }
    }
}
