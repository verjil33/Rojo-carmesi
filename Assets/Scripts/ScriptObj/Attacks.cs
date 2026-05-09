using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks
{
    public BaseAttack Base { get; set; }
    public int PP { get; set; }

    public Attacks(BaseAttack pBase)
    {
        Base = pBase;
        PP = pBase.PP;
    }


}
