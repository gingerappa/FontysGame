using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    public TMP_InputField inputField;
    public void NameChange()
    {
        LeaderboardManager.Instance.playerName = inputField.text;
    }
}
