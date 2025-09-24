using Unity.Mathematics;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]

    [field: SerializeField] public string weaponName { get; protected set; }

    public int2 damage;
    public int level { get; protected set; } = 0;
    [field: SerializeField] public int cost { get; protected set;}
    [field: SerializeField] public Sprite icon { get; protected set; }
    [SerializeField] protected int damageIncrease;
    [SerializeField] protected int costIncrease;


    protected virtual void Start()
    {
        
    }

    public virtual Weapon Spawn(Transform parent)
    {
        return Instantiate(this, parent);
    }

    public virtual void Upgrade()
    {
        level++;
        damage += damageIncrease;
        cost += costIncrease;
    }
    public virtual void Buy()
    {
        cost += costIncrease;
    }

    public virtual void Attack()
    {
    }
}
