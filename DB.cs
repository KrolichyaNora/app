using Npgsql;
namespace app
{
    interface IStorage
    {
        Dictionary<string, string> Register(string login, string password, int role = 1);
        Dictionary<string, string> Auth(string login, string password);
    }

    internal class DB: IStorage
    {
        string connString =
                String.Format(
                    "Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    Config.DBADDR,
                    Config.USER,
                    Config.DBNAME,
                    Config.DBPORT,
                    Config.PASS);

        public Dictionary<string, string> Register(string login, string password, int role = 1)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM {Config.DBNAME}_user WHERE login=@login;", conn))
                {
                    command.Parameters.AddWithValue("login", login);
                    var reader = command.ExecuteReader();
                    int cnt = 0;
                    while (reader.Read())
                    {
                        cnt++;
                    }
                    reader.Close();
                    if (cnt == 0)
                    {
                        // Fine
                    }
                    else if (cnt == 1)
                    {
                        return new Dictionary<string, string> { { "status", "fail" }, { "err", "already exists" } };
                    }
                    else
                    {
                        return new Dictionary<string, string> { { "status", "fail" }, { "err", "more than 1" } };
                    }
                }
                using (var command = new NpgsqlCommand($"INSERT INTO {Config.DBNAME}_user (login, password, role_id) VALUES (@login,@password,@role) RETURNING id;", conn))
                {
                    command.Parameters.AddWithValue("login", login);
                    command.Parameters.AddWithValue("password", password);
                    command.Parameters.AddWithValue("role", role);
                    var reader = command.ExecuteReader();
                    int newid = 0;
                    while (reader.Read())
                    {
                        newid = reader.GetInt32(0);
                    }
                    reader.Close();
                    return new Dictionary<string, string> { { "status", "ok" }, { "id", newid.ToString() }, { "login", login }, { "role", role.ToString() } };
                }
            }
        }

        public Dictionary<string, string> Auth(string login, string password)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var command = new NpgsqlCommand($"SELECT * FROM {Config.DBNAME}_user WHERE login=@login AND password=@password;", conn))
                {
                    command.Parameters.AddWithValue("login", login);
                    command.Parameters.AddWithValue("password", password);
                    var reader = command.ExecuteReader();
                    Dictionary<string, string> res = new Dictionary<string, string>();
                    int cnt = 0;
                    while (reader.Read())
                    {
                        cnt++;
                        if (cnt == 2)
                        {
                            break;
                        }
                        res["status"] = "ok";
                        res["id"] = reader.GetInt32(0).ToString();
                        res["login"] = reader.GetString(1);
                        // res["password"] = reader.GetString(2);
                        res["role"] = reader.GetInt32(3).ToString();
                        
                    }
                    reader.Close();
                    if (cnt == 0)
                    {
                        return new Dictionary<string, string> { { "status", "fail" }, { "err", "not found" } };
                    }
                    else if (cnt == 1)
                    {
                        return res;
                    }
                    else
                    {
                        return new Dictionary<string, string> { { "status", "fail" }, { "err", "more than 1" } };
                    }
                }
            }
        }
    }
}
