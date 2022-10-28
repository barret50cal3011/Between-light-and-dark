using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerControler : MonoBehaviour
{

    //variables
    [Header("Speed of the player")]
    [SerializeField] private float speed;

    //components
    private Rigidbody2D rb;
    private PlayerInput player_input;

    //Actions
    private InputAction move_action;
    private InputAction aim_action; 

    void Awake(){
        rb = GetComponent<Rigidbody2D>();
        player_input = GetComponent<PlayerInput>();

        move_action = player_input.actions["Move"];
        aim_action = player_input.actions["Aim"];
    }
    // Start is called before the first frame update
    void Start()
    {
        move_action.performed += move;
        move_action.canceled += stop_movement;
    } 

    // Update is called once per frame
    void Update()
    {
        aim(aim_action.ReadValue<Vector2>());
    }

    private void OnDisable() {
        move_action.performed -= move;
        move_action.canceled -= stop_movement;
    }

    //moves the player
    private void move(InputAction.CallbackContext context){
        rb.velocity = context.ReadValue<Vector2>() * speed;
    }

    //stops the movement of the player
    private void stop_movement(InputAction.CallbackContext context){
        rb.velocity = new Vector2(0,0);
    }

    //Keeps the player aiming at the mouse position
    private void aim(Vector2 mouse_screen_pos){
        Vector2 mouse_pos = Camera.main.ScreenToViewportPoint(mouse_screen_pos);
        Vector2 player_pos = Camera.main.WorldToViewportPoint(transform.position);

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
