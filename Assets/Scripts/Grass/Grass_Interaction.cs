using UnityEngine;

public class Grass_Interaction : MonoBehaviour
{
    [SerializeField] private Grass_Area data;

    [SerializeField] private Player player;
    
    void Awake()
    {
        data = transform.parent.GetComponent<Grass_Area>();

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (data.Begin_Battle())
            {
                Pokemon pokemon = data.Get_Pokemon();

                if (pokemon != null)
                {
                    BattleSystem.Instance.Start_Battle(player, pokemon);
                }
            }
        }
    }
}
