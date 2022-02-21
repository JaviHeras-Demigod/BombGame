using UnityEngine;
using Bomberman.Level;
using Bomberman.Player;

namespace Bomberman.AI
{
    public class EndGameObject: InteractableObject
    {

        public override void PlayerEnteredTrigger(PlayerController player)
        {
            player.WinGame(transform.position);
        }

    }
}



