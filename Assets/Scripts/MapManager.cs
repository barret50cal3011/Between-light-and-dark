using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    //Shared instance
    public static MapManager map;

    [SerializeField] private List<GameObject> enemy_prefabs;
    [SerializeField] private Text text_score;
    
    private GameObject player;
    private List<GameObject> enemies_in_map;
    private int score;

    private void Awake() {
        if(map == null){
            map = this;
        }

        enemies_in_map = new List<GameObject>();

        score = 0;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies_in_map.Count < 10){
            Debug.Log("In the if");
            create_enemy(enemy_prefabs[0]);
        }
    }

    private void create_enemy(GameObject enemy){   
        float angle = Random.Range(0, 359) * Mathf.Deg2Rad;
        Vector3 position = (new Vector3(Mathf.Cos(angle),Mathf.Sin(angle), 0)) * 10;
        position += player.transform.position;

        GameObject new_enemy = Instantiate(enemy, position, Quaternion.identity);
        enemies_in_map.Add(new_enemy);
    }

    public void remove_enemy(GameObject i_enemy){
        enemies_in_map.Remove(i_enemy);
        Destroy(i_enemy);
        score++;
        text_score.text = "Score: " + score;
    }
}
