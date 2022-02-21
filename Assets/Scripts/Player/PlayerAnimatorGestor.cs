using UnityEngine;

namespace Bomberman.Player
{
    public class PlayerAnimatorGestor: MonoBehaviour
        {
        private Animator animator;

        private readonly int speedAnimationId = Animator.StringToHash("Speed");
        private readonly int LaunchBombAnimationId = Animator.StringToHash("Launch");
        private readonly int dieAnimationId = Animator.StringToHash("Die");
        private readonly int winAnimationId = Animator.StringToHash("Win");

        [SerializeField] private ParticleSystem endGameParticles;
        [SerializeField] private ParticleSystem dieParticles;

        private void Awake()
            {
            animator = GetComponent<Animator>();
            }

        public void UpdateSpeedAnimation(float speed)
            {
            animator.SetFloat(speedAnimationId, speed);
            }

        public void PlayLaunchBombAnimation()
            {
            animator.SetTrigger(LaunchBombAnimationId);
            }

        public void PlayDieAnimation()
            {
            animator.SetTrigger(dieAnimationId);
            }

        public void PlayWinAnimation()
            {
            Camera.main.GetComponent<CameraBehaviour>().CameraShake(0.06f, 2.5f);
            animator.SetTrigger(winAnimationId);
            }
        }
}
