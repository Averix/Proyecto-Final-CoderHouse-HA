using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour
{
    // Players HUD
    [SerializeField] private GameObject[] playerHUD;

    private void Awake()
    {
        // Disables all players HUD
        for (int i = 0; i < playerHUD.Length; i++)
        {
            playerHUD[i].SetActive(false);
        }
        // Enables first player HUD
        playerHUD[0].SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Enables a Player HUD
    public void ActivePlayerHUD(int player)
    {
        playerHUD[player].SetActive(true);
    }
}
