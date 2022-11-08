using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

public class Enemy : MonoBehaviour
{
    //Serialized variables
    [Header("speed of the enemy")]
    [SerializeField]private float speed;

    //None serialized variables
    private int hp;
    private int damage;

    //Components
    private GameObject player;
    private Rigidbody2D rb;

    private void Awake() {
        hp = 100;
        damage = 5;

        rb = GetComponent<Rigidbody2D>();
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

    private void hit(int i_damage){
        hp -= i_damage;
        if(hp < 1){
            MapManager.map.remove_enemy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.transform.CompareTag("Light Proyectile") || other.transform.CompareTag("Dark Proyectile")){
            hit(other.transform.GetComponent<Ability>().get_damage());
            Destroy(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        if(other.transform.CompareTag("Black Hole")){
            transform.position = other.transform.position;
            hit(other.transform.GetComponent<Ability>().get_damage());
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.transform.CompareTag("Light Burst")){
            hit(other.transform.GetComponent<Ability>().get_damage());
        }
    }

    public int get_damage(){
        return damage;
    }
}
