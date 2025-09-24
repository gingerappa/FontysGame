using TMPro;
using UnityEngine;

public class UpdateText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    public void ChangeText(string newText)
    {
        text.text = newText;
    }
}
