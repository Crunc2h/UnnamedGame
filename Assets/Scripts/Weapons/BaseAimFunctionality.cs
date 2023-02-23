using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseAimFunctionality : MonoBehaviour
{
    private Vector2 targetWorldPosition;
    public Vector2 weaponAimDirection;
    public float weaponRotationAngle = 0f;
    private GameObject weaponSlotOne;
    private GameObject weaponSlotTwo;
    public GameObject weapon;

    private void Awake()
    {
        weaponSlotOne = transform.GetChild(0).gameObject;
        weaponSlotTwo = transform.GetChild(1).gameObject;
        if(weaponSlotOne.transform.GetChild(0) != null)
        {
            weapon = weaponSlotOne.transform.GetChild(0).gameObject;
        }
        else
        {
            weapon = weaponSlotTwo.transform.GetChild(0).gameObject;
        }
    }
    private void FixedUpdate()
    {
        SetWeaponRotation();
    }
    private void SetWeaponRotation()
    {
        CalculateWeaponRotationAngleNTargetPosition();
        if(gameObject.CompareTag("Player") || gameObject.name == "SecurityGuard")
        {
            if (weaponRotationAngle >= -90f && weaponRotationAngle <= 90f)
            {
                weaponSlotOne.transform.localRotation = Quaternion.Euler(0f, 0f, weaponRotationAngle);
                if (weapon.transform.parent.gameObject != weaponSlotOne)
                {
                    weapon.transform.SetParent(weaponSlotOne.transform, false);
                }
                if (weaponRotationAngle <= 90f && weaponRotationAngle >= 0f)
                {
                    weapon.GetComponent<SpriteRenderer>().sortingOrder = 0;
                }
                else if (weaponRotationAngle < 0f && weaponRotationAngle >= -90f)
                {
                    weapon.GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
            }
            else if (weaponRotationAngle > 90f || weaponRotationAngle < -90f)
            {
                weaponSlotTwo.transform.localRotation = Quaternion.Euler(180f, 0f, -weaponRotationAngle);
                if (weapon.transform.parent.gameObject != weaponSlotTwo)
                {
                    weapon.transform.SetParent(weaponSlotTwo.transform, false);
                }
                if (weaponRotationAngle > 90f && weaponRotationAngle <= 180f)
                {
                    weapon.GetComponent<SpriteRenderer>().sortingOrder = 0;
                }
                else if (weaponRotationAngle >= -180f && weaponRotationAngle < -90f)
                {
                    weapon.GetComponent<SpriteRenderer>().sortingOrder = 2;
                }
            }
        }
        else if(gameObject.name == "BasicTurret" || gameObject.name == "InvisibleSniperTurret")
        {
            if(weaponRotationAngle >= -180f && weaponRotationAngle <= 0f)
            {
                weaponSlotOne.transform.localRotation = Quaternion.Euler(0f, 0f, weaponRotationAngle);
                weapon.GetComponent<SpriteRenderer>().sortingOrder = 2;
                if (weapon.transform.parent.gameObject != weaponSlotOne)
                {
                    weapon.transform.SetParent(weaponSlotOne.transform, false);
                }
            }
            else
            {
                weaponSlotTwo.transform.localRotation = Quaternion.Euler(180f, 0f, -weaponRotationAngle);
                weapon.GetComponent<SpriteRenderer>().sortingOrder = 0;
                if (weapon.transform.parent.gameObject != weaponSlotTwo)
                {
                    weapon.transform.SetParent(weaponSlotTwo.transform, false);
                }
            }
        }
        


    }
    private void CalculateWeaponRotationAngleNTargetPosition()
    {
        if(gameObject.CompareTag("Player"))
        {
            targetWorldPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            weaponAimDirection = (targetWorldPosition - (Vector2)transform.position).normalized;
            weaponRotationAngle = Mathf.Atan2(weaponAimDirection.y, weaponAimDirection.x) * Mathf.Rad2Deg;
            GetComponent<BasePlayerMovement>().weaponAngle = weaponRotationAngle;
            GetComponent<BasePlayerMovement>().CalculateAimDirection();
        }
        else if(gameObject.CompareTag("Mob"))
        {
            targetWorldPosition = (Vector2)GameObject.FindGameObjectWithTag("Player").transform.position;
            weaponAimDirection = (targetWorldPosition - (Vector2)transform.position).normalized;
            weaponRotationAngle = Mathf.Atan2(weaponAimDirection.y, weaponAimDirection.x) * Mathf.Rad2Deg;
            GetComponentInChildren<BaseWeaponFunctionalityEnemy>().currentWeaponRotation = weaponRotationAngle;
        }
    }
   
}
