using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ManaType{
    Light,
    Dark
}

public class Mana : MonoBehaviour
{
    [Header("Mana information")]
    [Tooltip("Type of mana")]
    [SerializeField] private ManaType type;
    [Tooltip("Generation rate of mana per second")]
    [SerializeField] private float generation_rate;
    [Tooltip("Maximum mana")]
    [SerializeField] private float max_mana;
    [Tooltip("Startign mana amount")]
    [SerializeField] private float starting_mana;

    private Slider slider;
    private float mana_amount;

    private void Awake() {
        mana_amount = starting_mana;
    }

    private void Start() {
        if(type == ManaType.Dark){
            slider = GameObject.Find("Dark_Mana_Slider").GetComponent<Slider>();
        }else if(type == ManaType.Light){
            slider = GameObject.Find("Light_Mana_Slider").GetComponent<Slider>();
        }
        if(slider != null){
            slider.value = mana_amount;
            slider.maxValue = max_mana;
            slider.minValue = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(mana_amount < max_mana){
            mana_amount += generation_rate*Time.deltaTime;
            update_slider();
        }
    }

    public float get_mana_amount(){
        return mana_amount;
    }

    public void ability_used(float i_mana){
        mana_amount -= i_mana;
        update_slider();
    }

    private void update_slider(){
        if(slider != null){
            slider.value = mana_amount;
        }
    }

    public bool can_use_ability(float i_mana_cost){
        return mana_amount >= i_mana_cost;
    }

    public ManaType get_type(){
        return type;
    }
}
