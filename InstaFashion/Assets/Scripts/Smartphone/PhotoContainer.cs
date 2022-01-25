using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoContainer : MonoBehaviour
{
    [SerializeField]
    private SmartphoneManager manager;
    [SerializeField]
    private Image photoImage;

    private int followersCount;
    private int likeCount;

    private int currentLike;
    private int currentFollowers;

    public void InitializeContainer(SmartphoneManager _manager)
    {
        manager = _manager;
    }

    public void SetPhotoContainer(Sprite _sprite, int _like)
    {
        photoImage.sprite = _sprite;
        likeCount = _like;

        //totalLikes          += likes;
        //float followerPercentage = Random.Range(0.3f, 0.5f);
        //followersCount += Mathf.CeilToInt(likeCount * followerPercentage);        

        //StartCoroutine(DelayToLike(likeCount));
        //StartCoroutine(DelayToFollowers(followersCount));
    }

    IEnumerator DelayToLike(int _value)
    {
        float initiTime = Random.Range(0.1f, 0.5f);
        yield return new WaitForSeconds(initiTime);

        while(_value > 0)
        {
            int likeValue = Random.Range(1, _value + 1);
            float timer = Random.Range(0.1f, 0.5f);
            yield return new WaitForSeconds(timer);
            manager.AddLikes(likeValue);
            _value -= likeValue;
        }        
    }

    IEnumerator DelayToFollowers(int _value)
    {
        float initiTime = Random.Range(0.1f, 0.5f);
        yield return new WaitForSeconds(initiTime);
        while (_value > 0)
        {
            int likeValue = Random.Range(1, _value + 1);
            float timer = Random.Range(0.1f, 0.5f);
            yield return new WaitForSeconds(timer);
            manager.AddFollower(likeValue);
            _value -= likeValue;
        }
    }
}