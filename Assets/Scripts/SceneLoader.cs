using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour {
    //public string SceneToLoad;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown(KeyCode.Joystick1Button2) || (Input.GetKeyDown(KeyCode.Joystick2Button2)))
            SceneManager.LoadScene("Game");

    }
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
