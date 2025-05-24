using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientController : MonoBehaviour
{
    public List<Recipes> recipe;
    public List<IngredientStates> components = new List<IngredientStates>();
    // public bool isMaking;
    public bool areListsEqual = false;
    public GameObject nameofRecipe;
    // public Interactable interactable;
    public int matchedIndex = -1;
    // public SpriteRenderer spriteRenderer;
    public Sprite[] spritePotions;
    // Start is called before the first frame update
    void Start()
    {
        // if (components == null)
        // {
        //     components = new List<GameObject>();
        // }
        nameofRecipe.GetComponent<TMPro.TextMeshPro>().text = "";
        // spriteRenderer = nameofRecipe.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void UpdateIngredientState(GameObject ingredient, bool isWhole, bool isChopped, bool isDried, bool isGround)
    {
        // Find the corresponding IngredientState
        IngredientStates ingredientState = components.Find(state => state.ingredientItem == ingredient);

        // If it exists, update the state
        // if (ingredientState != null)
        // {
        //     ingredientState.UpdateState(isWhole, isChopped, isDried, isGround);
        // }
        // else
        // {
            // If it doesn't exist yet, create a new one and add it to the components list
            ingredientState = new IngredientStates(ingredient);
            ingredientState.UpdateState(isWhole, isChopped, isDried, isGround);
            components.Add(ingredientState);
        // }
    }

    public void matchingRecipe(GameObject drag, bool isWhole, bool isChopped, bool isDried, bool isGround) {

        UpdateIngredientState(drag, isWhole, isChopped, isDried, isGround);

        List<GameObject> componentsGameObjects = new List<GameObject>();
        foreach (var newComponent in components)
        {
            componentsGameObjects.Add(newComponent.ingredientItem);
        }
        
        matchedIndex = FindMatchingRecipe(recipe, componentsGameObjects);
        if (matchedIndex != -1)
        {
            // A match was found, print the name of the recipe
            Debug.Log("Matched Recipe: (" + matchedIndex + ") " + recipe[matchedIndex].name);
            nameofRecipe.GetComponent<TMPro.TextMeshPro>().text = recipe[matchedIndex].name;
        }
        else
        {
            // No match found
            Debug.Log("No matching recipe found.");
            nameofRecipe.GetComponent<TMPro.TextMeshPro>().text = "";
        }
    }

    public int FindMatchingRecipe(List<Recipes> recipeList, List<GameObject> ingredientsToMatch)
    {
        for (int i = 0; i < recipeList.Count; i++)
        {
            if (AreListsEquivalentWithOrder(recipeList[i].GetIngredientGameObjects(), ingredientsToMatch))
            {
                Debug.Log("Match found for recipe index: " + i);
                return i; // Return the index of the matched recipe
            }
        }

        return -1; // No match found
    }


    bool AreListsEquivalentWithOrder(List<GameObject> list1, List<GameObject> list2)
    {
        if (list1.Count != list2.Count)
            return false;

        for (int i = 0; i < list1.Count; i++)
        {
            if (!list1[i].Equals(list2[i]))  // Use the Equals method from IngredientState
            {
                return false;
            }
        }

        Debug.Log("Lists are equivalent with order.");
        return true;
    }

    // void isthisaRecipe(List<GameObject> whatsintheCauldron) {
    //     Debug.Log("An ingredient was added");
    //     for (var i = 0; i < components.Count; i++) {
    //         foreach (GameObject potionIng in recipe[i].ingredients) {
    //             foreach (GameObject inCauldreon in components) {
    //                 if (inCauldreon == potionIng) {
    //                     areListsEqual = true;
    //                     Debug.Log("Lists Equal");
    //                 } else {
    //                     areListsEqual = false;
    //                     Debug.Log("Lists Not Equal");
    //                 }
    //             }
    //         }
    //     }
    // }

    // public int FindMatchingRecipe(List<Recipes> recipeList, List<GameObject> ingredientsToMatch)
    // {
    //     for (int i = 0; i < recipeList.Count; i++)
    //     {
    //         if (AreListsEquivalentWithDuplicates(recipeList[i].ingredients, ingredientsToMatch))
    //         {
    //             Debug.Log("i returned");
    //             return i;  // Return the index of the matched recipe
    //         }
    //     }

    //     return -1;  // No match found
    // }

    // bool AreListsEquivalentWithDuplicates(List<GameObject> list1, List<GameObject> list2)
    // {
    //     if (list1.Count != list2.Count)
    //         return false;

    //     Dictionary<GameObject, int> countMap1 = GetElementCounts(list1);
    //     Dictionary<GameObject, int> countMap2 = GetElementCounts(list2);

    //     foreach (var kvp in countMap1)
    //     {
    //         int countInList2;
    //         if (!countMap2.TryGetValue(kvp.Key, out countInList2) || countInList2 != kvp.Value)
    //         {
    //             return false;
    //         }
    //     }

    //     Debug.Log("match found");
    //     return true;
    // }

    // Dictionary<GameObject, int> GetElementCounts(List<GameObject> list)
    // {
    //     Dictionary<GameObject, int> countMap = new Dictionary<GameObject, int>();
    //     foreach (GameObject obj in list)
    //     {
    //         if (countMap.ContainsKey(obj))
    //         {
    //             countMap[obj]++;
    //         }
    //         else
    //         {
    //             countMap[obj] = 1;
    //         }
    //     }
    //     return countMap;
    // }
}
