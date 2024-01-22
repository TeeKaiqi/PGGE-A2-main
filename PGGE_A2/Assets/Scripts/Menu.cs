using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    //public AudioClip buttonClickSound;

    public void OnClickSinglePlayer()
    {
        OnClickSound1();
        Invoke("SwitchSingleplayer", 0.5f);
        Debug.Log("Loading singleplayer game");
        //SceneManager.LoadScene("SinglePlayer");
    }

    public void OnClickMultiPlayer()
    {
        OnClickSound2();
        Invoke("SwitchMultiplayer", 0.5f);
        Debug.Log("Loading multiplayer game");
        //SceneManager.LoadScene("Multiplayer_Launcher");
    }

    public void OnClickSound1()
    {
        audioSource1.Play();
    }

    public void OnClickSound2()
    {
        audioSource2.Play();
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
