using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WelcomeInfo : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasInfo;
    [SerializeField]
    private RectTransform board;

    public void OpenWelcomeBoard()
    {
        gameObject.SetActive(true);

        canvasInfo.alpha = 0;
        canvasInfo.interactable = false;

        //450
        board.sizeDelta = Vector2.one * 200;
        board.DOSizeDelta(Vector2.one * 450, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            canvasInfo.interactable = true;
            canvasInfo.DOFade(1, 0.5f);
        });
    }

    public void CloseWelcomeBoard()
    {        
        GameController.Instance.CloseWelcom();           
    }

}
