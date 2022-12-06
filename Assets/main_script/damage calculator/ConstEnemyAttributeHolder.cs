using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstEnemyAttributeHolder
{                                                                        //range //stoprange//speed//health//attackdamage//healthregen
    public static readonly CreatureAttribute fatDragon = new CreatureAttribute(10f, 3f, 1f, 100f, 40f, 0f);
    public static readonly CreatureAttribute rhino = new CreatureAttribute(100f, 3f, 3f, 40f, 20f, 0f);
    public static readonly CreatureAttribute titan = new CreatureAttribute(100f, 3f, 1f, 500f, 30f, 0f);
    public static readonly CreatureAttribute arachnid = new CreatureAttribute(100f, 3f, 2f, 40f, 10f, 0f);

    public static readonly CreatureAttribute[] creatures = {fatDragon, rhino, titan, arachnid};
}
