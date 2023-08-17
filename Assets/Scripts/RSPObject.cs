using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using System.Drawing;
using UnityEngine.AI;

public class RSPObject : NetworkBehaviour
{
    public enum Shape { CUBE, SPHERE, TETRAHEDRON };
    public Shape shape;
    public int playerNumber;
    public GameObject target, follower;

    [SerializeField] private SpriteRenderer SelectionSprite;

    private Battle battle;
    private UnitGenerator unitGenerator;

    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private Collider col;
    private Vector3 dir;

    private float fastSpeed;
    private float slowSpeed;

    private bool move;
    private Vector3 point;

    private bool collisionEffect;
    private bool brakingDist;
    private Vector3 velocity;
    private Vector3 stopTarget;

    private void Start()
    {
        SelectionManager.Instance.AvailableUnits.Add(this);
        navMeshAgent = GetComponent<NavMeshAgent>();

        battle = GameObject.FindObjectOfType<Battle>();
        unitGenerator = GameObject.FindObjectOfType<UnitGenerator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        move = false;
        collisionEffect = false;
        brakingDist = false;
        velocity = Vector3.zero;

        fastSpeed = navMeshAgent.speed;
        slowSpeed = navMeshAgent.speed * 0.5f;

        //navMeshAgent.autoBraking = false;
    }

    private void Update()
    {
        CollisionEffectUpdate();
        BrakingDistanceUpdate();
    }

    public override void FixedUpdateNetwork()
    {
        //base.FixedUpdateNetwork();

        if (Runner.IsServer)
        {
            if (unitGenerator.gameStarted)
                ChasingTarget();
        }

        // 마우스 클릭 Move vs ChasingTarget() 머가 먼저 되지?? 어떻게 알지?
        // target쫓으러 가다가 움직임 넣어서 다른데도 가다가 타겟이 없어지면 어떤 명령이 들어갈까 다른데로 가던걸 마무리할까 다시 새로운 target을 잡으러갈까?
        if (move)
        {
            //dir = (point - transform.position).normalized;

            // 1
            //rb.MovePosition(transform.position + dir * 10f * Runner.DeltaTime);
            //rb.MovePosition(transform.position + dir.normalized * 10f * Time.deltaTime);
            //rb.rotation = Quaternion.LookRotation(dir);

            // 2
            //rb.position = transform.position + dir * Runner.DeltaTime * 3f;
            //if(target != null)
            //    transform.LookAt(target.transform.position);

            // 3
            //  속도를 좀 줄이고 싶은데
            navMeshAgent.speed = slowSpeed;
            navMeshAgent.SetDestination(point);
            

            if (Vector3.Distance(point, transform.position) < 0.1f)
            {
                move = false;
                navMeshAgent.speed = fastSpeed;
            }
        }
    }

    public void ChasingTarget()
    {
        if (target != null)
        {
            //dir = (target.transform.position - transform.position).normalized;

            //rb.MovePosition(transform.position + dir.normalized * Runner.DeltaTime * 5f);
            //rb.rotation = Quaternion.LookRotation(dir);

            //rb.position = transform.position + dir * Runner.DeltaTime * 5f;

            //transform.LookAt(target.transform.position);


            navMeshAgent.SetDestination(target.transform.position);
            
            
        }
        //else if ((target == null || target.activeSelf == false) && Object.isActiveAndEnabled == true && gameObject.activeSelf == true)
        //else if(target == null || target.activeSelf == false)
        else if(target == null)
        {

            target = null;
            navMeshAgent.isStopped = true;
            navMeshAgent.SetDestination(transform.position);
            battle.SelectTargetV2(gameObject);
            navMeshAgent.isStopped = false;
        } else if(target.activeSelf == false)
        {
            print("클린스만 감독");
        } else if(target.activeInHierarchy == false)
        {
            print("황선홍 감독");
        }

    }

    private IEnumerator BrakingDIstance()
    {
        if (navMeshAgent.isActiveAndEnabled)
        {
            brakingDist = true;
            navMeshAgent.isStopped = true;
        }

        // 제동목표 설정
        velocity = navMeshAgent.velocity;
        stopTarget = transform.position + transform.TransformDirection(Vector3.forward) * 5f;

        yield return new WaitForSeconds(2f);

        if (navMeshAgent.isActiveAndEnabled)
        {
            brakingDist = false;
            navMeshAgent.isStopped = false;
        }

        //brakingDist = false;
        //navMeshAgent.isStopped = false;
    }

    private void BrakingDistanceUpdate()
    {
        if (brakingDist)
        {
            transform.position = Vector3.SmoothDamp(transform.position, stopTarget, ref velocity, 2f);
        }
    }

