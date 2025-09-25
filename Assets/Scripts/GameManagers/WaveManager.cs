using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    public float enemySpawnDisctance;
    [Range(1f,10f)]
    public float spawnRange;

    public List<GameObject> enemiesInGame { get; private set; }
    private List<Enemy> enemyBacklog;
    private int amountOfEnemysInWave;
    private List<Enemy> nextWave;
    private bool computingWave;
    public int wave { get; private set; } = 1;
    public int scoreGoalTotal;

    public IEnumerator StartWave()
    {
        yield return new WaitWhile(() => computingWave);
        
        enemiesInGame = new();
        enemyBacklog = nextWave.ToList();
        amountOfEnemysInWave = enemyBacklog.Count;
        Debug.Log(amountOfEnemysInWave);
        StartCoroutine(Wave());

        wave += 1;
        scoreGoalTotal += UnityEngine.Random.Range(100, 200) * (int)math.ceil((float)wave/2);
        StartCoroutine(computeWave());
    }

    public IEnumerator Wave()
    {
        int enemyCountGoal = amountOfEnemysInWave / 2;
        while (enemiesInGame.Count > 0 || enemyBacklog.Count > 0)
        {
            if (enemyBacklog.Count == 0)
            {
                yield return new WaitUntil(() => enemiesInGame.Count == 0);
                break;
            }
            else if (enemiesInGame.Count >= enemyCountGoal)
            {
                yield return null;
                continue;
            }

            yield return new WaitForSeconds(UnityEngine.Random.Range(.2f, .3f));
            int randomIndex = UnityEngine.Random.Range(0, enemyBacklog.Count);
            enemiesInGame.Add(enemyBacklog[randomIndex].Spawn(enemySpawnDisctance, spawnRange));
            enemyBacklog.RemoveAt(randomIndex);
        }
        StartCoroutine(EndWave());
    }

    public IEnumerator EndWave()
    {
        yield return new WaitForSeconds(1);
        GameManager.Instance.EndWave();
    }

    public IEnumerator computeWave()
    {
        computingWave = true;
        int scoreGoal = scoreGoalTotal;
        List<Enemy> spawnableEnemys = GameManager.Instance.enemies.Where(e => e.GetScore() * e.GetMinimumAmout() <= scoreGoal / 3f).ToList();
        List<Enemy> removeEnemys = new();
        nextWave = new();
        while (scoreGoal > 0 && spawnableEnemys.Count > 0)
        {
            foreach (Enemy enemy in removeEnemys)
            {
                spawnableEnemys.Remove(enemy);
            }

            foreach (Enemy enemy in spawnableEnemys)
            {
                int enemyScore = enemy.GetScore();
                if (scoreGoal < enemyScore)
                {
                    removeEnemys.Add(enemy);
                    continue;
                }

                int spawnNumber = (int)math.ceil((float)scoreGoal / 3f / (float)enemyScore);
                int maximumSpawnAmount = enemy.GetMaximumAmout();
                if (spawnNumber >= maximumSpawnAmount && maximumSpawnAmount != 0)
                {
                    spawnNumber = maximumSpawnAmount;
                    removeEnemys.Add(enemy);
                }

                for (int i = 0; i < spawnNumber; i++)
                {
                    nextWave.Add(enemy);
                    scoreGoal -= enemyScore;
                    yield return null;
                }

                
            }
        }
        yield return null;
        computingWave = false;
    }

    private void Awake()
    {
        StartCoroutine(computeWave());

        enemiesInGame = new();
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