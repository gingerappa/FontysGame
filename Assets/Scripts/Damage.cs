using Unity.Mathematics;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public Weapon Weapon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(UnityEngine.Random.Range(Weapon.damage.x, Weapon.damage.y+1));
        }
    }
}
