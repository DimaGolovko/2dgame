using Player;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Animator _animator;
    private SpriteRenderer[] _spriteRenderers;
    
    void Start()
    {
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        _animator.SetBool("Grounded", _playerController.IsGrounded);
        var velocityX = _rb.velocity.x;
        if (velocityX > 0.1f)
        {
            foreach (var sprite in _spriteRenderers)
            {
                sprite.flipX = true;
            }
        }
        else if (velocityX < -0.1f)
        {
            foreach (var sprite in _spriteRenderers)
            {
                sprite.flipX = false;
            }
        }
        
        _animator.SetFloat("Speed", Mathf.Abs(velocityX));
    }
}
