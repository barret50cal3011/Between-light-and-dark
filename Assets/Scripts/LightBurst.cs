using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Ability))]
public class LightBurst : MonoBehaviour
{
    //Serialixed variables
    [SerializeField] private float total_time;
    [SerializeField] private float expansion_rate;

    //None serialized variables
    private float time;

    //Components
    private Ability ability;

    private void Awake() {
        ability = GetComponent<Ability>();
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time <= total_time){
            transform.localScale += new Vector3(expansion_rate, expansion_rate, 0) * Time.deltaTime;
        }else{
            Destroy(gameObject);
        }
        
    }
}
