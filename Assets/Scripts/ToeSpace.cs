using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class ToeSpace : MonoBehaviour, IDropHandler, IPointerDownHandler
{

    public bool empty;
    public int spaceNum;
    public string propertyNeeded;
    public bool canbeChanged;
    public LayoutGroup group;

    TicTacToeController boardref;
    TSpaceGenerator TspaceRef;
    public TMPro.TextMeshProUGUI tagText;
    public void OnDrop(PointerEventData eventData)
    {

    }

    private void Start()
    {
        boardref = GameObject.FindObjectOfType<TicTacToeController>();
        TspaceRef = GameObject.FindObjectOfType<TSpaceGenerator>();
        empty = true;
    }
    public void passSpaceNum()
    {
        empty = false;
        boardref.fillTicTacToeSpace(spaceNum);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(canbeChanged == true)
        {
            TspaceRef.RerollTspace(this.gameObject.GetComponent<ToeSpace>());
            canbeChanged = false;
        }

    }
    private void Update()
    {
        tagText.text = propertyNeeded;
    }

}
