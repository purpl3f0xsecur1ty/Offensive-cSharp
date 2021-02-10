using System;
using System.Data.SqlClient;
using static System.Console;

namespace SQL
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Enter SQL hostname:");
            String sqlServer = ReadLine();
            String database = "master";

            String conString = "Server = " + sqlServer + "; Database = " + database + "; Integrated Security = True;";
            SqlConnection con = new SqlConnection(conString);

            WriteLine($"[*] Attempting to connect to {sqlServer}.");

            try
            {
                con.Open();
                ForegroundColor = ConsoleColor.Green;
                WriteLine($"[+] Successfully authenticated to {sqlServer}.");
                ResetColor();
            }
            catch
            {
                BackgroundColor = ConsoleColor.Red;
                ForegroundColor = ConsoleColor.Black;
                WriteLine($"[!] Failed to authenticate to {sqlServer}.");
                ResetColor();
                Environment.Exit(0);
            }
            WriteLine("[*] Chose a task to perform:");
            WriteLine("1) Enumerate the user.");
            WriteLine("2) Execute SQL queries.");
            WriteLine("Enter the number only:");
            string option = ReadLine();
            if (option == "1")
            {
                Enum(con);
            }
            else if (option == "2")
            {
                RunQuery(con);
            }
            else
            {
                WriteLine("[!] Invalid option.");
            }
        }

        public static void Enum(SqlConnection con)
        {
            // Enum user and roles
            String queryLogin = "SELECT SYSTEM_USER";
            SqlCommand command = new SqlCommand(queryLogin, con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            WriteLine("Logged in as: " + reader[0]);
            reader.Close();

            String queryPublicRole = "SELECT IS_SRVROLEMEMBER('public');";
            command = new SqlCommand(queryPublicRole, con);
            reader = command.ExecuteReader();
            reader.Read();
            Int32 role = Int32.Parse(reader[0].ToString());
            if(role ==1)
            {
                WriteLine("User is a member of public role");
            }
            else
            {
                WriteLine("User is NOT a member of public role");
            }
            reader.Close();

            String querySA = "SELECT IS_SRVROLEMEMBER('sysadmin');";
            command = new SqlCommand(querySA, con);
            reader = command.ExecuteReader();
            reader.Read();
            Int32 role2 = Int32.Parse(reader[0].ToString());
            if (role2 == 1)
            {
                WriteLine("User is a member of SA role");
            }
            else
            {
                WriteLine("User is NOT a member of SA role");
            }
            reader.Close();
        }

        public static void RunQuery(SqlConnection con)
        {
            // Uncomment if impersonation is needed
            /*
            String impersonateUser = "EXECUTE AS LOGIN = '<name goes here>';";
            SqlCommand imp = new SqlCommand(impersonateUser, con);
            SqlDataReader impread = imp.ExecuteReader();
            impread.Close();
            */

            // TO DO: Add functionality to execute given SQL queries
        }
    }
}
