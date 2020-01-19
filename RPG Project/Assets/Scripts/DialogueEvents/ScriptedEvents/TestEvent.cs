using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent : BaseScriptedEvent
{

    public void TestMethod()
    {
        //StartCoroutine(StartMoving());
    }

    IEnumerator StartMoving()
    {
        yield return (StartCoroutine(MoveRandom(playerTransform, baseMoveSpeed, 3)));

        SavePosition(thisGameObject);
        //yield return (StartCoroutine(MoveLeftDown(thisTransform, (baseMoveSpeed*3), 3)));
    }

    void TestBattle()
    {
        CallBattle(1, "Battle");
    }
}
