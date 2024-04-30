using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace TextRPG
{
    public class GameManager
    {
        private Player player;
        private List<Monster> monsterlist;
        private bool isMonsterSponed = false;

        public GameManager()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            //플레이어 초기값
            player = new Player(1, "전사", 10, 5, 100, 1500);


            //몬스터 리스트 초기값
            monsterlist = new List<Monster>();
            //monsterlist.Add(new Monster(2, "미니언", 15, 5));
            //monsterlist.Add(new Monster(5, "대포미니언", 25, 8));
            //monsterlist.Add(new Monster(3, "공허충", 10, 9));

        }

        public void StartGame()
        {
            Console.Clear();
            MainMenu();
        }

        private void MainMenu()
        {
            Console.Clear();

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이제 전투를 시작할 수 있습니다.");
            Console.WriteLine("");

            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 전투시작");
            Console.WriteLine("");
            int choice = ConsoleUtil.MenuChoice(1, 2, "원하시는 행동을 입력해주세요.");

            switch (choice)
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    StartBattleMenu();
                    break;

            }
            MainMenu();
        }

        private void StatusMenu()
        {
            
            Console.Clear();

            Console.WriteLine("■ 상태보기 ■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");
            Console.WriteLine("");
            Console.WriteLine("Lv. {0}",player.Level);
            Console.WriteLine("Chad : {0}",player.Chad);
            Console.WriteLine("공격력 : {0}",player.AtkPlayer);
            Console.WriteLine("방어력 : {0}",player.DfdPlayer);
            Console.WriteLine("체 력 : {0}",player.Hp);
            Console.WriteLine("Gold : {0} G",player.Gold);
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            int choice = ConsoleUtil.MenuChoice(0, 0, "원하시는 행동을 입력해주세요.");

            switch (choice)
            {
                case 0:
                    MainMenu();
                    break;
            }
            StatusMenu();
        }

        private void StartBattleMenu()
        {
            if (!isMonsterSponed)
            {
                //몬스터 1~4마리 랜덤 생성. 종류 중복 가능
                Random random = new Random();
                int count = random.Next(1, 5); // 몬스터 마리수
                int cnt; // 몬스터 종류
                for (int i = 0; i < count; i++)
                {
                    cnt = random.Next(1, 4);
                    switch (cnt)
                    {
                        case 1:
                            monsterlist.Add(new Monster(2, "미니언", 15, 5));
                            break;
                        case 2:
                            monsterlist.Add(new Monster(5, "대포미니언", 25, 8));
                            break;
                        case 3:
                            monsterlist.Add(new Monster(3, "공허충", 10, 9));
                            break;
                    }
                }
                isMonsterSponed = true;
            }
            
            Console.Clear();

            Console.WriteLine("■ Battle!! ■");
            Console.WriteLine("");
            //Console.WriteLine("Lv.2 미니언  HP 15");
            //Console.WriteLine("Lv.5 대포미니언 HP 25");
            //Console.WriteLine("LV.3 공허충 HP 10");
            for (int i = 0; i < monsterlist.Count; i++)
            {
                monsterlist[i].PrintMonsterDescription(false, i + 1);
            }
            Console.WriteLine("");
            Console.WriteLine("[내정보]");
            //Console.WriteLine("Lv.1  Chad (전사) ");
            //Console.WriteLine("HP 100/100");
            Console.Write($"Lv.{player.Level} ");
            Console.WriteLine($"Chad ({player.Chad})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}"); 
            Console.WriteLine("");
            Console.WriteLine("1. 공격");
            Console.WriteLine("0. 도망가기");
            Console.WriteLine("");

            int choice = ConsoleUtil.MenuChoice(0, 1, "원하시는 행동을 입력해주세요.");

            switch (choice)
            {
                case 1:
                    AttackMenu();
                    break;
                case 0:
                    monsterlist.Clear();
                    isMonsterSponed = false;
                    MainMenu();
                    break;
            }
            StartBattleMenu();

        }

        private void AttackMenu()
        {
            Console.Clear();

            Console.WriteLine("■ Battle!! ■");
            Console.WriteLine("");
            for(int i = 0; i < monsterlist.Count; i++) 
            {
                monsterlist[i].PrintMonsterDescription(true, i + 1);
            }        
            Console.WriteLine("");
            Console.WriteLine("[내정보]");
            Console.Write($"Lv.{player.Level} ");
            Console.WriteLine($"Chad ({player.Chad})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
            Console.WriteLine("");
            Console.WriteLine("0. 취소");
            Console.WriteLine("");

            int choice = ConsoleUtil.MenuChoice(0, 3, "대상을 선택해주세요.");

            switch (choice)
            {
                case 1:
                case 2:
                case 3:
                    Attack(choice);
                    break;
                case 0:
                    StartBattleMenu();
                    break;
            }
            AttackMenu();
        }

        private void Attack(int choiceEnemy)
        {
            Console.Clear();

            Console.WriteLine("■ Battle!! ■");
            Console.WriteLine("");
            Console.WriteLine("Chad 의 공격!");
            Console.WriteLine("Lv.3 공허충 을(를) 맞췄습니다. [데미지 : 10]");
            Console.WriteLine("");
            Console.WriteLine("Lv.3 공허충");
            Console.WriteLine("HP 10 -> Dead");
            Console.WriteLine("HP 100/100");
            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            Console.WriteLine("");

            int choice = ConsoleUtil.MenuChoice(0, 0);

            switch (choice)
            {
                case 0:
                    EnemyPhase();
                    break;
            }
        }

        private void EnemyPhase()
        {
            Console.Clear();

            Console.WriteLine("■ Battle!! ■");
            Console.WriteLine("");
            Console.WriteLine("Lv.2 미니언 의 공격!");
            Console.WriteLine("Chad 을(를) 맞췄습니다.  [데미지 : 6]");
            Console.WriteLine("");
            Console.WriteLine("Lv.1 Chad");
            Console.WriteLine("HP 100 -> 94");
            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            Console.WriteLine("");
            int choice = ConsoleUtil.MenuChoice(0, 0);

            switch (choice)
            {
                case 0:
                    AttackMenu();
                    break;
            }
        }

        private void ResultBattle()
        {
            // 모든 몬스터가 Dead 상태가 된다면 게임이 종료됩니다. → Victory
            Victory();
            // 내 체력이 0이 되면 게임이 종료됩니다. → Lose
            Lose();
        }

        private void Victory()
        {
            Console.Clear();

            Console.WriteLine("■ Battle!! - Result ■");
            Console.WriteLine("");
            Console.WriteLine("Victory");
            Console.WriteLine("");
            Console.WriteLine("던전에서 몬스터 3마리를 잡았습니다.");
            Console.WriteLine("");
            Console.WriteLine("Lv.1 Chad");
            Console.WriteLine("HP 100 -> 74");
            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            Console.WriteLine("");
            int choice = ConsoleUtil.MenuChoice(0, 0);

            switch (choice)
            {
                case 0:
                    MainMenu();
                    break;
            }
            Victory();
        }

        private void Lose()
        {
            Console.Clear();

            Console.WriteLine("■ Battle!! - Result ■");
            Console.WriteLine("");
            Console.WriteLine("You Lose");
            Console.WriteLine("");
            Console.WriteLine("던전에서 몬스터 3마리를 잡았습니다.");
            Console.WriteLine("");
            Console.WriteLine("Lv.1 Chad");
            Console.WriteLine("HP 100 -> 0");
            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            Console.WriteLine("");
            int choice = ConsoleUtil.MenuChoice(0, 0);

            switch (choice)
            {
                case 0:
                    MainMenu();
                    break;
            }
            Lose();
        }


    }


    class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
    }
}
