using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
//using Photon.Realtime;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public Action<bool> MoveCompleted;
    public Action OnPlayerConnected;
    public Player[] players;
    public Player currentPlayer;
    public List<Photon.Realtime.Player> players1 = new List<Photon.Realtime.Player>();
    public Photon.Realtime.Player currentPlayer1;

    [SerializeField] private Board board;
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject popup;
    [SerializeField] private Text popupText;
    [SerializeField] private Button popupButton;
    [SerializeField] private Text popupButtonText;
    [SerializeField] private GameObject multiPlayerObjectPrefab;

    public Context stateContext;
    private int totalPlayers = 2;

    public Board Board
    {
        get { return board; }
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
            Instance = null;
        }
        Instance = this;
    }

    private void Start()
    {
        stateContext = new Context();
        stateContext.SetState(new Intro());
        board.MoveCompleted += OnMoveCompleted;
    }

    private void OnDestroy()
    {
        stateContext = null;
        board.MoveCompleted -= OnMoveCompleted;
    }

    private void OnMoveCompleted(Piece capturedPiece)
    {
        bool isGameOver = false;
        if(capturedPiece != null)
        {
            try
            {
                King king = (King)capturedPiece;
                if(king != null)
                {
                    isGameOver = true;
                }
            }
            catch
            {

            }
        }
        MoveCompleted?.Invoke(isGameOver);
    }

    public void ShowPopup(string _textContent, string _buttonContent,  bool showButton,UnityEngine.Events.UnityAction OnButtonClicked)
    {
        popup.SetActive(true);
        popupText.text = _textContent;
        popup.SetActive(showButton);
        if(showButton)
        {
            popupButtonText.text = _buttonContent;
            popupButton.onClick.AddListener(OnButtonClicked);
        }
    }

    public void ConnectToNetwork()
    {
        networkManager.Connect();
    }

    public void HidePopup()
    {
        popup.SetActive(false);
        popupButton.onClick.RemoveAllListeners();
    }

    public void SetupBoard()
    {
        board.SpawnChessBoard();
        board.SpawnPieces();
    }

    public void AddPlayers()
    {
        players = new Player[totalPlayers];
        for (int i = 0; i < totalPlayers; i++)
        {
            players[i] = new Player();
            players[i].selectedColor = (PieceColor)i;
            players[i].index = i;
        }
    }

    public void SetCurrentPlayer(int index)
    {
        //currentPlayer = players[index];
        currentPlayer1 = players1[index];
    }

    public void UpdatePlayersList(Dictionary<int, Photon.Realtime.Player> _players)
    {
        players1.Clear();
        Debug.Log("_players " + _players.Count);
        foreach (KeyValuePair<int, Photon.Realtime.Player> _player in _players)
        {
            players1.Add(_player.Value);
        }
    }

    public void AddMultiPlayerObject()
    {
        Photon.Pun.PhotonNetwork.Instantiate(multiPlayerObjectPrefab.name, Vector3.zero, Quaternion.identity);
    }
}
