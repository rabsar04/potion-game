using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public IngredientController ingredientRecipe;
    public CustomerController customerController;
    // public Interactable interacting;
    public static bool isReset;
    // public List<GameObject> sprites;
    public static bool isConfirm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown() {
        if(this.tag == "Confirm") {
            if (ingredientRecipe.matchedIndex == -1) {
                Debug.Log("Potion has not been created");
                customerController.correctPotion(ingredientRecipe.matchedIndex);
                ingredientRecipe.components.Clear();
            } else {
                Debug.Log("Confirmed " + ingredientRecipe.matchedIndex);

                // string thepotion = ingredientRecipe.nameofRecipe.GetComponent<TMPro.TextMeshPro>().text;
                customerController.correctPotion(ingredientRecipe.matchedIndex);
                isConfirm = true;
                ingredientRecipe.components.Clear();
                // ingredientRecipe.nameofRecipe.GetComponent<TMPro.TextMeshPro>().text = "";
                // ingredientRecipe.spriteRenderer.sprite = null;
            }
        } else {
            ingredientRecipe.components.Clear();
            ingredientRecipe.nameofRecipe.GetComponent<TMPro.TextMeshPro>().text = "";
            // interacting.transform.localPosition = interacting.originalPos;
            isReset = true;
            Debug.Log("Reset");
        }
    }

    void OnMouseUp() {
        if(this.tag == "Confirm") {
            isConfirm = false;
        } else {
            isReset = false;
        }
    }

    IEnumerator Cook() {
        yield return new WaitForSeconds(5);
    }
}
