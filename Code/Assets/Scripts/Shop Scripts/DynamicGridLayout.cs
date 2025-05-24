using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DynamicGridLayout : MonoBehaviour
{
    public float cellSize = 1f;       // Size of each cell (assuming square cells)
    public float horizontalSpacing = 0.1f; // Horizontal spacing between cells
    public float verticalSpacing = 0.1f;   // Vertical spacing between cells
    public Vector2 startPosition = Vector2.zero; // Starting position for the grid in 2D

    void OnValidate()
    {
        ArrangeGrid();
    }

    void ArrangeGrid()
    {
        int childCount = transform.childCount;
        if (childCount == 0) return;

        // Ensure at least 2 columns
        int columns = 2;
        int rows = Mathf.CeilToInt((float)childCount / columns);

        // Calculate width and height for each cell including spacing
        float totalCellWidth = cellSize + horizontalSpacing;
        float totalCellHeight = cellSize + verticalSpacing;

        // Position the objects
        for (int i = 0; i < childCount; i++)
        {
            int row = i / columns;
            int column = i % columns;

            // Calculate the position for the current child
            Vector3 position = new Vector3(
                startPosition.x + column * totalCellWidth,
                startPosition.y - row * totalCellHeight,  // Note the negative sign for y to go down
                transform.GetChild(i).localPosition.z   // Keep the existing z position
            );

            // Assign the calculated position to the child
            transform.GetChild(i).localPosition = position;
        }
    }
}