using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IngredientStates
{
    public GameObject ingredientItem;
    public bool isWhole;
    public bool isChopped;
    public bool isDried;
    public bool isGround;

    public IngredientStates(GameObject ingredientItem)
    {
        this.ingredientItem = ingredientItem;
        isWhole = true; // Default state
        isChopped = false;
        isDried = false;
        isGround = false;
    }

    public void UpdateState(bool whole, bool chopped, bool dried, bool ground)
    {
        isWhole = whole;
        isChopped = chopped;
        isDried = dried;
        isGround = ground;
    }
}