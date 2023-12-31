﻿using dotnet_rpg2.Models;

namespace dotnet_rpg2.Dtos;

public class AddCharacterDto
{
    public string Name { get; set; } = "BasicName";
    public Job Profession { get; set; } = Job.Knight;
    public int Hp { get; set; } = 10;
}