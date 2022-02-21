using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Bomberman.Level;
using Bomberman.Player;

namespace Bomberman.Player
    {
    public class BombBehaviour : MonoBehaviour
    {

        private Animator animator;
        private static readonly int turnOnBombAnimationId = Animator.StringToHash("TurnOn");
        private readonly Vector3[] directions = new Vector3[] { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };
        [SerializeField] private ExplosionCollisioner[] explosionCollisioners;
        [SerializeField] private LayerMask collisionLayer;

        private GridPoint gridPointoccuped;

        int flaresLaunched = 0;

        public delegate void OnDestroyObject();
        public event OnDestroyObject OnDestroy;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            for (int i = 0; i < directions.Length; i++)
            {
                explosionCollisioners[i].Direction = directions[i];
                explosionCollisioners[i].ParentBombBehaviour = this;
            }
        }

        public void LaunchBombToPosition(GridPoint gridPoint)
        {
            gridPointoccuped = gridPoint;
            StartCoroutine(Co_BombToPosition(gridPoint.position));
        }

        IEnumerator Co_BombToPosition(Vector3 endPosition)
        {
            float timeRemaining = 0.5f;

            Vector3 movement = Vector3.zero;

            movement.x = (endPosition.x - transform.position.x) / timeRemaining;
            movement.z = (endPosition.z - transform.position.z) / timeRemaining;

            movement.y = 8;

            //MRUA calculo de aceleración
            float yawAceleration = 0 - 2 * transform.position.y - 2 * movement.y * timeRemaining;
            yawAceleration = yawAceleration / Mathf.Pow(timeRemaining, 2);

            while (timeRemaining > 0)
            {
                movement.y += yawAceleration * Time.deltaTime;
                transform.Translate(movement * Time.deltaTime);

                timeRemaining -= Time.deltaTime;
                yield return null;
            }
            GetComponent<NavMeshObstacle>().enabled = true;
            animator.SetTrigger(turnOnBombAnimationId);
        }

        public void CalculateFlareDirections()
        {
            Camera.main.GetComponent<CameraBehaviour>().CameraShake(0.2f, 0.2f);
            flaresLaunched = 0;

            foreach (var collisioner in explosionCollisioners)
            {
                if (!Physics.Raycast(transform.position, collisioner.Direction, 1.3f, collisionLayer))
                {
                    collisioner.Launch();
                    flaresLaunched++;
                }
            }
        }

        public void EndFlare()
        {
            flaresLaunched--;
            if (flaresLaunched <= 0)
            {
                FinalizeBomb();
            }
        }

        private void FinalizeBomb()
        {
            OnDestroy?.Invoke();
            OnDestroy = null;
            GridController.LeaveFreeGrid(gridPointoccuped.index);
            Destroy(gameObject);
        }
    }
}


