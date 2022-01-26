using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OutfitContainerSTORE : OutfitContainer
{
    [Header("Store Outfit Components")]
    [SerializeField]
    private Button buyButton;
    [SerializeField]
    private TextMeshProUGUI BuyText;

    public override void SetContainer(InventoryManager _manager, Outfit _outfitInfo)
    {
        manager         = _manager;
        myInfo          = _outfitInfo;
        nameText.text   = _outfitInfo.name;
        popularity      = _outfitInfo.popularityStars;

        outlineImage.sprite = _outfitInfo.outlineIcon;
        fillImage.sprite    = _outfitInfo.fillIcon;

        Material mat = Instantiate(fillImage.material);
        mat.SetColor("_ColorMask", _outfitInfo.itemColor);
        fillImage.material = mat;

        posted.text = "Price:" + '\n' + "$" + _outfitInfo.price.ToString("0.00");
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
            manager.BuyOutfit(myInfo);
            myInfo.unlocked = true;
        }
        else
        {
            Debug.Log("Have no money left");
        }
    }
}