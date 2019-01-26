using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControllGameMap : MonoBehaviour
{

    private InputField tank_deploy, tank_attacker, tank_defender, tank_move;
    private Dropdown deployTerritory, land_attacker, land_defender, land_start, land_end;
    //da cancellare/modificare quando viene applicato i singleton delle classi
    public ModelGameMap model;

     private void Awake()//prepara i componenti da cui prendere gli input
    {
        //InputField
        tank_deploy = GameObject.Find("InputField_Deploy").GetComponent<InputField>(); 
        tank_attacker = GameObject.Find("InputField_attacker").GetComponent<InputField>();
        tank_defender = GameObject.Find("InputField_defender").GetComponent<InputField>(); 
        tank_move = GameObject.Find("InputField_move").GetComponent<InputField>();


        //Dropdown
        deployTerritory = GameObject.Find("Dropdown_Deploy").GetComponent<Dropdown>(); 
        land_attacker = GameObject.Find("Dropdown_attacker").GetComponent<Dropdown>(); 
        land_defender = GameObject.Find("Dropdown_defender").GetComponent<Dropdown>(); 
        land_start = GameObject.Find("Dropdown_start").GetComponent<Dropdown>(); 
        land_end = GameObject.Find("Dropdown_end").GetComponent<Dropdown>(); 
    }

    // metodi dei button

    public void onClickDeploy()
    {
        model.deploy(deployTerritory.options[deployTerritory.value].text, tank_deploy.text);
    }

    public void onClickAttack()
    {
        model.attack(land_attacker.options[land_attacker.value].text, land_defender.options[land_defender.value].text,
                     tank_attacker.text, tank_defender.text);
    }

    public void onClickMove()
    {
        model.move(land_start.options[land_start.value].text, land_end.options[land_end.value].text, tank_move.text);
    }

    public void onClickPassTurn(){
        model.pass();
    }

    // metodi dropdown

    // metodi degli inputfield

    


}