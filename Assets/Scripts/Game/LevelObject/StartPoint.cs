using UnityEngine;

public class StartPoint : Point
{
    [Header("PointSettings")]
    public float startVelocity = 5;

    public Player _localPlayer;

    private void Awake()
    {
        _localPlayer.transform.position = transform.position;
    }

    private void Update()
    {
        if (_localPlayer.StartPoint == null && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            Launch();
        }
    }

    private void Launch()
    {
        _localPlayer.StartPoint = this;
        _localPlayer.NextPoint = GetNextPoint(this);
        _localPlayer.Velocity = startVelocity;
    }
}