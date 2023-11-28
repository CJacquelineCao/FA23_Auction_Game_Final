using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TicTacToeController : MonoBehaviour
{
    [System.Serializable]
    public class GridSpaces
    {
       public ToeSpace space;
       public TMPro.TextMeshProUGUI tagSpace;
       public int whichPlayerPossessesit;
    }


    public int whosTurn; //1 == x, 2 == o;

    public int turnCount;
    public Color[] playerColors;
    public Color DefaultCol;

    public List<GridSpaces> tictactoeSpaces = new List<GridSpaces>();

    public bool PlayerWon;
    public bool GameTied;

    public TSpaceGenerator tSpaceref;

    public GameObject P1Inventory;
    public GameObject P2Inventory;

    bool firstplayersGone;
    public Button endTurnButton;

    public GameController controller;
    public DisplayWinnerText disref;
    // Start is called before the first frame update
    private void Awake()
    {
        tSpaceref.initalizeGrid();
    }
    void Start()
    {


    }


    public void GameStart(int playerwhoLost)
    {
        firstplayersGone = false;
        whosTurn = playerwhoLost;
        turnCount = 0;

        for(int i =0; i < tictactoeSpaces.Count; i++)
        {
            if(tictactoeSpaces[i].space.empty == true)
            {
                tictactoeSpaces[i].space.transform.GetChild(0).gameObject.GetComponent<Image>().color = DefaultCol;
                tictactoeSpaces[i].tagSpace = tictactoeSpaces[i].space.transform.GetChild(1).gameObject.GetComponent<TMPro.TextMeshProUGUI>();
                tictactoeSpaces[i].tagSpace.text = tictactoeSpaces[i].space.propertyNeeded;
            }
            tictactoeSpaces[i].space.GetComponent<ToeSpace>().spaceNum = i;
        }
    }

    public void fillTicTacToeSpace(int whichSpace)
    {
        tictactoeSpaces[whichSpace].space.transform.GetChild(0).gameObject.GetComponent<Image>().color = playerColors[whosTurn-1];
        tictactoeSpaces[whichSpace].whichPlayerPossessesit = whosTurn;

        if(IsGameWonBy(whosTurn))
        {
            PlayerWon = true;
        }
        else
        {

        }
    }
    public void endTurn()
    {
        if(firstplayersGone == false)
        {
            if (whosTurn == 1)
            {
                whosTurn = 2;
                P1Inventory.SetActive(false);
                P2Inventory.SetActive(true);
                firstplayersGone = true;
            }
            else
            {
                whosTurn = 1;
                P2Inventory.SetActive(false);
                P1Inventory.SetActive(true);
                firstplayersGone = true;
            }
        }
        else
        {
            endTurnButton.gameObject.SetActive(false);
            P2Inventory.SetActive(false);
            P1Inventory.SetActive(false);
            controller.startNewRound();

        }

    }

    public bool IsGameWonBy(int side)
    {
        if (tictactoeSpaces[0].whichPlayerPossessesit == side && tictactoeSpaces[1].whichPlayerPossessesit == side && tictactoeSpaces[2].whichPlayerPossessesit == side)
        {
            return true;
        }
        else if (tictactoeSpaces[3].whichPlayerPossessesit == side && tictactoeSpaces[4].whichPlayerPossessesit == side && tictactoeSpaces[5].whichPlayerPossessesit == side)
        {
            return true;
        }
        else if (tictactoeSpaces[6].whichPlayerPossessesit == side && tictactoeSpaces[7].whichPlayerPossessesit == side && tictactoeSpaces[8].whichPlayerPossessesit == side)
        {
            return true;
        }
        else if (tictactoeSpaces[0].whichPlayerPossessesit == side && tictactoeSpaces[4].whichPlayerPossessesit == side && tictactoeSpaces[8].whichPlayerPossessesit == side)
        {
            return true;
        }
        else if (tictactoeSpaces[2].whichPlayerPossessesit == side && tictactoeSpaces[4].whichPlayerPossessesit == side && tictactoeSpaces[6].whichPlayerPossessesit == side)
        {
            return true;
        }
        else if (tictactoeSpaces[0].whichPlayerPossessesit == side && tictactoeSpaces[3].whichPlayerPossessesit == side && tictactoeSpaces[6].whichPlayerPossessesit == side)
        {
            return true;
        }
        else if (tictactoeSpaces[1].whichPlayerPossessesit == side && tictactoeSpaces[4].whichPlayerPossessesit == side && tictactoeSpaces[7].whichPlayerPossessesit == side)
        {
            return true;
        }
        else if (tictactoeSpaces[2].whichPlayerPossessesit == side && tictactoeSpaces[5].whichPlayerPossessesit == side && tictactoeSpaces[8].whichPlayerPossessesit == side)
        {
            return true;
        }
        else
        {
            GameTied = true;
            for (int i =0; i<tictactoeSpaces.Count; i++)
            {
               if( tictactoeSpaces[i].whichPlayerPossessesit == 1 || tictactoeSpaces[i].whichPlayerPossessesit == 2)
                {
                    
                }
               else
                {
                    GameTied = false;
                    break;
                }

            }
            if(GameTied == true)
            {
                //load tied Scene;
            }
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        if(PlayerWon == true)
        {
            controller.loadWinScene(whosTurn);
        }
    }
}
