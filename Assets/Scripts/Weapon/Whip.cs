using System;
using Unity.Mathematics;
using UnityEngine;


public class Whip : Weapon
{
    [Space(10)]
    [Header("Whip Settings")]

    public float cooldown;
    [SerializeField] protected Animation animaton;
    private float lastUse;
    [SerializeField] protected float cooldownDecrease;

    /* 
    Ik weet niet hoe ik makkelijke lijkere reference geef naar de whip zonder of meerdere scripts voor damage te gebruiken of
    de hitboxes anders te doen wat in dit geval niet werkt 
    */

    public WhipDamage[] hitBoxes;

    protected override void Start()
    {
        base.Start();
        foreach (WhipDamage damageScript in hitBoxes)
        {
            damageScript.Weapon = this;
        }
    }

    public override void Attack()
    {
        if (lastUse + cooldown + animaton.clip.length > Time.time)
        {
            return;
        }
        animaton.Play();
        lastUse = Time.time;
    }

    public override void Upgrade()
    {
        base.Upgrade();
        cooldown -= cooldownDecrease;
    }
}