using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponScript : MonoBehaviour
{
    private Vector2 mouseWorldPosition;
    private Vector2 weaponAimDirection;
    private float weaponRotationAngle = 0f;
    private GameObject weaponRotationOrigin;
    private void Awake()
    {
        weaponRotationOrigin = GameObject.FindGameObjectWithTag("WeaponRotationOrigin");
    }
    private void Update()
    {
        GetMouseWorldPosition();     
        CalculateWeaponRotationAngle();
    }
    private void FixedUpdate()
    {
        SetWeaponRotation();
    }
    private void SetWeaponRotation()
    {
        weaponRotationOrigin.transform.eulerAngles = new Vector3(0f, 0f, weaponRotationAngle);
        
        if (weaponRotationAngle > 90f && weaponRotationAngle < 180f)
        {
            weaponRotationOrigin.transform.GetChild(1).gameObject.transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
        }
        else if(weaponRotationAngle > -180f && weaponRotationAngle < -90f)
        {
            weaponRotationOrigin.transform.GetChild(1).gameObject.transform.localRotation = Quaternion.Euler(180f, 0f, 0f);
        }
        else
        {
            weaponRotationOrigin.transform.GetChild(1).gameObject.transform.localRotation = Quaternion.Euler(0f,0f,0f);
        }

    }
    private void GetMouseWorldPosition()
    {
        mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
    }
    private void CalculateWeaponRotationAngle()
    {
        weaponAimDirection = (mouseWorldPosition - (Vector2)transform.position).normalized;
        weaponRotationAngle = Mathf.Atan2(weaponAimDirection.y, weaponAimDirection.x) * Mathf.Rad2Deg;
    }

    //1 - Rotates the "hand" and weapon towards the mouse icon - partially done

}