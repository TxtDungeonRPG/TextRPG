using System;

namespace TextRPG
{
    public class Quest
    {
        public string QuestTitle;
        public string QuestScript;
        public string QuestWhatToDo;

        public string Reward; //추후 item형 추가하여 수정하기
        public int QuestTarget;//목표 수치
        public int QuestTracker;//현재 달성도
        public bool IsDone = false;
        public bool QuestOut =false;
        public bool IsAccept = false;

        public int num;
        //interface
        public virtual void QuestProgress(List<Item>inventory, Player player)//진행도 업데이트 메서드
        {
            if (this.QuestTarget > this.QuestTracker)
            {
                this.QuestTracker++; 
            }
        }

        public void QuestClearCheck ()
        {
            if (QuestTarget <= QuestTracker)
            {
                QuestTarget = QuestTracker;
                IsDone = true;
            }
        }

        public virtual void QuestDone(List<Item> inventory,Player player)//보상받는 함수
        {

        }

        public virtual void Changenum(int num)//보상받는 함수
        {

        }
    }

    public class Quest1 : Quest
    {
        public int killMonsterCount = 0;
        public Quest1() 
        {
            QuestTitle = "마을을 위협하는 미니언 처치";
            QuestScript = "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\r\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n모험가인 자네가 좀 처치해주게!";
            QuestWhatToDo = "몬스터 5마리 처치";
            Reward = "쓸만한 방패 x 1\n5G";
            QuestTarget = 5;
            QuestTracker = 0;
        }
        public override void QuestProgress(List<Item> inventory, Player player)
        {
            QuestTracker = killMonsterCount;
            if (QuestTarget <= QuestTracker)
            {
                QuestTracker = QuestTarget;
            }
        }
        public override void QuestDone(List<Item> inventory,Player player)
        {
            inventory.Add(new Item("쓸만한 방패", "나름 나쁘지 않은 방패입니다", ItemType.ARMOR, 0, 2, 0, 100));
            player.Gold += 5;
            QuestOut = true;
        }
        public override void Changenum(int num)//보상받는 함수
        {
            killMonsterCount += num;
        }
    }

    public class Quest2 : Quest
    {
        public Quest2()
        {
            QuestTitle = "장비를 장착해보자";
            QuestScript = "아무런 옷도 입고있지 않네요. 뭐라도 입고 오세요.";
            QuestWhatToDo = "장비 착용하기";
            Reward = "멋진 칼 x 1\n10G";
            QuestTarget = 1;
            QuestTracker = 0;
        }
        public override void QuestProgress(List<Item> inventory, Player player)
        {
            foreach(Item item in inventory)
            {
                if (item.IsEquipped)
                {
                    QuestTracker = QuestTarget;
                }
            }
        }
        public override void QuestDone(List<Item> inventory, Player player)
        {
            inventory.Add(new Item("멋진 칼", "보기엔 좋은 칼입니다", ItemType.WEAPON, 2, 0, 0, 100));
            player.Gold += 10;
            QuestOut = true;
        }
    }

    public class Quest3 : Quest
    {
        public Quest3()
        {
            QuestTitle = "더욱 더 강해지기!";
            QuestScript = "당신은 너무 약합니다. 레벨업을 하고 강해져서 돌아와보세요";
            QuestWhatToDo = "레벨 2 달성";
            Reward = "10G";
            QuestTarget = 2;
            QuestTracker = 1;
        }
        public override void QuestProgress(List<Item> inventory, Player player)
        {
            QuestTracker = player.Level;
            if (QuestTarget <= QuestTracker)
            {
                QuestTracker = QuestTarget;
            }
        }
        public override void QuestDone(List<Item> inventory, Player player)
        {
            player.Gold += 10;
            QuestOut = true;
        }
    }
}
