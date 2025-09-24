using UnityEngine;

public interface IWeapon
{
    public string name { get; set; }
    public int level { get; set; }
    public int cost { get; set; }
    public void Attack();
    public void Upgrade();
}

