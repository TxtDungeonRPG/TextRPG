using System;
using System.Collections.Generic;
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
        private List<Item> storeInventory;
        private List<Quest> QuestList;
        private Potion potion;
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
            //inventory.Add(new Item("무쇠갑옷", "튼튼한 갑옷", ItemType.ARMOR, 0, 5, 0, 500));
            //inventory.Add(new Item("낡은 검", "낡은 검", ItemType.WEAPON, 2, 0, 0, 1000));
            //inventory.Add(new Item("골든 헬름", "희귀한 투구", ItemType.ARMOR, 0, 9, 0, 2000));
            //상점 아이템 목록
            storeInventory = new List<Item>();
            storeInventory.Add(new Item("무쇠갑옷", "튼튼한 갑옷", ItemType.ARMOR, 0, 5, 0, 500));
            storeInventory.Add(new Item("낡은 검", "낡은 검", ItemType.WEAPON, 2, 0, 0, 1000));
            storeInventory.Add(new Item("골든 헬름", "희귀한 투구", ItemType.ARMOR, 0, 9, 0, 2000));

            QuestList = new List<Quest>();
            QuestList.Add(new Quest1());
            QuestList.Add(new Quest2());
            QuestList.Add(new Quest3());
            potion = new Potion("회복 포션", "체력을 회복시킵니다.", 30, 100);
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

            Skill[] skills = [new Skill("알파 스트라이크", 10, 2, 1), new Skill("더블 스트라이크", 15, 1.5f, 2)];

            int choice = ConsoleUtil.MenuChoice(0, 4, "원하시는 직업을 입력해주세요.");

            switch (choice)
            {
                case 1:
                    player = new Player(1, playerName, "전사", 8, 7, 100, 30, 1500, skills);
                    break;
                case 2:
                    player = new Player(1, playerName, "마법사", 12, 3, 100, 80, 1500, skills);
                    break;
                case 3:
                    player = new Player(1, playerName, "도적", 11, 4, 100, 50, 2000, skills);
                    break;
                case 4:
                    player = new Player(1, playerName, "해적", 10, 5, 100, 50, 1700, skills);
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
            Console.WriteLine("3. 인벤토리");
            Console.WriteLine("4. 퀘스트");
            Console.WriteLine("5. 회복 아이템");
            Console.WriteLine("6. 아이템 상점");
            Console.WriteLine("");
            int choice = ConsoleUtil.MenuChoice(1, 6, "원하시는 행동을 입력해주세요.");

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
                case 5:
                    PotionMenu();
                    break;
                case 6:
                    StoreMenu();
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

        private void StoreMenu()
        {
            Console.Clear();

            Console.WriteLine("■ 상점 ■");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player.Gold.ToString() + " G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < storeInventory.Count; i++)
            {
                storeInventory[i].PrintStoreItemDescription();
            }
            Console.WriteLine("");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");
            int choice = ConsoleUtil.MenuChoice(0, 1, "원하시는 행동을 입력해주세요.");
            switch (choice)
            {
                case 0:
                    MainMenu();
                    break;
                case 1:
                    PurchaseMenu();
                    break;
            }
        }

        private void PurchaseMenu(string? prompt = null)
        {
            if (prompt != null)
            {
                // 1초간 메시지를 띄운 다음에 다시 진행
                Console.Clear();
                Console.WriteLine(prompt);
                Thread.Sleep(1000);
            }

            Console.Clear();

            Console.WriteLine("■ 상점 ■");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine(player.Gold.ToString() + " G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < storeInventory.Count; i++)
            {
                storeInventory[i].PrintStoreItemDescription(true, i + 1);
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            int choice = ConsoleUtil.MenuChoice(0, storeInventory.Count, "원하시는 행동을 입력해주세요.");

            switch (choice)
            {
                case 0:
                    StoreMenu();
                    break;
                default:
                    // 1 : 이미 구매한 경우
                    if (storeInventory[choice - 1].IsPurchased) // index 맞추기
                    {
                        PurchaseMenu("이미 구매한 아이템입니다.");
                    }
                    // 2 : 돈이 충분해서 살 수 있는 경우
                    else if (player.Gold >= storeInventory[choice - 1].Price)
                    {
                        player.Gold -= storeInventory[choice - 1].Price;
                        storeInventory[choice - 1].Purchase();
                        inventory.Add(storeInventory[choice - 1]);
                        PurchaseMenu();
                    }
                    // 3 : 돈이 모자라는 경우
                    else
                    {
                        PurchaseMenu("Gold가 부족합니다.");
                    }
                    break;
            }
        }

        private void StatusMenu()
        {

            Console.Clear();

            Console.WriteLine("■ 상태보기 ■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");
            Console.WriteLine("");
            Console.WriteLine("Lv. {0}", player.Level);
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
            Console.WriteLine("마 나 : " + (player.Mp).ToString());
            Console.WriteLine("Gold : {0} G", player.Gold);
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

        private void PotionMenu()
        {
            Console.Clear();

            Console.WriteLine("■ 회복 ■");
            Console.WriteLine("포션을 사용하면 체력을 {0} 회복할 수 있습니다. (남은 포션: {1})", potion.Hp, potion.Count);
            Console.WriteLine("");
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("");

            int choice = ConsoleUtil.MenuChoice(0, 1, "원하시는 행동을 입력해주세요.");
            switch (choice)
            {
                case 1:
                    if (potion.Count > 0)
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("회복을 완료했습니다.");
                        player.Hp += potion.Hp;
                        if (player.Hp > player.MaxHp) player.Hp = player.MaxHp;
                        potion.Count--;
                        Console.WriteLine("");
                        Console.WriteLine("0. 다음");
                        Console.WriteLine("");
                        ConsoleUtil.MenuChoice(0, 0);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("포션이 부족합니다.");
                        Console.WriteLine("");
                        Console.WriteLine("0. 다음");
                        Console.WriteLine("");
                        ConsoleUtil.MenuChoice(0, 0);
                    }
                    break;
                case 0:
                    MainMenu();
                    break;
            }
            PotionMenu();
        }

        private void StartBattleMenu()
        {
            if (!isMonsterSpawned)
            {
                //전투 시작당시의 체력. Victory(), Lose()에서 사용하기 위한 값.
                startHp = player.Hp; 
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
            Console.Clear();

            Console.WriteLine("■ Battle!! ■");
            Console.WriteLine("");
            for (int i = 0; i < monsterlist.Count; i++)
            {
                monsterlist[i].PrintMonsterDescription(false, i + 1);
            }
            Console.WriteLine("");
            player.PlayerInfo();
            Console.WriteLine("");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬");
            Console.WriteLine("0. 도망가기");
            Console.WriteLine("");

            int choice = ConsoleUtil.MenuChoice(0, 2, "원하시는 행동을 입력해주세요.");

            switch (choice)
            {
                case 1:
                    AttackMenu();
                    break;
                case 2:
                    SkillMenu();
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
            for (int i = 0; i < monsterlist.Count; i++)
            {
                monsterlist[i].PrintMonsterDescription(true, i + 1);
            }
            Console.WriteLine("");
            player.PlayerInfo();
            Console.WriteLine("");
            Console.WriteLine("0. 취소");
            Console.WriteLine("");

            IsCheckDeadMonster(out choice);

            if (choice == 0)
            {
                StartBattleMenu();

            }
            else
            {
                Attack(choice);
            }

        }

        private void Attack(int choiceEnemy)
        {
            Random random = new Random();
            int damage = (int)player.AtkPlayer + random.Next((int)Math.Floor(player.AtkPlayer * (-0.1)), (int)Math.Ceiling(player.AtkPlayer * 0.1));
            Console.Clear();
            Console.WriteLine("■ Battle!! ■");
            Console.WriteLine("");
            Console.WriteLine("{0}의 공격!", player.Name);
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

                    QuestList[0].Changenum(1);


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

        // 스킬 선택 메뉴
        private void SkillMenu()
        {
            Console.Clear();

            Console.WriteLine("■ Battle!! ■");
            Console.WriteLine("");
            for (int i = 0; i < monsterlist.Count; i++)
            {
                monsterlist[i].PrintMonsterDescription(false, i + 1);
            }
            Console.WriteLine("");
            player.PlayerInfo();
            Console.WriteLine("");

            for (int i = 0; i < player.Skills.Length; i++)
            {
                Skill skill = player.Skills[i];
                // 대상이 한명인 경우
                if (skill.DamageAmount == 1)
                {
                    Console.WriteLine($"{i + 1}. {skill.Name} - MP {skill.ExpendMp}");
                    Console.WriteLine($"   공격력 * {skill.DamageScale} 로 하나의 적을 공격합니다.");
                }
                // 대상이 여러명인 경우
                else if (skill.DamageAmount >= 2)
                {
                    Console.WriteLine($"{i + 1}. {skill.Name} - MP {skill.ExpendMp}");
                    Console.WriteLine($"   공격력 * {skill.DamageScale} 로 {skill.DamageAmount}명의 적을 랜덤으로 공격합니다.");

                }
            }
            Console.WriteLine("0. 취소");

            int choice = ConsoleUtil.MenuChoice(0, player.Skills.Length);

            if (choice == 0)
            {
                StartBattleMenu();
            }
            else
            {
                // 스킬을 사용할 MP가 부족한 경우 
                if(player.Mp < player.Skills[choice - 1].ExpendMp)
                {
                    Console.Clear();
                    Console.WriteLine("해당스킬을 사용할 MP가 부족합니다.");
                    Thread.Sleep(1000);
                    SkillMenu();
                }

                // 단일 스킬을 선택한 경우 몬스터 선택메뉴 이동
                if (player.Skills[choice - 1].DamageAmount == 1)
                {
                    SkillMenu(choice);
                }
                // 랜덤 스킬을 선택한 경우 스킬사용
                else
                {
                    Skill(choice);
                }


            }
        }

        // 단일 스킬을 선택한경우 몬스터 선택 활성화
        private void SkillMenu(int choiceSkill)
        {
            Console.Clear();

            Console.WriteLine("■ Battle!! ■");
            Console.WriteLine("");
            for (int i = 0; i < monsterlist.Count; i++)
            {
                monsterlist[i].PrintMonsterDescription(true, i + 1);
            }
            Console.WriteLine("");
            player.PlayerInfo();
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("0. 취소");

            int choice;

            IsCheckDeadMonster(out choice);

            if (choice == 0)
            {
                SkillMenu();
            }
            else
            {
                Skill(choiceSkill, choice);
            }
        }


        private void Skill(int skillIndex, int monsterIndex = 0)
        {
            Skill useSkill = player.Skills[skillIndex - 1];
            float skillDamage = player.AtkPlayer * useSkill.DamageScale;
            // MP 소모
            player.Mp -= useSkill.ExpendMp;

            Console.Clear();
            Console.WriteLine("■ Battle!! ■");
            Console.WriteLine("");
            Console.WriteLine($"{player.Name}의 {useSkill.Name}!");

            // 한마리 공격하는 경우 
            if (useSkill.DamageAmount == 1)
            {
                Monster selectedMonster = monsterlist[monsterIndex - 1];

                Console.WriteLine($"Lv.{selectedMonster.Level}  {selectedMonster.Name} 을(를) 맞췄습니다. [데미지 : {skillDamage}]");
                Console.WriteLine("");
                Console.WriteLine($"Lv.{selectedMonster.Level}  {selectedMonster.Name}");

                if (selectedMonster.Hp - skillDamage <= 0)
                {
                    // 몬스터가 죽은 경우
                    Console.WriteLine($"HP {selectedMonster.Hp} -> Dead");
                    selectedMonster.IsDead = true;
                    selectedMonster.Hp = 0;
                    // 경험치
                    player.Exp += (int)selectedMonster.Level;
                    // 퀘스트
                    QuestList[0].Changenum(1);
                }
                else
                {
                    Console.WriteLine($"HP {selectedMonster.Hp} -> {selectedMonster.Hp - skillDamage}");
                    selectedMonster.Hp -= skillDamage;
                }

            }
            // 여러 마리 공격하는 경우
            else
            {
                Random random = new Random();

                // 랜덤으로 공격할 몬스터의 정한다. 
                List<int> selectedIndexList = GetRandomMonsterIdx(monsterlist, useSkill.DamageAmount);


        
                // 랜덤으로 몬스터를 맞추고 표시
                foreach (int index in selectedIndexList) 
                {
                    Console.WriteLine($"Lv.{monsterlist[index].Level}  {monsterlist[index].Name} 을(를) 맞췄습니다. [데미지 : {skillDamage}]");

                }
                Console.WriteLine("");
                foreach (int index in selectedIndexList)
                {
                    Console.WriteLine($"Lv.{monsterlist[index].Level}  {monsterlist[index].Name}");

                    if (monsterlist[index].Hp - skillDamage <= 0)
                    {
                        // 몬스터가 죽은 경우

                        Console.WriteLine($"HP {monsterlist[index].Hp} -> Dead");
                        monsterlist[index].IsDead = true;
                        monsterlist[index].Hp = 0;
                        // 경험치
                        player.Exp += (int)monsterlist[index].Level;
                        // 퀘스트
                        QuestList[0].Changenum(1);

                    }
                    else
                    {
                        Console.WriteLine($"HP {monsterlist[index].Hp} -> {monsterlist[index].Hp - skillDamage}");
                        monsterlist[index].Hp -= skillDamage;
                    }
                }
               
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
                // 공격할 몬스터가 없는 경우 StartBattleMenu() 이동, 공격 여부 초기화
                monsterlist.ForEach(x => x.IsAttack = false);
                StartBattleMenu();
            }

            Console.WriteLine("");
            Console.WriteLine("0. 다음");
            Console.WriteLine("");

            int choice = ConsoleUtil.MenuChoice(0, 0);

            switch (choice)
            {
                case 0:
                    //캐릭터 체력이 0 이하가 된경우 패배
                    if (player.Hp <= 0)
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
            
            //레벨업 확인
            if (player.LevelUpcheck()) 
            {
                Console.SetCursorPosition(0, 6);
                Console.WriteLine("Lv.{0} {1} -> Lv.{2} {1}", player.Level - 1, player.Name, player.Level);
                Console.SetCursorPosition(0, 11);
            }

            //획득 아이템
            DropItem();
            Console.WriteLine("0. 다음");
            Console.WriteLine("");
            int choice = ConsoleUtil.MenuChoice(0, 0);

            switch (choice)
            {
                case 0:
                    monsterlist.Clear();
                    isMonsterSpawned = false;
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
                    isMonsterSpawned = false;
                    MainMenu();
                    break;
            }
            Lose();
        }
        private void StartQuestMenu()
        {
            int count = 0;

            foreach(Quest exquest in QuestList)
            {
                if (!exquest.IsDone)
                {
                    exquest.QuestProgress(inventory,player);
                    exquest.QuestClearCheck();
                }
            }


            Console.Clear();
            Console.WriteLine("■ Quest ■\n");
            foreach(Quest exquest in QuestList)
            {
                if (!exquest.QuestOut)
                {
                    count++;
                    if (exquest.IsDone)
                    {
                        Console.WriteLine($"{count}. {exquest.QuestTitle} (완료!!)");
                    }
                    else
                    {
                        Console.WriteLine($"{count}. {exquest.QuestTitle}");
                    }
                }

            }

            int choice = ConsoleUtil.MenuChoice(0, count, "\n0. 돌아가기\n원하시는 퀘스트를 입력해주세요.");

            for (int i = 0; i < choice; i++)//이미 달성한 퀘스트는 표기되지 않아 퀘스트리스트의 숫자와 맞지 않음, 이 반복문을 통해 그 부분을 줄여줌
            {
                if (QuestList[i].QuestOut)//반복문을 퀘스트 상위(0번째)부터 고른 숫자까지 진행하며, 이미 달성한 퀘스트가 있는경우 선택한 숫자 증가
                {
                    choice++;
                }
            }

            if (choice == 0)
            {
                MainMenu();
            }
            else
            {
                QuestDescription(choice);
            }

        }

        private void QuestDescription(int questChoice)
        {

            Console.Clear();
            Console.WriteLine("■ Quest ■\n");
            Console.WriteLine($"{QuestList[questChoice-1].QuestTitle}\n");
            Console.WriteLine($"{QuestList[questChoice - 1].QuestScript}\n");
            Console.WriteLine($" - {QuestList[questChoice - 1].QuestWhatToDo} ({QuestList[questChoice - 1].QuestTracker}/{QuestList[questChoice - 1].QuestTarget})\n");
            Console.WriteLine($" -  보상  - \n");
            Console.WriteLine($"{QuestList[questChoice - 1].Reward}\n");

            //퀘스트의 완료 여부에 따라 선택지 변경
            if (QuestList[questChoice-1].IsDone) 
            {
                Console.WriteLine("1. 보상 받기");
                Console.WriteLine("2. 돌아가기");
            }
            else
            {
                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");
            }

            int choice = ConsoleUtil.MenuChoice(1, 2, "\n원하시는 행동을 입력해주세요.");//숫자 수정하기

            switch (choice)
            {
                case 1:
                    if (QuestList[questChoice - 1].IsDone)//보상 획득
                    {
                        QuestList[questChoice - 1].QuestDone(inventory,player);
                    }
                    else
                    {
                        Console.WriteLine("퀘스트를 수락했습니다\n엔터를 눌러 계속...");
                        Console.ReadLine();
                    }
                        break;
                case 2:
                    StartQuestMenu();
                    break;
            }
        }

        // 랜덤으로 몬스터를 선택
        public List<int> GetRandomMonsterIdx(List<Monster> monsterList, int n)
        {
            Random rand = new Random();
            List<int> aliveMonsterIndexList = new List<int>();
            List<int> randomIndexList = new List<int>();

            // 죽은 몬스터의 인덱스를 제외한 유효한 인덱스 목록 생성
            for (int i = 0; i < monsterList.Count; i++)
            {
                if (!monsterList[i].IsDead)
                {
                    aliveMonsterIndexList.Add(i);
                }
            }

            // 리스트의 크기가 선택할 요소의 개수보다 작은 경우 리스트의 크기만큼 선택
            if (n > aliveMonsterIndexList.Count) n = aliveMonsterIndexList.Count;

            // 중복되지 않는 인덱스 선택
            while (randomIndexList.Count < n)
            {
                int randomIndex = aliveMonsterIndexList[rand.Next(0, aliveMonsterIndexList.Count)];
                // 이미 선택된 인덱스인지 확인, 죽은 몬스터 인지 확인
                if (!randomIndexList.Contains(randomIndex))
                {
                    randomIndexList.Add(randomIndex);
                }

            }

            return randomIndexList;
        }


        public void IsCheckDeadMonster(out int choice)
        {
            do
            {
                choice = ConsoleUtil.MenuChoice(0, monsterlist.Count, "대상을 선택해주세요.");
                if (choice != 0 && monsterlist[choice - 1].IsDead)
                {
                    Console.WriteLine("이미 죽은 몬스터입니다");
                }
            }
            while (choice != 0 && monsterlist[choice - 1].IsDead);
        }

        private void DropItem()
        {
            // 몬스터 별 잡을을 때 보상 추가
            // 경험치, 골드, 아이템 등
            int dropGold = 0, dropPotion = 0, dropSword = 0;
            Random random = new Random();
            foreach (var item in monsterlist) {
                int reward = random.Next(0, 100);
                if (reward >= 0 && reward < 50) 
                {
                    dropGold += 500;
                }
                else if (reward >= 50 && reward < 75)
                {
                    dropPotion++;
                }
                else
                {
                    dropSword++;
                }
            }
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine("");
            if (dropGold>0) Console.WriteLine("{0} Gold", dropGold);
            if(dropPotion>0) Console.WriteLine("포션 - {0}", dropPotion);
            if(dropSword>0) Console.WriteLine("낡은검 - {0}", dropSword);
            Console.WriteLine("");
            player.Gold += dropGold;
            potion.Count += dropPotion;
            for(int i=0; i<dropSword; i++) inventory.Add(new Item("낡은 검", "낡은 검", ItemType.WEAPON, 2, 0, 0, 1000));
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
