using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogue
{
    public Chat NextChat()
    {
        return default;
    }

    public Chat PreviousChat()
    {
        return default;
    }
}
