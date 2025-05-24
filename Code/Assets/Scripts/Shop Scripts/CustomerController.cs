using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CustomerController : MonoBehaviour
{
    public TextMeshProUGUI customerSpeech;
    // public List<string> customerOrdering;
    // public List<GameObject> customerPotions;
    public IngredientController ingredientRecipe;
    public List<CustomerOrders> customerOrder;
    public List<GameObject> customerList;
    public int numberofCustomer;
    public int lastNo;
    public bool isPlaying = false;
    public GameObject dialogueHolder;
    
    // Start is called before the first frame update
    void Start()
    {
        customerSpeech.text = "";
        foreach (GameObject customer in customerList) {
            customer.SetActive(false);
        }
        numberofCustomer = -1;
        lastNo = -1;
        // isPlaying = false;
        // numberGoesUp();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying) {
            dialogueHolder.SetActive(true);
        } else {
            dialogueHolder.SetActive(false);
            foreach (GameObject customer in customerList) {
                customer.SetActive(false);
            }
        }
    }

    public void correctPotion(int indexOfPotion) {
        StartCoroutine(CorrectPotionCoroutine(indexOfPotion));
        // sprites[indexOfPotion].SetActive(true);
    }

    private IEnumerator CorrectPotionCoroutine(int indexOfPotion) {
        Movement.isLocked = true;

        if (indexOfPotion == customerOrder[numberofCustomer].potionNo) {
            Debug.Log("Correct");
            customerSpeech.text = "...";
            yield return StartCoroutine(WriteSentence(1, customerOrder[numberofCustomer].correctText));
            numberGoesUp();
        } else {
            Debug.Log("Incorrect");
            customerSpeech.text = "...";
            yield return StartCoroutine(WriteSentence(1, customerOrder[numberofCustomer].incorrectText));
            Movement.isLocked = false;
            yield return StartCoroutine(WriteSentence(2, customerOrder[numberofCustomer].orderText));
        }

        // sprites[indexOfPotion].SetActive(false);
        Movement.isLocked = false; // Unlock the movement after everything is done
    }

    public void numberGoesUp() {
        numberofCustomer++;
        Debug.Log("number of customer: " + numberofCustomer);
        try
        {
            if (numberofCustomer < customerOrder.Count)
            {
                isPlaying = true;
            }
            else
            {
                // Handle the situation where there are no more customers
                Debug.Log("No more customers.");
                isPlaying = false;
            }
        }
        catch (ArgumentOutOfRangeException e)
        {
            Debug.LogError("ArgumentOutOfRangeException caught: " + e.Message);
        }
        StartCoroutine(NextCustomerCoroutine());
    }

    private IEnumerator NextCustomerCoroutine() {
        if (lastNo != numberofCustomer && isPlaying) {
            yield return new WaitForSeconds(2);
            Debug.Log("Next Customer");
            customerSpeech.text = "";
            yield return StartCoroutine(WriteSentence(0, customerOrder[numberofCustomer].orderText));
            lastNo = numberofCustomer;
        }
        Movement.isLocked = false;
    }

    void customerSprite() {
        foreach (GameObject customer in customerList) {
            customer.SetActive(false);
        }
        customerList[numberofCustomer].SetActive(true);
    }

    IEnumerator WriteSentence(int seconds, string whatText)
    {
        yield return new WaitForSeconds(seconds);
        Debug.Log("typing");
        customerSpeech.text = "";
        customerSprite();
        for (int i = 0; i < whatText.Length; i++)
        {
            customerSpeech.text += whatText[i];
            yield return new WaitForSeconds(0.05f);
        }
        // yield return null;
    }
}
