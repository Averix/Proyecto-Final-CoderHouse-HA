using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuDataTransfer : MonoBehaviour
{
    // THE SOLE PURPOSE OF THIS SCRIPT IS TO TELL THE GAMEMANAGER THE AMOUNT OF PLAYERS
    // THIS WILL BE EXPANDED ONCE SETTINGS IS FINALIZED

    // MenuDataTransfer instance
    public static MenuDataTransfer instance;
    // Amount of players
    [SerializeField] [Range(1, 4)] public int numPlayers;
    [SerializeField] [Range(1, 4)] public int numTurns;

    void Awake()
    {
        // Singleton method
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to assign number of players
    public void PlayerAssign(int players)
    {
        numPlayers = players;
    }
    // Method to assign number of turns
    public void TurnrAssign(int turns)
    {
        numTurns = turns;
    }
}
