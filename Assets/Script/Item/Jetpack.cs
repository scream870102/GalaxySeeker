using Eccentric.Utils;

using UnityEngine;
public class Jetpack : Item {
    //max capacity of gas
    [SerializeField] float capacityOfGas = 5f;
    //gas consumption rate per second
    [SerializeField] float consumptionRate = 1.25f;
    //gas recover rate per second
    [SerializeField] float recoverRate = 0.75f;
    //how many force will add to player per second
    [SerializeField] int gasForce = 150;
    //the gas force to init to player when it pressed every time
    [SerializeField] int initGasForce = 2500;
    //jetpack cooldown between each time
    [SerializeField] float cooldown = 0.3f;
    [Header ("Monitor")]
    [SerializeField] float currentCapacityOfGas = 0f;
    Timer timer = null;
    //if jetpack can use right now
    bool bCanUse = false;
    // if using jetpack rightnow
    bool bUsing = false;
    // ref for ptc
    ParticleSystem ptc = null;

    override protected void UsingItem ( ) {
        //if player hit jump button on air start to using jetPack if it can use right now
        if (Input.GetButtonDown ("Jump") && bCanUse && currentCapacityOfGas > 0f && !owner.IsOnGround) {
            BeginUsing ( );
            owner.IsFlying = true;
            bUsing = true;
            bCanUse = false;
            Debug.Log ("Add force");
            owner.AddForce (new Vector2 (0, initGasForce), ForceMode2D.Force);
        }
        //if player release button let pack enter cd
        else if (Input.GetButtonUp ("Jump") && bUsing) {
            ResetState ( );
        }
        //if player keep hold button keep add force to player
        else if (Input.GetButton ("Jump") && bUsing) {
            ptc.Play ( );
            if (currentCapacityOfGas <= 0f)
                ResetState ( );
            currentCapacityOfGas -= consumptionRate * Time.deltaTime;
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

    }

    //when player release button make jetpack enter cd
    void ResetState ( ) {
        ptc.Stop ( );
        bUsing = false;
        timer.Reset ( );
        owner.IsFlying = false;
    }

    //reset currentGas to max
    override protected void Reset ( ) {
        currentCapacityOfGas = capacityOfGas;
        ptc.Stop ( );
    }

    //enable backpack sprite
    override protected void Init ( ) {
        sr.enabled = true;
        currentCapacityOfGas = capacityOfGas;
        bCanUse = true;
        owner.Props.FlyingGasForce = gasForce;
        ptc = GetComponent<ParticleSystem> ( );
        timer = new Timer (cooldown);
    }
}
