using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Level : MonoBehaviour
{
    public static event Action<int> OnLapsChanged;
    public static event Action CompletedPuzzle;

    [SerializeField] bool _verbose = true;
    [SerializeField] int _lapsToWin = 3;

    private bool _hasDirection = false; // which direction the player decided to go
    private bool _goingFuture = false; // if the direction is future or past
    private bool _goingCorrectDirection = true; // Correct direction is the direction we started (after has direction)
    private bool _passedOtherBoundary = false; // If they have passed the other boundary they needed to
    private int _laps = -1; // undefined number of laps, the user has not chosen a direction

    private PlayerInput _playerInput;

    private void ResetVariables()
    {
        print("RESET");
        _hasDirection = false;
        _goingFuture = false;
        _goingCorrectDirection = true;
        _passedOtherBoundary = false;
        _laps = -1;
    }
    private void Awake()
    {
        Boundary.OnBoundaryCollision += CheckBoundaries;
        _playerInput = GetComponent<PlayerInput>();
        ExitPlate.TouchedExitPlate += DeactivateInput;
        ResetVariables();
    }

    private void Start()
    {
        OnLapsChanged?.Invoke(_laps);
        // Change to -1 so I can test from the beginningh
    }

    private void CheckBoundaries(bool futureBoundary)
    {
        ChangeLaps(futureBoundary);
        if(_laps >= _lapsToWin)
        {
            CompletedPuzzle?.Invoke();
        }
        OnLapsChanged?.Invoke(_laps); // Not every time they are changed this changes, but I do not care lol
    }

    private void DeactivateInput()
    {
        //print("Player input: " + _playerInput.inputIsActive);
        _playerInput.DeactivateInput();
        //print("Type of message: " + _playerInput.inputIsActive);
    }

    public void OnFire(InputValue input)
    {
        print("\nGoing future: " + _goingFuture + "\nOther bundary has been touched: " + _passedOtherBoundary + "\nLaps: " + _laps + "\nCorrect direction: " + _goingCorrectDirection);
    }

    private void ChangeLaps(bool futureBoundary)
    {
        if (!_hasDirection) // No boundary has been touched / player came back and the laps were reset
        {
            ResetVariables();
            _laps = 0;
            
            _goingFuture = futureBoundary; // Set the idrection I am going, just for reference. Other calculations are done based on "correct" or "incorrect
            _hasDirection = true;
            
            if(_verbose) print("0) Started.");
            return;
        }

        switch (futureBoundary)
        {
            case true:
                if (_goingFuture && futureBoundary && _passedOtherBoundary && _goingCorrectDirection) // If i touch the selected boundary again and and passedOherBoudnary is true, then I have completed a lap
                {
                    _laps++;
                    _passedOtherBoundary = false;
                    if (_verbose) print("0.0) THIS Touched selected boundary, going in the correct direction, reset having touched other boundary.");
                    return;
                }

                if (_goingFuture && futureBoundary && _passedOtherBoundary && _goingCorrectDirection)
                {
                    // _laps++;
                    // _passedOtherBoundary = false;
                    if (_verbose) print("**) Can this happen?");
                    return;
                }

                if (!_goingFuture && futureBoundary && !_passedOtherBoundary && _goingCorrectDirection) // 3) but I am going into correct directuin because I am going to the future
                {
                    _passedOtherBoundary = true;
                    if (_verbose) print("0.1) Touched other boundary while going into the correct direction: past. Reset having touched other boundary.");
                    return;
                }

                if (_goingFuture && futureBoundary && !_passedOtherBoundary && _goingCorrectDirection) // If I touch the selected boundary but I have not passed the other boundary and I am starting to go in the incorrect direction after just completing a lap. So I loose a lap. IT IS THE FIRST TIME I GO IN THE WRONG DIRECTION
                {
                    _laps--;
                    if (_laps < 0) // I reset if my laps are 0 because it means the player can now go into the different time
                    {
                        ResetVariables();
                    }
                    else
                    {
                        _passedOtherBoundary = true;
                        _goingCorrectDirection = false;
                    }
                    if (_verbose) print("0.2) Started going in the wrong direction aftre completing a lap."); // Backwards in selection as a concept, can be either past or future
                    return;
                }

                if (!_goingFuture && futureBoundary && !_passedOtherBoundary && _goingCorrectDirection)
                {
                    /* _laps--;
                    if (_laps < 0) 
                    {
                        ResetVariables();
                    }
                    else
                    {
                        _goingCorrectDirection = false;
                    }*/
                    if (_verbose) print("**) Something IDK."); 
                    return;
                }

                if (_goingFuture && futureBoundary && !_passedOtherBoundary && !_goingCorrectDirection) // ** Continued goping wrong
                {
                    _laps--;
                    if (_laps < 0)
                    {
                        ResetVariables();
                    }
                    else
                    {
                        _passedOtherBoundary = true;
                    }
                    if (_verbose) print("0.3) Touched selected boundary without having touched other boundary, so went wrong."); // Forward in selection as a concept, can be either past or future
                    return;
                }

                if (_goingFuture && futureBoundary && _passedOtherBoundary && !_goingCorrectDirection) // I am completing my wrong direction lapp
                {
                    _laps++;
                    /*_laps--;
                    if (_laps < 0)
                    {
                        ResetVariables();
                    }*/
                    _passedOtherBoundary = false;
                    if (_verbose) print("0.4) Touched sleected boundary after touching other boundary and going in the wrong direction, so completed a lap and going correct");
                    return;
                }

                if(!_goingFuture && futureBoundary && _passedOtherBoundary && !_goingCorrectDirection)
                {
                    _passedOtherBoundary = false;
                    print("0.5) I continue to go on the worng directino and just passed the other boundary");
                    return;
                }

                if (!_goingFuture && futureBoundary && !_passedOtherBoundary && !_goingCorrectDirection) // COntinue to go in wring directino (passed other boundary)
                {
                    //_passedOtherBoundary = true;
                    if (_verbose) print("0.6) IDK if this is correct"); // 
                    return;
                }
                break;
            case false:
                if (!_goingFuture && !futureBoundary && !_passedOtherBoundary && _goingCorrectDirection) // If I am not going to the future and I touuch the not future boundary after I have not passed the other boundary and I am going into the correct direction then i started going  into the wrong direction, which would be the future
                {
                    _laps--;
                    if (_laps < 0) // I reset if my laps are 0 because it means the player can now go into the different time
                    {
                        ResetVariables();
                    }
                    else
                    {
                        print("tHis?");
                        _passedOtherBoundary = true; // Reset having pssed through the other boundary cause techncially I have after loosing the lap
                        _goingCorrectDirection = false;
                    }
                    if (_verbose) print("1.0) The other option?");
                    return;
                }

                if (_goingFuture && !futureBoundary && !_passedOtherBoundary && _goingCorrectDirection) // If I am touching the other boundary and I am going in the correct direction then I am in the correct path to complete a lap, so I make true my _passedOtherBoundary true so whenever i touch the correct boundary one again, I complete one lap
                {
                    _passedOtherBoundary = true;
                    if (_verbose) print("1.1) Touched other boundary, going in the correct direction."); // SHould not be called when the correct direction is going into the not futyre
                    return;
                }

                if (_goingFuture && !futureBoundary && !_passedOtherBoundary && !_goingCorrectDirection)
                {
                    _goingCorrectDirection = true;
                    _passedOtherBoundary = true;

                    if (_verbose) print("1.2) Touched other boundary while going in the wrong direction, so now I go in correct direction.");
                    return;
                }

                if (_goingFuture && !futureBoundary && !_passedOtherBoundary && !_goingCorrectDirection) // Started going in the correct direction after going to the [ast
                {
                    _goingCorrectDirection = true;

                    if (_verbose) print("1.3) Touched other boundary while going in the wrong direction, so now I go in the correct direction because I am going to the past.");
                    return;
                }

                if (_goingFuture && !futureBoundary && _passedOtherBoundary && !_goingCorrectDirection) // If I am touching the other boundary, and I am going NOT in the correct direction 
                {
                    _passedOtherBoundary = false;
                    if (_verbose) print("1.4) Touched past boundary and currently going to the future.");
                    return;
                }

                if (!_goingFuture && !futureBoundary && _passedOtherBoundary && !_goingCorrectDirection) /// Now I am going in the ocrrect directino and i completed a lap to the past
                {
                    _laps++;
                    _passedOtherBoundary = false;
                    _goingCorrectDirection = true;

                    if (_verbose) print("1.5) I am now going correctly to the past.");
                    return;
                }

                /* if (!_goingFuture && !futureBoundary && _passedOtherBoundary && !_goingCorrectDirection) // I am going wring in the past so I strated gioing to the future
                {
                    _laps--;
                    if (_laps < 0)
                    {
                        ResetVariables();
                    }
                    else
                    {
                        _passedOtherBoundary = false;
                    }
                    
                    if (_verbose) print("1.5) Went so wrong in the past that I removed a lap");
                    return;
                }*/

                if (_goingFuture && !futureBoundary && _passedOtherBoundary && _goingCorrectDirection) // If I touch the other boundary and I passed the otehr boundary before and I was going in the correct direction then I started to go in the wrong direction
                {
                    _goingCorrectDirection = false;
                    if (_verbose) print("1.6) Touched other boundary and STARTED to go wrong.");
                    return;

                }

                if (!_goingFuture && !futureBoundary && _passedOtherBoundary && _goingCorrectDirection)
                {
                    _laps++;
                    _passedOtherBoundary = false;
                    if (_verbose) print("1.7) Touched selected boundary while going to the past so completed a lap.");
                    return;

                }
                if(!_goingFuture && !futureBoundary && !_passedOtherBoundary && !_goingCorrectDirection) // Started going in the correetc direction again after going in the wrong direction
                {
                    _laps--;
                    if (_laps < 0)
                    {
                        ResetVariables();
                    }
                    if (_verbose) print("1.8) Continue to go wrong in the past direction, so now I am going to the future");
                    return;
                }
                break;
        }
        print("Not contemplated");
    }

    /*
     * 
        

     * private void ChangeLaps(bool futureBoundary)
    {
        if (!_goingFuture && !_goingPast) // No boundary has been touched / player came back and the laps were reset
        {
            if(futureBoundary) _goingFuture = true;
            if(!futureBoundary) _goingPast = true;
            _passedOtherBoundary = false; // I reset passedOtherBoundary
            _goingCorrectDirection = true;
            _laps = 0;
            if(_verbose) print("0) Started.");
            return;
        }
        if (!futureBoundary && _goingFuture && !_passedOtherBoundary && _goingCorrectDirection) // If I am touching the past boundary and I am currently going to the future, and I am going in the correct direction then I am completing a future lap, so I make true my _passedOtherBoundary flag so whenever i touch the future one again, I complete one lap
        {
            _passedOtherBoundary = true;
            if (_verbose) print("1) Touched past boundary and currently going to the future.");
            return;
        }
        if (futureBoundary && _goingFuture && _passedOtherBoundary && _goingCorrectDirection) // If i touch the going future boundary again and I was going to the future and passedOherBoudnary is true, then I have completed a lap into the future
        {
            _laps++;
            _passedOtherBoundary = false;
            _goingCorrectDirection = true;
            if (_verbose) print("2) Touched future boundary and currently going to the future and passedOtherBoundary is true.");
            return;
        }
        if (!futureBoundary && _goingFuture && _passedOtherBoundary && _goingCorrectDirection) // If I am touching the past boundary and I am currently going to the future, and I am going in the correct direction BUT I touched the passed boundary again, then it means I start going in the wrong direction, so I changes _passed boundary into false and change to wrong direction
        {
            _goingCorrectDirection = false;
            _passedOtherBoundary = false;
            if (_verbose) print("3) Touched past boundary and currently going 'wrong' (not direction I was going, which was future.)");
            return;
        }
        if (futureBoundary && _goingFuture && !_passedOtherBoundary && _goingCorrectDirection) // If I touch the future boundary and I am going to the future but I have not passed the other boundary and I am going in the correct direction then I am going back after just copleting a lap? So I loose a lap. IT IS THE FIRST TIME I GO IN THE WRONG DIRECTION
        {
            _laps--;
            if(_laps < 0) // I reset
            {
                print("Reseted.");
                _goingFuture = false;
                _goingPast = false;
                _goingCorrectDirection = true;
                _passedOtherBoundary = false;
            }
            else
            {
                _goingCorrectDirection = false;
            }
            if (_verbose) print("4) Touched future boundary and currently going to the future and passedOtherBoundary is false.");
            return;
        }
        if (futureBoundary && _goingFuture && !_passedOtherBoundary && !_goingCorrectDirection) // 4) But I continue to go in the wrong direction
        {
            _laps--;
            if(_laps < 0)
            {
                print("Reseted.");
                _goingPast = false;
                _goingFuture = false;
                _goingCorrectDirection = true;
                _passedOtherBoundary = false;
            }
            else
            {
                _goingCorrectDirection = false; // Do not need it
            }
            if (_verbose) print("5) Continue to go wrong.");
        }
        if (!futureBoundary && _goingFuture && !_goingCorrectDirection) // If I am touching the past boundary and I am currently going to the future, and I am going NOT in the correct direction then I am completing a future lap, so I make true my _passedOtherBoundary flag so whenever i touch the future one again, I complete one lap
        {
            _passedOtherBoundary = false;
            if (_verbose) print("6) Touched past boundary and currently going to the future.");
            return;
        }

    }
    */
}
