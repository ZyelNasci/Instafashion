using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    [SerializeField]
    private GameData dataSO;
    [SerializeField]
    private SmartphoneManager smartphone;
    [SerializeField]
    private PlayerController player;
    public PlayerController GetPlayer { get { return player; } }

    [Header("Pause Components")]
    [SerializeField]
    private GameObject pauseGroup;
    [SerializeField]
    private GameObject instructionGroup;
    [SerializeField]
    private GameObject HomeGroup;

    [SerializeField]
    private TextMeshProUGUI textMoney;

    public void Start()
    {
        if (SaveSystem.Instance.LoadGame() && !dataSO.tutorial)
        {
            UpdateMoney(dataSO.Money);
            smartphone.LoadSmartphone(dataSO);
            smartphone.SwitchScreen(SmartphoneScreen.None);
            player.SwitchState(player.idleState);
            player.LoadPlayerSettings(dataSO);
        }
        else
        {
            UpdateMoney(0);
            dataSO.ResetGameData();
            player.SwitchState(player.interactState);
            smartphone.Open_CreateCharacter();
        }
    }

    public void UpdateMoney(int _value)
    {
        dataSO.Money += _value;
        textMoney.text =dataSO.Money.ToString();
    }

    public bool CheckHasMoney(int _value)
    {
        if(dataSO.Money >= _value)
        {
            dataSO.Money -= _value;
            textMoney.text = dataSO.Money.ToString();
            return true;
        }
        return false;
    }

    public void OnClick_OpenInstruction()
    {
        instructionGroup.SetActive(true);        
        HomeGroup.SetActive(false);
    }

    public void OnClick_OpenPause()
    {
        pauseGroup.SetActive(true);
        HomeGroup.SetActive(true);
        instructionGroup.SetActive(false);
    }
    public void OnClick_ClosePause()
    {
        HomeGroup.SetActive(false);
        instructionGroup.SetActive(false);
        pauseGroup.gameObject.SetActive(false);
    }

    public void OnClick_QuitGame()
    {
        Application.Quit();
    }
}