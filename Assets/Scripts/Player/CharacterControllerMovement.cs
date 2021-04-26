using UnityEngine;

namespace Players
{
    public class CharacterControllerMovement : MonoBehaviour, IMovement
    {
        [Header("Movement")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _airMovementSpeed;
        [SerializeField] private float _movementReaction;
        
        [Header("Jump")]
        [SerializeField] private float _gravityCoefficient;
        [SerializeField] private float _jumpHeight;
        
        [Header("GroundCheck")]
        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundDistance = 1f;
        [SerializeField] private LayerMask _groundMask;

        private Player _player;
        private Vector3 _velocity;
        
        private bool _isGrounded;
        private bool _isRoofed;
        private bool _isWalled;
        
        private bool _isJump;
        private bool _hasAcceleration;
        
        private float _movement;
        private float _currentAcceleration;

        private float _targetTimeScale;
        private float _currentTimeScale;

        private bool _stanning;

        public bool IsGrounded
        {
            get => _isGrounded;
            set => _isGrounded = value;
        }
        
        public Vector3 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        public void Init(Player player)
        {
            _player = player;

            _player.OnDied += ResetVelocity;
        }

        public void Move()
        {
            CheckCollisions();
            CalculateMovement();

            _player.CharacterController.Move(_player.InputController.MoveDirection * _movement * Time.deltaTime);

            //TryJump();
            ApplyGravity();
            
            _player.CharacterController.Move(_velocity * _gravityCoefficient * Time.deltaTime);

            _velocity = Vector3.Lerp(_velocity, new Vector3(0f, _velocity.y, 0f), Time.deltaTime * 5f);

            //Time.timeScale = Mathf.Lerp(Time.timeScale, _targetTimeScale, Time.fixedDeltaTime * 2f);
        }
        
        private void CheckCollisions()
        {
            _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
            
            if (_isRoofed && _stanning == false)
                _velocity.y = 2f;
            
            if (_isGrounded && _velocity.y < 0)
                _velocity.y = -2f;
        }

        private void ResetVelocity()
        {
            _velocity.y = 0f;
        }

        private void CalculateMovement()
        {
            if (_isGrounded || _isWalled || _isRoofed)
            {
                _movement = Mathf.Lerp(_movement, _movementSpeed * _currentAcceleration, _movementReaction * Time.deltaTime);
                //_movement = _movementSpeed * _currentAcceleration;
            }
            else
                _movement = Mathf.Lerp(_movement, _airMovementSpeed, _movementReaction * Time.deltaTime);
        }

        private void ApplyGravity()
        {
            _velocity.y += PhysicsConstants.Gravity * Time.deltaTime;
        }

        public void Jump()
        {
            if (_isGrounded)
                _velocity.y = Mathf.Sqrt(_jumpHeight) * _currentAcceleration;
        }
    }
}