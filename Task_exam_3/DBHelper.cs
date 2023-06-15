using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.ComponentModel;

namespace Task_exam_3
{
    public class DBHelper
    {

        private static MySqlConnection? conn = null;

        private DBHelper(
            String host,
            int port,
            string User,
            string Password,
            String database
        )
        {
            var connStr = $"Server={host}; DataBase = {database}; port = {port}; User Id = {User}; password = {Password}";
            conn = new MySqlConnection(connStr);
            conn.Open();
        }

        // Статический метод, который возвращает сущность (инстанс) класса ДБХелпер
        // Он специально сделан, ибо наше подключение существует в единственном экземпляре для конкретной базы
        // Нам нет смысла делать более 1 подключения к ней в нашем проекте

        private static DBHelper instance = null;
        public static DBHelper GetInstance(
            String host = "localhost",
            int port = 0,
            string User = "root",
            string Password = "",
            String database = "")
        {
            if (instance == null)
            {
                instance = new DBHelper(host, port, User, Password, database);
            }
            return instance;
        }

        public bool addUser(string login, string password, string name)
        {
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $"INSERT INTO `users` (login, password, name) VALUES (@login, @password, @name);";

            cmd.Parameters.Add(new MySqlParameter("@login", login));
            cmd.Parameters.Add(new MySqlParameter("@password", password));
            cmd.Parameters.Add(new MySqlParameter("@name", name));

            try { cmd.ExecuteNonQuery(); return true; }
            catch { return false;  }
        }

        public (string,bool) checkUser(string login, string password)
        {
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $"SELECT `id` FROM `users` WHERE `login` = @login AND `password` = @password";

            cmd.Parameters.Add(new MySqlParameter("@login", login));
            cmd.Parameters.Add(new MySqlParameter("@password", password));

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        return (reader[0].ToString(), true);
                    }
                }
            }
            return (null, false);
        }

        public void addResult(int user_id, string coefs, string coefs_res, string result)
        {
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = $"INSERT INTO `values` (user_id, coef, coef_res, result) VALUES (@user_id, @coef, @coef_res, @result);";

            cmd.Parameters.Add(new MySqlParameter("@user_id", user_id));
            cmd.Parameters.Add(new MySqlParameter("@coef", coefs));
            cmd.Parameters.Add(new MySqlParameter("@coef_res", coefs_res));
            cmd.Parameters.Add(new MySqlParameter("@result", result));

            cmd.ExecuteNonQuery();

        }

    }
}
