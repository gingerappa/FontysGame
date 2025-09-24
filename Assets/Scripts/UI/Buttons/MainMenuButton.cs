using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    public void ShowLeaderboard()
    {
        MainMenuManager.Instance.cameraAnimator.SetBool("Rank", true);
        MainMenuManager.Instance.LoadLeaderboard(LeaderboardManager.Instance.leaderboard);
    }
    public void HideLeaderboard()
    {
        MainMenuManager.Instance.cameraAnimator.SetBool("Rank", false);
    }

    public void ShowPlayScreen()
    {
        MainMenuManager.Instance.cameraAnimator.SetBool("Play", true);
    }
    public void HidePlayScreen()
    {
        MainMenuManager.Instance.cameraAnimator.SetBool("Play", false);
    }

    public void LoadGameScene()
    {
        if(LeaderboardManager.Instance.playerName == "")
        {
            MainMenuManager.Instance.PlayNoNameAnimation();
            return;
        }
        SceneManager.LoadScene("Main");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
