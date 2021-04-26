using UnityEngine;

namespace Players
{
    public interface IMovement
    {
        void Init(Player player);
        void Move();
        void Jump();
    }
}