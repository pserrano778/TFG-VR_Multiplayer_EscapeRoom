using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager2 : MonoBehaviourPunCallbacks
{
    public Button searchButton;
    public Button cancelButton;
    public Button returnToPreviousMenuButton;
    public Button exitButton;

    public GameObject searchingText;

    private static int player;
    private bool owner = false;

    private void Start()
    {
        ChangeMainButtonsState(true);
        ChangeCancelButtonState(false);
    }

    public enum QueueState
    {
        Queuing,
        Queued,
        Starting,
        Cancelled,
    }

    public void SearchPlayer()
    {
        // Al iniciar la búsqueda, se desactivan los botones
        ChangeMainButtonsState(false);
        ChangeCancelButtonState(false);

        // Conexión con el servidor
        ConnectedToServer();
    }

    void ConnectedToServer()
    {
        // Se cambia el texto
        UpdateQueueText(QueueState.Queuing);

        // Conexión a Photon Network
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        // Se intenta unir a una sala
        JoinRandomRoom();
    }

    private void JoinRandomRoom()
    {
        // Se intenta encontrar una sala
        PhotonNetwork.JoinRandomRoom(null, 0, MatchmakingMode.FillRoom, TypedLobby.Default, null);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // Si no se encuentra una sala, se crea
        CreateRoom();
    }

    private void CreateRoom()
    {
        owner = true;

        RoomOptions roomOptions = new RoomOptions();

        // Opciones de configuración básicas
        roomOptions.MaxPlayers = 2;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        // Se crea la sala
        PhotonNetwork.CreateRoom(null, roomOptions, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
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

        // Se hay dos jugadores, se carga el nivel
        //if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        //{
            // Se actualiza el texto
            UpdateQueueText(QueueState.Starting);

            // Se desactiva el botón que cancela la búsqueda
            ChangeCancelButtonState(false);

            // Se carga la escena
            LoadLevel("Multijugador");
        //}
        /*else
        {
            // Se actualiza el texto
            UpdateQueueText(QueueState.Queued);

            // Se activa el botón que permite cancelar la búsqueda
            ChangeCancelButtonState(true);
        }*/
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        // Si hay dos jugadores, se oculta la sala y se inicia el juego
        if (PhotonNetwork.CurrentRoom.PlayerCount == 2)
        {
            // Se cambia el texto
            UpdateQueueText(QueueState.Starting);

            // Se desactiva el botón que cance la la búsqueda
            ChangeCancelButtonState(false);

            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;

            // Load scene
            LoadLevel("Multijugador");
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        base.OnDisconnected(cause);
    }

    public void LoadLevel(string levelName)
    {
        // Carga el juego multijugador
        PhotonNetwork.LoadLevel(levelName);
    }

    private void UpdateQueueText(QueueState queueState)
    {
        // Si el jugador está en elgún punto de la búsqueda de otro jugador, actualiza el texto
        if (queueState == QueueState.Queued)
        {
            searchingText.GetComponent<Text>().text = "Buscando a otro jugador";
            searchingText.SetActive(true);
        }
        else if (queueState == QueueState.Queuing)
        {
            searchingText.GetComponent<Text>().text = "Iniciando búsqueda";
            searchingText.SetActive(true);
        }
        else if (queueState == QueueState.Starting)
        {
            searchingText.GetComponent<Text>().text = "Iniciando la partida";
            searchingText.SetActive(true);
        }
        else // En otro caso, desactiva el texto
        {
            searchingText.GetComponent<Text>().text = "";
            searchingText.SetActive(false);
        }
    }

    public void CancelSearch()
    {
        // Desconexta al jugador del servidor
        PhotonNetwork.Disconnect();

        // Actualiza el texto
        UpdateQueueText(QueueState.Cancelled);


        // Desactiva el botón que cancela la búsqueda
        ChangeCancelButtonState(false);

        // Activa los botones principales
        ChangeMainButtonsState(true);
    }

    public void ChangeMainButtonsState(bool state)
    {
        searchButton.gameObject.SetActive(state);
        returnToPreviousMenuButton.gameObject.SetActive(state);
        exitButton.gameObject.SetActive(state);
    }

    public void ChangeCancelButtonState(bool state)
    {
        cancelButton.gameObject.SetActive(state);
    }

    static public int getPlayer()
    {
        return player;
    }
}
