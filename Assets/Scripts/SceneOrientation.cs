using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneOrientation : MonoBehaviour
{

    private void Start()
    {
        SetResolutionAndOrientation(SceneManager.GetActiveScene().name);
    }


    public void SetResolutionAndOrientation(string sceneName)
    {
        if (sceneName == "Main Menu")
        {

            Screen.SetResolution(720, 1270, true);
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else if (sceneName == "Level 0")
        {

            Screen.SetResolution(1270, 720, true);
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }
    }
}
