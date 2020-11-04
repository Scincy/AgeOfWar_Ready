using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public enum Team { left, right }
public class CharactorInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Stats")]
    public float HP = 100;//체력
    public float ATK = 25;//공격력
    public float AttackTick = 1;//공격을 수행할 때 까지 기다려야 하는 시간
    public float speed = 1;//이동 속도
    public int deadEXP;
    public Team team;
    public int id;
    public int level;

    //++
    public float attackCooldown = 0;
    public CharactorHPbar HPbar;

    public void ResetAttackCooldown()
    {
        attackCooldown = 0;
    }


    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("Entered!");
        HPbar.Show();
    }
    public void OnPointerExit(PointerEventData data)
    {
        HPbar.Hide();
    }
}
