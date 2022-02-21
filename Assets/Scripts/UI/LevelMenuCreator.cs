using System.Collections;
using UnityEngine;
using Bomberman.Data;

namespace Bomberman.UI
{
    public class LevelMenuCreator : MonoBehaviour
    {

        [SerializeField] private LevelDataConfig levelData;
        [SerializeField] private LevelsResults levelsResults;

        [SerializeField] private GameObject levelButtonPrefab;

        [SerializeField] private Transform standardLevelPanel;
        [SerializeField] private Transform customLevelPanel;

        private Animator panelAnimator;

        private void Awake()
        {

            CheckLevels();
            CreateButtons();
            TryGetComponent(out panelAnimator);
        }



        private void CheckLevels()
        {
            if (levelData.levels.Count != levelsResults.results.Count)
            {
                foreach (StandardLevel level in levelData.levels)
                {
                    if (!levelsResults.ContainsLevel(level.name))
                    {
                        levelsResults.AddNewLevel(level.name);
                    }
                }
            }
        }

        private void CreateButtons()
        {
            Transform parentTransform;
            foreach (StandardLevel level in levelData.levels)
            {

                if (level.name.StartsWith("LEVEL"))
                    parentTransform = standardLevelPanel;
                else
                    parentTransform = customLevelPanel;

                GameObject button = Instantiate(levelButtonPrefab, parentTransform);
                LevelResult result = levelsResults.GetLevelResults(level.name);
                button.GetComponent<LevelMenuButton>().SetLevelButtonProperties(result.levelName, result.starsEarned);
            }
        }

        public void CloseLevelMenu()
        {
            panelAnimator.SetTrigger("Close");
        }

        public void DeactivePanel()
        {
            gameObject.SetActive(false);
        }

    }

}
