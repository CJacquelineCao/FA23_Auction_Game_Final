using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject bidScreen;
    public GameObject BoardScreen;

    public TicTacToeController tictac;
    public AllBidItemsManager allbids;

    public int playerWhoWon;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void closeBidScreen(int lostPlayer)
    {
        bidScreen.SetActive(false);
        BoardScreen.SetActive(true);
        tictac.endTurnButton.gameObject.SetActive(true);
        tictac.GameStart(lostPlayer);
    }
    public void startNewRound()
    {
        bidScreen.SetActive(true);
        BoardScreen.SetActive(false);
        allbids.SpawnnewItem();
    }

    public void loadWinScene(int wonPlayer)
    {
        SceneManager.LoadScene(2);
        playerWhoWon = wonPlayer;
    }
}
