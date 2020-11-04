using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorHPbar : MonoBehaviour
{
    /////////////////////
    public Slider slider;
    public Vector3 offset;
    public Image[] images;
    ////////////////////


    public WarriorInfo[] warriorInfo;
    CharactorInfo myInfo;
    private float Hp;
    private float MaxHp;

    private void Start()
    {
        myInfo = GetComponentInParent<CharactorInfo>();
        images = GetComponentsInChildren<Image>();
        if (myInfo == null)
        {
            Debug.LogError("HPbar Info is null");
        }
        Hide();
        MaxHp = myInfo.level * GameManager.instance.HPupAmount + warriorInfo[myInfo.id].HP; //내 레벨의 최대 체력

        slider.maxValue = MaxHp;
        slider.minValue = 0;
    }
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
        slider.value = myInfo.HP;
    }

     public void Hide()
    {

        foreach (Image item in images)
        {
            item.color = Color.clear;
        }
    }
    public void Show()
    {
        foreach (Image item in images)
        {
            item.color = Color.red;
        }
    }
}
