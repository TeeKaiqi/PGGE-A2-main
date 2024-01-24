using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;


public class PlayerNameInput : MonoBehaviour
{
    private InputField mInputField; //private reference to the input field for the player nickname input
    private const string playerNamePrefKey = "PlayerName"; //Key for storing and retrieving the player's nickname that I refactored by making private

    // Start is called before the first frame update
    void Start()
    {
        mInputField = GetComponent<InputField>(); //Get the inputfield component attached to the game object

        if (mInputField != null) //checks if the inputfield isn't empty
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey)) //checks that the player name is already stored in playerNamePrefKey
            {
                mInputField.text = PlayerPrefs.GetString(playerNamePrefKey); //retrieve the name stored in playerNamePrefkey and assign that to mInputField.text, got rid of unecessary defaultName
                PhotonNetwork.NickName = mInputField.text; //set the nickname in the photonNetwork as the text in the inputfield without using defaultname
            }
        }
    }

    public void SetPlayerName() //method is called by the player name input field
    {
        string nameEntered = mInputField.text; //get the name entered in the input field and assign it to the renamed nameEntered
        if (string.IsNullOrEmpty(nameEntered)) //if the input field is empty or null
        {
            Debug.LogError("Player Name is null or empty"); //debug that the player name is null or empty
            return;
        }
        PhotonNetwork.NickName = nameEntered; //else set the Photonnetowerks nickname to the nameEntered
        PlayerPrefs.SetString(playerNamePrefKey, nameEntered); //store the player name in PlayerPrefs 

        Debug.Log("Nickname entered: " + nameEntered); //debug the name entered
    }
}
