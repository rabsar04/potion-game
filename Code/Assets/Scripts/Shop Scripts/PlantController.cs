using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public GameObject ingredient;
    public Sprite[] spriteArray;
    private int plantNo;
    public int regrow;
    public int pickPlant;
    public bool canClick;
    public AudioSource flowerPop;
    private Interactable interactable;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        canClick = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        interactable = ingredient.GetComponent<Interactable>();
        ChangeSprite(0);
        StartCoroutine(GrowBack(regrow));
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable.howMany > 0) {
            ingredient.SetActive(true);
        } else {
            ingredient.SetActive(false);
            interactable.transform.position = interactable.originalPos;
        }
    }

    void OnMouseDown() {
        Debug.Log("Plant Clicked");
        if (canClick) {
            Debug.Log("Before: " + interactable.howMany);
            interactable.howMany += pickPlant;
            Debug.Log("After: " + interactable.howMany);
            canClick = false;
            flowerPop.Play();
            // spriteRenderer.color = Color.white;
            ChangeSprite(0);
            StartCoroutine(GrowBack(regrow));
        } else {
            Debug.Log("Still Growing");
        }
    }

    IEnumerator GrowBack(int time) {
        // yield return new WaitForSeconds(time);
        // Debug.Log("Grown Back");
        // canClick = true;
        // // spriteRenderer.color = Color.red;
        // ChangeSprite(1);
        if (spriteArray == null || spriteArray.Length == 0)
        {
            Debug.LogError("spriteArray is not assigned or is empty!");
            yield break; // Exit the coroutine if no sprites are available
        }

        int spriteCount = spriteArray.Length;
        float interval = time / spriteCount;  // Calculate time interval for each sprite

        for (int i = 0; i < spriteCount; i++)
        {
            // Change the sprite
            ChangeSprite(i);
            // Wait for the interval before changing to the next sprite
            yield return new WaitForSeconds(interval);
        }

        Debug.Log("Grown Back");
        canClick = true;
    }

    void ChangeSprite(int changeSprite)
    {
        spriteRenderer.sprite = spriteArray[changeSprite]; 
    }
}
