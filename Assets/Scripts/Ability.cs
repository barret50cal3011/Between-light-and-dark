using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float mana_cost;

    public float get_mana_cost(){
        return mana_cost;
    }

    public int get_damage(){
        return damage;
    }

    //Ability instantiation

    //To add an ability, add a case with the ability tag and create 
    //a method that instantiates the new ability.

    //This instantiates all the abilities that the player casts
    public static void instantiate_ability(Transform player, GameObject prefab){
        switch(prefab.tag){
        case "Light Proyectile":
            instantiate_light_projectile(player, prefab);
            break;
        case "Dark Proyectile":
            instantiate_dark_projectile(player, prefab);
            break;
        case "Black Hole":
            instantiate_black_hole(player, prefab);
            break;
        case "Shadow Dash":
            instantiate_shadow_dash(player, prefab);
            break;
        case "Light Burst":
            instantiate_light_burst(player, prefab);
            break;
        case "Light Beam":
            instantiate_light_beam(player, prefab);
            break;
        }
    }

    //Methods that are used to instaniate a especific ability
    private static void instantiate_light_projectile(Transform player, GameObject prefab){
        GameObject proj = Instantiate(prefab, player.position, player.rotation);

        Vector2 aim = player.GetComponent<PlayerControler>().get_aim();
        Vector2 mouse_point = Camera.main.ScreenToWorldPoint(aim);
        Vector2 player_point = player.position;
        Vector2 velocity = mouse_point - player_point;

        proj.GetComponent<Proyectile>().set_velocity(velocity);
    }

    private static void instantiate_dark_projectile(Transform player, GameObject prefab){
        GameObject proj = Instantiate(prefab, player.position, player.rotation);

        Vector2 mouse_on_screen = player.GetComponent<PlayerControler>().get_aim();
        Vector2 mouse_point = Camera.main.ScreenToWorldPoint(mouse_on_screen);
        Vector2 player_point = player.position;
        Vector2 velocity = mouse_point - player_point;

        proj.GetComponent<Proyectile>().set_velocity(velocity);
    }

    private static void instantiate_black_hole(Transform player, GameObject prefab){
        Vector2 mouse_on_screen = player.GetComponent<PlayerControler>().get_aim();
        Vector2 world_pos = Camera.main.ScreenToWorldPoint(mouse_on_screen);
        Instantiate(prefab, world_pos, player.rotation);
    }

    private static void instantiate_shadow_dash(Transform player, GameObject prefab){
        Vector2 mouse_on_screen = player.GetComponent<PlayerControler>().get_aim();
        Vector2 mouse_pos = Camera.main.ScreenToWorldPoint(mouse_on_screen);
        Vector2 direction = mouse_pos - (Vector2)player.position;

        Instantiate(prefab, player).GetComponent<ShadowDash>().set_direction(direction / direction.magnitude);
    }

    private static void instantiate_light_burst(Transform player, GameObject prefab){
        Instantiate(prefab, player.position, player.rotation);
    }

    private static void instantiate_light_beam(Transform player, GameObject prefab){
        Instantiate(prefab, player);
    } 
}
