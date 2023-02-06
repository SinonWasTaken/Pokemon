using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class GeneratePokemon
{
    public static Pokemon Generate(int natID, int level)
    {
        PokemonData data = Pokemon_Database.Instance.Get_Pokemon(natID);

        return new Pokemon(data, "", gen_Unique_ID(data), level, get_Nature(), new IV(get_IV_Value(),get_IV_Value(),get_IV_Value(),get_IV_Value(),get_IV_Value(),get_IV_Value()), new EV(0,0,0,0,0,0), Pokemon.Status_Effects.None, get_Ability(data), get_Moves(data, level), null, "1");
    }

    private static string gen_Unique_ID(PokemonData data)
    {
        return create_Name_ID(data.PokemonName) + get_Time_As_String();
    }

    private static string get_Time_As_String()
    {
        string time = DateTime.Now.ToString();
        return time.Replace("/", "").Replace(" ", "").Replace(":", "").Replace("PM", "").Replace("AM", "");
    }
    
    private static string create_Name_ID(string name)
    {
        int random = Random.Range(0, name.ToCharArray().Length);

        string name_char = "";

        for (int i = 0; i < random; i++)
        {
            int index = Random.Range(0, name.ToCharArray().Length);
            name_char += name.ToCharArray()[i];
        }

        return name_char;
    }
    
    private static Pokemon.Nature get_Nature()
    {
        int rand = Random.Range(0, Enum.GetNames(typeof(Pokemon.Nature)).Length);
        return (Pokemon.Nature) Enum.GetValues(typeof(Pokemon.Nature)).GetValue(rand);
    }

    private static PokemonData.Abilities get_Ability(PokemonData data)
    {
        float rand = Random.Range(0, 100);

        if (rand <= 45)
        {
            return data.AbilityOne;
        }
        else if (rand <= 90)
        {
            if (data.AbilityTwo != PokemonData.Abilities.None)
            {
                return data.AbilityTwo;
            }
            else
            {
                return data.AbilityOne;
            }
        }
        else
        {
            return data.HiddenAbility;
        }
    }
    
    private static List<Move> get_Moves(PokemonData data, int level)
    {
        List<Move> moveSet = new List<Move>();

        if (data.Moveset != null)
        {
            if (data.Moveset.Learn != null)
            {
                for (int i = 0; i < data.Moveset.Learn.Length; i++)
                {
                    string name = data.Moveset.Learn[i].Split(':')[0];
                    int learn_level = Convert.ToInt32(data.Moveset.Learn[i].Split(':')[1]);

                    if (level >= learn_level)
                    {
                        if (moveSet.Count < 4)
                        {
                            MoveData moveData = Move_Database.Instance.get_move(name);
                            moveSet.Add(new Move(moveData, moveData.BasePp, moveData.BasePp));
                        }
                        else
                        {
                            int random = Random.Range(0, 5);
                            MoveData moveData = Move_Database.Instance.get_move(name);
                            moveSet[random] = new Move(moveData, moveData.BasePp, moveData.BasePp);
                        }
                    }
                }
            }
        }

        return moveSet;
    }

    private static int get_IV_Value()
    {
        return Random.Range(1, 33);
    }
}