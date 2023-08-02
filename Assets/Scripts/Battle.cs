using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Battle : MonoBehaviour
{
    public HashSet<GameObject> player1, player2;

    private void Awake()
    {
        print("BATTLE");

        // HashSet<RPSObject> 로 할 수는 없나?
        player1 = GameObject.FindGameObjectsWithTag("Player 1").ToHashSet();
        player2 = GameObject.FindGameObjectsWithTag("Player 2").ToHashSet();
    }

    private void Start()
    {
        print("BATTLE 2");

        // Awake() 내용이 여기에 있었을 때는 FindGameObjectsWithTag가 끝나기 전에 SelectTarget()이 실행되면서 NULL 오류가 났었다.
        SelectTarget();
    }

    private void Update()
    {
        print("BATTLE 3");

        print(player1.Count + " " + player2.Count);
    }

    private void SelectTarget()
    {
        // 타겟 정하기 전에 너무 일찍 죽으면 안될거같은데
        foreach (GameObject objP1 in player1)
        {
            RSPObject p1 = objP1.GetComponent<RSPObject>();

            foreach (GameObject objP2 in player2)
            {
                RSPObject p2 = objP2.GetComponent<RSPObject>();

                // p1 싸우러 가자
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

                // p2 싸우러 가자
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

    public void SelecTarget(GameObject obj)
    {
        RSPObject rspObj = obj.GetComponent<RSPObject>();

        if(rspObj.playerNumber == 1)
        {
            foreach (GameObject objP2 in player2)
            {
                RSPObject p2 = objP2.GetComponent<RSPObject>();

                if (rspObj.shape == RSPObject.Shape.CUBE && p2.shape == RSPObject.Shape.SPHERE)
                {
                    if (rspObj.target == null)
                        rspObj.target = p2.gameObject;
                }
                else if (rspObj.shape == RSPObject.Shape.SPHERE && p2.shape == RSPObject.Shape.TETRAHEDRON)
                {
                    if (rspObj.target == null)
                        rspObj.target = p2.gameObject;
                }
                else if (rspObj.shape == RSPObject.Shape.TETRAHEDRON && p2.shape == RSPObject.Shape.CUBE)
                {
                    if (rspObj.target == null)
                        rspObj.target = p2.gameObject;
                }
            }
        } else if(rspObj.playerNumber == 2)
        {
            foreach (GameObject objP1 in player1)
            {
                RSPObject p1 = objP1.GetComponent<RSPObject>();

                if (rspObj.shape == RSPObject.Shape.CUBE && p1.shape == RSPObject.Shape.SPHERE)
                {
                    if (rspObj.target == null)
                        rspObj.target = p1.gameObject;
                }
                else if (rspObj.shape == RSPObject.Shape.SPHERE && p1.shape == RSPObject.Shape.TETRAHEDRON)
                {
                    if (rspObj.target == null)
                        rspObj.target = p1.gameObject;
                }
                else if (rspObj.shape == RSPObject.Shape.TETRAHEDRON && p1.shape == RSPObject.Shape.CUBE)
                {
                    if (rspObj.target == null)
                        rspObj.target = p1.gameObject;
                }
            }
        }
    }
}
