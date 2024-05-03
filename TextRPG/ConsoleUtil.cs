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

    public static int GetPrintableLength(string str)
    {
        int length = 0;
        foreach (char c in str)
        {
            if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
            {
                length += 2; // 한글과 같은 넓은 문자에 대해 길이를 2로 취급
            }
            else
            {
                length += 1; // 나머지 문자에 대해 길이를 1로 취급
            }
        }

        return length;
    }

    public static string PadRightForMixedText(string str, int totalLength)
    {
        int currentLength = GetPrintableLength(str);
        int padding = totalLength - currentLength;
        return str.PadRight(str.Length + padding);
    }
}
