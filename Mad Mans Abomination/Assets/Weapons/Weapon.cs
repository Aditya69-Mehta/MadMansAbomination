using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Weapon : MonoBehaviour
{

    [Header("Referenced GameObjects")]
    [SerializeField] Camera FPCamera;
    [SerializeField] CinemachineVirtualCamera virtualCam;
    [SerializeField] ParticleSystem muzzleFlashVFX;
    [SerializeField] GameObject hitEffectVFX;

    [Header("Weapon Settings")]
    [SerializeField] int zoomFOV = 40;
    [SerializeField] float weaponRange = 100f;
    [SerializeField] int damage = 2;
    [SerializeField] float shootCooldown = .5f;
    [SerializeField] AmmoType ammoType;


    float defaultFOV = 60f;
    float defaultMouseSensitivity = 15f;
    float zoomedMouseSensitivity = 10f;
    bool canShoot = true;

    FirstPersonController firstPersonController;
    Ammo ammoSlot;


    void OnEnable(){
        canShoot = true;
    }

    void OnDisable() {
        virtualCam.m_Lens.FieldOfView = defaultFOV;
        firstPersonController.RotationSpeed = defaultMouseSensitivity;
    }

    void Start(){
        firstPersonController = GetComponentInParent<FirstPersonController>();
        ammoSlot = GetComponentInParent<Ammo>();
    }


    void Update(){
        if(Input.GetButtonDown("Fire1") && canShoot){
            StartCoroutine(Shoot());
        }
        WeaponZoom();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if(ammoSlot.GetCurrentAmmo(ammoType) > 0){
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.DecreseAmmo(ammoType);
        }
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    void PlayMuzzleFlash(){
        muzzleFlashVFX.Play();
    }

    void ProcessRaycast(){
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, weaponRange))
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
