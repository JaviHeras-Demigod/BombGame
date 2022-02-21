using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bomberman.Level;
using UnityEngine.SceneManagement;

namespace Bomberman.UI
{
    public class LevelMenuButton : MonoBehaviour
    {
        [SerializeField] private Text levelNameText;

        [SerializeField] private GameObject[] starsImages;


        public void SetLevelButtonProperties(string levelName, int starsAchieved)
        {
            levelNameText.text = levelName;
            if (starsAchieved <= starsImages.Length)
            {
                for (int i = 0; i < starsAchieved; i++)
                {
                    starsImages[i].SetActive(true);
                }
                return;
            }
            throw new System.Exception("Not enough stars");
        }


        public void OnClickLevelButton()
        {
            LevelLoader.LevelToLoad = levelNameText.text;
            SceneManager.LoadScene("2_PlayScene");
        }
    }

}
