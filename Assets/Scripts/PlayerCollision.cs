using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CollisionX { None, Left, Middle, Right }
public enum CollisionY { None, Up, Middle, Down, LowDown }
public enum CollisionZ { None, Forward, Middle, Backward }

public class PlayerCollision : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private CollisionX collisionX;
    [SerializeField] private CollisionY collisionY;
    [SerializeField] private CollisionZ collisionZ;

    // Start is called before the first frame update
    void Awake()
    {
       playerController= gameObject.GetComponent<PlayerController>();
    }

    public void OnCharacterCollision(Collider col)
    {
        collisionX = GetCollisionX(col);  
        collisionY = GetCollisionY(col);
        collisionZ = GetCollisionZ(col);
        SetAnimatorByCollision();
    }

    private void SetAnimatorByCollision()
    {
        if (collisionZ == CollisionZ.Backward && collisionX == CollisionX.Middle)
        {
            if (collisionY == CollisionY.LowDown)
            {
                playerController.SetPlayerAnimator(playerController.IdStumbleLow,false);
            }
        }
    }

    private CollisionX GetCollisionX(Collider collider)
    {
        Bounds characterControllerBounds = playerController.MyCharacterController.bounds;
        Bounds colliderBounds = collider.bounds;
        float minX = Mathf.Max(colliderBounds.min.x, characterControllerBounds.min.x);
        float maxX = Mathf.Min(colliderBounds.max.x, characterControllerBounds.max.x);
        float average = (minX + maxX) /2 - colliderBounds.min.x;
        CollisionX colx;
        if (average > colliderBounds.size.x - 0.33f)
        {
            colx = CollisionX.Right;
        }
        else if (average < 0.33f)
        {
            colx = CollisionX.Left;
        }
        else
        {
            colx = CollisionX.Middle;
        }
        return colx;
    }

    private CollisionY GetCollisionY(Collider collider)
    {
        Bounds characterControllerBounds = playerController.MyCharacterController.bounds;
        Bounds colliderBounds = collider.bounds;
        float minY = Mathf.Max(colliderBounds.min.y, characterControllerBounds.min.y);
        float maxY = Mathf.Min(colliderBounds.max.y, characterControllerBounds.max.y);
        float average = (minY + maxY) / 2 - colliderBounds.min.y;
        CollisionY coly;
        
        if (average > colliderBounds.size.y - 0.33f)
        {
            coly = CollisionY.Up;
        }
        else if (average < 0.17f)
        {
            coly = CollisionY.LowDown;
        }
        else if (average < 0.33f)
        {
            coly = CollisionY.Down;
        }
        else
        {
            coly = CollisionY.Middle;
        }
        return coly;
    }

    private CollisionZ GetCollisionZ(Collider collider)
    {
        Bounds characterControllerBounds = playerController.MyCharacterController.bounds;
        Bounds colliderBounds = collider.bounds;
        float minZ = Mathf.Max(colliderBounds.min.z, characterControllerBounds.min.z);
        float maxZ = Mathf.Min(colliderBounds.max.z, characterControllerBounds.max.z);
        float average = (minZ + maxZ) / 2 - colliderBounds.min.z;
        CollisionZ colz;
        if (average > colliderBounds.size.z - 0.33f)
        {
            colz = CollisionZ.Forward;
        }
        else if (average < 0.33f)
        {
            colz = CollisionZ.Backward;
        }
        else
        {
            colz = CollisionZ.Middle;
        }
        return colz;
    }
}
