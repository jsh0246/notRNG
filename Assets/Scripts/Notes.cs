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
    //    // ��� �÷��̾� �ε��� ����(1-->1, 2-->0, (obj.playerNumber)-->(UnitSets���� ���� index)
    //    int enemyCamp = 2 - obj.playerNumber;

    //    // ���� Cube(���ڱ�)�϶���
    //    if (obj.shape == RSPObject.Shape.CUBE)
    //    {

    //        // ���� Cube(���ڱ�)�϶��� ������� 1�� �ε����� ����(��, �ָԿ� ����)�� �����Ѵ�.
    //        int enemyShapeNum = UnitSets.instance.units[enemyCamp][1].Count;

    //        // �̱� �� �ִ� ���� ������ ���� ����
    //        if (enemyShapeNum == 0)
    //        {
    //            obj.target = null;
    //            return;
    //        }

    //        // 1. ���Ǿ��� ������ ��� ����
    //        int randomTarget = Random.Range(0, enemyShapeNum);
    //        obj.target = UnitSets.instance.units[enemyCamp][1].ToList()[randomTarget].gameObject;

    //        // 2. �ִ��� �۶߷��� �� �����ϴ� ���
    //        if (UnitSets.instance.outsider2[enemyCamp][1].Count == 0)
    //            foreach (RSPObject o in UnitSets.instance.units[enemyCamp][1])
    //                UnitSets.instance.outsider2[enemyCamp][1].Add(o);

    //        int n = Random.Range(0, UnitSets.instance.outsider2[enemyCamp][1].Count);

    //        RSPObject target = UnitSets.instance.outsider2[enemyCamp][1].ToList()[n];
    //        obj.target = target.gameObject;
    //        UnitSets.instance.outsider2[enemyCamp][1].Remove(target);
    //    }

    //    // ������ �ΰ��� ������ ���� ������� ����
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
    //                // ���º�
    //            }
    //            else if (otherShape == Shape.SPHERE)
    //            {
    //                // ���� �¸�
    //                //if (playerNumber == 1) battle.player2.Remove(other.gameObject);
    //                //else if (playerNumber == 2) battle.player1.Remove(other.gameObject);

    //                UnitSets.instance.units[enemyCamp][1].Remove(otherRSPObject);
    //                UnitSets.instance.outsider2[enemyCamp][1].Remove(otherRSPObject);

    //                SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

    //                if (otherRSPObject.SelectionSprite.gameObject.activeSelf)
    //                    SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

    //                //Destroy(other.gameObject);
    //                Runner.Despawn(other.GetComponent<NetworkObject>());

    //                // �����Ÿ� �߰�
    //                StartCoroutine(BrakingDIstance());

    //                VoronoiColoring.instance.mp.ColoringVoronoiCell(other.ClosestPoint(transform.position), playerNumber);
    //            }
    //            //else if (otherShape == Shape.TETRAHEDRON)
    //            //{
    //            //    // ���� �й�
    //            //}
    //        }
    //    }
    //}
}

// ���� ����Ʈ ���ƴٴϱ�?
// �����Ǵ� ���� ����
// ���� ���� �ӵ��� �ֱ⳪ ���� ��ư?
// ��������Ʈ �ð�ȿ��
// �浹 �ִϸ��̼��̳� �浹 ȿ�� ���̰�

// ���γ��� ���ؽ� ���� ���� ������ ���γ��� ����
// ���γ��� ���ؽ� ������ ���?

// �ӵ�����?
// 