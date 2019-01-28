using System.Collections.Generic;

public class Continent
{
	/*
    MODIFICHE DA EFFETTUARE:
        -Adattare la classe quando viene implementato il game vero e proprio

        Note: NON METTETE VARIABILI CALCOLABILI come numero totale di ect. se vi serve e non 
        si può usare List.Count createvi un metodo 
    */
	
    private string name;
    private List<Land> lands;
    private string nameSprite;
    private int bonusTank;

    public Continent(string name, List<Land> lands, int bonusTank)
    {
        this.name = name;
        this.lands = lands;
        this.bonusTank = bonusTank;
    }

    public Continent(string name): this(name, new List<Land>())
    {
    }

    public void addLand(Land newLand)
    {
    	lands.Add(newLand);
    }

    public string getName()
    {
    	return name;
    }

    public List<Land> getLands(){
        return lands;
    }

    public override bool Equals(object obj)  
    {  
       if (obj == null)  
            return false;  
       if (this.GetType() != obj.GetType()) 
       	return false;
	   Continent p = (Continent)obj;  
	   return (name == p.getName());
    }

    public int getBonusTank()
    {
        return bonusTank;
    }
}
