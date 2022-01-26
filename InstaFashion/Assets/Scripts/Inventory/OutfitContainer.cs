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
    protected Image outlineImage;
    [SerializeField]
    protected Image fillImage;
    [SerializeField]
    protected Color yellowStar;
    [SerializeField]
    protected Color grayStar;
    [SerializeField]
    protected Color unselecColor;
    [SerializeField]
    protected Color selectedColor;
    [SerializeField]
    protected Image selectedImage;
    [SerializeField]
    protected Image[] starImage;

    //[SerializeField]
    //private Image

    protected int popularity;
    private int postedCount;
    private int myIndex;

    protected InventoryPage pageManager;
    protected Outfit myInfo;
    public virtual void SetContainer(InventoryPage _manager, Outfit _outfitInfo)
    {
        pageManager         = _manager;
        myInfo              = _outfitInfo;
        nameText.text       = _outfitInfo.name;
        popularity          = _outfitInfo.currentPopularityStar;
        outlineImage.sprite = _outfitInfo.outlineIcon;
        fillImage.sprite    = _outfitInfo.fillIcon;

        CheckPopularity();

        Material mat = Instantiate(fillImage.material);
        mat.SetColor("_ColorMask", _outfitInfo.itemColor);
        fillImage.material = mat;

        //posted.text         = "Posts: 00";        
    }

    public void CheckPopularity()
    {
        popularity = myInfo.currentPopularityStar;
        for (int i = 0; i < starImage.Length; i++)
        {
            if (i < popularity)
            {
                starImage[i].color = yellowStar;
            }
            else
            {
                starImage[i].color = grayStar;
            }
        }
    }

    public void OnClick_SetOutfit()
    {
        pageManager.SelectOutfit(myInfo, this);
        SelectFeedback();
    }

    public void SelectFeedback()
    {
        selectedImage.color = selectedColor;
    }
    public void UnselectedFeedback()
    {
        selectedImage.color = unselecColor;
    }
}