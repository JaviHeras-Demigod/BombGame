using System.Collections.Generic;
using UnityEngine;


namespace Bomberman.Data
{
    [CreateAssetMenu(fileName = "LevelsConfiguration", menuName = "Custom/LevelConfig")]
    public class LevelDataConfig : ScriptableObject
    {
        public List<StandardLevel> levels;

        public StandardLevel GetLevelData(string levelName)
        {
            foreach (StandardLevel level in levels)
            {
                if (level.name.Equals(levelName))
                {
                    return level;
                }
            }
            throw new System.Exception(string.Format("The level named {0} does not exist", levelName));
        }

        public void AddNewLevel(StandardLevel level)
        {
            levels.Add(level);
        }

        public void RemoveCustomLevel(string customLevelName)
        {
            levels.RemoveAt(GetLevelIndexByName(customLevelName));
        }

        public void EditLevel(string levelName, StandardLevel levelData)
        {
            int index = -1;
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].name == levelName)
                    index = i;
            }
            if (index == -1)
                throw new System.Exception(string.Format("The level named {0} does not exist", levelName));

            levels[index] = levelData;
        }

        private int GetLevelIndexByName(string levelName)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                if (levels[i].name.Equals(levelName))
                {
                    return i;
                }
            }
            throw new System.Exception(string.Format("The level named {0} does not exist", levelName));
        }
    }
}