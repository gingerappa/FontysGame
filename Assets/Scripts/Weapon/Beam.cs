using UnityEngine;

public class Beam : Weapon
{
    [Space(10)]
    [Header("Beam Settings")]

    public int miniBeamAmount;
    [SerializeField] protected int miniBeamIncrease;

    public float cooldown;
    [SerializeField] protected float cooldownDecrease;
    protected float lastUse;

    [SerializeField] protected Animation animaton;
    [SerializeField] private MiniBeam miniBeam;

    public override void Attack()
    {
        if (lastUse + cooldown + animaton.clip.length > Time.time)
        {
            return;
        }

        transform.rotation = Quaternion.Euler(new(0, 0, Random.Range(0f, 360f)));
        animaton.Play();

        lastUse = Time.time;
    }

    public override void Upgrade()
    {
        base.Upgrade();
        cooldown -= cooldownDecrease;
        miniBeamAmount += miniBeamIncrease;
    }

    public void MiniBeam(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            miniBeam.SpawnMiniBeam(transform.position, level);
        }
    }
}
