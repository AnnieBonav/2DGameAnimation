using System;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// TODO: Implement changing time of day based on where they are going and not left or right
public enum Tense { Past, Present, Future };
public class Direction
{
    private Tense _tense;

    public Tense Tense {
        get { return _tense; }
        set { _tense = value; }
    }
    public Direction()
    {
        _tense = Tense.Past;
    }
    public Direction(Tense tense)
    {
        _tense = tense;
    }
    public bool IsFuture()
    {
        return _tense == Tense.Future;
    }

    public override string ToString()
    {
        return _tense.ToString();
    }

    public bool MatchesDirection(Direction direction) // the two directions have the same tense
    {
        if(_tense == direction.Tense)
        {
            return true;
        }
        return false;
    }
}
public class Level : MonoBehaviour
{
    public static event Action<int, string> OnLapsChanged;
    public static event Action CompletedPuzzle;

    [SerializeField] private bool _verbose = true;
    [SerializeField] private int _lapsToWin = 3;    

    private bool _goingCorrectDirection = true; // Correct direction is the direction we started (after has direction)
    private bool _passedOtherBoundary = false; // If they have passed the other boundary they needed to
    private int _laps = -1; // undefined number of laps, the user has not chosen a direction

    private PlayerInput _playerInput;
    private Direction _currentDirection;

    private void ResetVariables()
    {
        print("RESET");
        _currentDirection = null;
        _goingCorrectDirection = true;
        _passedOtherBoundary = false;
        _laps = -1;
        OnLapsChanged?.Invoke(_laps, "Not anymore");
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
        OnLapsChanged?.Invoke(_laps, "Not yet");
    }

    private void CheckBoundaries(Direction touchedDirection)
    {      
        ChangeLaps(touchedDirection); // The direction of the boundary that was touched
        if (_laps >= _lapsToWin && _currentDirection.Tense == Tense.Past)
        {
            CompletedPuzzle?.Invoke();
        }
        string printTense = "";
        if (_currentDirection == null) printTense = "undefined";
        else printTense = _currentDirection.ToString();
        OnLapsChanged?.Invoke(_laps, printTense); // Not every time they are changed this changes, but I do not care lol
    }

    private void DeactivateInput()
    {
        //print("Player input: " + _playerInput.inputIsActive);
        _playerInput.DeactivateInput();
        //print("Type of message: " + _playerInput.inputIsActive);
    }

    public void OnFire(InputValue input)
    {
        //print("\nGoing: " + _currentDirection.ToString() + "\nOther bundary has been touched: " + _passedOtherBoundary + "\nLaps: " + _laps + "\nCorrect direction: " + _goingCorrectDirection);
    }

    private void ChangeLaps(Direction touchedDirection)
    {
        if (_currentDirection == null) // We have not chosen a direction. No boundary has been touched / player came back and the laps were reset
        {
            _laps = 0;
            _currentDirection = touchedDirection; // set the current direction to where I am going. I already know I have direction, because _currentDirection is not null
            if (_verbose) print("0) Started.");
            return;
        }

        switch (_currentDirection.MatchesDirection(touchedDirection)) // Check if they are the same direction
        {
            // TRUE handles passing laps
            case true: // If they are the same direction, I want to check if the otehr boundary has been touched. If it has, and they are going in the correct direction, then a lap has been completed
                if (_passedOtherBoundary && _goingCorrectDirection)
                {
                    _laps++;
                    _passedOtherBoundary = false;
                    if (_verbose) print("1) True passed other and going correct, completed lap.");
                    return;
                }

                if (!_passedOtherBoundary && _goingCorrectDirection) // If I have NOT passed the other boundary but I am not going in the correct direction, then I started going wrong from outside of the correct biundary
                {
                    _laps--;
                    _goingCorrectDirection = false;
                    if (_laps < 0) // If the laps are <= then I reset the variables, it is not <= then it means I started to go wrong but there are still laps in the correct direction
                    {
                        ResetVariables();
                        if (_verbose) print("Going default");
                    }
                    if (_verbose) print("2) True did not pass other and going correct, so now going wrog OR going default.");
                    return;
                }

                if (_passedOtherBoundary && !_goingCorrectDirection)
                {
                    _laps--;
                    _passedOtherBoundary = false;
                    if(_laps < 0)
                    {
                        ResetVariables();
                        if (_verbose) print("I am actually resetting");
                    }
                    if (_verbose) print("3) True, have passed other boundary and not going in correct direction, so continue to go in wrong direction OR reset.");
                    return;
                }

                if (!_passedOtherBoundary && !_goingCorrectDirection)
                {
                    _laps++;
                    _goingCorrectDirection = true;
                    if (_verbose) print("4) True have not passed other boundary and was not goping in correct direction, now I am WUUUU, I also regained my lap lol.");
                    return;
                }
                break;
            case false:
                if (!_passedOtherBoundary && _goingCorrectDirection)
                {
                    _passedOtherBoundary = true; // I have passed a boundary!
                    if (_verbose) print("1) False, !passed other boundary, going correct.");
                    return;
                }

                if (_passedOtherBoundary && _goingCorrectDirection)
                {
                    _goingCorrectDirection = false; // I touched the other boundary while not having passed it, so started going wrong from the middle
                    if (_verbose) print("2) False, Passed other boundary, going correct, so now NOT going correct lol");
                    return;
                }

                if(_passedOtherBoundary && !_goingCorrectDirection)
                {
                    _passedOtherBoundary = true;
                    if (_verbose) print("3) False, Have passed other boudndary, NOT going correct direction. SO continue to fo in worng direction?");
                    return;
                }

                if(!_passedOtherBoundary && !_goingCorrectDirection){
                    _passedOtherBoundary = true;
                    if (_verbose) print("4) False, Did not have passed other boudndary, NOT going correct direction, but changed to now passed other boundary");
                    return;
                }
                break;
        }
        print("Something else, apparently");
    }
}
