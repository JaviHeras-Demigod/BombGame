namespace Bomberman.Level
{
    public class LevelLoader
    {
        private static LevelLoader instance;
        private static string levelToLoad;

        public static string LevelToLoad
        {
            get
            {
                if (instance == null)
                {
                    instance = new LevelLoader();
                }
                return levelToLoad;
            }
            set
            {
                if (instance == null)
                {
                    instance = new LevelLoader();
                }
                levelToLoad = value;
            }
        }
    }
}