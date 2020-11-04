using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Charactor
{
    
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<CharactorInfo>().team != Team.right)
        {
           
           Destroy(this.GetComponent<Enemy>());
           this.enabled = false;
        }

        myInfo = GetComponent<CharactorInfo>();
        animator = GetComponent<Animator>();
    }



    public override void Move()
    {
        transform.Translate(new Vector2(-myInfo.speed * Time.deltaTime, 0));
    }

    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.tag.Equals("Building")) //캐릭터끼리 겹쳤을 때 수행할 부분
        {
            GameObject otherChar = other.gameObject;
            Team collidedChar = otherChar.GetComponent<CharactorInfo>().team;

            if (collidedChar != GetComponent<CharactorInfo>().team)
            {
                action = State.attack;
                attackTarget = otherChar;
                attackTargetInfo = otherChar.GetComponent<Warrior>().myInfo;
                Attack();
            }
        }
        else // 건물과 닿았을 때 수행할 구문
        {
            
        }
        //상대방이 같은 팀일때
        else if (enemy.GetComponent<Warrior>().moveDirection == moveDirection)
        {
            moveDirection = MoveWay.stop;
        }
    }*/
}
