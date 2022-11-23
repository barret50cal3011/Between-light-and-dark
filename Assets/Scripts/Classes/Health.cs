using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    //Health of the object  when it is at full health
    [SerializeField] private float max_health;
    [SerializeField] private bool is_player;

    //Silider representing the health. 
    //Can be null
    private Slider health_slider;

    //Health of the object
    private float health;

    //Action event 
    public Action on_death;

    //sets initial health;
    private void Awake() {
        health = max_health;
    }

    private void Start() {
        if(is_player){
            health_slider = GameObject.Find("Health_Slider").GetComponent<Slider>();
            set_slider();
        }else{
            health_slider = null;
        }
    }

    //sets the silder info, if ther is a slider
    //DO NOT INVOKE IF THE SLIDER DOES NOT EXIST
    private void set_slider(){ 
        health_slider.maxValue = max_health;
        health_slider.minValue = 0;
        health_slider.value = health;
    }

    //removes the damage done from the health
    public void hit(float i_damage){
        health -= i_damage;

        if(health_slider != null){
            health_slider.value = health;
        }

        if(health <= 0){
            on_death?.Invoke();
        }
    }

    public float get_health(){
        return health;
    }    
}
