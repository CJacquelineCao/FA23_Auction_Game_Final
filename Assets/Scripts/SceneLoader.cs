using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {

        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void goBacktoMenu()
    {
        GameObject gameref = GameObject.FindObjectOfType<GameController>().gameObject;
        Destroy(gameref);
        SceneManager.LoadScene(0);

    }
}
