using UnityEngine;

namespace Bomberman.Data
{
    [System.Serializable]
    public class StandardLevelObject
    {
        public string prefabId;
        public int indexGridPosition;
        public Quaternion rotation;
    }

    [System.Serializable]
    public class StandardNpcsObject : StandardLevelObject
    {
        public string enemyData;
    }
}
