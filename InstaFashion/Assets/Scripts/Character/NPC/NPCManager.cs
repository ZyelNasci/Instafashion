using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField]
    private NPCWayPoint[] wayPoints;
    [SerializeField]
    private Transform[] stopPoints;

    [SerializeField]
    private int NPCCount;
    [SerializeField]
    private NPC_Walker npcPrefab;

    public void Start()
    {
        CreateNPCS();
    }

    /// <summary>
    /// Instantiates and sets the path of an NPC
    /// </summary>
    public void CreateNPCS()
    {
        List<NPCWayPoint> ways = new List<NPCWayPoint>();
        ways.AddRange(wayPoints);
        for (int i = 0; i < NPCCount; i++)
        {
            NPC_Walker temp = Instantiate(npcPrefab, Vector3.zero, Quaternion.identity);

            if(ways.Count <= 0)
            {
                ways.AddRange(wayPoints);
            }

            int sort = Random.Range(0, ways.Count);
            temp.SetWalker(ways[Random.Range(0, ways.Count)]);
            ways.RemoveAt(sort);
        }
        for (int i = 0; i < stopPoints.Length; i++)
        {
            NPC_Walker temp = Instantiate(npcPrefab, Vector3.zero, Quaternion.identity);
            temp.SetIdle(stopPoints[i].transform.localScale, stopPoints[i].position);
        }
    }
}