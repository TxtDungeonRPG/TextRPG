using System;
using System.Numerics;

public class Player
{
	public int Level;
    public string Name;
    public string Class;
    public float AtkPlayer;
    public float DfdPlayer;
    public int Hp;
	public int MaxHp;
    public int Mp;
    public int MaxMp;
    public int Gold;
	public int Exp;

	public Player(int level, string name, string _class, float atkPlayer,float dfdPlayer, int hp, int mp, int gold)
	{
		Level = level;
		Name = name;
        Class = _class;
		AtkPlayer = atkPlayer;
		DfdPlayer = dfdPlayer;
		Hp = hp;
		MaxHp = hp;
		Mp = mp;
		MaxMp = mp;
		Gold = gold;
		Exp = 0;
	}

	public bool LevelUpcheck()
	{
        int[] needExp = { 10, 35, 65, 100 };

        if ((Level<= 4) && Exp >= needExp[Level-1])
		{
			Level++;
			AtkPlayer += 0.5f;
			DfdPlayer += 1;
			Exp = 0;
			return true;
		}
		return false;
	}

	public void PlayerInfo()
	{
        Console.WriteLine("[내정보]");
        Console.Write($"Lv.{Level} ");
        Console.WriteLine($"{Name} ({Class})");
        Console.WriteLine($"HP {Hp}/{MaxHp}");
        Console.WriteLine($"MP {Mp}/{MaxMp}");
    }


}