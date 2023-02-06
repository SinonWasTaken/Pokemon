using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class BattlePokemon
{
    public enum Special_Status_Effect
    {
        None,
        Sleep,
        Freeze,
        Confuse
    }
    public enum Turn_Type
    {
        None,
        Attack,
        Run,
        Item,
        Switch
    }

    [SerializeField] private List<Move_Effect> permanent_effects;
    
    [SerializeField]  Special_Status_Effect special_effect;

    [SerializeField] private int special_move_delay;
    
    [SerializeField] private Pokemon pokemon;
    
    //Used for moves that have a cooldown. If last move is a move that has a cooldown, then the pokemon will have its turn skipped, and the last move will be removed
    [SerializeField] private Move current_move;
    [SerializeField] private Move last_move;

    [SerializeField] private int attack_stat_stage = 6;
    [SerializeField] private int defense_stat_stage = 6;
    [SerializeField] private int spattack_stat_stage = 6;
    [SerializeField] private int spdefense_stat_stage = 6;
    [SerializeField] private int speed_stat_stage = 6;

    [SerializeField] private int accuracy_stage = 6;
    [SerializeField] private int evasion_stage = 6;
    
    [SerializeField] private float[] stat_change = new float[] { 2f / 8f, 2f / 7f, 2f / 6f, 2f / 5f, 2f / 4f, 2f / 3f, 2f / 2f, 3f / 2f, 4f / 2f, 5f / 2f, 6f / 2f, 7f / 2f, 8f / 2f};

    [SerializeField] private float[] eva_change = new float[] { 9f/3f, 8f/3f, 7f/3f, 6f/3f, 5f/3f, 4f/3f, 3f/3f, 3f/4f, 3f/5f, 3f/6f, 3f/7f, 3f/8f, 3f/9f };
    
    [SerializeField] private float[] acc_change = new float[] { 3f/9f, 3f/8f, 3f/7f, 3f/6f, 3f/5f, 3f/4f, 3f/3f, 4f/3f, 5f/3f, 6f/3f, 7f/3f, 8f/3f, 9f/3f };
    
    [SerializeField] private List<BattlePokemon> targets;

    [SerializeField] private bool hasGone;

    [SerializeField] private Turn_Type turn;

    [SerializeField] private List<string> foughtUniqueIds;

    [SerializeField] private BattleParticipant trainer; 

    public BattlePokemon(Pokemon pokemon, BattleParticipant trainer)
    {
        this.trainer = trainer;
        this.pokemon = pokemon;
        permanent_effects = new List<Move_Effect>();

        targets = new List<BattlePokemon>();

        foughtUniqueIds = new List<string>();
    }

    public void addEnemyUniqueIds(string id)
    {
        foughtUniqueIds.Add(id);
    }

    public bool hasEnemyUniqueId(string id) 
    {
        for (int i = 0; i < foughtUniqueIds.Count; i++)
        {
            if (foughtUniqueIds[i] == id)
            {
                return true;
            }
        }

        return false;
    }
    
    public void Apply_Effect(Move_Effect effect)
    {
        //Battle conditions. Are removed when the battle ends
        if (effect.M_Effect == Move_Effect.Effect.Confuse)
        {
            apply_Battle_Conditions(Special_Status_Effect.Confuse);
        }
        else if (effect.M_Effect == Move_Effect.Effect.Sleep)
        {
            apply_Battle_Conditions(Special_Status_Effect.Sleep);
        }
        else if (effect.M_Effect == Move_Effect.Effect.Freeze)
        {
            apply_Battle_Conditions(Special_Status_Effect.Freeze);
        }

        //Accuracy and evasion stat changes
        if (effect.M_Effect == Move_Effect.Effect.Accuracy)
        {
            Update_Accuracy_Stage((int) effect.Value);
        }
        else if (effect.M_Effect == Move_Effect.Effect.Evasion)
        {
            Update_Evasion_Stage((int) effect.Value);
        }

        //Battle stats. Are reset when a new battle starts
        if (effect.M_Effect == Move_Effect.Effect.Attack)
        {
            Update_Attack_Stage((int) effect.Value);
        }
        else if (effect.M_Effect == Move_Effect.Effect.Defense)
        {
            Update_Defense_Stage((int) effect.Value);
        }
        else if (effect.M_Effect == Move_Effect.Effect.SpAttack)
        {
            Update_Sp_Attack_Stage((int) effect.Value);
        }
        else if (effect.M_Effect == Move_Effect.Effect.SpDefense)
        {
            Update_Sp_Defense_Stage((int) effect.Value);
        }
        else if (effect.M_Effect == Move_Effect.Effect.Speed)
        {
            Update_Speed_Stage((int) effect.Value);
        }
        else if (effect.M_Effect == Move_Effect.Effect.All_Stats)
        {
            Update_Attack_Stage((int) effect.Value);
            Update_Defense_Stage((int) effect.Value);
            Update_Sp_Attack_Stage((int) effect.Value);
            Update_Sp_Defense_Stage((int) effect.Value);
            Update_Speed_Stage((int) effect.Value);
        }
        else if (effect.M_Effect == Move_Effect.Effect.RandomStat)
        {
            int random = Random.Range(0, 5);
            switch (random)
            {
                case 0:
                    Update_Attack_Stage((int) effect.Value);
                    break;
                case 1:
                    Update_Defense_Stage((int) effect.Value);
                    break;
                case 2:
                    Update_Sp_Attack_Stage((int) effect.Value);
                    break;
                case 3:
                    Update_Sp_Defense_Stage((int) effect.Value);
                    break;
                case 4:
                    Update_Speed_Stage((int) effect.Value);
                    break;
            }
        }
        
        //Pokemon conditions. Wont be removed after the battle is over
        if (effect.M_Effect == Move_Effect.Effect.Poison)
        {
            if (pokemon.Status == Pokemon.Status_Effects.None)
            {
                pokemon.Status = Pokemon.Status_Effects.Poison;
            }
        }
        else if (effect.M_Effect == Move_Effect.Effect.Bad_Poison)
        {
            if (pokemon.Status == Pokemon.Status_Effects.None)
            {
                pokemon.Status = Pokemon.Status_Effects.Bad_Poison;
            }
        }
        else if (effect.M_Effect == Move_Effect.Effect.Paralyze)
        {
            if (pokemon.Status == Pokemon.Status_Effects.None)
            {
                pokemon.Status = Pokemon.Status_Effects.Paralyze;
            }
        }
        else if (effect.M_Effect == Move_Effect.Effect.Burn)
        {
            if (pokemon.Status == Pokemon.Status_Effects.None)
            {
                pokemon.Status = Pokemon.Status_Effects.Burn;
            }
        }
        
        //Place for permanent effects to be added. Moves like aqua ring go here, which heals the pokemon until switching out
        if (effect.M_Effect == Move_Effect.Effect.Heal_Self)
        {
            permanent_effects.Add(effect);
        }
    }

    //Occurs at the end of the round, or at the end of the pokemons turn. Haven't decided
    public void Check_End_Of_Round_Effects()
    {
        for (int i = 0; i < permanent_effects.Count; i++)
        {
            //Heals the pokemon. Used with moves like Aqua Ring
            if (permanent_effects[i].M_Effect == Move_Effect.Effect.Heal_Self)
            {
                float value = pokemon.CalHpStat * permanent_effects[i].Value;

                //If the pokemon is holding a big root, the effect is increased by 30%
                if (pokemon.HeldItem != null)
                {
                    if (pokemon.HeldItem.ItemName == "Big Root")
                    {
                        value *= 1.3f;
                    }
                }
                
                pokemon.RegainHealth((int) value);
            }   
        }
    }
    
    private void apply_Battle_Conditions(Special_Status_Effect effect)
    {
        if (special_effect == Special_Status_Effect.None)
        {
            special_effect = effect;
            special_move_delay = 0;
        }
    }
    
    //Checks if the current pokemon will still be asleep at the start of its turn
    public bool Check_If_Still_Asleep()
    {
        //increments the special_move_delay
        special_move_delay++;
        //if special move delay is greater than or equal to 1 and is less that 3, then 
        if (special_move_delay >= 1 && special_move_delay < 3)
        {
            //Generate a random value
            float random = UnityEngine.Random.Range(0, 100);

            //if the random value is less than or equal to 20
            if (random <= 20)
            {
                //then the pokemon is no longer sleeping
                return false;
            }
            else
            {
                //other wise it still is alseep
                return true;
            }
        }
        //if special move delay equals 3
        else if (special_move_delay == 3)
        {
            //then the pokemon is no longer sleeping
            return false;
        }

        return false;
    }

    public bool Check_If_Defrosted()
    {
        float random = Random.Range(0, 100);
        
        //if the random value is less than or equal to 20
        if (random <= 20)
        {
            //then the pokemon is no longer frozen
            return false;
        }
        else
        {
            //other wise it still is frozen
            return true;
        }
    }

    public bool Is_Still_Confused()
    {
        special_move_delay++;

        if (special_move_delay >= 2 && special_move_delay < 5)
        {
            float random = Random.Range(0, 100);
        
            //if the random value is less than or equal to 20
            if (random <= 20)
            {
                //then the pokemon is no longer confused
                return false;
            }
            else
            {
                //other wise it still is confused
                return true;
            }
        }
        else if (special_move_delay == 5)
        {
            return false;            
        }

        return false;
    }

    public bool Does_Hit_Self_In_Confusion()
    {
        float random = Random.Range(0, 100);

        if (random <= 50)
        {
            return true;
        }

        return false;
    }

    public void Take_Damage(Move move, BattlePokemon pokemon)
    {
        float targets = 1;
        float weather = 1;
        float critical = 1;
        float random = 1;
        float stab = 1;
        float type = 1;
        float burn = 1;

        float attack = 0;
        float defense = 0;
        if (move.MoveCategory == MoveData.Move_Category.Physical)
        {
            attack = pokemon.AttackStat;
            defense = DefenseStat;
        }
        else if (move.MoveCategory == MoveData.Move_Category.Special)
        {
            attack = pokemon.SpAttackStat;
            defense = SpDefenseStat;
        }

        float level1 = (((2f * pokemon.pokemon.Level) / 5f) + 2f) * move.Power * (attack * 1f / defense * 1f);

        float level2 = (level1 / 50f) + 2;

        //calculate critical chance here

        //calculate type value here
        
        //calculate weather value
        
        if (pokemon.targets.Count > 1)
        {
            targets = 0.75f;
        }
        else
        {
            targets = 1f;
        }

        random = Random.Range(0.8f, 1f);
        
        if (pokemon.pokemon.Status == Pokemon.Status_Effects.Burn)
        {
            if (move.MoveCategory == MoveData.Move_Category.Physical)
            {
                if (pokemon.pokemon.Ability != PokemonData.Abilities.Guts)
                {
                    burn = 0.5f;
                }
            }
        }

        if (move.MoveType == this.pokemon.TypeOne || move.MoveType == this.pokemon.TypeTwo)
        {
            if (pokemon.pokemon.AbilityOne == PokemonData.Abilities.Adaptability)
            {
                stab = 2f;
            }
            else
            {
                stab = 1.5f;
            }
        }

        float level3 = level2 * targets * weather * critical * random * stab * type * burn;

        this.pokemon.TakeDamage((int)level3);
    }

    public bool Can_Catch_Pokemon()
    {
        bool can_catch = pokemon.UniqueId.Contains("wild");

        if (can_catch)
        {
            bool caught = attempt_catch();

            return caught;
        }
        else
        {
            return false;
        }

        return false;
    }

    private bool attempt_catch()
    {
        float pokeball_bonus = 1;
        
        float bonus_status = get_bonus_value();

        float a = (((3 * pokemon.CalHpStat - 2 * pokemon.CurrentHp) * pokemon.TrainingData.CatchRate * pokeball_bonus) /
                   (3 * pokemon.CalHpStat)) * bonus_status;

        if (a >= 255)
        {
            return true;
        }
        
        int b = (int) (65536f / Math.Sqrt(Math.Sqrt(255f / a)));

        int random = 0;

        int shake_count = 0;
        
        for (int i = 0; i < 4; i++)
        {
            random = Random.Range(0, 65537);
            
            if (random >= b)
            {
                break;
            }
            else
            {
                shake_count++;
            }
        }

        //return a data set that shows how many times a shake check was successful.
        if (shake_count == 4)
            return true;
        else
            return false;
    }

    private float get_bonus_value()
    {
        if (special_effect == Special_Status_Effect.Freeze || special_effect == Special_Status_Effect.Sleep)
        {
            return 2.5f;
        }
        else if (pokemon.Status == Pokemon.Status_Effects.Burn || pokemon.Status == Pokemon.Status_Effects.Paralyze || pokemon.Status == Pokemon.Status_Effects.Poison || pokemon.Status == Pokemon.Status_Effects.Bad_Poison)
        {
            return 1.5f;
        }

        return 1;
    }

    private void Update_Evasion_Stage(int amount)
    {
        evasion_stage = update_Stat(evasion_stage, amount, eva_change, "Evasion");
    }
    private void Update_Accuracy_Stage(int amount)
    {
        accuracy_stage = update_Stat(accuracy_stage, amount, acc_change, "Accuracy");
    }
    
    private void Update_Attack_Stage(int amount)
    {
        attack_stat_stage = update_Stat(attack_stat_stage, amount, stat_change,"Attack");
    }
    private void Update_Defense_Stage(int amount)
    {
        defense_stat_stage = update_Stat(defense_stat_stage, amount, stat_change,"Defense");
    }
    private void Update_Sp_Attack_Stage(int amount)
    {
        spattack_stat_stage = update_Stat(spattack_stat_stage, amount, stat_change,"Sp.Attack");
    }
    private void Update_Sp_Defense_Stage(int amount)
    {
        spdefense_stat_stage = update_Stat(spdefense_stat_stage, amount, stat_change,"Sp.Defense");
    }
    private void Update_Speed_Stage(int amount)
    {
        speed_stat_stage = update_Stat(speed_stat_stage, amount, stat_change,"Speed");
    }
    
    private int update_Stat(int stat, int amount, float[] array, string stat_name)
    {
        if (amount < 0)
        {
            if (stat == 0)
            {
                Console.WriteLine($"Couldn't lower {stat_name}!");
            }
        }
        else if (amount > 0)
        {
            if (stat == array.Length - 1)
            {
                Console.WriteLine($"Couldn't raise {stat_name}!");
            }
        }
        
        stat += amount;

        return Mathf.Clamp(stat, 0, array.Length - 1);
    }
    
    #region Property

    public BattleParticipant Trainer => trainer;
    
    public Pokemon Pokemon => pokemon;

    public Move CurrentMove
    {
        get => current_move;
        set => current_move = value;
    }
    public Move LastMove
    {
        get => last_move;
        set => last_move = value;
    }

    public bool HasGone
    {
        get => hasGone;
        set => hasGone = value;
    }

    public Turn_Type Turn
    {
        get => turn;
        set => turn = value;
    }

    public float Evasion => eva_change[evasion_stage];
    public float Accuracy => acc_change[accuracy_stage];
    
    public int AttackStat => (int) (pokemon.CalAttackStat * stat_change[attack_stat_stage]);
    public int DefenseStat => (int) (pokemon.CalDefenseStat * stat_change[defense_stat_stage]);
    public int SpAttackStat => (int) (pokemon.CalSpAttackStat * stat_change[spattack_stat_stage]);
    public int SpDefenseStat => (int) (pokemon.CalSpDefenseStat * stat_change[spdefense_stat_stage]);
    public int SpeedStat => (int) (pokemon.CalSpeedStat * stat_change[speed_stat_stage]);

    #endregion
}