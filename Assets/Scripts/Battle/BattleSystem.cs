using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleSystem : MonoBehaviour
{
    public static BattleSystem Instance;
    
    [SerializeField] private bool has_Started;

    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject option_ui;
    [SerializeField] private GameObject attack_ui;

    [SerializeField] private BattleParticipant player;
    [SerializeField] private BattleParticipant enemy;
    
    [SerializeField] private List<BattlePokemon> pokemon;

    private BattlePokemon currentPlayerPokemonSelecting;
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject);
        }
        
        Instance = this;
        
        option_ui.SetActive(false);
        attack_ui.SetActive(false);
        ui.SetActive(false);
    }

    public void Start_Battle(Player player, Pokemon wild)
    {
        this.player = new BattleParticipant(player);
        enemy = new BattleParticipant(new OverworldEntity(), wild);
        
        pokemon = new List<BattlePokemon>();

        BattlePokemon p = this.player.get_First_Battle_Ready_Pokemon();
        BattlePokemon e = enemy.get_First_Battle_Ready_Pokemon();
        
        p.addEnemyUniqueIds(e.Pokemon.UniqueId);
        
        pokemon.Add(p);
        pokemon.Add(e);

        currentPlayerPokemonSelecting = pokemon[0];
        
        has_Started = true;

        ui.SetActive(true);
        option_ui.SetActive(true);
        
        StartCoroutine(Wait_For_Turn());
    }
    
    private void endBattle()
    {
        has_Started = false;
    
        StopAllCoroutines();
        
        pokemon = null;
        player = null;
        enemy = null;
        currentPlayerPokemonSelecting = null;
        
        ui.SetActive(false);
        option_ui.SetActive(false);
        attack_ui.SetActive(false);
    }

    private IEnumerator Wait_For_Turn()
    {
        int ready = 0;

        while (has_Started)
        {
            for (int i = 0; i < pokemon.Count; i++)
            {
                if (pokemon[i].Turn != BattlePokemon.Turn_Type.None)
                    ready++;
            }

            if (ready == pokemon.Count)
            {
                pokemon = new List<BattlePokemon>(pokemon.OrderByDescending(o => o.SpeedStat));
                StartCoroutine(prepare_turns());
                ready = 0;
            }
            else
            {
                ready = 0;
            }

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
    }

    private IEnumerator prepare_turns()
    {
        for (int i = 0; i < pokemon.Count; i++)
        {
            if (!pokemon[i].HasGone)
            {
                if (pokemon[i].Turn == BattlePokemon.Turn_Type.Run)
                {
                    BattleParticipant participant = player.isPokemonFromParticipant(pokemon[i]) ? player : enemy;
                    
                    if (!attempToEscape(participant))
                    {
                        participant.incrementEscapeAttempts();
                        pokemon[i].HasGone = true;
                    }
                    else
                    {
                        endBattle();
                        break;
                    }
                }
                else if (pokemon[i].Turn == BattlePokemon.Turn_Type.Switch)
                {
                    pokemon[i].HasGone = true;
                }
                else if (pokemon[i].Turn == BattlePokemon.Turn_Type.Item)
                {
                    pokemon[i].HasGone = true;
                }
            }
        }

        if (pokemon != null)
        {
            for (int i = 0; i < pokemon.Count; i++)
            {
                if (!pokemon[i].HasGone)
                {
                    if (pokemon[i].Turn == BattlePokemon.Turn_Type.Attack)
                    {
                        BattlePokemon target = getPokemonTarget(pokemon[i]);
                        if (!Attack(pokemon[i], target))
                        {
                            if (!battleCanContinue())
                            {
                                endBattle();
                                break;
                            }
                            else
                            {
                                //Get next pokemon
                            }
                        }
                    }
                }
            }

            yield return new WaitForSeconds(0.1f);
        }

        if (has_Started)
        {
            reset_turn();
        }
    }

    private void reset_turn()
    {
        for (int i = 0; i < pokemon.Count; i++)
        {
            pokemon[i].Turn = BattlePokemon.Turn_Type.None;
            pokemon[i].LastMove = pokemon[i].CurrentMove;
            pokemon[i].CurrentMove = null;
            pokemon[i].HasGone = false;
        }
        
        option_ui.SetActive(true);
        attack_ui.SetActive(false);
    }

    private IEnumerator playerSwitchPokemon()
    {
        yield return new WaitForSeconds(0);
    }
    
    //DEBUG
    private BattlePokemon getPokemonTarget(BattlePokemon self)
    {
        if (self.CurrentMove.MoveTarget == MoveData.Move_Target.Self)
        {
            return self;
        }
        else
        {
            if (player.isPokemonFromParticipant(self))
            {
                return get_Enemy_Pokemon();
            }
            else
            {
                return get_Player_Pokemon();
            }
        }
    }
    
    private bool Attack(BattlePokemon attacker, BattlePokemon defender)
    {
        attacker.HasGone = true;
        
        if (does_move_hit(attacker.CurrentMove, attacker, defender))
        {
            if (attacker.CurrentMove.Power != 0)
            {
                defender.Take_Damage(attacker.CurrentMove, attacker);

                if (defender.Pokemon.Status == Pokemon.Status_Effects.Fainted)
                {
                    if (attacker.Trainer == player)
                    {
                        List<BattlePokemon> foughtAgainstDefeated = player.checkEnemyUniqueID(defender.Pokemon.UniqueId);

                        if (foughtAgainstDefeated.Count == 1)
                        {
                            attacker.Pokemon.AddExp(calculate_exp_earned(defender, attacker));
                            pokemon.Remove(defender);
                            return false;
                        }
                        else if(foughtAgainstDefeated.Count > 1)
                        {
                            int exp = calculate_exp_earned(defender, attacker);
                            exp /= 2;
                            
                            attacker.Pokemon.AddExp(exp);
                            foughtAgainstDefeated.Remove(attacker);

                            int remainingExp = exp / foughtAgainstDefeated.Count;

                            for (int i = 0; i < foughtAgainstDefeated.Count; i++)
                            {
                                foughtAgainstDefeated[i].Pokemon.AddExp(remainingExp);
                            }
                        }
                        else
                        {
                            Debug.Log("There weren't any pokemon who fought against this pokemon?");
                        }
                    }
                }
            }
            
            ApplyModifier(attacker.CurrentMove, attacker, defender);
        }


        return true;
    }

    private bool battleCanContinue()
    {
        if (!player.hasActivePokemon() || !enemy.hasActivePokemon())
        {
            Debug.Log("Player out of pokemon");
            return false;
        }
        else
        {
            return true;
        }
    }
    
    private void ApplyModifier(Move move, BattlePokemon attacker, BattlePokemon defender)
    {
        if (move.Effect != null)
        {
            for (int i = 0; i < move.Effect.Length; i++)
            {
                if (move.Effect[i] is Chance_MoveEffect)
                {
                    Chance_MoveEffect chance = (Chance_MoveEffect) move.Effect[i];
                    if (move.Effect[i] is SelfEffect)
                    {
                        chance.Apply_Effect(attacker);
                    }
                    else if (move.Effect != null)
                    {
                        if (move.Effect is SelfEffect)
                        {
                            chance.Apply_Effect(attacker);
                        }
                    }
                    else
                    {
                        chance.Apply_Effect(defender);
                    }
                }
                else
                {
                    move.Effect[i].Apply_Effect(defender);
                }
            }
        }
    }

    private BattlePokemon get_Player_Pokemon()
    {
        for (int i = 0; i < pokemon.Count; i++)
        {
            if (player.isPokemonFromParticipant(pokemon[i]))
            {
                return pokemon[i];
            }
        }

        return null;
    }
    
    //Debug
    private BattlePokemon get_Enemy_Pokemon()
    {
        for (int i = 0; i < pokemon.Count; i++)
        {
            if (enemy.isPokemonFromParticipant(pokemon[i]))
            {
                return pokemon[i];
            }
        }

        return null;
    }
    
    private int calculate_exp_earned(BattlePokemon defeated, BattlePokemon attacker)
    {
        float a = !enemy.isPokemonFromParticipant(defeated) ? 1.5f : 1f;
        float b = defeated.Pokemon.TrainingData.BaseExp;

        float e = 1;
        if (defeated.Pokemon.HeldItem != null)
        {
            e = defeated.Pokemon.HeldItem.ItemName == "Lucky Egg" ? 1.5f : 1f;
        }

        float L = defeated.Pokemon.Level;
        float Lp = attacker.Pokemon.Level;
        
//        float p
        // s equals : if no pokemon has exp share on, then s equals the # of pokemon that participated and havent fainted
        //if at least one pokemon has an exp share, then s equals 2 * # of pokemon that participated that havent fainted
        float s = 1;
        
        //t equals 1 if the pokemon's current is its original, and 1.5f if not. Might reverse this
        float t = 1;

        return (int) ((((a * b * L) / (5 * s)) * Math.Pow((2 * L + 10) / (L + Lp + 10), 2.5f) + 1) * t * e);
    }

    private bool does_move_hit(Move move, BattlePokemon attacker, BattlePokemon defender)
    {
        float accuracy = (move.Accuracy / 100f) * attacker.Accuracy * defender.Evasion;

        float random = Random.Range(0, 1);

        return (random <= accuracy);
    }

    private void getEnemyMoves()
    {
        BattlePokemon pokemon = get_Enemy_Pokemon();
        pokemon.CurrentMove = pokemon.Pokemon.Moves[0];
        pokemon.Turn = BattlePokemon.Turn_Type.Attack;
    }
    
    #region Button_Options

    public void getPlayerOption(Text button)
    {
        switch (button.text)
        {
            case "Attack":
                
                option_ui.SetActive(false);
                attack_ui.SetActive(true);
                loadPokemonMoves();
                break;
            case "Pokemon":
                currentPlayerPokemonSelecting.Turn = BattlePokemon.Turn_Type.Switch;
                break;
            case "Item":
                currentPlayerPokemonSelecting.Turn = BattlePokemon.Turn_Type.Item;
                break;
            case "Run":
                currentPlayerPokemonSelecting.Turn = BattlePokemon.Turn_Type.Run;
                getEnemyMoves();
                break;
            
            default:
                break;
        }
    }

    public void goToPlayerOption()
    {
        option_ui.SetActive(true);
        attack_ui.SetActive(false);
    }

    private bool attempToEscape(BattleParticipant participant)
    {
        if (currentPlayerPokemonSelecting.SpeedStat > get_Enemy_Pokemon().SpeedStat)
        {
            return true;
        }
        else
        {
            float a = (((currentPlayerPokemonSelecting.Pokemon.CalSpeedStat * 128) / get_Enemy_Pokemon().Pokemon.CalSpeedStat) + 30 * participant.EscapeAttempts) / 256f;
            float rand = Random.Range(0, 1);
            return rand <= a;
        }
    }
    
    #endregion

    #region Button_Attack

    public void setPokemonMove(GameObject obj)
    {
        currentPlayerPokemonSelecting.CurrentMove = currentPlayerPokemonSelecting.Pokemon.Moves[Int32.Parse(obj.name)];
        currentPlayerPokemonSelecting.Turn = BattlePokemon.Turn_Type.Attack;

        getEnemyMoves();
    }
    
    private void loadPokemonMoves()
    {
        for (int j = 0; j < currentPlayerPokemonSelecting.Pokemon.Moves.Count; j++)
        {
            attack_ui.transform.GetChild(j).name = $"{j}";
            attack_ui.transform.GetChild(j).transform.GetChild(0).GetComponent<Text>().text = currentPlayerPokemonSelecting.Pokemon.Moves[j].MoveName;
        }
        
        for (int j = 0; j < attack_ui.transform.childCount; j++)
        {
            if (attack_ui.transform.GetChild(j).transform.GetChild(0).GetComponent<Text>().text == "Empty")
            {
                attack_ui.transform.GetChild(j).gameObject.SetActive(false);
            }
        }
    }

    #endregion

    public bool HasStarted => has_Started;
}