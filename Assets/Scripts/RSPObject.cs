using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class RSPObject : NetworkBehaviour
{
    public enum Shape { CUBE, SPHERE, TETRAHEDRON };
    public Shape shape;
    public int playerNumber;
    public GameObject target, follower;

    private Battle battle;

    private Rigidbody rb;
    private Vector3 dir;

    private void Start()
    {
        battle = GameObject.FindObjectOfType<Battle>();
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        //ChasingTarget();
    }

    public override void FixedUpdateNetwork()
    {
        ChasingTarget();
    }

    public void ChasingTarget()
    {
        if (target != null)
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 10f);
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Runner.DeltaTime * 10f);
            //GetComponent<Rigidbody>().velocity = Vector3.forward * 5f;

            dir = target.transform.position - transform.position;
            rb.MovePosition(transform.position + dir.normalized * 10f * Runner.DeltaTime);
        } else
        {
            battle.SelecTarget(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(this.tag) == false && (other.CompareTag("Player 1") || other.CompareTag("Player 2")))
        {
            // 애니메이션 추가

            Shape otherShape = other.GetComponent<RSPObject>().shape;

            if (shape == Shape.CUBE)
            {
                if (otherShape == Shape.CUBE)
                {
                    // 무승부
                }
                else if (otherShape == Shape.SPHERE)
                {
                    // 본인 승리
                    battle.player2.Remove(other.gameObject);
                    Runner.Despawn(other.GetComponent<NetworkObject>());
                    Destroy(other.gameObject);
                }
                else if (otherShape == Shape.TETRAHEDRON)
                {
                    // 본인 패배
                    battle.player1.Remove(gameObject);
                    Runner.Despawn(Object);
                    Destroy(gameObject);
                }
            }
            else if (shape == Shape.SPHERE)
            {
                if (otherShape == Shape.CUBE)
                {
                    // 본인 패배
                    battle.player1.Remove(gameObject);
                    Runner.Despawn(Object);
                    Destroy(gameObject);
                }
                else if (otherShape == Shape.SPHERE)
                {
                    // 무승부
                }
                else if (otherShape == Shape.TETRAHEDRON)
                {
                    // 본인 승리
                    battle.player2.Remove(other.gameObject);
                    Runner.Despawn(other.GetComponent<NetworkObject>());
                    Destroy(other.gameObject);
                }
            }
            else if (shape == Shape.TETRAHEDRON)
            {
                if (otherShape == Shape.CUBE)
                {
                    // 본인 승리
                    battle.player2.Remove(other.gameObject);
                    Runner.Despawn(other.GetComponent<NetworkObject>());
                    Destroy(other.gameObject);
                }
                else if (otherShape == Shape.SPHERE)
                {
                    // 본인 패배
                    battle.player1.Remove(gameObject);
                    Runner.Despawn(Object);
                    Destroy(gameObject);
                }
                else if (otherShape == Shape.TETRAHEDRON)
                {
                    // 무승부
                }
            }
        }
    }
}