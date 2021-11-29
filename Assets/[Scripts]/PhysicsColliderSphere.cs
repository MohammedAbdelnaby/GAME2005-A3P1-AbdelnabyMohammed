using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PhysicsColliderSphere : PhysicsCollider
{
    public Axis alignment = Axis.Y;
    public float raduis = 1;
    private CollistionShape shapeType = CollistionShape.Sphere;

    public override CollistionShape GetCollistionShape()
    {
        return shapeType;
    }

    public float getRaduis()
    {
        return raduis;
    }
    public Vector3 getNormal()
    {
        switch (alignment)
        {
            case (Axis.X):
            {
                return transform.right;
            }
            case (Axis.Y):
            {
                return transform.up;
            }
            case (Axis.Z):
            {
                return transform.forward;
            }
            default:
            {
                throw new Exception("Invalid plane alignment");
            }
        }
    }
}