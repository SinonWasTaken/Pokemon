using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleParticipant
{
    [SerializeField] private OverworldEntity participant;
    [SerializeField] private bool isPlayer;
    [SerializeField] private int escapeAttempts;

    [SerializeField] private List<BattlePokemon> pokemon;
    
    public BattleParticipant(Player player)
    {
        escapeAttempts = 1;
        
        isPlayer = true;
        
        participant = player;
        pokemon = new List<BattlePokemon>();

        for (int i = 0; i < player.PlayerParty.Count; i++)
        {
            pokemon.Add(new BattlePokemon((Pokemon) player.PlayerParty[i], this));
        }
    }

    public BattleParticipant(OverworldEntity entity, Pokemon pokemon)
    {
        escapeAttempts = 1;
        isPlayer = false;

        participant = entity;
        this.pokemon = new List<BattlePokemon>();
        this.pokemon.Add(new BattlePokemon(pokemon, this));
    }

    public BattlePokemon get_First_Battle_Ready_Pokemon()
    {
        for (int i = 0; i < pokemon.Count; i++)
        {
            BattlePokemon battle = pokemon[i];
            if (battle.Pokemon.Status != Pokemon.Status_Effects.Fainted)
            {
                return battle;
            }
        }

        return null;
    }
    
    public List<BattlePokemon> checkEnemyUniqueID(string id)
    {
        List<BattlePokemon> battle = new List<BattlePokemon>(); 
        
        for (int i = 0; i < pokemon.Count; i++)
        {
            if (pokemon[i].hasEnemyUniqueId(id))
            {
                battle.Add(pokemon[i]);
            }
        }

        return battle;
    }
    
    public bool hasActivePokemon()
    {
        for (int i = 0; i < pokemon.Count; i++)
        {
            if (pokemon[i].Pokemon.Status != Pokemon.Status_Effects.Fainted)
            {
                return true;
            }
        }

        return false;
    }
    
    public bool isPokemonFromParticipant(BattlePokemon pokemon)
    {
        for (int i = 0; i < this.pokemon.Count; i++)
        {
            if (this.pokemon[i].Pokemon.UniqueId == pokemon.Pokemon.UniqueId)
            {
                return true;
            }
        }

        return false;
    } 
    
    public void incrementEscapeAttempts()
    {
        escapeAttempts++;
    }
    public void resetEscapeAttempts()
    {
        escapeAttempts = 0;
    }

    public int EscapeAttempts => escapeAttempts;
}
