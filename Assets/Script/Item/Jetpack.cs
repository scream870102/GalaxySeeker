﻿using Eccentric.Utils;
using E = Eccentric;

using UnityEngine;
namespace GalaxySeeker.Item {
    public class Jetpack : Item {
        //max capacity of gas
        [SerializeField] float capacityOfGas = 5f;
        //gas consumption rate per second
        [SerializeField] float consumptionRate = 1.25f;
        //gas recover rate per second
        [SerializeField] float recoverRate = .75f;
        //how many force will add to player per second
        [SerializeField] int gasForce = 150;
        //the gas force to init to player when it pressed every time
        [SerializeField] int initGasForce = 2500;
        //jetpack cooldown between each time
        [SerializeField] float cooldown = .3f;
        [SerializeField] float jitterOffset = .2f;
        [SerializeField] float jitterHz = .1f;
        [SerializeField] SpriteRenderer fireRender = null;
        [SerializeField] float fireJitterOffset = .05f;
        [SerializeField] float fireJitterHz = .1f;
        [Header ("Monitor")]
        [SerializeField] float currentCapacityOfGas = 0f;
        //if jetpack can use right now
        bool bCanUse = false;
        // if using jetpack rightnow
        bool bUsing = false;
        // ref for ptc
        //ParticleSystem ptc = null;
        Vector3 initPackPos = Vector3.zero;
        Vector3 initFirePos = Vector3.zero;
        Timer jitterTimer = null;
        Timer fireJitterTimer = null;
        public event System.Action<float> OnGasChanged = null;

        override protected void UsingItem ( ) {
            Jitter ( );
            //if player hit jump button on air start to using jetPack if it can use right now
            if (Input.GetButtonDown ("Jump") && bCanUse && currentCapacityOfGas > 0f && !Owner.IsOnGround) {
                BeginUsing ( );
                Owner.IsFlying = true;
                bUsing = true;
                bCanUse = false;
                Owner.AddForce (new Vector2 (0, initGasForce), ForceMode2D.Force);
            }
            //if player release button let pack enter cd
            else if (Input.GetButtonUp ("Jump") && bUsing) {
                ResetState ( );
            }
            //if player keep hold button keep add force to player
            else if (Input.GetButton ("Jump") && bUsing) {
                //ptc.Play ( );
                if (currentCapacityOfGas <= 0f)
                    ResetState ( );
                currentCapacityOfGas -= consumptionRate * Time.deltaTime;
                UsingJitter ( );
            }
            //add gas to jetpack
            else {
                currentCapacityOfGas += recoverRate * Time.deltaTime;
                currentCapacityOfGas = Mathf.Clamp (currentCapacityOfGas, 0f, capacityOfGas);

            }
            //if over cd time make pack can use again
            if (timer.IsFinished && !bCanUse && !bUsing) {
                bCanUse = true;
            }
            //if gas is full and in can use state tell player can switch item
            if (bCanUse && currentCapacityOfGas >= capacityOfGas)
                AlreadyUsed ( );
            if (OnGasChanged != null)
                OnGasChanged (currentCapacityOfGas / capacityOfGas);
        }

        //when player release button make jetpack enter cd
        void ResetState ( ) {
            //ptc.Stop ( );
            bUsing = false;
            timer.Reset ( );
            Owner.IsFlying = false;
            fireRender.enabled = false;
        }

        //reset currentGas to max
        override protected void Reset ( ) {
            currentCapacityOfGas = capacityOfGas;
            //ptc.Stop ( );
        }

        //enable backpack sprite
        override protected void Init ( ) {
            this.type = EItemType.JETPACK;
            SpriteRenderer.enabled = true;
            currentCapacityOfGas = capacityOfGas;
            bCanUse = true;
            Owner.Props.FlyingGasForce = gasForce;
            //ptc = GetComponent<ParticleSystem> ( );
            timer = new Timer (cooldown);
            jitterTimer = new Timer (jitterHz);
            initPackPos = this.transform.localPosition;
            fireRender.enabled = false;
            initFirePos = fireRender.transform.localPosition;
            fireJitterTimer = new Timer (fireJitterHz);
        }

        void Jitter ( ) {
            if (jitterTimer.IsFinished) {
                jitterTimer.Reset ( );
                transform.localPosition = initPackPos + E.Math.RandomVec3 (jitterOffset);
            }
        }

        void UsingJitter ( ) {
            if (fireJitterTimer.IsFinished) {
                fireRender.enabled = true;
                fireJitterTimer.Reset ( );
                fireRender.transform.localPosition = initFirePos + E.Math.RandomVec3 (fireJitterOffset);
            }
        }
    }

}
