using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayState : IState
{
    private float waitForSeconds;
    private float deadLine;
    public DelayState(float waitForSeconds){
        this.waitForSeconds = waitForSeconds;
    }
    public void OnEnter(){
        deadLine = Time.time + waitForSeconds;
    }
    public void OnExit(){
        Debug.Log("EnemyDelay onExit");
    }
    public void Tick(){

    }
    public Color GizmoColor(){
        return Color.white;
    }
    public bool IsDone(){
        return Time.time >= deadLine;
    }
}
