using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WizardManager : MonoBehaviourPunCallbacks
{
    public string mPlayerPrefabName; //public variable to store name of the player prefab
    public PlayerSpawnPoints mSpawnPoints; //public variable that stores the info about the spawnpoints

    [HideInInspector]
    public GameObject mPlayerGameObject; //game object that stores the instantiated player charcater
    [HideInInspector]
    private ThirdPersonCamera mThirdPersonCamera;

    // Start is called before the first frame update
    private void Start()
    {
        Transform randomSpawnTransform = mSpawnPoints.GetSpawnPoint(); //get a random spaw point for the player  by calling the getspawnpoint function from the pawnpoint script
        mPlayerGameObject = PhotonNetwork.Instantiate(mPlayerPrefabName,randomSpawnTransform.position,randomSpawnTransform.rotation, 0); //instantiate the player prefab

        mThirdPersonCamera = Camera.main.gameObject.AddComponent<ThirdPersonCamera>(); //add thirdpersoncamera script to main camera

        mPlayerGameObject.GetComponent<WizardMovement>().mFollowCameraForward = false; //makes it so that the players movement doesnt follow the cameras forward direction by setting it false
        mThirdPersonCamera.mPlayer = mPlayerGameObject.transform; 
        mThirdPersonCamera.mDamping = 20.0f;
        mThirdPersonCamera.mCameraType = CameraType.Follow_Track_Pos_Rot;
    }


    public void LeaveRoom()
    {
        Debug.LogFormat("LeaveRoom");
        PhotonNetwork.LeaveRoom(); //call photon network function to leave the room
    }

    public override void OnLeftRoom()
    {
        Debug.LogFormat("OnLeftRoom()"); 
        SceneManager.LoadScene("Menu"); //load the menu scene once the player has left the room
    }

}
