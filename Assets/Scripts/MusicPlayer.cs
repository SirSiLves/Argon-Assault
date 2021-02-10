using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    private void Awake()
    {
        // don't create a new music layer if one already exist
        int numMusicPlayer = FindObjectsOfType<MusicPlayer>().Length;
        if (numMusicPlayer > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

}
