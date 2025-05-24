using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<AudioSource> audios;
    public List<Interactable> draggableObjects;
    public float snapRange = 0.5f;
    public IngredientController ingredientRecipe;
    public List<GameObject> timers;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Interactable draggable in draggableObjects) {
            draggable.dragEndedCallback = OnDragEnded;
        }
    }

    private void OnDragEnded(Interactable draggable) {
        float closestDistance = -1;
        Transform closestSnapPoint = null;

        foreach(Transform snapPoint in snapPoints) {
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.localPosition);
            if(closestSnapPoint == null || currentDistance < closestDistance) {
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange) {
            draggable.transform.localPosition = closestSnapPoint.localPosition;
            Debug.Log("snap");
            Debug.Log(draggable.gameObject.name);
            if (ingredientRecipe == null) {
                Debug.LogError("ingredientRecipe is null! Cannot add components.");
                return; // Exit to prevent further errors
            } else if (ingredientRecipe.components == null) {
                Debug.LogError("ingredientRecipe.components is null! Cannot add components.");
                return;
            }
            if (draggable.transform.localPosition == snapPoints[0].localPosition) {
                ingredientRecipe.matchingRecipe(draggable.gameObject, draggable.isWhole, draggable.isChopped, draggable. isDried, draggable.isGround);
            }
        } else {
            draggable.transform.localPosition = draggable.originalPos;
            Debug.Log("original");
        }
    }
}
