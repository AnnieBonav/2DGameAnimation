using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Spine.Unity;

public class CalendarHandler : MonoBehaviour
{
    [SpineAnimation][SerializeField] private string _moveWeirdAnimation;
    [SpineAnimation][SerializeField] private string _moveCenterAnimation;
    [SpineAnimation][SerializeField] private string _moveMiddleAnimation;
    [SpineAnimation][SerializeField] private string _moveOuterAnimation;

    [SerializeField] private SkeletonAnimation _calendarSkeletonAnimation;
    [SerializeField] private Spine.AnimationState _animationState;
    [SerializeField] private Spine.Skeleton _calendarSkeleton;

    [SerializeField] private float _animationTimeScale = 1f;

    private bool _animationRunning = true;


    private void Awake()
    {
        _calendarSkeletonAnimation.timeScale = _animationTimeScale;
        _animationState = _calendarSkeletonAnimation.AnimationState;
        _calendarSkeleton = _calendarSkeletonAnimation.Skeleton;

        _animationState.SetAnimation(0, _moveWeirdAnimation, true);
    }

    private void FixedUpdate()
    {
        //_calendarSkeletonAnimation.timeScale = _animationTimeScale;
    }

    public void OnPoint(InputValue value)
    {
    }

    public void IncreaseSpeed()
    {
        if( _animationRunning ) _calendarSkeletonAnimation.timeScale = 2 * _animationTimeScale;
    }

    public void DecreaseSpeed()
    {
        if(_animationRunning) _calendarSkeletonAnimation.timeScale = _animationTimeScale; // Do not change if animaiton is paused
    }

    public void ChangeState()
    {
        if (_animationRunning) // Then pause
        {
            _calendarSkeletonAnimation.timeScale = 0;
        }
        else
        {
            _calendarSkeletonAnimation.timeScale = _animationTimeScale;
        }

        _animationRunning = !_animationRunning;

    }
}
