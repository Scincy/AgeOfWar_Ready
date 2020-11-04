using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 게임에서 소환되는 전사의 기본 클래스 입니다.
/// </summary>
public class Warrior : Charactor
{    
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<CharactorInfo>().team != Team.left)
        {
            Destroy(this.GetComponent<Warrior>());
            this.enabled = false;
        }

        myInfo = GetComponent<CharactorInfo>();
        animator = GetComponent<Animator>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        //방향 설정
        if (GetComponent<CharactorInfo>().team == Team.left)
        {
            spriteRenderer.flipX = true;
        }
        else {
            this.enabled = false;


        }

    }

    public override void Move()
    {
        transform.Translate(new Vector2(myInfo.speed * Time.deltaTime, 0));
        
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject otherChar = other.gameObject;
        Team collidedChar = otherChar.GetComponent<CharactorInfo>().team;
        Debug.Log(collidedChar.ToString());
        if (collidedChar != myInfo.team)
        {
            action = State.attack;
            attackTarget = otherChar;
            attackTargetInfo = otherChar.GetComponent<CharactorInfo>();
            Attack();
        }
        //상대방이 같은 팀일때
        else if (enemy.GetComponent<Warrior>().moveDirection == moveDirection)
        {
            moveDirection = MoveWay.stop;
        }
    }*/

}
