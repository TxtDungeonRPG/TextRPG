using System;

namespace TextRPG
{
    public class Quest
    {
        public string QuestTitle;
        public string QuestScript;
        public string QuestWhatToDo;

        public string Reward; //추후 item형 추가하여 수정하기
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
            QuestWhatToDo = "미니언 5마리 처치";
            IsDone = false;
        }
    }

    public class Quest2 : Quest
    {
        public Quest2()
        {
            QuestTitle = "더욱 더 강해지기!";
            QuestScript = "더 강해져보도록 합시다";
            QuestWhatToDo = "레벨 2 달성";
            IsDone = false;
        }
    }
}
