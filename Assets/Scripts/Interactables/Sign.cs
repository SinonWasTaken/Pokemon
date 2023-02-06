using System;
using UnityEngine;

public class Sign : MonoBehaviour, Interact
{
    [SerializeField] private Dialogue_Lines lines;

    private void Awake()
    {
        Interact();
    }

    public void Interact()
    {
    }

    public void Interact(Player player)
    {
        Dialogue_System.Instance.Start_Dialogue(lines);
    }
}
