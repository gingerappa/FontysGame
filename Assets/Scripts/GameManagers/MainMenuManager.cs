using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class MainMenuManager : MonoBehaviour
{
    public Animator cameraAnimator;
    public Animation noNameAnimaton;

    public RankItem[] rankItems;

    private void Start()
    {
        LoadLeaderboard(LeaderboardManager.Instance.leaderboard);
    }

    public void LoadLeaderboard(LeaderBoard leaderBoard)
    {
        int count = leaderBoard.Count();
        for (int i = 0; i < rankItems.Length; i++)
        {
            if(i >= count)
            {
                rankItems[i].Load();
                continue;
            }
            Score score = leaderBoard.Get(i);
            rankItems[i].Load(score.name, score.amount);
        }
    }

    public void PlayNoNameAnimation()
    {
        noNameAnimaton.Play();
    }


    public static MainMenuManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}