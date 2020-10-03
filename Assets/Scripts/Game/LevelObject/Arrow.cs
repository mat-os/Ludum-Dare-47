using UnityEngine;

namespace Game.LevelObject
{
    public class Arrow : MonoBehaviour
    {
        public SpriteRenderer sprite;

        public void SetColor(Color color)
        {
            sprite.color = color;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}