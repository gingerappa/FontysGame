using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ShopManager : MonoBehaviour
{
    public bool shopActive { get; private set; }
    public GameObject shop;
    public ShopItem[] shopItems;
    List<Weapon> shopWeapons;

    [SerializeField] private int upgradeChance;

    public void LoadShop()
    {
        shopActive = true;
        List<Weapon> availableUpgrades = GameManager.Instance.player.weapons.ToList();
        List<Weapon> availableWeapons = shopWeapons.ToList();
        availableUpgrades = availableUpgrades.Where(x => x.level < 5).ToList();

        for (int i = 0; i < shopItems.Length; i++)
        {
            if ((availableUpgrades.Count == 0 && availableWeapons.Count == 0) || (availableUpgrades.Count == 0 && GameManager.Instance.player.weapons.Count == Player.MaxWeapons))
            {
                shopItems[i].LoadEmpty();
            }
            else if ((availableWeapons.Count == 0 || GameManager.Instance.player.weapons.Count == Player.MaxWeapons || Random.Range(0, upgradeChance) == 0) && availableUpgrades.Count != 0)
            {
                shopItems[i].weapon = availableUpgrades[Random.Range(0, availableUpgrades.Count)];
                shopItems[i].type = ShopType.UPGRADE;
                availableUpgrades.Remove(shopItems[i].weapon);
            }
            else
            {
                shopItems[i].weapon = availableWeapons[Random.Range(0, availableWeapons.Count)];
                shopItems[i].type = ShopType.BUY;
                availableWeapons.Remove(shopItems[i].weapon);
            }
        }
        shop.SetActive(true);
    }

    public void UnLoadShop()
    {
        shopActive = false;
        shop.SetActive(false);
        GameManager.Instance.StartWave();
    }

    public void RemoveWeaponRotation(Weapon weapon)
    {
        shopWeapons.Remove(weapon);
    }

    public static ShopManager Instance;
    private void Awake()
    {
        shopWeapons = GameManager.Instance.weapons.ToList();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}