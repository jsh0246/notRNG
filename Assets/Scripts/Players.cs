using Fusion;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public static Players instance = null;

    public List<PlayerRef> players;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

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
