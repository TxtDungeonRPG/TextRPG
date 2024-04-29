using System;

public class Player
{
	public int Level;
	public string Chad;
	public float AtkPlayer;
	public float DfdPlayer;
	public int Hp;
	public int Gold;

	public void SetPlayer(int _level, string _chad, float _atkPlayer,float _dfdPlayer, int _hp, int _gold)
	{
		Level = _level;
		Chad = _chad;
		AtkPlayer = _atkPlayer;
		DfdPlayer = _dfdPlayer;
		Hp = _hp;
		Gold = _gold;
	}
}