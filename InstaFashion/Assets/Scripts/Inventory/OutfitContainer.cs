using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OutfitContainer : MonoBehaviour
{
    [SerializeField]
    protected TextMeshProUGUI nameText;
    [SerializeField]
    protected TextMeshProUGUI posted;
    [SerializeField]
    protected Image outlineImage;
    [SerializeField]
    protected Image fillImage;

    protected int popularity;
    private int postedCount;
    private int myIndex;

    protected InventoryManager manager;
    protected Outfit myInfo;
    public virtual void SetContainer(InventoryManager _manager, Outfit _outfitInfo)
    {
        manager             = _manager;
        myInfo              = _outfitInfo;
        nameText.text       = _outfitInfo.name;
        popularity          = _outfitInfo.popularityStars;
        outlineImage.sprite = _outfitInfo.outlineIcon;
        fillImage.sprite    = _outfitInfo.fillIcon;

        Material mat = Instantiate(fillImage.material);
        mat.SetColor("_ColorMask", _outfitInfo.itemColor);
        fillImage.material = mat;

        posted.text         = "Posts: 00";        
    }

    public void OnClick_SetOutfit()
    {
        manager.SetPlayerOutfit(myInfo);
    }
}