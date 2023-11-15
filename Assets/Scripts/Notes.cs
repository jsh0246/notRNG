using Fusion;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static RSPObject;

public class Notes : MonoBehaviour
{
    public static Notes instance = null;

    private void Start()
    {

    }

    private void Update()
    {

    }

    //private void MoveOne(RSPObject obj)
    //{
    //    // 상대 플레이어 인덱스 설정(1-->1, 2-->0, (obj.playerNumber)-->(UnitSets에서 적의 index)
    //    int enemyCamp = 2 - obj.playerNumber;

    //    // 내가 Cube(보자기)일때는
    //    if (obj.shape == RSPObject.Shape.CUBE)
    //    {

    //        // 내가 Cube(보자기)일때는 상대편의 1번 인덱스의 도형(구, 주먹에 대응)을 추적한다.
    //        int enemyShapeNum = UnitSets.instance.units[enemyCamp][1].Count;

    //        // 이길 수 있는 적이 없으면 추적 종료
    //        if (enemyShapeNum == 0)
    //        {
    //            obj.target = null;
    //            return;
    //        }

    //        // 1. 조건없는 무작위 대상 공격
    //        int randomTarget = Random.Range(0, enemyShapeNum);
    //        obj.target = UnitSets.instance.units[enemyCamp][1].ToList()[randomTarget].gameObject;

    //        // 2. 최대한 퍼뜨려서 적 추적하는 방식
    //        if (UnitSets.instance.outsider2[enemyCamp][1].Count == 0)
    //            foreach (RSPObject o in UnitSets.instance.units[enemyCamp][1])
    //                UnitSets.instance.outsider2[enemyCamp][1].Add(o);

    //        int n = Random.Range(0, UnitSets.instance.outsider2[enemyCamp][1].Count);

    //        RSPObject target = UnitSets.instance.outsider2[enemyCamp][1].ToList()[n];
    //        obj.target = target.gameObject;
    //        UnitSets.instance.outsider2[enemyCamp][1].Remove(target);
    //    }

    //    // 나머지 두가지 도형도 같은 방식으로 적용
    //    // ...
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(gameObject.tag) == false && (other.CompareTag("Player 1") || other.CompareTag("Player 2")))
    //    {
    //        Shape otherShape = other.GetComponent<RSPObject>().shape;
    //        RSPObject otherRSPObject = other.GetComponent<RSPObject>();
    //        int enemyCamp = otherRSPObject.playerNumber - 1;

    //        if (shape == Shape.CUBE)
    //        {
    //            if (otherShape == Shape.CUBE)
    //            {
    //                // 무승부
    //            }
    //            else if (otherShape == Shape.SPHERE)
    //            {
    //                // 본인 승리
    //                //if (playerNumber == 1) battle.player2.Remove(other.gameObject);
    //                //else if (playerNumber == 2) battle.player1.Remove(other.gameObject);

    //                UnitSets.instance.units[enemyCamp][1].Remove(otherRSPObject);
    //                UnitSets.instance.outsider2[enemyCamp][1].Remove(otherRSPObject);

    //                SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

    //                if (otherRSPObject.SelectionSprite.gameObject.activeSelf)
    //                    SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

    //                //Destroy(other.gameObject);
    //                Runner.Despawn(other.GetComponent<NetworkObject>());

    //                // 제동거리 추가
    //                StartCoroutine(BrakingDIstance());

    //                VoronoiColoring.instance.mp.ColoringVoronoiCell(other.ClosestPoint(transform.position), playerNumber);
    //            }
    //            //else if (otherShape == Shape.TETRAHEDRON)
    //            //{
    //            //    // 본인 패배
    //            //}
    //        }
    //    }
    //}
}

// 스폰 포인트 돌아다니기?
// 생성되는 유닛 랜덤
// 유닛 생성 속도나 주기나 생성 버튼?
// 스폰포인트 시각효과
// 충돌 애니메이션이나 충돌 효과 보이게

// 보로노이 버텍스 지점 랜덤 생성후 보로노이 생성
// 보로노이 버텍스 점령은 어떻게?

// 속도랜덤?
// 