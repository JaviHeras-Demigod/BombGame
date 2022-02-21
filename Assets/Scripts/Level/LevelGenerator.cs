using System.Collections;
using UnityEngine;
using Bomberman.Data;
using Bomberman.AI;
using Bomberman.Player;

namespace Bomberman.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private LevelDataConfig levelConfig;
        [SerializeField] private SpawnablePrefabsConfig prefabConfig;

        private PrefabsSpawnerFactory prefabsSpawner;


        [SerializeField] private PlayerController player;


        void Awake()
        {
            LoadLevel();
            Destroy(gameObject);
        }

        private void LoadLevel()
        {
            //A borrar
            //LevelLoader.LevelToLoad = "LEVEL1";
            StandardLevel level = levelConfig.GetLevelData(LevelLoader.LevelToLoad);
            GridController grid = new GridController(level);
            CreateLevel(level);
        }


        private void CreateLevel(StandardLevel level)
        {
            prefabsSpawner = new PrefabsSpawnerFactory(prefabConfig);

            foreach (var item in level.levelBlocks)
            {
                GameObject createdBlock = prefabsSpawner.Create(item.prefabId, GridController.Grid[item.indexGridPosition], item.rotation);
                createdBlock.GetComponent<DestructibleWall>().SetGridPoint(new GridPoint(item.indexGridPosition, GridController.Grid[item.indexGridPosition]));
                GridController.TakeGridPosition(item.indexGridPosition);
            }

            foreach (var item in level.levelPowerUps)
            {
                prefabsSpawner.Create(item.prefabId, GridController.Grid[item.indexGridPosition], item.rotation);
            }

            foreach (var item in level.levelNpcs)
            {
                GameObject npcSpawned = prefabsSpawner.Create(item.prefabId, GridController.Grid[item.indexGridPosition], item.rotation);
                npcSpawned.GetComponent<Enemy>().InjectEnemyData(item.enemyData);
            }

            StandardLevelStateController.SetLevelProperties(level.levelNpcs.Length, level.timeToAchieve);
            player.transform.position = (level.playerSpawnPosition);
        }
    }
}