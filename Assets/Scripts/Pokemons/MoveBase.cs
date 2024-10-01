using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] private string name;

    [TextArea]
    [SerializeField] private string description;

    [SerializeField] PokemonTypes type;

    [SerializeField] private int power;
    [SerializeField] private int accuracy;
    [SerializeField] private int pp;

    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public PokemonTypes Type
    {
        get { return type; }
    }

    public int Power
    {
        get { return power; }
    }

    public int Accuracy
    {
        get { return accuracy; }    
    }

    public int PP
    {
        get { return pp; }
    }

    public bool IsSpecial
    {
        get
        {
            if (type == PokemonTypes.Fire || type == PokemonTypes.Water || type == PokemonTypes.Grass
                || type == PokemonTypes.Ice || type == PokemonTypes.Electric || type == PokemonTypes.Dragon)
            {
                return true;
            }else
            {
                return false;
            }
        }
    }
}
