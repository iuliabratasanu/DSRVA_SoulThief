using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{

    public SceneOrientation orientationManager;
    public void LoadScene(string sceneName)
    {
        orientationManager.SetResolutionAndOrientation(sceneName);
        SceneManager.LoadScene(sceneName);
    }
}

