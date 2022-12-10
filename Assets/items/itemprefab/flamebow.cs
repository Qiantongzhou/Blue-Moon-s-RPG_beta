using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flamebow : Equipment
{
    public override Attributes attributeList =>transform.GetComponent<Attributes>();

    public override string Name =>"flamebow,god";

    public override string Description => "god tier";

    public override kind GetKinds()
    {
        return kind.normal;
    }

    // Start is called before the first frame update
   
}
