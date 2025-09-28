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

        if(this.GetType() == miniBeam.GetType())
        {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }
        else
        {
            Movement movement = GameManager.Instance.player.GetComponent<Movement>();

            float angle = Mathf.Atan2(-movement.direction.y, -movement.direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(angle - 10, angle + 10));
        }

        animaton.Play();
        lastUse = Time.time;
    }

    public override void Upgrade()
    {
        base.Upgrade();
        cooldown -= cooldownDecrease;
        miniBeamAmount += miniBeamIncrease;
    }

    public void CreateMiniBeam(int amount, Transform parent)
    {
        for (int i = 0; i < amount; i++)
        {
            miniBeam.SpawnMiniBeam(parent, level);
        }
    }
}
