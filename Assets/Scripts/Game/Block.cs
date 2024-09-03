using System;
using UnityEngine;

namespace Arkanoid.Game
{
    public class Block : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private bool _isHit;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_isHit)
            {
                _isHit = true;
                _spriteRenderer.color = _color;
                return;
            }
            Debug.Log($"OnCollisionEnter2D");
            Destroy(gameObject);
        }
        
        // private void OnCollisionStay2D(Collision2D other) //inside collision
        // {
        //     Debug.Log($"OnCollisionStay2D");
        // }
        // private void OnCollisionExit2D(Collision2D other) //inside collision
        // {
        //     Debug.Log($"OnCollisionExit2D");
        // }
        
    }
}
