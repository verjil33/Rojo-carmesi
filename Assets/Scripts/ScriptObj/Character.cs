using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public BaseCharacter Base { get; set; }
    public int Level { get; set; }
    public int CurHp { get; set; }

    public int curSlots1 { get; set; }
    public int curSlots2 { get; set; }
    public int curSlots3 { get; set; }

    public List<Attacks> Attacks { get; set; }
    public Character(BaseCharacter pBase, int pLevel)
    {
        Base = pBase;
        Level = pLevel;
        CurHp = MaxHp;
        Attacks = new List<Attacks>();
        foreach(var move in Base.AtaquesUsables)
        {
            if (move.Level <= Level) Attacks.Add(new Attacks(move.MoveBase));
        }
    }

    public int MaxHp
    {
        get { return Base.LifeClass(); }
    }

    public DamageDeatails TakeDamage(Attacks move, Character attacker)
    {
        int modificadorDaño = 0;
        float _resistencia = 1f;
        float _critico = 1f;

        if (Random.value * 100f <= 6.25f) _critico = 2f;


        
        /*
        float type = TypeChart.GetEffectiveness(move.Base.Type, this.Base.GetType1) * TypeChart.GetEffectiveness(move.Base.Type, this.Base.GetType2)        
        //estilo pokemon!!!
        float modifiers = Random.Range(0.85f, 1f) * type;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        int daño = move.Base.Power + modificadorDaño;

        CurHp -= daño;
        if(CurHp <= 0)
        {
            CurHp = 0;
            return true;
        }

        return false;
        */

        //ver si tiene resistencias


        if (move.Base.IsMagic)
        {
            //hay que ver si usa carisma, sabiduria o inteligencia.

            switch (move.Base.ModDaño)
            {
                case Caracteristica.Inteligencia:
                    modificadorDaño = (attacker.Base.Inteligencia - 10) / 2;
                    break;
                case Caracteristica.Sabiduria:
                    modificadorDaño = (attacker.Base.Sabiduria - 10) / 2;
                    break;
                case Caracteristica.Carisma:
                    modificadorDaño = (attacker.Base.Carisma - 10) / 2;
                    break;
            }

        }
        else
        {
            //fuerza o destreza
            switch (move.Base.ModDaño)
            {
                case Caracteristica.Fuerza:
                    modificadorDaño = (attacker.Base.Fuerza - 10) / 2;
                    break;
                case Caracteristica.Destreza:
                    modificadorDaño = (attacker.Base.Destreza - 10) / 2;
                    break;
            }
        }
        /*
        Debug.Log($"Es daño magico: {move.Base.IsMagic}");
        Debug.Log($"Daño base del arma: {move.Base.Power}");
        Debug.Log($"Caracteriscita del daño: {move.Base.ModDaño}");
        Debug.Log($"Modificador de daño: {modificadorDaño}");
        */




        for (int i = 0; i < Base.Resistencia.Length; i++)
        {
            if (move.Base.Type == Base.Resistencia[i]) _resistencia = 0.5f; //es resistente a ese daño
            else if (move.Base.Type == Base.Debilidades[i]) _resistencia = 2f; //es debil a ese daño
            else if (move.Base.Type == Base.Invulnerabilidad[i]) _resistencia = 0f; //es invulnerable a ese daño
            else _resistencia = 1f; //recibe daño normal
        }

        var DamageDeatails = new DamageDeatails()
        {
            Resist = _resistencia,
            Critico = _critico,
            Fainted = false
        };


        //Debug.Log("resistencia? " + _resistencia);
        int daño = (int)((move.Base.Power + modificadorDaño)*_resistencia * _critico);
        //Debug.Log("Daño total:" + daño);
        CurHp -= daño;
        if (CurHp <= 0)
        {
            CurHp = 0;
            DamageDeatails.Fainted = true;
        }

        return DamageDeatails;
    }

    public Attacks GetRandomMove()
    {
        int r = Random.Range(0, Attacks.Count);
        return Attacks[r];
    }

}

public class DamageDeatails
{   
    public bool Fainted { get; set; }

    public float Critico { get; set; }
    public float Resist { get; set; }
}
