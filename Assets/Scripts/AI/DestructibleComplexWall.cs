using UnityEngine;

namespace Bomberman.AI
{
    public class DestructibleComplexWall : DestructibleWall
    {

        int currentModel;

        [SerializeField] private ModelsPerLife[] modelsPerLife;

        private void Awake()
        {
            InitializeWallComponents();
            ChangeModel();
        }

        protected override void PlayCollisionEffects()
        {
            base.PlayCollisionEffects();
            ChangeModel();
        }

        private void ChangeModel()
        {
            for (int i = 0; i < modelsPerLife.Length; i++)
            {
                if (life == modelsPerLife[i].life)
                {
                    modelsPerLife[currentModel].model.SetActive(false);
                    modelsPerLife[i].model.SetActive(true);
                    currentModel = i;
                }
            }
        }
    }
}


[System.Serializable]
public struct ModelsPerLife
{
    public int life;
    public GameObject model;
}
