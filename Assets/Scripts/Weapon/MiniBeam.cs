using UnityEngine;

public class MiniBeam : Beam
{
    public void SpawnMiniBeam(Vector3 position, int level)
    {
        this.damage += damageIncrease * level;
        Instantiate(gameObject, position, Quaternion.Euler(new(0, 0, Random.Range(0f, 360f))));
    }

    private void Awake()
    {
        Attack();
        Destroy(gameObject,animaton.clip.length+.5f);
    }
}
