using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionHolder : MonoBehaviour
{

    
    public DragableItem currentActiveObject;

    public GameController gameref;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UseItem()
    {
        gameref.effectPlayer.Play();
        currentActiveObject.gameObject.GetComponent<ItemUtility>().UseItem();
    }
}
