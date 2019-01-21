using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ViewCreateMap : MonoBehaviour
{

	/*
	MODIFICHE DA EFFETTUARE:
		->APPLICARE IL SINGLETON :
		 http://csharpindepth.com/articles/general/singleton.aspx (implementate la quarta versione che è il più semplice)
		 dopo di chè aggiornare i file che la usano 
	*/


	private Text continent_title, continent_list, continent_name, continent_propriety,
    	land_title, land_name, land_description, land_choose,
    	neighbor_title;
    private InputField name_continent, name_land, name_map;
    private Button create_continent, create_relation, create_land, create_file;
    private Dropdown continent, neighbor_right, neighbor_left, choose_land;

    //da cancellare/modificare quando viene applicato i singleton delle classi
	public ModelCreateMap model;

    private void Awake()//prepara l'interfaccia
    {
    	//Text
    	continent_title = GameObject.Find("Text_Continent_Title").GetComponent<Text>(); 
    	continent_list = GameObject.Find("Text_Continent_List").GetComponent<Text>(); 
    	continent_name = GameObject.Find("Text_Continent_Name").GetComponent<Text>(); 
    	continent_propriety = GameObject.Find("Text_Continent_Propriety").GetComponent<Text>();
    	land_title = GameObject.Find("Text_Land_Title").GetComponent<Text>(); 
    	land_name = GameObject.Find("Text_Land_Name").GetComponent<Text>(); 
    	land_description = GameObject.Find("Text_Land_Description").GetComponent<Text>(); 
    	land_choose = GameObject.Find("Text_Land_Choose").GetComponent<Text>();
    	neighbor_title = GameObject.Find("Text_Neighbor_Title").GetComponent<Text>();

    	continent_title.text = "Edita Continente";
    	continent_list.text = ""; 
    	continent_name.text = "Nome :";
    	continent_propriety.text = "Continente :";
    	land_title.text = "Edita Territorio";  
    	land_name.text = "Nome :"; 
    	land_description.text = ""; 
    	land_choose.text = "Territorio :"; 
    	neighbor_title.text = "Edita Vicini"; 

	    //InputField
    	name_continent = GameObject.Find("InputField_Name_Continent").GetComponent<InputField>();
    	name_land = GameObject.Find("InputField_Name_Land").GetComponent<InputField>();
        name_map = GameObject.Find("InputField_Name_Map").GetComponent<InputField>();

    	name_continent.text = "";
    	name_land.text = "";
        name_map.text = "Nome mappa";

  		//Button
    	create_continent = GameObject.Find("Button_Create_Continent").GetComponent<Button>();
    	create_relation = GameObject.Find("Button_Create_Relation").GetComponent<Button>();
    	create_land = GameObject.Find("Button_Create_Land").GetComponent<Button>();
    	create_file = GameObject.Find("Button_Create_File").GetComponent<Button>();

    	create_continent.GetComponentsInChildren<Text>()[0].text = "Crea continente";
    	create_relation.GetComponentsInChildren<Text>()[0].text = "Crea relazione";
    	create_land.GetComponentsInChildren<Text>()[0].text = "Crea stato";
    	create_file.GetComponentsInChildren<Text>()[0].text = "Crea file";

  		//Dropdown
    	continent = GameObject.Find("Dropdown_Continent").GetComponent<Dropdown>(); 
    	neighbor_right = GameObject.Find("Dropdown_Neighbor_Right").GetComponent<Dropdown>(); 
    	neighbor_left = GameObject.Find("Dropdown_Neighbor_Left").GetComponent<Dropdown>(); 
    	choose_land = GameObject.Find("Dropdown_Choose_Land").GetComponent<Dropdown>();

    	continent.ClearOptions();
    	neighbor_right.ClearOptions();
		neighbor_left.ClearOptions();
		choose_land.ClearOptions();
    }

    public void changeLandInfo(string info){
    	land_description.text = info;
    }

    public void addContinentView(string name){
    	continent_list.text += " " + name; 
    	addOptionDroppdown(continent, name);
    }

    public void addLandView(Land land){
    	addOptionDroppdown(neighbor_left, land.getName());
    	addOptionDroppdown(neighbor_right, land.getName());
    	addOptionDroppdown(choose_land, land.getName());
    }

    public void popupSuccefull(){
        Debug.Log("File creato");
        //sarebbe meglio se creasse un pop-up con scritto "Il file è stato creato !!!"
    }

    private void addOptionDroppdown(Dropdown dropdownObject, string optionString){
    	Dropdown.OptionData newOption = new Dropdown.OptionData();
    	newOption.text = optionString;
    	dropdownObject.options.Add(newOption);
    	dropdownObject.RefreshShownValue();
    }
}
