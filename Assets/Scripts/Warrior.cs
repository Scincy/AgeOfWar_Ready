using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Warrior : MonoBehaviour
{

    public enum MoveWay { left, right, stop }

    public MoveWay moveDirection;
    public bool isAttacking = false;


    public float HP = 100;//체력
    public float ATK = 25;//공격력
    public float AttackTick = 1;//공격을 수행할 때 까지 기다려야 하는 시간
    public float speed = 1;//이동 속도


    private GameObject attackTarget;
    private Warrior attackTargetInfo;

    private Animator animator;
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        //방향 설정
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
        if (moveDirection == MoveWay.stop)
        {
            return;
        }

        if (moveDirection == MoveWay.right)
        {
            transform.Translate(new Vector2(-speed * Time.deltaTime, 0));

        }
        else { transform.Translate(new Vector2(speed * Time.deltaTime, 0)); }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        //상대방이 적일 때 (내 방향의 반대방향에서 오고 있을 떄
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
        //가던 길을 멈추게 한다.
        moveDirection = MoveWay.stop;
        //공격을 시작한다.
        isAttacking = true;
        //공격 애니메이션 재생
        animator.SetBool("Attacking", true);

        //공격 대상에 대한 코드
        StartCoroutine("ApplyDemage");
    }
    IEnumerator ApplyDemage()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(AttackTick);
            attackTargetInfo.HP -= ATK;
            if (attackTargetInfo.HP <= 0)
            {
                attackTarget.SendMessage("Die");
                Debug.Log("죽어라!1");
                break;
            }
        }
    }

    public void Die()
    {

        animator.SetTrigger("Die");
        Destroy(gameObject);

    }

}
