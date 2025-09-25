using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Coin : MonoBehaviour
{
    public float disapearTime;
    [SerializeField] private Animation disapearAnimation;
    [SerializeField] private float2 spawnForce;

    private void Awake()
    {
        Invoke("StartDisapear", disapearTime);
    }

    public void StartDisapear()
    {
        disapearAnimation.Play();
    }

    public void Disapear()
    {
        GameManager.Instance.coinGameObjects.Remove(gameObject);
        Destroy(gameObject);
    }

    public void spawn(Vector2 origin)
    {
        GameObject coin = Instantiate(gameObject, origin, quaternion.identity);
        Vector2 force = new(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        force = force.normalized;
        coin.GetComponent<Rigidbody2D>().AddForce(force * Random.Range(spawnForce.x,spawnForce.y));
        GameManager.Instance.coinGameObjects.Add(coin);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddCoin();
            Destroy(gameObject);
        }
    }
}
