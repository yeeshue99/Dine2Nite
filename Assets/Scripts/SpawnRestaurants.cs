using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnRestaurants : MonoBehaviour
{
    public GameObject restuarantPrefab;
    public GameObject loadingAnimation;
    public RestaurantUtility ru;

    IEnumerator Start()
    {

        ru = GameObject.FindObjectOfType<RestaurantUtility>();


#if !UNITY_EDITOR
        if (!Input.location.isEnabledByUser)
            yield break;

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        Input.location.Stop();
#endif
#if UNITY_EDITOR
        yield return null;
        yield return new WaitForSeconds(3);
#endif

        loadingAnimation.SetActive(false);
        TextAsset r = Resources.Load<TextAsset>("Restaurants");
        RestaurantList json = JsonUtility.FromJson<RestaurantList>(r.ToString());

        ru.RestaurantList = json;
        
        foreach (var restaurant in json.restaurants){
            GameObject newRestaurant = Instantiate(restuarantPrefab, transform);

            TextMeshProUGUI name = newRestaurant.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            name.text = restaurant.name;

            TextMeshProUGUI type = newRestaurant.transform.Find("Type").GetComponent<TextMeshProUGUI>();
            type.text = restaurant.foodType;

            TextMeshProUGUI meal = newRestaurant.transform.Find("Meal").GetComponent<TextMeshProUGUI>();
            string tempText = "";
            foreach (var m in restaurant.mealType)
            {
                tempText = tempText + (MealType) m + " • " ;
            }
            meal.text = tempText.Substring(0, tempText.Length - 3);

            TextMeshProUGUI distance = newRestaurant.transform.Find("Distance").GetComponent<TextMeshProUGUI>();
            Vector2 restaurantLocation;
            Vector2 userLocation;
#if UNITY_EDITOR
            restaurantLocation = new Vector2((float)restaurant.longitude, (float)restaurant.latitude);
            userLocation = new Vector2((float)10.1, (float)9.9);
#endif
#if !UNITY_EDITOR
            restaurantLocation = new Vector2((float)restaurant.longitude, (float)restaurant.latitude);
            userLocation = new Vector2(Input.location.lastData.longitude, Input.location.lastData.latitude);
#endif
            distance.text = (Mathf.Round(Vector2.Distance(restaurantLocation, userLocation) * 100) / 100.0f).ToString() + " MILES";

            ru.RestaurantButtons.Add(newRestaurant);
        }
    }
}
