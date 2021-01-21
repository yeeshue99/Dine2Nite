using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RestaurantUtility : MonoBehaviour
{
    private RestaurantUtility singleton;

    public RestaurantList RestaurantList { get => singleton._RestaurantList; set => singleton._RestaurantList = value; }
    private RestaurantList _RestaurantList {get; set; }

    public List<GameObject> RestaurantButtons { get => singleton._RestaurantButtons; set => singleton._RestaurantButtons = value; }
    private List<GameObject> _RestaurantButtons;

    public Restaurant currentRestaurant { get => singleton._currentRestaurant; set => singleton._currentRestaurant = value; }
    private Restaurant _currentRestaurant;

    private void Awake()
    {
        _RestaurantButtons = new List<GameObject>();

        DontDestroyOnLoad(this);
        singleton = this;
    }
}
