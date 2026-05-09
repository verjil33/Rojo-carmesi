using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "Character/Create new Attack")]
public class BaseAttack : ScriptableObject
{
    [SerializeField] string name;
    [TextArea]
    [SerializeField] string description;

    [SerializeField] bool isMagic;
    [SerializeField] AttackType type;
    [SerializeField] List<Class> classType;

    [SerializeField] int poder;
    [SerializeField] Caracteristica modDaño;
    [SerializeField] int accuracy;
    [SerializeField] int pp;
    [SerializeField] int nivel;

    public string Name
    {
        get { return name; }
    }
    public string Description
    {
        get { return description; }
    }
    public bool IsMagic
    {
        get { return isMagic; }
    }
    public AttackType Type
    {
        get { return type; }
    }
    public int Power
    {
        get { return poder; }
    }
    public int Accuracy
    {
        get { return accuracy; }
    }
    public int PP
    {
        get { return pp; }
    }
    public int Nivel
    {
        get { return nivel; }
    }

    public Caracteristica ModDaño
    {
        get { return modDaño; }
    }

}
