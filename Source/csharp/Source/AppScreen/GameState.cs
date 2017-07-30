namespace RozenMaiden.AppScreen
{
    public class GameState
    {
        /// <summary>
        /// Состояние игровой сцены
        /// </summary>
        public enum Current
        {
            SplashScreen = 0, // Сплеш-скрин
            MainMenu = 1, // Главное меню
            GameScreen = 2 // Сцена с игровым процессом
        }

        public Current State = Current.MainMenu;
    }
}