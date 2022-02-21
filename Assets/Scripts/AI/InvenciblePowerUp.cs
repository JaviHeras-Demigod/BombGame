using Bomberman.Player;
using UnityEngine;

namespace Bomberman.AI
    {
    public class InvenciblePowerUp: InteractableObject
    {
        private void Update()
        {
            transform.Rotate(Vector3.up * (70 * Time.deltaTime));
        }

        public override void PlayerEnteredTrigger(PlayerController player)
        {
            player.PlayerPowerUp.InvenciblePowerUp();
            Destroy(gameObject);
        }
    }
}