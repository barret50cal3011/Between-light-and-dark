using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    //Components
    private Health health;

    private void Awake() {
        health = GetComponent<Health>();

        health.on_death += player_killed;
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.CompareTag("Enemy")){
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            health.hit(enemy.get_damage());

            Debug.Log(health.get_health());
        }
    }

    private void player_killed(){
        Debug.Log("Death");
    }
}
