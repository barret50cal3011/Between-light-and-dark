using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Collider2D))]

public class PlayerControler : MonoBehaviour
{

    //Serialized variables
    [Header("Speed of the player")]
    [SerializeField] private float speed;

    [Header("attaks")]
    [SerializeField]private GameObject light_basic;
    [SerializeField]private GameObject dark_basic;
    [SerializeField]private GameObject light_special1;
    [SerializeField]private GameObject dark_special1;
    [SerializeField]private GameObject light_special2;
    [SerializeField]private GameObject dark_special2;

    //None serialized variales 
    private bool special1_is_active;
    private bool special2_is_active;

    //components
    private Rigidbody2D rb;
    private PlayerInput player_input;
    private Player player;

    //Actions
    private InputAction move_action;
    private InputAction aim_action; 

    private InputAction light_action;
    private InputAction dark_action;

    private InputAction special1_action;
    private InputAction special2_action;

    private void Awake(){
        special1_is_active = false;
        special2_is_active = false;

        rb = GetComponent<Rigidbody2D>();
        player_input = GetComponent<PlayerInput>();
        player = GetComponent<Player>();

        move_action = player_input.actions["Move"];
        aim_action = player_input.actions["Aim"];

        light_action = player_input.actions["Light"];
        dark_action = player_input.actions["Dark"];

        special1_action = player_input.actions["Special1"];
        special2_action = player_input.actions["Special2"];
    }

    private void OnEnable() {
        move_action.performed += move;
        move_action.canceled += stop_movement;

        light_action.performed += light;
        dark_action.performed += dark;

        special1_action.performed += special1;
        special2_action.performed += special2;
    }

    // Update is called once per frame
    void Update()
    {
        aim(aim_action.ReadValue<Vector2>());
    }

    private void OnDisable() {
        move_action.performed -= move;
        move_action.canceled -= stop_movement;

        light_action.performed -= light;
        dark_action.performed -= dark;

        special1_action.performed -= special1;
        special2_action.performed -= special2;
    }

    //moves the player
    private void move(InputAction.CallbackContext context){
        rb.velocity = context.ReadValue<Vector2>() * speed;
    }

    //stops the movement of the player
    private void stop_movement(InputAction.CallbackContext context){
        rb.velocity = new Vector2(0,0);
    }

    private void light(InputAction.CallbackContext context){
        if(special1_is_active){
            Debug.Log("light special1");
            special1_is_active = false;
        }else if(special2_is_active){
            Debug.Log("light special2");
            special2_is_active = false;
        }else{
            if(light_basic.GetComponent<Ability>().get_mana_cost() <= player.get_light_mana()){
                GameObject proj = Instantiate(light_basic, transform.position, transform.rotation);

                Vector2 mouse_point = Camera.main.ScreenToWorldPoint(aim_action.ReadValue<Vector2>());
                Vector2 player_point = transform.position;
                Vector2 velocity = mouse_point - player_point;

                proj.GetComponent<Proyectile>().set_velocity(velocity);

                Ability ability = proj.GetComponent<Ability>();
                player.set_light_mana(player.get_light_mana() - ability.get_mana_cost());
            }
        }
    }

    private void dark(InputAction.CallbackContext context){
        if(special1_is_active){
            Debug.Log("dark special1");
            special1_is_active = false;
        }else if(special2_is_active){
            Debug.Log("dakr special2");
            special2_is_active = false;
        }else{
            if(dark_basic.GetComponent<Ability>().get_mana_cost() <= player.get_dark_mana()){
                GameObject proj = Instantiate(dark_basic, transform.position, transform.rotation);

                Vector2 mouse_point = Camera.main.ScreenToWorldPoint(aim_action.ReadValue<Vector2>());
                Vector2 player_point = transform.position;
                Vector2 velocity = mouse_point - player_point;

                proj.GetComponent<Proyectile>().set_velocity(velocity);

                Ability ability = proj.GetComponent<Ability>();
                player.set_dark_mana(player.get_dark_mana() - ability.get_mana_cost());
            }
        }
    }

    private void special1(InputAction.CallbackContext context){
        special1_is_active = !special1_is_active;
        special2_is_active = false;
    }

    private void special2(InputAction.CallbackContext context){
        special2_is_active = !special2_is_active;
        special1_is_active = false;
    }

    //Keeps the player aiming at the mouse position
    private void aim(Vector2 mouse_screen_pos){
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(mouse_screen_pos);
        Vector2 player_pos = transform.position;

        float angle = calculate_angle(mouse_pos, player_pos);

        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        //Stops the camara from moving
        Transform camara = transform.Find("Main Camera");
        camara.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
    }

    private float calculate_angle(Vector2 v1, Vector2 v2){
        return Mathf.Atan2(v1.y - v2.y, v1.x - v2.x) * Mathf.Rad2Deg;
    }

}
