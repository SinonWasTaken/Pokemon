using System;
using Random = UnityEngine.Random;

public static class GeneratePokemon
{
    public static Pokemon generatePokemon(int natID, int level)
    {
        PokemonData data = PokemonDatabase.getPokemon(natID);

        return new Pokemon(data, "", genUniqueID(), level, genRandomNature(), genIVStats(), new EV_Stats());;
    }

    #region generate_Random_Nature
    private static Pokemon.Nature genRandomNature()
    {
       int rand =  Random.Range(0, Enum.GetValues(typeof(Pokemon.Nature)).Length);
       return (Pokemon.Nature) (rand);
    }
    #endregion
    
    #region generate_Unique_ID
    private static long genUniqueID()
    {
        return 0;
    }
    #endregion
    
    #region generating IV Stats
    private static IV_Stats genIVStats(int hp, int attk, int def, int spattk, int spdef, int spd)
    {
        return new IV_Stats(hp, attk, def, spattk, spdef, spd);
    }
    
    private static IV_Stats genIVStats()
    {
        return genIVStats(0,0,0,0,0,0);
    }
    #endregion
}
