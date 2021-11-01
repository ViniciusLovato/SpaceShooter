using System;
using Behaviors;
using UnityEngine;
using Weapons;

namespace Entities
{
    public class DestructibleShip : MonoBehaviour
    {
        [SerializeField] private float health = 100;
        [SerializeField] private int points = 0;
        [SerializeField] private GameObject destroyVFX;
        [SerializeField] private AudioClip destroySFX;

        private Blinking blinking;
        private GameSession gameSession;

        public void Start()
        {
            gameSession = FindObjectOfType<GameSession>();
            blinking = GetComponent<Blinking>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            DamageDealer damageDealer = other.GetComponent<DamageDealer>();
            if (damageDealer)
            {
                ProcessHit(damageDealer, damageDealer.GetExplosionRadius() > 0);    
            }
        }

        private void ProcessHit(DamageDealer damageDealer, bool shouldProcessOverlap)
        {
            if (shouldProcessOverlap)
            {
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, damageDealer.GetExplosionRadius());

                foreach (var hitCollider in hitColliders)
                {
                    var destructibleShip = hitCollider.GetComponent<DestructibleShip>();
                    if (destructibleShip)
                    {
                        destructibleShip.ProcessHit(damageDealer, false);
                    }
                }
            }
            
            health -= damageDealer.GetDamage();

            if (health <= 0)
            {
                AudioSource.PlayClipAtPoint(destroySFX, transform.position);
                GameObject particle = Instantiate(destroyVFX, transform.position, Quaternion.identity);
                
                gameSession.AddScore(points);
                
                Destroy(gameObject);
                Destroy(particle, 1f);
            }
            
            if (blinking && health <= 100)
            {
                blinking.ShouldBlink = true;
            }
        }
    }
}
