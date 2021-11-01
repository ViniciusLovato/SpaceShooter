using Enums;
using UnityEngine;

namespace Weapons
{
    public class DamageDealer : MonoBehaviour
    {
        [SerializeField] private int damage = 100;
        [Range(0,2)][SerializeField] private float explosionRadius = 1f;

        public int GetDamage() => damage;
        public float GetExplosionRadius() => explosionRadius;
    }
}
