using System.Collections;
using DG.Tweening;
using Script.Utils.CameraControllers;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CamBrain : MonoBehaviour
{
    private Camera _thisCam;

    private TargetCamPoint _currentCamPoint;

    private bool _isLookAtTarget;
    private bool _isFollowTarget;
    private GameObject _target;

    private void Awake()
    {
        _thisCam = GetComponent<Camera>();
    }

    public void SetCamPoint(TargetCamPoint newPoint)
    {
        _currentCamPoint = newPoint;
        _isLookAtTarget = newPoint.LookOnTarget;
        _isFollowTarget = newPoint.FollowTarget;

        if (_isLookAtTarget)
        {
            StartCoroutine(LookAtTargetRoutine(_currentCamPoint.Target));
            if(!_isFollowTarget)
                transform.DOMove(newPoint.transform.position, newPoint.TimeToChange).SetEase(newPoint.ChangeEase);
        }
        if (_isFollowTarget)
        {
            if (_currentCamPoint.SmoothSetupOnPosition)
                transform.DOMove(newPoint.transform.position, newPoint.TimeToChange).SetEase(newPoint.ChangeEase).OnComplete(
                    () =>
                    {
                        StartCoroutine(FollowTargetRoutine());
                    });
            else
            {
                StartCoroutine(FollowTargetRoutine());
            }
            
            if(!_isLookAtTarget)
                transform.DORotateQuaternion(newPoint.transform.rotation, newPoint.TimeToChange).SetEase(newPoint.ChangeEase);
        }
        if(!_isFollowTarget && !_isFollowTarget)
        {
            transform.DOMove(newPoint.transform.position, newPoint.TimeToChange).SetEase(newPoint.ChangeEase);
            transform.DORotateQuaternion(newPoint.transform.rotation, newPoint.TimeToChange).SetEase(newPoint.ChangeEase);
        }

        // _thisCam.DOFieldOfView(newPoint.Fov, newPoint.TimeToChange);
    }
    
    private IEnumerator LookAtTargetRoutine(GameObject target)
    {
        while (_isLookAtTarget)
        { 
            _thisCam.transform.LookAt(target.transform.position, Vector3.up);
            yield return null;
        }
    }
    private Vector3 velocity = Vector3.zero;
    IEnumerator FollowTargetRoutine()
    {
        while (_isFollowTarget)
        {
            //var currentPos = transform.position;
            
            //var newPos = currentPos;
            //newPos.x = _currentCamPoint.Target.transform.position.x - _currentCamPoint.StartOffset.x;

            var newPos = _currentCamPoint.transform.position;
            //currentPos =  Vector3.Lerp(currentPos, newPos, Time.deltaTime * _currentCamPoint.FollowMovementSmoother);
            //_thisCam.transform.position = currentPos;
            
            transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, _currentCamPoint.FollowMovementSmoother);
            yield return null;
        }
    }
}
