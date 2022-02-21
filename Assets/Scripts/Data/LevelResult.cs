
namespace Bomberman.Data
{
    [System.Serializable]
    public struct LevelResult
    {
        public string levelName;
        public int starsEarned;
        public float timeAchieved;

        public LevelResult(string name, int stars, float time)
        {
            levelName = name;
            starsEarned = stars;
            timeAchieved = time;
        }
    }

}
