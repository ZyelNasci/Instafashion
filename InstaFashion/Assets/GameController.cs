using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameData dataSO;
    [SerializeField]
    private SmartphoneManager smartphone;
    [SerializeField]
    private PlayerController player;

    public void Start()
    {
        if (SaveSystem.Instance.LoadGame())
        {
            smartphone.LoadSmartphone(dataSO);
            smartphone.SwitchScreen(SmartphoneScreen.None);
            player.SwitchState(player.idleState);
            player.LoadPlayerSettings(dataSO);
        }
        else
        {
            dataSO.ResetGameData();
            player.SwitchState(player.interactState);
            smartphone.Open_CreateCharacter();
        }
    }

}
