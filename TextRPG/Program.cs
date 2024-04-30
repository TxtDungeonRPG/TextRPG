using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Xml.Serialization;

namespace TextRPG
{
    public class GameManager
    {
        private Player player;
        private List<Monster> monsterlist;
        private bool isMonsterSpawned = false;
        private int startHp;

        public GameManager()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            //몬스터 리스트 초기값
            monsterlist = new List<Monster>();
            PlayerCreate();//캐릭터생성
        }

        private void PlayerCreate()
        {
            string playerName = "홍길동";
            Console.Clear();
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("원하시는 이름을 설정해주세요");
            playerName = Console.ReadLine();
            if (playerName == "")
            {
                playerName = "홍길동";
            }
            Console.Clear();
            Console.WriteLine("■ 직업 선택 ■");
            Console.WriteLine("1. 전사\n2. 마법사\n3. 도적\n4. 해적");

            int choice = ConsoleUtil.MenuChoice(0, 4, "원하시는 직업을 입력해주세요.");
            switch (choice)
            {
                case 1:
                    player = new Player(1, playerName, "전사", 8, 7, 100, 1500);
                    break;
                case 2:
                    player = new Player(1, playerName, "마법사", 12, 3, 100, 1500);
                    break;
                case 3:
                    player = new Player(1, playerName, "도적", 11, 4, 100, 2000);
                    break;
                case 4:
                    player = new Player(1, playerName, "해적", 10, 5, 100, 1700);
                    break;
            }
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
            Console.WriteLine("{0} ({1})", player.Name, player.Class);
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
            if (!isMonsterSpawned)
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
                isMonsterSpawned = true;
            }
            startHp = player.Hp; //전투 시작당시의 체력. Victory(), Lose()에서 사용하기 위한 값.
            Console.Clear();

