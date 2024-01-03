using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int ScoreSumm;
}
public class CloudData : MonoBehaviour
{
    public static CloudData Instance;

    public PlayerInfo PlayerInfo;

    [SerializeField] TextMeshProUGUI _playerInfoText;

    [DllImport("__Internal")]
    private static extern void SaveExtern(string date);

    [DllImport("__Internal")]
    private static extern void LoadExtern();

    [DllImport("__Internal")]
    private static extern void SetToLeaderboard(int value);

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Save()
    {
        string jsonString = JsonUtility.ToJson(PlayerInfo);
        SaveExtern(jsonString);
    }
    public void IncreaseScoreSumm(int gems)
    {
        PlayerInfo.ScoreSumm += gems;
        SetToLeaderboard(PlayerInfo.ScoreSumm);
        Save();
    }

    public void SetPlayerInfo(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        _playerInfoText.text = "scoresumm" + PlayerInfo.ScoreSumm;
    }

    public void LoadExternButton()
    {
        LoadExtern();
    }
}
