using UnityEngine;
using UnityEngine.Events;

namespace Players
{
    public class Player : MonoBehaviour
    {
        public event UnityAction OnDied;
        
        public bool CanReborn;
        
        public CharacterController CharacterController { get; private set; }
        public IInputController InputController { get; private set; }
        public IMovement Movement { get; private set; }
        

        private float _timer;
        
        private void Start()
        {
            CharacterController = GetComponent<CharacterController>();
            
            InputController = new KeyboardController();
            
            Movement = GetComponent<IMovement>();
            Movement.Init(this);
            
            InputController.OnPressedJump += Movement.Jump;
        }

        private void Update()
        {
            InputController.Input(this);
            Movement.Move();
        }

        private void OnDestroy()
        {
            InputController.OnPressedJump -= Movement.Jump;
        }

        /*
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
        }
        */
    }
}