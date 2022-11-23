using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Breakable : MonoBehaviour
{
    //Serialized variables
    [Header("Default options")]
    [Tooltip("Amount of hits before the object is destroied")]
    [SerializeField] private int hp;
    [Space(10)]

    [Header("Explotion")]
    [Tooltip("Set wether the object explotes on destruction or not")]
    [SerializeField] private bool explodes;
    [Tooltip("Set the size of the explotion")]
    [SerializeField] private float expltion_size;

    private void OnTriggerEnter2D(Collider2D other) {
        hp--;
        if(hp <= 0){
            destroy();
        }
    }

    private void destroy(){
        //TODO
        Destroy(gameObject);
    }
}
