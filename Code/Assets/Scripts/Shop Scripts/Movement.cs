using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Movement : MonoBehaviour
{
    public float moveHorizontal = 19.2f;
    public float moveVertical = 10f;
    // Speed of the camera movement
    public float smoothTime = 0.3f;

    // Internal variables to handle smooth transition
    private Vector3 targetPosition;
    private Vector3 velocity = Vector3.zero;

    public Vector2 minCameraPosition;
    public Vector2 maxCameraPosition;
    public static bool isLocked;
    public Vector2 customerScreen;

    public List<Vector2> screenVectors;

    // public TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        isLocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocked) {
            if (Input.GetKeyDown(KeyCode.W))
            {
                targetPosition.y += moveVertical;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                targetPosition.y -= moveVertical;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                targetPosition.x -= moveHorizontal;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                targetPosition.x += moveHorizontal;
            }

            float f = targetPosition.x;
            f = Mathf.Round(f * 10.0f) * 0.1f;

            if (targetPosition.x == 0f || targetPosition.x == -35.6f) {
                targetPosition.y = Mathf.Clamp(targetPosition.y, 0f, 0f);
            }

            targetPosition.x = Mathf.Clamp(targetPosition.x, minCameraPosition.x, maxCameraPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minCameraPosition.y, maxCameraPosition.y);
        }

        // Update the camera's position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        if (Reset.isConfirm) {
            isLocked = true;
            targetPosition.x = customerScreen.x;
            targetPosition.y = customerScreen.y;
            StartCoroutine(PauseforCustomer());
        }
    }

    IEnumerator PauseforCustomer() {
        yield return new WaitForSeconds(5);
        isLocked = false;
    }

    void OnDropdownValueChanged(TMP_Dropdown change) {
        targetPosition.x = screenVectors[change.value].x;
    }
}
