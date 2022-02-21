using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bomberman.UI
{
    public class MainMenuButtonsActions : MonoBehaviour
    {
        [SerializeField] private GameObject levelPanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private GameObject editPanel;

        public void ExitButonClick()
        {
            Application.Quit();
        }

        public void PlayButtonClick()
        {
            levelPanel.SetActive(true);
        }

        public void SettingsButtonClick()
        {
            settingsPanel.SetActive(true);
        }

        public void EditButtonClick()
        {
            editPanel.SetActive(true);
        }

    }

}
