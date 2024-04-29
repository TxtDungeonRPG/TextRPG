using System;

public class ConsoleUtil
{
    // 메뉴 선택
    public static int MenuChoice(int min, int max, string message = "")
    {
        while (true)
        {
            Console.WriteLine(message);
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max)
            {
                return choice;
            }
            Console.WriteLine("잘못된 입력입니다.");
        }
    }

}
