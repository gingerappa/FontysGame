using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeapon : MonoBehaviour
{
    public int2 iconSize;
    public RawImage[] levelImages;
    public GameObject levelsContainer;
    public Image iconImage;


    [HideInInspector] public Weapon weapon;

    public void Load()
    {
        levelsContainer.SetActive(true);
        UpdateLevels();

        Vector2 newIconSize = CalculateIconSize(new(weapon.icon.texture.width, weapon.icon.texture.height), iconSize);
        iconImage.rectTransform.sizeDelta = newIconSize;
        iconImage.sprite = weapon.icon;
        iconImage.gameObject.SetActive(true);

    }

    public void UpdateLevels()
    {
        for (int i = 0; i < levelImages.Length; i++)
        {
            if (i < weapon.level)
            {
                levelImages[i].color = Color.black;
                continue;
            }
            levelImages[i].color = Color.white;
        }
    }

    private Vector2 CalculateIconSize(Vector2 iconSize, int2 originalSize)
    {
        if (iconSize.x <= originalSize.x && iconSize.y <= originalSize.y)
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
