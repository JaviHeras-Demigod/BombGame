using UnityEngine;
using Bomberman.Data;

namespace Bomberman.Level
{
    public class PrefabsSpawnerFactory
    {

        private SpawnablePrefabsConfig prefabsConfig;

        public PrefabsSpawnerFactory(SpawnablePrefabsConfig prefabsConfig)
        {
            this.prefabsConfig = prefabsConfig;
        }

        public GameObject Create(string name)
        {
            return UnityEngine.Object.Instantiate(prefabsConfig.GetPrefabById(name));
        }

        public GameObject Create(string name, Vector3 position, Quaternion rotation)
        {
            return UnityEngine.Object.Instantiate(prefabsConfig.GetPrefabById(name), position, rotation);
        }
    }
}

