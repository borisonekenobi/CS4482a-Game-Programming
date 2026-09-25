using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class LeaderboardEntry
{
    public string name;
    public float time;

    public LeaderboardEntry(string name, float time)
    {
        this.name = name;
        this.time = time;
    }
}

[Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> entries = new();
}

public class LeaderboardManager : MonoBehaviour
{
    private string _savePath;
    private LeaderboardData _leaderboardData = new();

    private void Awake()
    {
        _savePath = Path.Combine(Application.persistentDataPath, "leaderboard.json");
        LoadLeaderboard();
    }

    public void AddEntry(string playerName, float time)
    {
        _leaderboardData.entries.Add(new LeaderboardEntry(playerName, time));
        _leaderboardData.entries.Sort((a, b) => a.time.CompareTo(b.time));
        SaveLeaderboard();
    }

    public List<LeaderboardEntry> GetEntries()
    {
        return _leaderboardData.entries;
    }

    private void SaveLeaderboard()
    {
        var json = JsonUtility.ToJson(_leaderboardData, true);
        File.WriteAllText(_savePath, json);
    }

    private void LoadLeaderboard()
    {
        if (File.Exists(_savePath))
        {
            var json = File.ReadAllText(_savePath);
            _leaderboardData = JsonUtility.FromJson<LeaderboardData>(json);
        }
        else
        {
            _leaderboardData = new LeaderboardData();
        }
    }
}
