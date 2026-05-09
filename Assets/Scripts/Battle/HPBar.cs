using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] GameObject healt;

    public void SetHP(float hpNormalized)
    {
        healt.transform.localScale = new Vector3(hpNormalized, 1f);
    }

    public IEnumerator SetHPSmooth(float newHP)
    {
        float curHP = healt.transform.localScale.x;
        float changeAmt = curHP - newHP;
        while (curHP - newHP > Mathf.Epsilon)
        {
            curHP -= changeAmt * Time.deltaTime;
            healt.transform.localScale = new Vector3(curHP, 1f);
            yield return null;
        }
        healt.transform.localScale = new Vector3(newHP, 1f);
    }

    
}
