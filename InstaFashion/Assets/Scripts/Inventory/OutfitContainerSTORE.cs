using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class OutfitContainerSTORE : OutfitContainer
{
    [Header("Store Outfit Components")]
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private TextMeshProUGUI BuyText;
    [SerializeField]
    protected TextMeshProUGUI priceText;

    public override void SetContainer(InventoryPage _manager, Outfit _outfitInfo)
    {
        pageManager     = _manager;
        myInfo          = _outfitInfo;
        nameText.text   = _outfitInfo.name;
        popularity      = _outfitInfo.popularityStars;

        outlineImage.sprite = _outfitInfo.outlineIcon;
        fillImage.sprite    = _outfitInfo.fillIcon;

        Material mat = Instantiate(fillImage.material);
        mat.SetColor("_ColorMask", _outfitInfo.itemColor);
        fillImage.material = mat;

        priceText.text = "$" + _outfitInfo.price.ToString();
        if (_outfitInfo.unlocked)
        {
            buyButton.interactable = false;
            BuyText.text = "SOLD";
        }
        else
        {
            buyButton.interactable = true;
            BuyText.text = "BUY";
        }
    }

    public void OnClick_BuyOutfit()
    {
        if (GameController.Instance.CheckHasMoney((int)myInfo.price))
        {
            buyButton.interactable = false;
            BuyText.text = "SOLD";
            pageManager.BuyOutfit(myInfo);
            myInfo.unlocked = true;
        }
        else
        {
            priceText.DOKill();
            priceText.color = Color.black;
            priceText.DOColor(Color.red, 0.3f).SetLoops(2, LoopType.Yoyo);
            priceText.transform.DOShakePosition(0.2f, 3);
            Debug.Log("Have no money left");
        }
    }
}