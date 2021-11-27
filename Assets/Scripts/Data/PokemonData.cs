public class PokemonData
{
    public bool editor_is_fold_open;

    public enum PokemonType {None, Psychic, Fire, Water, Grass, Dark, Dragon, Ground, Electric, Ice, Fairy, Fighting, Poison, Flying, Normal}

    public enum Abilities { None, Victory_Star, Overgrow, Contrary, Blaze, Thick_Fat }

    public string pokemon_name { get; private set; }
    
    public string pokemon_species { get; private set; }

    public string pokedex_description { get; private set; }

    public int national_id { get; private set; }
    public int regional_id { get; private set; }

    public PokemonType type_one { get; private set; }
    public PokemonType type_two { get; private set; }

    public float height { get; private set; }
    public float weight { get; private set; }

    public Abilities ability_one { get; private set; }
    public Abilities ability_two { get; private set; }
    public Abilities hidden_ability { get; private set; }

    public Training_Data training_data { get; private set; }

    public Breeding_Data breeding_Data { get; private set; }

    public Base_Stats base_Stats { get; private set; }

    public int evolution_level { get; private set; }
    public int evolution_id { get; private set; }

    public PokemonData(string pokemon_name, string pokemon_species, string pokedex_description, int national_id, int regional_id, PokemonType type_one, PokemonType type_two, float height, float weight, Abilities ability_one, Abilities ability_two, Abilities hidden_ability, Training_Data training_data, Breeding_Data breeding_Data, Base_Stats base_Stats, int evolution_level, int evolution_id)
    {
        this.pokemon_name = pokemon_name;
        this.pokemon_species = pokemon_species;
        this.pokedex_description = pokedex_description;
        this.national_id = national_id;
        this.regional_id = regional_id;
        this.type_one = type_one;
        this.type_two = type_two;
        this.height = height;
        this.weight = weight;
        this.ability_one = ability_one;
        this.ability_two = ability_two;
        this.hidden_ability = hidden_ability;
        this.training_data = training_data;
        this.breeding_Data = breeding_Data;
        this.base_Stats = base_Stats;
        this.evolution_level = evolution_level;
        this.evolution_id = evolution_id;
    }
}

public class Training_Data
{
    public bool editor_is_open;

    public enum EV_Yield_Type { HP, ATTK, DEF, SPATTK, SPDEF, SPD}

    public enum Growth_Rate { Erratic, Fast, MediumFast, MediumSlow, Slow, Fluctuating}

    public EV_Yield_Type ev { get; private set; }
    public int ev_Yield_Amount { get; private set; }

    public float catch_Rate { get; private set; } 
    public float friend_Ship { get; private set; }

    public int base_Exp { get; private set; }

    public Growth_Rate growth_Rate { get; private set;  }

    public Training_Data(EV_Yield_Type type, int yield_amount, float catch_rate, float friend, int base_exp, Growth_Rate growth_Rate)
    {
        ev = type;
        ev_Yield_Amount = yield_amount;
        this.catch_Rate = catch_rate;
        friend_Ship = friend;
        this.base_Exp = base_exp;
        this.growth_Rate = growth_Rate;
    }
}

public class Breeding_Data
{
    public bool editor_is_open;

    public enum Egg_Groups { None, Undiscovered, Monster, Amorphous, Bug, Dragon, Fairy, Field, Flying, Grass, Human_like, Mineral, Water_one, Water_two, Water_three, Ditto }
    public enum Gender { Genderless, Normal, AllMale, AllFemale }

    public Egg_Groups group_one { get; private set; }
    public Egg_Groups group_two { get; private set; }
    public Gender gender { get; private set; }
    public int[] egg_cycles { get; private set; }
    public float male_percent;

    public Breeding_Data(Egg_Groups group_one, Egg_Groups group_two,Gender gender, int[] egg_cycles, float male_percent)
    {
        this.group_one = group_one;
        this.group_two = group_two;
        this.gender = gender;
        this.egg_cycles = egg_cycles;
        this.male_percent = male_percent;
    }
}

public class Base_Stats
{
    public bool editor_is_open;

    public int HP { get; private set; }
    public int ATTK { get; private set; }
    public int DEF { get; private set; }
    public int SPATTK { get; private set; }
    public int SPDEF { get; private set; }
    public int SPD { get; private set; }

    public Base_Stats(int value)
    {
        HP = value;
        ATTK = value;
        DEF = value;
        SPATTK = value;
        SPDEF = value;
        SPD = value;
    }

    public Base_Stats(int hp, int attk, int def, int spattk, int spdef, int spd)
    {
        HP = hp;
        ATTK = attk;
        DEF = def;
        SPATTK = spattk;
        SPDEF = spdef;
        SPD = spd;
    }
}
