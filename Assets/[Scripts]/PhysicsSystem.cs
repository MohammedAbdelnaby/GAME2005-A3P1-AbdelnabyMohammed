using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

public class PhysicsSystem : MonoBehaviour
{
    public Slider gravityScaleSlider;
    public Toggle gravityCheckBox;
    public List<PhysicsObjects> lab8Physics = new List<PhysicsObjects>();
    private Vector3 gravity = new Vector3(0, 0, 0);
    public List<PhysicsCollider> ColliderShapes;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float gravityValue = gravityScaleSlider.value;
        gravity = new Vector3(0, gravityValue, 0);

        if (gravityCheckBox.isOn)
        {
            for (int i = 0; i < lab8Physics.Count; i++)
            {
                if (lab8Physics[i].shape.GetCollistionShape() == CollistionShape.Sphere)
                {
                    lab8Physics[i].velocity += gravity * Time.fixedDeltaTime;
                }

            }
        }
        else
        {
            gravity = new Vector3(0, 0, 0);
        }

        CollisionUpdate();
    }
    void CollisionUpdate()
    {
        for (int i = 0; i < lab8Physics.Count; i++)
        {
            for (int j = i + 1; j < lab8Physics.Count; j++)
            {
                PhysicsObjects ObjectA = lab8Physics[i];
                PhysicsObjects ObjectB = lab8Physics[j];

                Vector3 ObjectAPosition = ObjectA.transform.position;
                Vector3 ObjectBPosition = ObjectB.transform.position;
                Vector3 displacement = ObjectBPosition - ObjectAPosition;

                if (ObjectA.shape == null || ObjectB.shape == null)
                {
                    continue;
                }

                if (ObjectA.shape.GetCollistionShape() == CollistionShape.Sphere
                    && ObjectB.shape.GetCollistionShape() == CollistionShape.Sphere)
                {
                    SphereSphereCollision((PhysicsColliderSphere)ObjectA.shape, (PhysicsColliderSphere)ObjectB.shape);
                }

                if (ObjectA.shape.GetCollistionShape() == CollistionShape.Sphere
                    && ObjectB.shape.GetCollistionShape() == CollistionShape.Plane)
                {
                    SpherePlaneCollision((PhysicsColliderSphere) ObjectA.shape, (PhysicsColliderPlane) ObjectB.shape);
                }

                if (ObjectA.shape.GetCollistionShape() == CollistionShape.Plane
                    && ObjectB.shape.GetCollistionShape() == CollistionShape.Sphere)
                {
                    SpherePlaneCollision((PhysicsColliderSphere) ObjectB.shape, (PhysicsColliderPlane) ObjectA.shape);
                }
            }
        }
    }

    static void SphereSphereCollision(PhysicsColliderSphere sphere1, PhysicsColliderSphere sphere2)
    {
        Vector3 Displacement = sphere2.transform.position - sphere1.transform.position;
        float Distance = Displacement.magnitude;
        float SumRaduis = sphere1.getRaduis() + sphere2.getRaduis();
        float PenetrationDepth = SumRaduis - Distance;
        bool IsOverLapping = PenetrationDepth > 0;
        if (!IsOverLapping)
        {
            return;
        }
        float minimumDistance = 0.0001f;
        if (Distance < minimumDistance)
        {
            Distance = minimumDistance;
        }
        // https://exploratoria.github.io/exhibits/mechanics/elastic-collisions-in-3d/
        Vector3 CollisionNormalAToB = Displacement / Distance;
        Vector3 relative = sphere1.KinematicsObject.velocity - sphere2.KinematicsObject.velocity;
        Vector3 norVec = (Vector3.Dot(relative, CollisionNormalAToB)) * CollisionNormalAToB;
        sphere1.KinematicsObject.velocity -= norVec;
        sphere2.KinematicsObject.velocity += norVec;
    }

    static void SpherePlaneCollision(PhysicsColliderSphere sphere, PhysicsColliderPlane plane)
    {
        Vector3 PointonOnPlane = plane.transform.position;
        Vector3 CenterOfSphere = sphere.transform.position;
        Vector3 FromPlaneToSphere = CenterOfSphere - PointonOnPlane;
        float dot = Vector3.Dot(FromPlaneToSphere, plane.getNormal());
        Vector3 Displacement = PointonOnPlane - sphere.transform.position;
        float Distance = dot;
        float penetrationdepth = sphere.getRaduis() - Distance;
        bool isOverLapping = penetrationdepth > 0;
        if (isOverLapping)
        {
            //https://math.stackexchange.com/questions/13261/how-to-get-a-reflection-vector
            sphere.KinematicsObject.velocity -=
                ((2 * (Vector3.Dot(sphere.KinematicsObject.velocity, plane.getNormal()))) * plane.getNormal()) * 0.9f;
        }
        else
        {
            return;
        }
    }
}
