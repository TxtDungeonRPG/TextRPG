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
        public bool IsDone;

        //public Quest(string questTitle, string questDescription, string questScript, string questWhatToDo, string reward)
        //{
        //    QuestTitle = questTitle;
        //    QuestDescription = questDescription;
        //    QuestScript = questScript;
        //    QuestWhatToDo = questWhatToDo;
        //    Reward = reward;
        //}
    }

    public class Quest1 : Quest
    {
        public Quest1() 
        {
            QuestTitle = "마을을 위협하는 미니언 처치";
            QuestScript = "이봐! 마을 근처에 미니언들이 너무 많아졌다고 생각하지 않나?\r\n마을주민들의 안전을 위해서라도 저것들 수를 좀 줄여야 한다고!\r\n모험가인 자네가 좀 처치해주게!";
            QuestWhatToDo = "몬스터 5마리 처치";
            QuestTarget = 5;
            QuestTracker = 0;
            IsDone = false;
        }
    }

    public class Quest2 : Quest
    {
        public Quest2()
        {
            QuestTitle = "장비를 장착해보자";
            QuestScript = "아무런 옷도 입고있지 않네요. 뭐라도 입고 오세요.";
            QuestWhatToDo = "장비 착용하기";
            QuestTarget = 1;
            QuestTracker = 0;
            IsDone = false;
        }
    }

    public class Quest3 : Quest
    {
        public Quest3()
        {
            QuestTitle = "더욱 더 강해지기!";
            QuestScript = "당신은 너무 약합니다. 레벨업을 하고 강해져서 돌아와보세요";
            QuestWhatToDo = "레벨 2 달성";
            QuestTarget = 2;
            QuestTracker = 1;
            IsDone = false;
        }
    }
}
