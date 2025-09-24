using Unity.Mathematics;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public void Update()
    {
        Player player = GameManager.Instance.player;
        transform.localScale = new Vector3(math.lerp(0, 1, Mathf.InverseLerp(0, player.maxHealth, player.health)), 1, 1);
    }
}
