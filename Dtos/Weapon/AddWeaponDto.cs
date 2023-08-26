namespace dotnet_rpg2.Dtos.Weapon;

public class AddWeaponDto
{
    public string Name { get; set; } = string.Empty;
    public int Damage { get; set; } = 10;
    public int CharacterId { get; set; }
}