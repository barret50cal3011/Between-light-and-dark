using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Proyectile : MonoBehaviour
{
    //info
    private int damage;
    private int speed;
    private float destruction_distance;

    //Components
    private Rigidbody2D rb;
    
    //Player
    private GameObject player;

    private void Awake() {
        damage = 10;
        speed = 15;
        destruction_distance = 18;

        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if((transform.position - player.transform.position).sqrMagnitude  > destruction_distance * destruction_distance){
            Destroy(gameObject);
        }
    }

    public int get_damage(){
        return damage;
    }

    public void set_velocity(Vector2 i_vel){
        Vector2 unit_vel = (i_vel/i_vel.magnitude);
        rb.velocity = unit_vel*speed;
    }
}
