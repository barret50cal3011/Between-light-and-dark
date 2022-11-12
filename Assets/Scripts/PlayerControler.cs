using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Player))]

public class PlayerControler : MonoBehaviour
{

    //Serialized variables
    [Header("Speed of the player")]
    [SerializeField] private float speed;

    [Header("Abilities")]
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

    private bool can_move;

    private void Awake(){
        special1_is_active = false;
        special2_is_active = false;
        can_move = true;

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

        light_action.performed += light_casted;
        dark_action.performed += dark_casted;

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

        light_action.performed -= light_casted;
        dark_action.performed -= dark_casted;

        special1_action.performed -= special1;
        special2_action.performed -= special2;
    }

    //moves the player
    private void move(InputAction.CallbackContext context){
        if(can_move){
            rb.velocity = context.ReadValue<Vector2>() * speed;
        }
    }

    //stops the movement of the player
    private void stop_movement(InputAction.CallbackContext context){
        rb.velocity = new Vector2(0,0);
    }

    private void light_casted(InputAction.CallbackContext context){
        if(special1_is_active){
            float mana_cost = light_special1.GetComponent<Ability>().get_mana_cost();
            if(mana_cost <= player.get_light_mana()){
                Ability.instantiate_ability(transform, light_special1);
                player.set_light_mana(player.get_light_mana() - mana_cost);
            }
            special1_is_active = false;
        }else if(special2_is_active){
            float mana_cost = light_special2.GetComponent<Ability>().get_mana_cost();
            if(mana_cost <= player.get_light_mana()){
                Ability.instantiate_ability(transform, light_special2);
                player.set_light_mana(player.get_light_mana() - mana_cost);
            }
            special2_is_active = false;
        }else{
            float mana_cost = light_basic.GetComponent<Ability>().get_mana_cost();
            if(mana_cost <= player.get_light_mana()){
                Ability.instantiate_ability(transform, light_basic);
                player.set_light_mana(player.get_light_mana() - mana_cost);
            }
        }
    }

    private void dark_casted(InputAction.CallbackContext context){
        if(special1_is_active){
            float mana_cost = dark_special1.GetComponent<Ability>().get_mana_cost();
            if(mana_cost <= player.get_dark_mana()){
                Ability.instantiate_ability(transform, dark_special1);
                player.set_dark_mana(player.get_dark_mana() - mana_cost);
            }
            special1_is_active = false;
        }else if(special2_is_active){
            float mana_cost = dark_special2.GetComponent<Ability>().get_mana_cost();
            if(mana_cost <= player.get_dark_mana()){
                Ability.instantiate_ability(transform, dark_special2);
                player.set_dark_mana(player.get_dark_mana() - mana_cost);
            }
            special2_is_active = false;
        }else{
            float mana_cost = dark_basic.GetComponent<Ability>().get_mana_cost();
            if(mana_cost <= player.get_dark_mana()){
                Ability.instantiate_ability(transform, dark_basic);
                player.set_dark_mana(player.get_dark_mana() - mana_cost);
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

    public void dash_activated(){
        can_move = false;
    }

    public void dash_ended(){
        can_move = true;
    }

    public Quaternion get_aim_direction(){
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(aim_action.ReadValue<Vector2>());
        Vector2 player_pos = transform.position;

        float angle = calculate_angle(mouse_pos, player_pos);

        Quaternion direction = Quaternion.Euler(new Vector3(0f, 0f, angle));
        return direction;
    }

    public Vector2 get_aim(){
        return aim_action.ReadValue<Vector2>();
    }
}
