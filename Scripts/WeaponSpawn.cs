﻿using UnityEngine;
using System.Collections;

public class WeaponSpawn : MonoBehaviour {

    //--------------------------------
    // 1 - Designer variables
    //--------------------------------

    public Transform weaponPrefab;

    public float procRate = 0.25f; //Cooldown before another shot can be fired

    //--------------------------------
    // 2 - Cooldown
    //--------------------------------

    private float procCooldown;

    void Start()
    {
        procCooldown = 0f;
    }

    void Update()
    {
        if (procCooldown > 0)
        {
            procCooldown -= Time.deltaTime;
        }
    }

    //--------------------------------
    // 3 - Shooting from another script
    //--------------------------------

    public void Attack(bool isEnemy)
    { //Creates a projectile with its components
        if (CanAttack)
        { //Cooldown has reached zero
            procCooldown = procRate;

            // Create a new projectile
            var weaponTransform = Instantiate(weaponPrefab) as Transform;

            // Assign position
            weaponTransform.position = transform.position;

            // The is enemy property
            pWeapon proc = weaponTransform.gameObject.GetComponent<pWeapon>();
            if (proc != null)
            {
                proc.isEnemyWeapon = isEnemy;
            }

            // Make the weapon shot always towards it
			ProjMovement move = weaponTransform.gameObject.GetComponent<ProjMovement>();
            if (move != null)
            {
                move.direction = this.transform.right; // towards in 2D space is the right of the sprite (CHANGE THIS TO THE DIRECTION THE PLAYER FACES)
            }
        }
    }

    public bool CanAttack
    { //Function that checks cooldown
        get{
            return procCooldown <= 0f;
        }
    }
}