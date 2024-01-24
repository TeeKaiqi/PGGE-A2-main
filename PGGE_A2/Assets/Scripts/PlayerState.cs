using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE.Patterns;

public enum PlayerStateType //enumeration that defines the different player states = moving, attacking and reloading
{
    MOVEMENT = 0,
    ATTACK,
    RELOAD,
}

public class PlayerState : FSMState //base class for all the player states that inherits from the FSMState
{
    protected Player mPlayer = null; //reference to the player

    public PlayerState(Player player) : base() //initialise the player state with a reference to the player
    {
        mPlayer = player;
        mFsm = mPlayer.mFsm;
    }

    public override void Enter() //This function is called when entering this state
    {
        base.Enter();
    }
    public override void Exit() //this function is called when exiting this state
    {
        base.Exit();
    }
    public override void Update() //the update function will be called every frame when in this particular state
    {
        base.Update();
    }
    public override void FixedUpdate() //this function will be called every fixed frame while while in this state
    {
        base.FixedUpdate();
    }
}

public class PlayerState_MOVEMENT : PlayerState //this is the state that represents the player movement
{
    public PlayerState_MOVEMENT(Player player) : base(player) //instantiates the movement state with a reference to the player
    {
        mId = (int)(PlayerStateType.MOVEMENT);
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update() //this is the refactored update function, the original code that checks the whether the player is attacking has been moved
    {
        base.Update();
        mPlayer.Move();
        CheckAndChangeState();
    }

    private void CheckAndChangeState() //logic that checks for player attacking was put into its own function that is called in the update function
    {
        for (int i = 0; i < mPlayer.mAttackButtons.Length; ++i)
        {
            if (mPlayer.mAttackButtons[i]) //if the player is attacking
            {
                if (mPlayer.mBulletsInMagazine > 0) //and if the player's bullets is more than 0, then it will become the attack state
                {
                    PlayerState_ATTACK attack = (PlayerState_ATTACK)mFsm.GetState((int)PlayerStateType.ATTACK);
                    attack.AttackID = i;
                    mPlayer.mFsm.SetCurrentState((int)PlayerStateType.ATTACK); //set the state to attack state
                }
                else
                {
                    Debug.Log("No more ammo left"); //debug that there is no more ammo left
                }
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

public class PlayerState_ATTACK : PlayerState //state that represents the player attacking
{
    private int mAttackID = 0;
    private string mAttackName;

    public int AttackID
    {
        get
        {
            return mAttackID;
        }
        set
        {
            mAttackID = value;
            mAttackName = "Attack" + (mAttackID + 1).ToString();
        }
    }

    public PlayerState_ATTACK(Player player) : base(player) //initialise the attack state with a reference to the player
    {
        mId = (int)(PlayerStateType.ATTACK);
    }

    public override void Enter()
    {
        mPlayer.mAnimator.SetBool(mAttackName, true);
    }
    public override void Exit()
    {
        mPlayer.mAnimator.SetBool(mAttackName, false);
    }
    public override void Update() //update function that was refactored by moving the logic to check ammo and attack into its own function
    {
        base.Update();
        CheckAmmoAndAttack();
    }

    private void CheckAmmoAndAttack()
    {
        Debug.Log("Ammo count: " + mPlayer.mAmunitionCount + ", In Magazine: " + mPlayer.mBulletsInMagazine);
        if (mPlayer.mBulletsInMagazine == 0 && mPlayer.mAmunitionCount > 0)
        {
            mPlayer.mFsm.SetCurrentState((int)PlayerStateType.RELOAD); //transitions to the reload state if the payer is out of bullets but there is still ammunition
            return;
        }

        if (mPlayer.mAmunitionCount <= 0 && mPlayer.mBulletsInMagazine <= 0)
        {
            mPlayer.mFsm.SetCurrentState((int)PlayerStateType.MOVEMENT); //transitions to the movement state when there is no bullets and no ammunition
            mPlayer.NoAmmo(); //calls the noammo function 
            return;
        }

        if (mPlayer.mAttackButtons[mAttackID]) //if the attack button was pressed, perform the attack
        {
            mPlayer.mAnimator.SetBool(mAttackName, true);
            mPlayer.Fire(AttackID);
        }
        else
        {
            mPlayer.mAnimator.SetBool(mAttackName, false); //else set the attack animation to false and transition to movement state
            mPlayer.mFsm.SetCurrentState((int)PlayerStateType.MOVEMENT);
        }
    }
}

public class PlayerState_RELOAD : PlayerState //state that represents the player reloading
{
    public float ReloadTime = 3.0f;
    float dt = 0.0f;
    public int previousState;
    public PlayerState_RELOAD(Player player) : base(player) //initialises the reload state with a reference to the player
    {
        mId = (int)(PlayerStateType.RELOAD);
    }

    public override void Enter()
    {
        mPlayer.mAnimator.SetTrigger("Reload"); //set trigger for the reload animation 
        mPlayer.Reload();
        dt = 0.0f;
    }
    public override void Exit()
    {
        if (mPlayer.mAmunitionCount > mPlayer.mMaxAmunitionBeforeReload) //login that calculates the bullets in the magazine and ammunition ased on the reload
        {
            mPlayer.mBulletsInMagazine += mPlayer.mMaxAmunitionBeforeReload;
            mPlayer.mAmunitionCount -= mPlayer.mBulletsInMagazine;
        }
        else if (mPlayer.mAmunitionCount > 0 && mPlayer.mAmunitionCount < mPlayer.mMaxAmunitionBeforeReload)
        {
            mPlayer.mBulletsInMagazine += mPlayer.mAmunitionCount;
            mPlayer.mAmunitionCount = 0;
        }
    }

    public override void Update()
    {
        dt += Time.deltaTime;
        if (dt >= ReloadTime)
        {
            mPlayer.mFsm.SetCurrentState((int)PlayerStateType.MOVEMENT);
        }
    }

    public override void FixedUpdate()
    {
    }
}
