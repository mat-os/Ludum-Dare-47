using System;
using Game.Framework;
using UnityEngine;

public class StartPoint : Point
{
    [Header("PointSettings")]
    public float startVelocity = 5;

    public Player _localPlayer;

    private void Awake()
    {
        SoundController.Instance.BeforeStart();
    }

    private void Update()
    {
        if (_localPlayer.PrevPoint == null && (Input.GetKeyUp(KeyCode.UpArrow)
                                                || Input.GetKeyUp(KeyCode.W)
                                                || Input.GetKeyUp(KeyCode.Space)
                                                || Input.GetKeyUp(KeyCode.DownArrow)
                                                || Input.GetKeyUp(KeyCode.S)
                                                || Input.GetKeyUp(KeyCode.LeftArrow)
                                                || Input.GetKeyUp(KeyCode.A)
                                                || Input.GetKeyUp(KeyCode.RightArrow)
                                                || Input.GetKeyUp(KeyCode.D)))
        {
            Prepare();
            Launch();
        }
    }

    public void Prepare()
    {
        SoundController.Instance.BeforeStart();
        var localPlayerTransform = _localPlayer.transform;
        localPlayerTransform.position = transform.position;
        var transformEulerAngles = localPlayerTransform.eulerAngles;
        transformEulerAngles = Vector3.zero;
        localPlayerTransform.eulerAngles = transformEulerAngles;
        _localPlayer.PrevPoint = null;
        _localPlayer.Velocity = 0;
    }

    public void Launch()
    {
        SoundController.Instance.AfterStart();
        _localPlayer.PrevPoint = this;
        _localPlayer.StartPoint = this;
        _localPlayer.NextPoint = GetNextPoint(this);
        _localPlayer.NextPoint.SetAsNext(_localPlayer);
        _localPlayer.Velocity = startVelocity;
    }
}