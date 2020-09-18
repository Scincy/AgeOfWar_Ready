using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnQueueItem : MonoBehaviour
{
    public bool activated= false;
    public Sprite offImage;//켜지면 바꾸게 할 이미지
    public Sprite onImage;//켜지면 바꾸게 할 이미지

    private Image checker;

    //싱글톤 디자인 패턴 선언
    private static SpawnQueueItem instance = null;



    private void Awake()
    {
        // 싱글톤
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this) //앞에서 이미 생성한 상태에서 새롭게 자리를 잡으려 하면?
        {
            Destroy(this); //죽인다.
        }
        // 싱글톤 끝
    }
    private void Start()
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
