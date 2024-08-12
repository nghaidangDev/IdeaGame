using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    PokemonBase _base;
    int level;

    public int Hp { get; set; }

    public List<Move> Moves { get; set; }


    //khởi tạo một Pokémon với các đòn tấn công mà nó có thể học được ở cấp độ hiện tại, giới hạn tối đa là 4 đòn.
    public Pokemon (PokemonBase pBase, int pLevel)
    {
        _base = pBase;
        level = pLevel;
        Hp = pBase.MaxHp;

        Moves = new List<Move>();
        foreach (var move in _base.LearnableMoves)
        {
            if (move.Level <= level)
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
        get { return Mathf.FloorToInt((_base.Attack * level) / 100f) + 5; }
    }

    public int Defence
    {
        get { return Mathf.FloorToInt((_base.Defence * level) / 100f) + 5; }
    }

    public int SpAttack
    {
        get { return Mathf.FloorToInt((_base.SpAttack * level) / 100f) + 5; }
    }

    public int SpDefence
    {
        get { return Mathf.FloorToInt((_base.SpDefence * level) / 100f) + 5; }
    }

    public int Speed
    {
        get { return Mathf.FloorToInt((_base.Speed * level) / 100f) + 5; }
    }

    public int MaxHp
    {
        get { return Mathf.FloorToInt((_base.MaxHp * level) / 100f) + 10; }
    }
}
