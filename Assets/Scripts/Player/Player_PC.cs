using System.Collections.Generic;
using UnityEngine;

public class Player_PC : MonoBehaviour
{
    public static Player_PC Instance;
    [SerializeField] private List<PC_Slots> slots;

    private void Start()
    {
        Instance = this;
        
        slots = new List<PC_Slots>();
        
        for (int i = 0; i < 6; i++)
        {
            slots.Add(new PC_Slots());
        }
    }

    public void Add_Pokemon(Pokemon pokemon)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (!slots[i].is_full())
            {
                slots[i].Add_Pokemon(pokemon);
                break;
            }
        }
    }
}

public class PC_Slots
{
    private Pokemon[,] slots;

    public PC_Slots()
    {
        slots = new Pokemon[6, 6];

        Pokemon empty = GeneratePokemon.Generate(-1, 0);
        
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                slots[x, y] = empty;
            }
        }
    }

    public void Add_Pokemon(Pokemon pokemon)
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                if (slots[x, y].PokemonName == "Empty")
                {
                    slots[x, y] = pokemon;
                    break;
                }
            }
        }
    }

    public bool is_full()
    {
        for (int x = 0; x < 6; x++)
        {
            for (int y = 0; y < 6; y++)
            {
                if (slots[x, y].PokemonName == "Empty")
                {
                    return false;
                }
            }
        }

        return true;
    }
}
