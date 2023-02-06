using System;
using System.Collections.Generic;
using UnityEngine;

public class Move_Database : MonoBehaviour
{
    public static Move_Database Instance;
    
    [SerializeField] private List<MoveData> data;

    public Move_Database()
    {
        Instance = this;
        
        data = new List<MoveData>
        {
            //A moves
            new MoveData(PokemonData.PokemonType.Grass, "Absorb", "User recovers half the HP inflicted on opponent.", 20, 25, 40, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Special, new Move_Effect[] { new Move_Effect(Move_Effect.Effect.HP_Drain, 0.5f)}, 0, false, false, false, false, false, false, true, true),
            new MoveData(PokemonData.PokemonType.Poison, "Acid", "The opposing team is attacked with a spray of harsh acid. The acid may also lower the targets� Sp. Def stats.", 40, 30, 48, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Special, new Move_Effect[] { new Chance_MoveEffect(10, Move_Effect.Effect.SpDefense, -1)}, 0, false, false, false, false,false, false, true, true),
            new MoveData(PokemonData.PokemonType.Poison, "Acid Armor", "The user alters its cellular structure to liquefy itself, sharply raising its Defense stat.", 0, 40, /*Look for moves max PP*/0, 0, MoveData.Move_Target.Self, MoveData.Move_Category.Status,new Move_Effect[] {new SelfEffect(Move_Effect.Effect.Defense, 2)}, 0, false, false, true, false, false, false, false, false),
            new MoveData(PokemonData.PokemonType.Poison, "Acid Spray", "The user spits fluid that works to melt the target. This harshly reduces the target's Sp. Def stat.", 40, 20, /*Look for moves max PP*/0, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Special, /*Check for move effect*/ null, 0, false, false, false, false, false, false, true, true),
            new MoveData(PokemonData.PokemonType.Flying, "Acrobatics", "The user nimbly strikes the target. If the user is not holding an item, this attack inflicts massive damage.", 55, 15, /*Look for moves max PP*/0, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Physical, /*Check for move effect*/ null, 0, false, false, false, false, false, false, true, true),
            new MoveData(PokemonData.PokemonType.Normal, "Acupressure", "The user applies pressure to stress points, sharply boosting one of its stats.", 0, 30, /*Look for moves max PP*/0, 0, MoveData.Move_Target.Self, MoveData.Move_Category.Status, /*Check for move effect*/ new Move_Effect[] {new SelfEffect(Move_Effect.Effect.RandomStat, 2)}, 0, false, false, false, false, false, false, false, false),
            new MoveData(PokemonData.PokemonType.Flying, "Aerial Ace", "The user confounds the target with speed, then slashes. The attack lands without fail.", 60, 20, /*Look for moves max PP*/0, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Physical, /*Check for move effect*/ null, 0, false, false, false, false, false, false, true, true),
            new MoveData(PokemonData.PokemonType.Flying, "Aeroblast", "A vortex of air is shot at the target to inflict damage. Critical hits land more easily.", 100, 5, 8, 95, MoveData.Move_Target.Opponent, MoveData.Move_Category.Special, new Move_Effect[] { new Chance_MoveEffect(50f, Move_Effect.Effect.Critical, 1f) }, 0, false, false, false, false, false, true, true, true),
            new MoveData(PokemonData.PokemonType.Normal, "After you", "The user helps the target and makes it use its move after the user.", 0, 15, 24,0, MoveData.Move_Target.Opponent, MoveData.Move_Category.Status, null, 0, false, false, false, false, false, false, false, false),
            new MoveData(PokemonData.PokemonType.Psychic, "Agility", "The user relaxes and lightens its body to move faster. It sharply boots the Speed stat.",  0, 30, 48, 0, MoveData.Move_Target.Self, MoveData.Move_Category.Status, new Move_Effect[] { new SelfEffect(Move_Effect.Effect.Speed, 2)}, 0, false, false, true, false, false, false, false, false),
            new MoveData(PokemonData.PokemonType.Flying, "Air cutter", "The user launches razor-like wind to slash the opposing team. Critical hits land more easily.", 55, 25, 40, 95, MoveData.Move_Target.All, MoveData.Move_Category.Special, new Move_Effect[] { new Chance_MoveEffect(30f, Move_Effect.Effect.Critical, 1)}, 0, false, false, false, false, false, false, true, true),
            new MoveData(PokemonData.PokemonType.Flying, "Air Slash", "The user attacks with a blade of ait that slices even the sky. It may also make the target flinch.", 75, 20, 28, 95,  MoveData.Move_Target.Opponent, MoveData.Move_Category.Special, new Move_Effect[] {new Chance_MoveEffect(30f, Move_Effect.Effect.Flinch, 1)}, 0, false, false, false, false, false, false, true, true),
            new MoveData(PokemonData.PokemonType.Psychic, "Ally Switch", "The user teleports using a strange power and switches its place with one of its allies", 0, 15, 24, 0, MoveData.Move_Target.Self, MoveData.Move_Category.Status, new Move_Effect[] {new SelfEffect(Move_Effect.Effect.Ally_Switch, 1)}, 1, false, false, false, false, false, false, false, false),
            new MoveData(PokemonData.PokemonType.Psychic, "Amnesia", "The user temporarily empties its mind to forget its concerns. It sharply raises the user's Sp.Defense stat.", 0, 20, 32, 0, MoveData.Move_Target.Self, MoveData.Move_Category.Status, new Move_Effect[] {new SelfEffect(Move_Effect.Effect.SpDefense, 2)}, 0, false, false, true, false, false, false, false, false),
            new MoveData(PokemonData.PokemonType.Rock, "Ancientpower", "The user attacks with a prehistoric power. It may also raise all the user's stats at once", 60, 5, 7, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Special, new Move_Effect[] { new Chance_MoveEffect(10f, new SelfEffect(Move_Effect.Effect.All_Stats, 1)) }, 0, false, false, false, false, false, false, true, true),
            new MoveData(PokemonData.PokemonType.Water, "Aqua Jet", "The user lunges at the target at a speed that makes it almost invisible. It is sure to strike first.", 40, 20, 32, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Physical, null, 1, false, false, false, false, false, false, true, true),
            new MoveData(PokemonData.PokemonType.Water, "Aqua Ring", "The user envelopes itself in a veil made of water. It regains some HP on every turn.", 0, 20, 32, 0, MoveData.Move_Target.Self, MoveData.Move_Category.Status, new Move_Effect[] {new SelfEffect(Move_Effect.Effect.Heal_Self, 16)}, 0, false, false, true, false, false, false, false, false),
            new MoveData(PokemonData.PokemonType.Water, "Aqua Tail", "The user attacks by swinging its tail as if it were a vicious wave in a raging storm.", 90, 10, 16, 90, MoveData.Move_Target.Opponent, MoveData.Move_Category.Physical, null, 0, false, false, false, false, false, false,true, true),
            new MoveData(PokemonData.PokemonType.Fighting, "Arm Thrust", "The user looses a flurry of open-palmed arm thrusts that hit two to five times in a row.", 15, 20, 32, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Physical, new Move_Effect[] {new Move_Effect(Move_Effect.Effect.X_Hits, 2), new Move_Effect(Move_Effect.Effect.X_Hits, 5)}, 0, false, false, false, false, false, false, true, true),

            //L
            new MoveData(PokemonData.PokemonType.Normal, "Leer", "The opposing team gains an intimidating leer with sharp eyes. The opposing team’s Defense stats are reduced.", 0, 30, 48, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Status, new Move_Effect[] {new Move_Effect(Move_Effect.Effect.Defense, -1)}, 0, false, false, false, false, false, true, true, true),
            
            //T
            new MoveData(PokemonData.PokemonType.Normal, "Tackle", "A physical attack in which the user charges and slams into the target with its whole body.", 40, 35, 56, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Physical, null, 0, false, false, false,false, false, false, true, true),
            new MoveData(PokemonData.PokemonType.None, "Tail Whip", "The user wags its tail cutely, making opposing Pokémon less wary and lowering their Defense stat.", 0 , 30, 48, 100, MoveData.Move_Target.Opponent, MoveData.Move_Category.Status, new Move_Effect[] {new Move_Effect(Move_Effect.Effect.Defense, -1)}, 0, false, false, false, false, false, true, true, true),
            
            //Other
            new MoveData(PokemonData.PokemonType.None, "Empty", "", 0, 0, 0, 0, MoveData.Move_Target.Opponent, MoveData.Move_Category.Physical, null, 0, false, false, false, false, false, false, false, false)
        };
        
        //Need to check each move for a missing move_effect. The way I write things down doesnt include this information. remove once ive filled in all the missing information
        list_empty_move_effects();
    }

    //Debug method that will tell me what moves may be missing their MoveEffectData
    private void list_empty_move_effects()
    {
        foreach (MoveData moveData in data)
        {
            if (moveData.Effect == null)
            {
                Console.WriteLine($"{moveData.MoveName} is possibly missing its move_effect data!");
            }
        }
    }

    public MoveData get_move(string name)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i].MoveName == name)
            {
                return data[i];
            }
        }

        return data[data.Count - 1];
    }
}