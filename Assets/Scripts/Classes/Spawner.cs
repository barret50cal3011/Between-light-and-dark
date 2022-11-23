using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public void spawn_enemy(GameObject enemy){
        Instantiate(enemy, transform.position, transform.rotation);
    }
}
