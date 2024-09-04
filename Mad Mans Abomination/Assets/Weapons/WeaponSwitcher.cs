using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera vCam;
    [SerializeField] int currWeaponIndex = 0;

    void Start()
    {
        SetWeaponActive();
    }

    void Update(){
        int previousWeapon = currWeaponIndex;

        ProcessKeyInput();
        ProcessScrollInput();

        if(previousWeapon != currWeaponIndex) SetWeaponActive();

    }

    void ProcessKeyInput(){

        if(Input.GetKeyDown(KeyCode.Alpha1)) currWeaponIndex = 1;
        else if(Input.GetKeyDown(KeyCode.Alpha2)) currWeaponIndex = 2;
        else if(Input.GetKeyDown(KeyCode.Alpha3)) currWeaponIndex = 3;

    }

    void ProcessScrollInput(){
        if(Input.GetAxis("Mouse ScrollWheel") > 0){
            if(currWeaponIndex >= transform.childCount){
                currWeaponIndex = 0;
            }else{
                currWeaponIndex++;
            }
        }else if(Input.GetAxis("Mouse ScrollWheel") < 0){
            if(currWeaponIndex <= 0){
                currWeaponIndex = transform.childCount;
            }else{
                currWeaponIndex--;
            }
        }

    }

    void SetWeaponActive(){
        int weaponIndex = 1;
        foreach(Transform weapon in transform){
            if(weaponIndex == currWeaponIndex){
                weapon.gameObject.SetActive(true);
            }else{
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }


}
