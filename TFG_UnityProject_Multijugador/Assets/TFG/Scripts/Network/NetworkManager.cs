using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class NetworkManager : MonoBehaviourPunCallbacks
{
    private static int player;
    private bool owner = false;

    // Start is called before the first frame update
    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Try Connect to Server...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connect To Server.");

        base.OnConnectedToMaster();

        // Se intenta unir a una sala aleatoria
        JoinRandomRoom();
    }

    private void JoinRandomRoom()
    {
        // Se intenta encontrar una sala
        PhotonNetwork.JoinRandomRoom(null, 0, MatchmakingMode.FillRoom, TypedLobby.Default, null);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // Si no se ha encontrado un sala, se crea una nueva
        Debug.Log("Creating a Room");
        CreateRoom();
    }

    private void CreateRoom()
    {
        owner = true;

        RoomOptions roomOptions = new RoomOptions();

        // Opciones básicas
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        // Se crea la sala
        PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("joined a Room");

        // Se establece el número del jugador
        if (owner)
        {
            player = 0;
        }
        else
        {
            player = 1;
        }

        base.OnJoinedRoom();
        /*
        // If there is 2 players, load the game
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            LoadLevel("Test");
        }
        */
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("A new player has joined.");
        base.OnPlayerEnteredRoom(newPlayer);
        
        // If there is 2 players, load the game and hide the room
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            //LoadLevel("Test");
        }   
    }
    /*
    public void LoadLevel(string levelName)
    {
        // Load the game
        SceneManager.LoadScene(levelName);
    }
    */

    static public int getPlayer()
    {
        return player;
    }
}