    private IEnumerator CollisionEffect()
    {
        collisionEffect = true;
        GetComponent<Collider>().isTrigger = false;

        yield return new WaitForSeconds(1.5f);

        collisionEffect = false;
        GetComponent<Collider>().isTrigger = true;
    }

    private void CollisionEffectUpdate()
    {
        if (collisionEffect)
        {
            //transform.position = transform.InverseTransformDirection(Vector3.Lerp(transform.position, transform.position + Vector3.forward * 5f, Runner.DeltaTime));
            //transform.position = transform.InverseTransformDirection(Vector3.Lerp(transform.position, transform.position + Vector3.left * 5f, Runner.DeltaTime));
            transform.position = transform.TransformDirection(Vector3.Lerp(transform.position, transform.position + Vector3.left * 5f, Runner.DeltaTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //RSPObject thisRSPOBject = GetComponent<RSPObject>();

        if (other.CompareTag(gameObject.tag) == false && (other.CompareTag("Player 1") || other.CompareTag("Player 2")))
        {
            // 애니메이션 추가

            Shape otherShape = other.GetComponent<RSPObject>().shape;
            RSPObject otherRSPObject = other.GetComponent<RSPObject>();
            int enemyCamp = otherRSPObject.playerNumber - 1;

            if (shape == Shape.CUBE)
            {
                if (otherShape == Shape.CUBE)
                {
                    // 무승부
                }
                else if (otherShape == Shape.SPHERE)
                {
                    // 본인 승리
                    //if (playerNumber == 1) battle.player2.Remove(other.gameObject);
                    //else if (playerNumber == 2) battle.player1.Remove(other.gameObject);

                    UnitSets.instance.units[enemyCamp][1].Remove(otherRSPObject);
                    UnitSets.instance.outsider2[enemyCamp][1].Remove(otherRSPObject);

                    SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

                    if (otherRSPObject.SelectionSprite.gameObject.activeSelf)
                        SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

                    //Destroy(other.gameObject);
                    Runner.Despawn(other.GetComponent<NetworkObject>());

                    // 제동거리 추가
                    StartCoroutine(BrakingDIstance());
                    //StartCoroutine(CollisionEffect());

                    VoronoiColoring.instance.mp.ColoringVoronoiCell(other.ClosestPoint(transform.position), playerNumber);
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

                    //if (playerNumber == 1) battle.player2.Remove(other.gameObject);
                    //else if (playerNumber == 2) battle.player1.Remove(other.gameObject);

                    UnitSets.instance.units[enemyCamp][2].Remove(otherRSPObject);
                    UnitSets.instance.outsider2[enemyCamp][2].Remove(otherRSPObject);

                    SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

                    if (otherRSPObject.SelectionSprite.gameObject.activeSelf)
                        SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

                    //Destroy(other.gameObject);
                    Runner.Despawn(other.GetComponent<NetworkObject>());

                    StartCoroutine(BrakingDIstance());
                    //StartCoroutine(CollisionEffect());

                    VoronoiColoring.instance.mp.ColoringVoronoiCell(other.ClosestPoint(transform.position), playerNumber);
                }
            }
            else if (shape == Shape.TETRAHEDRON)
            {
                if (otherShape == Shape.CUBE)
                {
                    // 본인 승리
                    //battle.player2.Remove(other.gameObject);

                    //if (playerNumber == 1) battle.player2.Remove(other.gameObject);
                    //else if (playerNumber == 2) battle.player1.Remove(other.gameObject);

                    UnitSets.instance.units[enemyCamp][0].Remove(otherRSPObject);
                    UnitSets.instance.outsider2[enemyCamp][0].Remove(otherRSPObject);

                    SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

                    if (otherRSPObject.SelectionSprite.gameObject.activeSelf)
                        SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

                    //Destroy(other.gameObject);
                    Runner.Despawn(other.GetComponent<NetworkObject>());

                    StartCoroutine(BrakingDIstance());
                    //StartCoroutine(CollisionEffect());

                    VoronoiColoring.instance.mp.ColoringVoronoiCell(other.ClosestPoint(transform.position), playerNumber);
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


    //private void OnCollisionEnter(Collision collision)
    //{
    //    //RSPObject thisRSPOBject = GetComponent<RSPObject>();
    //    if (collision.gameObject.CompareTag(gameObject.tag) == false && (collision.gameObject.CompareTag("Player 1") || collision.gameObject.CompareTag("Player 2")))
    //    {
    //        // 애니메이션 추가

    //        Shape otherShape = collision.gameObject.GetComponent<RSPObject>().shape;
    //        RSPObject otherRSPObject = collision.gameObject.GetComponent<RSPObject>();

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

    //                UnitSets.instance.units[2 - playerNumber][1].Remove(otherRSPObject);

    //                SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

    //                if (otherRSPObject.SelectionSprite.gameObject.activeSelf)
    //                    SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

    //                //Destroy(other.gameObject);
    //                Runner.Despawn(collision.gameObject.GetComponent<NetworkObject>());

    //                // 제동거리 추가
    //                StartCoroutine(BrakingDIstance());
    //                //StartCoroutine(CollisionEffect());

    //            }
    //            //else if (otherShape == Shape.TETRAHEDRON)
    //            //{
    //            //    // 본인 패배
    //            //    if (playerNumber == 1) battle.player2.Remove(gameObject);
    //            //    else if (playerNumber == 2) battle.player1.Remove(gameObject);

    //            //    battle.player1.Remove(gameObject);

    //            //    SelectionManager.Instance.AvailableUnits.Remove(this);

    //            //    if (SelectionSprite.gameObject.activeSelf)
    //            //        SelectionManager.Instance.SelectedUnits.Remove(this);

    //            //    //Destroy(gameObject);
    //            //    Runner.Despawn(Object);
    //            //}
    //        }
    //        else if (shape == Shape.SPHERE)
    //        {
    //            //if (otherShape == Shape.CUBE)
    //            //{
    //            //    // 본인 패배
    //            //    //battle.player1.Remove(gameObject);

    //            //    if (playerNumber == 1) battle.player2.Remove(gameObject);
    //            //    else if (playerNumber == 2) battle.player1.Remove(gameObject);


    //            //    SelectionManager.Instance.AvailableUnits.Remove(this);

    //            //    if (SelectionSprite.gameObject.activeSelf)
    //            //        SelectionManager.Instance.SelectedUnits.Remove(this);

    //            //    Destroy(gameObject);
    //            //    Runner.Despawn(Object);
    //            //}
    //            //else 
    //            if (otherShape == Shape.SPHERE)
    //            {
    //                // 무승부
    //            }
    //            else if (otherShape == Shape.TETRAHEDRON)
    //            {
    //                // 본인 승리
    //                //battle.player2.Remove(other.gameObject);

    //                //if (playerNumber == 1) battle.player2.Remove(other.gameObject);
    //                //else if (playerNumber == 2) battle.player1.Remove(other.gameObject);

    //                UnitSets.instance.units[2 - playerNumber][2].Remove(otherRSPObject);

    //                SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

    //                if (otherRSPObject.SelectionSprite.gameObject.activeSelf)
    //                    SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

    //                //Destroy(other.gameObject);
    //                Runner.Despawn(collision.gameObject.GetComponent<NetworkObject>());

    //                StartCoroutine(BrakingDIstance());
    //                //StartCoroutine(CollisionEffect());
    //            }
    //        }
    //        else if (shape == Shape.TETRAHEDRON)
    //        {
    //            if (otherShape == Shape.CUBE)
    //            {
    //                // 본인 승리
    //                //battle.player2.Remove(other.gameObject);

    //                //if (playerNumber == 1) battle.player2.Remove(other.gameObject);
    //                //else if (playerNumber == 2) battle.player1.Remove(other.gameObject);

    //                UnitSets.instance.units[2 - playerNumber][0].Remove(otherRSPObject);

    //                SelectionManager.Instance.AvailableUnits.Remove(otherRSPObject);

    //                if (otherRSPObject.SelectionSprite.gameObject.activeSelf)
    //                    SelectionManager.Instance.SelectedUnits.Remove(otherRSPObject);

    //                //Destroy(other.gameObject);
    //                Runner.Despawn(collision.gameObject.GetComponent<NetworkObject>());

    //                StartCoroutine(BrakingDIstance());
    //                //StartCoroutine(CollisionEffect());
    //            }
    //            //else if (otherShape == Shape.SPHERE)
    //            //{
    //            //    // 본인 패배
    //            //    //battle.player1.Remove(gameObject);

    //            //    if (playerNumber == 1) battle.player2.Remove(gameObject);
    //            //    else if (playerNumber == 2) battle.player1.Remove(gameObject);


    //            //    SelectionManager.Instance.AvailableUnits.Remove(this);

    //            //    if (SelectionSprite.gameObject.activeSelf)
    //            //        SelectionManager.Instance.SelectedUnits.Remove(this);

    //            //    Destroy(gameObject);
    //            //    Runner.Despawn(Object);
    //            //}
    //            else if (otherShape == Shape.TETRAHEDRON)
    //            {
    //                // 무승부
    //            }
    //        }
    //    }
    //}
}