using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Ability))]
public class Proyectile : MonoBehaviour, IAbility
{
    //info
    private int speed;
    private float destruction_distance;

    //Components
    private Rigidbody2D rb;
    
    //Player
    private GameObject player;

    private void Awake() {
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

    public void set_velocity(Vector2 i_vel){
        Vector2 unit_vel = (i_vel/i_vel.magnitude);
        rb.velocity = unit_vel*speed;
    }

    public Ability instantiate_ability(Transform player, GameObject prefab){
        GameObject proj = Instantiate(prefab, player.position, player.rotation);

        Vector2 aim = player.GetComponent<PlayerControler>().get_aim();
        Vector2 mouse_point = Camera.main.ScreenToWorldPoint(aim);
        Vector2 player_point = player.position;
        Vector2 velocity = mouse_point - player_point;

        proj.GetComponent<Proyectile>().set_velocity(velocity);

        return proj.GetComponent<Ability>();
    }
}
