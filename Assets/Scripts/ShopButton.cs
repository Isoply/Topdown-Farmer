using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]  public GameManager gameManager;

    [HideInInspector] public bool deactivateOnClick = false;
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
            StartCoroutine(StartParticle(0.15f, new Color32(255, 25, 0, 150)));
            if (deactivateOnClick) DeactivateButton();
        }
        else if (itemType == ItemTypes.Sell && gameManager.itemManager.CheckItemAmount(name) > 0)
        {
            gameManager.player.money -= amount;
            gameManager.itemManager.ChangeItemAmount(name, itemAmount);
            if(gameManager.crops.FindCrop(name) != null) gameManager.itemManager.ResetCropDecayTimer(gameManager.crops.FindCrop(name));
            StartCoroutine(StartParticle(0.15f, new Color32(255, 200, 0, 150)));
            if (deactivateOnClick) DeactivateButton();
        }
        else if (itemType == ItemTypes.Craft)
        {
            if (CheckCraftAmount())
            {
                gameManager.itemManager.ChangeItemAmount(name, 1);
                RemoveIngrediants(); 
                if (deactivateOnClick) DeactivateButton();
            }
        }
    }

    void DeactivateButton()
    {
        transform.Find("Item").GetComponent<Image>().color = GetComponent<Button>().colors.disabledColor;
        GetComponent<Button>().enabled = false;
    }

    IEnumerator StartParticle(float _seconds, Color32 _color)
    {
        gameManager.UIManager.clickParticles.startColor = _color;
        gameManager.UIManager.clickParticles.transform.position = GameObject.FindObjectOfType<Canvas>().transform.position + Camera.main.ScreenToWorldPoint(Input.mousePosition);
        gameManager.UIManager.clickParticles.maxParticles = 1000;
        yield return new WaitForSeconds(_seconds);
        gameManager.UIManager.clickParticles.maxParticles = 0;
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
            if (gameManager.crops.FindCrop(ingrediant.item.name) != null) gameManager.itemManager.ResetCropDecayTimer(gameManager.crops.FindCrop(ingrediant.item.name));
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
        gameManager.UIManager.ChangeRecipeDisplay(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameManager.UIManager.ChangeRecipeDisplay(this);
    }
}