            Console.WriteLine("■ Battle!! ■");
            Console.WriteLine("");
            for (int i = 0; i < monsterlist.Count; i++)
            {
                monsterlist[i].PrintMonsterDescription(false, i + 1);
            }
            Console.WriteLine("");
            Console.WriteLine("[내정보]");
            Console.Write($"Lv.{player.Level} ");
            Console.WriteLine($"{player.Name} ({player.Class})");
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
                    isMonsterSpawned = false;
                    MainMenu();
                    break;
            }
            StartBattleMenu();

        }

        private void AttackMenu()
        {
            int choice;
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
            Console.WriteLine($"{player.Name} ({player.Class})");
            Console.WriteLine($"HP {player.Hp}/{player.MaxHp}");
            Console.WriteLine("");
            Console.WriteLine("0. 취소");
            Console.WriteLine("");
            do
            {
                choice = ConsoleUtil.MenuChoice(0, monsterlist.Count, "대상을 선택해주세요.");
                if (choice != 0 && monsterlist[choice - 1].IsDead)
                {
                    Console.WriteLine("이미 죽은 몬스터입니다");
                }
            }
            while (choice != 0 && monsterlist[choice - 1].IsDead);

            if(choice == 0)
            {
                StartBattleMenu();

            }else
            {
                Attack(choice);
            }

        }

        private void Attack(int choiceEnemy)
        {
            Random random = new Random();
            int damage = (int)player.AtkPlayer+random.Next((int)Math.Floor(player.AtkPlayer*(-0.1)), (int)Math.Ceiling(player.AtkPlayer*0.1));
            Console.Clear();
            Console.WriteLine("■ Battle!! ■");
            Console.WriteLine("");
            Console.WriteLine("{0}의 공격!",player.Name);
            int attackChance = random.Next(100);
            if (attackChance < 90)
            {
                if (attackChance < 15)
                {
                    damage = (int)(damage * 1.6);
                    Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}] - 치명타 공격!!",
                        monsterlist[choiceEnemy - 1].Level, monsterlist[choiceEnemy - 1].Name, damage);

                }
                else
                {
                    Console.WriteLine("Lv.{0} {1} 을(를) 맞췄습니다. [데미지 : {2}]", 
                                        monsterlist[choiceEnemy - 1].Level, monsterlist[choiceEnemy - 1].Name, damage);
                }
                Console.WriteLine("");
                Console.WriteLine("Lv.{0} {1}", monsterlist[choiceEnemy - 1].Level, monsterlist[choiceEnemy - 1].Name);
                if (monsterlist[choiceEnemy - 1].Hp - damage <= 0)
                {
                    Console.WriteLine("HP {0} -> Dead", monsterlist[choiceEnemy - 1].Hp);
                    monsterlist[choiceEnemy - 1].IsDead = true;
                    monsterlist[choiceEnemy - 1].Hp = 0;
                }
                else
                {
                    Console.WriteLine("HP {0} -> {1}", monsterlist[choiceEnemy - 1].Hp, monsterlist[choiceEnemy - 1].Hp - damage);
                    monsterlist[choiceEnemy - 1].Hp -= damage;
                }
            }
            else
            {
                Console.WriteLine("Lv.{0} {1} 을(를) 공격했지만 아무일도 일어나지 않았습니다.", monsterlist[choiceEnemy - 1].Level, monsterlist[choiceEnemy - 1].Name);
            }
            

            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            Console.WriteLine("");

            int choice = ConsoleUtil.MenuChoice(0, 0);

            switch (choice)
            {
                case 0:
                    // 몬스터가 모두 죽은경우 승리
                    foreach (var monster in monsterlist)
                    {
                        if (!monster.IsDead)
                        {
                            EnemyPhase();
                        }
                    }
                    Victory();
                    break;
            }
        }

        private void EnemyPhase()
        {
            
            Monster? monster = null;

            for (int i = 0; i < monsterlist.Count; i++)
            {
                // 몬스터가 살아있고 공격하지 않은 경우 공격할 몬스터 선택
                if (!monsterlist[i].IsDead)
                {
                    if (!monsterlist[i].IsAttack)
                    {
                        monster = monsterlist[i];
                        monster.IsAttack = true;
                        break;
                    }                 
                }             
            }


            // 공격할 몬스터가 있는 경우
            if (monster != null)
            {
                Console.Clear();

                Console.WriteLine("■ Battle!! ■");
                Console.WriteLine("");

                Console.WriteLine($"Lv.{monster.Level} {monster.Name} 의 공격!");
                Console.WriteLine($"{player.Name} 을(를) 맞췄습니다.  [데미지 : {monster.Attack}]");
                Console.WriteLine("");
                Console.WriteLine($"Lv.{player.Level} {player.Name}");
                Console.WriteLine($"HP {player.Hp} -> {player.Hp - monster.Attack}");

                player.Hp -= (int)monster.Attack;
            }
            else
            {
                // 공격할 몬스터가 없는 경우 AttackMenu() 이동, 공격 여부 초기화
                monsterlist.ForEach(x => x.IsAttack = false);
                AttackMenu();
            }

            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            Console.WriteLine("");

            int choice = ConsoleUtil.MenuChoice(0, 0);
            
            switch (choice)
            {
                case 0:
                    //캐릭터 체력이 0 이하가 된경우 패배
                    if(player.Hp <= 0)
                    {
                        Lose();
                    }
                    else
                    {
                        EnemyPhase();
                    }
                    break;
            }
        }

        private void ResultBattle()
        {

            bool allDead = true; // 몬스터가 모두 죽었는지 판단
            
            // 몬스터가 한 마리라도 살아있으면, false
            for(int i=0; i<monsterlist.Count; i++)
            {
                if (!monsterlist[i].IsDead) allDead = false;
            }
            // 모든 몬스터가 Dead 상태가 된다면 게임이 종료됩니다. → Victory
            if (allDead)
            {
                Victory();
            }
            // 내 체력이 0이 되면 게임이 종료됩니다. → Lose
            if(player.Hp <= 0)
            {
                Lose();
            }                

        }

        private void Victory()
        {
            Console.Clear();

            Console.WriteLine("■ Battle!! - Result ■");
            Console.WriteLine("");
            Console.WriteLine("Victory");
            Console.WriteLine("");
            Console.WriteLine("던전에서 몬스터 {0}마리를 잡았습니다.", monsterlist.Count);
            Console.WriteLine("");
            Console.WriteLine("Lv.{0} {1}", player.Level, player.Name);
            Console.WriteLine("HP {0} -> {1}", startHp, player.Hp); //전투시작 당시 체력값 받아와야함!
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
            Console.WriteLine("Lv. Chad");
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
        }}



    class Program
   
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
        }
    }
}
