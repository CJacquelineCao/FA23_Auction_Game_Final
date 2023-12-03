using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TSpaceGenerator : MonoBehaviour
{
    [System.Serializable]
    public class ToeSpaceDetails
    {
        public string propertyTag;

    }
    public List<ToeSpaceDetails> TagList = new List<ToeSpaceDetails>();
   public TicTacToeController boardref;
    public void initalizeGrid()
    {
        for (int i = 0; i < boardref.tictactoeSpaces.Count; i++)
        {    

            boardref.tictactoeSpaces[i].space.propertyNeeded = TagList[Random.Range(0, TagList.Count)].propertyTag;
        }
    }

    public void RerollTspace(ToeSpace space)
    {
        int T = Random.Range(0, TagList.Count);
        if(space.propertyNeeded == TagList[T].propertyTag)
        {
            RerollTspace(space);
        }
        else
        {
            space.propertyNeeded = TagList[T].propertyTag;
            boardref.restoreColors();
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
