using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIHandling : MonoBehaviour
{
    private Button _startButton;
    private Button _options;
    private Button _credits;
    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _startButton = root.Q<Button>("start-game");
        _options = root.Q<Button>("options");
        _credits = root.Q<Button>("credits");

        _startButton.clicked += StartGame;
        _options.clicked += OpenOptions;
        _credits.clicked += OpenCredits;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void OpenOptions()
    {
        print("Opened options");
    }

    public void OnClick(InputValue value)
    {
        print("Clicked");
    }

    private void OpenCredits()
    {
        print("Opened credits");
    }
}
