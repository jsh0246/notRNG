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

    private void Update()
    {
        //transform.Rotate(0f, Time.deltaTime * 50f, 0f);
        //GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(Vector3.right * Time.deltaTime * 50f));
    }


    // 로테이트 네트워크 환경만 들어가면 왜 안됌?? ㅠㅠㅠㅠ 리지드바디로 해야하나
    // 트랜스폼에서 각도 안돌아감 왜그렇지
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        transform.Rotate(0f, Runner.DeltaTime * 50f, 0f);
    }
}