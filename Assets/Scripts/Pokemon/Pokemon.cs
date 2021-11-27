using UnityEngine;

public class Pokemon
{
    public enum Nature { Hardy, Lonely, Brave, Adamant, Naughty, Bold, Docile, Relaxed, Impish, Lax, Timid, Hasty, Serious, Jolly, Naive, Modest, Mild, Quiet, Bashful, Rash, Calm, Gentle, Sassy, Careful, Quirky}
    private enum Nature_Affects { Attack, Defense, SpAttack, SpDefense, Speed}

    public PokemonData data { get; private set; }

    public string pokemon_nickname { get; private set; }

    public long unique_id { get; private set; }

    public int level { get; private set; }

    public Nature nature { get; private set; }

    public int hp_stat { get; private set; }
    public int attack_stat { get; private set; }
    public int defense_stat { get; private set; }
    public int spattack_stat { get; private set; }
    public int spdefense_stat { get; private set; }
    public int speed_stat { get; private set; }

    public IV_Stats iv_Stats { get; private set; }

    public EV_Stats ev_Stats { get; private set; }

    public Pokemon(PokemonData data, string pokemonNickname, long uniqueID, int level, Nature nature, IV_Stats ivStats, EV_Stats evStats)
    {
        this.data = data;
        pokemon_nickname = pokemonNickname;
        unique_id = uniqueID;
        this.level = level;
        this.nature = nature;
        iv_Stats = ivStats;
        ev_Stats = evStats;
        
        calculateStats();
    }

    private void calculateStats()
    {
        hp_stat = calculate_HP_Stat();
        attack_stat = calculate_Stat(Nature_Affects.Attack, data.base_Stats.ATTK, iv_Stats.IV_ATTK, ev_Stats.EV_ATTK);
        defense_stat = calculate_Stat(Nature_Affects.Defense, data.base_Stats.DEF, iv_Stats.IV_DEF, ev_Stats.EV_DEF);
        spattack_stat = calculate_Stat(Nature_Affects.SpAttack, data.base_Stats.SPATTK, iv_Stats.IV_SPATTK, ev_Stats.EV_SPATTK);
        spdefense_stat = calculate_Stat(Nature_Affects.SpDefense, data.base_Stats.SPDEF, iv_Stats.IV_SPDEF, ev_Stats.EV_SPDEF);
        speed_stat = calculate_Stat(Nature_Affects.Speed, data.base_Stats.SPD, iv_Stats.IV_SPD, ev_Stats.EV_SPD);
    }

    private int calculate_HP_Stat()
    {
        float step_one = ((2 * data.base_Stats.HP + iv_Stats.IV_HP + (ev_Stats.EV_HP / 4)) / 100);
        return (int)(step_one + level + 10);
    }

    private int calculate_Stat(Nature_Affects nature_effect, int base_stat_to_calculate, int iv_stat_to_calculate, int ev_stat_to_calculate)
    {
        float step_one = ((2 * base_stat_to_calculate + iv_stat_to_calculate + (ev_stat_to_calculate / 4) * level) / 100) + 5;
        return (int)(step_one * get_nature(nature_effect));
    }

    private float get_nature(Nature_Affects nature_effect)
    {
        if (nature == Nature.Lonely)
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
        else if (nature == Nature.Brave)
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
        else if (nature == Nature.Adamant)
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
        else if (nature == Nature.Naughty)
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
        else if (nature == Nature.Bold)
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
        else if (nature == Nature.Relaxed)
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
        else if (nature == Nature.Impish)
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
        else if (nature == Nature.Lax)
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
        else if (nature == Nature.Timid)
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
        else if (nature == Nature.Hasty)
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
        else if (nature == Nature.Jolly)
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
        else if (nature == Nature.Naive)
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
        else if (nature == Nature.Modest)
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
        else if (nature == Nature.Mild)
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
        else if (nature == Nature.Quiet)
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
        else if (nature == Nature.Rash)
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
        else if (nature == Nature.Calm)
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
        else if (nature == Nature.Gentle)
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
        else if (nature == Nature.Sassy)
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
        else if (nature == Nature.Careful)
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
}

public class EV_Stats
{
    public int max_total_EV = 255;
    public int single_max_EV = 100;

    public int current_total;

    public int EV_HP { get; private set; }
    public int EV_ATTK { get; private set; }
    public int EV_DEF { get; private set; }
    public int EV_SPATTK { get; private set; }
    public int EV_SPDEF { get; private set; }
    public int EV_SPD { get; private set; }

    public EV_Stats()
    {
        EV_HP = 0;
        EV_ATTK = 0;
        EV_DEF = 0;
        EV_SPATTK = 0;
        EV_SPDEF = 0;
        EV_SPD = 0;
    }
}

public class IV_Stats
{
    public int IV_HP { get; private set; }
    public int IV_ATTK { get; private set; }
    public int IV_DEF { get; private set; }
    public int IV_SPATTK { get; private set; }
    public int IV_SPDEF { get; private set; }
    public int IV_SPD { get; private set; }

    public IV_Stats()
    {
        IV_HP = Random.Range(0, 32);
        IV_ATTK = Random.Range(0, 32);
        IV_DEF = Random.Range(0, 32);
        IV_SPATTK = Random.Range(0, 32);
        IV_SPDEF = Random.Range(0, 32);
        IV_SPD = Random.Range(0, 32);
    }

    public IV_Stats(int ivHp, int ivAttk, int ivDef, int ivSpattk, int ivSpdef, int ivSpd)
    {
        if(ivHp != 0)
            IV_HP = ivHp;
        else
            IV_HP = Random.Range(0, 32);
        
        if(ivAttk != 0)
            IV_ATTK = ivAttk;
        else
            IV_ATTK = Random.Range(0, 32);
        
        if(ivDef != 0)
            IV_DEF = ivDef;
        else
            IV_DEF = Random.Range(0, 32);
        
        if(ivSpattk != 0)
            IV_SPATTK = ivSpattk;
        else
            IV_SPATTK = Random.Range(0, 32);
        
        if(ivSpdef != 0)
            IV_SPDEF = ivSpdef;
        else
            IV_SPDEF = Random.Range(0, 32);
        
        if(ivSpd != 0)
            IV_SPD = ivSpd;
        else
            IV_SPD = Random.Range(0, 32);
    }
}