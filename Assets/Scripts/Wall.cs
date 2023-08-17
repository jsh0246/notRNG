using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Vector3 dir, t;

    private void Update()
    {
        //Debug.DrawRay(t, dir * 100, Color.yellow);
        //Debug.DrawLine(t, transform.position);
        //Debug.DrawRay(Vector3.zero, dir * 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player 1") || other.CompareTag("Player 2"))
        {
            Rigidbody rb = other.attachedRigidbody;

            Vector3 collisionPoint = other.ClosestPointOnBounds(transform.position);
            //Vector3 collisionPoint = other.ClosestPoint(transform.position);
            //Vector3 collisionPoint = other.ClosestPoint(other.transform.position);
            collisionPoint = new Vector3(collisionPoint.x, 0, collisionPoint.z);

            t = collisionPoint;
            //rb.AddForce(rb.transform.forward * -10, ForceMode.Impulse);
            //rb.AddTorque(rb.transform.forward * -10, ForceMode.Impulse);

            Vector3 p = new Vector3(other.gameObject.transform.position.x, 0f, other.gameObject.transform.position.z);

            dir = p - collisionPoint;
            dir = dir.normalized;

            //dir = (other.gameObject.transform.position - collisionPoint).normalized;

            //Vector3 dir = Vector3.left;

            //rb.AddForce(dir * 20f);
            //rb.velocity = dir * 20f;

            //rb.AddExplosionForce(15f, collisionPoint, 5f, 0f, ForceMode.Impulse);
            //rb.AddExplosionForce(15f, collisionPoint - dir, 5f, 0f, ForceMode.Impulse);
            
            // 잘모르고 transformdirection씀 어떤결과가 나오게 되는지 모름
            // 이정도하고 일단 넘어감
            rb.AddExplosionForce(15f, collisionPoint+transform.TransformDirection(Vector3.forward)*2f, 5f, 0f, ForceMode.Impulse);

            //rb.AddExplosionForce(100f, other.transform.position, 5);

        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Player 1") || collision.collider.CompareTag("Player 2"))
    //    {
    //        Rigidbody rb = collision.rigidbody;

    //        //rb.AddForce(rb.transform.forward * -10, ForceMode.Impulse);
    //        //rb.AddTorque(rb.transform.forward * -10, ForceMode.Impulse);

    //        rb.AddExplosionForce(15f, collision.contacts[0].point, 5f, 0f, ForceMode.Impulse);

    //    }
    //}
}