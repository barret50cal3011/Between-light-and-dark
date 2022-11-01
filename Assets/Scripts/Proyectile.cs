using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Proyectile : MonoBehaviour
{

    //info
    private int damage;
    
    //Player
    private GameObject player;

    private void Awake() {
        damage = 10;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int get_damage(){
        return damage;
    }
}
