using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
	private readonly int[] pattern = { 0, 0 };

	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine("AutoSpawn");
	}

	IEnumerator AutoSpawn()
    {
		int patternIndex = 0;
		while (true)
        {
			Debug.Log("Pat:"+patternIndex+ "/" + pattern.Length);
			yield return new WaitForSeconds(warriorinfo[pattern[patternIndex]].spawnTick);
            if (Random.Range(0,100) < 90) //45%확률로 소환
            {
				Spawn(pattern[patternIndex]);
				patternIndex++;
                if (patternIndex >= pattern.Length)
                {
					patternIndex = 0;
                }
            }
        }
    }


	void GameEnded()
    {
		StopCoroutine("AutoSpawn");
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
		target.team = Team.right;
		target.deadEXP = GameManager.instance.EXPupAmount * GameManager.instance.level + warriorinfo[id].deadEXP;
		target.id = id;
		target.level = GameManager.instance.level;
		GameManager.instance.teamRight.Add(warrior);
	}
}
