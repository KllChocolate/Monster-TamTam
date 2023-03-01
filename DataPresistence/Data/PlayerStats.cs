using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public int playerId;
    public float strength;
    public float agility;
    public float dexterity;
    public float intelligent;
    public float currentHp;
    public float currentFood;
    public Vector3 position;

    public PlayerStats(int Id,float str, float agi, float dex, float intel, float hp, float food, Vector3 pos)
    {
        playerId = Id;
        strength = str;
        agility = agi;
        dexterity = dex;
        intelligent = intel;
        currentHp = hp;
        currentFood = food;
        position = pos;
    }
}