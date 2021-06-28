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
    GameObject displayedRecipies;
    int maxPreviewHeight;

    int priceDifferent = 5;

    private void Start()
    {
        GameObject HUD = GameObject.Find("HUD");
        invSlots.Add(new Inventory(HUD.transform.Find("Shop").Find("Header").Find("Inventory").gameObject));
        invSlots.Add(new Inventory(HUD.transform.Find("Player").Find("Inventory").gameObject));
        invSlots.Add(new Inventory(HUD.transform.Find("Crafting").Find("Inventory").gameObject));
        recipePreview = HUD.transform.Find("Crafting").Find("Recipe").gameObject;
        recipePreview.SetActive(false);
        displayedRecipies = HUD.transform.Find("Shop").Find("Creatables").gameObject;
        displayedRecipies.SetActive(false);
        maxPreviewHeight = (int)recipePreview.GetComponent<RectTransform>().sizeDelta.y;

        gameManager = GameObject.FindObjectOfType<GameManager>();
        if (GameObject.Find("Buying") != null) SetShop(GameObject.Find("Buying").transform.GetChild(0).GetChild(0).gameObject, gameManager.itemManager.ToItems(gameManager.itemManager.allSlots.ToArray()), priceDifferent);
        if(GameObject.Find("Selling") != null) SetShop(GameObject.Find("Selling").transform.GetChild(0).GetChild(0).gameObject, gameManager.itemManager.ToItems(gameManager.itemManager.allSlots.ToArray()), -priceDifferent);
        if (GameObject.Find("Crafting") != null) SetShop(GameObject.Find("Crafting").transform.Find("Items").gameObject, gameManager.itemManager.allRecipes.ToArray(), 0);

        gameManager.LateStart += LateStart;
    }

    private void LateStart()
    {
        invSlots[0].moneyText = invSlots[0].gameObject.transform.parent.Find("Money").GetComponentInChildren<Text>();
        invSlots[0].allSlots = CreateInventory(invSlots[0].gameObject, gameManager.itemManager.allSlots.ToArray());
        invSlots[1].allSlots = CreateInventory(invSlots[1].gameObject, gameManager.itemManager.allCrops.ToArray());
        invSlots[2].allSlots = CreateInventory(invSlots[2].gameObject, gameManager.itemManager.allSlots.ToArray());
        UpdateInventory();
        gameManager.crops.ToItems();

        foreach (var crop in gameManager.crops.allCrops)
        {
            foreach (var inventory in gameManager.UIManager.invSlots[1].allSlots)
            {
                if (inventory.name == crop.item.name) inventory.timer.text = (crop.decayTime - crop.timer).ToString("0.00");
            }
        }

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
        if (thisButton.itemType == ShopButton.ItemTypes.Craft) recipePreview.SetActive(!recipePreview.activeSelf);
        if (thisButton.itemType == ShopButton.ItemTypes.Buy) displayedRecipies.SetActive(!displayedRecipies.activeSelf);
        UpdateRecipeDisplay(thisButton);
    }

    public void UpdateRecipeDisplay(ShopButton thisButton)
    {
        GameObject parent = null;
        if (thisButton.itemType == ShopButton.ItemTypes.Craft) parent = recipePreview;
        if (thisButton.itemType == ShopButton.ItemTypes.Buy) parent = displayedRecipies;

        if (parent != null)
        {
            int size = parent.transform.childCount - 1;
            for (int i = 0; i < size; i++)
            {
                Destroy(parent.transform.GetChild(size - i).gameObject);
            }

            GameObject template = parent.transform.GetChild(0).gameObject;

            if (thisButton.itemType == ShopButton.ItemTypes.Craft)
            {
                Item curItem = gameManager.itemManager.GetItem(thisButton.name);
                size = 0;
                template.SetActive(true);
                if (curItem.recipe.Count >= 1) size += UpdateCraftTemplate(Instantiate(template, parent.transform), curItem.recipe[0]);
                if (curItem.recipe.Count >= 2) size += UpdateCraftTemplate(Instantiate(template, parent.transform), curItem.recipe[1]);
                if (curItem.recipe.Count >= 3) size += UpdateCraftTemplate(Instantiate(template, parent.transform), curItem.recipe[2]);
                parent.GetComponent<RectTransform>().sizeDelta = new Vector2(parent.GetComponent<RectTransform>().sizeDelta.x, size);
                template.SetActive(false);
            }
            else if (thisButton.itemType == ShopButton.ItemTypes.Buy)
            {
                List<Item> newItems = new List<Item>();
                foreach (var recipe in gameManager.itemManager.allRecipes)
                {
                    foreach (var item in recipe.recipe)
                    {
                        if (item.item.name == thisButton.name) newItems.Add(recipe);
                    }
                }
                size = 0;
                foreach (var item in newItems)
                {
                    template.SetActive(true);
                    GameObject newItem = Instantiate(template, parent.transform);
                    template.SetActive(false);
                    newItem.GetComponent<Image>().sprite = item.icon;
                    size += (int)parent.GetComponent<RectTransform>().sizeDelta.y;
                    bool canCraft = true;
                    foreach (var curItem in item.recipe) if (gameManager.itemManager.CheckItemAmount(curItem.item.name) < curItem.amount) canCraft = false;
                    if (!canCraft) newItem.GetComponent<Image>().color = new Color32(255, 150, 150, 255);
                    else newItem.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }
                parent.GetComponent<RectTransform>().sizeDelta = new Vector2(size, parent.GetComponent<RectTransform>().sizeDelta.y);
            }
        }
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
        if (displayedRecipies.activeSelf && Input.mousePosition.y + 100 >= Screen.currentResolution.height / 2) displayedRecipies.transform.position = new Vector3(Input.mousePosition.x, (Input.mousePosition.y) - displayedRecipies.GetComponent<RectTransform>().sizeDelta.y / 2, Input.mousePosition.z);
        else if (displayedRecipies.activeSelf) displayedRecipies.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + displayedRecipies.GetComponent<RectTransform>().sizeDelta.y / 2, Input.mousePosition.z);
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
            newSlot.gameObject.name = item.item.name;
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
    public string name;
    public GameObject parent;
    public GameObject gameObject;
    public Text text;
    public Image icon;
    public Text timer;

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
            name = gameObject.name;
            text = gameObject.transform.Find("Text").GetComponent<Text>();
            if (gameObject.transform.Find("Timer")) timer = gameObject.transform.Find("Timer").GetComponent<Text>();
            if (gameObject.transform.GetComponentsInChildren<Image>().Length > 0) icon = gameObject.transform.GetComponentsInChildren<Image>()[1];
            else icon = gameObject.transform.GetComponentInChildren<Image>();
            return true;
        }
        else return false;
    }
}

