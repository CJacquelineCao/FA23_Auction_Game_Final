using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HowToPlayStuff : MonoBehaviour
{
    public GameObject PageOne;
    public GameObject PageOneButton;
    public GameObject PageTwo;
    public GameObject PageTwoButton;
    public GameObject StartGameButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PageTwo.activeSelf == true)
        {
            StartGameButton.SetActive(true);
        }
         else
        {
            StartGameButton.SetActive(false);
        }
    }
    public void OpenPage()
    {
        if(PageOne.activeSelf == true)
        {
            PageOne.SetActive(false);
            PageTwo.SetActive(true);
            PageTwoButton.SetActive(true);
            PageOneButton.SetActive(false);
        }
        else if(PageTwo.activeSelf == true)
        {
            PageOne.SetActive(true);
            PageTwo.SetActive(false);
            PageTwoButton.SetActive(false);
            PageOneButton.SetActive(true);
        }

    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
