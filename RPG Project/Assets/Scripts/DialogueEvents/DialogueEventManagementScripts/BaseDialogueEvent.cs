using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class BaseDialogueEvent
{
    public enum triggerActions
    {
        ONACTION,
        ONTOUCH,
        AUTOSTART,
        PARALLEL
    }

    public triggerActions triggerAction;

    public bool switch1;
    public bool switch2;

    //graphic for face

    public enum VoiceTones
    {
        ONE,
        TWO,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        TEN
    }


    public VoiceTones voicetone;

    public List<BaseEvent> eventsBefore = new List<BaseEvent>();

    [TextArea(1, 5)]  //Dialog text
    public string[] dialogText;
       
    public List<BaseEvent> eventsAfter = new List<BaseEvent>();
    
    public enum processIfTrueOptions
    {
        ANY,
        ALL
    }
    public processIfTrueOptions processOptions;
    public List<int> processIfTrue = new List<int>();

    public enum dontProcessIfTrueOptions
    {
        ANY,
        ALL
    }
    public dontProcessIfTrueOptions dontProcessOptions;
    public List<int> dontProcessIfTrue = new List<int>();

    public List<int> markAsTrue = new List<int>();
    public List<int> markAsFalse = new List<int>();
}
