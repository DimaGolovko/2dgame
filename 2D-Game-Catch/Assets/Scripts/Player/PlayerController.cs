using Collectibles.Common.Classes;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float jumpForce = 10f;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private AudioClip collectSound;
        private AudioSource audioSource;
        private InputHorizontalAxisDecorator _input;
        private int score = 0;
        private float sceneWidth;
        public bool IsGrounded { get; private set; }
        
        void Start()
        {
            _input = new InputHorizontalAxisDecorator();
            
            var camHeight = Camera.main.orthographicSize * 2f;
            sceneWidth = camHeight * Camera.main.aspect;

            audioSource = gameObject.AddComponent<AudioSource>();
        }

        void Update()
        {
            var horizontalInput = _input.HorizontalInput;
            var moveDirection = new Vector2(horizontalInput, 0);
         
            MovePlayer(moveDirection);

            if (Input.GetButtonDown("Jump") && IsGrounded)
            {
                Jump();
            }

            ClampPlayerPosition();
        }
        
        void FixedUpdate()
        {
            CheckGrounded();
        }

        private void MovePlayer(Vector2 moveDirection)
        {
            var velocity = GetComponent<Rigidbody2D>().velocity;
            velocity.x = moveDirection.x * speed;
            GetComponent<Rigidbody2D>().velocity = velocity;
        }

        private void ClampPlayerPosition()
        {
            var clampedX = Mathf.Clamp(transform.position.x, -sceneWidth / 2f, sceneWidth / 2f);
            transform.position = new Vector2(clampedX, transform.position.y);
        }

        private void Jump()
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpForce);
            IsGrounded = false;
        }

        private void CheckGrounded()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, groundLayer);
            IsGrounded = hit.collider != null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Item"))
            {
                var item = other.GetComponent<Item>();
                item.SetPlayerTransform(transform);
                item.Collect();
                IncreaseScore(item.PointValue);
                PlayCollectSound();
            }
        }

        private void PlayCollectSound()
        {
            if (audioSource != null && collectSound != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
        }

        private void IncreaseScore(int points)
        {
            score += points;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            Debug.Log("Score: " + score);
        }
    }
}