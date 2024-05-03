using System;
using System.Numerics;

public class Player
{
	public int Level;
    public string Name;
    public string Class;
    public float AtkPlayer;
    public float DfdPlayer;

    private int _hp;
    public int Hp
    {
        get { return _hp; }
        set
        {
            // Hp가 0 이하인 경우 0으로 설정
            _hp = Math.Max(value, 0);
        }
    }

    public int MaxHp;

    private int _mp;
    public int Mp
    {
        get { return _mp; }
        set
        {
            // Hp가 0 이하인 경우 0으로 설정
            _mp = Math.Max(value, 0);
        }
    }

    public int MaxMp;

    private int _gold;

    public int Gold
    {
        get { return _gold; }
        set
        {
            // Hp가 0 이하인 경우 0으로 설정
            _gold = Math.Max(value, 0);
        }
    }

	public int Exp;
	public Skill[] Skills;

	public Player(int level, string name, string _class, float atkPlayer,float dfdPlayer, int hp, int mp, int gold, Skill[] skills)
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
        Skills = skills;
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