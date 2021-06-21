using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    GameManager gameManager;

    bool isPlaying;
    Animator animator;
    [HideInInspector] public List<Item> buyItems = new List<Item>();
    [HideInInspector] public List<Item> sellItems = new List<Item>();
    [HideInInspector] public List<Inventory> invSlots = new List<Inventory>();

    private void Start()
    {
        GameObject HUD = GameObject.Find("HUD");
        gameManager = GameObject.FindObjectOfType<GameManager>();
        SetShop(GameObject.Find("Buying"), gameManager.crops.ToItems(), 5);
        SetShop(GameObject.Find("Selling"), gameManager.crops.ToItems(), -5);

        invSlots.Add(new Inventory(HUD.transform.Find("Shop").Find("Header").Find("Inventory").gameObject));
        gameManager.LateStart += LateStart;
    }

    private void LateStart()
    {
        invSlots[0].moneyText = invSlots[0].gameObject.transform.parent.Find("Money").GetComponentInChildren<Text>();
        invSlots[0].allSlots = CreateInventory(gameManager.UIManager.invSlots[0].gameObject, gameManager.itemManager.allSlots.ToArray());
        UpdateInventory();
        gameManager.crops.ToItems();
    }

    public void StartAnimationBool(Animator newAnimator)
    {
        animator = newAnimator;

        isPlaying = !isPlaying;
        animator.SetBool("isAnimating", true);
        animator.SetBool("isPlaying", isPlaying);

        if (animator.name == "Shop") gameManager.ChangePauseState(GameManager.PauseStates.Change);
    }

    public void SetShop(GameObject parent, Item[] items, int modifier)
    {
        GameObject prefab = parent.transform.GetChild(0).GetChild(0).GetChild(0).gameObject;
        Transform newParent = parent.transform.GetChild(0).GetChild(0);

        int size = 0;

        Item[] usedArray = null;
        bool skips;
        if (modifier >= 0) usedArray = buyItems.ToArray();
        else if (modifier < 0) usedArray = sellItems.ToArray();
        foreach (var item in items)
        {
            skips = false;
            foreach (var curItem in usedArray) if (curItem.name == item.name) skips = true;
            if (skips) continue;
            GameObject newShopItem = Instantiate(prefab, newParent);
            newShopItem.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
            newShopItem.GetComponentInChildren<HorizontalLayoutGroup>().transform.GetChild(0).GetComponent<Text>().text = item.name;
            newShopItem.GetComponentInChildren<HorizontalLayoutGroup>().transform.GetChild(1).GetComponent<Text>().text = ((int)(item.price + ((item.price / 100f) * modifier))).ToString() + "$";
            if (modifier >= 0) newShopItem.GetComponentInChildren<HorizontalLayoutGroup>().transform.GetChild(1).GetComponent<Text>().color = Color.red;
            else if (modifier < 0) newShopItem.GetComponentInChildren<HorizontalLayoutGroup>().transform.GetChild(1).GetComponent<Text>().color = Color.green;
            newShopItem.SetActive(true);

            ShopButton newButton = newShopItem.GetComponent<ShopButton>();
            if (newButton != null)
            {
                newButton.gameManager = gameManager;
                newButton.name = item.name;
                newButton.amount = modifier * (int)(item.price + ((item.price / 100f) * modifier));
            }

            if (modifier >= 0) buyItems.Add(item);
            else if (modifier < 0) sellItems.Add(item);
            size += 120;
        }

        newParent.GetComponent<RectTransform>().sizeDelta = new Vector2(size, newParent.GetComponent<RectTransform>().sizeDelta.y);
    }

    public InventorySlot[] CreateInventory(GameObject parent, Slot[] items)
    {
        GameObject prefab = parent.transform.GetChild(0).gameObject;
        List<InventorySlot> slots = new List<InventorySlot>();
        foreach (var item in items)
        {
            InventorySlot newSlot = new InventorySlot(Instantiate(prefab, parent.transform));
            newSlot.gameObject.SetActive(true);
            newSlot.GetVariables();
            UpdateSlot(newSlot, item);
            newSlot.text.text = item.amount.ToString();
            newSlot.icon.sprite = gameManager.crops.FindCrop(item.name).item.icon;
            newSlot.gameObject.name = item.name;
            slots.Add(newSlot);
        }
        return slots.ToArray();
    }

    public void UpdateInventory()
    {
        foreach (var curInv in invSlots)
        {
            foreach (var curSlot in curInv.allSlots)
            {
                UpdateSlot(curSlot, gameManager.itemManager.FindSlot(curSlot.gameObject.name));
                curInv.moneyText.text = $"{gameManager.player.money} $";
            }
        }
    }

    void UpdateSlot(InventorySlot slot, Slot item)
    {
        slot.text.text = item.amount.ToString();
        slot.icon.sprite = gameManager.crops.FindCrop(item.name).item.icon;
        slot.gameObject.name = item.name;
    }
}

public class Inventory
{
    public GameObject gameObject;
    public InventorySlot[] allSlots;
    public Text moneyText;

    public Inventory(GameObject _gameObject)
    {
        gameObject = _gameObject;
    }
}

public class InventorySlot
{
    public GameObject parent;
    public GameObject gameObject;
    public Text text;
    public Image icon;

    public InventorySlot(GameObject _gameObject)
    {
        gameObject = _gameObject;
    }

    public InventorySlot(GameObject _gameObject, Text _text, Image _icon)
    {
        gameObject = _gameObject;
        text = _text;
        icon = _icon;
    }

    public bool GetVariables()
    {
        if (gameObject != null)
        {
            text = gameObject.transform.GetComponentInChildren<Text>();
            if (gameObject.transform.GetComponentsInChildren<Image>().Length > 0) icon = gameObject.transform.GetComponentsInChildren<Image>()[1];
            else icon = gameObject.transform.GetComponentInChildren<Image>();
            return true;
        }
        else return false;
    }
}

