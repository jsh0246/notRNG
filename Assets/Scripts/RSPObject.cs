using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSPObject : MonoBehaviour
{
    public enum Shape { CUBE, SPHERE, TETRAHEDRON };
    public Shape shape;
    public int playerNumber;
    public GameObject target, follower;

    private Battle battle;

    private void Start()
    {
        battle = GameObject.FindObjectOfType<Battle>();
    }

    private void Update()
    {
        ChasingTarget();
    }

    public void ChasingTarget()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 10f);
        }
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(this.tag) == false && (other.CompareTag("Player 1") || other.CompareTag("Player 2")))
        {
            // �ִϸ��̼� �߰�

            Shape otherShape = other.GetComponent<RSPObject>().shape;

            if (shape == Shape.CUBE)
            {
                if (otherShape == Shape.CUBE)
                {
                    // ���º�
                }
                else if (otherShape == Shape.SPHERE)
                {
                    // ���� �¸�
                    battle.player2.Remove(other.gameObject);
                    Destroy(other.gameObject);
                }
                else if (otherShape == Shape.TETRAHEDRON)
                {
                    // ���� �й�
                    battle.player1.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
            else if (shape == Shape.SPHERE)
            {
                if (otherShape == Shape.CUBE)
                {
                    // ���� �й�
                    battle.player1.Remove(gameObject);
                    Destroy(gameObject);
                }
                else if (otherShape == Shape.SPHERE)
                {
                    // ���º�
                }
                else if (otherShape == Shape.TETRAHEDRON)
                {
                    // ���� �¸�
                    battle.player2.Remove(other.gameObject);
                    Destroy(other.gameObject);
                }
            }
            else if (shape == Shape.TETRAHEDRON)
            {
                if (otherShape == Shape.CUBE)
                {
                    // ���� �¸�
                    battle.player2.Remove(other.gameObject);
                    Destroy(other.gameObject);
                }
                else if (otherShape == Shape.SPHERE)
                {
                    // ���� �й�
                    battle.player1.Remove(gameObject);
                    Destroy(gameObject);
                }
                else if (otherShape == Shape.TETRAHEDRON)
                {
                    // ���º�
                }
            }
        }
    }
}