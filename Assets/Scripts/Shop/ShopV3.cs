using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class ShopV3 : MonoBehaviour
{
    // WE DECLARE List<int> of all shopItems
    private List<int> _shopItems;

    // WE DECLARE Dictonary<string,vector3> of start positions for shop items
    private Dictionary<string, Vector3> _startPositions, _endPositions;

    // Here we will store data from shop json data
    private ShopData _shopData;

    private Rocket[] _shopRockets;

    // WE USE THIS TO IMPORT SHOP UTILITY FUNCTIONS
    private ShopUtilities _shopUtilities;

    private void Awake()
    {
        _shopData = DataManager.Instance.shopData;
        _shopRockets = _shopData.rockets;


        _shopUtilities = new ShopUtilities();

        _shopItems = new List<int>();
        _shopItems = ShopUtilities.FillShopItemsList(_shopItems, _shopRockets.Length);

        // HANDLE SHOP ITEM POSITIONS GENERATION
        _startPositions = new Dictionary<string, Vector3>();
        _endPositions = new Dictionary<string, Vector3>();
        _startPositions = ShopUtilities.FillStartPositionsDictionary(_startPositions);
        _endPositions = ShopUtilities.FillEndPositionsDictionary(_endPositions);
        
        // TODO next up sort out spawning of shop items
        
        
    }
}