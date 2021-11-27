using System.Collections.Generic;
using UnityEngine;

public class MoveDatabase : MonoBehaviour
{
    public static List<MoveData> data;

    public void Awake() 
    {
        data = new List<MoveData>
        {
            //A move
            new MoveData(PokemonData.PokemonType.Grass, "Absorb", "User recovers half the HP inflicted on oppoenent.", 20, 25, 40, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Special, new Move_Effect[] { new Move_Effect(Move_Effect.Effect.HP_Drain, 0.5f)}, 0, false, false, false, false, false, false, true, true),
            new MoveData(PokemonData.PokemonType.Poison, "Acid", "The opposing team is attacked with a spray of harsh acid. The acid may also lower the targets� Sp. Def stats.", 40, 30, 48, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Special, new Move_Effect[] { new Move_Effect(Move_Effect.Effect.Chance, 10), new Move_Effect(Move_Effect.Effect.SpDefense, -1)}, 0, false, false, false, false,false, false, true, true),
            new MoveData(PokemonData.PokemonType.Poison, "Acid Armor", "The user alters its cellular structure to liquefy itself, sharply raising its Defense stat.", 0, 40, /*Look for moves max PP*/0, 0, MoveData.Move_Target.Self, MoveData.Move_Category.Status, /*Check for move effect*/ null, 0, false, false, true, false, false, false, false, false),
            new MoveData(PokemonData.PokemonType.Poison, "Acid Spray", "The user spits fluid that works to melt the target. This harshly reduces the target's Sp. Def stat.", 40, 20, /*Look for moves max PP*/0, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Special, /*Check for move effect*/ null, 0, false, false, false, false, false, false, true, true),
            new MoveData(PokemonData.PokemonType.Flying, "Acrobatics", "The user nimbly strikes the target. If the user is not holding an item, this attack inflicts massive damage.", 55, 15, /*Look for moves max PP*/0, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Physical, /*Check for move effect*/ null, 0, false, false, false, false, false, false, true, true),
            new MoveData(PokemonData.PokemonType.Normal, "Acupressure", "The user applies pressure to stress points, sharply boosting one of its stats.", 0, 30, /*Look for moves max PP*/0, 0, MoveData.Move_Target.Self, MoveData.Move_Category.Status, /*Check for move effect*/ null, 0, false, false, false, false, false, false, false, false),
            new MoveData(PokemonData.PokemonType.Flying, "Aerial Ace", "The user confounds the target with speed, then slashes. The attack lands without fail.", 60, 20, /*Look for moves max PP*/0, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Physical, /*Check for move effect*/ null, 0, false, false, false, false, false, false, true, true),

        };
        
        //Need to check each move for a missing move_effect. The way I write things down doesnt include this information. remove once ive filled in all the missing information
        list_empty_move_effects();
    }

    private void list_empty_move_effects()
    {
        foreach (MoveData moveData in data)
        {
            if (moveData.effect == null)
            {
                Debug.Log(moveData.move_name + " is possibly missing its move_effect data!");
            }
        }
    }
}
