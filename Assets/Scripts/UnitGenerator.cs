using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class UnitGenerator : NetworkBehaviour
{
    public List<GameObject[]> units;
    public GameObject[] player1, player2;
    public GameObject[] spawnPoint;
    public GameObject[] playerUnitSets;

    //private Battle battle;

    public bool gameStarted;

    private void Awake()
    {
        units = new List<GameObject[]> { player1, player2 };
        //battle = GameObject.FindObjectOfType<Battle>();

        gameStarted = false;
    }

    //public override void FixedUpdateNetwork()
    //{
    //    print("·¯³ÊÇü");
    //    print(Runner.State);
    //}

    public void StartGenerateUnits()
    {
        //if (Runner.IsServer)
        {
            StartCoroutine(GeneratePlayer1());
        }
        //else if (Runner.IsClient)
        {
            StartCoroutine(GeneratePlayer2());
        }

        //battle.enabled = true;
        gameStarted = true;
    }

    public IEnumerator GeneratePlayer1()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            int r = Random.Range(0, 3);
            int rPos = Random.Range(-20, 20);

            //NetworkObject obj = Runner.Spawn(units[0][r], spawnPoint[0].transform.position + Vector3.forward * rPos, units[0][r].transform.rotation);
            NetworkObject obj = Runner.Spawn(units[0][r], spawnPoint[0].transform.position + Vector3.forward * rPos, units[0][r].transform.rotation, Players.instance.players[0]);

            RSPObject rspObj = obj.gameObject.GetComponent<RSPObject>();
            UnitSets.instance.units[0][r].Add(rspObj);
            UnitSets.instance.outsider2[0][r].Add(rspObj);

            //UnitSets.instance.units[0][r].Add(obj.gameObject.GetComponent<RSPObject>());
            //battle.player1.Add(obj.gameObject);
            //battle.SelecTarget(obj.gameObject);
            obj.transform.parent = playerUnitSets[0].transform;
        }
    }

    public IEnumerator GeneratePlayer2()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            int r = Random.Range(0, 3);
            int rPos = Random.Range(-20, 20);

            //NetworkObject obj = Runner.Spawn(units[1][r], spawnPoint[1].transform.position + Vector3.forward * rPos, units[1][r].transform.rotation);
            NetworkObject obj = Runner.Spawn(units[1][r], spawnPoint[1].transform.position + Vector3.forward * rPos, units[1][r].transform.rotation, Players.instance.players[1]);

            RSPObject rspObj = obj.gameObject.GetComponent<RSPObject>();
            UnitSets.instance.units[1][r].Add(rspObj);
            UnitSets.instance.outsider2[1][r].Add(rspObj);

            //UnitSets.instance.units[1][r].Add(obj.gameObject.GetComponent<RSPObject>());
            //battle.player2.Add(obj.gameObject);
            //battle.SelecTarget(obj.gameObject);
            obj.transform.parent = playerUnitSets[1].transform;
        }
    }
}