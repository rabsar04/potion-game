using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sentence
{
    public string name;      // Name of the character
    public List<string> dialogue;  // Dialogue text
    public List<Sprites> sprites;
    public AudioSource audio;
    public float dialoguePause;
    public float dialogueSpeed;
}
