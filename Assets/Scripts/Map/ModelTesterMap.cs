using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ModelTesterMap : MonoBehaviour
{
	//constant
	private const int MAX_NEIGHBORS = 10;
	//data

	//da cancellare/modificare quando viene applicato i singleton delle classi
	
	public ViewGameMap view;
	public GameManager manager;
	private string lastAction ="";
	private string firstland = "", secondland = "";

	public void deploy(string land, string tank)
    {
    	manager.addTroup(land, int.Parse(tank));
    	lastAction = "Deployed";
    	updateView();
    }

    public void attack(string landAttacker , string landDefender, string tankAttacker, string tankDefender)
    {
    	manager.attack(landAttacker, landDefender, int.Parse(tankAttacker), int.Parse(tankDefender));
    	lastAction = "Attacked";
    	updateView();
    }

    public void move(string landStart, string landEnd, string tank_deploy)
    {
    	manager.move(landStart, landEnd, int.Parse(tank_deploy));
    	lastAction = "Moved";
    	updateView();
    }

    public void pass(){
        manager.passTurn();
        lastAction = "Passed";
        updateView();
    }

    private void updateView()
    {
    	List<string> data =	manager.getDataForView();
    	data.Add(lastAction);
    	view.updateText(data);
    }


	private void Awake()
	{
		temporaryData();
	}

	private void temporaryData()//inizializza i dei dati su cui ci si può testare la parte logica
	{
		List<Player> players = new List<Player>();
		Player pippo = new Player("Pippo");
		players.Add(pippo);
		Player paperino = new Player("Paperino");
		players.Add(paperino);
		Player pluto = new Player("Pluto");
		players.Add(pluto);

		List<Continent> WorldTmp = new List<Continent>();

		Continent tmpContinent = new Continent("Europa");
		Land a = new Land("Italia", 5);
		Land b = new Land("Germania", 8);
		Land c = new Land("Svizzera", 9);
		Land d = new Land("Francia", 4);
		Land e = new Land("Grecia", 3);
		Land f = new Land("EuropaNordOvest", 2);
		createRelation(a,c);
		createRelation(a,d);
		createRelation(a,e);
		createRelation(b,c);
		createRelation(b,d);
		createRelation(b,f);
		createRelation(c,d);
		createRelation(c,f);
		createRelation(e,f);
		tmpContinent.addLand(a);
		tmpContinent.addLand(b);
		tmpContinent.addLand(c);
		tmpContinent.addLand(d);
		tmpContinent.addLand(e);
		tmpContinent.addLand(f);
		WorldTmp.Add(tmpContinent);

		paperino.addLand(a);
		paperino.addLand(b);
		paperino.addLand(c);
		pluto.addLand(d);
		pluto.addLand(e);
		pluto.addLand(f);


		Land italia = a;
		Land grecia = e;
		Land eu_nordest = f;

		tmpContinent = new Continent("Africa");
		a = new Land("Libia", 3);
		b = new Land("Egitto", 4);
		c = new Land("Sud'Africa", 5);
		d = new Land("Africa Centrale", 6);
		e = new Land("Africa Orientale", 7);
		f = new Land("Africa Occidentale",8);
		createRelation(a,b);
		createRelation(a,d);
		createRelation(a,e);
		createRelation(b,d);
		createRelation(b,f);
		createRelation(c,d);
		createRelation(c,e);
		createRelation(c,f);
		createRelation(d,e);
		createRelation(d,f);
		tmpContinent.addLand(a);
		tmpContinent.addLand(b);
		tmpContinent.addLand(c);
		tmpContinent.addLand(d);
		tmpContinent.addLand(e);
		tmpContinent.addLand(f);
		WorldTmp.Add(tmpContinent);


		pippo.addLand(a);
		paperino.addLand(b);
		pippo.addLand(c);
		pippo.addLand(d);
		pippo.addLand(e);
		pluto.addLand(f);

		Land libia = a;
		Land egitto = b;
		Land africa_orientale = e;

		tmpContinent = new Continent("Asia");
		a = new Land("Turchia", 9);
		b = new Land("Russia", 7);
		c = new Land("Cina", 5);
		d = new Land("Asia Centrale", 3);
		e = new Land("isole dell'Asia", 4);
		f = new Land("Asia sud-occidentale", 6);
		createRelation(a,b);
		createRelation(a,d);
		createRelation(a,e);
		createRelation(b,c);
		createRelation(b,d);
		createRelation(c,d);
		createRelation(c,e);
		createRelation(d,e);
		createRelation(d,f);
		createRelation(e,f);
		tmpContinent.addLand(a);
		tmpContinent.addLand(b);
		tmpContinent.addLand(c);
		tmpContinent.addLand(d);
		tmpContinent.addLand(e);
		tmpContinent.addLand(f);
		WorldTmp.Add(tmpContinent);

		pippo.addLand(a);
		paperino.addLand(b);
		pluto.addLand(c);
		pippo.addLand(d);
		pluto.addLand(e);
		paperino.addLand(f);

		Land turchia = a;
		Land russia = b;
		Land asia_sud_occidentale = e;

		createRelation(italia,libia);
		createRelation(grecia,egitto);
		createRelation(grecia,turchia);
		createRelation(eu_nordest, turchia);
		createRelation(eu_nordest, russia);
		createRelation(egitto, turchia);
		createRelation(africa_orientale, asia_sud_occidentale);

		manager = new GameManager(players, WorldTmp);

		view.loadDropdownList(getOptions(WorldTmp));
		lastAction = "Started";
		updateView();
	}

	private List<string> getOptions(List<Continent> World)
	{
		List<string> options = new List<string>();
		foreach(Continent continent in World)
		{
			List<Land> lands = continent.getLands();
			foreach(Land land in lands)
			{
				options.Add(land.getName());
			}
		}

		return options;
	}

	private void createRelation(Land landLeft, Land landRight){
		//manca il controllo
		landLeft.addNeighbor(landRight);
		landRight.addNeighbor(landLeft);
	}

}
