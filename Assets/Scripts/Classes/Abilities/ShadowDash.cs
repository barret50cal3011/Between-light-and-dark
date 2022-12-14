using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Ability))]
public class ShadowDash : MonoBehaviour, IAbility
{
    [SerializeField] private float speed;
    [SerializeField] private float dash_time;

    private Collider2D player_collider;
    private PlayerControler player_controler;
    private Rigidbody2D player_rb;
    private float time;
    private Vector2 direction;

    private void Awake() {
        time = 0;
        direction = new Vector2(0,0);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        player_collider = player.GetComponent<Collider2D>();
        player_controler = player.GetComponent<PlayerControler>();
        player_rb = player.GetComponent<Rigidbody2D>();

        player_rb.velocity = calculate_velocity();

        player_collider.isTrigger = true;
        player_controler.dash_activated();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(dash_time > time){
            
        }else{
            player_collider.isTrigger = false;
            player_controler.dash_ended();
            Destroy(gameObject);
            player_rb.velocity = new Vector2(0,0);
        }
    }

    private Vector2 calculate_velocity(){
        return direction * speed;
    }

    public void set_direction(Vector2 i_direction)
    {
        direction = i_direction;
    }

    public Ability instantiate_ability(Transform player, GameObject prefab){
        Vector2 mouse_on_screen = player.GetComponent<PlayerControler>().get_aim();
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(mouse_on_screen);
        Vector2 direction = mouse_pos - (Vector2)player.position;

        GameObject shadow_dash = Instantiate(prefab, player);
        shadow_dash.GetComponent<ShadowDash>().set_direction(direction / direction.magnitude);

        return shadow_dash.GetComponent<Ability>();
    }
}
