using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Drawing;

public class RSPObject : NetworkBehaviour
{
    public enum Shape { CUBE, SPHERE, TETRAHEDRON };
    public Shape shape;
    public int playerNumber;
    public GameObject target, follower;

    [SerializeField] private SpriteRenderer SelectionSprite;

    private Battle battle;
    private UnitGenerator unitGenerator;

    private Rigidbody rb;
    private Vector3 dir;

    private bool move;
    private Vector3 point;

    private void Start()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);

        battle = GameObject.FindObjectOfType<Battle>();
        unitGenerator = GameObject.FindObjectOfType<UnitGenerator>();
        rb = GetComponent<Rigidbody>();

        move = false;
    }

    private void Update()
    {

    }

    public override void FixedUpdateNetwork()
    {
        //base.FixedUpdateNetwork();

        if(unitGenerator.gameStarted)
            ChasingTarget();

        // ���콺 Ŭ�� Move vs ChasingTarget() �Ӱ� ���� ����?? ��� ����?
        // target������ ���ٰ� ������ �־ �ٸ����� ���ٰ� Ÿ���� �������� � ����� ���� �ٸ����� ������ �������ұ� �ٽ� ���ο� target�� ����������?
        if (move)
        {
            dir = point - transform.position;
            rb.MovePosition(transform.position + dir.normalized * 10f * Runner.DeltaTime);
            //rb.MovePosition(transform.position + dir.normalized * 10f * Time.deltaTime);
            rb.rotation = Quaternion.LookRotation(dir);

            if (Vector3.Distance(point, transform.position) < 0.1f)
                move = false;
        }
    }

    public void ChasingTarget()
    {
        if (target != null)
        {
            dir = target.transform.position - transform.position;

            rb.MovePosition(transform.position + dir.normalized * Runner.DeltaTime * 5f);
            rb.rotation = Quaternion.LookRotation(dir);
        }
        //else if ((target == null || target.activeSelf == false) && Object.isActiveAndEnabled == true && gameObject.activeSelf == true)
        else if(target == null || target.activeSelf == false)
        {
            // ���� Ŭ�� ������ ���� ������ ����
            battle.SelecTarget(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //RSPObject thisRSPOBject = GetComponent<RSPObject>();

        if(other.CompareTag(gameObject.tag) == false && (other.CompareTag("Player 1") || other.CompareTag("Player 2")))
        {
            // �ִϸ��̼� �߰�

            Shape otherShape = other.GetComponent<RSPObject>().shape;
            RSPObject otherRSPObject = other.GetComponent<RSPObject>();

            if (shape == Shape.CUBE)
            {
                if (otherShape == Shape.CUBE)
                {
                    // ���º�
                }
                else if (otherShape == Shape.SPHERE)
                {
                    // ���� �¸�
                    if(playerNumber == 1) battle.player2.Remove(other.gameObject);
                    else if (playerNumber == 2) battle.player1.Remove(other.gameObject);

                    
                    SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

                    if(otherRSPObject.SelectionSprite.gameObject.activeSelf)
                        SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

                    //Destroy(other.gameObject);
                    Runner.Despawn(other.GetComponent<NetworkObject>());
                }
                //else if (otherShape == Shape.TETRAHEDRON)
                //{
                //    // ���� �й�
                //    if (playerNumber == 1) battle.player2.Remove(gameObject);
                //    else if (playerNumber == 2) battle.player1.Remove(gameObject);

                //    battle.player1.Remove(gameObject);

                //    SelectionManager.Instance.AvailableUnits.Remove(this);

                //    if (SelectionSprite.gameObject.activeSelf)
                //        SelectionManager.Instance.SelectedUnits.Remove(this);

                //    //Destroy(gameObject);
                //    Runner.Despawn(Object);
                //}
            }
            else if (shape == Shape.SPHERE)
            {
                //if (otherShape == Shape.CUBE)
                //{
                //    // ���� �й�
                //    //battle.player1.Remove(gameObject);

                //    if (playerNumber == 1) battle.player2.Remove(gameObject);
                //    else if (playerNumber == 2) battle.player1.Remove(gameObject);


                //    SelectionManager.Instance.AvailableUnits.Remove(this);

                //    if (SelectionSprite.gameObject.activeSelf)
                //        SelectionManager.Instance.SelectedUnits.Remove(this);

                //    Destroy(gameObject);
                //    Runner.Despawn(Object);
                //}
                //else 
                if (otherShape == Shape.SPHERE)
                {
                    // ���º�
                }
                else if (otherShape == Shape.TETRAHEDRON)
                {
                    // ���� �¸�
                    //battle.player2.Remove(other.gameObject);

                    if (playerNumber == 1) battle.player2.Remove(other.gameObject);
                    else if (playerNumber == 2) battle.player1.Remove(other.gameObject);

                    
                    SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

                    if (otherRSPObject.SelectionSprite.gameObject.activeSelf)
                        SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

                    //Destroy(other.gameObject);
                    Runner.Despawn(other.GetComponent<NetworkObject>());
                }
            }
            else if (shape == Shape.TETRAHEDRON)
            {
                if (otherShape == Shape.CUBE)
                {
                    // ���� �¸�
                    //battle.player2.Remove(other.gameObject);

                    if (playerNumber == 1) battle.player2.Remove(other.gameObject);
                    else if (playerNumber == 2) battle.player1.Remove(other.gameObject);

                    
                    SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

                    if (otherRSPObject.SelectionSprite.gameObject.activeSelf)
                        SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

                    //Destroy(other.gameObject);
                    Runner.Despawn(other.GetComponent<NetworkObject>());
                }
                //else if (otherShape == Shape.SPHERE)
                //{
                //    // ���� �й�
                //    //battle.player1.Remove(gameObject);

                //    if (playerNumber == 1) battle.player2.Remove(gameObject);
                //    else if (playerNumber == 2) battle.player1.Remove(gameObject);


                //    SelectionManager.Instance.AvailableUnits.Remove(this);

                //    if (SelectionSprite.gameObject.activeSelf)
                //        SelectionManager.Instance.SelectedUnits.Remove(this);

                //    Destroy(gameObject);
                //    Runner.Despawn(Object);
                //}
                else if (otherShape == Shape.TETRAHEDRON)
                {
                    // ���º�
                }
            }
        }
    }

    public void MoveTo(Vector3 point)
    {
        //dir = new Vector3(point.x, 0f, point.z) - transform.position;
        ////rb.MovePosition(transform.position + dir.normalized * 10f * Runner.DeltaTime);
        //rb.MovePosition(transform.position + dir * 10f * Time.deltaTime);
        //rb.rotation = Quaternion.LookRotation(dir);

        move = true;
        this.point = new Vector3(point.x, 0f, point.z);
    }

    public void Onselected()
    {
        SelectionSprite.gameObject.SetActive(true);
    }

    public void OnDeselected()
    {
        SelectionSprite.gameObject.SetActive(false);
    }
}