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

        // 마우스 클릭 Move vs ChasingTarget() 머가 먼저 되지?? 어떻게 알지?
        // target쫓으러 가다가 움직임 넣어서 다른데도 가다가 타겟이 없어지면 어떤 명령이 들어갈까 다른데로 가던걸 마무리할까 다시 새로운 target을 잡으러갈까?
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
            // 서버 클라 실행할 때는 여전히 오류
            battle.SelecTarget(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //RSPObject thisRSPOBject = GetComponent<RSPObject>();

        if(other.CompareTag(gameObject.tag) == false && (other.CompareTag("Player 1") || other.CompareTag("Player 2")))
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
                //    // 본인 패배
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
                //    // 본인 패배
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
                    // 무승부
                }
                else if (otherShape == Shape.TETRAHEDRON)
                {
                    // 본인 승리
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
                    // 본인 승리
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
                //    // 본인 패배
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