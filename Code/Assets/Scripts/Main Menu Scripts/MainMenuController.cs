using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject button;
    public static bool fullScreen = true;
    public string sceneName = "";
    // Start is called before the first frame update
    void Start()
    {
        button.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        button.GetComponent<Image>().color = fullScreen ? new Color(1f, 1f, 1f, 0f) : new Color(1f, 1f, 1f, 1f);
    }

    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        fullScreen = !fullScreen;
        Debug.Log(fullScreen);
    }

    public void LoadScene()
    {
        if (sceneName == null) {
            sceneName = "MainMenu";
        }
        SceneManager.LoadScene("Scenes/" + sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
