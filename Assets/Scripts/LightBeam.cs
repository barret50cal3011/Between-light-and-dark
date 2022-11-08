using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Ability))]
public class LightBeam : MonoBehaviour
{
    [SerializeField]private float total_time;

    private float time;

    private void Awake() {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > total_time){
            Destroy(gameObject);
        }
    }
}
