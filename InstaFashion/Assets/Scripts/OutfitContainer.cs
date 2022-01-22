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

    public Color myColor;

    public void SetContainer(Outfit _newOutfit, Color _color)
    {
        nameText.text       = _newOutfit.name;
        popularity          = _newOutfit.popularityStars;
        outlineImage.sprite = _newOutfit.outlineIcon;
        fillImage.sprite    = _newOutfit.fillIcon;

        myColor             = _newOutfit.itemColor;

        Material mat = Instantiate(fillImage.material);
        mat.SetColor("_ColorMask", _newOutfit.itemColor);
        fillImage.material = mat;

        //fillImage.material.SetColor("_ColorMask", _newOutfit.itemColor);

        posted.text         = "Posts: 00";        
    }    

}