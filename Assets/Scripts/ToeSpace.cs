using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToeSpace : MonoBehaviour, IDropHandler
{

    public bool empty;
    public int spaceNum;
    public string propertyNeeded;
    public LayoutGroup group;

    TicTacToeController boardref;
    public void OnDrop(PointerEventData eventData)
    {

    }

    private void Start()
    {
        boardref = GameObject.FindObjectOfType<TicTacToeController>();
        empty = true;
    }
    public void passSpaceNum()
    {
        empty = false;
        boardref.fillTicTacToeSpace(spaceNum);
    }


}
