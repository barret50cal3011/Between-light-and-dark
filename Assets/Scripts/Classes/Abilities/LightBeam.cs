using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Ability))]
public class LightBeam : MonoBehaviour, IAbility
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

    public Ability instantiate_ability(Transform player, GameObject prefab){
        GameObject light_beam = Instantiate(prefab, player);
        return light_beam.GetComponent<Ability>();
    }
}
