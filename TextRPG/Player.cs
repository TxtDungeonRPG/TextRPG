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
	}


}