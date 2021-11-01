using System;
using System.Collections;
using UnityEngine;

namespace Behaviors
{
    public class Blinking : MonoBehaviour
    {
        private SpriteRenderer sprite;
        private Color originalColor;
        
        [SerializeField] private Color red = new Color(255, 0, 0);
        [Range(0, 10)][SerializeField] private float speed = 10;

        public bool ShouldBlink {get; set;}
        
        private void Start()
        {
            sprite = GetComponent<SpriteRenderer>();
            originalColor = sprite.color;
        }

        private void Update()
        {
            if (ShouldBlink)
            {
                sprite.color = BlinkBetween(originalColor, red, speed);    
            }
        }

        private Color BlinkBetween(Color firstColor, Color secondColor, float speed)
        {
            return Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time * speed, 1));
        }
        
        
    }
}