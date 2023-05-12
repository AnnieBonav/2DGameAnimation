using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AztechButton : MonoBehaviour
{
    [SerializeField] private string _nextScene;
    [SerializeField] private Animator _crossfade;
    private void Awake()
    {
        if(_nextScene == "")
        {
            print("You did not provide a scene name. New name is Scene 404");
            _nextScene = "Scene404";
        }
    }
    public void ChangeScene()
    {
        StartCoroutine(HandleChangeScene());
    }

    private IEnumerator HandleChangeScene()
    {
        _crossfade.SetTrigger("Start");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(_nextScene);
        print("Trying to change scene to: " + _nextScene);
    }
}
