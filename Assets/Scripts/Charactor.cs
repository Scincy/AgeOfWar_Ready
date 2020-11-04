using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum State { move, stop, attack, buildingAttack }

public abstract class Charactor : MonoBehaviour, IGameManagement
{
    [Header("Info")]
    public State action;

    public bool isAttacking = false;
    [Space]

    [Header("Setting")]
    float dieFadeOutTime = 1;
    float attackdistance = 1;

    // 공격 상대에 대한 정보
    protected GameObject attackTarget;
    public Animator animator;
    public CharactorInfo myInfo;
    public CharactorInfo attackTargetInfo;

    private GameObject ally;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        //////////////////////////////////
        attackTarget = GetNearestEnemy(myInfo.team);
        ReadyForAttack();
        /*ally = GetClosestAlly(myInfo.team);
        if (!ReferenceEquals(ally,null)) action = State.stop;*/
        //////////////////////////////////
        
        //animation selection
        switch (action)
        {
            case State.move:
                Move();
                animator.SetBool("Moving", true);
                break;
            case State.stop:
                animator.SetBool("Moving", false);
                animator.SetBool("Attacking", false);
                break;
            case State.attack:
                animator.SetBool("Attacking", true);
                break;
            case State.buildingAttack:
                animator.SetBool("Moving", false);
                animator.SetBool("Attacking", true);
                break;
            default:
                break;
        }
    }
    

    public abstract void Move();


    GameObject GetClosestAlly(Team team)
    {
        //전투중이면 작동하지 않음
        if (isAttacking) return null;
        
        int index = 0;
        float distance;
        float closestDistance= float.MaxValue;

        if (team == Team.left)
        {
            for (int i = 0; i < GetTeamLeft().Count; i++)
            {
                if (ReferenceEquals(GetTeamLeft()[i], this.gameObject)) continue;
                if (GetTeamLeft()[i].tag.Equals("Building")) continue;

                distance = Vector3.Distance(transform.position, GetTeamLeft()[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    index = i;
                }
            }
            if ((closestDistance < 0.5f) && (GetTeamLeft()[index].GetComponent<Charactor>().action != State.move))
                return GetTeamLeft()[index];
        }
        else
        {
            for (int i = 0; i < GetTeamRight().Count; i++)
            {
                if (ReferenceEquals(GetTeamRight()[i], this.gameObject)) continue;
                if (GetTeamRight()[i].tag.Equals("Building")) continue;
                distance = Vector3.Distance(transform.position, GetTeamRight()[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    index = i;
                }
            }
            if (closestDistance < 0.5f && GetTeamRight()[index].GetComponent<Charactor>().action != State.move)
                return GetTeamRight()[index];
        }
        return null;


    }
    public List<GameObject> GetTeamRight()
    {
        return GameManager.instance.teamRight;
    }
    public List<GameObject> GetTeamLeft()
    {
        return GameManager.instance.teamLeft;
    }

    GameObject GetNearestEnemy(Team team)
    {
        int index = 0;
        float distance, closestDistance;
        if (team == Team.left)
        {
            if (GameManager.instance.teamRight.Count == 0 || GameManager.instance.teamRight == null)
            {
                Debug.LogWarning("Team Right List is Empty!");
                return null;
            }
            closestDistance = Vector3.Distance(transform.position, GameManager.instance.teamRight[0].transform.position);

            for (int i = 0; i < GameManager.instance.teamRight.Count; i++)
            {
                distance = Vector3.Distance(transform.position, GameManager.instance.teamRight[i].transform.position);
                if (distance < closestDistance)
                {
                    index = i;
                    closestDistance = distance;
                }
            }
            if (closestDistance <= attackdistance)
            {
                return GameManager.instance.teamRight[index];
            }
        }
        else
        {
            if (GameManager.instance.teamLeft.Count == 0 || GameManager.instance.teamLeft == null)
            {
                Debug.LogWarning("Team Right List is Empty!");
                return null;
            }
            
            closestDistance = Vector3.Distance(transform.position, GameManager.instance.teamLeft[0].transform.position);
            for (int i = 0; i < GameManager.instance.teamLeft.Count; i++)
            {
                distance = Vector3.Distance(transform.position, GameManager.instance.teamLeft[i].transform.position);
                if (distance < closestDistance)
                {
                    index = i;
                    closestDistance = distance;
                }
            }
            if (closestDistance <= attackdistance)
            {
                return GameManager.instance.teamLeft[index];
            }
        }
        return null;
    }
    /*
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
    }*/

    public void Die()
    {
        GameManager.instance.exp += myInfo.deadEXP;
        animator.SetTrigger("Die");
        StartCoroutine(DieFadeOut());
        if (myInfo.team == Team.left)
        {
            bool remove = GameManager.instance.teamLeft.Remove(gameObject);
            Debug.Log(remove.ToString());
        }
        else GameManager.instance.teamRight.Remove(gameObject);
        Destroy(gameObject, dieFadeOutTime);

    }

    public void AttackBuilding()
    {
        if (myInfo.team == Team.left)
        {
            GameManager.instance.BuildingRight.HP -= myInfo.ATK;
        }
        else GameManager.instance.BuildingLeft.HP -= myInfo.ATK;
    }
    
    public void ApplyDemage()
    {
        attackTargetInfo.HP -= myInfo.ATK;
        if (attackTargetInfo.HP <= 0)
        {
            attackTarget.SendMessage("Die");
            attackTarget = null;
            attackTargetInfo = null;
        }
    }

    public void ReadyForAttack()
    {
        if (attackTarget != null)
        {
            isAttacking = true;
            action = State.attack;
            myInfo.attackCooldown += Time.deltaTime;
            if (attackTarget.tag.Equals("Building")) //건물 공격 상태
            {
                if (myInfo.attackCooldown >= myInfo.AttackTick)
                {
                    AttackBuilding();
                    myInfo.ResetAttackCooldown();
                }
            }
            else // 적 캐릭터 공격 상태
            {
                attackTargetInfo = attackTarget.GetComponent<CharactorInfo>();
                if (myInfo.attackCooldown >= myInfo.AttackTick)
                {

                    ApplyDemage();
                    myInfo.ResetAttackCooldown();
                }
            }
        }
        else
        {
            isAttacking = false;
            animator.SetBool("Attacking", false);
            action = State.move;
        }
    }

    /*
    public IEnumerator ApplyDemage()
    {
        while (true)
        {

            yield return new WaitForSeconds(myInfo.AttackTick);
            

            //적 사망
            if (attackTargetInfo.HP <= 0)
            {
                //TODO 
                action = State.move;

                attackTarget.SendMessage("Die");

                attackTarget = null;
                attackTargetInfo = null;

                animator.SetBool("Attacking", false);
                Debug.Log("죽어라!1");
                break;
            }
            else
            {
                attackTargetInfo.HP -= myInfo.ATK;
            }
        }
    }
    */
    protected IEnumerator DieFadeOut()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Color newColor = renderer.color;
        while (newColor.a > 0)
        {
            float fadeAmount = newColor.a - (dieFadeOutTime * Time.deltaTime);
            newColor = new Color(newColor.r, newColor.g, newColor.b, fadeAmount);
            renderer.color = newColor;
            yield return null;
        }
    }

    public void Stop()
    {
        animator.SetBool("Attacking", false);
    }


}
