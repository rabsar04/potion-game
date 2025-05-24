using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipes
{
    public string name;      // Name of recipe
    public List<IngredientStates> ingredients;  // ingredient list

    public List<GameObject> GetIngredientGameObjects()
    {
        List<GameObject> gameObjects = new List<GameObject>();
        foreach (var ingredientState in ingredients)
        {
            gameObjects.Add(ingredientState.ingredientItem);
        }
        return gameObjects;
    }
}
