using Collectibles.Common.Interfaces;
using UnityEngine;

namespace Collectibles.Common.Classes
{
    public class Item : MonoBehaviour, ICollectible
    {
        private Transform _playerTransform;

        public int PointValue => 1;
        public string Tag => "Item";

        public void SetPlayerTransform(Transform player)
        {
            _playerTransform = player;
        }

        private void Update()
        {
            if (transform.position.y < -6f)
            {
                Destroy(gameObject);
            }
        }
        
        public void Collect()
        {
            if (IsCollidingWithPlayer())
            {
                Destroy(gameObject);
                return;
            }
            
            Destroy(gameObject);
        }

        private bool IsCollidingWithPlayer()
        {
            if (_playerTransform == null)
                return false;

            var playerCollider = _playerTransform.GetComponent<BoxCollider2D>();
            var eggCollider = GetComponent<BoxCollider2D>();

            if (playerCollider == null || eggCollider == null || !eggCollider.IsTouching(playerCollider))
                return false;
            
            return true;
        }
    }
}