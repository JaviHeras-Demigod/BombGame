using UnityEngine.AI;
using UnityEngine;
using Bomberman.Level;

namespace Bomberman.AI
{

    [RequireComponent(typeof(NavMeshAgent),typeof(Animator))]
    public class Enemy : DestructibleObject
    {

        protected NavMeshAgent agent;
        private Animator animator;
        private readonly int movementAnimationId = Animator.StringToHash("Speed");
        private readonly int destroyEnemiAnimationId = Animator.StringToHash("Die");

        

        public virtual void InjectEnemyData(string enemyData)
        {

        }
        protected void InitializeEnemyBaseComponents()
        {
            TryGetComponent(out agent);
            TryGetComponent(out animator);
        }


        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
        }

        protected void KillEnemy()
        {
            agent.enabled = false;
            animator.SetTrigger(destroyEnemiAnimationId);
            StandardLevelStateController.EnemyDestroyed();
        }

        protected void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        protected void SetDestination(Vector3 position)
        {
            agent.SetDestination(position);
        }

        protected void UpdateMovementAnimation(float speed)
        {
            animator.SetFloat(movementAnimationId, speed);
        }
    }
}

