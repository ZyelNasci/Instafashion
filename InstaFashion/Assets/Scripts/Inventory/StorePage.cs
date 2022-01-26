using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StorePage : InventoryPage
{
//    [Header("Store Page Components")]
//    [SerializeField]
//    private TextMeshPro priceText;
//    [SerializeField]
//    private Button buyButton;
//    [SerializeField]
//    private TextMeshPro BuyText;    

//    public override void SetContainer(InventoryType type)
//    {
//        currentOutfits.Clear();
//        Outfit[] outfits = outfitSO.outfits;
//        for (int i = 0; i < outfits.Length; i++)
//        {
//            if (outfits[i].inventoryType == type)
//            {
//                OutfitContainer temp    = manager.GetContainer();
//                outfits[i].myType       = outfitSO.type;
//                temp.transform.parent   = Content;
//                temp.transform.SetAsLastSibling();
//                temp.SetContainer(manager, outfits[i]);
//                currentOutfits.Add(temp); 

//                priceText.text = "Price:" + '\n' + "$" + outfits[i].price.ToString("0.00");
//                if (outfits[i].unlocked)
//                {
//                    buyButton.interactable = false;
//                    BuyText.text = "SOLD";
//                }
//                else
//                {
//                    buyButton.interactable = true;
//                    BuyText.text = "BUY";
//                }                
//            }
//        }
//    }
}
