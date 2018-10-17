using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public float rotationX = 14f;
    public float rotationZ = -10f;
    private float smooth = 5.0f;
    private Quaternion target;

    void Start()
    {
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        while (true)
        {
            DetermineNewRotation();
            yield return new WaitForSeconds(1);
        }
    }

    private void DetermineNewRotation()
    {
        target = Quaternion.Euler(rotationX, Random.Range(-360.0f, 360.0f), rotationZ);
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }
}
