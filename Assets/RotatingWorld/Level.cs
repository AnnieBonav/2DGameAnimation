using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] bool _verbose = true;
    private bool _goingFuture = false;
    private bool _goingPast = false;
    private bool _goingCorrectDirection = true; // Correct direction is the direction we started going in

    private int _laps = -1;
    private bool _passedOtherBoundary = false;

    private void Awake()
    {
        Boundary.OnBoundaryCollision += ChangeLaps;
    }

    private void ChangeLaps(bool futureBoundary)
    {
        if(!_goingFuture && !_goingPast) // No boundary has been touched / player came back and the laps were reset
        {
            if(futureBoundary) _goingFuture = true;
            if(!futureBoundary) _goingPast = true;
            _passedOtherBoundary = false; // I reset passedOtherBoundary
            _goingCorrectDirection = true;
            _laps = 0;
            if(_verbose) print("0) Touched future boundary: " + _goingFuture + " passed Other Boundary: " + _passedOtherBoundary + " Laps: " + _laps + " Correct direction: " + _goingCorrectDirection);
            return;
        }
        if (futureBoundary && _goingFuture && !_passedOtherBoundary && _goingCorrectDirection) // Maybe I do not need _goingCorrect direction // If i touch the going future boundary again and I was going to the future and passedOherBoudnary is FALSE, then I went to the other direction, so I lost a lap
        {
            _laps--;
            _goingCorrectDirection = false;
            if (_verbose) print("1) Touched future boundary and currently going to the future and passedOtherBoundary is false." + " Future: " + _goingFuture + " Past: " + _goingPast + " Passed: " + _passedOtherBoundary + "Laps: " + _laps + " Correct direction: " +  _goingCorrectDirection);
            return;
        }
        if (futureBoundary && _goingFuture && _passedOtherBoundary && _goingCorrectDirection) // If i touch the going future boundary again and I was going to the future and passedOherBoudnary is true, then I have completed a lap into the future
        {
            _laps++;
            _passedOtherBoundary = false;
            _goingCorrectDirection = true;
            if (_verbose) print("2) Touched future boundary and currently going to the future and passedOtherBoundary is true." + " Future: " + _goingFuture + " Past: " + _goingPast + " Passed: " + _passedOtherBoundary + "Laps: " + _laps + " Correct direction: " + _goingCorrectDirection);
            return;
        }
        if (!futureBoundary && _goingFuture && _goingCorrectDirection) // If I am touching the past boundary and I am currently going to the future, and I am going in the correct direction then I am completing a future lap, so I make true my _passedOtherBoundary flag so whenever i touch the future one again, I complete one lap
        {
            _passedOtherBoundary = true;
            if(_verbose) print("3) Touched past boundary and currently going to the future." + " Future: " + _goingFuture + " Past: " + _goingPast + " Passed: " + _passedOtherBoundary + "Laps: " + _laps + " Correct direction: " + _goingCorrectDirection);
            return;
        }
        if (!futureBoundary && _goingFuture && !_goingCorrectDirection) // If I am touching the past boundary and I am currently going to the future, and I am going NOT in the correct direction then I am completing a future lap, so I make true my _passedOtherBoundary flag so whenever i touch the future one again, I complete one lap
        {
            // _passedOtherBoundary = false;
            if (_verbose) print("4) Touched past boundary and currently going to the future." + " Future: " + _goingFuture + " Past: " + _goingPast + " Passed: " + _passedOtherBoundary + "Laps: " + _laps + " Correct direction: " + _goingCorrectDirection);
            return;
        }
        /* if (!futureBoundary && _goingFuture && _goingCorrectDirection) // If I am touching the past boundary and I am currently going to the future, BUT I am going in the wrong direction, I am removing a future lap, so I keep _passedOtherBoundary false, and
        {
            _passedOtherBoundary = true;
            if (_verbose) print("Touched past boundary and currently going to the future." + " Future: " + _goingFuture + " Past: " + _goingPast + " Passed: " + _passedOtherBoundary + "Laps: " + _laps);
            return;
        }*/



    }
}
