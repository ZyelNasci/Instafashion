using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Walker : CharacterBase
{
    [SerializeField]
    private NPCWayPoint myWayPoint;
    private Transform currentPoint;
    [SerializeField]
    private Gradient colorSkin;

    private bool walker;
    private int currentWayIndex;
    private bool reverseWay;

    public override void Update()
    {
        base.Update();

        if (walker)
        {
            if (currentPoint != null && Vector2.Distance(transform.position, currentPoint.position) < 0.1f)            
                SetWalkPath();            
        }        
    }

    public void SetIdle(Vector2 _dir, Vector2 _position)
    {
        transform.position = _position;

        for (int i = 0; i < anim.Length; i++)
        {
            anim[i].SetFloat("velX", _dir.x);
            anim[i].SetFloat("velY", _dir.y);
        }
        RandomizeOutfit();
        SwitchState(idleState);
    }

    public void SetWalker(NPCWayPoint _way)
    {
        walker = true;

        myWayPoint = _way;
        currentWayIndex = Random.Range(0, myWayPoint.points.Length);

        if(Random.Range(0,10) >= 5)
        {
            reverseWay = true;
        }

        speed = Random.Range(0.5f, 2.5f);
        SwitchState(idleState);
        //myWayPoint.transform.parent = null;
        RandomizeOutfit();
        transform.position = myWayPoint.points[currentWayIndex].position;
        SetWalkPath();
    }
    
    public void SetWalkPath()
    {
        if(!reverseWay)
            currentPoint = GetMyNextPoint();
        else
            currentPoint = GetMyPreviewsPoint();

        Vector2 direction = currentPoint.position - transform.position;
        input_walk = direction.normalized;
    }

    public Transform GetMyNextPoint()
    {
        currentWayIndex++;
        if (currentWayIndex >= myWayPoint.points.Length)
            currentWayIndex = 0;

        return myWayPoint.points[currentWayIndex];
    }

    public Transform GetMyPreviewsPoint()
    {
        currentWayIndex--;
        if (currentWayIndex < 0)
            currentWayIndex = myWayPoint.points.Length - 1;

        return myWayPoint.points[currentWayIndex];
    }

    /// <summary>
    /// Randomize hair, clothes and accessories
    /// </summary>
    public void RandomizeOutfit()
    {
        OutfitSO clothes = Resources.Load<OutfitSO>("Scriptables/ClothesSO");
        OutfitSO hairs = Resources.Load<OutfitSO>("Scriptables/HairSO");
        OutfitSO Accessories = Resources.Load<OutfitSO>("Scriptables/AccessoriesSO");

        int clothesIndex = Random.Range(0, clothes.outfits.Length);
        int hairIndex = Random.Range(0, hairs.outfits.Length);
        int accessoriesIndex = Random.Range(0, Accessories.outfits.Length);

        SetClotheOutfit(clothes.outfits[clothesIndex]);
        SetClotheOutfit(hairs.outfits[hairIndex]);
        SetClotheOutfit(Accessories.outfits[accessoriesIndex]);

        spRender[0].material.SetColor("_ColorMask", colorSkin.Evaluate(Random.Range(0f, 1f)));
        spRender[1].sprite = clothes.outfits[clothesIndex].outlineIcon;
        spRender[2].sprite = hairs.outfits[hairIndex].outlineIcon;
        spRender[3].sprite = Accessories.outfits[accessoriesIndex].outlineIcon;
    }
}
