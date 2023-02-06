using System;
using UnityEngine;

[Serializable]
public class MoveData
{
    //Enum for the moves targeted entity
    public enum Move_Target { Self, Opponent, Ally, Adjacent, All}
    //What category this move fall into
    public enum Move_Category { Physical, Status, Special}

    //The name of the move
    [SerializeField] private string move_name;
    //The description of the move
    [SerializeField] private string move_description;
    
    //The type of the move. May do extra damage against weak types
    [SerializeField] private PokemonData.PokemonType move_type;

    //The base damage for the move. Used to calculate the final damage dealt to an opponent
    [SerializeField] private int power;

    //The base amount of times a move can be used, before it can no longer be used
    [SerializeField] private int base_pp;
    //The maximum value that the moves pp value can get to
    [SerializeField] private int max_pp;

    //Used to determine if the moves hits an opponent
    [SerializeField] private int accuracy;

    [SerializeField] private Move_Target move_target;

    [SerializeField] private Move_Category move_Category;

    //The effects that this move may cause, such as burn, sleep, or fly etc...
    [SerializeField] private Move_Effect[] effect;

    //Used to detmerine which pokemon will move first. This is used specifically for moves such as quick attack
    [SerializeField] private int speed_priority;
    
    [SerializeField] private bool sound_type;
    [SerializeField] private bool punch_move;
    [SerializeField] private bool snatchable;
    //grounds an opponent when hit
    [SerializeField] private bool grounded_by_gravity;
    //defrosts the opponent, or use, not sure, when used
    [SerializeField] private bool defrost_when_used;
    //can this move be reflected 
    [SerializeField] private bool reflected_by_magic_coat_or_magic_bounce;
    //can this move be blocked by protect or detect
    [SerializeField] private bool blocked_by_protec_detect;
    //can this move be copied by mirror move
    [SerializeField] private bool copyable_by_mirror_move;

    //Used for the editor
    public bool editor_is_open;
    
    public MoveData(MoveData data)
    {
        move_type = data.move_type;
        move_name = data.move_name;
        move_description = data.move_description;
        power = data.power;
        base_pp = data.base_pp;
        max_pp = data.max_pp;
        accuracy = data.accuracy;
        move_target = data.move_target;
        move_Category = data.move_Category;
        effect = data.effect;
        speed_priority = data.speed_priority;
        sound_type = data.sound_type;
        punch_move = data.punch_move;
        snatchable = data.snatchable;
        grounded_by_gravity = data.grounded_by_gravity;
        defrost_when_used = data.defrost_when_used;
        reflected_by_magic_coat_or_magic_bounce = data.reflected_by_magic_coat_or_magic_bounce;
        blocked_by_protec_detect = data.blocked_by_protec_detect;
        copyable_by_mirror_move = data.copyable_by_mirror_move;
    }
    
    public MoveData(PokemonData.PokemonType move_type, string move_name, string move_description, int power, int base_pp, int max_pp, int accuracy, Move_Target move_target, Move_Category move_Category, Move_Effect[] effect, int speed_priority, bool sound_type, bool punch_move, bool snatchable, bool grounded_by_gravity, bool defrost_when_used, bool reflected_by_magic_coat_or_magic_bounce, bool blocked_by_protec_detect, bool copyable_by_mirror_move)
    {
        this.move_type = move_type;
        this.move_name = move_name;
        this.move_description = move_description;
        this.power = power;
        this.base_pp = base_pp;
        this.max_pp = max_pp;
        this.accuracy = accuracy;
        this.move_target = move_target;
        this.move_Category = move_Category;
        this.effect = effect;
        this.speed_priority = speed_priority;
        this.sound_type = sound_type;
        this.punch_move = punch_move;
        this.snatchable = snatchable;
        this.grounded_by_gravity = grounded_by_gravity;
        this.defrost_when_used = defrost_when_used;
        this.reflected_by_magic_coat_or_magic_bounce = reflected_by_magic_coat_or_magic_bounce;
        this.blocked_by_protec_detect = blocked_by_protec_detect;
        this.copyable_by_mirror_move = copyable_by_mirror_move;
    }

    public bool EditorIsOpen => editor_is_open;
    public PokemonData.PokemonType MoveType => move_type;
    public string MoveName => move_name;
    public string MoveDescription => move_description;
    public int Power => power;
    public int BasePp => base_pp;
    public int MaxPp => max_pp;
    public int Accuracy => accuracy;
    public Move_Target MoveTarget => move_target;
    public Move_Category MoveCategory => move_Category;
    public Move_Effect[] Effect => effect;
    public int SpeedPriority => speed_priority;
    public bool SoundType => sound_type;
    public bool PunchMove => punch_move;
    public bool Snatchable => snatchable;
    public bool GroundedByGravity => grounded_by_gravity;
    public bool DefrostWhenUsed => defrost_when_used;
    public bool ReflectedByMagicCoatOrMagicBounce => reflected_by_magic_coat_or_magic_bounce;
    public bool BlockedByProtecDetect => blocked_by_protec_detect;
    public bool CopyableByMirrorMove => copyable_by_mirror_move;
}

public class Move_Effect
{
    //The effects a move can have
    public enum Effect { Confuse, Sleep, Poison, Freeze, Attack, Defense, SpAttack, SpDefense, Speed, Burn, Flinch, Recharge, Accuracy, Evasion, Critical, HP_Drain, Berry_Steal, Ally_Switch, All_Stats, RandomStat, Heal_Self, X_Hits, Paralyze, Bad_Poison }

    //the effect
    private Effect effect;
    //the value that determines its potency, or chance of something occuring. EX: Move == growl: effect == attack: value == -1 IE lowers the attack stage by 1 
    private float value;

    public Move_Effect(Effect effect, float value)
    {
        this.effect = effect;
        this.value = value;
    }

    public virtual void Apply_Effect(BattlePokemon pokemon)
    {
        pokemon.Apply_Effect(this);
    }

    public Effect M_Effect => effect;
    public float Value => value;
}
