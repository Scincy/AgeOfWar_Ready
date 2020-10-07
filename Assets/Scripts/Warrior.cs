using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// 게임에서 소환되는 전사의 기본 클래스 입니다.
/// </summary>
public class Warrior : MonoBehaviour
{

    public enum MoveWay { left, right, stop, attack }
    [Header("Info")]
    public MoveWay moveDirection;
    public bool isAttacking = false;
    [Space]
    [Header("Stats")]
    public float HP = 100;//체력
    public float ATK = 25;//공격력
    public float AttackTick = 1;//공격을 수행할 때 까지 기다려야 하는 시간
    public float speed = 1;//이동 속도

    // 공격 상대에 대한 정보
    private GameObject attackTarget;
    private Warrior attackTargetInfo;

    private Animator animator;
    private GameObject enemy;

    //TODO Moveway Backup
    [SerializeField]
    private MoveWay teamInfo;
    // Start is called before the first frame update
    void Start()
    {
        //TODo
        teamInfo = moveDirection;

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

        //TODO
        switch (moveDirection)
        {
            case MoveWay.left:
                transform.Translate(new Vector2(speed * Time.deltaTime, 0));
                break;
            case MoveWay.right:
                transform.Translate(new Vector2(-speed * Time.deltaTime, 0));
                break;
            case MoveWay.stop:
                Stop();
                break;
            case MoveWay.attack:

                break;
            default:
                break;
        }
    }

    void Move()
    {
        // TODO 코드정리 switch문으로 바꿀 것
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
        Warrior otherteam = other.gameObject.GetComponent<Warrior>();
        if (otherteam.teamInfo == teamInfo)
        {
            Debug.Log("SAME!");
        }
        else Debug.Log("Enemy!");
        //상대방이 적일 때 (내 방향의 반대방향에서 오고 있을 떄
        if (otherteam.teamInfo != teamInfo)
        {
            moveDirection = MoveWay.attack;
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
        //가던 길을 멈추게 한다.//TODO
        //moveDirection = MoveWay.attack;
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

            //적 사망
            if (attackTargetInfo.HP <= 0)
            {
                //TODO 
                moveDirection = teamInfo;//원래 가려던 길 가게 만들기

                attackTarget.SendMessage("Die");
                Debug.Log("죽어라!1");
                break;
            }
        }
    }

    //TODO
    public void Stop()
    {
        // 이동모션이므로 애니메이터에 변수 추가 및 내용 변경 필요
        // Bandit과 Heavy bandit이 서로 Attack모션에 대한 animator변수 정보가 다르므로 설정 필요
        animator.SetBool("Attacking", false);
    }

    public void Die()
    {

        animator.SetTrigger("Die");
        Destroy(gameObject);

    }

}
