using System;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon_Database : MonoBehaviour
{
    public static Pokemon_Database Instance;

    [SerializeField] private List<PokemonData> data;

    [SerializeField] private GrowthRateData[] growthRates;
    
    private void Awake()
    {
        if(Instance != null && Instance != this)
            Destroy(gameObject);

        Instance = this;
        
        load_pokemon();
    }

    public int[] getGrowthRate(TrainingData.Growth_Rate rate)
    {
        for (int i = 0; i < growthRates.Length; i++)
        {
            if (rate == growthRates[i].Rate)
            {
                return growthRates[i].Exp;
            }
        }

        return null;
    }
    
    private void load_pokemon()
    {
        data = new List<PokemonData>()
        {
            new PokemonData("Empty", "Empty", "Empty", -1, -1, 0.0f, 0.0f, PokemonData.PokemonType.None, PokemonData.PokemonType.None, PokemonData.Abilities.None, PokemonData.Abilities.None, PokemonData.Abilities.None, null, null, null, -1, -1),
            
            //Regional
            new PokemonData("Victini", "Victory Pokémon", "When it shares the infinite energy it creates, that being's entire body will be overflowing with power.", 494, 0, 0.4f, 4.0f, PokemonData.PokemonType.Psychic, PokemonData.PokemonType.Fire, PokemonData.Abilities.Victory_Star,PokemonData.Abilities.None, PokemonData.Abilities.None, new TrainingData(TrainingData.EV_Yield_Type.HP, TrainingData.Growth_Rate.Slow, 3, 3, 100, 270), new BreedingData(BreedingData.Egg_Groups.Undiscovered, BreedingData.Egg_Groups.None, BreedingData.Gender.Genderless, new int[]{ 30584, 30840}, 0), new BaseStats(100, 100, 100, 100, 100, 100), -1, -1),
            
            new PokemonData("Snivy", "Grass Snake Pokémon", "Being exposed to sunlight makes its movements swifter. It uses vines more adeptly than its hands.", 495, 1, 0.6f, 8.1f, PokemonData.PokemonType.Grass, PokemonData.PokemonType.None, PokemonData.Abilities.Overgrow, PokemonData.Abilities.None, PokemonData.Abilities.Contrary, new TrainingData(TrainingData.EV_Yield_Type.SPD, TrainingData.Growth_Rate.MediumSlow, 1, 45, 50, 62), new BreedingData(BreedingData.Egg_Groups.Field, BreedingData.Egg_Groups.Grass, BreedingData.Gender.Normal, new int[] { 4884, 5140}, 87.5f), new BaseStats(45,45,55,45,55,63), 17, 496),
            new PokemonData("Servine", "Grass Snake Pokémon", "When it gets dirty, its leaves can't be used in photosynthesis, so it always keeps itself clean",496, 2, 0.8f, 16.0f, PokemonData.PokemonType.Grass, PokemonData.PokemonType.None, PokemonData.Abilities.Overgrow, PokemonData.Abilities.None, PokemonData.Abilities.Contrary, new TrainingData(TrainingData.EV_Yield_Type.SPD, TrainingData.Growth_Rate.MediumSlow, 2, 45, 50, 145), new BreedingData(BreedingData.Egg_Groups.Field, BreedingData.Egg_Groups.Grass, BreedingData.Gender.Normal, new int[] {4884, 5140}, 87.5f), new BaseStats(60, 60, 75, 60, 75, 83), 36, 497),
            new PokemonData("Serperior", "Regal Pokémon", "It only gives its all against strong opponents who are not fazed by the glare from Serperior’s noble eyes.", 497, 3, 3.3f,63.0f, PokemonData.PokemonType.Grass, PokemonData.PokemonType.None, PokemonData.Abilities.Overgrow, PokemonData.Abilities.None, PokemonData.Abilities.Contrary, new TrainingData(TrainingData.EV_Yield_Type.SPD, TrainingData.Growth_Rate.MediumSlow, 3,45,50,238), new BreedingData(BreedingData.Egg_Groups.Field, BreedingData.Egg_Groups.Grass, BreedingData.Gender.Normal, new int[] {4884, 5140}, 87.5f), new BaseStats(75,75,95,75,95,113), -1, -1),
            
            new PokemonData("Tepig", "Fire Pig Pokémon", "It loves to eat roasted berries, but sometimes it gets too excited and burns them to a crisp.", 498, 4, 0.5f, 9.9f, PokemonData.PokemonType.Fire, PokemonData.PokemonType.None, PokemonData.Abilities.Blaze, PokemonData.Abilities.None, PokemonData.Abilities.Thick_Fat, new TrainingData(TrainingData.EV_Yield_Type.HP, TrainingData.Growth_Rate.MediumSlow, 1, 45,50,61), new BreedingData(BreedingData.Egg_Groups.Field, BreedingData.Egg_Groups.None, BreedingData.Gender.Normal, new int[] {4884,5140}, 87.5f), new BaseStats(65,63,45,45,45,45), 17,499),
            new PokemonData("Pignite", "Fire Pig Pokémon", "The more it eats, the more fuel it has to make the fire in its stomach stronger. This fills it with even more power.", 499, 5, 1.0f, 55.5f,PokemonData.PokemonType.Fire, PokemonData.PokemonType.Fighting, PokemonData.Abilities.Blaze, PokemonData.Abilities.None, PokemonData.Abilities.Thick_Fat, new TrainingData(TrainingData.EV_Yield_Type.ATTK, TrainingData.Growth_Rate.MediumSlow, 2, 45,50,146), new BreedingData(BreedingData.Egg_Groups.Fairy,BreedingData.Egg_Groups.None, BreedingData.Gender.Normal, new int[] {4884, 5140}, 87.5f), new BaseStats(90,93,55,70,55,55), 36, 500),
            new PokemonData("Emboar", "Mega Fire Pig Pokemon", "It can throw a fire punch by setting its fists on fire with its fiery chin. It cares deeply about its friends.", 500, 6, 150, 1.6f, PokemonData.PokemonType.Fire, PokemonData.PokemonType.Fighting, PokemonData.Abilities.Blaze, PokemonData.Abilities.None, PokemonData.Abilities.Reckless, new TrainingData(TrainingData.EV_Yield_Type.ATTK, TrainingData.Growth_Rate.MediumSlow, 3, 238, 45, 50), new BreedingData(BreedingData.Egg_Groups.Field, BreedingData.Egg_Groups.None, BreedingData.Gender.Normal, new int[] {4884, 5140}, 87.5f), new BaseStats(110, 123, 65, 100, 65, 65), -1, -1),
            
            new PokemonData("Oshawott", "Sea Otter Pokemon", "It fights using the scalchop on its stomach. In response to an attack, it retaliates immediately by slashing.", 501, 7, 5.9f, 0.5f, PokemonData.PokemonType.Water, PokemonData.PokemonType.None, PokemonData.Abilities.Torrent, PokemonData.Abilities.None, PokemonData.Abilities.Shell_Armor, new TrainingData(TrainingData.EV_Yield_Type.SPATTK, TrainingData.Growth_Rate.MediumSlow, 1, 62, 45, 50), new BreedingData(BreedingData.Egg_Groups.Field, BreedingData.Egg_Groups.None, BreedingData.Gender.Normal, new int[] {4884, 5140}, 87.5f), new BaseStats(55,55,45,63,45,45), 502, 17),
        };
        
        init_moveSetData();
    }

    private void init_moveSetData()
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].PokemonName != "Empty")
            {
                data[i].Set_Movedata();
            }
        }
    }

    public PokemonData Get_Pokemon(int natID)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].NatId == natID)
            {
                return data[i];
            }
        }

        return null;
    }
}

[System.Serializable]
public class GrowthRateData
{
    [SerializeField] private TrainingData.Growth_Rate rate;
    [SerializeField] private int[] exp;

    public GrowthRateData(TrainingData.Growth_Rate rate)
    {
        this.rate = rate;
    }

    public TrainingData.Growth_Rate Rate => rate;

    public int[] Exp
    {
        get => exp;
        set => exp = value;
    }
}