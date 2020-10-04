using UnityEngine;

namespace Game.Framework
{
    public class SoundController : MonoBehaviourSingleton<SoundController>
    {
        public GameObject start;
        public GameObject play;

        public void AfterStart()
        {
            play.SetActive(true);
            start.SetActive(false);
        }
        
        public void BeforeStart()
        {
            play.SetActive(false);
            start.SetActive(true);
        }
    }
}