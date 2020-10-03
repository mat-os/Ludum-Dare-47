using UnityEngine;

public class StartPoint : Point
{
    [Header("PointSettings")]
    public float startVelocity = 5;

    public Player _player;

    private void Awake()
    {
        _player.transform.position = transform.position;
    }

    private void Update()
    {
        if (_player.StartPoint == null && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)))
        {
            Launch();
        }
    }

    private void Launch()
    {
        _player.StartPoint = this;
        _player.NextPoint = GetNextPoint(this);
        _player.Velocity = startVelocity;
    }
}