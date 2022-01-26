using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
public class GameController : Singleton<GameController>
{
    [SerializeField]
    private GameData dataSO;
    [SerializeField]
    private SmartphoneManager smartphone;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private CanvasGroup canvas;
    [SerializeField]
    private WelcomeInfo welcomeInfo;
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
            canvas.alpha = 1;
            UpdateMoney(dataSO.Money);
            smartphone.LoadSmartphone(dataSO);
            smartphone.SwitchScreen(SmartphoneScreen.None);
            player.SwitchState(player.idleState);
            player.LoadPlayerSettings(dataSO);
        }
        else
        {
            canvas.alpha = 0;
            UpdateMoney(0);
            dataSO.ResetGameData();
            player.SwitchState(player.interactState);
            smartphone.Open_CreateCharacter();
        }
    }

    #region Welcome Methods
    public void ShowWelcome()
    {
        StartCoroutine(DelayToWelcome());
    }

    public void CloseWelcom()
    {
        dataSO.tutorial = false;
        welcomeInfo.gameObject.SetActive(false);
        ShowHUD();
        player.SwitchState(player.idleState);
    }
    public IEnumerator DelayToWelcome()
    {
        yield return new WaitForSeconds(0.5f);
        welcomeInfo.OpenWelcomeBoard();
    }
    #endregion

    #region MoneyMethods
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
    #endregion

    #region HudMethods
    public void HideHUD()
    {
        canvas.interactable = false;
        canvas.DOFade(0, 0.3f);
    }

    public void ShowHUD()
    {
        canvas.interactable = true;
        canvas.DOFade(1, 0.3f);
    }
    #endregion

    #region On Click Methods
    public void OnClick_OpenInstruction()
    {
        instructionGroup.SetActive(true);        
        HomeGroup.SetActive(false);
    }

    public void OnClick_OpenPause()
    {
        player.SwitchState(player.interactState);
        pauseGroup.SetActive(true);
        HomeGroup.SetActive(true);
        instructionGroup.SetActive(false);
    }
    public void OnClick_ClosePause()
    {
        player.SwitchState(player.idleState);
        HomeGroup.SetActive(false);
        instructionGroup.SetActive(false);
        pauseGroup.gameObject.SetActive(false);
    }

    public void OnClick_QuitGame()
    {
        Application.Quit();
    }
    #endregion
}