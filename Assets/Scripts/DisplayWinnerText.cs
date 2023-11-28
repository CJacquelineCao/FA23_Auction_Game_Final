using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayWinnerText : MonoBehaviour
{
    GameController controller;
    public TMPro.TextMeshProUGUI winText;
    public int playerwhoWon;
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.FindFirstObjectByType<GameController>();
        playerwhoWon = controller.playerWhoWon;
        callWinner();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void callWinner()
    {

        winText = GameObject.FindGameObjectWithTag("WinText").GetComponent<TMPro.TextMeshProUGUI>();
        winText.text = "Player " + playerwhoWon + " Wins!";
        
    }
}
