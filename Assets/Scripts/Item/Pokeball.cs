using System;
using UnityEngine;

[Serializable]
public class Pokeball : ItemData
{
    public enum Effective_Area
    {
        None,
        Tall_Grass,
        Water,
        Underwater,
        Dark
    }
    public enum Pokeball_Type
    {
        None,
        Quick,
        Timer,
        NoFail,
        Heal,
        Friendly,
        Repeat
    }

    [SerializeField] private PokemonData.PokemonType effective_type;

    [SerializeField] private Effective_Area _effectiveArea;
    [SerializeField] private Pokeball_Type _type;

    [SerializeField] private float _catchRate;

    public Pokeball(string itemName, string itemDescription, Item_Type type, int cost, int fling, PokemonData.PokemonType effectiveType, Effective_Area effectiveArea, Pokeball_Type type2, float catchRate) : base(itemName, itemDescription, type, cost, fling)
    {
        effective_type = effectiveType;
        _effectiveArea = effectiveArea;
        _type = type2;
        _catchRate = catchRate;
    }

    public PokemonData.PokemonType EffectiveType => effective_type;

    public Effective_Area EffectiveArea => _effectiveArea;
    public Pokeball_Type Type1 => _type;

    public float CatchRate => _catchRate;
}