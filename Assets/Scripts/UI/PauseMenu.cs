using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bomberman.UI
{
    public class PauseMenu: MonoBehaviour
        {

        public void PauseGame()
            {
            Time.timeScale = 0;
            gameObject.SetActive(true);
            }

        public void ContinueGame()
            {
            Time.timeScale = 1;
            gameObject.SetActive(false);
            }

        public void RestartGame()
            {
            Time.timeScale = 1;
            SceneManager.LoadScene("2_PlayScene");
            }

        public void GoToMainMenu()
            {
            Time.timeScale = 1;
            SceneManager.LoadScene("1_MenuScene");
            }

        public void CloseApp()
            {
            Application.Quit();
            }
        }
}

