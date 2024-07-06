using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState<Bot>
{
     //Attack -> di ve dich
    public void OnEnter(Bot t)
    {
        t.SetDestination(LevelManager.Ins.FinishPoint);
    }
    //dieu kien de khong di ve dich, chuyen trang thai
    public void OnExecute(Bot t)
    {
        if (t.BrickCount == 0)
        {
            t.ChangeState(new PatrolState());
        }
    }
    public void OnExit(Bot t)
    {
    }
}
