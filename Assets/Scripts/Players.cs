using Fusion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Players : NetworkBehaviour
{
    public static Players instance = null;
    
    public List<PlayerRef> players;

    public TextMeshProUGUI message;

    private  void Awake()
    {
        if (instance == null)
        {
            instance = this;

            message = GameObject.FindFirstObjectByType<TextMeshProUGUI>();

            players = new List<PlayerRef>();

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(this.gameObject);
        }
    }

    public void AddPlayers(PlayerRef player)
    {
        players.Add(player);
        
        print(players.Count);
        message.text = players.Count.ToString();
    }

    public static Players Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }

}
