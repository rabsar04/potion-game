using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public delegate void DragEndedDelegate(Interactable draggableObject);

    public DragEndedDelegate dragEndedCallback;
    public SnapController snapControl;
    public Sprite[] spriteStates;

    private bool isDragged = false;
    public bool isWhole = true;
    public bool isChopped = false;
    public bool isGround = false;
    public bool isDried = false;
    // public bool isInteracting;
    public Vector3 mouseDragStartPos;
    public Vector3 spriteDragStartPos;
    public Vector3 originalPos;
    public Vector3 tempPos;
    public bool isIngredient;
    public bool isMoveable;
    public int howMany;

    void Awake() {
        originalPos = transform.position;
        tempPos = transform.position;
    }

    void Start() {
        // howMany = 3;
    }

    void Update() {
        if (transform.position == originalPos) {
            isWhole = true;
            isChopped = false;
            isGround = false;
            isDried = false;
        }
        
        if (Reset.isReset) {
            transform.position = originalPos;
        }

        // if (howMany == 0) {
        //     gameObject.SetActive(false);
        // } else {
        //     gameObject.SetActive(true);
        // }
    }

    private void OnMouseDown() {
        if(isMoveable){
            isDragged = true;
            mouseDragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spriteDragStartPos = transform.position;
        }
        // Debug.Log(originalPos);
    }

    private void OnMouseDrag() {
        if (isDragged) {
            transform.position = spriteDragStartPos + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPos);
        }
    }

    private void OnMouseUp() {
        isDragged = false;
        dragEndedCallback(this);
        StartCoroutine(Replace());
    }

    IEnumerator Replace() {
        // if (transform.position == snapControl.snapPoints[0].position) {
        //     findAudio(0, true);
        //     yield return new WaitForSeconds(0.5f);
        //     transform.position = originalPos;
        //     if (isIngredient) {
        //         howMany--;
        //     }
        //     Debug.Log("Returned");
        // } else if (transform.position == snapControl.snapPoints[1].position && isIngredient) {
        //     isChopped = true;
        //     isWhole = false;
        //     yield return StartCoroutine(Prep(3, 1, 0));
        //     Debug.Log("Chopped");
        // } else if (transform.position == snapControl.snapPoints[2].position && isIngredient) {
        //     isGround = true;
        //     isWhole = false;
        //     yield return StartCoroutine(Prep(5, 2, 1));
        //     Debug.Log("Ground");
        // } else if (transform.position == snapControl.snapPoints[3].position && isIngredient) {
        //     isDried = true;
        //     isWhole = false;
        //     yield return StartCoroutine(Prep(10, 3, 2));
        //     Debug.Log("Dried");
        // } else {
        //     transform.position = tempPos;
        // }
        int snapIndex = -1; // Default value if no match is found

        // Find which snap point the object is currently at
        for (int i = 0; i < snapControl.snapPoints.Count; i++)
        {
            if (transform.position == snapControl.snapPoints[i].position)
            {
                snapIndex = i;
                break;
            }
        }

        switch (snapIndex)
        {
            case 0:
                findAudio(0, true);
                yield return new WaitForSeconds(0.5f);
                transform.position = originalPos;
                if (isIngredient)
                {
                    howMany--;
                    changeSprite(3);
                }
                Debug.Log("Returned");
                break;

            case 1:
                if (isIngredient)
                {
                    isChopped = true;
                    isWhole = false;
                    yield return StartCoroutine(Prep(3, 1, 0));
                    Debug.Log("Chopped");
                }
                break;

            case 2:
                if (isIngredient)
                {
                    isGround = true;
                    isWhole = false;
                    yield return StartCoroutine(Prep(5, 2, 1));
                    Debug.Log("Ground");
                }
                break;

            case 3:
                if (isIngredient)
                {
                    isDried = true;
                    isWhole = false;
                    yield return StartCoroutine(Prep(10, 3, 2));
                    Debug.Log("Dried");
                }
                break;

            default:
                transform.position = tempPos;
                break;
        }
    }

    IEnumerator Prep(float time, int indexforAudio, int location) {
        isMoveable = false;
        tempPos = transform.position;
        findAudio(indexforAudio, true);
        // snapControl.snapTimer(time, location);
        Image radialTimer = snapControl.timers[location].GetComponent<Image>();
        snapControl.timers[location].SetActive(true);
        // radialTimer.fillAmount -= 1.0f / time * Time.deltaTime;
        // yield return new WaitForSeconds(time);
        // if (radialTimer.fillAmount <= 0f) {
        //     snapControl.timers[location].SetActive(false);
        //     radialTimer.fillAmount = 1f;
        // }
        float elapsedTime = 0f;

        // Gradually decrease the fillAmount over the specified time
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;  // Increment elapsed time
            radialTimer.fillAmount = 1f - (elapsedTime / time);  // Update fill amount

            yield return null;  // Wait for the next frame
        }

        // Ensure fillAmount is set to 0 at the end
        radialTimer.fillAmount = 0f;

        findAudio(indexforAudio, false);
        isMoveable = true;
        changeSprite(location);
    }

    void changeSprite(int spriteIndex) {
        SpriteRenderer childSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        childSpriteRenderer.sprite = spriteStates[spriteIndex];
    }

    public void findAudio(int indexOfAudio, bool isPlaying) {
        if (isPlaying) {
            snapControl.audios[indexOfAudio].Play();
        } else {
            snapControl.audios[indexOfAudio].Stop();
        }
    }
}
