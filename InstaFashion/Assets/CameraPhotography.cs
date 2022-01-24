using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraPhotography : Singleton<CameraPhotography>
{
    [SerializeField]
    private Camera photoCam;    

    private bool Screenshot;
    private int photoIndex;

}
