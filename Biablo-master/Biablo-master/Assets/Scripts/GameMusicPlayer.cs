using UnityEngine;

public class GameMusicPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.instance.PlayGameMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
