using System;
using System.Collections;
using System.Collections.Generic;
//using TapticPlugin;
using UnityEngine;

public class VibrationController : MonoBehaviour
{
    [SerializeField]private Sprite enableSprite = null;
    [SerializeField]private Sprite disableSprite = null;
    private bool isVibroEnabled
    {
        get
        {
            return PlayerPrefs.GetInt("TearVibroSettings", 0) >= 1;
        }

        set
        {
            var intVal = value == true ? 1 : 0;
            PlayerPrefs.SetInt("TearVibroSettings", intVal) ;
        }
    } 
    
    private void Start()
    {
        UpdateVibroIcon();
    }

    private void UpdateVibroIcon()
    {
        var sprite = isVibroEnabled  ? enableSprite : disableSprite;
        //GameplayManager.Instance.UiManager.GameplayUiController.ChangeVibrationButtonImage(sprite);
    }

    public void ChangeIsVibroEnabled()
    {
        isVibroEnabled = !isVibroEnabled;
        UpdateVibroIcon();

        if (isVibroEnabled)
        {
            //Vibrate(ImpactFeedback.Medium);
        }
    }
    
    /*public void Vibrate(ImpactFeedback impactFeedback)
    {
        if (isVibroEnabled)
        {
#if UNITY_IOS
            TapticManager.Impact(impactFeedback);
#else
            Handheld.Vibrate();
#endif
        }
    }*/
}
