using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public HashSet<GameObject> player1, player2;

    private void Start()
    {
        // HashSet<RPSObject> �� �� ���� ����?
        player1 = GameObject.FindGameObjectsWithTag("Player 1").ToHashSet();
        player2 = GameObject.FindGameObjectsWithTag("Player 2").ToHashSet();

        SelectTarget();
    }

    private void Update()
    {
        //print(player1.Count + " " + player2.Count);
    }

    private void SelectTarget()
    {
        // Ÿ�� ���ϱ� ���� �ʹ� ���� ������ �ȵɰŰ�����
        foreach (GameObject objP1 in player1)
        {
            RSPObject p1 = objP1.GetComponent<RSPObject>();

            foreach (GameObject objP2 in player2)
            {
                RSPObject p2 = objP2.GetComponent<RSPObject>();

                // p1 �ο췯 ����
                if (p1.shape == RSPObject.Shape.CUBE && p2.shape == RSPObject.Shape.SPHERE)
                {
                    if (p1.target == null)
                        p1.target = p2.gameObject;
                }
                else if (p1.shape == RSPObject.Shape.SPHERE && p2.shape == RSPObject.Shape.TETRAHEDRON)
                {
                    if (p1.target == null)
                        p1.target = p2.gameObject;
                }
                else if (p1.shape == RSPObject.Shape.TETRAHEDRON && p2.shape == RSPObject.Shape.CUBE)
                {
                    if (p1.target == null)
                        p1.target = p2.gameObject;
                }

                // p2 �ο췯 ����
                if (p2.shape == RSPObject.Shape.CUBE && p1.shape == RSPObject.Shape.SPHERE)
                {
                    if (p2.target == null)
                        p2.target = p1.gameObject;
                }
                else if (p2.shape == RSPObject.Shape.SPHERE && p1.shape == RSPObject.Shape.TETRAHEDRON)
                {
                    if (p2.target == null)
                        p2.target = p1.gameObject;
                }
                else if (p2.shape == RSPObject.Shape.TETRAHEDRON && p1.shape == RSPObject.Shape.CUBE)
                {
                    if (p2.target == null)
                        p2.target = p1.gameObject;
                }
            }
        }
    }
}
