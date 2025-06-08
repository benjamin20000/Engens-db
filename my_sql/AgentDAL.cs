using MySql.Data.MySqlClient;
using System;

namespace my_sql;

public class AgentDAL
{
    private static string connectionString = "server=localhost;" +
                                             "user=root;" +
                                             "database=eagleEyeDB;" +
                                             "port=3306;";
    public AgentDAL() {}
    public void CreateAgentsTable(string tableName = "Agents")
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = $"CREATE TABLE IF NOT EXISTS `{tableName}` (" +
                               $"id INT AUTO_INCREMENT PRIMARY KEY, " +
                               $"codeName VARCHAR(255), " +
                               $"realName VARCHAR(255), " +
                               $"location VARCHAR(255), " +
                               $"status ENUM('Active', 'Injured', 'Missing', 'Retired'), " +
                               $"missionsCompleted INT)";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Agents Table was created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection error: " + ex.Message);
            }
        }
    }



    public void AddAgent(Agent agent)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = $" INSERT INTO Agents (codeName, realName, location, status, missionsCompleted)" +
                               $"VALUES ('{agent.CodeName}', '{agent.RealName}','{agent.Location}','{agent.Status}','{agent.MissionsCompleted}')";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Agent was inserted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection error: " + ex.Message);
            }
        }
    }

    public List<Agent> GetAllAgents()
    {
        List<Agent> agents = new List<Agent>();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = "SELECT * FROM Agents";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                { 
                    Agent agent = new Agent(
                        reader.GetInt32("id"),
                        reader.GetString("codeName"),
                        reader.GetString("realName"),
                        reader.GetString("location"),
                        Enum.Parse<Status>(reader.GetString("status")));                  
                    agents.Add(agent);
                }

                Console.WriteLine($"{agents.Count} agents loaded.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection error: " + ex.Message);
            }
        }
        return agents;
    }

    public void UpdateAgentLocation(int agentId, string newLocation)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = $"UPDATE Agents " +
                               $"SET location = '{newLocation}' " +
                               $"WHERE id = {agentId};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Agent {agentId} location updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection error: " + ex.Message);
            }
        }
    }

    public void DeleteAgent(int agentId)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                string query = $"DELETE FROM Agents WHERE id = {agentId};";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Agent {agentId} deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection error: " + ex.Message);
            }
        }
    }

    
    
    // private static void ConnectionWrapper(Action<MySqlConnection> dbAction)
    // {
    //     using (MySqlConnection conn = new MySqlConnection(connectionString))
    //     {
    //         try
    //         {
    //             conn.Open();
    //             Console.WriteLine("✅ Connection opened.");
    //             dbAction(conn);
    //         }
    //         catch (Exception ex)
    //         {
    //             Console.WriteLine("❌ Connection error: " + ex.Message);
    //         }
    //     }
    // }
    //
}