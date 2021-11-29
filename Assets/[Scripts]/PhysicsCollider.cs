using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollistionShape
{
    Sphere,
    Plane,
    AABB
}

public abstract class PhysicsCollider : MonoBehaviour
{

    public abstract CollistionShape GetCollistionShape();
    public PhysicsObjects KinematicsObject;

    public void Start()
    {
        KinematicsObject = GetComponent<PhysicsObjects>();
        FindObjectOfType<PhysicsSystem>().ColliderShapes.Add(this);
    }
}