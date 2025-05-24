using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    public MainMenuController mmc;
    public List<Events> events;

    private int Index = 0;
    private bool nextEvent = false;

    void Start()
    {
        nextEvent = false;
        EventType();
    }

    void Update()
    {
        if (nextEvent) {
            EventType();
        }
    }

    void EventType() {
        try {
            nextEvent = false;
            events[Index].currentEvent.SetActive(true);
            if (events[Index].isDialogue) {
                events[Index].currentEvent.GetComponent<DialogueController>().isActive = true;
            }
            StartCoroutine(WaitForEventCompletion());
        } catch (ArgumentOutOfRangeException ex) {
            Debug.Log("Index Out of Range: " + ex);
            mmc.LoadScene();
        }
    }

    IEnumerator WaitForEventCompletion() {
        while (events[Index].currentEvent.activeSelf) {
            yield return null;
        }

        if (events[Index].isDialogue && events[Index].currentEvent.GetComponent<DialogueController>().CC != null) {
            while (events[Index].currentEvent.GetComponent<DialogueController>().CC.isPlaying) {
                yield return null;
            }
        }

        Index++;
        nextEvent = true;
        Debug.Log(Index + " " + nextEvent);
    }
}