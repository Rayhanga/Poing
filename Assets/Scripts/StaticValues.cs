using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticValues{
    private static string P1 = "Biru", P2 = "BOT (MEDIUM)";
    private static int MS = 5;
    private static bool P2BOT = true;
    private static float VBGM = 1f, DIFF = 3.75f;

    public static string P1NAME{
        get{
            return P1;
        }
        set{
            P1 = value;
        }
    }
    public static string P2NAME{
        get{
            return P2;
        }
        set{
            P2 = value;
        }
    }
    public static int MAXSCORE{
        get{
            return MS;
        }
        set{
            MS = value;
        }
    }
    public static float VOLUME_BGM{
        get{
            return VBGM;
        }
        set{
            VBGM = value;
        }
    }
    public static float DIFFICULTY{
        get{
            return DIFF;
        }
        set{
            DIFF = value;
        }
    }
    public static bool P2ISBOT{
        get{
            return P2BOT;
        }
        set{
            P2BOT = value;
        }
    }
}
