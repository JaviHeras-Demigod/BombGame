using UnityEngine;
using UnityEngine.UI;
using Object = System.Object;
using Bomberman.Player;
using Bomberman.Level;
using Bomberman.Data;

namespace Bomberman.UI
{
    public class CanvasLevelController: MonoBehaviour
        {

        [SerializeField] private Text remainingEnemiesText;

        [SerializeField] private Text remainingTimeText;

        [SerializeField] private Text currentBombRecountText;

        [SerializeField] private Animator EndGamePanelAnimator;

        [SerializeField] private BombPlayerGestor playerBomber;

        [SerializeField] private EndGameCanvasEffects endGameEffects;

        private void Awake()
        {
        }

        private void OnEnable()
            {
            StandardLevelStateController.EnemyDie += OnUpdateRemainingEnemies;
            StandardLevelStateController.OnUpdateTime += OnUpdateRemainingTime;
            StandardLevelStateController.OnEndGame += OnEndGame;
            playerBomber.UpdateBombCount += OnUpdateBombRecount;
            }

        private void OnDisable()
            {
            StandardLevelStateController.EnemyDie -= OnUpdateRemainingEnemies;
            StandardLevelStateController.OnUpdateTime -= OnUpdateRemainingTime;
            StandardLevelStateController.OnEndGame -= OnEndGame;
            playerBomber.UpdateBombCount -= OnUpdateBombRecount;
            }

        private void OnUpdateRemainingEnemies(Object sender, int enemies)
            {
            remainingEnemiesText.text = enemies.ToString();
            }

        private void OnUpdateRemainingTime(Object sender, float remainingTime)
            {
            remainingTimeText.text = ((int)remainingTime).ToString();
            }

        private void OnUpdateBombRecount(Object sender, int count)
            {
            currentBombRecountText.text = count.ToString();
            }

        private void OnEndGame(Object sender, LevelResult result)
            {
            endGameEffects.gameObject.SetActive(true);
            endGameEffects.StartCanvasEffects(result);
            }
        }
}