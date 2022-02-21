using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bomberman.Data;

namespace Bomberman.UI
{
    public class EndGameCanvasEffects : MonoBehaviour
    {

        private AudioSource audioSource;
        [SerializeField] private AudioClip starClip;
        [SerializeField] private AudioClip winClip;
        [SerializeField] private AudioClip loseClip;

        [SerializeField] private GameObject[] objectsToEnableAtEnd;

        [SerializeField] private GameObject[] stars;

        private void Awake()
        {
            TryGetComponent(out audioSource);
        }

        public void StartCanvasEffects(LevelResult result)
        {
            StartCoroutine(Co_EndGameEffects(result));
        }


       IEnumerator Co_EndGameEffects(LevelResult result)
        {

            if(result.starsEarned > 0)
                audioSource.PlayOneShot(winClip);
            else
                audioSource.PlayOneShot(loseClip);

            //Stars

            if (result.starsEarned > stars.Length)
                throw new System.Exception("Not enough stars in canvas");

            yield return new WaitForSecondsRealtime(0.3f);

            for (int i = 0; i < result.starsEarned; i++)
            {
                stars[i].SetActive(true);
                yield return new WaitForSecondsRealtime(0.3f);
            }

            yield return new WaitForSecondsRealtime(0.4f);
            foreach (GameObject obj in objectsToEnableAtEnd)
            {
                obj.SetActive(true);
            }
        }
    }
}
