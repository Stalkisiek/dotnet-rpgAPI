﻿namespace dotnet_rpg2.Models;

public class Weapon
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Damage { get; set; } = 10;
    public Character? Character { get; set; }
    public int CharacterId { get; set; }
}