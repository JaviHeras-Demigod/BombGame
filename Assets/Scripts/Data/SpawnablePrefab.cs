using UnityEngine;

namespace Bomberman.Data
{
    [System.Serializable]
    public struct SpawnablePrefab
    {
        public string id;
        public GameObject prefab;
    }

    [System.Serializable]
    public struct SlimeEnemyData
    {
        public Vector3 patrolPosition;
    }

    [System.Serializable]
    public class TurtleEnemyData
    {
        public Vector3[] patrolPositions;
        public float minSpeed;
        public float maxSpeed;
    }
}


