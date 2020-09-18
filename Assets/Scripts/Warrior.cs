using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrior : MonoBehaviour
{
    
    public enum MoveWay { left, right, stop, attack }
    
    public MoveWay moveDirection;
    public float speed = 1;

    public float HP = 100;
    public float ATK = 25;


    private GameObject attackTarget;
    private Warrior attackTargetInfo;

    private Animator animator;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (moveDirection == MoveWay.left)
        {
            spriteRenderer.flipX = true;
        }
        else { spriteRenderer.flipX = false; }
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        if (moveDirection==MoveWay.attack)
        {

            //공격 처리
            return;
        }

        if (moveDirection == MoveWay.stop)
        {
            return;
        }

        if (moveDirection==MoveWay.right)
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));

        }
        else { transform.Translate(new Vector2(speed * Time.deltaTime, 0)); }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        //상대방이 적일 때
        if (other.gameObject.GetComponent<Warrior>().moveDirection != moveDirection)
        {
            attackTarget = other.gameObject;
            attackTargetInfo = attackTarget.GetComponent<Warrior>();
            Attack();
        }
        /*
        //상대방이 같은 팀일때
        else if (enemy.GetComponent<Warrior>().moveDirection == moveDirection)
        {
            moveDirection = MoveWay.stop;
        }*/
    }

    public void Attack()
    {
        //나 자신에 대한 코드
        moveDirection = MoveWay.attack;
        animator.SetBool("Attacking", true);

        //공격 대상에 대한 코드
        StartCoroutine("ApplyDemage");


    }
    IEnumerator ApplyDemage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            attackTargetInfo.HP -= ATK;
            if (attackTargetInfo.HP <= 0)
            {
                attackTarget.SendMessage("Die");
                break;
                Debug.Log("죽어라!1");
            }
        }
    }

    public void Die()
    {

        animator.SetTrigger("Die");

        Destroy(gameObject);

    }
    
}
