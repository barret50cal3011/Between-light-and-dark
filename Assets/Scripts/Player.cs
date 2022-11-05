using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //gui variables
    private Slider health_slider;
    private Slider dark_mana_slider;
    private Slider light_mana_slider;

    private int hp;
    private int light_mana;
    private int dark_mana;

    private void Awake() {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        health_slider = GameObject.Find("Health_Slider").GetComponent<Slider>();
        dark_mana_slider = GameObject.Find("Dark_Mana_Slider").GetComponent<Slider>();
        light_mana_slider = GameObject.Find("Light_Mana_Slider").GetComponent<Slider>();

        update_hp(100);
        update_light_mana(0);
        update_dark_mana(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void update_hp(int i_hp){
        hp = i_hp;
        health_slider.value = hp;
    }

    private void update_light_mana(int i_light_mana){
        light_mana = i_light_mana;
        light_mana_slider.value = light_mana;
    }

    private void update_dark_mana(int i_dark_mana){
        dark_mana = i_dark_mana;
        dark_mana_slider.value = dark_mana;
    }

    public int get_hp(){
        return hp;
    }

    public int get_light_mana(){
        return light_mana;
    }

    public int get_dark_mana(){
        return dark_mana;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.CompareTag("Enemy")){
            Debug.Log("I was hit :(");
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            update_hp(hp - enemy.get_damage());
        }
    }
}
