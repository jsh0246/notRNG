using UnityEngine;

public class Rotation : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0f, Time.deltaTime * 50f, 0f);
    }
}