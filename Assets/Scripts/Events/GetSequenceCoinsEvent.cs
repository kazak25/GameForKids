using System.Collections;
using System.Collections.Generic;
using SimpleEventBus.Events;
using UnityEngine;

public class GetSequenceCoinsEvent : EventBase
{
    public PigController PigController { get; private set; }

    public GetSequenceCoinsEvent(PigController pigController)
    {
        PigController = pigController;
    }
}
