using UnityEngine;
using Random = UnityEngine.Random;

public class Grass_Area : MonoBehaviour
{
    [SerializeField] private Grass_Data[] data;

    public bool Begin_Battle()
    {
        float rand = Random.Range(0, 100);

        if (rand <= 10)
        {
            return true;
        }

        return false;
    }

    public Pokemon Get_Pokemon()
    {
        return data[0].Get_Pokemon();
    }
}

[System.Serializable]
public class Grass_Data
{
    public enum Spawn_Time
    {
        Day,
        Night
    }

    [SerializeField] private Spawn_Time time;
    [SerializeField] private Pokemon_Grass_Data[] data;

    public Spawn_Time Time => time;
    public Pokemon_Grass_Data[] Data => data;

    public Pokemon Get_Pokemon()
    {
        float max = get_max_chance_value();

        float rand = Random.Range(0, max);

        float current = 0;
        
        for (int i = 0; i < data.Length; i++)
        {
            current += data[i].SpawnChance;

            if (rand <= current)
            {
                Pokemon pokemon = GeneratePokemon.Generate(data[i].PokemonNATID, Random.Range(data[i].MinMaxLevels[0], data[i].MinMaxLevels[1] + 1));
                return pokemon;
            }
        }

        return GeneratePokemon.Generate(data[data.Length - 1].PokemonNATID,
            Random.Range(data[data.Length - 1].MinMaxLevels[0], data[data.Length - 1].MinMaxLevels[1] + 1));;
    }

    private float get_max_chance_value()
    {
        float max = 0;

        for (int i = 0; i < data.Length; i++)
        {
            max += data[i].SpawnChance;
        }

        return max;
    }
}

[System.Serializable]
public class Pokemon_Grass_Data
{
    [SerializeField] private int pokemon_nat_id;
    [SerializeField] private int[] min_max_levels;
    [SerializeField] private float spawn_chance;

    public int PokemonNATID => pokemon_nat_id;
    public int[] MinMaxLevels => min_max_levels;
    public float SpawnChance => spawn_chance;
}
