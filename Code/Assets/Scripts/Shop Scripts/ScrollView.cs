using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    public Transform contentContainer;
    public float scrollSpeed = 1.0f;
    
    private float minY;
    private float maxY;
    private Vector3 _initialMousePosition;
    private Vector3 _initialContentPosition;

    void Start()
    {
        CalculateBounds();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _initialMousePosition = Input.mousePosition;
            _initialContentPosition = contentContainer.position;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 deltaMousePosition = Input.mousePosition - _initialMousePosition;
            Vector3 newPosition = _initialContentPosition + new Vector3(0, deltaMousePosition.y * scrollSpeed, 0);

            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            contentContainer.position = newPosition;
        }
    }

    void CalculateBounds()
    {
        if (contentContainer.childCount == 0)
        {
            minY = maxY = 0;
            return;
        }

        float minYPos = float.MaxValue;
        float maxYPos = float.MinValue;

        foreach (Transform child in contentContainer)
        {
            float childMinY = child.position.y;
            float childMaxY = child.position.y;

            minYPos = Mathf.Min(minYPos, childMinY);
            maxYPos = Mathf.Max(maxYPos, childMaxY);
        }

        // Set the minY and maxY based on the calculated positions
        minY = minYPos;
        maxY = maxYPos;
    }

}
