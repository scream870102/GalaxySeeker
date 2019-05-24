﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Jetpack : Item {
    /// <summary>max capacity of gas</summary>
    public float capacityOfGas;
    public float currentCapacityOfGas;
    /// <summary>gas consumption rate per second</summary>
    public float consumptionRate;
    /// <summary>gas recover rate per second</summary>
    public float recoverRate;
    /// <summary>how many force will add to player per second</summary>
    public int gasForce;
    /// <summary>jetpack cooldown between each time</summary>
    public float cooldown;
    //if jetpack can use right now
    private bool bCanUse;
    //store time of jetpack can use next time
    private float nextCanUseTime;
    // if using jetpack rightnow
    private bool bUsing;

    override protected void UsingItem ( ) {
        //if player hit jump button on air start to using jetPack if it can use right now
        if (Input.GetButtonDown ("Jump") && bCanUse && currentCapacityOfGas > 0f && !owner.IsOnGround) {
            BeginUsing ( );
            owner.IsFlying = true;
            bUsing = true;
            bCanUse = false;
        }
        //if player release button let pack enter cd
        else if (Input.GetButtonUp ("Jump") && bUsing) {
            ResetState ( );
        }
        //if player keep hold button keep add force to player
        else if (Input.GetButton ("Jump") && bUsing) {
            if (currentCapacityOfGas <= 0f)
                ResetState ( );
            //keep add force
            currentCapacityOfGas -= consumptionRate * Time.deltaTime;
            // owner.SetVelocity (new Vector2 (0f, 0f));
            // owner.AddForce (new Vector2 (0f, gasForce * Time.deltaTime), ForceMode2D.Force);
        }
        //add gas to jetpack
        else {
            currentCapacityOfGas += recoverRate * Time.deltaTime;
            currentCapacityOfGas = Mathf.Clamp (currentCapacityOfGas, 0f, capacityOfGas);

        }
        //if over cd time make pack can use again
        if (Time.time > nextCanUseTime && !bCanUse && !bUsing) {
            bCanUse = true;
        }
        //if gas is full and in can use state tell player can swith item
        if (bCanUse && currentCapacityOfGas >= capacityOfGas)
            AlreadUsed ( );

    }

    //when player release button make jetpack enter cd
    private void ResetState ( ) {
        bUsing = false;
        nextCanUseTime = Time.time + cooldown;
        owner.IsFlying = false;
    }

    //reset currentGas to max
    override protected void Reset ( ) {
        currentCapacityOfGas = capacityOfGas;
    }

    //enable backpack sprite
    override protected void Init ( ) {
        sr.enabled = true;
        currentCapacityOfGas = capacityOfGas;
        bCanUse = true;
        owner.Stats.flyingGasForce.baseValue = gasForce;
    }
}
