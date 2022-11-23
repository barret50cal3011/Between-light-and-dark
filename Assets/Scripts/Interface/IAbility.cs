using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    public Ability instantiate_ability(Transform player, GameObject prefab);
}
