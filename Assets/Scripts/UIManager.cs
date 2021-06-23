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
    [HideInInspector] public List<Item> craftItems = new List<Item>();
    [HideInInspector] public List<Inventory> invSlots = new List<Inventory>();

    GameObject recipePreview;
    int maxPreviewHeight;

    int priceDifferent = 5;

    private void Start()
    {
        GameObject HUD = GameObject.Find("HUD");
        invSlots.Add(new Inventory(HUD.transform.Find("Shop").Find("Header").Find("Inventory").gameObject));
        invSlots.Add(new Inventory(HUD.transform.Find("Player").Find("Inventory").gameObject));
        invSlots.Add(new Inventory(HUD.transform.Find("Crafting").Find("Inventory").gameObject));
        recipePreview = HUD.transform.Find("Crafting").Find("Recipe").gameObject;
        maxPreviewHeight = (int)recipePreview.GetComponent<RectTransform>().sizeDelta.y;
        recipePreview.SetActive(false);

        gameManager = GameObject.FindObjectOfType<GameManager>();
        SetShop(GameObject.Find("Buying").transform.GetChild(0).GetChild(0).gameObject, gameManager.itemManager.ToItems(gameManager.itemManager.allSlots.ToArray()), priceDifferent);
        SetShop(GameObject.Find("Selling").transform.GetChild(0).GetChild(0).gameObject, gameManager.itemManager.ToItems(gameManager.itemManager.allSlots.ToArray()), -priceDifferent);
        SetShop(GameObject.Find("Crafting").transform.Find("Items").gameObject, gameManager.itemManager.allRecipes.ToArray(), 0);

        gameManager.LateStart += LateStart;
    }

    private void LateStart()
    {
        invSlots[0].moneyText = invSlots[0].gameObject.transform.parent.Find("Money").GetComponentInChildren<Text>();
        invSlots[0].allSlots = CreateInventory(invSlots[0].gameObject, gameManager.itemManager.allSlots.ToArray());
        invSlots[1].allSlots = CreateInventory(invSlots[1].gameObject, gameManager.itemManager.allSlots.ToArray());
        invSlots[2].allSlots = CreateInventory(invSlots[2].gameObject, gameManager.itemManager.allSlots.ToArray());
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

    public void ChangeRecipeDisplay(ShopButton thisButton)
    {
        int size = recipePreview.transform.childCount - 1;
        for (int i = 0; i < size; i++)
        {
            Destroy(recipePreview.transform.GetChild(size - i).gameObject);
        }

        recipePreview.SetActive(!recipePreview.activeSelf);
        GameObject template = recipePreview.transform.GetChild(0).gameObject;
        template.SetActive(true);
        Item curItem = gameManager.itemManager.GetItem(thisButton.name);
        size = 0;
        if (curItem.recipe.Count >= 1) size += UpdateCraftTemplate(Instantiate(template, recipePreview.transform), curItem.recipe[0]);
        if (curItem.recipe.Count >= 2) size += UpdateCraftTemplate(Instantiate(template, recipePreview.transform), curItem.recipe[1]);
        if (curItem.recipe.Count >= 3) size += UpdateCraftTemplate(Instantiate(template, recipePreview.transform), curItem.recipe[2]);
        recipePreview.GetComponent<RectTransform>().sizeDelta = new Vector2(recipePreview.GetComponent<RectTransform>().sizeDelta.x, size);
        template.SetActive(false);
    }

    int UpdateCraftTemplate(GameObject _template, Ingrediant ingrediant)
    {
        _template.GetComponentInChildren<Image>().sprite = ingrediant.item.icon;
        if (gameManager.itemManager.CheckItemAmount(ingrediant.item.name) >= ingrediant.amount) _template.GetComponentInChildren<Image>().color = new Color32(255, 255, 255, 255);
        else _template.GetComponentInChildren<Image>().color = new Color32(255, 150, 150, 255);
        _template.GetComponentInChildren<Text>().text = $"{gameManager.itemManager.CheckItemAmount(ingrediant.item.name)}/{ingrediant.amount}";
        return maxPreviewHeight / 3;
    }

    private void Update()
    {
        if (recipePreview.activeSelf && Input.mousePosition.x >= Screen.currentResolution.width / 2) recipePreview.transform.position = new Vector3(Input.mousePosition.x - recipePreview.GetComponent<RectTransform>().sizeDelta.x / 2, Input.mousePosition.y, Input.mousePosition.z);
        else if (recipePreview.activeSelf) recipePreview.transform.position = new Vector3(Input.mousePosition.x + recipePreview.GetComponent<RectTransform>().sizeDelta.x / 2, Input.mousePosition.y, Input.mousePosition.z);
    }

    public void SetShop(GameObject parent, Item[] items, int modifier)
    {
        GameObject prefab = parent.transform.GetChild(0).gameObject;

        int size = 0;


        Item[] usedArray = null;
        bool skips;
        if (modifier > 0) usedArray = buyItems.ToArray();
        else if (modifier < 0) usedArray = sellItems.ToArray();
        else if (modifier == 0) usedArray = craftItems.ToArray();
        foreach (var item in items)
        {
            skips = false;
            foreach (var curItem in usedArray) if (curItem.name == item.name) skips = true;
            if (skips) continue;
            GameObject newShopItem = Instantiate(prefab, parent.transform);
            newShopItem.transform.GetChild(0).GetComponent<Image>().sprite = item.icon;
            if (newShopItem.transform.childCount > 1)
            {
                newShopItem.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = item.name;
                newShopItem.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = ((int)(item.price + ((item.price / 100f) * modifier))).ToString() + "$";
                if (modifier >= 0) newShopItem.transform.GetChild(1).GetChild(1).GetComponent<Text>().color = Color.red;
                else if (modifier < 0) newShopItem.transform.GetChild(1).GetChild(1).GetComponent<Text>().color = Color.green;
            }
            newShopItem.SetActive(true);

            ShopButton newButton = newShopItem.GetComponent<ShopButton>();
            if (newButton != null)
            {
                newButton.gameManager = gameManager;
                newButton.name = item.name;
                newButton.amount = modifier * (int)(item.price + ((item.price / 100f) * modifier));
                if (modifier > 0) newButton.itemType = ShopButton.ItemTypes.Buy;
                else if (modifier < 0) newButton.itemType = ShopButton.ItemTypes.Sell;
                else if (modifier == 0) newButton.itemType = ShopButton.ItemTypes.Craft;
            }

            if (modifier > 0) buyItems.Add(item);
            else if (modifier < 0) sellItems.Add(item);
            else if (modifier == 0) craftItems.Add(item);
            size += 165;
        }
        if (parent.GetComponent<HorizontalLayoutGroup>()) parent.GetComponent<RectTransform>().sizeDelta = new Vector2(size, parent.GetComponent<RectTransform>().sizeDelta.y);
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
            newSlot.icon.sprite = gameManager.itemManager.GetItem(item.name).icon;
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
                if (curInv.moneyText != null) curInv.moneyText.text = $"{gameManager.player.money} $";
            }
        }
        gameManager.UpdateShop();
    }

    void UpdateSlot(InventorySlot slot, Slot item)
    {
        slot.text.text = item.amount.ToString();
        slot.icon.sprite = gameManager.itemManager.GetItem(item.name).icon;
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

