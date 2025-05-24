using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RecipeBook : MonoBehaviour
{
    public IngredientController ingCon;
    public GameObject recipeBook;
    public GameObject potionName;
    public GameObject potionRecipe;
    public List<string> recipeBookList = new List<string>();
    public static int indexOfPotion;
    // public static int prevIndex;
    // public int changeIndex;
    // Start is called before the first frame update
    void Start()
    {
        recipeBook.SetActive(false);
        indexOfPotion = 0;
        Debug.Log("Index of Potion:" + indexOfPotion);
        recipePrint(indexOfPotion);
    }

    // Update is called once per frame
    void Update()
    {
        if (recipeBook.activeSelf) {
            Movement.isLocked = true;
            // if (Input.GetKeyDown(KeyCode.A))
            // {
            //     recipePrint(indexOfPotion--);
            // }
            // if (Input.GetKeyDown(KeyCode.D))
            // {
            //     recipePrint(indexOfPotion++);
            // }
        } else {
            Movement.isLocked = false;
        }
    }

    public void recipePrint(int recipeIndex) {
        // potionName.GetComponent<TextMeshPro>().text = ingCon.recipe[recipeIndex].name;
        // potionRecipe.GetComponent<TextMeshPro>().text =
        // foreach (GameObject prefab in prefabs) {
        //     prefab.SetActive(false);
        // }
        // prefabs[indexOfPotion].SetActive(true);
        TextMeshProUGUI tmp = potionName.GetComponent<TextMeshProUGUI>();
        if (tmp == null)
        {
            Debug.Log("TextMeshPro component is missing from potionName GameObject.");
            tmp = potionName.AddComponent<TextMeshProUGUI>();
            return;
        }
        tmp.GetComponent<TextMeshProUGUI>().text = "";
        potionRecipe.GetComponent<TextMeshProUGUI>().text = "";
        potionName.GetComponent<TextMeshProUGUI>().text = ingCon.recipe[recipeIndex].name;
        List<string> modifiedRecipes = new List<string>();
        foreach (var recipe in recipeBookList)
        {
            // Replace ', ' with '\n' (new line)
            string modifiedRecipe = recipe.Replace(", ", "\n");
            modifiedRecipes.Add(modifiedRecipe);
        }
        potionRecipe.GetComponent<TextMeshProUGUI>().text = string.Join("\n", modifiedRecipes[recipeIndex]);
    }

    void OnMouseDown() {
        recipeBook.SetActive(true);
    }

    public void buttonPress(int number) {
        // if (number == 1) {
        //     Debug.Log("index before: " + indexOfPotion);
        //     if (indexOfPotion == ingCon.recipe.Count-1) {
        //         indexOfPotion = -1;
        //     }
        //     recipePrint(indexOfPotion++);
        //     Debug.Log("index after: " + indexOfPotion);
        // } else if (number == 0) {
        //     Debug.Log("index before: " + indexOfPotion);
        //     if (indexOfPotion == 0) {
        //         indexOfPotion = ingCon.recipe.Count;
        //     }
        //     recipePrint(indexOfPotion--);
        //     Debug.Log("index after: " + indexOfPotion);
        // }

        switch (number) {
            case 0:
                indexOfPotion--;
                break;
            case 1:
                indexOfPotion++;
                break;
            default:
                break;
        }

        if (indexOfPotion == -1) {
            indexOfPotion = ingCon.recipe.Count-1;
        } else if (indexOfPotion == ingCon.recipe.Count) {
            indexOfPotion = 0;
        }

        Debug.Log(indexOfPotion);

        recipePrint(indexOfPotion);
    }

    public void close() {
        recipeBook.SetActive(false);
    }
}
