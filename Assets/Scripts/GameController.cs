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

    [SerializeField] private Board board;
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] private GameObject popup;
    [SerializeField] private Text popupText;
    [SerializeField] private Button popupButton;
    [SerializeField] private Text popupButtonText;
    [SerializeField] private GameObject multiPlayerObjectPrefab;

    public Context stateContext;
    private int maxPlayers = 2;

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
        players = new Player[maxPlayers];
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

    public void AddPlayers(PieceColor color, int index)
    {
        Player newPlayer = new Player(color, index);
        players[index] = newPlayer;
    }

    public void SetCurrentPlayer(int index)
    {
        currentPlayer = players[index];
    }

    public void AddMultiPlayerObject()
    {
        Photon.Pun.PhotonNetwork.Instantiate(multiPlayerObjectPrefab.name, Vector3.zero, Quaternion.identity);
    }
}
