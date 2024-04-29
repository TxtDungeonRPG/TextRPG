using System;


internal class Monster
{
    public string Name; //이름
    public float Hp; //체력
    public float Level; //레벨
    public float Attack; //공격력
    public bool IsDead; //몬스터 사망여부

    public Monster(string name, float hp, float level, float attack, bool isDead = false)
    {
        Name = name;
        Hp = hp;
        Level = level;
        Attack = attack;
        IsDead = isDead;
    }

    //몬스터 상태정보 표시
    //[번호] [레벨] [이름] [체력] 예) 1 Lv.2 미니언  HP 15
    //사망한 몬스터는 [체력] 대신 Dead 표시
    internal void PrintMonsterDescription(bool withNumber = false, int idx = 0)
    {
        if (withNumber)
        {
            Console.Write("{0} ", idx);
        }
        Console.Write(" | ");
        Console.Write($"Lv.{Level} ");
        Console.Write(Name);
        if (IsDead)
        {
            Console.WriteLine("Dead");
        }
        else
        {
            Console.WriteLine($"HP {Hp}");
        }


    }









}
