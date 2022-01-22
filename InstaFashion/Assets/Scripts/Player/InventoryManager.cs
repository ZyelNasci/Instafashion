using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private Transform content;

    [SerializeField]
    private OutfitContainer outfitContainerPrefab;

    [SerializeField]
    private OutfitSO outfitSO;

    private Stack<OutfitContainer> outfitStack = new Stack<OutfitContainer>();
    private List<OutfitContainer> currentOutfits = new List<OutfitContainer>();

    private void Start()
    {
        CreateContainers();
        SetContainer();
    }

    private void SetContainer()
    {
        currentOutfits.Clear();
        Debug.Log("VeioAqui");
        Outfit[] outfits = outfitSO.outfits;
        for (int i = 0; i < outfits.Length; i++)
        {
            OutfitContainer temp = GetContainer();
            temp.SetContainer(outfits[i], outfits[i].itemColor);            
            currentOutfits.Add(temp);
        }
    }

    #region Pooling
    private void CreateContainers()
    {
        Debug.Log("VeioAquiTambem");
        for (int i = 0; i < 10; i++)
        {
            OutfitContainer temp = Instantiate(outfitContainerPrefab, content);
            temp.gameObject.SetActive(false);
            outfitStack.Push(temp);
        }
    }

    private OutfitContainer GetContainer()
    {
        if(outfitStack.Count > 0)
        {
            OutfitContainer temp = outfitStack.Pop();
            temp.gameObject.SetActive(true);
            return temp;
        }
        else
        {
            OutfitContainer temp = Instantiate(outfitContainerPrefab, content);
            temp.gameObject.SetActive(true);
            return temp;
        }
    }

    private void StoreContainer (OutfitContainer _container)
    {
        outfitStack.Push(_container);
    }
    #endregion
}