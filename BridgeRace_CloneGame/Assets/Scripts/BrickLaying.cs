using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class BrickLaying: MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        DetectBrickAndPick _detectBrickAndPick = collision.transform.GetComponent<DetectBrickAndPick>();
        Transform _stairsParentObject = transform.parent.parent;

        Material playerLastBrick = _detectBrickAndPick.Bricks[_detectBrickAndPick.Bricks.Count - 1].GetComponent<MeshRenderer>().material;
        Material stairLastBrick = _stairsParentObject.GetChild(_stairsParentObject.childCount - 1).GetComponent<MeshRenderer>().material;

        if (stairLastBrick.color == playerLastBrick.color)
            transform.parent.GetComponent<BrickManager>().PutNextBrick(collision.transform);
        else
            transform.parent.GetComponent<BrickManager>().PutBricksOnStairs(_stairsParentObject, stairLastBrick, playerLastBrick);
    }
    
}
