using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public DamageNumber damageNumber;
    public UpdateText updateScore;
    public UpdateText updateCoin;
    public WaveUpdate waveUpdate;

    public GameObject deathScreen;
    public Player player;
    public Coin coin;
    public Weapon[] weapons;
    public List<Enemy> enemies;
    [field: SerializeField] public List<InventoryWeapon> inventoryWeaponsObjects { get; private set; }
    public Dictionary<Weapon, InventoryWeapon> inventoryWeapons { get; private set; }

    public float roundScoreBonus;
    public float roundScoreBonusMultiplier;

    public int score { get; private set; }
    public int coins { get; private set; }

    public List<GameObject> coinGameObjects { get; private set; }

    private void Start()
    {
        if(LeaderboardManager.Instance.playerName == "")
        {
            LeaderboardManager.Instance.playerName = "John Doe";
        }
        StartWave();
    }

    public void CreateInventoryWeapon(Weapon weapon)
    {
        inventoryWeaponsObjects[0].weapon = weapon;
        inventoryWeaponsObjects[0].Load();
        inventoryWeapons.Add(weapon, inventoryWeaponsObjects[0]);
        inventoryWeaponsObjects.RemoveAt(0);
    }

    public void EndWave()
    {
        Debug.Log("End Wave");
        AddScore((int)roundScoreBonus);
        roundScoreBonus *= roundScoreBonusMultiplier;

        for (int i = 0; i < coinGameObjects.Count; i++)
        {
            Destroy(coinGameObjects[i]);
        }

        ShopManager.Instance.LoadShop();
    }

    public void StartWave()
    {
        waveUpdate.updateWave(WaveManager.Instance.wave);
        StartCoroutine(WaveManager.Instance.StartWave());
    }

    public void AddScore(int points)
    {
        score += points;
        updateScore.ChangeText("Score: " + score.ToString());
    }

    public void AddCoin()
    {
        coins++;
        AddScore(10);
        updateCoin.ChangeText(coins.ToString());
    }

    public void RemoveCoins(int amount)
    {
        coins -= amount;
        updateCoin.ChangeText(coins.ToString());
    }

    public void EndGame()
    {
        deathScreen.SetActive(true);
        player.gameObject.SetActive(false);
        player.transform.DetachChildren();
        LeaderboardManager.Instance.Add(score);
    }
    
    private void Awake()
    {
        enemies = enemies.OrderByDescending((Enemy i) => i.GetScore()).ToList();
        inventoryWeapons = new();
        coinGameObjects = new();
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