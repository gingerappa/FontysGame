using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
