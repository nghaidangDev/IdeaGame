﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Tạo 1 asset mới trên Menu Unity
[CreateAssetMenu(fileName = "Pokemon", menuName = "Pokemon/Create new Pokemon")]
public class PokemonBase : ScriptableObject  //Quản lý dữ liệu tĩnh
{
    [SerializeField] private string name;

    [TextArea]
    [SerializeField] private string description; //mô tả

    [SerializeField] private Sprite fontSprite;
    [SerializeField] private Sprite backSprite;

    [SerializeField] private PokemonTypes type1;
    [SerializeField] private PokemonTypes type2;

    //Base Stats
    [SerializeField] private int maxHp;
    [SerializeField] private int attack;
    [SerializeField] private int defence;
    [SerializeField] private int spAttack;
    [SerializeField] private int spDefence;
    [SerializeField] private int speed;

    [SerializeField] private List<LearnableMove> learnableMoves;


    public string Name
    {
        get { return name; }
    }

    public string Description
    {
        get { return description; }
    }

    public Sprite FontSprite
    {
        get { return fontSprite; }
    }

    public Sprite BackSprite
    {
        get { return backSprite; }
    }

    public PokemonTypes Type1
    {
        get { return type1; }
    }

    public PokemonTypes Type2
    {
        get { return type2; }
    }

    public int MaxHp
    {
        get { return maxHp; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int Defence
    {
        get { return defence; }
    }

    public int SpAttack
    {
        get { return spAttack; }
    }

    public int SpDefence
    {
        get { return spDefence; }
    }

    public int Speed
    {
        get { return speed; }
    }

    public List<LearnableMove> LearnableMoves
    {
        get { return learnableMoves; }
    }

}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] private MoveBase moveBase;
    [SerializeField] private int level;

    public MoveBase Base
    {
        get { return moveBase; }
    }

    public int Level
    {
        get { return level; }
    }
}

public enum PokemonTypes
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon
}

public class TypeChart
{
    static float[][] chart =
    {
        //                  NOR  FIR   WAT   ELE   GRA  ICE FIG  POI
        /*NOR*/ new float[] {1f,  1f,   1f,  1f,   1f,  1f, 1f,  1f},
        /*FIR*/ new float[] {1f, 0.5f, 0.5f, 1f,   2f,  2f, 1f,  1f},
        /*WAT*/ new float[] {1f,  2f,  0.5f, 2f,  0.5f, 1f, 1f,  1f},
        /*ELE*/ new float[] {1f,  1f,   2f, 0.5f, 0.5f, 2f, 1f,  1f},
        /*GRS*/ new float[] {1f, 0.5f,  2f,  2f,  0.5f, 1f, 1f, 0.5f},
        /*POI*/ new float[] {1f,  1f,   1f,  1f,   1f,  1f, 1f,  1f}
    };

    public static float GetEffectiveness(PokemonTypes attackType, PokemonTypes defenseType)
    {
        if (attackType == PokemonTypes.None || defenseType == PokemonTypes.None)
        {
            return 1;
        }

        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
    }
}
