using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PhysicsColliderPlane : PhysicsCollider
{
    public Axis alignment = Axis.Y;



    private CollistionShape shapeType = CollistionShape.Plane;
    public override CollistionShape GetCollistionShape()
    {
        return shapeType;
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