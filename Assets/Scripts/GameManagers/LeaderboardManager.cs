using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance;

    public string playerName;
    public LeaderBoard leaderboard { get; private set; }

    private void Start()
    {
        leaderboard.Print();
    }

    public void Add(int score)
    {
        leaderboard.Add(new(playerName, score));
        Save();
    }

    public void Save()
    {
        string destination = Application.persistentDataPath + "/leaderboard.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, leaderboard);
        file.Close();
    }

    public void Load()
    {
        string destination = Application.persistentDataPath + "/leaderboard.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
            leaderboard = new(new());
            return;
        }

        BinaryFormatter bf = new BinaryFormatter();
        leaderboard = (LeaderBoard)bf.Deserialize(file);
        file.Close();
    }

    private void Awake()
    {
        Load();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class LeaderBoard
{
    private List<Score> leaderBoard;

    public LeaderBoard(List<Score> leaderBoard)
    {
        this.leaderBoard = leaderBoard;
    }

    public void Add(Score score)
    {
        for (int i = 0; i < leaderBoard.Count; i++)
        {
            if(score.amount >= leaderBoard[i].amount)
            {
                leaderBoard.Insert(i, score);
                return;
            }
        }
        leaderBoard.Add(score);
    }

    public int Count() { return leaderBoard.Count; }

    public Score Get(int index) { return leaderBoard[index]; }
    public void Remove(int index) { leaderBoard.RemoveAt(index); }

    public void Print()
    {
        string p = "";
        foreach (Score score in leaderBoard)
        {
            p += score.name + ": " + score.amount.ToString() + "\n";
        }
        Debug.Log(p);
    }
    
}

[System.Serializable]
public class Score
{
    public string name;
    public int amount;
    public Score(string name, int score)
    {
        this.name = name;
        this.amount = score;
    }
}