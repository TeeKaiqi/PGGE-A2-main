using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip buttonClickSound;

    public void OnClickSinglePlayer()
    {
        OnClickSound();
        Invoke("SwitchSingleplayer", 0.5f);
        Debug.Log("Loading singleplayer game");
        //SceneManager.LoadScene("SinglePlayer");
    }

    public void OnClickMultiPlayer()
    {
        OnClickSound();
        Invoke("SwitchMultiplayer", 0.5f);
        Debug.Log("Loading multiplayer game");
        //SceneManager.LoadScene("Multiplayer_Launcher");
    }

    public void OnClickSound()
    {
        audioSource.Play();
    }

    public void SwitchMultiplayer()
    {
        SceneManager.LoadScene("Multiplayer_Launcher");
    }

    public void SwitchSingleplayer()
    {
        SceneManager.LoadScene("SinglePlayer");
    }
        
}
