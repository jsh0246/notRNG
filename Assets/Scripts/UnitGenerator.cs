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

    private Battle battle;

    private void Start()
    {
        print("UNITGENERATOR");

        units = new List<GameObject[]> { player1, player2 };
        battle = GameObject.FindObjectOfType<Battle>();

        StartCoroutine("GeneratePlayer1");
        StartCoroutine("GeneratePlayer2");
    }

    public IEnumerator GeneratePlayer1()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            int r = Random.Range(0, 3);
            int rPos = Random.Range(-20, 20);
            //GameObject obj = Instantiate(units[0][r], spawnPoint[0].transform.position + Vector3.forward * rPos, Quaternion.identity);
            NetworkObject obj = Runner.Spawn(units[0][r], spawnPoint[0].transform.position + Vector3.forward * rPos, units[0][r].transform.rotation);

            //if (obj.GetComponent<RSPObject>().shape == RSPObject.Shape.TETRAHEDRON)
            //{
            //    obj.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -90f));
            //}

            // error
            battle.player1.Add(obj.gameObject);
            //battle.SelecTarget(obj.gameObject);
            obj.transform.parent = playerUnitSets[0].transform;
        }
    }

    public IEnumerator GeneratePlayer2()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            int r = Random.Range(0, 3);
            int rPos = Random.Range(-20, 20);
            //GameObject obj = (Instantiate(units[1][r], spawnPoint[1].transform.position + Vector3.forward * rPos, Quaternion.identity));
            NetworkObject obj = Runner.Spawn(units[1][r], spawnPoint[1].transform.position + Vector3.forward * rPos, units[1][r].transform.rotation);

            //if (obj.GetComponent<RSPObject>().shape == RSPObject.Shape.TETRAHEDRON)
            //{
            //    obj.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
            //}

            // error
            battle.player2.Add(obj.gameObject);
            //battle.SelecTarget(obj.gameObject);
            obj.transform.parent = playerUnitSets[1].transform;
        }
    }
}