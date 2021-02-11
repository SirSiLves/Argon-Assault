using UnityEngine;
using UnityEngine.SceneManagement;


public class CollisionHandler : MonoBehaviour
{

    [Tooltip("In seconds")][SerializeField] float levelLoadDely = 1f;
    [Tooltip("FX prefab on player")][SerializeField] GameObject deathFX;


    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke("ReloadScene", levelLoadDely);
    }

    private void StartDeathSequence()
    {
        // Disable control
        SendMessage("OnPlayerDeath");

    }

    private void ReloadScene() // string reference
    {
        gameObject.SetActive(true);
        SceneManager.LoadScene(1);
    }
}
