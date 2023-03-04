using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    private Transform PlayerTransform;
    private DetectBrickAndPick Player_DetectBrickAndPick;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float RayRadius;

    public bool putFirstStair;

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z - .6f));
    }

    private void Start()
    {
        putFirstStair = false;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Player_DetectBrickAndPick = PlayerTransform.GetComponent<DetectBrickAndPick>();
    }

    private void FixedUpdate()
    {
        if(!putFirstStair)
            DetectPlayer();
        else
            updatePlayerAxisY();
    }

    void DetectPlayer()
    {
        if (Physics.CheckSphere(transform.position, RayRadius, playerLayer))
            Player_DetectBrickAndPick.PickBricks(transform);
    }

    private void updatePlayerAxisY()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, Vector3.back, out hitInfo, .6f))
            PlayerTransform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y + .25f, PlayerTransform.position.z + .05f);
    }

    public void PutBricksOnStairs(Transform StairsParentObject)
    {
        putFirstStair = true;

        if (StairsParentObject.childCount == 0)
        {
            transform.position = StairsParentObject.position;
            transform.parent = StairsParentObject;
        }
        else
        {
            Transform newStairPos = StairsParentObject.GetChild(StairsParentObject.childCount - 1);;
            transform.position = new Vector3(newStairPos.position.x, newStairPos.position.y + .3f, newStairPos.position.z + 1f);
            transform.parent = StairsParentObject;
        }

    }
}
