using UnityEngine;
using UnityEngine.SceneManagement;

public class TapToStart : MonoBehaviour
{
    void Update()
    {
        // Pour mobile : touche sur l'écran
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            LoadNextScene();
        }

        // Pour test sur PC : clic de souris
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("Game"); // Remplace par le nom exact de ta scène
    }
}