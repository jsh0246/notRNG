using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGenerator : MonoBehaviour
{
    public List<GameObject[]> units;
    public GameObject[] player1, player2;
    public GameObject[] spawnPoint;
    public GameObject[] playerUnitSets;

    private Battle battle;

    private void Start()
    {
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
            GameObject obj = (Instantiate(units[0][r], spawnPoint[0].transform.position, Quaternion.identity));
            battle.player1.Add(obj);
            obj.transform.parent = playerUnitSets[0].transform;
        }
    }

    public IEnumerator GeneratePlayer2()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            int r = Random.Range(0, 3);
            GameObject obj = (Instantiate(units[1][r], spawnPoint[1].transform.position, Quaternion.identity));
            battle.player2.Add(obj);
            obj.transform.parent = playerUnitSets[1].transform;
        }
    }






}