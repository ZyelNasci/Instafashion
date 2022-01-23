using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OutfitContainer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI posted;
    [SerializeField]
    private Image outlineImage;
    [SerializeField]
    private Image fillImage;

    private int popularity;
    private int postedCount;
    private int myIndex;

    private InventoryManager manager;
    private Outfit myInfo;
    public void SetContainer(InventoryManager _manager, Outfit _outfitInfo, Color _color)
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