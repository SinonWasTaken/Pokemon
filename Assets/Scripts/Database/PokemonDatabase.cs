using System.Collections.Generic;
using UnityEngine;

public class PokemonDatabase : MonoBehaviour
{
    public static List<PokemonData> data;

    public static bool editor_foldOpen;

    public void Awake()
    {
        generate_pokemon_data();
    }

    private void generate_pokemon_data()
    {
        data = new List<PokemonData>
        {
            //Regional
            new PokemonData("Victini", "Victory Pokémon", "When it shares the infinite energy it creates, that being's entire body will be overflowing with power.", 494, 0, PokemonData.PokemonType.Psychic, PokemonData.PokemonType.Fire, 0.4f, 4.0f, PokemonData.Abilities.Victory_Star,PokemonData.Abilities.None, PokemonData.Abilities.None, new Training_Data(Training_Data.EV_Yield_Type.HP, 3, 3, 100, 270, Training_Data.Growth_Rate.Slow), new Breeding_Data(Breeding_Data.Egg_Groups.Undiscovered, Breeding_Data.Egg_Groups.None, Breeding_Data.Gender.Genderless, new int[]{ 30584, 30840}, 0), new Base_Stats(100), -1, -1),
            new PokemonData("Snivy", "Grass Snake Pokémon", "Being exposed to sunlight makes its movements swifter. It uses vines more adeptly than its hands.", 495, 1, PokemonData.PokemonType.Grass, PokemonData.PokemonType.None, 0.6f, 8.1f, PokemonData.Abilities.Overgrow, PokemonData.Abilities.None, PokemonData.Abilities.Contrary, new Training_Data(Training_Data.EV_Yield_Type.SPD, 1, 45, 50, 62, Training_Data.Growth_Rate.MediumSlow), new Breeding_Data(Breeding_Data.Egg_Groups.Field, Breeding_Data.Egg_Groups.Grass, Breeding_Data.Gender.Normal, new int[] { 4884, 5140}, 87.5f), new Base_Stats(45,45,55,45,55,63), 17, 496),
            new PokemonData("Servine", "Grass Snake Pokémon", "When it gets dirty, its leaves can't be used in photosynthesis, so it always keeps itself clean",496, 2, PokemonData.PokemonType.Grass, PokemonData.PokemonType.None, 0.8f, 16.0f, PokemonData.Abilities.Overgrow, PokemonData.Abilities.None, PokemonData.Abilities.Contrary, new Training_Data(Training_Data.EV_Yield_Type.SPD, 2, 45, 50, 145, Training_Data.Growth_Rate.MediumSlow), new Breeding_Data(Breeding_Data.Egg_Groups.Field, Breeding_Data.Egg_Groups.Grass, Breeding_Data.Gender.Normal, new int[] {4884, 5140}, 87.5f), new Base_Stats(60, 60, 75, 60, 75, 83), 36, 497),
            new PokemonData("Serperior", "Regal Pokémon", "It only gives its all against strong opponents who are not fazed by the glare from Serperior’s noble eyes.", 497, 3, PokemonData.PokemonType.Grass, PokemonData.PokemonType.None, 3.3f,63.0f, PokemonData.Abilities.Overgrow, PokemonData.Abilities.None, PokemonData.Abilities.Contrary, new Training_Data(Training_Data.EV_Yield_Type.SPD, 3,45,50,238,Training_Data.Growth_Rate.MediumSlow), new Breeding_Data(Breeding_Data.Egg_Groups.Field, Breeding_Data.Egg_Groups.Grass, Breeding_Data.Gender.Normal, new int[] {4884, 5140}, 87.5f), new Base_Stats(75,75,95,75,95,113), -1, -1),
            new PokemonData("Tepig", "Fire Pig Pokémon", "It loves to eat roasted berries, but sometimes it gets too excited and burns them to a crisp.", 498, 4, PokemonData.PokemonType.Fire, PokemonData.PokemonType.None, 0.5f, 9.9f, PokemonData.Abilities.Blaze, PokemonData.Abilities.None, PokemonData.Abilities.Thick_Fat, new Training_Data(Training_Data.EV_Yield_Type.HP, 1, 45,50,61, Training_Data.Growth_Rate.MediumSlow), new Breeding_Data(Breeding_Data.Egg_Groups.Field, Breeding_Data.Egg_Groups.None, Breeding_Data.Gender.Normal, new int[] {4884,5140}, 87.5f), new Base_Stats(65,63,45,45,45,45), 17,499),
            new PokemonData("Pignite", "Fire Pig Pokémon", "The more it eats, the more fuel it has to make the fire in its stomach stronger. This fills it with even more power.", 499, 5, PokemonData.PokemonType.Fire, PokemonData.PokemonType.Fighting, 1.0f, 55.5f, PokemonData.Abilities.Blaze, PokemonData.Abilities.None, PokemonData.Abilities.Thick_Fat, new Training_Data(Training_Data.EV_Yield_Type.ATTK, 2, 45,50,146, Training_Data.Growth_Rate.MediumSlow), new Breeding_Data(Breeding_Data.Egg_Groups.Fairy,Breeding_Data.Egg_Groups.None, Breeding_Data.Gender.Normal, new int[] {4884, 5140}, 87.5f), new Base_Stats(90,93,55,70,55,55), 36, 500),
            //National
        };
        
        //Only do this once, bad things may happen if it is run after the game starts
        load_pokemon_moveset();
    }

    private void load_pokemon_moveset()
    {
        //Loop through each Pokemon
        
        //Load the current pokemon's moveset from the resources file
            //Check the Egg, Tm, Learned, Special, TM, Tutor folders and see if the pokemon has a file in it
    }

    public static PokemonData getPokemon(int natID)
    {
        foreach (PokemonData pdata in data)
        {
            if (pdata.national_id == natID)
                return pdata;
        }

        return null;
    }
}
