using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create new Character")]
public class BaseCharacter : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite battleSprite;
    [SerializeField] Sprite dialogueSprite;
    [SerializeField] Sprite worldSprite;

    [SerializeField] int nivel;
    [SerializeField] Class baseClass;
    [SerializeField] Class[] clases;

    [SerializeField] AttackType[] resistencias;
    [SerializeField] AttackType[] debilidades;
    [SerializeField] AttackType[] invulnerabilidad;
    //BaseStats
    [SerializeField] int maxHp;

    //la formula para los modificadores es: el valor - 10 y el resultado / 2, redondeado hacia abajo. Ejemplo: 15-10= 5/2=2,5 que es 2 el mod. Y los int descartan centesimos.

    [SerializeField] int fuerza;
    [SerializeField] int destreza;
    [SerializeField] int constitucion;
    [SerializeField] int inteligencia;
    [SerializeField] int sabiduria;
    [SerializeField] int carisma;

    [SerializeField] Caracteristica modDañoFisico;
    [SerializeField] Caracteristica modDañoMagico;

    [SerializeField] int spellSlots1;
    [SerializeField] int spellSlots2;
    [SerializeField] int spellSlots3;



    [SerializeField] List<AtaquesUsables> ataquesUsables;

    public int LifeClass()
    {
        if (baseClass == Class.Barbaro) //vida de 12
        {
            if ((constitucion - 10) / 2 >= 0)
            {
                return maxHp = 12 + constitucion;
            }
            else return maxHp = 12;
        }
        else if (baseClass == Class.Guerrero || baseClass == Class.Explorador || baseClass == Class.Paladin) //vida de 10
        {
            if ((constitucion - 10) / 2 >= 0)
            {
                return maxHp = 10 + constitucion;
            }
            else return maxHp = 10;
        }
        else if (baseClass == Class.Artificiero || baseClass == Class.Bardo || baseClass == Class.Brujo || baseClass == Class.Clerigo || baseClass == Class.Druida || baseClass == Class.Monje || baseClass == Class.Picaro) // vida de 8
        {
            if ((constitucion - 10) / 2 >= 0)
            {
                return maxHp = 8 + constitucion;
            }
            else return maxHp = 8;
        }
        else //vida de 6
        {
            if ((constitucion - 10) / 2 >= 0)
            {
                return maxHp = 6 + constitucion;
            }
            else return maxHp = 6;
        }
    }




    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite BattleSprite
    {
        get { return battleSprite; }
    }

    public Sprite DialogueSprite
    {
        get { return dialogueSprite; }
    }
    public Sprite WorldSprite
    {
        get { return worldSprite; }
    }

    public AttackType[] Resistencia
    {
        get { return resistencias; }
    } 
    public AttackType[] Debilidades
    {
        get { return debilidades; }
    }
    public AttackType[] Invulnerabilidad
    {
        get { return invulnerabilidad; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public int Fuerza
    {
        get { return fuerza; }
    }

    public int Destreza
    {
        get { return destreza; }
    }
    public int Constitucion
    {
        get { return constitucion; }
    }

    public int Inteligencia
    {
        get { return inteligencia; }
    }

    public int Sabiduria
    {
        get { return sabiduria; }
    } 
    public int Carisma
    {
        get { return carisma; }
    }
    public Caracteristica ModDañoFisico
    {
        get { return modDañoFisico; }
    }
    public Caracteristica ModDañoMagico
    {
        get { return modDañoMagico; }
    }


    public List<AtaquesUsables> AtaquesUsables
    {
        get { return ataquesUsables; }
    }

}


public class TypeChart
{
    //esto es con la formula de pokemon, capaz despues no sirve
    static float[][] chart =
    {
        /*nor*/ new float[] {1f, 1f, 1f},        
        /*fire*/ new float[] {1f, 1f, 1f},
        /*water*/new float[] {1f, 1f, 1f}
    };
    public static float GetEffectiveness(AttackType attackType, AttackType defenseType)
    {       
        if(attackType  == AttackType.Ninguno || defenseType == AttackType.Ninguno)        
            return 1;
        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;
        return chart[row][col];        
    }


}



[System.Serializable]
public class AtaquesUsables
{
    [SerializeField] BaseAttack moveBase;
    [SerializeField] int level;

    public BaseAttack MoveBase
    {
        get { return moveBase; }
    }
    public int Level
    {
        get { return level; }
    }


    
}

