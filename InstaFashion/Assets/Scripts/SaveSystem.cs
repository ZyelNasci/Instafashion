using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    [SerializeField]
    public GameData dataSO;

    private void OnDisable()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        DataInfo data = new DataInfo(dataSO);
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Data", json);
        Debug.Log("Game Saved!");
    }

    public bool LoadGame()
    {
        string json = PlayerPrefs.GetString("Data", "");
        if (!string.IsNullOrEmpty(json))
        {
            DataInfo newData = JsonUtility.FromJson<DataInfo>(json);      
            if(newData != null)
            {
                newData.RestoreValues(dataSO);
                return true;
            }
            Debug.Log("Don't have data2");
            return false;
        }
        else
        {
            Debug.Log("Don't have data");
            return false;
        }
    }
}