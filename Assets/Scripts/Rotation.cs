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


    // ������Ʈ ��Ʈ��ũ ȯ�游 ���� �� �ȉ�?? �ФФФ� ������ٵ�� �ؾ��ϳ�
    // Ʈ���������� ���� �ȵ��ư� �ֱ׷���
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        transform.Rotate(0f, Runner.DeltaTime * 50f, 0f);
    }
}