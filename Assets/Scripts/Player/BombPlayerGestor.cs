using UnityEngine;
using Bomberman.Level;
using System;

namespace Bomberman.Player
{
    public class BombPlayerGestor : MonoBehaviour
    {
        private PlayerController player;

        [SerializeField] private int maxBombCount;
        private int currentBombCount;

        [SerializeField] private GameObject bombPrefab;
        [SerializeField] private Transform bombLaunchPosition;

        public event EventHandler<int> UpdateBombCount;

        private void Awake()
        {
            player = GetComponent<PlayerController>();
            currentBombCount = maxBombCount;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TryLaunchBomb();
            }
        }

        public void TryLaunchBomb()
        {
            if (currentBombCount > 0 && GridController.HasNearestsGridFree(transform.position))
                LaunchBomb();
        }

        private void LaunchBomb()
        {
            player.PlayerAnimatorGestor.PlayLaunchBombAnimation();

            BombBehaviour bomb;
            Instantiate(bombPrefab, bombLaunchPosition.position, Quaternion.identity).TryGetComponent(out bomb);

            GridPoint gridToBeLaunched = GridController.GetNearestGridFreePosition(transform.position);
            GridController.TakeGridPosition(gridToBeLaunched.index);

            bomb.LaunchBombToPosition(gridToBeLaunched);
            bomb.OnDestroy += BombExploded;

            currentBombCount--;
            UpdateBombCount?.Invoke(null, currentBombCount);
        }

        private void BombExploded()
        {
            currentBombCount++;
            UpdateBombCount?.Invoke(null, currentBombCount);
        }
    }
}
