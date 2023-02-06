using System;
using UnityEngine;

[Serializable]
public class Move : MoveData
{
    [SerializeField] private int current_total_pp;
    [SerializeField] private int current_pp;

    public Move(MoveData data, int currentTotalPp, int currentPp) : base(data)
    {
        current_total_pp = currentTotalPp;
        current_pp = currentPp;
    }

    public Move(PokemonData.PokemonType move_type, string move_name, string move_description, int power, int base_pp, int max_pp, int accuracy, Move_Target move_target, Move_Category move_Category, Move_Effect[] effect, int speed_priority, bool sound_type, bool punch_move, bool snatchable, bool grounded_by_gravity, bool defrost_when_used, bool reflected_by_magic_coat_or_magic_bounce, bool blocked_by_protec_detect, bool copyable_by_mirror_move, int currentTotalPp, int currentPp) : base(move_type, move_name, move_description, power, base_pp, max_pp, accuracy, move_target, move_Category, effect, speed_priority, sound_type, punch_move, snatchable, grounded_by_gravity, defrost_when_used, reflected_by_magic_coat_or_magic_bounce, blocked_by_protec_detect, copyable_by_mirror_move)
    {
        current_total_pp = currentTotalPp;
        current_pp = currentPp;
    }

    public void Reset()
    {
        current_pp = current_total_pp;
    }
    
    public bool Use_Move()
    {
        if (current_pp == 0)
            return false;
        
        current_pp--;
        return true;
    }
    
    public void Raise_Current_PP(int amount)
    {
        current_total_pp += amount;
        current_total_pp = Mathf.Clamp(current_total_pp, 0, MaxPp);
    }
}