using UnityEngine;

public class MiniBeam : Beam
{
    public void SpawnMiniBeam(Transform parent, int level)
    {
        GameObject miniBeam = Instantiate(gameObject, parent);
    }

    private void Awake()
    {
        this.damage += damageIncrease * level;
        Attack();
        Destroy(gameObject,animaton.clip.length+.5f);
    }
}
