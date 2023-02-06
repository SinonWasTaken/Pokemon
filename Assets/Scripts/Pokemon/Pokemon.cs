using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pokemon : PokemonData
{
    //The nature of a pokemom
    public enum Nature { Hardy, Lonely, Brave, Adamant, Naughty, Bold, Docile, Relaxed, Impish, Lax, Timid, Hasty, Serious, Jolly, Naive, Modest, Mild, Quiet, Bashful, Rash, Calm, Gentle, Sassy, Careful, Quirky}
    //Enum used to determine which stat is currently being calculated
    public enum Nature_Affects { Attack, Defense, SpAttack, SpDefense, Speed}

    //List of status effects that can affect a pokemon
    public enum Status_Effects { None, Poison, Paralyze, Burn, Bad_Poison, Fainted }

    [SerializeField] private string pokemon_nickname;
    [SerializeField] private string unique_id;

    [SerializeField] private int level;

    [SerializeField] private Nature pokemon_nature;

    [SerializeField] private int cal_hp_stat;
    [SerializeField] private int cal_attack_stat;
    [SerializeField] private int cal_defense_stat;
    [SerializeField] private int cal_sp_attack_stat;
    [SerializeField] private int cal_sp_defense_stat;
    [SerializeField] private int cal_speed_stat;

    [SerializeField] private int current_hp;

    private IV iv;
    private EV ev;

    [SerializeField] private Status_Effects status;

    private Abilities ability;

    [SerializeField] private List<Move> moves;

    private ItemData held_item;

    [SerializeField] private string original_trainer_id;

    [SerializeField] private int current_exp;
    [SerializeField] private int exp_To_Next_Level;
    
    public Pokemon(PokemonData data, string pokemonNickname, string uniqueId, int level, Nature pokemonNature, IV iv, EV ev, Status_Effects status, Abilities ability, List<Move> moves, ItemData heldItem, string originalTrainerId) : base(data)
    {
        pokemon_nickname = pokemonNickname;
        unique_id = uniqueId;
        this.level = level;
        pokemon_nature = pokemonNature;
        this.iv = iv;
        this.ev = ev;
        this.status = status;
        this.ability = ability;
        this.moves = moves;
        held_item = heldItem;
        original_trainer_id = originalTrainerId;

        if (PokemonName != "Empty")
        {
            exp_To_Next_Level = GrowthValues[level - 1];
            calculate_all_stats();
        }
    }

    private void calculate_all_stats()
    {
        //Add growth rate values
        //exp_To_Next_Level = 
        
        //The hp stat is calculated in a different way from the other stats
        cal_hp_stat = calculate_hp();
        //Below, the pokemons stats are being calculated
        cal_attack_stat = calculate_Stat(Nature_Affects.Attack, BaseStats.Attack, iv.IvAttack, ev.EvAttack);
        cal_defense_stat = calculate_Stat(Nature_Affects.Defense, BaseStats.Defense, iv.IvDefense, ev.EvDefense);
        cal_sp_attack_stat = calculate_Stat(Nature_Affects.SpAttack, BaseStats.SpAttack, iv.IvSpAttack, ev.EvSpAttack);
        cal_sp_defense_stat = calculate_Stat(Nature_Affects.SpDefense, BaseStats.SpDefense, iv.IvSpDefense, ev.EvSpDefense);
        cal_speed_stat = calculate_Stat(Nature_Affects.Speed, BaseStats.Speed, iv.IvSpeed, ev.EvSpeed);
    }

    private int calculate_hp()
    {
        float a = (2 * BaseStats.HP + iv.IvHp + (ev.EvHp / 4)) * level;
        float b = (a / 100) + level + 10;

        if (current_hp == 0)
        {
            if (status != Status_Effects.Fainted)
            {
                current_hp = (int) b;
            }
        }
        else
        {
            if (cal_hp_stat != 0)
            {
                int difference = (int) (b) - cal_hp_stat;
                current_hp += difference;
            }
        }

        return (int)(b); 
    }
    
    //The method that calculates the pokemon's stats. base_stat_to_calculate is the base stats value found in the data reference. The two parameters after are basically self explanitory.
    //nature_effect is used to calculate the stat changes brought on by the pokemon's nature. Each nature affects different stats
    private int calculate_Stat(Nature_Affects nature_effect, int base_stat_to_calculate, int iv_stat_to_calculate, int ev_stat_to_calculate)
    {
        float a = (2 * base_stat_to_calculate + iv_stat_to_calculate + (ev_stat_to_calculate / 4)) * level;
        float b = (a / 100) + 5;
        return (int)(b * get_nature(nature_effect));
    }
    
    //Based on the pokemons nature and the stat currently being calculated, an int value will be returned. This method is too long to fully comment 
    private float get_nature(Nature_Affects nature_effect)
    {
        if (pokemon_nature == Nature.Lonely)
        {
            if (nature_effect == Nature_Affects.Attack)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.Defense)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Brave)
        {
            if (nature_effect == Nature_Affects.Attack)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.Speed)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Adamant)
        {
            if (nature_effect == Nature_Affects.Attack)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.SpAttack)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Naughty)
        {
            if (nature_effect == Nature_Affects.Attack)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.SpDefense)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Bold)
        {
            if (nature_effect == Nature_Affects.Defense)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.Attack)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Relaxed)
        {
            if (nature_effect == Nature_Affects.Defense)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.Speed)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Impish)
        {
            if (nature_effect == Nature_Affects.Defense)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.SpAttack)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Lax)
        {
            if (nature_effect == Nature_Affects.Defense)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.SpDefense)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Timid)
        {
            if (nature_effect == Nature_Affects.Speed)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.Attack)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Hasty)
        {
            if (nature_effect == Nature_Affects.Speed)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.Defense)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Jolly)
        {
            if (nature_effect == Nature_Affects.Speed)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.SpAttack)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Naive)
        {
            if (nature_effect == Nature_Affects.Speed)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.SpDefense)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Modest)
        {
            if (nature_effect == Nature_Affects.SpAttack)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.Attack)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Mild)
        {
            if (nature_effect == Nature_Affects.SpAttack)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.Defense)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Quiet)
        {
            if (nature_effect == Nature_Affects.SpAttack)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.Speed)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Rash)
        {
            if (nature_effect == Nature_Affects.SpAttack)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.SpDefense)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Calm)
        {
            if (nature_effect == Nature_Affects.SpDefense)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.Attack)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Gentle)
        {
            if (nature_effect == Nature_Affects.SpDefense)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.Defense)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Sassy)
        {
            if (nature_effect == Nature_Affects.SpDefense)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.Speed)
            {
                return 0.9f;
            }
        }
        else if (pokemon_nature == Nature.Careful)
        {
            if (nature_effect == Nature_Affects.SpDefense)
            {
                return 1.1f;
            }
            else if (nature_effect == Nature_Affects.SpAttack)
            {
                return 0.9f;
            }
        }
        else
            return 1;

        return 1;
    }

    public void RegainHealth(int amount)
    {
        current_hp += amount;
        current_hp = Mathf.Clamp(current_hp, 0, cal_hp_stat);
    }
    
    public void TakeDamage(int damage)
    {
        current_hp -= damage;
        current_hp = Mathf.Clamp(current_hp, 0, cal_hp_stat);

        if (current_hp == 0)
        {
            status = Status_Effects.Fainted;
        }
    }

    public void AddExp(int amount)
    {
        current_exp += amount;

        while (current_exp >= exp_To_Next_Level)
        {
            current_exp -= exp_To_Next_Level;
            level++;

            exp_To_Next_Level = GrowthValues[level - 1];
            
            checkForNewMoves();
        }
        
        calculate_all_stats();
    }

    private void checkForNewMoves()
    {
        if (Moveset != null)
        {
            if (Moveset.Learn != null)
            {
                for (int i = 0; i < Moveset.Learn.Length; i++)
                {
                    string name = Moveset.Learn[i].Split(':')[0];
                    int learn_level = Convert.ToInt32(Moveset.Learn[i].Split(':')[1]);

                    if (level >= learn_level)
                    {
                        if (Moves.Count < 4)
                        {
                            MoveData moveData = Move_Database.Instance.get_move(name);
                            Moves.Add(new Move(moveData, moveData.BasePp, moveData.BasePp));
                        }
                        else
                        {
                            Debug.Log("More than 4 moves, implement move changing");
                        }
                    }
                }
            }
        }
    }

    public void Heal()
    {
        current_hp = cal_hp_stat;
    }
    
    #region Properties

    public string UniqueId => unique_id;

    public int Level => level;

    public Nature PokemonNature => pokemon_nature;

    public int CalHpStat => cal_hp_stat;
    public int CalAttackStat => cal_attack_stat;
    public int CalDefenseStat => cal_defense_stat;
    public int CalSpAttackStat => cal_sp_attack_stat;
    public int CalSpDefenseStat => cal_sp_defense_stat;
    public int CalSpeedStat => cal_speed_stat;
    public int CurrentHp => current_hp;

    public IV Iv => iv;
    public EV Ev => ev;

    public Status_Effects Status
    {
        get => status;
        set => status = value;
    }
    public Abilities Ability => ability;

    public string OriginalTrainerId => original_trainer_id;

    public ItemData HeldItem => held_item;

    public List<Move> Moves => moves;
    
    #endregion

    public override string ToString()
    {
        return $"{PokemonName} {level} {cal_hp_stat} {cal_attack_stat} {cal_defense_stat} {cal_sp_attack_stat} {cal_sp_defense_stat} {cal_speed_stat}";
    }
}

