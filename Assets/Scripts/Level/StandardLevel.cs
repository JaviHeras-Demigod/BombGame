using UnityEngine;

namespace Bomberman.Data
{
    [System.Serializable]
    public struct StandardLevel
    {
        public string name;
        public Vector2 dimensions;
        public float timeToAchieve;
        public Vector3 playerSpawnPosition;
        public StandardLevelObject[] levelBlocks;
        public StandardLevelObject[] levelPowerUps;
        public StandardNpcsObject[] levelNpcs;
    }
}

