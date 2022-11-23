using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Health))]

public class Enemy : MonoBehaviour
{
    //Serialized variables
    [Header("speed of the enemy")]
    [SerializeField]private float speed;

    //None serialized variables
    private int damage;

    //Components
    private GameObject player;
    private Rigidbody2D rb;
    private Health health;

    private void Awake() {
        damage = 5;

        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        health.on_death += enemy_death;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player != null){
            Vector2 player_pos = player.transform.position;
            Vector2 enemy_pos = transform.position;

            Vector2 direction = player_pos - enemy_pos;
            rb.velocity = direction * speed / direction.magnitude;

            float angle = calculate_angle(player_pos, enemy_pos);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private float calculate_angle(Vector2 v1, Vector2 v2){
        return Mathf.Atan2(v1.y - v2.y, v1.x - v2.x) * Mathf.Rad2Deg;
    }

    private void enemy_death(){
        MapManager.map_instance.on_enemy_killed.Invoke(this);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Light Proyectile") || other.transform.CompareTag("Dark Proyectile")){
            health.hit(other.transform.GetComponent<Ability>().get_damage());
            Destroy(other.gameObject);
        }else if(other.transform.CompareTag("Shadow Dash")){
            health.hit(other.transform.GetComponent<Ability>().get_damage());
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.transform.CompareTag("Black Hole")){
            transform.position = other.transform.position;
            health.hit(other.transform.GetComponent<Ability>().get_damage());
        }else if(other.transform.CompareTag("Light Beam")){
            health.hit(other.transform.GetComponent<Ability>().get_damage());
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.transform.CompareTag("Light Burst")){
            health.hit(other.transform.GetComponent<Ability>().get_damage());
        }
    }

    public int get_damage(){
        return damage;
    }
}
