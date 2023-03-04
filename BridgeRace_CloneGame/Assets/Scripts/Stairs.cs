using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField] private Transform detectPoint;
    [SerializeField] private Transform StairsParentObject;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector3 detectArea;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(detectPoint.position, new Vector3(detectPoint.position.x, detectPoint.position.y, detectPoint.position.z - .5f));
    }

    private void FixedUpdate()
    {
        DetectFonc();
    }

    private void DetectFonc()
    {
        RaycastHit hitInfo;
        DetectBrickAndPick detectBrickAndPick_Player;
        Transform bricksOldParent; 

        if (Physics.Raycast(detectPoint.position, Vector3.back, out hitInfo, .5f))
        {
            detectBrickAndPick_Player = hitInfo.transform.GetComponent<DetectBrickAndPick>();
            bricksOldParent = detectBrickAndPick_Player.PickBrickPoint;
            
            if (bricksOldParent.childCount == 0)
                return;

            else
            {
                detectBrickAndPick_Player.Bricks[detectBrickAndPick_Player.Bricks.Count - 1].GetComponent<BrickManager>().PutBricksOnStairs(StairsParentObject);
                detectBrickAndPick_Player.Bricks[detectBrickAndPick_Player.Bricks.Count - 1].GetComponent<BrickManager>().enabled = true;
                detectBrickAndPick_Player.Bricks[detectBrickAndPick_Player.Bricks.Count - 1].GetComponent<Collider>().isTrigger = false;
                detectBrickAndPick_Player.Bricks.Remove(detectBrickAndPick_Player.Bricks[detectBrickAndPick_Player.Bricks.Count - 1]);
            }
        }
    }
}
