using UnityEngine;

namespace Bomberman.AI
{
    public class DestructibleObject : MonoBehaviour
    {

        [SerializeField] protected int life;

        public virtual void TakeDamage(int damage)
        {
            life -= damage;
        }
    }
}