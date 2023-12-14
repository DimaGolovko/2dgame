using UnityEngine;

namespace Player
{
    public class InputHorizontalAxisDecorator
    {
        public float HorizontalInput => Input.GetAxis("Horizontal");
    }
}