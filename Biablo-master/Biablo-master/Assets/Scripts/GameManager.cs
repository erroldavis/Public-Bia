using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);



        DontDestroyOnLoad(gameObject);
    }
}
