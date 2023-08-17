using UnityEngine;
using Fusion;

public class Rotation : NetworkBehaviour
{
    //private void Update()
    //{
    //    transform.Rotate(0f, Time.deltaTime * 50f, 0f);
    //}

    //public override void FixedUpdateNetwork()
    //{
    //transform.Rotate(0f, Runner.DeltaTime * 5f, 0f);
    //transform.rotation *= Quaternion.Euler(Vector3.up * Runner.DeltaTime * 5f);
    //GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(Vector3.up * Runner.DeltaTime * 5f));
    //GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(Vector3.right * Runner.DeltaTime * 50f));
    //}
    private Transform childTransform;

    private void Awake()
    {
        childTransform = GetComponentInChildren<Transform>();
    }

    // 로테이트 네트워크 환경만 들어가면 왜 안됌?? ㅠㅠㅠㅠ 리지드바디로 해야하나
    // 트랜스폼에서 각도 안돌아감 왜그렇지
    // 자식에서 뭔가 하려고하면 죽도밥도 안되는듯 스크립트는 부모로 몰아야하는거같기도 아닐지도
    public override void FixedUpdateNetwork()
    {
        childTransform.Rotate(0f,Runner.DeltaTime * 50f, 0f);
    }
}