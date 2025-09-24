using UnityEngine;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected Rigidbody2D rb;

    [SerializeField] protected int health;
    [SerializeField] protected int scoreValue;
    [Range(2.5f,5f)]
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected int coinsSpawning;
    [SerializeField] protected int minmumSpawnAmount;
    [SerializeField] protected int maximumSpawnAmount;


    protected bool isDamaging;

    protected float lastPosCheck;
    protected float lastPosCheckCooldown = 1f;

    protected virtual void FixedUpdate()
    {
        if(lastPosCheck + lastPosCheckCooldown < Time.time)
        {
            if (Vector2.Distance(transform.position, GameManager.Instance.player.transform.position) >= WaveManager.Instance.enemySpawnDisctance + WaveManager.Instance.spawnRange + 2  )
            {
                ReSpawn(WaveManager.Instance.enemySpawnDisctance, WaveManager.Instance.spawnRange);
            }
            lastPosCheck = Time.time;
        }
        MoveTowards(GameManager.Instance.player.transform.position, speed);
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage * Time.deltaTime);
        }
    }


    public virtual void TakeDamage(int damage)
    {

        health -= damage;
        if (health <= 0)
        {
            kill();
        }
        GameManager.Instance.damageNumber.Create(damage, transform.position);
    }

    public virtual void MoveTowards(Vector2 location, float speed)
    {
        float step = speed * Time.deltaTime;
        rb.MovePosition(Vector2.MoveTowards(transform.position, location, step));
    }

    public virtual void kill()
    {
        for (int i = 0; i < coinsSpawning; i++)
        {
            GameManager.Instance.coin.spawn(transform.position);
        }
        WaveManager.Instance.enemiesInGame.Remove(gameObject);
        GameManager.Instance.AddScore(scoreValue);
        GameManager.Destroy(gameObject);
    }

    public virtual GameObject Spawn(Vector2 location)
    {
        GameObject newGameObject = GameManager.Instantiate(prefab, location , Quaternion.identity);
        return newGameObject;
    }

    public virtual GameObject Spawn(float enemySpawnDisctance, float spawnRange)
    {
        Vector2 spawnPos = GetRandomSpawn(enemySpawnDisctance, spawnRange);

        GameObject newGameObject = GameManager.Instantiate(prefab, spawnPos, Quaternion.identity);
        return newGameObject;
    }

    public virtual void ReSpawn(float enemySpawnDisctance, float spawnRange)
    {
        transform.position = GetRandomSpawn(enemySpawnDisctance, spawnRange);
    }

    private Vector2 GetRandomSpawn(float enemySpawnDisctance, float spawnRange)
    {
        Vector2 origin = GameManager.Instance.player.transform.position;

        float xPos;
        float yPos;

        switch (Random.Range(0, 4))
        {
            case 0:
                xPos = Random.Range(-enemySpawnDisctance, -enemySpawnDisctance - spawnRange);
                yPos = Random.Range(-spawnRange, spawnRange);
                break;
            case 1:
                xPos = Random.Range(enemySpawnDisctance, enemySpawnDisctance + spawnRange);
                yPos = Random.Range(-spawnRange, spawnRange);
                break;
            case 2:
                xPos = Random.Range(-spawnRange * 1.5f, spawnRange * 1.5f);
                yPos = Random.Range(-enemySpawnDisctance, -enemySpawnDisctance - spawnRange);
                break;
            case 3:
                xPos = Random.Range(-spawnRange * 1.5f, spawnRange * 1.5f);
                yPos = Random.Range(enemySpawnDisctance, enemySpawnDisctance + spawnRange);
                break;
            default:
                xPos= 0; yPos = 0;
                break;
        }
        Vector2 spawnPos = new Vector2(xPos, yPos) + origin;

        return spawnPos;
    }


    public int GetScore() { return scoreValue; }
    public int GetMinimumAmout() { return minmumSpawnAmount; }
    public int GetMaximumAmout() { return maximumSpawnAmount; }
    public float GetDamage() { return damage; }
}
