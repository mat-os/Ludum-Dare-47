using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.GameManager
{
    public class GameController : MonoBehaviourSingleton<GameController>
    {
        public Image logo;
        public Image text;
        public Image panel;
        public bool isStarted;
        private bool _preStart;

        private Sequence _sequence;

        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
        private void Awake()
        {
            _sequence = DOTween.Sequence();
            _sequence.SetLoops(-1);
            _sequence
                .Append(text.DOFade(0, 0.6f))
                .Append(text.DOFade(1, 0.6f));
        }

        public void StartGame()
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/InsertCoin");
            _sequence.Kill();
            var sequence = DOTween.Sequence();
            sequence
                .Append(text.DOFade(0, 0.15f))
                .Append(text.DOFade(1, 0.15f))
                .Append(text.DOFade(0, 0.15f))
                .Append(text.DOFade(1, 0.15f))
                .Append(text.DOFade(0, 0.15f))
                .Append(text.DOFade(1, 0.15f))
                .Append(text.DOFade(0, 0.15f))
                .Append(text.DOFade(1, 0.15f))
                .Append(text.DOFade(0, 0.15f));
            sequence.Play();
            sequence.OnComplete(() =>
            {
                logo.DOFade(0, 0.5f);
                panel.DOFade(0, 0.5f);
                isStarted = true;
            });
        }

        private void Update()
        {
            if (!isStarted && !_preStart && Input.anyKey)
            {
                _preStart = true;
                StartGame();
            }
        }
    }
}