using UnityEngine;
using UnityEditor;


public class ArrowScript : MonoBehaviour
{
    [SerializeField]
    private string _playerTag = "Player";
    void Start()
    {
        //ParticleSystem particleSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag(_playerTag))
            Debug.Log(other.tag);
    }
}
