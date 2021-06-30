using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]  public GameManager gameManager;

    [HideInInspector] public new string name;
    [HideInInspector] public int amount;
    public enum ItemTypes
    {
        Sell,
        Buy,
        Craft,
    }
    [HideInInspector] public ItemTypes itemType;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.UpdateShop += UpdateButton;
        gameManager.LateStart += LateStart;
    }

    void LateStart()
    {
        UpdateButton();
    }

    public void ClickButton(int itemAmount)
    {
        if (itemType == ItemTypes.Buy && gameManager.player.money >= amount)
        {
            gameManager.player.money -= amount;
            gameManager.itemManager.ChangeItemAmount(name, itemAmount);
        }
        else if (itemType == ItemTypes.Sell && gameManager.itemManager.CheckItemAmount(name) > 0)
        {
            gameManager.player.money -= amount;
            gameManager.itemManager.ChangeItemAmount(name, itemAmount);
            if(gameManager.crops.FindCrop(name) != null) gameManager.itemManager.ResetCropDecayTimer(gameManager.crops.FindCrop(name));
        }
        else if (itemType == ItemTypes.Craft)
        {
            if (CheckCraftAmount())
            {
                gameManager.itemManager.ChangeItemAmount(name, 1);
                RemoveIngrediants();
            }
        }
    }

    bool CheckCraftAmount()
    {
        bool hasItems = true;
        foreach (var ingrediant in gameManager.itemManager.GetItem(name).recipe) if (gameManager.itemManager.CheckItemAmount(ingrediant.item.name) < ingrediant.amount) hasItems = false;
        return hasItems;
    }

    void RemoveIngrediants()
    {
        foreach (var ingrediant in gameManager.itemManager.GetItem(name).recipe.ToArray())
        {
            gameManager.itemManager.ChangeItemAmount(ingrediant.item.name, -ingrediant.amount);
        }
    }

    void UpdateButton()
    {
        if (itemType == ItemTypes.Buy) if (gameManager.player.money >= amount) GetComponent<Button>().interactable = true;
            else if (itemType == ItemTypes.Buy) GetComponent<Button>().interactable = false;
        if (itemType == ItemTypes.Sell) if (gameManager.itemManager.CheckItemAmount(name) >= 1) GetComponent<Button>().interactable = true;
            else if (itemType == ItemTypes.Sell) GetComponent<Button>().interactable = false;
        if (itemType == ItemTypes.Craft) if (CheckCraftAmount()) GetComponent<Button>().interactable = true;
            else if (itemType == ItemTypes.Craft) GetComponent<Button>().interactable = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ScrollRect scrollRect = GetComponentInParent<ScrollRect>();
        gameManager.UIManager.ChangeRecipeDisplay(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ScrollRect scrollRect = GetComponentInParent<ScrollRect>();
        gameManager.UIManager.ChangeRecipeDisplay(this);
    }
}
