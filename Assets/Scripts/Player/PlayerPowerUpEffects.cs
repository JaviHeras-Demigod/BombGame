using UnityEngine;
using UnityEngine.AI;
using System.Collections;

namespace Bomberman.Player
    {
    public class PlayerPowerUpEffects: MonoBehaviour
        {

        //Speed
        private NavMeshAgent agent;
        private float startSpeed;

        //Invencible
        private Collider collisioner;
        private float remainingTime = 0;

        [SerializeField] private GameObject invencibleShield;
        [SerializeField] private ParticleSystem speedEffect;

        private void Awake()
            {
            TryGetComponent(out collisioner);
            TryGetComponent(out agent);
            startSpeed = agent.speed;
            }


        public void SpeedPowerUp()
            {
            if (!IsInvoking("LostSpeedPowerUp"))
                {
                agent.speed = startSpeed * 1.5f;
                speedEffect.Play();
                }
            else
                CancelInvoke("LostSpeedPowerUp");

            Invoke("LostSpeedPowerUp", 8f);
            }

        private void LostSpeedPowerUp()
            {
            agent.speed = startSpeed;
            speedEffect.Stop();
            }

        public void InvenciblePowerUp()
        {
            if (remainingTime <= 0)
                StartCoroutine(Co_InvenciblePowerUp());
            else
                remainingTime = 12f;
        }


        IEnumerator Co_InvenciblePowerUp()
        {
            remainingTime = 12f;
            invencibleShield.SetActive(true);
            collisioner.enabled = false;

            while (remainingTime -0.1f > 0)
            {
                yield return new WaitForSeconds(remainingTime / 2f);
                remainingTime /= 2;
                invencibleShield.SetActive(false);
                yield return new WaitForSeconds(0.1f);
                invencibleShield.SetActive(true);
            }

            remainingTime = 1;

            while (remainingTime > 0)
            {
                yield return new WaitForSeconds(0.08f);
                remainingTime -= 0.08f;
                invencibleShield.SetActive(!invencibleShield.activeSelf);
            }

            yield return new WaitForSeconds(1f);

            invencibleShield.SetActive(false);
            collisioner.enabled = true;

        }

    }
}