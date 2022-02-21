using System.Collections;
using Bomberman.Data;
using Bomberman.Level;
using UnityEngine;
using UnityEngine.AI;

namespace Bomberman.AI
{
    public class SlimeEnemy : Enemy
    {

        private int destPoint = 0;
        private Vector3[] patrolPositions;

        private void Awake()
        {
            InitializeEnemyBaseComponents();
        }

        private void Start()
        {
            TakeNextDestination();
            StartCoroutine(Co_CalculateNextPoint());
        }

        public override void InjectEnemyData(string enemyData)
        {

            patrolPositions = new Vector3[]
            {
                transform.position,
                JsonUtility.FromJson<SlimeEnemyData>(enemyData).patrolPosition
            };
        }

        private void Update()
        {
            UpdateMovementAnimation(agent.velocity.magnitude / agent.speed);
            if (agent.enabled && agent.remainingDistance < 0.1f)
            {
                TakeNextDestination();
            }
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            if (life == 0) { KillEnemy(); }
        }

        private void TakeNextDestination()
        {
            SetDestination(patrolPositions[destPoint % patrolPositions.Length]);
            destPoint++;
        }

        IEnumerator Co_CalculateNextPoint()
        {

            while (life > 0)
            {
                if (agent.velocity.sqrMagnitude < 0.15f)
                {
                    TakeNextDestination();
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        private void OnTriggerEnter(Collider other)
            {
            if (life > 0 && other.CompareTag("Player"))
                {
                other.GetComponent<Bomberman.Player.PlayerController>().KillPlayer();
                }
            }
        }
}