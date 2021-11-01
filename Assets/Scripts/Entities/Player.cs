using System;
using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;
using Weapons;

namespace Entities
{
    public class Player : MonoBehaviour
    {
        [Header("Ship configuration")]
        [SerializeField][Range(1f, 10f)] private float shipSpeed = 6f;

        [Header("Weapons configuration")]
        [SerializeField] private GameObject primaryWeapon;

        [SerializeField] private GameObject secondaryWeapon;
        
        [SerializeField] private float primaryFireRate = 0.15f;
        [SerializeField] private float secondaryFireRate = 2f;
        [SerializeField] private int secondaryBulletsCount = 5;

        private float xMin;
        private float xMax;

        private float yMin;
        private float yMax;

        private float xPadding;
        private float yPadding;

        private bool canShootPrimary = true;
        private bool canShootSecondary = true;

        // Start is called before the first frame update
        void Start()
        {
            SetUpMoveBoundaries();
        }

        private void OnDestroy()
        {
            Level level = FindObjectOfType<Level>();
            level.LoadGameOver();
        }

        private void Update()
        {
            HandleMovement();
            HandleFire();
        }

        private void HandleMovement()
        {
            float deltaX = Input.GetAxisRaw("Horizontal") * shipSpeed * Time.deltaTime;
            float deltaY = Input.GetAxisRaw("Vertical") * shipSpeed * Time.deltaTime;
        
            float newXPosition = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
            float newYPosition = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
        
            transform.position = new Vector2(newXPosition, newYPosition);
        }

 
        private void HandleFire()
        {
            bool shootButton = Input.GetButton("Fire1");
            if (canShootPrimary & shootButton)
            {
                canShootPrimary = false;
                StartCoroutine(FireContinuously(primaryWeapon, primaryFireRate, true));
            }

            if (canShootSecondary && secondaryBulletsCount > 0 && Input.GetButton("Fire2"))
            {
                canShootSecondary = false;
                StartCoroutine(FireContinuously(secondaryWeapon, secondaryFireRate, false));
                secondaryBulletsCount--;
            }
        }
        
        private void SetUpMoveBoundaries()
        {
            Camera gameCamera = Camera.main;
        
            xPadding = transform.localScale.x / 2;
            yPadding = transform.localScale.y / 2;
        
            xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
            xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        
            yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
            yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
        }

        private IEnumerator FireContinuously(GameObject weapon, float fireRate, bool isPrimary)
        {
            
            GameObject instance = Instantiate(weapon, transform.position, Quaternion.identity);
            instance.GetComponent<Shootable>().Init(Direction.Up, LayerMask.NameToLayer("Player Projectile"));

            yield return new WaitForSeconds(fireRate);
            if (isPrimary)
            {
                canShootPrimary = true;
            }
            else canShootSecondary = true;
        }
    }
}
