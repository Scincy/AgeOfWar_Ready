using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnQueueItem : MonoBehaviour
{
    public bool activated= false;
    public Sprite offImage;//켜지면 바꾸게 할 이미지
    public Sprite onImage;//켜지면 바꾸게 할 이미지

    public Image checker;


    void Start()
    {
        checker = GetComponent<Image>();


        //Debug purpose
        if (activated)
        {
            On();
        }
    }

    public void On()
    {
        activated = true;
        checker.sprite = onImage;
    }
    public void Off()
    {
        activated = false;
        checker.sprite = offImage;
    }

}
