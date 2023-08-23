using dotnet_rpg2.Dtos;

namespace dotnet_rpg2.Models;

public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = "BasicName";
    public Job Profession { get; set; } = Job.Knight;
}