using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadState : IState
{
    private EnemyReferences enemyReferences;
    public ReloadState(EnemyReferences enemyReferences){
        this.enemyReferences = enemyReferences;
    }
    public void OnEnter(){
        Debug.Log("Start Reload");
        enemyReferences.animator.SetFloat("cover", 1);
        enemyReferences.animator.SetTrigger("reload");
    }

    public void OnExit(){
        Debug.Log("Stop Reload");
        enemyReferences.animator.SetFloat("cover", 0);
    }
    public void Tick(){

    }
    public Color GizmoColor(){
        return Color.gray;
    }
}
