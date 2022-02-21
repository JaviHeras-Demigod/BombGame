using UnityEngine;
using Bomberman.Player;

namespace Bomberman.AI
{
    public class InteractableObject: MonoBehaviour
    {

        public virtual void PlayerEnteredTrigger(PlayerController player)
            {
            Debug.Log("el jugadó me ha tocado :$");
            }

     }
}



