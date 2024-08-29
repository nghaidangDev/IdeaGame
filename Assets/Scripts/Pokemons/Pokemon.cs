﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public PokemonBase Base { get; set; }
    public int Level { get; set; }

    public int Hp { get; set; }

    public List<Move> Moves { get; set; }


    //khởi tạo một Pokémon với các đòn tấn công mà nó có thể học được ở cấp độ hiện tại, giới hạn tối đa là 4 đòn.
    public Pokemon (PokemonBase pBase, int pLevel)
    {
        Base = pBase;
        Level = pLevel;
        Hp = MaxHp;

        Moves = new List<Move>();
        foreach (var move in Base.LearnableMoves)
        {
            if (move.Level <= Level)
            {
                Moves.Add(new Move(move.Base));
            }

            if (Moves.Count >= 4)
            {
                break;
            }
        }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5; }
    }

    public int Defence
    {
        get { return Mathf.FloorToInt((Base.Defence * Level) / 100f) + 5; }
    }

    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; }
    }

    public int SpDefence
    {
        get { return Mathf.FloorToInt((Base.SpDefence * Level) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 10; }
    }

    public int MaxHp
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; }
    }

    public DamageDetails TakeDamage(Move move, Pokemon attacker)
    {
        float critical = 1f;
        if (Random.value * 100f <= 6.25f)
            critical = 2f;

        float type = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1)
            * TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2);

        var damageDetails = new DamageDetails()
        {
            TypeEffectiveness = type,
            Critical = critical,
            Fainted = false
        };


        float modifiers = Random.Range(0.85f, 1f) * type * critical;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.Attack / Defence) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        Hp -= damage;
        if (Hp < 0)
        {
            Hp = 0;
            damageDetails.Fainted = true;
        }
        return damageDetails;
    }


    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);

        return Moves[r];
    }

    public class DamageDetails
    {
        public bool Fainted { get; set; }
        public float Critical { get; set; }
        public float TypeEffectiveness { get; set; }
    }
}
