using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Aura : Weapon
{
    [Space(10)]
    [Header("Aura Settings")]
    public float cooldown;
    public float increaceRange;


    private List<Enemy> enemysInAura;
    private float lastUse;
    private Vector3 size;


    protected override void Start()
    {
        base.Start();
        size = transform.localScale;
    }

    private void Awake()
    {
        enemysInAura = new();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemysInAura.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemysInAura.Remove(collision.GetComponent<Enemy>());
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        size = new(size.x + increaceRange, size.y + increaceRange, size.z + increaceRange);
        transform.localScale = size;
    }

    public override void Attack()
    {
        if (lastUse + cooldown > Time.time)
        {
            return;
        }

        List<Enemy> enemiesTakingDamage = enemysInAura.ToList();
        foreach (Enemy enemy in enemiesTakingDamage)
        {
            if (enemy == null)
            { 
                continue;
            }

            enemy.TakeDamage(Random.Range(damage.x, damage.y+1));
        }

        lastUse = Time.time;
    }
}
