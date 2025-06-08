using MySql.Data.MySqlClient;
using System;

namespace my_sql;

public class AgentDAL
{
    void AddAgent(Agent agent);
    List<Agent> GetAllAgents();
    void UpdateAgentLocation(int agentId, string newLocation);
    void DeleteAgent(int agentId);
    private static string connectionString =
        "server=localhost;" +
        "user=root;" +
        "database=morder;" +
        "port=3306;";

    // Generic method to handle connection lifecycle
    private static void RunWithConnection(Action<MySqlConnection> dbAction)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                Console.WriteLine("✅ Connection opened.");
                dbAction(conn);
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Connection error: " + ex.Message);
            }
        }
    }

    // Your DB operation method
    private static void CreateTable(MySqlConnection connection, string tableName)
    {
        try
        {
            string query = $"CREATE TABLE IF NOT EXISTS `{tableName}` (id INT PRIMARY KEY AUTO_INCREMENT)";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            Console.WriteLine($"✅ Table `{tableName}` created.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Error: " + ex.Message);
        }
    }
    
    

    // Public method to trigger the action
    public static void Init()
    {
        RunWithConnection(conn => CreateTable(conn, "my_table"));
    }
}