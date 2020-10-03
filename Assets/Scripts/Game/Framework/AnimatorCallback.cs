using UnityEngine;

namespace Game.Framework
{
    public class AnimatorCallback : MonoBehaviour
    {
        public Player proxy;

        public void TurnRight()
        {
            proxy.TurnRight();
        }

        public void TurnLeft()
        {
            proxy.TurnLeft();
        }
    }
}