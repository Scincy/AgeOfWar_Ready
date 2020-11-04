using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WarriorInfo", menuName = "Create Warrior Info", order = 1)]
public class WarriorInfo : ScriptableObject
{
    public float HP;
    public float ATK;
    public float attackTick;
    public float speed;
    public float spawnTick;
    public int id;
    public int deadEXP;
}
