using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ShopData
{
    public string name;
    public string currentlySelectedItem;
    public Rocket[] rockets;
    


}
[System.Serializable]
public class Rocket
{

    public int _id;
    public string name;
    public int price;
    public string[] materials;
    public bool unlocked;
    public string specialPower;
}


