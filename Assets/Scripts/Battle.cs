using ExitGames.Client.Photon;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class Battle : MonoBehaviour
{
    //public HashSet<GameObject> player1, player2;
    
    private void Awake()
    {
        // HashSet<RPSObject> 로 할 수는 없나?
        // 유닛 관리 V1
        //player1 = GameObject.FindGameObjectsWithTag("Player 1").ToHashSet();
        //player2 = GameObject.FindGameObjectsWithTag("Player 2").ToHashSet();

        // 유닛 관리 V2, UnitSet singleton class 사용, 리스트화
        // singlton class 의 awake에서 초기화
    }

    private void Start()
    {
        // Awake() 내용이 여기에 있었을 때는 FindGameObjectsWithTag가 끝나기 전에 SelectTarget()이 실행되면서 NULL 오류가 났었다.
        SelectTargetV2();
    }

    private void Update()
    {
        // 양 진영 유닛수 각각 출력
        //int p1 = UnitSets.instance.units[0][0].Count + UnitSets.instance.units[0][1].Count + UnitSets.instance.units[0][2].Count;
        //int p2 = UnitSets.instance.units[1][0].Count + UnitSets.instance.units[1][1].Count + UnitSets.instance.units[1][2].Count;
        //print(p1 + " " + p2);
    }

    public void SelectTargetV2()
    {
        foreach (List<HashSet<RSPObject>> player in UnitSets.instance.units)
        {
            foreach (HashSet<RSPObject> shape in player)
            {
                foreach (RSPObject obj in shape)
                {
                    MoveOne(obj);




                    //// 적 인덱스 설정(1-->1, 2-->0, (obj.playerNumber)-->(UnitSets에서 적의 index)
                    //int enemyCamp = 2 - obj.playerNumber;

                    
                    //if (obj.shape == RSPObject.Shape.CUBE)
                    //{
                    //    // 1. 조건없는 무작위 대상 공격
                    //    //int enemyShapeNum = UnitSets.instance.units[enemyCamp][1].Count;

                    //    //if (enemyShapeNum == 0)
                    //    //{
                    //    //    obj.target = null;
                    //    //    continue;
                    //    //}

                    //    //int randomTarget = Random.Range(0, enemyShapeNum);
                    //    //obj.target = UnitSets.instance.units[enemyCamp][1].ToList()[randomTarget].gameObject;


                    //    // 2. 최대한 퍼뜨려서 적 추적하는 방식
                    //    if (UnitSets.instance.outsider[enemyCamp][1].Count == 0)
                    //        for (int k = 0; k < UnitSets.instance.units[enemyCamp][1].Count; k++)
                    //            UnitSets.instance.outsider[enemyCamp][1].Add(k);

                    //    int n = Random.Range(0, UnitSets.instance.outsider[enemyCamp][1].Count);
                    //    print(UnitSets.instance.outsider[enemyCamp][1].Count);

                    //    obj.target = UnitSets.instance.units[enemyCamp][1].ToList()[UnitSets.instance.outsider[enemyCamp][1][n]].gameObject;
                    //    UnitSets.instance.outsider[enemyCamp][1].Remove(UnitSets.instance.outsider[enemyCamp][1][n]);

                    //    /* ------ 작성하면서 드는 생각 ------ */
                    //    // 처음 설정을 RSPObject와 GameObject를 혼용했는데 가능한 한 좀 더 낮은단계로 통일하는게 좋은가?

                    //    // 팔로워 설정할 이유가 있을까? 아래 2줄
                    //    //RSPObject target = UnitSets.instance.units[enemyCamp][1].ToList()[randomTarget];
                    //    //target.follower = obj.gameObject;

                    //    // (2. 최대한 퍼뜨려서 적 추적하는 방식) 작성하면서 했던 생각
                    //    // 타겟 잡고 타겟은 outsider 배열에서 지운다. 비둘기집처럼 한바퀴 돌기 전까지는 다시 선택되지 않도록
                    //    // 타겟도 원래의 units이 아닌 outsider에서 하나씩 빼와야겠네
                    //    // 죽으면 팔로워가 2명일수도 있으니까 팔로워가 없으면 아웃사이더로 다시 복귀
                    //    // 가까운녀석부터 쫓는 방식도 좋긴할듯
                    //}
                    //else if(obj.shape == RSPObject.Shape.SPHERE)
                    //{
                    //    int enemyShapeNum = UnitSets.instance.units[enemyCamp][2].Count;

                    //    if (enemyShapeNum == 0)
                    //    {
                    //        obj.target = null;
                    //        continue;
                    //    }

                    //    int randomTarget = Random.Range(0, enemyShapeNum);

                    //    //obj.target = UnitSets.instance.units[enemyCamp][2].ToList()[randomTarget].gameObject;
                    //} else if(obj.shape == RSPObject.Shape.TETRAHEDRON)
                    //{
                    //    int enemyShapeNum = UnitSets.instance.units[enemyCamp][0].Count;

                    //    if (enemyShapeNum == 0)
                    //    {
                    //        obj.target = null;
                    //        continue;
                    //    }

                    //    int randomTarget = Random.Range(0, enemyShapeNum);

                    //    //obj.target = UnitSets.instance.units[enemyCamp][0].ToList()[randomTarget].gameObject;
                    //}
                }
            }
        }
    }

    public void SelectTargetV2(GameObject o)
    {
        RSPObject obj = o.GetComponent<RSPObject>();

        MoveOne(obj);
    }

    private void MoveOne(RSPObject obj)
    {
        // 적 인덱스 설정(1-->1, 2-->0, (obj.playerNumber)-->(UnitSets에서 적의 index)
        int enemyCamp = 2 - obj.playerNumber;

        if (obj.shape == RSPObject.Shape.CUBE)
        {
            int enemyShapeNum = UnitSets.instance.units[enemyCamp][1].Count;

            if (enemyShapeNum == 0)
            {
                obj.target = null;
                return;
                //continue;
            }

            // 1. 조건없는 무작위 대상 공격
            int randomTarget = Random.Range(0, enemyShapeNum);
            obj.target = UnitSets.instance.units[enemyCamp][1].ToList()[randomTarget].gameObject;

            // 2. 최대한 퍼뜨려서 적 추적하는 방식
            if (UnitSets.instance.outsider[enemyCamp][1].Count == 0)
                for (int k = 0; k < UnitSets.instance.units[enemyCamp][1].Count; k++)
                    UnitSets.instance.outsider[enemyCamp][1].Add(k);

            //int n = Random.Range(0, UnitSets.instance.outsider[enemyCamp][1].Count);
            //int n = UnitSets.instance.outsider[enemyCamp][1].Count - 1;

            //obj.target = UnitSets.instance.units[enemyCamp][1].ToList()[UnitSets.instance.outsider[enemyCamp][1][n]].gameObject;
            //UnitSets.instance.outsider[enemyCamp][1].Remove(UnitSets.instance.outsider[enemyCamp][1][n]);
            //UnitSets.instance.outsider[enemyCamp][1].Remove(UnitSets.instance.outsider[enemyCamp][1][UnitSets.instance.outsider[enemyCamp][1].Count-1]);



            //int n = UnitSets.instance.outsider[enemyCamp][1].Count - 1;

            //obj.target = UnitSets.instance.units[enemyCamp][1].ToList()[n].gameObject;
            //UnitSets.instance.outsider[enemyCamp][1].RemoveAt(n);



            // 3. Outsider2 시도
            if (UnitSets.instance.outsider2[enemyCamp][1].Count == 0)
                foreach(RSPObject o in UnitSets.instance.units[enemyCamp][1])
                    UnitSets.instance.outsider2[enemyCamp][1].Add(o);

            int n = Random.Range(0, UnitSets.instance.outsider2[enemyCamp][1].Count);

            RSPObject target = UnitSets.instance.outsider2[enemyCamp][1].ToList()[n];
            obj.target = target.gameObject;
            UnitSets.instance.outsider2[enemyCamp][1].Remove(target);






            //UniformNaturalNumber(enemyCamp, 1);

            /* ------ 작성하면서 드는 생각 ------ */
            // 처음 설정을 RSPObject와 GameObject를 혼용했는데 가능한 한 좀 더 낮은단계로 통일하는게 좋은가?

            // 팔로워 설정할 이유가 있을까? 아래 2줄
            //RSPObject target = UnitSets.instance.units[enemyCamp][1].ToList()[randomTarget];
            //target.follower = obj.gameObject;

            // (2. 최대한 퍼뜨려서 적 추적하는 방식) 작성하면서 했던 생각
            // 타겟 잡고 타겟은 outsider 배열에서 지운다. 비둘기집처럼 한바퀴 돌기 전까지는 다시 선택되지 않도록
            // 타겟도 원래의 units이 아닌 outsider에서 하나씩 빼와야겠네
            // 죽으면 팔로워가 2명일수도 있으니까 팔로워가 없으면 아웃사이더로 다시 복귀
            // 가까운녀석부터 쫓는 방식도 좋긴할듯
        }
        else if (obj.shape == RSPObject.Shape.SPHERE)
        {
            int enemyShapeNum = UnitSets.instance.units[enemyCamp][2].Count;

            if (enemyShapeNum == 0)
            {
                obj.target = null;
                return;
                //continue;
            }

            int randomTarget = Random.Range(0, enemyShapeNum);
            obj.target = UnitSets.instance.units[enemyCamp][2].ToList()[randomTarget].gameObject;

            //if (UnitSets.instance.outsider[enemyCamp][2].Count == 0)
            //    for (int k = 0; k < UnitSets.instance.units[enemyCamp][2].Count; k++)
            //        UnitSets.instance.outsider[enemyCamp][2].Add(k);

            //int n = UnitSets.instance.outsider[enemyCamp][2].Count - 1;

            //obj.target = UnitSets.instance.units[enemyCamp][2].ToList()[UnitSets.instance.outsider[enemyCamp][2][n]].gameObject;
            //UnitSets.instance.outsider[enemyCamp][2].Remove(UnitSets.instance.outsider[enemyCamp][2][n]);

            //int n = UnitSets.instance.outsider[enemyCamp][2].Count - 1;

            //obj.target = UnitSets.instance.units[enemyCamp][2].ToList()[n].gameObject;
            //UnitSets.instance.outsider[enemyCamp][2].RemoveAt(n);


            // 3. Outsider2 시도
            if (UnitSets.instance.outsider2[enemyCamp][2].Count == 0)
                foreach (RSPObject o in UnitSets.instance.units[enemyCamp][2])
                    UnitSets.instance.outsider2[enemyCamp][2].Add(o);


            int n = Random.Range(0, UnitSets.instance.outsider2[enemyCamp][2].Count);

            RSPObject target = UnitSets.instance.outsider2[enemyCamp][2].ToList()[n];
            obj.target = target.gameObject;
            UnitSets.instance.outsider2[enemyCamp][2].Remove(target);
        }
        else if (obj.shape == RSPObject.Shape.TETRAHEDRON)
        {
            int enemyShapeNum = UnitSets.instance.units[enemyCamp][0].Count;

            if (enemyShapeNum == 0)
            {
                obj.target = null;
                return;
                //continue;
            }

            int randomTarget = Random.Range(0, enemyShapeNum);
            obj.target = UnitSets.instance.units[enemyCamp][0].ToList()[randomTarget].gameObject;

            //if (UnitSets.instance.outsider[enemyCamp][0].Count == 0)
            //    for (int k = 0; k < UnitSets.instance.units[enemyCamp][0].Count; k++)
            //        UnitSets.instance.outsider[enemyCamp][0].Add(k);

            //int n = UnitSets.instance.outsider[enemyCamp][0].Count - 1;

            //obj.target = UnitSets.instance.units[enemyCamp][0].ToList()[UnitSets.instance.outsider[enemyCamp][0][n]].gameObject;
            //UnitSets.instance.outsider[enemyCamp][0].Remove(UnitSets.instance.outsider[enemyCamp][0][n]);

            //int n = UnitSets.instance.outsider[enemyCamp][0].Count - 1;

            //obj.target = UnitSets.instance.units[enemyCamp][0].ToList()[n].gameObject;
            //UnitSets.instance.outsider[enemyCamp][0].RemoveAt(n);

            // 3. Outsider2 시도
            if (UnitSets.instance.outsider2[enemyCamp][0].Count == 0)
                foreach (RSPObject o in UnitSets.instance.units[enemyCamp][0])
                    UnitSets.instance.outsider2[enemyCamp][0].Add(o);

            int n = Random.Range(0, UnitSets.instance.outsider2[enemyCamp][0].Count);

            RSPObject target = UnitSets.instance.outsider2[enemyCamp][0].ToList()[n];
            obj.target = target.gameObject;
            UnitSets.instance.outsider2[enemyCamp][0].Remove(target);
        }
    }

    //private void MoveOne(RSPObject obj)
    //{
    //    // 적 인덱스 설정(1-->1, 2-->0, (obj.playerNumber)-->(UnitSets에서 적의 index)
    //    int enemyCamp = 2 - obj.playerNumber;

    //    if (obj.shape == RSPObject.Shape.CUBE)
    //    {
    //        int enemyShapeNum = UnitSets.instance.units[enemyCamp][1].Count;

    //        if (enemyShapeNum == 0)
    //        {
    //            obj.target = null;
    //            return;
    //            //continue;
    //        }

    //        // 1. 조건없는 무작위 대상 공격
    //        //int randomTarget = Random.Range(0, enemyShapeNum);
    //        //obj.target = UnitSets.instance.units[enemyCamp][1].ToList()[randomTarget].gameObject;


    //        // 2. 최대한 퍼뜨려서 적 추적하는 방식
    //        if (UnitSets.instance.outsider[enemyCamp][1].Count == 0)
    //            for (int k = 0; k < UnitSets.instance.units[enemyCamp][1].Count; k++)
    //                UnitSets.instance.outsider[enemyCamp][1].Add(k);

    //        //string s = null;
    //        //string t = null;
    //        //for (int k = 0; k < UnitSets.instance.outsider[enemyCamp][1].Count; k++)
    //        //{
    //        //    s += UnitSets.instance.outsider[enemyCamp][1][k] + " ";
    //        //}
    //        ////print("string : " + s);

    //        //foreach(RSPObject o in UnitSets.instance.units[enemyCamp][1])
    //        //{
    //        //    t += o.ToString() + " // ";
    //        //}
    //        //print("string : " + s + "            " + t);


    //        //int n = Random.Range(0, UnitSets.instance.outsider[enemyCamp][1].Count);
    //        int n = UnitSets.instance.outsider[enemyCamp][1].Count - 1;


    //        //print(UnitSets.instance.outsider[enemyCamp][1].Count);
    //        //print(n);

    //        //print("Cube : " + UnitSets.instance.outsider[enemyCamp][1].Count + " " + UnitSets.instance.units[enemyCamp][1].Count);
    //        //if (UnitSets.instance.units[enemyCamp][1].Count == 0)
    //        //    print("없숴요1");
    //        //print(n);
    //        //print("잡았다요놈 " + UnitSets.instance.outsider[enemyCamp][1][n] + UnitSets.instance.units[enemyCamp][1].Count);

    //        obj.target = UnitSets.instance.units[enemyCamp][1].ToList()[UnitSets.instance.outsider[enemyCamp][1][n]].gameObject;
    //        UnitSets.instance.outsider[enemyCamp][1].Remove(UnitSets.instance.outsider[enemyCamp][1][n]);
    //        //UnitSets.instance.outsider[enemyCamp][1].Remove(UnitSets.instance.outsider[enemyCamp][1][UnitSets.instance.outsider[enemyCamp][1].Count-1]);

    //        //UniformNaturalNumber(enemyCamp, 1);

    //        /* ------ 작성하면서 드는 생각 ------ */
    //        // 처음 설정을 RSPObject와 GameObject를 혼용했는데 가능한 한 좀 더 낮은단계로 통일하는게 좋은가?

    //        // 팔로워 설정할 이유가 있을까? 아래 2줄
    //        //RSPObject target = UnitSets.instance.units[enemyCamp][1].ToList()[randomTarget];
    //        //target.follower = obj.gameObject;

    //        // (2. 최대한 퍼뜨려서 적 추적하는 방식) 작성하면서 했던 생각
    //        // 타겟 잡고 타겟은 outsider 배열에서 지운다. 비둘기집처럼 한바퀴 돌기 전까지는 다시 선택되지 않도록
    //        // 타겟도 원래의 units이 아닌 outsider에서 하나씩 빼와야겠네
    //        // 죽으면 팔로워가 2명일수도 있으니까 팔로워가 없으면 아웃사이더로 다시 복귀
    //        // 가까운녀석부터 쫓는 방식도 좋긴할듯
    //    }
    //    else if (obj.shape == RSPObject.Shape.SPHERE)
    //    {
    //        int enemyShapeNum = UnitSets.instance.units[enemyCamp][2].Count;

    //        if (enemyShapeNum == 0)
    //        {
    //            obj.target = null;
    //            return;
    //            //continue;
    //        }

    //        //int randomTarget = Random.Range(0, enemyShapeNum);
    //        //obj.target = UnitSets.instance.units[enemyCamp][2].ToList()[randomTarget].gameObject;

    //        if (UnitSets.instance.outsider[enemyCamp][2].Count == 0)
    //            for (int k = 0; k < UnitSets.instance.units[enemyCamp][2].Count; k++)
    //                UnitSets.instance.outsider[enemyCamp][2].Add(k);

    //        int n = Random.Range(0, UnitSets.instance.outsider[enemyCamp][2].Count);
    //        //print(UnitSets.instance.outsider[enemyCamp][2].Count);

    //        print("sphere : " + UnitSets.instance.outsider[enemyCamp][2].Count + " " + UnitSets.instance.units[enemyCamp][2].Count);
    //        if (UnitSets.instance.units[enemyCamp][2].Count == 0)
    //            print("없숴요2");

    //        obj.target = UnitSets.instance.units[enemyCamp][2].ToList()[UnitSets.instance.outsider[enemyCamp][2][n]].gameObject;
    //        UnitSets.instance.outsider[enemyCamp][2].Remove(UnitSets.instance.outsider[enemyCamp][2][n]);
    //    }
    //    else if (obj.shape == RSPObject.Shape.TETRAHEDRON)
    //    {
    //        int enemyShapeNum = UnitSets.instance.units[enemyCamp][0].Count;

    //        if (enemyShapeNum == 0)
    //        {
    //            obj.target = null;
    //            return;
    //            //continue;
    //        }

    //        //int randomTarget = Random.Range(0, enemyShapeNum);
    //        //obj.target = UnitSets.instance.units[enemyCamp][0].ToList()[randomTarget].gameObject;

    //        if (UnitSets.instance.outsider[enemyCamp][0].Count == 0)
    //            for (int k = 0; k < UnitSets.instance.units[enemyCamp][0].Count; k++)
    //                UnitSets.instance.outsider[enemyCamp][0].Add(k);

    //        int n = Random.Range(0, UnitSets.instance.outsider[enemyCamp][0].Count);
    //        //print(UnitSets.instance.outsider[enemyCamp][0].Count);

    //        print("tetra : " + UnitSets.instance.outsider[enemyCamp][0].Count + " " + UnitSets.instance.units[enemyCamp][0].Count);
    //        if (UnitSets.instance.units[enemyCamp][0].Count == 0)
    //            print("없숴요3");

    //        obj.target = UnitSets.instance.units[enemyCamp][0].ToList()[UnitSets.instance.outsider[enemyCamp][0][n]].gameObject;
    //        UnitSets.instance.outsider[enemyCamp][0].Remove(UnitSets.instance.outsider[enemyCamp][0][n]);
    //    }
    //}

    private void UniformNaturalNumber(int s, int t)
    {
        for (int i = 0; i < UnitSets.instance.outsider[s][t].Count; i++) {
            UnitSets.instance.outsider[s][t][i] = i;
        }
    }

    //private void SelectTarget()
    //{
    //    // 타겟 정하기 전에 너무 일찍 죽으면 안될거같은데
    //    foreach (GameObject objP1 in player1)
    //    {
    //        RSPObject p1 = objP1.GetComponent<RSPObject>();

    //        foreach (GameObject objP2 in player2)
    //        {
    //            RSPObject p2 = objP2.GetComponent<RSPObject>();

    //            // p1 싸우러 가자
    //            if (p1.shape == RSPObject.Shape.CUBE && p2.shape == RSPObject.Shape.SPHERE)
    //            {
    //                if (p1.target == null)
    //                    p1.target = p2.gameObject;
    //            }
    //            else if (p1.shape == RSPObject.Shape.SPHERE && p2.shape == RSPObject.Shape.TETRAHEDRON)
    //            {
    //                if (p1.target == null)
    //                    p1.target = p2.gameObject;
    //            }
    //            else if (p1.shape == RSPObject.Shape.TETRAHEDRON && p2.shape == RSPObject.Shape.CUBE)
    //            {
    //                if (p1.target == null)
    //                    p1.target = p2.gameObject;
    //            }

    //            // p2 싸우러 가자
    //            if (p2.shape == RSPObject.Shape.CUBE && p1.shape == RSPObject.Shape.SPHERE)
    //            {
    //                if (p2.target == null)
    //                    p2.target = p1.gameObject;
    //            }
    //            else if (p2.shape == RSPObject.Shape.SPHERE && p1.shape == RSPObject.Shape.TETRAHEDRON)
    //            {
    //                if (p2.target == null)
    //                    p2.target = p1.gameObject;
    //            }
    //            else if (p2.shape == RSPObject.Shape.TETRAHEDRON && p1.shape == RSPObject.Shape.CUBE)
    //            {
    //                if (p2.target == null)
    //                    p2.target = p1.gameObject;
    //            }
    //        }
    //    }
    //}

    //public void SelecTarget(GameObject obj)
    //{
    //    RSPObject rspObj = obj.GetComponent<RSPObject>();

    //    if(rspObj.playerNumber == 1)
    //    {
    //        foreach (GameObject objP2 in player2)
    //        {
    //            RSPObject p2 = objP2.GetComponent<RSPObject>();

    //            if (rspObj.shape == RSPObject.Shape.CUBE && p2.shape == RSPObject.Shape.SPHERE)
    //            {
    //                if (rspObj.target == null)
    //                    rspObj.target = p2.gameObject;
    //            }
    //            else if (rspObj.shape == RSPObject.Shape.SPHERE && p2.shape == RSPObject.Shape.TETRAHEDRON)
    //            {
    //                if (rspObj.target == null)
    //                    rspObj.target = p2.gameObject;
    //            }
    //            else if (rspObj.shape == RSPObject.Shape.TETRAHEDRON && p2.shape == RSPObject.Shape.CUBE)
    //            {
    //                if (rspObj.target == null)
    //                    rspObj.target = p2.gameObject;
    //            }
    //        }
    //    } else if(rspObj.playerNumber == 2)
    //    {
    //        foreach (GameObject objP1 in player1)
    //        {
    //            RSPObject p1 = objP1.GetComponent<RSPObject>();

    //            if (rspObj.shape == RSPObject.Shape.CUBE && p1.shape == RSPObject.Shape.SPHERE)
    //            {
    //                if (rspObj.target == null)
    //                    rspObj.target = p1.gameObject;
    //            }
    //            else if (rspObj.shape == RSPObject.Shape.SPHERE && p1.shape == RSPObject.Shape.TETRAHEDRON)
    //            {
    //                if (rspObj.target == null)
    //                    rspObj.target = p1.gameObject;
    //            }
    //            else if (rspObj.shape == RSPObject.Shape.TETRAHEDRON && p1.shape == RSPObject.Shape.CUBE)
    //            {
    //                if (rspObj.target == null)
    //                    rspObj.target = p1.gameObject;
    //            }
    //        }
    //    }

    //    //if (rspObj.target == null)
    //    //    print("Cannot find appropriate target :(");
    //}
}
