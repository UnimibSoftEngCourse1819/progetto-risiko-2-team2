using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControllCreateMap : MonoBehaviour
{

	/*
	MODIFICHE DA EFFETTUARE:
		->APPLICARE IL SINGLETON :
		 http://csharpindepth.com/articles/general/singleton.aspx (implementate la quarta versione che è il più semplice)
		 dopo di chè aggiornare i file che la usano 
	*/

	private InputField name_continent, name_land;
	private Dropdown continent, neighbor_right, neighbor_left, choose_land;

	//da cancellare/modificare quando viene applicato i singleton delle classi
	public ModelCreateMap model;

	 private void Awake()//prepara i componenti da cui prendere gli input
    {
	    //InputField
    	name_continent = GameObject.Find("InputField_Name_Continent").GetComponent<InputField>();
    	name_land = GameObject.Find("InputField_Name_Land").GetComponent<InputField>();

  		//Dropdown
    	continent = GameObject.Find("Dropdown_Continent").GetComponent<Dropdown>(); 
    	neighbor_right = GameObject.Find("Dropdown_Neighbor_Right").GetComponent<Dropdown>(); 
    	neighbor_left = GameObject.Find("Dropdown_Neighbor_Left").GetComponent<Dropdown>(); 
    	choose_land = GameObject.Find("Dropdown_Choose_Land").GetComponent<Dropdown>();
    }

    // metodi dei button

    public void onClickCreateContinent()
    {
    	model.createContinent(name_continent.text);
    }

    public void onClickCreateLand()
    {
    	model.createLand(name_land.text, continent.options[continent.value].text);
    }

    public void onClickCreateRelationship()
    {
    	model.createRelation(neighbor_left.options[neighbor_left.value].text, neighbor_right.options[neighbor_right.value].text);
    }

    // metodi dropdown

    public void onChangeOptionLand()
    {
    	model.loadLandInfo(choose_land.options[choose_land.value].text);
    }

    // metodi degli inputfield

    


}