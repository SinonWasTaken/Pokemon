using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Chance_MoveEffect : Move_Effect
{
    [SerializeField] private float chance_percent;
    private Move_Effect effect;
    
    public Chance_MoveEffect(float chancePercent, Effect effect, float value) : base(effect, value)
    {
        chance_percent = chancePercent;
    }

    public Chance_MoveEffect(float chancePercent, Move_Effect effect) : base(effect.M_Effect, effect.Value)
    {
        chance_percent = chancePercent;
        this.effect = effect;
    }

    public override void Apply_Effect(BattlePokemon pokemon)
    {
        float random = Random.Range(0, 100);
        if (random <= chance_percent)
        {
            pokemon.Apply_Effect(this);
        }
    }

    public Move_Effect Effect => effect;
    public float ChancePercent => chance_percent;
}