using UnityEngine;

public class CollisionHandler : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
    }

    private void StartDeathSequence()
    {
        // Disable control
        SendMessage("OnPlayerDeath");
    }
}