public class IV
{
    private int iv_hp;
    private int iv_attack;
    private int iv_defense;
    private int iv_sp_attack;
    private int iv_sp_defense;
    private int iv_speed;

    public IV(int ivHp, int ivAttack, int ivDefense, int ivSpAttack, int ivSpDefense, int ivSpeed)
    {
        iv_hp = ivHp;
        iv_attack = ivAttack;
        iv_defense = ivDefense;
        iv_sp_attack = ivSpAttack;
        iv_sp_defense = ivSpDefense;
        iv_speed = ivSpeed;
    }

    public int IvHp => iv_hp;
    public int IvAttack => iv_attack;
    public int IvDefense => iv_defense;
    public int IvSpAttack => iv_sp_attack;
    public int IvSpDefense => iv_sp_defense;
    public int IvSpeed => iv_speed;
}

public class EV
{
    private const int max_total_ev_limit = 510;
    private const int max_single_ev_limit = 252;

    private int current_total_ev;
    
    private int ev_hp;
    private int ev_attack;
    private int ev_defense;
    private int ev_sp_attack;
    private int ev_sp_defense;
    private int ev_speed;

    public EV(int evHp, int evAttack, int evDefense, int evSpAttack, int evSpDefense, int evSpeed)
    {
        ev_hp = evHp;
        ev_attack = evAttack;
        ev_defense = evDefense;
        ev_sp_attack = evSpAttack;
        ev_sp_defense = evSpDefense;
        ev_speed = evSpeed;
    }

