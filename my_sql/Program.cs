using System;
using MySql.Data.MySqlClient;
namespace my_sql;
class Program
{
  

    static void Main()
    {
        
        Agent agent1 = new Agent(1,"123", "beeny", "home");
        Agent agent2 = new Agent(2,"567", "dany", "work");
        Agent agent3 = new Agent(3,"568", "david", "sea");

        AgentDAL agentDB = new AgentDAL();
        agentDB.CreateAgentsTable();
        agentDB.AddAgent(agent1);
        agentDB.AddAgent(agent2);
        agentDB.AddAgent(agent3);
        List<Agent> agentList = agentDB.GetAllAgents();
        foreach (Agent agent in agentList)
        {
           Console.WriteLine(agent.ToString());
        }
        agentDB.UpdateAgentLocation(1, "work");
        agentList = agentDB.GetAllAgents();
        foreach (Agent agent in agentList)
        {
            Console.WriteLine(agent.ToString());
        }
        
        agentDB.DeleteAgent(1);
        agentList = agentDB.GetAllAgents();
        foreach (Agent agent in agentList)
        {
            Console.WriteLine(agent.ToString());
        }


    }
}