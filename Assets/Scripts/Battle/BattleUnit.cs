using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] BaseCharacter _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;


    public Character Character { get; set; }
    public void SetUp()
    {
        Character = new Character(_base, level);
        GetComponent<Image>().sprite = Character.Base.BattleSprite;

        if (isPlayerUnit) 
        {
            
            //GetComponent<Image>().sprite = character.Base.BattleSprite; 
        }
        else 
        {
            transform.localScale = new Vector3(-1f, 1f);
            //GetComponent<Image>().sprite = character.Base.BattleSprite; 
        }
        
    }

}
