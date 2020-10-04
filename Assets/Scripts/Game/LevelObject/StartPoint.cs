using Game.Framework;
using Game.GameManager;
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

    public override void Apply(Player player)
    {
        player.StartPoint = this;
        player.Velocity = 0;
        Prepare();
    }

    private void Update()
    {
        if (_localPlayer.StartPoint == this && _localPlayer.PrevPoint == null && (Input.GetKeyUp(KeyCode.UpArrow)
                                                || Input.GetKeyUp(KeyCode.W)
                                                || Input.GetKeyUp(KeyCode.Space)
                                                || Input.GetKeyUp(KeyCode.DownArrow)
                                                || Input.GetKeyUp(KeyCode.S)
                                                || Input.GetKeyUp(KeyCode.LeftArrow)
                                                || Input.GetKeyUp(KeyCode.A)
                                                || Input.GetKeyUp(KeyCode.RightArrow)
                                                || Input.GetKeyUp(KeyCode.D))
            && GameController.Instance.isStarted)
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
        localPlayerTransform.eulerAngles = transform.eulerAngles;
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