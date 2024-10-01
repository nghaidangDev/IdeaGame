using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonParty : MonoBehaviour
{
    [SerializeField] List<Pokemon> pokemons;

    private void Start()
    {
        foreach (var pokemon in pokemons)
        {
            pokemon.Init();
        }
    }
}
