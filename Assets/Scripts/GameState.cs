using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameState 
{
    public enum UserStates
    {
        cultist,
        otherSpell,
        thirdSpell,
        fourthSpell,
        fifthSpell
    }
    public static UserStates curUserState;

}
