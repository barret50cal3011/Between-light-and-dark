using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    [Header("Mana generated per second")]
    [SerializeField] private float mana_generation;

    //gui variables
    private Slider health_slider;
    private Slider dark_mana_slider;
    private Slider light_mana_slider;

    private int hp;
    private float light_mana;
    private float dark_mana;

    private void Awake() {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        health_slider = GameObject.Find("Health_Slider").GetComponent<Slider>();
        dark_mana_slider = GameObject.Find("Dark_Mana_Slider").GetComponent<Slider>();
        light_mana_slider = GameObject.Find("Light_Mana_Slider").GetComponent<Slider>();


        set_hp(100);
        set_light_mana(0);
        set_dark_mana(0);
    }

    // Update is called once per frame
    void Update()
    {
        //generate mana
        set_dark_mana(dark_mana + (mana_generation * Time.deltaTime));
        set_light_mana(light_mana + (mana_generation * Time.deltaTime));

        //Debug.Log("Light: " + light_mana);
        //Debug.Log("Dark: " + dark_mana);
    }

    private void set_hp(int i_hp){
        hp = i_hp;
        health_slider.value = hp;
    }

    public void set_light_mana(float i_light_mana){
        light_mana = i_light_mana;
        light_mana_slider.value = light_mana;
    }

    public void set_dark_mana(float i_dark_mana){
        dark_mana = i_dark_mana;
        dark_mana_slider.value = dark_mana;
    }

    public float get_light_mana(){
        return light_mana;
    }

    public float get_dark_mana(){
        return dark_mana;
    }

    public int get_hp(){
        return hp;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.CompareTag("Enemy")){
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            set_hp(hp - enemy.get_damage());
        }
    }
}
