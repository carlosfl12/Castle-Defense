 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;
public class SceneHandler : MonoBehaviour
{
    public Canvas parentCanvas;

    public void SelectScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void Back(Canvas canvas) {
        canvas.enabled = false;
        parentCanvas.enabled = true;
    }

    public void OpenOptionsMenu(Canvas canvas) {
        parentCanvas.enabled = false;
        canvas.enabled = true;
    }
    public void ExitGame() {
        Application.Quit();
    }
}
