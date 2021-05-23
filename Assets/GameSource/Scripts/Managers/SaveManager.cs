using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SaveManager : Singleton<SaveManager>
{
    public override void Awake()
    {
        base.Awake();
    }
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("FirstStart"))
        {
            PlayerPrefs.SetInt("FirstStart", 1);
            PlayerPrefs.SetInt("RandomLevelIndex", -1);
            PlayerPrefs.SetInt("LevelIndex", 0);
        }
    }


    public static void Save(string saveName, int _saving)
    {
        PlayerPrefs.SetInt(saveName, _saving);
    }
    public static void Save(string saveName, float _saving)
    {
        PlayerPrefs.SetFloat(saveName, _saving);
    }
    public static void Save(string saveName, string _saving)
    {
        PlayerPrefs.SetString(saveName, _saving);
    }
    public static void Save(string saveName, bool _saving)
    {
        if (_saving)
        {
            PlayerPrefs.SetInt(saveName, 1);
        }
        else
        {
            PlayerPrefs.SetInt(saveName, 0);
        }
    }

    public static int GetSaveDataInt(string saveName)
    {
        if (PlayerPrefs.HasKey(saveName))
        {
            return PlayerPrefs.GetInt(saveName);
        }
        else
        {
            Debug.Log(saveName+ ": Not saved");
            return -1;
        }
    }
    public static string GetSaveDataString(string saveName)
    {
        if (PlayerPrefs.HasKey(saveName))
        {
            return PlayerPrefs.GetString(saveName);
        }
        else
        {
            Debug.Log(saveName + ": Not saved");
            return "";
        }
    }
    public static float GetSaveDataFloat(string saveName)
    {
        if (PlayerPrefs.HasKey(saveName))
        {
            return PlayerPrefs.GetFloat(saveName);
        }
        else
        {
            Debug.Log(saveName + ": Not saved");
            return 0f;
        }
    }
    public static bool GetSaveDataBool(string saveName)
    {
        if (PlayerPrefs.HasKey(saveName))
        {
            int boolInt = PlayerPrefs.GetInt(saveName);
            if(boolInt == 1)
            {
                return true;
            }
            else
            {
                return false;   
            }
        }
        else
        {
            Debug.Log(saveName + ": Not saved");
            return false;
        }
    }
    [NaughtyAttributes.Button("Clear All Saves")]
    public void ClearAllData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("Saves Cleared");
    }
}
