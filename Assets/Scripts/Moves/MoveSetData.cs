using System;
using UnityEngine;

[Serializable]
public class MoveSetData
{
    [SerializeField] private string[] hms;
    [SerializeField] private string[] tms;
    
    [SerializeField] private string[] learn;
    [SerializeField] private string[] special;
    [SerializeField] private string[] tutor;
    [SerializeField] private string[] egg;

    public MoveSetData(int natId)
    {
        string pokemon_name = Pokemon_Database.Instance.Get_Pokemon(natId).PokemonName;

        TextAsset line = Resources.Load<TextAsset>($"MoveSetData/{natId}{pokemon_name}/HM");
        if (line != null)
        {
            hms = line.text.Split('\n');
        }
        
        line = Resources.Load<TextAsset>($"MoveSetData/{natId}{pokemon_name}/TM");
        if (line != null)
        {
            tms = line.text.Split('\n');
        }
        
        line = Resources.Load<TextAsset>($"MoveSetData/{natId}{pokemon_name}/Learn");
        if (line != null)
        {
            learn = line.text.Split('\n');
        }
        
        line = Resources.Load<TextAsset>($"MoveSetData/{natId}{pokemon_name}/Egg");
        if (line != null)
        {
            egg = line.text.Split('\n');
        }
        
        line = Resources.Load<TextAsset>($"MoveSetData/{natId}{pokemon_name}/Special");
        if (line != null)
        {
            special = line.text.Split('\n');
        }
        
        line = Resources.Load<TextAsset>($"MoveSetData/{natId}{pokemon_name}/Tutor");
        if (line != null)
        {
            tutor = line.text.Split('\n');
        }
    }

    public string[] Hms => hms;
    public string[] Tms => tms;
    public string[] Learn => learn;
    public string[] Special => special;
    public string[] Tutor => tutor;
    public string[] Egg => egg;
}