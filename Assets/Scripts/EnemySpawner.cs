using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Serialized variables
    [Header("Enemies that can be spawn")]
    [SerializeField]private List<GameObject> enemies;

    //None serialized variables
    private GameObject player;
    private List<GameObject> enemies_in_map;

    private void Awake() {
        enemies_in_map = new List<GameObject>();
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
            create_enemy(enemies[0]);
        }
    }

    private void create_enemy(GameObject enemy){   
        float angle = Random.Range(0, 359) * Mathf.Deg2Rad;
        Vector3 position = (new Vector3(Mathf.Cos(angle),Mathf.Sin(angle), 0)) * 10;
        position += player.transform.position;
        enemies_in_map.Add(Instantiate(enemy, position, Quaternion.identity));
    }
}
