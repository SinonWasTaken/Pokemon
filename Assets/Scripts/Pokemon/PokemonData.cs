using System;
using UnityEngine;

[Serializable]
public class PokemonData
{
    public enum PokemonType
    {
        None,
        Psychic,
        Fire,
        Water,
        Grass,
        Dark,
        Dragon,
        Ground,
        Rock,
        Electric,
        Ice,
        Fairy,
        Fighting,
        Poison,
        Flying,
        Normal
    }

    public enum Abilities
    {
        None,
        Victory_Star,
        Overgrow,
        Contrary,
        Blaze,
        Thick_Fat,
        Guts,
        Adaptability,
        Reckless,
        Torrent,
        Shell_Armor,
    }

    [SerializeField] private string pokemon_name;
    [SerializeField] private string pokemon_description;
    [SerializeField] private string pokemon_species;

    [SerializeField] private int nat_id;
    [SerializeField] private int reg_id;

    private float weight;
    private float height;

    private PokemonType type_one;
    private PokemonType type_two;

    private Abilities ability_one;
    private Abilities ability_two;
    private Abilities hidden_ability;

    [SerializeField] private TrainingData training_data;
    [SerializeField] private BreedingData breeding_data;
    [SerializeField] private BaseStats base_stats;

    [SerializeField] private int evolution_nat_id;
    [SerializeField] private int evolution_level;

    [SerializeField] private MoveSetData moveset;

    [SerializeField] private int[] growthValues;
    
    public PokemonData(PokemonData data)
    {
        pokemon_name = data.pokemon_name;
        pokemon_description = data.pokemon_description;
        pokemon_species = data.pokemon_species;
        nat_id = data.NatId;
        reg_id = data.reg_id;
        weight = data.weight;
        height = data.height;
        type_one = data.type_one;
        type_two = data.type_two;
        ability_one = data.ability_one;
        ability_two = data.ability_two;
        hidden_ability = data.hidden_ability;
        training_data = data.TrainingData;
        breeding_data = data.BreedingData;
        base_stats = data.BaseStats;
        evolution_nat_id = data.evolution_nat_id;
        evolution_level = data.evolution_level;
        moveset = data.moveset;
    }
    
    public PokemonData(string pokemonName, string pokemonSpecies, string pokemonDescription, int natId, int regId, float weight, float height, PokemonType typeOne, PokemonType typeTwo, Abilities abilityOne, Abilities abilityTwo, Abilities hiddenAbility, TrainingData trainingData, BreedingData breedingData, BaseStats baseStats, int evolutionNatId, int evolutionLevel)
    {
        pokemon_name = pokemonName;
        pokemon_description = pokemonDescription;
        pokemon_species = pokemonSpecies;
        nat_id = natId;
        reg_id = regId;
        this.weight = weight;
        this.height = height;
        type_one = typeOne;
        type_two = typeTwo;
        ability_one = abilityOne;
        ability_two = abilityTwo;
        hidden_ability = hiddenAbility;
        training_data = trainingData;
        breeding_data = breedingData;
        base_stats = baseStats;
        evolution_nat_id = evolutionNatId;
        evolution_level = evolutionLevel;
    }

    public void Set_Movedata()
    {
        moveset = new MoveSetData(nat_id);
    }
    
    public string PokemonName => pokemon_name;
    public string PokemonDescription => pokemon_description;
    public string PokemonSpecies => pokemon_species;
    public int NatId => nat_id;
    public int RegId => reg_id;
    public float Weight => weight;
    public float Height => height;
    public PokemonType TypeOne => type_one;
    public PokemonType TypeTwo => type_two;
    public Abilities AbilityOne => ability_one;
    public Abilities AbilityTwo => ability_two;
    public Abilities HiddenAbility => hidden_ability;
    public TrainingData TrainingData => training_data;
    public BreedingData BreedingData => breeding_data;
    public BaseStats BaseStats => base_stats;
    public int EvolutionNatId => evolution_nat_id;
    public int EvolutionLevel => evolution_level;
    public MoveSetData Moveset => moveset;

