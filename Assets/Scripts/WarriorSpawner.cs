using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WarriorSpawner : Spawner
{       
    void Start()
    {
    }

    public override void SetCharactorInfo(int id)
    {
        
        CharactorInfo target = warrior.GetComponent<CharactorInfo>();
        target.ATK = warriorinfo[id].ATK + GameManager.instance.ATKupAmount * GameManager.instance.level;
        target.HP = warriorinfo[id].HP + GameManager.instance.HPupAmount * GameManager.instance.level;
        target.speed = warriorinfo[id].speed;
        target.AttackTick = warriorinfo[id].attackTick;
        target.AttackTick = warriorinfo[id].attackTick;
        spawnTick = warriorinfo[id].spawnTick;
        target.team = Team.left;
        target.id = id;
        target.level = GameManager.instance.level;
        GameManager.instance.teamLeft.Add(warrior);
    }
    /*
    public void Spawn(int id)
    {
        
        
        Debug.Log(id+"was Spawned!");
        

        //TODO
        //팀 정보를 삽입합니다.
        if (spawnDirection == Warrior.MoveWay.left)
        {
            GameManager.instance.teamLeft.Add(warrior);
        }
        else
        {
            GameManager.instance.teamRight.Add(warrior);
        }
        
        warriorSetting = warrior.GetComponent<Warrior>();
        warriorSetting.moveDirection = spawnDirection;
        warriorSetting.HP = warriorinfo[id].HP;// + (GameManager.instance.level * GameManager.instance.HPupAmount);
        warriorSetting.ATK = warriorinfo[id].ATK;// + (GameManager.instance.level * GameManager.instance.ATKupAmount);
        warriorSetting.AttackTick = warriorinfo[id].attackTick;
        warriorSetting.speed = warriorinfo[id].speed
        // 워리어를 스폰 장소에 배치
        // 워리어 활성화
        // 활성화되면서 스탯 증가량 * level + (기본 스텟량 능력치) 부여
        // 워리어 팀 배정(warriorInfo.moveDirection = spawnDirection;)
        // 



        
        warrior = Instantiate(warriorPrefab[id], transform.position, Quaternion.identity);
        
        warriorInfo = warrior.GetComponent<Warrior>();
        warriorInfo.moveDirection = spawnDirection;
        

    }*/

    /*
    IEnumerator AISpawn()
    {

    }
    */
}
