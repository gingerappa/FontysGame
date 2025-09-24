using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public enum ShopType
{
    UPGRADE,
    BUY
}

public class ShopItem : MonoBehaviour
{
    public int2 iconSize;

    public TMP_Text nameText;
    public TMP_Text costText;
    public GameObject costIcon;
    public Image iconImage;
    public BuyButton button;
    public RawImage[] levelImages;

    [HideInInspector] public Weapon weapon;
    [HideInInspector] public ShopType type;
    private bool empty;

    public void LoadEmpty()
    {
        SetAllActive(false);
        empty = true;
    }

    private void SetAllActive(bool isActive)
    {
        if (!isActive)
        {
            nameText.text = string.Empty;
            costText.text = string.Empty;
        }
        iconImage.gameObject.SetActive(isActive);
        costIcon.gameObject.SetActive(isActive);
        button.gameObject.SetActive(isActive);
        foreach (RawImage image in levelImages)
        {
            image.gameObject.SetActive(isActive);
        }
    }

    private void OnEnable()
    {
        if (empty)
        {
            return;
        }
        else
        {
            SetAllActive(true);
        }

        nameText.text = weapon.weaponName;
        costText.text = weapon.cost.ToString();

        for (int i = 0; i < levelImages.Length; i++)
        {
            if(i <= weapon.level && type == ShopType.UPGRADE)
            {
                levelImages[i].color = Color.black;
                continue;
            }
            levelImages[i].color = Color.white;
        }

        Vector2 newIconSize = CalculateIconSize(new(weapon.icon.texture.width, weapon.icon.texture.height), iconSize);
        iconImage.rectTransform.sizeDelta = newIconSize;
        iconImage.sprite = weapon.icon;
    }

    public void Buy()
    {
        if (weapon.cost > GameManager.Instance.coins)
        {
            return;
        }

        GameManager.Instance.RemoveCoins(weapon.cost);
        if (GameManager.Instance.player.weapons.Contains(weapon))
        {
            GameManager.Instance.player.UpgradeWeapon(weapon);
        }
        else 
        { 
            GameManager.Instance.player.BuyWeapon(weapon);
        }
        SetAllActive(false);
    }
    
    private Vector2 CalculateIconSize(Vector2 iconSize, int2 originalSize)
    {
        if(iconSize.x <= originalSize.x && iconSize.y <= originalSize.y)
        {
            return new(originalSize.x, originalSize.y);
        }
        else if (iconSize.x > iconSize.y)
        {
            iconSize = new(originalSize.x, iconSize.y / (iconSize.x / originalSize.x));
        }
        else
        {
            iconSize = new(iconSize.x / (iconSize.y / originalSize.y), originalSize.y);
        }
        return iconSize;
    }
}
