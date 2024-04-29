namespace TextRPG
{
    public class GameManager
    {
        public GameManager() 
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
  

        }

        public void StartGame()
        {

        }
    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gameManager = new GameManager();
            gameManager.StartGame();
            Console.WriteLine("hi");
        }
    }
}
