using System.Collections;
using UnityEngine;

public class Pokecenter : MonoBehaviour, Interact
{
    [SerializeField] private Question_Dialogue dialogue;
    
    public void Interact()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(Player player)
    {
        StartCoroutine(askForHeal(player));
    }

    private IEnumerator askForHeal(Player player)
    {
        Dialogue_System.Instance.Start_Dialogue(dialogue);
        yield return new WaitUntil(() => Dialogue_System.Instance.DialogueActive == false);
        
        for (int i = 0; i < player.PlayerParty.Count; i++)
        {
            Pokemon pokemon = (Pokemon) player.PlayerParty[i];
            if (pokemon != null)
            {
                pokemon.Heal();
            }
        }
    }
}
