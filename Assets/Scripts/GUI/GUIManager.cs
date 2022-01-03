using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    // Players HUD
    [SerializeField] private GameObject[] playerHUD;
    // Players Stats
    public Dictionary<int, TextMeshProUGUI[]> players = new Dictionary<int, TextMeshProUGUI[]>();
    public Dictionary<int, int[]> playersStats = new Dictionary<int, int[]>();
    [SerializeField] private TextMeshProUGUI[] statsPlayer1 = new TextMeshProUGUI[3];
    [SerializeField] private int[] statsValueP1 = new int[3];
    [SerializeField] private TextMeshProUGUI[] statsPlayer2 = new TextMeshProUGUI[3];
    [SerializeField] private int[] statsValueP2 = new int[3];
    [SerializeField] private TextMeshProUGUI[] statsPlayer3 = new TextMeshProUGUI[3];
    [SerializeField] private int[] statsValueP3 = new int[3];
    [SerializeField] private TextMeshProUGUI[] statsPlayer4 = new TextMeshProUGUI[3];
    [SerializeField] private int[] statsValueP4 = new int[3];

    private void Awake()
    {
        // Disables all players HUD
        for (int i = 0; i < playerHUD.Length; i++)
        {
            playerHUD[i].SetActive(false);
        }
        // Enables first player HUD
        playerHUD[0].SetActive(true);

        // Resets all player stats
        StatsReset(statsPlayer1, statsValueP1);
        StatsReset(statsPlayer2, statsValueP2);
        StatsReset(statsPlayer3, statsValueP3);
        StatsReset(statsPlayer4, statsValueP4);

        // Sets Dictionary for external use
        DictionaryInitialization();
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

    // Resets all stats to 0
    public void StatsReset(TextMeshProUGUI[] playerStats, int[] statsValue)
    {
        for (int i = 0; i < playerStats.Length; i++)
        {
            statsValue[i] =  0;
            playerStats[i].text = "" + statsValue[i];
        }
    }

    // Increases Stats by 1
    public void StatIncrease (int playerIndex, int statIndex)
    {
        TextMeshProUGUI[] playerStats;
        int[] statsValue;
        players.TryGetValue(playerIndex, out playerStats);
        playersStats.TryGetValue(playerIndex, out statsValue);
        statsValue[statIndex] += 1;
        playerStats[statIndex].text = "" + statsValue[statIndex];
    }

    // Decreases Stats by 1
    public void StatDecrease(int playerIndex, int statIndex)
    {
        TextMeshProUGUI[] playerStats;
        int[] statsValue;
        players.TryGetValue(playerIndex, out playerStats);
        playersStats.TryGetValue(playerIndex, out statsValue);
        statsValue[statIndex] --;
        playerStats[statIndex].text = "" + statsValue[statIndex];
    }
    
    private void DictionaryInitialization()
    {
        players.Add(0, statsPlayer1);
        players.Add(1, statsPlayer2);
        players.Add(2, statsPlayer3);
        players.Add(3, statsPlayer4);

        playersStats.Add(0, statsValueP1);
        playersStats.Add(1, statsValueP2);
        playersStats.Add(2, statsValueP3);
        playersStats.Add(3, statsValueP4);
    }
}