    public void AddEvs(TrainingData.EV_Yield_Type type, int amount)
    {
        if (type == TrainingData.EV_Yield_Type.HP)
        {
            ev_hp = doEvCheck(ev_hp, amount);
        }
        else if (type == TrainingData.EV_Yield_Type.ATTK)
        {
            ev_attack = doEvCheck(ev_attack, amount);
        }
        else if (type == TrainingData.EV_Yield_Type.DEF)
        {
            ev_defense = doEvCheck(ev_defense, amount);
        }
        else if (type == TrainingData.EV_Yield_Type.SPATTK)
        {
            ev_sp_attack = doEvCheck(ev_sp_attack, amount);
        }
        else if (type == TrainingData.EV_Yield_Type.SPDEF)
        {
            ev_sp_defense = doEvCheck(ev_sp_defense, amount);
        }
        else
        {
            ev_speed = doEvCheck(ev_speed, amount);
        }
    }

    private int doEvCheck(int stat, int amount)
    {
        if (current_total_ev < max_total_ev_limit)
        {
            for (int i = 0; i < amount; i++)
            {
                if (current_total_ev < max_total_ev_limit)
                {
                    current_total_ev++;

                    if (stat < max_single_ev_limit)
                    {
                        stat++;
                    }
                }
            }
        }

        return stat;
    }
    
    public int EvHp => ev_hp;
    public int EvAttack => ev_attack;
    public int EvDefense => ev_defense;
    public int EvSpAttack => ev_sp_attack;
    public int EvSpDefense => ev_sp_defense;
    public int EvSpeed => ev_speed;
}