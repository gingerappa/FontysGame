using UnityEngine;
using UnityEngine.SceneManagement;

public class Reload : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Main");
    }
}
