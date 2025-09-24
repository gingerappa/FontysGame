using TMPro;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    [SerializeField] private GameObject rankIcon;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text scoreText;

    public void Load(string name, int score)
    {
        nameText.text = name;
        scoreText.text = score.ToString();
        rankIcon.SetActive(true);
    }

    public void Load()
    {
        nameText.text = string.Empty;
        scoreText.text = string.Empty;
        rankIcon.SetActive(false);
    }
}
