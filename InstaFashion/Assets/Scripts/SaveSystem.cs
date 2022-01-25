using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : Singleton<SaveSystem>
{
    [SerializeField]
    public GameData dataSO;

    private void OnApplicationQuit()
    {
        //SaveGame();
    }

    public void SaveGame()
    {
        DataInfo data = new DataInfo(dataSO);
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Data", json);
        Debug.Log("Game Saved!");
    }
    public void LoadGame()
    {
        string json = PlayerPrefs.GetString("Data", "");
        if (!string.IsNullOrEmpty(json))
        {
            DataInfo newData = JsonUtility.FromJson<DataInfo>(json);      
            if(newData != null)
            {
                newData.RestoreValues(dataSO);                
            }
            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.Log("Don't have data");
        }
    }
}