    public int[] GrowthValues
    {
        get
        {
            if (growthValues == null)
            {
                growthValues = new int[0];
                growthValues = Pokemon_Database.Instance.getGrowthRate(training_data.GrowthRate);
            }

            return growthValues;
        }
    }
}

[Serializable]
public class TrainingData
{
    public enum EV_Yield_Type
    {
        HP,
        ATTK,
        DEF,
        SPATTK,
        SPDEF,
        SPD
    }

    public enum Growth_Rate
    {
        Erratic,
        Fast,
        MediumFast,
        MediumSlow,
        Slow,
        Fluctuating
    }

    [SerializeField] private EV_Yield_Type yield;
    [SerializeField] private Growth_Rate growth_rate;

    [SerializeField] private int ev_yield_amount;
    [SerializeField] private int base_exp;

    [SerializeField] private float catch_Rate;
    [SerializeField] private float friend_Ship;

    public TrainingData(EV_Yield_Type yield, Growth_Rate growthRate, int evYieldAmount, int baseExp, float catchRate,
        float friendShip)
    {
        this.yield = yield;
        growth_rate = growthRate;
        ev_yield_amount = evYieldAmount;
        base_exp = baseExp;
        catch_Rate = catchRate;
        friend_Ship = friendShip;
    }

    public EV_Yield_Type Yield => yield;
    public Growth_Rate GrowthRate => growth_rate;
    public int EvYieldAmount => ev_yield_amount;
    public int BaseExp => base_exp;
    public float CatchRate => catch_Rate;
    public float FriendShip => friend_Ship;
}
[Serializable]
public class BreedingData
{
    public enum Egg_Groups
    {
        None,
        Undiscovered,
        Monster,
        Amorphous,
        Bug,
        Dragon,
        Fairy,
        Field,
        Flying,
        Grass,
        Human_like,
        Mineral,
        Water_one,
        Water_two,
        Water_three,
        Ditto
    }

    public enum Gender
    {
        Genderless,
        Normal,
        AllMale,
        AllFemale
    }

    [SerializeField] private Egg_Groups group_one;
    [SerializeField] private Egg_Groups group_two;

    [SerializeField] private Gender gender;

    [SerializeField] private int[] egg_cycles;
    [SerializeField] private float male_percent;

    public BreedingData(Egg_Groups groupOne, Egg_Groups groupTwo, Gender gender, int[] eggCycles, float malePercent)
    {
        group_one = groupOne;
        group_two = groupTwo;
        this.gender = gender;
        egg_cycles = eggCycles;
        male_percent = malePercent;
    }

    public Egg_Groups GroupOne => group_one;
    public Egg_Groups GroupTwo => group_two;
    public Gender Gender_Group => gender;
    public int[] EggCycles => egg_cycles;
    public float MalePercent => male_percent;
}
[Serializable]
public class BaseStats
{
    [SerializeField] private int healthPoints;
    [SerializeField] private int attack;
    [SerializeField] private int defense;
    [SerializeField] private int sp_attack;
    [SerializeField] private int sp_defense;
    [SerializeField] private int speed;

    public BaseStats(int healthPoints, int attack, int defense, int spAttack, int spDefense, int speed)
    {
        this.healthPoints = healthPoints;
        this.attack = attack;
        this.defense = defense;
        sp_attack = spAttack;
        sp_defense = spDefense;
        this.speed = speed;
    }

    public int HP
    {
        get => healthPoints;
        set => healthPoints = value;
    }

    public int Attack
    {
        get => attack;
        set => attack = value;
    }

    public int Defense
    {
        get => defense;
        set => defense = value;
    }

    public int SpAttack
    {
        get => sp_attack;
        set => sp_attack = value;
    }

    public int SpDefense
    {
        get => sp_defense;
        set => sp_defense = value;
    }

    public int Speed
    {
        get => speed;
        set => speed = value;
    }
}