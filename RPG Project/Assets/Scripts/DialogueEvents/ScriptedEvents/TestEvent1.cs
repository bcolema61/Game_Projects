using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent1 : BaseScriptedEvent
{
    public void TestMethod()
    {
        StartCoroutine(StartMoving());
    }

    IEnumerator StartMoving()
    {
        yield return (StartCoroutine(MoveRight(this.gameObject.transform, .5f, 1)));
    }
}
