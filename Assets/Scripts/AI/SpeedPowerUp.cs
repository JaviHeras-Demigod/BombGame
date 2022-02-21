using Bomberman.Player;
using UnityEngine;

namespace Bomberman.AI
    {
    public class SpeedPowerUp: InteractableObject
        {

        private void Update()
        {
            transform.Rotate(Vector3.up * (70 * Time.deltaTime));
        }

        public override void PlayerEnteredTrigger(PlayerController player)
            {
            player.PlayerPowerUp.SpeedPowerUp();
            Destroy(gameObject);
            }
        }
}



