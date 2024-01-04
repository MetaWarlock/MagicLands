using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public int ScoreSumm;

    public string CurrentScene;

    public string Scene;

    public int SceneStatus;

    public int Gems;

    public float Time;

    public string Scene02;

    public int SceneStatus02;

    public int Gems02;

    public float Time02;

    public string Scene03;

    public int SceneStatus03;

    public int Gems03;

    public float Time03;

    public string Scene04;

    public int SceneStatus04;

    public int Gems04;

    public float Time04;

    public PlayerInfo()
    {
        // Установите значение Scene в конструкторе
        Scene = "01 Swamp Ruins";
        Scene02 = "02 Swamp Forest";
        Scene03 = "03 Swamp End";
        Scene04 = "04 Swamp Boss";
    }

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

        PlayerInfo.SceneStatus = PlayerPrefs.GetInt(PlayerInfo.Scene + "_unlocked");
        PlayerInfo.SceneStatus02 = PlayerPrefs.GetInt(PlayerInfo.Scene02 + "_unlocked");
        PlayerInfo.SceneStatus03 = PlayerPrefs.GetInt(PlayerInfo.Scene03 + "_unlocked");
        PlayerInfo.SceneStatus04 = PlayerPrefs.GetInt(PlayerInfo.Scene04 + "_unlocked");

        PlayerInfo.Time = PlayerPrefs.GetFloat(PlayerInfo.Scene + "_time");
        PlayerInfo.Time02 = PlayerPrefs.GetFloat(PlayerInfo.Scene02 + "_time");
        PlayerInfo.Time03 = PlayerPrefs.GetFloat(PlayerInfo.Scene03 + "_time");
        PlayerInfo.Time04 = PlayerPrefs.GetFloat(PlayerInfo.Scene04 + "_time");

        PlayerInfo.Gems = PlayerPrefs.GetInt(PlayerInfo.Scene + "_gems");
        PlayerInfo.Gems02 = PlayerPrefs.GetInt(PlayerInfo.Scene02 + "_gems");
        PlayerInfo.Gems03 = PlayerPrefs.GetInt(PlayerInfo.Scene03 + "_gems");
        PlayerInfo.Gems04 = PlayerPrefs.GetInt(PlayerInfo.Scene04 + "_gems");

        SetToLeaderboard(PlayerInfo.ScoreSumm);
        Save();
    }

    public void SetPlayerInfo(string value)
    {
        PlayerInfo = JsonUtility.FromJson<PlayerInfo>(value);
        _playerInfoText.text = "scoresumm" + PlayerInfo.ScoreSumm;
        PlayerPrefs.SetInt(PlayerInfo.Scene + "_gems", PlayerInfo.Gems);
        PlayerPrefs.SetInt(PlayerInfo.Scene02 + "_gems", PlayerInfo.Gems02);
        PlayerPrefs.SetInt(PlayerInfo.Scene03 + "_gems", PlayerInfo.Gems03);
        PlayerPrefs.SetInt(PlayerInfo.Scene04 + "_gems", PlayerInfo.Gems04);

        PlayerPrefs.SetFloat(PlayerInfo.Scene + "_time", PlayerInfo.Time);
        PlayerPrefs.SetFloat(PlayerInfo.Scene02 + "_time", PlayerInfo.Time02);
        PlayerPrefs.SetFloat(PlayerInfo.Scene03 + "_time", PlayerInfo.Time03);
        PlayerPrefs.SetFloat(PlayerInfo.Scene04 + "_time", PlayerInfo.Time04);

        PlayerPrefs.SetInt(PlayerInfo.Scene + "_unlocked", PlayerInfo.SceneStatus);
        PlayerPrefs.SetInt(PlayerInfo.Scene02 + "_unlocked", PlayerInfo.SceneStatus02);
        PlayerPrefs.SetInt(PlayerInfo.Scene03 + "_unlocked", PlayerInfo.SceneStatus03);
        PlayerPrefs.SetInt(PlayerInfo.Scene04 + "_unlocked", PlayerInfo.SceneStatus04);
    }

    public void LoadExternButton()
    {
        LoadExtern();
    }
}
