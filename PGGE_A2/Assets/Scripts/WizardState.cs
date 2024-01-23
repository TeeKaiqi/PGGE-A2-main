using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGGE.Patterns;


public enum WizardStateType
{
    MOVEMENT = 0,
    ATTACK,
    RELOAD,
}
public class WizardState : FSMState
{
    protected Wizard mWizard = null;

    public WizardState(Wizard wizard)
        : base()
    {
        mWizard = wizard;
        mFsm = mWizard.mFsm;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

public class WizardState_MOVEMENT : WizardState
{
    public WizardState_MOVEMENT(Wizard wizard) : base(wizard)
    {
        mId = (int)(WizardStateType.MOVEMENT);
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();

        // For Student ---------------------------------------------------//
        // Implement the logic of player movement. 
        //----------------------------------------------------------------//
        // Hint:
        //----------------------------------------------------------------//
        // You should remember that the logic for movement
        // has already been implemented in PlayerMovement.cs.
        // So, how do we make use of that?
        // We certainly do not want to copy and paste the movement 
        // code from PlayerMovement to here.
        // Think of a way to call the Move method. 
        //
        // You should also
        // check if fire buttons are pressed so that 
        // you can transit to ATTACK state.

        mWizard.Move();

        for (int i = 0; i < mWizard.mAttackButtons.Length; ++i)
        {
            if (mWizard.mAttackButtons[i])
            {
                if (mWizard.mBulletsInMagazine > 0)
                {
                    WizardState_ATTACK attack = (WizardState_ATTACK)mFsm.GetState((int)WizardStateType.ATTACK);

                    attack.AttackID = i;
                    mWizard.mFsm.SetCurrentState(
                        (int)WizardStateType.ATTACK);
                }
                else
                {
                    Debug.Log("No more ammo left");
                }
            }
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}

public class WizardState_ATTACK : WizardState
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

    public WizardState_ATTACK(Wizard wizard) : base(wizard)
    {
        mId = (int)(WizardStateType.ATTACK);
    }

    public override void Enter()
    {
        mWizard.mAnimator.SetBool(mAttackName, true);
    }
    public override void Exit()
    {
        mWizard.mAnimator.SetBool(mAttackName, false);
    }
    public override void Update()
    {
        base.Update();

        Debug.Log("Ammo count: " + mWizard.mAmunitionCount + ", In Magazine: " + mWizard.mBulletsInMagazine);
        if (mWizard.mBulletsInMagazine == 0 && mWizard.mAmunitionCount > 0)
        {
            mWizard.mFsm.SetCurrentState((int)WizardStateType.RELOAD);
            return;
        }

        if (mWizard.mAmunitionCount <= 0 && mWizard.mBulletsInMagazine <= 0)
        {
            mWizard.mFsm.SetCurrentState((int)WizardStateType.MOVEMENT);
            mWizard.NoAmmo();
            return;
        }

        if (mWizard.mAttackButtons[mAttackID])
        {
            mWizard.mAnimator.SetBool(mAttackName, true);
            mWizard.Fire(AttackID);
        }
        else
        {
            mWizard.mAnimator.SetBool(mAttackName, false);
            mWizard.mFsm.SetCurrentState((int)WizardStateType.MOVEMENT);
        }
    }
}
public class WizardState_RELOAD : WizardState
{
    public float ReloadTime = 3.0f;
    float dt = 0.0f;
    public int previousState;
    public WizardState_RELOAD(Wizard wizard) : base(wizard)
    {
        mId = (int)(WizardStateType.RELOAD);
    }

    public override void Enter()
    {
        mWizard.mAnimator.SetTrigger("Reload");
        mWizard.Reload();
        dt = 0.0f;
    }
    public override void Exit()
    {
        if (mWizard.mAmunitionCount > mWizard.mMaxAmunitionBeforeReload)
        {   
            mWizard.mBulletsInMagazine += mWizard.mMaxAmunitionBeforeReload;
            mWizard.mAmunitionCount -= mWizard.mBulletsInMagazine;
        }
        else if (mWizard.mAmunitionCount > 0 && mWizard.mAmunitionCount < mWizard.mMaxAmunitionBeforeReload)
        {
            mWizard.mBulletsInMagazine += mWizard.mAmunitionCount;
            mWizard.mAmunitionCount = 0;
        }
    }

    public override void Update()
    {
        dt += Time.deltaTime;
        if (dt >= ReloadTime)
        {
            mWizard.mFsm.SetCurrentState((int)WizardStateType.MOVEMENT);
        }
    }

    public override void FixedUpdate()
    {
    }
}