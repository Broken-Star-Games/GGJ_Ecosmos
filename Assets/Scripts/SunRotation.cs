using UnityEngine;

public class SunRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Vector3.zero, Vector3.up, 5*Time.deltaTime);
    }
}
