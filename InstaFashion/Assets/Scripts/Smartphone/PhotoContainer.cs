using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoContainer : MonoBehaviour
{
    [SerializeField]
    private Image photoImage;

    private int likeCount;

    public void SetPhotoContainer(Sprite _sprite)
    {
        photoImage.sprite = _sprite;
    }
}
