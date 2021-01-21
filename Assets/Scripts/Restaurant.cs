[System.Serializable]
public class Restaurant
{
    public string name;
    public string foodType;
    public System.Collections.Generic.List<int> mealType;
    public double latitude;
    public double longitude;
}

public enum MealType
{
    Breakfast,
    Lunch,
    Dinner,
    Dessert
}

public class RestaurantList
{
    public System.Collections.Generic.List<Restaurant> restaurants;
}