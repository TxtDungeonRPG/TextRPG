using System;

public class Player
{
	public int Level;
    public string Name;
    public string Class;
    public float AtkPlayer;
    public float DfdPlayer;
    public int Hp;
	public int MaxHp;
    public int Gold;
	public int Exp;

	public Player(int level, string name, string _class, float atkPlayer,float dfdPlayer, int hp, int gold)
	{
		Level = level;
		Name = name;
        Class = _class;
		AtkPlayer = atkPlayer;
		DfdPlayer = dfdPlayer;
		Hp = hp;
		MaxHp = hp;
		Gold = gold;
		Exp = 0;
	}

	public bool LevelUpcheck()
	{
        int[] needExp = { 1, 2, 65, 100 };

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


}