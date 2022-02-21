using System.Collections;
using Bomberman.Data;
using Bomberman.Level;
using UnityEngine;
using UnityEngine.AI;

namespace Bomberman.AI
{
    public class TurtleEnemy : Enemy
    {
        private Vector3[] patrolPositions;

        private float baseSpeed;

        private float minSpeed;
        private float maxSpeed;

        private void Awake()
        {
            InitializeEnemyBaseComponents();
        }

        private void Start()
        {
            TakeNextDestination();
            StartCoroutine(Co_CalculateNewPoint());
            StartCoroutine(Co_CalculateNewSpeed());
            baseSpeed = agent.speed;
        }

        public override void InjectEnemyData(string enemyData)
        {
            TurtleEnemyData data = JsonUtility.FromJson<TurtleEnemyData>(enemyData);
            patrolPositions = new Vector3[data.patrolPositions.Length + 1];
            patrolPositions[0] = transform.position;

            for (int i = 1; i < patrolPositions.Length; i++)
            {
                patrolPositions[i] = data.patrolPositions[i - 1];
            }

            minSpeed = data.minSpeed;
            maxSpeed = data.maxSpeed;
        }


        private void Update()
        {
            UpdateMovementAnimation(agent.velocity.magnitude / baseSpeed);
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
            SetDestination(patrolPositions[Random.Range(0, patrolPositions.Length)]);
        }

        IEnumerator Co_CalculateNewPoint()
        {
            while (life > 0)
            {
                if (agent.velocity.sqrMagnitude < 0.15f)
                    TakeNextDestination();

                yield return new WaitForSeconds(0.5f);
            }
        }

        IEnumerator Co_CalculateNewSpeed()
        {
            while (life > 0)
            {
                agent.speed = Random.Range(minSpeed,maxSpeed);
                yield return new WaitForSeconds(1.3f);
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

