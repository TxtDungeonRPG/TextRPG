using System;

public class Player
{
	public int Level;
    public string Chad;
    public float AtkPlayer;
    public float DfdPlayer;
    public int Hp;
    public int Gold;

	public Player(int level, string chad, float atkPlayer,float dfdPlayer, int hp, int gold)
	{
		Level = level;
		Chad = chad;
		AtkPlayer = atkPlayer;
		DfdPlayer = dfdPlayer;
		Hp = hp;
		Gold = gold;
	}
}