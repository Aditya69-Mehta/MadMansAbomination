using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    [SerializeField] SessionHandler sessionHandler;

    void Start(){
        gameOverCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DeathHandle(){
        Time.timeScale = 0;
        GetComponentInChildren<WeaponSwitcher>().enabled = false;
        gameOverCanvas.enabled = true;
        sessionHandler.ProcessPause();
        // Cursor.lockState = CursorLockMode.None;
    }
}
