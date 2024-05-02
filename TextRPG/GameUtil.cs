using Newtonsoft.Json;
using TextRPG;

public class GameUtil
{
    public Player Player;
    public List<Monster> Monsterlist;
    public bool IsMonsterSpawned = false;
    public int StartHp;
    public List<Item> Inventory;
    public List<Item> StoreInventory;
    public List<Quest> QuestList;

    public int BonusAtk;
    public int BonusDef;
    public int BonusHp;

    public List<Potion> PotionList;
    public Stage CurrentStage;

    public GameUtil(Player player, List<Monster> monsterlist, bool isMonsterSpawned, int startHp, List<Item> inventory, List<Item> storeInventory, List<Quest> questList,
                        int bonusAtk, int bonusDef, int bonusHp, List<Potion> potionList, Stage currentStage)
    {
        this.Player = player;
        this.Monsterlist = monsterlist;
        this.IsMonsterSpawned = isMonsterSpawned;
        this.StartHp = startHp;
        this.Inventory = inventory;
        this.StoreInventory = storeInventory;
        this.QuestList = questList;
        this.BonusAtk = bonusAtk;
        this.BonusDef = bonusDef;
        this.BonusHp = bonusHp;
        this.PotionList = potionList;
        this.CurrentStage = currentStage;

    }

    public void Save(GameUtil gameUtil)
    {
        string json = JsonConvert.SerializeObject(gameUtil, Formatting.Indented);
        // TextRPG\TextRPG\bin\Debug\net8.0 경로에 저장됨
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gameSave.json"); // 파일 경로 생성
        File.WriteAllText(filePath, json); // JSON

        Console.WriteLine();
        Console.WriteLine("게임이 저장되었습니다.");
        Thread.Sleep(1500);
    }

}
