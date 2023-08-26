namespace dotnet_rpg2.Models;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = "BasicName";
    public Job Profession { get; set; } = Job.Knight;
    public int Hp { get; set; } = 10;
    public User? User { get; set; }
    public Weapon? Weapon { get; set; }
    public List<Skill>? Skills { get; set; }
}