using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    //Shared instance
    public static MapManager map_instance;

    [SerializeField] private List<GameObject> enemy_prefabs;
    [SerializeField] private Text text_score;
    
    private GameObject player;
    private List<GameObject> enemies_in_map;
    private int score;
    private bool game_ended;

    public Action<Enemy> on_enemy_killed;

    private void Awake() {
        if(map_instance == null){
            map_instance = this;
        }

        enemies_in_map = new List<GameObject>();

        score = 0;
        game_ended = false;

        on_enemy_killed += remove_enemy;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        while(enemies_in_map.Count < 10 && !game_ended){
            
            create_enemy(enemy_prefabs[0]);
        }
    }

    private void create_enemy(GameObject enemy){   
        float angle = UnityEngine.Random.Range(0, 359) * Mathf.Deg2Rad;
        Vector3 position = (new Vector3(Mathf.Cos(angle),Mathf.Sin(angle), 0)) * 10;
        position += player.transform.position;

        GameObject new_enemy = Instantiate(enemy, position, Quaternion.identity);
        enemies_in_map.Add(new_enemy);
    }

    private void end_game(){
        game_ended = true;
        while(enemies_in_map.Count > 0){
            GameObject enemy = enemies_in_map[0];
            enemies_in_map.Remove(enemy);
            Destroy(enemy);
        }
    }

    public void remove_enemy(Enemy i_enemy){
        enemies_in_map.Remove(i_enemy.gameObject);
        Destroy(i_enemy.gameObject);
        score++;
        text_score.text = "Score: " + score;
    }

    public void player_died(){
        end_game();
    }
}
