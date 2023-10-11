using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CoverState : IState
{
    private EnemyReferences enemyReferences;
    private StateMachine stateMachine;
    public CoverState(EnemyReferences enemyReferences){
        this.enemyReferences = enemyReferences;

        stateMachine = new StateMachine();
        //States
        var enemyShoot = new ShootingState(enemyReferences);
        var enemyDelay = new DelayState(1f);
        var enemyReload = new ReloadState(enemyReferences);

        //Transitions
        At (enemyShoot, enemyReload, () => enemyReferences.shooter.ShouldReload());
        At (enemyReload, enemyDelay, () => !enemyReferences.shooter.ShouldReload());
        At (enemyDelay, enemyShoot, () => enemyDelay.IsDone());

        // StartState
        stateMachine.SetState(enemyShoot);
        //Functions
        void At (IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from,to,condition);
        void Any (IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to, condition);
    }

    public void OnEnter(){
        enemyReferences.animator.SetBool("combat", true);
    }
    public void OnExit(){
        enemyReferences.animator.SetBool("combat", false);
    }
    public void Tick(){
        stateMachine.Tick();
    }
    public Color GizmoColor(){
        return stateMachine.GetGizmoColor();
    }
}
