using System.Collections.Generic;
using UnityEngine;

namespace Bomberman.Data
{
    [CreateAssetMenu(fileName = "LevelsResults", menuName = "Bomberman/Results")]
    public class LevelsResults : ScriptableObject
    {
        public List<LevelResult> results;

        public void SetLevelResults(string levelName, int stars, float time)
        {

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].levelName == levelName)
                {
                    results[i] = new LevelResult(levelName, stars, time);
                    return;
                }
            }
            throw new System.Exception(string.Format("The level {0} has not been included in results", levelName));
        }

        public void AddNewLevel(string levelName)
        {
            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].levelName == levelName)
                {
                    throw new System.Exception(string.Format("The level {0} already exist in results", levelName));
                }
            }
            results.Add(new LevelResult(levelName, 0, -100));
        }

        public LevelResult GetLevelResults(string name)
        {
            foreach (LevelResult level in results)
            {
                if (level.levelName == name)
                {
                    return level;
                }
            }
            throw new System.Exception(string.Format("The level {0} has not been included in results", name));
        }

        public bool ContainsLevel(string name)
        {
            foreach (LevelResult level in results)
            {
                if (level.levelName == name)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
