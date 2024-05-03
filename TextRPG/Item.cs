using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum ItemType
{
    WEAPON,
    ARMOR
}

public class Item
{
    public string Name { get; }
    public string Desc { get; }

    private ItemType Type;

    public int Atk { get; }
    public int Def { get; }
    public int Hp { get; }
    public int Price { get; }
    public bool IsEquipped { get; private set; }
    public bool IsPurchased { get; private set; }

    public Item(string name, string desc, ItemType type, int atk, int def, int hp, int price, bool isEquipped = false, bool isPurchased = false)
    {
        Name = name;
        Desc = desc;
        Type = type;
        Atk = atk;
        Def = def;
        Hp = hp;
        Price = price;
        IsEquipped = isEquipped;
        IsPurchased = isPurchased;
    }

    // 아이템 정보를 보여줄때 타입이 비슷한게 2가지있음.
    // 1. 인벤토리에서 그냥 내가 어느 아이템 가지고 있는지 보여줄 때
    // 2. 장착관리에서 내가 어떤 아이템을 낄지 말지 결정할 때
    internal void PrintItemStatDescription(bool withNumber = false, int idx = 0)
    {
        if(IsEquipped) Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write("- ");
        if (withNumber)
        {
            Console.Write($"{idx} ");
        }
        if (IsEquipped)
        {
            Console.Write("[");
            Console.Write("E");
            Console.Write("]");
        }
        Console.Write(Name);
        Console.Write(" | ");

        if (Atk != 0) Console.Write($"공격력 {(Atk >= 0 ? "+" : "")}{Atk} ");
        if (Def != 0) Console.Write($"방어력 {(Def >= 0 ? "+" : "")}{Def} ");
        if (Hp != 0) Console.Write($"체  력 {(Hp >= 0 ? "+" : "")}{Hp} ");

        Console.Write(" | ");
        Console.WriteLine(Desc);
        Console.ResetColor();
    }


    public void PrintStoreItemDescription(bool withNumber = false, int idx = 0)
    {
        if (IsPurchased) Console.ForegroundColor = ConsoleColor.DarkGray; 
        Console.Write("- ");
        // 장착관리 전용
        if (withNumber)
        {
            Console.Write("{0} ", idx);
        }
        Console.Write(Name);
        Console.Write(" | ");

        if (Atk != 0) Console.Write($"공격력 {(Atk >= 0 ? "+" : "")}{Atk} ");
        if (Def != 0) Console.Write($"방어력 {(Def >= 0 ? "+" : "")}{Def} ");
        if (Hp != 0) Console.Write($"체  력 {(Hp >= 0 ? "+" : "")}{Hp}");

        Console.Write(" | ");
        Console.Write(Desc);
        Console.Write(" | ");

        if (IsPurchased)
        {
            Console.WriteLine("구매완료");
        }
        else
        {
            Console.WriteLine(""+Price.ToString()+" G");
        }
        Console.ResetColor ();
    }

    internal void ToggleEquipStatus()
    {
        IsEquipped = !IsEquipped;
    }

    internal void Purchase()
    {
        IsPurchased = true;
    }
}


public class Potion
{
    public string Name;
    public string Desc;
    public int Hp;
    public int Mp;
    public int Price;
    public int Count;

    public Potion(string name, string description, int hp, int mp, int price, bool isEquipped = false, bool isPurchased = false)
    {
        Name = name;
        Desc = description;
        Hp = hp;
        Mp = mp;
        Price = price;
        Count = 3;
    }
  
}