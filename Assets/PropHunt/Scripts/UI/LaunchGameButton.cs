using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LaunchGameButton : MonoBehaviour
{
  
    // Start is called before the first frame update
    void Start()
    {
        bool isHost = NetworkManager.Singleton.IsHost;
        if (!isHost)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchGame()
    {
        NetworkManager.Singleton.SceneManager.LoadScene("Game", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
}
