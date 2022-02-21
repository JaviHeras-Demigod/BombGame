using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bomberman.Data;

namespace Bomberman.Level
{
    public class StandardLevelStateController: MonoBehaviour
    {

        [SerializeField] private LevelsResults results;

        public static StandardLevelStateController instance;
        private static int remainingEnemies;
        private static float timeRemaining;

        public static event EventHandler<int> EnemyDie;
        public static event EventHandler<float> OnUpdateTime;
        public static event EventHandler<LevelResult> OnEndGame;

        private static int stars;
        
        private void Awake()
            {
            if (instance == null)
                {
                InitializeVariables();
                instance = this;
                }
            else
                Destroy(gameObject);
            }

        private void InitializeVariables()
            {
            stars = 0;
            }

        private void OnDisable()
            {
            instance = null;
            }

        private void Update()
            {
            UpdateTime();
            }

        private void UpdateTime()
        {
            if(timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                OnUpdateTime?.Invoke(this, timeRemaining);
            }
        }

        public static void LoseGame()
            {
            stars = 0;
            instance.FinalizeGame();
            }

        public static void WinGame()
            {
            stars++;
            instance.FinalizeGame();
            }

        private void FinalizeGame()
            {
            if(timeRemaining > 0 && stars > 0)
            {
                stars++;
            }
            LevelResult bestResult = results.GetLevelResults(LevelLoader.LevelToLoad);

            bestResult.starsEarned = bestResult.starsEarned > stars? bestResult.starsEarned : stars;
            bestResult.timeAchieved = bestResult.timeAchieved > timeRemaining ? bestResult.timeAchieved : timeRemaining;

            results.SetLevelResults(LevelLoader.LevelToLoad, bestResult.starsEarned, bestResult.timeAchieved);

            Time.timeScale = 0;
            OnEndGame?.Invoke(this, new LevelResult(LevelLoader.LevelToLoad, stars, timeRemaining));
            }

        public static void SetLevelProperties(int enemyCount, float timeToAchieve)
            {
            remainingEnemies = enemyCount;
            timeRemaining = timeToAchieve;
            }

        public static void EnemyDestroyed()
        {

            if (remainingEnemies > 0)
            {
                remainingEnemies--;
                instance.UpdateEnemyCount();
                if(remainingEnemies == 0)
                {
                    stars++;
                    instance.UpdateEnemyCount();
                    EnemyDie = null;
                }
            }


        }

        private void UpdateEnemyCount()
            {
            EnemyDie?.Invoke(null, remainingEnemies);
            }
        }

}
