﻿using dotnet_rpg2.Dtos.Skill;
using dotnet_rpg2.Dtos.Weapon;
using dotnet_rpg2.Models;

namespace dotnet_rpg2.Dtos;

public class GetCharacterDto
{
    
    public int Id { get; set; }
    public string Name { get; set; } = "BasicName";
    public Job Profession { get; set; } = Job.Knight;
    public int Hp { get; set; } = 10;
    public GetWeaponDto? Weapon { get; set; }
    public List<GetSkillDto>? Skills { get; set; }
}