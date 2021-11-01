using System;
using Enums;
using UnityEngine;

namespace Weapons
{
    public class Shootable : MonoBehaviour
    {
        [SerializeField] private float laserSpeed = 10f;
        [SerializeField] private AudioClip shootSFX;

        private Rigidbody2D body;
        private float yMax;
        private float yMin;

        private Direction direction;

        public Shootable Init(Direction direction, int nameToLayer)
        {
            this.direction = direction;
            gameObject.layer = nameToLayer;
            
            return this;
        }
        
        private void Awake()
        {
            Camera gameCamera = Camera.main;
            body = GetComponent<Rigidbody2D>();
        
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
            
        }

        private void Start()
        {
            body.velocity = new Vector2(0,  Convert.ToInt32(this.direction) * laserSpeed);
            PlayAudioEffect();
        }

        // Update is called once per frame
        void Update()
        {
            CheckOutOfBoundaries();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            Destroy(gameObject);
        }

        private void CheckOutOfBoundaries()
        {
            float currentYPosition = transform.position.y;
            if (currentYPosition > yMax || currentYPosition < yMin)
            {
                Destroy(gameObject);
            }
        }

        private void PlayAudioEffect()
        {
            if (shootSFX)
            {
                AudioSource.PlayClipAtPoint(shootSFX, transform.position);    
            }
        }
    }
}
