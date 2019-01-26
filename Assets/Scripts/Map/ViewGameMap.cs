using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ViewGameMap : MonoBehaviour
{


	private Text data_player, error, player;
    private InputField tank_deploy, tank_attacker, tank_defender, tank_move;
    private Button deploy, attack, move, passTurn;
    private Dropdown deployTerritory, land_attacker, land_defender, land_start, land_end;

    private void Awake()//prepara l'interfaccia
    {
    	//Text
        data_player = GameObject.Find("Text_Data").GetComponent<Text>(); 
        error = GameObject.Find("Text_other").GetComponent<Text>(); 
        player = GameObject.Find("Text_Player").GetComponent<Text>();

        data_player.text = ""; 
        error.text = "";
        player.text = "E' il turno di ";
    	
	    //InputField

        tank_deploy = GameObject.Find("InputField_Deploy").GetComponent<InputField>(); 
        tank_attacker = GameObject.Find("InputField_attacker").GetComponent<InputField>();
        tank_defender = GameObject.Find("InputField_defender").GetComponent<InputField>(); 
        tank_move = GameObject.Find("InputField_move").GetComponent<InputField>();

    	tank_deploy.text = "";
        tank_attacker.text = "";
        tank_defender.text = "";
        tank_move.text = "";

  		//Button

        deploy = GameObject.Find("Button_Deploy").GetComponent<Button>(); 
        attack = GameObject.Find("Button_attack").GetComponent<Button>(); 
        move = GameObject.Find("Button_move").GetComponent<Button>(); 
        passTurn = GameObject.Find("Button_pass_turn").GetComponent<Button>();

    	deploy.GetComponentsInChildren<Text>()[0].text = "Aggiungi rinforzi";
        attack.GetComponentsInChildren<Text>()[0].text = "Attacca";
        move.GetComponentsInChildren<Text>()[0].text = "Sposta";
        passTurn.GetComponentsInChildren<Text>()[0].text = "Passa turno";

  		//Dropdown

        deployTerritory = GameObject.Find("Dropdown_Deploy").GetComponent<Dropdown>(); 
        land_attacker = GameObject.Find("Dropdown_attacker").GetComponent<Dropdown>(); 
        land_defender = GameObject.Find("Dropdown_defender").GetComponent<Dropdown>(); 
        land_start = GameObject.Find("Dropdown_start").GetComponent<Dropdown>(); 
        land_end = GameObject.Find("Dropdown_end").GetComponent<Dropdown>(); 

        deployTerritory.ClearOptions();
        land_attacker.ClearOptions();
        land_defender.ClearOptions();
        land_start.ClearOptions();
        land_end.ClearOptions();
    }

    public void loadDropdownList(List<string> options)
    {
        foreach(string option in options)
        {
            addOptionDroppdown(deployTerritory, option);
            addOptionDroppdown(land_attacker, option);
            addOptionDroppdown(land_defender, option);
            addOptionDroppdown(land_start, option);
            addOptionDroppdown(land_end, option);
        }
    }

    public void changeDataPlayer(string data){
    	data_player.text = data;
    }

    public void changeLog(string message){
    	error.text = message;
    }

    public void changePlayer(string name){
    	player.text = "E' il turno di "+ name;
    }

    private void addOptionDroppdown(Dropdown dropdownObject, string optionString){
        Dropdown.OptionData newOption = new Dropdown.OptionData();
        newOption.text = optionString;
        dropdownObject.options.Add(newOption);
        dropdownObject.RefreshShownValue();
    }
}
