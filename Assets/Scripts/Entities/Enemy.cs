using System;
using Enums;
using UnityEngine;
using Weapons;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

namespace Entities
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float shotCounter;
        [SerializeField] private float minTimeBetweenShots = 0.2f;
        [SerializeField] private float maxTimeBetweenShots = 3f;
        [SerializeField] private GameObject projectile;
        
        private void Start()
        {
            shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }

        private void Update()
        {
            CountDownAndShoot();
        }

        private void CountDownAndShoot()
        {
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Fire();
                shotCounter = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            }
        }

        private void Fire()
        {
            GameObject instance = Instantiate(projectile, transform.position, Quaternion.identity);
            instance.GetComponent<Shootable>().Init(Direction.Down, LayerMask.NameToLayer("Enemy Projectile"));
        }
    }
} 