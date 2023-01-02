using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public System.Action<bool> MoveCompleted;
    public Player[] players;
    public Player currentPlayer;

    [SerializeField] private Board board;
    [SerializeField] private GameObject popup;
    [SerializeField] private Text popupText;
    [SerializeField] private Button popupButton;
    [SerializeField] private Text popupButtonText;

    private Context stateContext;
    private int totalPlayers = 2;

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

    private void OnDestroy()
    {
        stateContext = null;
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
        currentPlayer = players[index];
    }
}
