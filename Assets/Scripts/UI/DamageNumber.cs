using TMPro;
using UnityEngine;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    public void Create(int damage, Vector2 location)
    {
        text.text = damage.ToString();
        text.color = new(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        GameObject newNumber = Instantiate(gameObject, location, Quaternion.identity);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
