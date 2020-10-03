using System;
using Script.Utils.CameraControllers;
using UnityEngine;

namespace Script.Utils.CameraControllers
{
    public class NewCamController : MonoBehaviour
    {
        public CamStage startCamStage;
        
        public CamBrain _camBrain;
        public CamHolder[] camerasInGame;

        private CamStage _currentCamStage;
        private void Start()
        {
            SetActiveCam(startCamStage);
            _currentCamStage = startCamStage;
        }

        public void NextCamStage()
        {
            _currentCamStage++;
            SetActiveCam(_currentCamStage);
        }

        public void SetActiveCam(CamStage camStage)
        {
            foreach (var cam in camerasInGame)
            {
                if(cam.CamStage == camStage)
                    _camBrain.SetCamPoint(cam.TargetCamPoint);
            }
        }

        
        //TEST
        /*private void Update()
        {
            if (Input.GetButtonDown("Jump"))
            {
                NextCamStage();
            }
        }*/
    }
}

[Serializable]
public class CamHolder
{
    public CamStage CamStage;
    public TargetCamPoint TargetCamPoint;
}
public enum CamStage
{
    Start,
    Game,
    Finish
}