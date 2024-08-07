using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{

    public void ChangeScene()
    {
        StartCoroutine(LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }
    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // Si estamos en una build, cerrar la aplicaci�n
            Application.Quit();
#endif
    }
}