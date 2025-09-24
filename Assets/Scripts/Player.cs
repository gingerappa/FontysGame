using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public const int MaxWeapons = 5;

    public Whip whip;
    public float maxHealth;
    public float health { get; private set; }
    public int maxWeapons { get; private set; } = 5;
    public List<Weapon> weapons { get; private set; } = new();

    private bool dead;

    private void Awake()
    {
        health = maxHealth;
        BuyWeapon(GameManager.Instance.weapons[0]);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Weapon weapon in weapons)
        {
            if(weapon != null) 
            { 
                weapon.Attack();
            }
        }
    }

    public void BuyWeapon(Weapon weapon)
    {
        if(weapons.Count >= MaxWeapons)
        {
            Debug.LogWarning("Cant have more than " + maxWeapons.ToString() + " weapons");
            return;
        }
        ShopManager.Instance.RemoveWeaponRotation(weapon);
        weapon = weapon.Spawn(transform);
        weapon.Buy();
        GameManager.Instance.CreateInventoryWeapon(weapon);
        weapons.Add(weapon);

    }

    public void UpgradeWeapon(Weapon weapon)
    {
        weapons[weapons.IndexOf(weapon)].Upgrade();
        GameManager.Instance.inventoryWeapons[weapon].UpdateLevels();
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0 && !dead)
        {
            Kill();
        }
    }

    private void Kill()
    {
        dead = true;
        GameManager.Instance.EndGame();
    }
}
