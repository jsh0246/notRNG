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
        base.FixedUpdateNetwork();

        if(unitGenerator.gameStarted)
            ChasingTarget();

        if (move)
        {
            dir = point - transform.position;
            rb.MovePosition(transform.position + dir.normalized * 10f * Runner.DeltaTime);
            //rb.MovePosition(transform.position + dir.normalized * 10f * Time.deltaTime);
            rb.rotation = Quaternion.LookRotation(dir);

            if (Vector3.Distance(point, transform.position) < 0.01f)
                move = false;
        }
    }

    public void ChasingTarget()
    {
        if (target != null)
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Time.deltaTime * 10f);
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, Runner.DeltaTime * 15f);
            //GetComponent<Rigidbody>().velocity = Vector3.forward * 5f;

            dir = target.transform.position - transform.position;
            //dirNor = dir.normalized;


            rb.MovePosition(transform.position + dir.normalized * Runner.DeltaTime * 20f);
            rb.rotation = Quaternion.LookRotation(dir);



            //transform.rotation = Quaternion.Euler(transform.rotation * new Vector3(0f, dir.y, 0f));

            //rb.rotation = Quaternion.LookRotation(new Vector3(0, dir.y, 0f));
            //rb.rotation = Quaternion.Lerp(rb.rotation, Quaternion.Euler(dir), 1f);

            //Quaternion.Lerp(rb.rotation, Quaternion.Euler(dir), lerp + Runner.DeltaTime);

            //rb.MoveRotation(Quaternion.Euler(dir));



            //rb.rotation *= Quaternion.Euler(dir);
            //transform.rotation = Quaternion.Euler(dir);

            //transform.position += dir.normalized * Runner.DeltaTime * 10f;
            //transform.rotation = Quaternion.Euler(dir);
        }
        //else if ((target == null || target.activeSelf == false) && Object.isActiveAndEnabled == true && gameObject.activeSelf == true)
        else if(target == null || target.activeSelf == false)
        {
            // 서버 클라 실행할 때는 여전히 오류
            battle.SelecTarget(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //RSPObject thisRSPOBject = GetComponent<RSPObject>();

        if(other.CompareTag(this.tag) == false && (other.CompareTag("Player 1") || other.CompareTag("Player 2")))
        {
            // 애니메이션 추가

            Shape otherShape = other.GetComponent<RSPObject>().shape;
            RSPObject otherRSPObject = other.GetComponent<RSPObject>();

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
                    //Runner.Despawn(other.GetComponent<NetworkObject>());
                    SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

                    if(otherRSPObject.SelectionSprite.gameObject.activeSelf)
                        SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

                    Destroy(other.gameObject);
                }
                else if (otherShape == Shape.TETRAHEDRON)
                {
                    // 본인 패배
                    battle.player1.Remove(gameObject);
                    //Runner.Despawn(Object);
                    SelectionManager.Instance.AvailableUnits.Remove(this);

                    if (SelectionSprite.gameObject.activeSelf)
                        SelectionManager.Instance.SelectedUnits.Remove(this);

                    Destroy(gameObject);
                }
            }
            else if (shape == Shape.SPHERE)
            {
                if (otherShape == Shape.CUBE)
                {
                    // 본인 패배
                    battle.player1.Remove(gameObject);
                    //Runner.Despawn(Object);
                    SelectionManager.Instance.AvailableUnits.Remove(this);

                    if (SelectionSprite.gameObject.activeSelf)
                        SelectionManager.Instance.SelectedUnits.Remove(this);

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
                    //Runner.Despawn(other.GetComponent<NetworkObject>());
                    SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

                    if (otherRSPObject.SelectionSprite.gameObject.activeSelf)
                        SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

                    Destroy(other.gameObject);
                }
            }
            else if (shape == Shape.TETRAHEDRON)
            {
                if (otherShape == Shape.CUBE)
                {
                    // 본인 승리
                    battle.player2.Remove(other.gameObject);
                    //Runner.Despawn(other.GetComponent<NetworkObject>());
                    SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

                    if (otherRSPObject.SelectionSprite.gameObject.activeSelf)
                        SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

                    Destroy(other.gameObject);
                }
                else if (otherShape == Shape.SPHERE)
                {
                    // 본인 패배
                    battle.player1.Remove(gameObject);
                    //Runner.Despawn(Object);
                    SelectionManager.Instance.AvailableUnits.Remove(this);

                    if (SelectionSprite.gameObject.activeSelf)
                        SelectionManager.Instance.SelectedUnits.Remove(this);

                    Destroy(gameObject);
                }
                else if (otherShape == Shape.TETRAHEDRON)
                {
                    // 무승부
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