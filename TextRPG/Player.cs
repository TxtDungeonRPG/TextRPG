using System;

public class Player
{
	int level;
	string chad;
	float atkPlayer;
	float dfdPlayer;
	int hp;
	int gold;

	public void SetPlayer(int _level, string _chad, float _atkPlayer,float _dfdPlayer, int _hp, int _gold)
	{
		level = _level;
		chad = _chad;
		atkPlayer = _atkPlayer;
		dfdPlayer = _dfdPlayer;
		hp = _hp;
		gold = _gold;
	}
}