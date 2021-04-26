using UnityEngine;
using UnityEngine.Events;

namespace Players
{
    public class KeyboardController : IInputController
    {
        public event UnityAction OnPressedJump;

        private float _horizontal;
        private float _vertical;
        
        private Vector3 _moveDirection;

        public Vector3 MoveDirection
        {
            get => _moveDirection;
            set => _moveDirection = value;
        }

        public void Input(Player player)
        {
            _horizontal = 0f;
            if (UnityEngine.Input.GetKey(KeyCode.A)) _horizontal = -1f;
            else if (UnityEngine.Input.GetKey(KeyCode.D)) _horizontal = 1f;

            _vertical = 0f;
            if (UnityEngine.Input.GetKey(KeyCode.W)) _vertical = 1f;
            else if (UnityEngine.Input.GetKey(KeyCode.S)) _vertical = -1f;
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                OnPressedJump?.Invoke();
            
            MoveDirection = (player.transform.right * _horizontal + player.transform.forward * _vertical).normalized;
        }
    }
}