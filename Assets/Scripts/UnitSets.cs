using Fusion;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

// UnitSets �� GamaManager�� ���� �δϱ� UnitGenerator Ŭ������ ��Ʈ��ũ������ ������ �ȴ� ��ü ��?
// �̱����� ���� �θ� �ȵǳ�?
public class UnitSets : MonoBehaviour
{
    public static UnitSets instance = null;

    public List<List<HashSet<RSPObject>>> units;
    public List<List<List<int>>> outsider;
    public List<List<HashSet<RSPObject>>> outsider2;
    public int playerNumber {  get; private set; }
    public int shapeNumber { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

            playerNumber = 2;
            shapeNumber = 3;

            units = new List<List<HashSet<RSPObject>>>();
            for (int i = 0; i < playerNumber; i++) {
                // 2���� �÷��̾� UnitSets
                units.Add(new List<HashSet<RSPObject>>());

                for(int j= 0; j < shapeNumber; j++)
                {
                    // ������ �÷��̾�� 3���� ���� ����
                    units[i].Add(new HashSet<RSPObject>());
                }
            }

            List<GameObject> list = GameObject.FindGameObjectsWithTag("Player 1").ToList();

            foreach (GameObject obj in list)
            {
                RSPObject rsp = obj.GetComponent<RSPObject>();

                if (rsp.shape == RSPObject.Shape.CUBE)
                    units[0][0].Add(rsp);
                else if (rsp.shape == RSPObject.Shape.SPHERE)
                    units[0][1].Add(rsp);
                else if (rsp.shape == RSPObject.Shape.TETRAHEDRON)
                    units[0][2].Add(rsp);
            }

            list = GameObject.FindGameObjectsWithTag("Player 2").ToList();

            foreach (GameObject obj in list)
            {
                RSPObject rsp = obj.GetComponent<RSPObject>();

                if (rsp.shape == RSPObject.Shape.CUBE)
                    units[1][0].Add(rsp);
                else if (rsp.shape == RSPObject.Shape.SPHERE)
                    units[1][1].Add(rsp);
                else if (rsp.shape == RSPObject.Shape.TETRAHEDRON)
                    units[1][2].Add(rsp);
            }

            //Initialize outsider list
            outsider = new List<List<List<int>>>();

            for (int i = 0; i < 2; i++)
            {
                outsider.Add(new List<List<int>>());

                for (int j = 0; j < 3; j++)
                {
                    outsider[i].Add(new List<int>());

                    for (int k = 0; k < UnitSets.instance.units[i][j].Count; k++)
                    {
                        outsider[i][j].Add(k);
                    }
                }
            }

            outsider2 = new List<List<HashSet<RSPObject>>>();

            for (int i = 0; i < 2; i++)
            {
                outsider2.Add(new List<HashSet<RSPObject>>());

                for (int j = 0; j < 3; j++)
                {
                    outsider2[i].Add(new HashSet<RSPObject>());

                    foreach(RSPObject o in UnitSets.instance.units[i][j])
                    {
                        outsider2[i][j].Add(o);
                    }
                }
            }

            DontDestroyOnLoad(this.gameObject);
        } else
        {
            if(instance != this)
                Destroy(this.gameObject);
        }
    }

    public void SetAuthority(int playerCount)
    {
        for(int i=0; i<shapeNumber; i++)
        {
            foreach(RSPObject o in units[playerCount][i])
            {
                o.GetComponent<NetworkObject>().AssignInputAuthority(Players.instance.players[playerCount]);
            }
        }
    }

    public static UnitSets Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }
}
