using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBidItemsManager : MonoBehaviour
{
    [System.Serializable]
    public class BidItems
    {
        public string name;
        public string tag;
        public Sprite itemSprite;
        public int startingPrice;
    }
    public List<BidItems> TotalBidableList = new List<BidItems>();

    public BidItems currentBidItem;
    public GameObject itemPrefab;
    [SerializeField] private Canvas canvas;
    public int playerlastBid;

    public GameObject P1Hand;
    public GameObject P2Hand;

    public TMPro.TextMeshProUGUI sentence;

    public TMPro.TextMeshProUGUI nameofItem;
    public TMPro.TextMeshProUGUI priceofItem;
    public TMPro.TextMeshProUGUI tagofItem;
    public GameObject spriteofItem;
    public bool canRaise;

    public Timer timer;

    public InventoryMenu P1Inventory;
    public InventoryMenu P2Inventory;
    public GameObject P1InventoryPanel;
    public GameObject P2InventoryPanel;



    public GameController controller;
    public TMPro.TextMeshProUGUI priceTag;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        SpawnnewItem();
    }

    // Update is called once per frame
    void Update()
    {
        priceTag.text = "Current price: " + currentBidItem.startingPrice;
        if (canRaise == true)
        {

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (playerlastBid == 2)
                {

                    Raise(1);


                }
                else if (playerlastBid == 0)
                {
                    startBid(1);
                }

            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (playerlastBid == 1)
                {

                    Raise(2);


                }
                else if (playerlastBid == 0)
                {
                    startBid(2);
                }

            }
        }


    }
    public void SpawnnewItem()
    {
        playerlastBid = 0;

        BidItems chosenItem = TotalBidableList[Random.Range(0, TotalBidableList.Count)];
        currentBidItem.name = chosenItem.name;
        currentBidItem.tag = chosenItem.tag;
        currentBidItem.itemSprite = chosenItem.itemSprite;
        currentBidItem.startingPrice = chosenItem.startingPrice;

        nameofItem.text = "Name: " + currentBidItem.name;
        priceofItem.text = "Market Price: " + currentBidItem.startingPrice + " shillings";
        tagofItem.text = "Item Type: " + currentBidItem.tag;
        spriteofItem.GetComponent<SpriteRenderer>().sprite = currentBidItem.itemSprite;

        itemPrefab.GetComponent<DragableItem>().ItemSprite = currentBidItem.itemSprite;
        itemPrefab.GetComponent<DragableItem>().itemTag = currentBidItem.tag;
        itemPrefab.GetComponent<DragableItem>().marketPrice = currentBidItem.startingPrice;

        sentence.text = "Let's bring out our next item, a " + currentBidItem.name;
        StartCoroutine(startitemRoutine());
    }
    void startBid(int playernum)
    {
        sentence.text = "Player " + playernum + " has expressed interest in the object";
        playerlastBid = playernum;
        if (playernum == 1)
        {
            P1Hand.SetActive(true);
        }
        else if (playernum == 2)
        {
            P2Hand.SetActive(true);
        }
        timer.timerStarted = false;
        StartCoroutine(FirstbidRoutine());
    }

    void Raise(int playernum)
    {
        playerlastBid = playernum;
        if(playernum == 1)
        {
            P1Hand.SetActive(true);
            P2Hand.SetActive(false);
            canRaise = false;

        }
        else
        {
            P2Hand.SetActive(true);
            P1Hand.SetActive(false);
            canRaise = false;
        }
        currentBidItem.startingPrice += 10;
        sentence.text = "Player " + playernum + " has raised the price by 10 shillings";
        timer.timerStarted = false;
        StartCoroutine(bidRoutine());
    }
    IEnumerator startitemRoutine()
    {
        yield return new WaitForSeconds(2f);
        sentence.text = "Is anyone interested in this object?";
        timer.timerStarted = true;
        canRaise = true;
    }
    IEnumerator FirstbidRoutine()
    {
        yield return new WaitForSeconds(2f);
        int potentialPrice = currentBidItem.startingPrice + 10;
        sentence.text = "Do I hear a " + potentialPrice + " ?";
        canRaise = true;
        timer.resetTimer();
        timer.timerStarted = true;
       
    }
    IEnumerator bidRoutine()
    {
        yield return new WaitForSeconds(2f);
        int potentialPrice = currentBidItem.startingPrice + 10;
        sentence.text = "Do I hear a " + potentialPrice + " ?";
        canRaise = true;
        timer.resetTimer();
        timer.timerStarted = true;
    }

    public IEnumerator soldRoutine()
    {
        timer.timerStarted = false;
        timer.resetTimer();
        if(playerlastBid == 1)
        {
            if(P1Inventory.currentMoneyCount >= currentBidItem.startingPrice)
            {
                sentence.text = "Sold! The item now belongs to player " + playerlastBid;
                yield return new WaitForSeconds(2f);
                Sold();
            }
            else
            {
                sentence.text = "Uh oh! It looks like you don't have enough money!";
                yield return new WaitForSeconds(2f);
                //player 2 wins
            }
            
        }
        else if(playerlastBid == 2) 
        {
            if (P2Inventory.currentMoneyCount >= currentBidItem.startingPrice)
            {
                sentence.text = "Sold! The item now belongs to player " + playerlastBid;
                yield return new WaitForSeconds(2f);
                Sold();
            }
            else
            {
                sentence.text = "Uh oh! It looks like you don't have enough money!";
                yield return new WaitForSeconds(2f);
                //player 1 wins
            }
        }
        else
        {
            sentence.text = "It looks like no one is interested in this item.";
            yield return new WaitForSeconds(2f);
            SpawnnewItem();
        }



    }
    public void Sold()
    {
        DragableItem spawnedItem = Instantiate(itemPrefab, canvas.transform, false).GetComponent<DragableItem>();
        if (playerlastBid == 1)
        {
            P1Inventory.changeMoney(-currentBidItem.startingPrice);
            P2InventoryPanel.gameObject.SetActive(true);
            P1Hand.SetActive(false);
            P1Inventory.addItems(spawnedItem);
            controller.closeBidScreen(2);

        }
        else if(playerlastBid == 2)
        {
            P2Inventory.changeMoney(-currentBidItem.startingPrice);
            P1InventoryPanel.gameObject.SetActive(true);
            P2Hand.SetActive(false);
            P2Inventory.addItems(spawnedItem);
            controller.closeBidScreen(1);
        }

    }
}
