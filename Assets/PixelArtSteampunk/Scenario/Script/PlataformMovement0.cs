using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformMovement0 : MonoBehaviour
{
    public Transform position1, position2;
    public Transform initialPosition;
    public float speed = 1f;

    private Vector3 nextPosition;

    void Start()
    {
        nextPosition = initialPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == position1.position)
        {
            nextPosition = position2.position;
        }
        if (transform.position == position2.position)
        {
            nextPosition = position1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }
    // Mostra a linha por onde a plataforma se move
    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(position1.position, position2.position);
    }
    */
}
