using UnityEngine;
using System.Collections;
using Bomberman.AI;

namespace Bomberman.Player
{
    public class ExplosionCollisioner : MonoBehaviour
    {
        private BombBehaviour parentBombBehaviour;
        private Vector3 direction;

        private ParticleSystem particles;
        private Collider triggerCollisioner;

        public Vector3 Direction { get => direction; set => direction = value; }
        public BombBehaviour ParentBombBehaviour { set => parentBombBehaviour = value; }

        private void Awake()
        {
            particles = GetComponent<ParticleSystem>();
            triggerCollisioner = GetComponent<Collider>();
            gameObject.SetActive(false);
        }

        private void Update()
        {
            transform.Translate(Direction * (4f * Time.deltaTime));
        }

        public void Launch()
        {
            gameObject.SetActive(true);
            StartCoroutine(Co_ExpandExplosion());
        }

        private void StartExplosion()
        {
            particles.Play();
            triggerCollisioner.enabled = true;
        }

        public void EndExplosion()
        {
            GetComponent<TrailRenderer>().emitting = false;
            particles.Stop();
            triggerCollisioner.enabled = false;
        }

        IEnumerator Co_ExpandExplosion()
        {
            StartExplosion();
            yield return new WaitForSeconds(0.55f);
            EndExplosion();
            yield return new WaitForSeconds(0.7f);
            gameObject.SetActive(false);
            parentBombBehaviour.EndFlare();
        }


        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.layer == LayerMask.NameToLayer("ExplosionAffected"))
            {
                if (other.TryGetComponent(out DestructibleObject destructibleCollisioned))
                    destructibleCollisioned.TakeDamage(1);
                EndExplosion();
            }
            else if (other.CompareTag("Player"))
            {
                other.GetComponent<Bomberman.Player.PlayerController>().KillPlayer();
            }
        }
    }
}
