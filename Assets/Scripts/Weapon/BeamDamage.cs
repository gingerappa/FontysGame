using UnityEngine;

public class BeamDamage : MonoBehaviour
{
    public bool isMiniBeam;
    public Beam Beam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(Random.Range(Beam.damage.x, Beam.damage.y + 1));
            if (!isMiniBeam)
            {
                Beam.CreateMiniBeam(Beam.miniBeamAmount, collision.transform);
            }
        }
    }
}
