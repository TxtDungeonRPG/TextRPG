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
        private List<Item> inventory;
        private Stage currentStage; //현재 스테이지 변수 추가
    

        public GameManager()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            //몬스터 리스트 초기값
            monsterlist = new List<Monster>();
            PlayerCreate();//캐릭터생성
            inventory = new List<Item>();

            //장착기능 잘 되는지 확인하기 위해 임시로 넣어둔 아이템입니다. 나중에 지우셔도 무방합니다! - 김신우
            inventory.Add(new Item("무쇠갑옷", "튼튼한 갑옷", ItemType.ARMOR, 0, 5, 0, 500));
            inventory.Add(new Item("낡은 검", "낡은 검", ItemType.WEAPON, 2, 0, 0, 1000));
            inventory.Add(new Item("골든 헬름", "희귀한 투구", ItemType.ARMOR, 0, 9, 0, 2000));
        
            //현재 스테이지 초기화
            currentStage = new Stage(1);
        
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

            //현재 진행 중인 스테이지 추가
            Console.WriteLine($"현재 진행 중인 스테이지: {currentStage.CurrentFloor}층");
            Console.WriteLine("");
            Console.WriteLine("1. 상태보기");
            Console.WriteLine($"2. 전투시작 (현재진행: {currentStage.CurrentFloor}층)");
            Console.WriteLine("3. 인벤토리");
            Console.WriteLine("4. 퀘스트");
            Console.WriteLine("");
            int choice = ConsoleUtil.MenuChoice(1, 4, "원하시는 행동을 입력해주세요.");

            switch (choice)
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    StartBattleMenu();
                    break;
                case 3:
                    InventoryMenu();
                    break;
                case 4:
                    StartQuestMenu();
                    break;

            }
            MainMenu();
        }

        private void InventoryMenu()
        {
            Console.Clear();

            Console.WriteLine("■ 인벤토리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].PrintItemStatDescription();
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("");
            int choice = ConsoleUtil.MenuChoice(0, 1, "원하시는 행동을 입력해주세요.");
            switch (choice)
            {
                case 0: 
                    MainMenu();
                    break;
                case 1:
                    EquipMenu();
                    break;
            }
        }

        private void EquipMenu()
        {
            Console.Clear();

            Console.WriteLine("■ 인벤토리 - 장착 관리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < inventory.Count; i++)
            {
                inventory[i].PrintItemStatDescription(true, i + 1); // 나가기가 0번 고정, 나머지가 1번부터 배정
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");

            int choice = ConsoleUtil.MenuChoice(0, inventory.Count, "원하시는 행동을 입력해주세요.");

            switch (choice)
            {
                case 0:
                    InventoryMenu();
                    break;
                default:
                    inventory[choice - 1].ToggleEquipStatus();
                    EquipMenu();
                    break;
            }
        }

        private void StatusMenu()
        {
            
            Console.Clear();

            Console.WriteLine("■ 상태보기 ■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");
            Console.WriteLine("");
            Console.WriteLine("Lv. {0}",player.Level);
            Console.WriteLine("{0} ({1})", player.Name, player.Class);

            // TODO : 능력치 강화분을 표현하도록 변경

            int bonusAtk = inventory.Select(item => item.IsEquipped ? item.Atk : 0).Sum();
            int bonusDef = inventory.Select(item => item.IsEquipped ? item.Def : 0).Sum();
            int bonusHp = inventory.Select(item => item.IsEquipped ? item.Hp : 0).Sum();

            Console.Write("공격력 : " + (player.AtkPlayer + bonusAtk).ToString());
            if (bonusAtk > 0) Console.WriteLine($" (+{bonusAtk})"); else Console.WriteLine("");
            Console.Write("방어력 : " + (player.DfdPlayer + bonusDef).ToString());
            if (bonusDef > 0) Console.WriteLine($" (+{bonusDef})"); else Console.WriteLine("");
            Console.Write("체 력 : " + (player.Hp + bonusHp).ToString());
            if (bonusHp > 0) Console.WriteLine($" (+{bonusHp})"); else Console.WriteLine("");
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
                    cnt = random.Next(1, 6);
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
                        case 4:
                            monsterlist.Add(new Monster(1, "칼날부리", 8, 7));
                            break;
                        case 5:
                            monsterlist.Add(new Monster(5, "블루", 30, 10));
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
                    player.Exp += (int)monsterlist[choiceEnemy - 1].Level;
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

            //레벨업 확인
            if (player.LevelUpcheck()){ 
                Console.SetCursorPosition(0, 6);
                Console.WriteLine("Lv.{0} {1} -> Lv.{2} {1}", player.Level-1, player.Name, player.Level);
                Console.SetCursorPosition(0, 11);
            }

            int choice = ConsoleUtil.MenuChoice(0, 0);

            switch (choice)
            {
                case 0:
                    monsterlist.Clear();
                    isMonsterSpawned=false;
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
            Console.WriteLine("Lv{0} {1}", player.Level, player.Name);
            Console.WriteLine("HP {0} -> 0", startHp);
            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            Console.WriteLine("");
            int choice = ConsoleUtil.MenuChoice(0, 0);

            switch (choice)
            {
                case 0:
                    monsterlist.Clear();
                    isMonsterSpawned=false;
                    MainMenu();
                    break;
            }
            Lose();
        }
        private void StartQuestMenu()
        {


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
