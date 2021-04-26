using UnityEngine;
using UnityEngine.Events;

namespace Players
{
    public interface IInputController
    {
        event UnityAction OnPressedJump;
        
        Vector3 MoveDirection { get; set; }

        void Input(Player player);
    }
}