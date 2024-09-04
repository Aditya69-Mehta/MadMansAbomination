using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionHandler : MonoBehaviour
{
    StarterAssets.StarterAssetsInputs starterAssetsInputs;
    // bool gamePaused = false;


    void Start(){
        starterAssetsInputs = FindObjectOfType<StarterAssets.StarterAssetsInputs>();
        // ProcessPause();
        UnPause();

    }

    void Update(){
    }

    void UnPause(){
        // gamePaused = false;

        starterAssetsInputs.cursorLocked = true;
        starterAssetsInputs.cursorInputForLook = true;
        starterAssetsInputs.SetCursorState(starterAssetsInputs.cursorLocked);
    }

    public void ProcessPause(){
        // gamePaused = true;

        starterAssetsInputs.cursorLocked = false;
        starterAssetsInputs.cursorInputForLook = false;
        starterAssetsInputs.SetCursorState(starterAssetsInputs.cursorLocked);
    }

    public void ReloadScene(){
        Time.timeScale = 1;
        Debug.Log("Reloading");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        UnPause();
    }

    public void MainMenu(){
        Application.Quit();
    }
}
