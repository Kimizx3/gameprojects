using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyBrain : MonoBehaviour
{
    
    private EnemyReferences enemyReferences;
    private StateMachine stateMachine;
    void Start(){
        enemyReferences = GetComponent<EnemyReferences>();
        stateMachine = new StateMachine();
        // Only one area
        CoverArea coverArea = FindObjectOfType<CoverArea>();

        //States
        var runToCover = new RunToCover(enemyReferences, coverArea);
        var delayAfterRun = new DelayState(2f);
        var cover = new CoverState(enemyReferences);
        //Transitions
        At(runToCover, delayAfterRun, () => runToCover.HasArrivedDestination());
        At(delayAfterRun, cover, () => delayAfterRun.IsDone());
        //StartState
        stateMachine.SetState(runToCover);
        //Functions&Conditions
        void At(IState from, IState to, Func<bool> condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, Func<bool> condition) => stateMachine.AddAnyTransition(to,condition);
    }

    void Update(){
        stateMachine.Tick();
    }
    

    private void OnDrawGizmos(){
        if(stateMachine != null){
            Gizmos.color = stateMachine.GetGizmoColor();
            Gizmos.DrawSphere(transform.position + Vector3.up * 3, 0.4f);
        }
    }
}
