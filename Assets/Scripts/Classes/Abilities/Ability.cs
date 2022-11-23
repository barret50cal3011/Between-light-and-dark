using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float mana_cost;

    public float get_mana_cost(){
        return mana_cost;
    }

    public int get_damage(){
        return damage;
    }
}
