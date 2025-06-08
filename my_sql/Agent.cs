namespace my_sql;


public enum Status
{
    Active,
    Injured,
    Missing,
    Retired
}

public class Agent
{
    public int Id { get; set; }
    public string CodeName { get; set;}
    public string RealName { get; set;}
    public string Location { get; set;}
    public Status Status { get; set; }
    public int MissionsCompleted { get; set; }

    public Agent(int id, string CodeName, string RealName, string Location, Status Status = Status.Active)
    {
        this.Id = id;
        this.CodeName = CodeName;
        this.RealName = RealName;
        this.Location = Location;
        this.Status = Status;
        this.MissionsCompleted = 0;
    }
    public override string ToString()
    {
        return $"Id: {Id}, CodeName: {CodeName}, RealName: {RealName}, Location: {Location}, Status: {Status}, MissionsCompleted: {MissionsCompleted}";
    }
}