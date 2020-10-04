using UnityEngine;

namespace Game.Framework
{
    public class SoundController : MonoBehaviourSingleton<SoundController>
    {
        public GameObject start;
        public GameObject play;
        public GameObject final;

        public void AfterStart()
        {
            play.SetActive(true);
            start.SetActive(false);
            final.SetActive(false);
        }
        
        public void BeforeStart()
        {
            play.SetActive(false);
            start.SetActive(true);
            final.SetActive(false);
        }

        public void FinalMusic()
        {
            play.SetActive(false);
            start.SetActive(false);
            final.SetActive(true);
        }
    }
}