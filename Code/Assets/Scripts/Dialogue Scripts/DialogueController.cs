using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public bool isActive;
    public CustomerController CC;
    private bool isTyping = false; // To track if typing is in progress
    private bool skipToNext = false; // To track if the user wants to skip to the next dialogue
    public List<GameObject> spriteHolders;
    public TextMeshProUGUI CharacterName;
    public TextMeshProUGUI DialogueText;
    public GameObject characterHolder;
    public GameObject dialogueParent;
    public List<Sentence> Sentences;  // List of sentences (each with a name and dialogue)
    
    private int Index = 0;
    private float DialogueSpeed;
    private static string setName = "";

    // Start is called before the first frame update
    void Start()
    {
        CleanUp();
        // DialogueText.text = "";
        // CharacterName.text = "";
        // characterHolder.SetActive(false);
        // dialogueParent.SetActive(false);
        // foreach (GameObject parent in spriteHolders)
        // {
        //     // Loop through all the children of the current parent object
        //     foreach (Transform child in parent.transform)
        //     {
        //         // Set each child to inactive
        //         child.gameObject.SetActive(false);
        //     }
        // }
        // spriteHolders.SetActive(false);
        // foreach (GameObject sprite in Sentences[Index].sprites)
        // {
        //     sprite.SetActive(false);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) 
        {
            characterHolder.SetActive(true);
            dialogueParent.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                try {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (isTyping)
                        {
                            // If typing, speed it up or complete instantly
                            skipToNext = true;
                        }
                        else
                        {
                            // If not typing, move to the next sentence
                            NextSentence();
                        }
                    }
                } catch (ArgumentOutOfRangeException ex) {
                    isActive = false;
                    SentenceFinish();
                    // gameObject.SetActive(false);
                    Debug.Log("Index Out of Range: " + ex);
                }
        //         // foreach (GameObject sprite in Sentences[Index].sprites)
        //         // {
        //         //     sprite.SetActive(true);
        //         // }
        //         // Sentences[Index].sprites.Clear();
            }
        }

        if (!isActive) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
        // else {
        //     SentenceFinish();
        // }
    
    }

    // public void turnActiveOn()
    // {
    //     isActive = true;
    // }

    // public void turnActiveOff()
    // {
    //     isActive = false;
    // }

    void NextSentence()
    {
        DialogueText.text = "";
        if (Index > 0) {
            if (Sentences[Index-1].audio != null) {
                Sentences[Index-1].audio.Stop();
            }
            SpriteDisplay(Index-1, false);
        }
        if(Index < Sentences.Count)
        {
            if (Sentences[Index].name == "Player")
            {
                CharacterName.text = setName;
            } else {
                CharacterName.text = Sentences[Index].name;
            }
            SpriteDisplay(Index, true);
            StartCoroutine(WriteSentence());
            if (Sentences[Index].audio != null) {
                Debug.Log("Coroutine Audio");
                StartCoroutine(AudioStop(Sentences[Index].audio));
            }
        } 
        else 
        {
            isActive = false;
            SpriteDisplay(Index, false);
            if (Sentences[Index].audio != null) {
                Sentences[Index].audio.Stop();
            }
            SentenceFinish();
        }
    //     foreach (GameObject sprite in spriteList)
    //     {
    //         if (Sentences[Index].sprites.Contains(sprite))
    //         {
    //             sprite.SetActive(true);
    //         } else {
    //             sprite.SetActive(false);
    //         }
    //     }
    }

    IEnumerator AudioStop(AudioSource audio) {
        audio.Play();
        Debug.Log("Play");
        yield return new WaitForSeconds(3);
        audio.Stop();
        Debug.Log("Stop");
    }

    void SpriteDisplay(int indexofSprite, bool showSprite) {
        if (Sentences[indexofSprite].sprites.Count != 0) {
            Debug.Log("sprite count is not 0");
            foreach (Sprites sprite in Sentences[indexofSprite].sprites) {
                Debug.Log("for each sprite in sprites");
                switch(sprite.isFlipped) {
                    case true:
                        sprite.spriteImage.transform.localRotation = Quaternion.Euler(0, 180, 0);
                        Debug.Log("sprite is flipped");
                        break;
                    case false:
                        sprite.spriteImage.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        Debug.Log("sprite is not flipped");
                        break;
                }
                List<bool> spriteBooleans = new List<bool>();
                spriteBooleans.Add(sprite.Neutral);
                spriteBooleans.Add(sprite.EyesClosed);
                spriteBooleans.Add(sprite.Happy);
                spriteBooleans.Add(sprite.Worried);
                spriteBooleans.Add(sprite.Sad);
                spriteBooleans.Add(sprite.Annoyed);
                int i = 0;
                foreach (bool spritebool in spriteBooleans) {
                    if (spritebool) {
                        sprite.spriteImage.transform.parent.gameObject.SetActive(showSprite);
                        sprite.spriteImage.SetActive(showSprite);
                        sprite.spriteImage.transform.GetChild(i).gameObject.SetActive(showSprite);
                        Debug.Log("sprite active: " + showSprite);
                    }
                    i++;
                }
            }
        }
    }

    IEnumerator WriteSentence()
    {
        isTyping = true; // Set the typing flag to true
        skipToNext = false; // Reset the skip flag

        List<string> dialogueList = Sentences[Index].dialogue; // Access the dialogue text
        DialogueSpeed = Sentences[Index].dialogueSpeed;

        foreach (string dialogue in dialogueList)
        {
            for (int i = 0; i < dialogue.Length; i++)
            {
                if (skipToNext)
                {
                    // If space was pressed during typing, add the rest of the sentence instantly
                    DialogueText.text += dialogue.Substring(i); // Append the remaining dialogue
                    break;
                }

                DialogueText.text += dialogue[i];
                yield return new WaitForSeconds(DialogueSpeed);
            }
            yield return new WaitForSeconds(Sentences[Index].dialoguePause);
        }

        isTyping = false;
        Index++;
    }

    void CleanUp() {
        DialogueText.text = "";
        CharacterName.text = "";
        characterHolder.SetActive(false);
        dialogueParent.SetActive(false);
        foreach (GameObject parent in spriteHolders)
        {
            // Loop through all the children of the current parent object
            foreach (Transform child in parent.transform)
            {
                // Set each child to inactive
                child.gameObject.SetActive(false);
            }
            parent.SetActive(false);
        }
    }

    void SentenceFinish()
    {
        // DialogueText.text = "";
        // CharacterName.text = "";
        // characterHolder.SetActive(false);
        // dialogueParent.SetActive(false);
        // foreach (GameObject sprite in Sentences[Index-1].sprites)
        // {
        //     sprite.SetActive(false);
        // }
        CleanUp();
        if (CC != null) {
            CC.isPlaying = true;
            CC.numberGoesUp();
        }
    }

    public void inputName(string s) {
        setName = s;
        Debug.Log(setName);
    }
}