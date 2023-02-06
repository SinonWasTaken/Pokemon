using UnityEngine;

public class Trainer : MonoBehaviour
{
    [SerializeField] private string trainer_name;

    public void set_Trainer_Name(string name)
    {
        trainer_name = name;
    }

    public string TrainerName => trainer_name;
}
