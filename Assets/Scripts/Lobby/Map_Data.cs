using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Data : MonoBehaviour
{
    private string name;
    private int maxEasy, maxNormal, maxHard, minEasy, minNormal, minHard;
    private Sprite image;

    public Map_Data(string name, int maxEasy, int maxNormal, int maxHard, int minEasy, int minNormal, int minHard, Sprite image)
    {
        this.name = name;
        this.maxEasy = maxEasy;
        this.maxNormal = maxNormal;
        this.maxHard = maxHard;
        this.minEasy = minEasy;
        this.minNormal = minNormal;
        this.minHard = minHard;
        this.image = image;
    }

    public string getName()
    {
        return name;
    }

    public int getmaxEasy()
    {
        return maxEasy;
    }

    public int getmaxNormal()
    {
        return maxNormal;
    }

    public int getmaxHard()
    {
        return maxHard;
    }

    public int getminEasy()
    {
        return minEasy;
    }

    public int getminNormal()
    {
        return minNormal;
    }

    public int getminHard()
    {
        return minHard;
    }

    public Sprite getimage()
    {
        return image;
    }

}
