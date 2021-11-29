using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab8PhysicsObjects : MonoBehaviour
{
    public float mass = 1.0f;
    public Vector3 velocity = Vector3.zero;
    public PhysicsCollider shape = null;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<Lab8PhysicsSystem>().lab8Physics.Add(this);
        shape = GetComponent<PhysicsCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = new Vector3(0, Mathf.Sin(Time.time),0);
        transform.position += velocity * Time.deltaTime;
    }
}