using UnityEngine;
using UnityEngine.AI;
using Bomberman.AI;
using System.Collections;


namespace Bomberman.Player
{
    public class PlayerController : MonoBehaviour
    {
        private NavMeshAgent agent;
        private PlayerAnimatorGestor animatorGestor;
        private PlayerPowerUpEffects playerPowerUp;

        public PlayerAnimatorGestor PlayerAnimatorGestor { get => animatorGestor; }
        public PlayerPowerUpEffects PlayerPowerUp { get => playerPowerUp; }

        private bool controllable = true;

        void Awake()
        {
            TryGetComponent(out agent);
            TryGetComponent(out animatorGestor);
            TryGetComponent(out playerPowerUp);
        }

        private void OnEnable()
        {
            agent.enabled = true;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && controllable)
            {
                RaycastHit ray;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out ray, 50f))
                {
                    agent.SetDestination(ray.point);
                }
            }

            animatorGestor.UpdateSpeedAnimation(agent.velocity.magnitude / agent.speed);
        }

        public void KillPlayer()
            {
            controllable = false;
            agent.SetDestination(transform.position);
            GetComponent<Collider>().enabled = false;
            animatorGestor.PlayDieAnimation();
            Camera.main.GetComponent<CameraBehaviour>().CameraShake(0.12f, 1.2f);
            }

        public void LoseGame()
            {
            Bomberman.Level.StandardLevelStateController.LoseGame();
            }

        public void WinGame(Vector3 position)
            {
            controllable = false;
            agent.SetDestination(position);
            animatorGestor.PlayWinAnimation();
            }

        private void FinalizeGame()
            {
            Bomberman.Level.StandardLevelStateController.WinGame();
            }

        private void OnTriggerEnter(Collider other)
            {

            if (other.CompareTag("Finish"))
                {
                other.GetComponent<InteractableObject>().PlayerEnteredTrigger(this);
                }
            }

        }
}

