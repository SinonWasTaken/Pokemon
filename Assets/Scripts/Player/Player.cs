using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : OverworldEntity
{
    [SerializeField] private string player_name;

    [SerializeField] private string unique_player_id;

    [SerializeField] private int currency;

    [SerializeField] private List<PokemonData> player_party;

    [SerializeField] private float distance = 5f;
    
    private Player_Controls controller;

    [SerializeField] private Transform interactPosition;
    
    private void Awake()
    {
        controller = new Player_Controls();
        controller.Controls.Interact.performed += InteractOnperformed;
        controller.Enable();
    }

    private void InteractOnperformed(InputAction.CallbackContext obj)
    {
        Ray ray = new Ray(interactPosition.position, interactPosition.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            Debug.Log(hit.transform.name);
            Interact interact = hit.transform.GetComponent<Interact>();
            if (interact != null)
            {
                interact.Interact(this);
            }
        }
    }

    private void Start()
    {
        player_party = new List<PokemonData>();
        
        Add_Pokemon(GeneratePokemon.Generate(495, 5));
    }

    public void Add_Pokemon(PokemonData pokemon)
    {
        if (player_party.Count != 6)
        {
            player_party.Add(pokemon);
        }
        else
        {
            //Add to pc here
        }
    }

    #region Properties
    public string PlayerName => player_name;
    public string UniquePlayerId => unique_player_id;

    public int Currency => currency;

    public List<PokemonData> PlayerParty => player_party;
    #endregion
}