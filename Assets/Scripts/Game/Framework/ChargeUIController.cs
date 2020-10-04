using System;
using UnityEngine;

namespace Game.Framework
{
    public class ChargeUIController : MonoBehaviour
    {
        public Transform bars;
        public Sprite redSprite;
        public Sprite greenSprite;
        private Player _localPlayer;
        private SpriteRenderer _firstBar;

        private int _current;

        private void Start()
        {
            _localPlayer = Player.Instance;
            _firstBar = bars.GetChild(0).GetComponent<SpriteRenderer>();

            Redraw(2);
        }

        private void FixedUpdate()
        {
            if (_localPlayer.PrevPoint != null)
            {
                var roundToInt = Mathf.RoundToInt(_localPlayer.Velocity);
                if (roundToInt != _current)
                {
                    Redraw(roundToInt);
                }
            }
        }

        private void Redraw(int value)
        {
            _current = value;
            for (var i = 0; i < bars.childCount; i++)
            {
                bars.GetChild(i).gameObject.SetActive(i < _current);
            }

            _firstBar.sprite = _current == 1 ? redSprite : greenSprite;
        }
    }
}