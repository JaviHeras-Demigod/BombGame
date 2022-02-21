using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

namespace Bomberman.UI
{
    public class SettingsMenu : MonoBehaviour
    {
        //Audio
        [SerializeField] private AudioMixer audioMixer;
       
        [SerializeField] private AudioModifier[] modifiers;
        private Dictionary<int, AudioModifier> stringToSliderAudio;


        [SerializeField] private Button applyChangesButton;

        private void Awake()
        {



            stringToSliderAudio = new Dictionary<int, AudioModifier>();

            for (int i = 0; i < modifiers.Length; i++)
            {
                int index = i;
                stringToSliderAudio.Add(i, modifiers[i]);
                modifiers[i].slider.onValueChanged.AddListener(delegate { OnSliderChanged(index); });
            }
         
        }

        private void OnEnable()
        {
            LoadSoundSettings();
        }

        private void LoadSoundSettings()
        {

            float volume = 0;
            foreach (AudioModifier modifier in modifiers)
            {
                volume = PlayerPrefs.GetFloat(modifier.name, 0);
                audioMixer.SetFloat(modifier.name, volume);
                modifier.slider.value = volume;
            }

            applyChangesButton.interactable = false;
        }

        public void ApplyChanges()
        {
            foreach (AudioModifier modifier in modifiers)
            {
                PlayerPrefs.SetFloat(modifier.name, modifier.slider.value);
                audioMixer.SetFloat(modifier.name, modifier.slider.value);
            }

            applyChangesButton.interactable = false;
        }
    
        public void OnModifyValues(int sliderIndex)
        {
            if (!applyChangesButton.interactable)
                applyChangesButton.interactable = true;
                /*
            if (stringToSliderAudio.TryGetValue(valueName, out Slider slider))
                audioMixer.SetFloat(valueName, slider.value);
                */
            
        }

        public void OnSliderChanged(int sliderCode)
        {
                
        }

        public void OpenSettingsPanel()
        {

        }

        public void CloseSettingsPanel()
        {
            LoadSoundSettings();
        }

        public void DeactivateSettingsPanel()
        {

        }
    }

}

[System.Serializable]
public class AudioModifier
{
    public string name;
    public Slider slider;
    
    public float GetSliderValue()
    {
        return slider.value;
    }
}
