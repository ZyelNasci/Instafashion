using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class NPC_Controler : CharacterBase
{
    [Header("NPC Components")]
    [SerializeField]
    private GameObject iconInteraction;
    [SerializeField]
    private InventoryManager inventory;

    [SerializeField]
    private Gradient colorSkin;

    public void OnClick_Interact()
    {
        inventory.gameObject.SetActive(true);
    }

    public void RandomizeOutfit()
    {
        OutfitSO clothes = Resources.Load<OutfitSO>("Scriptables/ClothesSO");
        OutfitSO hairs = Resources.Load<OutfitSO>("Scriptables/HairSO");
        OutfitSO Accessories = Resources.Load<OutfitSO>("Scriptables/AccessoriesSO");

        int clothesIndex        = Random.Range(0, clothes.outfits.Length);
        int hairIndex           = Random.Range(0, hairs.outfits.Length);
        int accessoriesIndex    = Random.Range(0, Accessories.outfits.Length);

        SetClotheOutfit(clothes.outfits[clothesIndex]);
        SetClotheOutfit(hairs.outfits[hairIndex]);
        SetClotheOutfit(Accessories.outfits[accessoriesIndex]);

        spRender[0].material.SetColor("_ColorMask", colorSkin.Evaluate(Random.Range(0f,1f)));
        spRender[1].sprite = clothes.outfits[clothesIndex].outlineIcon;
        spRender[2].sprite = hairs.outfits[hairIndex].outlineIcon;
        spRender[3].sprite = Accessories.outfits[accessoriesIndex].outlineIcon;
    }
}

#if UNITY_EDITOR
// provide the component type for which this inspector UI is required
[CustomEditor(typeof(NPC_Controler))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        NPC_Controler _target = (NPC_Controler)target;
        if (GUILayout.Button("Randomize Outfit"))
        {
            _target.RandomizeOutfit();
        }
    }
}
#endif
