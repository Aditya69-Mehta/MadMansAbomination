using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] CinemachineVirtualCamera virtualCam;

    [SerializeField] int zoomFOV = 40;
    [SerializeField] float rayRange = 100f;
    [SerializeField] int damage = 2;

    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject hitEffectVFX;

    float defaultFOV = 60f;
    float defaultMouseSensitivity = 15f;
    float zoomedMouseSensitivity = 10f;

    FirstPersonController firstPersonController;

    void Start(){
        firstPersonController = GetComponentInParent<FirstPersonController>();
    }


    void Update(){
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
        WeaponZoom();
    }

    void Shoot()
    {
        PlayMuzzleFlash();
        ProcessRaycast();
    }

    void PlayMuzzleFlash(){
        muzzleFlashVFX.Play();
    }

    void ProcessRaycast(){
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, rayRange))
        {
            Debug.Log(hit.transform.name);

            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

        }
        else { return; }
    }

    void CreateHitImpact(RaycastHit hit)
    {
        GameObject vfx = Instantiate(hitEffectVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(vfx,.5f);
    }

    void WeaponZoom(){
        if(Input.GetKey(KeyCode.Mouse1)){
            virtualCam.m_Lens.FieldOfView = zoomFOV;
            firstPersonController.RotationSpeed = zoomedMouseSensitivity;
        }else{
            virtualCam.m_Lens.FieldOfView = defaultFOV;
            firstPersonController.RotationSpeed = defaultMouseSensitivity;
        }
    }
}
