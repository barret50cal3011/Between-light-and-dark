using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ability))]
[RequireComponent(typeof(Collider2D))]
public class BlackHole : MonoBehaviour , IAbility
{
    //Serialized variables
    [SerializeField] private float expansion_rate;
    [SerializeField] private float expansion_time;
    [SerializeField] private float static_time;

    //None serialized variable
    private float time;
    private bool is_static;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        is_static = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(is_static && time >= static_time)
        {
            Destroy(gameObject);
        }else if(!is_static && time >= expansion_time)
        {
            is_static = true;
        }else if(!is_static && time < expansion_time){
            transform.localScale += new Vector3(expansion_rate, expansion_rate, 0) * Time.deltaTime;
        }
    }

    public Ability instantiate_ability(Transform player, GameObject prefab){
        Vector2 mouse_on_screen = player.GetComponent<PlayerControler>().get_aim();
        Vector2 world_pos = Camera.main.ScreenToWorldPoint(mouse_on_screen);
        GameObject bh = Instantiate(prefab, world_pos, player.rotation);

        return bh.GetComponent<Ability>();
    }
}
