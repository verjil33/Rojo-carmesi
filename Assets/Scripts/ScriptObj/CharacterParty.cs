using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterParty : MonoBehaviour
{
    [SerializeField] List<Character> characters;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var character in characters)
        {
            character.Init();
        }
    }

    public Character GetHealtyCharacter()
    {
        return characters.Where(x => x.CurHp > 0).FirstOrDefault();
    }
    
}
