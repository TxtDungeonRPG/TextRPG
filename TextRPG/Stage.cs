using System;

public class Stage
{
    public int CurrentFloor { get; set; }

    public Stage(int currentFloor)
    {
        CurrentFloor = currentFloor;
    }
}

public class MainClass
{
    // Main 메서드 밖에서 currentStage 변수를 선언
    private static Stage currentStage;

   

    // MainMenu 메서드 정의
    private static void MainMenu()
    {
        // currentStage 객체 사용 가능
        Console.WriteLine($"현재 진행 중인 스테이지: {currentStage.CurrentFloor}층");
    }
}