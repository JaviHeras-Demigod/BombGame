using UnityEngine;

namespace Bomberman.Data
{
    [CreateAssetMenu(fileName = "PrefabConfig", menuName = "Custom/PrefabConfig")]
    public class SpawnablePrefabsConfig : ScriptableObject
    {
        public SpawnablePrefab[] prefabs;

        public GameObject GetPrefabById(string id)
        {
            foreach (var item in prefabs)
            {
                if (item.id == id)
                {
                    return item.prefab;
                }
            }
            throw new System.Exception(string.Format("The prefab named {0} does not exist.", id));
        }
    }

}


