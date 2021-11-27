[System.Serializable]
public class MoveData
{
    public bool editor_is_open;

    public enum Move_Target { Self, Opponent, Ally, Adjacent, All}
    public enum Move_Category { Physical, Status, Special}

    public PokemonData.PokemonType move_type { get; private set; }

    public string move_name { get; private set; }
    public string move_description { get; private set; }

    public int power { get; private set; }

    public int base_pp { get; private set; }
    public int max_pp { get; private set; }

    public int accuracy { get; private set; }

    public Move_Target move_target {  get; private set; }

    public Move_Category move_Category { get; private set; }

    public Move_Effect[] effect { get; private set; }

    public int speed_priority { get; private set; }

    public bool sound_type { get; private set; }
    public bool punch_move { get; private set; }
    public bool snatchable { get; private set; }
    public bool grounded_by_gravity { get; private set; }
    public bool defrost_when_used { get; private set; }
    public bool reflected_by_magic_coat_or_magic_bounce { get; private set; }
    public bool blocked_by_protec_detect { get; private set; }
    public bool copyable_by_mirror_move { get; private set; }

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
}

public class Move_Effect
{
    public enum Effect { Chance, Confuse, Sleep, Poision, Freeze, Attack, Defense, SpAttack, SpDefense, Speed, AttackSelf, DefenseSelf, SpAttackSelf, SpDefenseSelf, SpeedSelf, Burn, Flinch, Recharge, Accuracy, Evasion, AccuracySelf, EvasionSelf, Critical, HP_Drain, Berry_Steal }

    public Effect effect { get; private set; }
    public float value { get; private set; }

    public Move_Effect(Effect effect, float value)
    {
        this.effect = effect;
        this.value = value;
    }
}
