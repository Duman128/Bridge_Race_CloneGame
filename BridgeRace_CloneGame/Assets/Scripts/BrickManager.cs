using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    private Transform PlayerTransform;
    private Material brickColorMaterial;
   
    public Stairs _Stairs;

    private DetectBrickAndPick Player_DetectBrickAndPick;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask stairsLayer;
    [SerializeField] private float RayRadius;
    [SerializeField] private Vector3 detectBoxSize;

    public bool putFirstStair;
    
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        brickColorMaterial = GetComponent<MeshRenderer>().material;

        putFirstStair = false;
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Player_DetectBrickAndPick = PlayerTransform.GetComponent<DetectBrickAndPick>();
    }

    private void FixedUpdate()
    {

        if (!putFirstStair)
            DetectPlayer();
        else
            WallAndUpdateYAxisFonc();
            
    }

    private void WallAndUpdateYAxisFonc()
    {
        _Stairs = transform.parent.parent.GetComponent<Stairs>();

        updatePlayerAxisY();

        if (_Stairs.StairsParentObject.childCount == _Stairs.stairCount)
            transform.GetChild(0).gameObject.SetActive(false);

        else if (transform == _Stairs.StairsParentObject.GetChild(_Stairs.StairsParentObject.childCount - 1))
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        else
            transform.GetChild(0).gameObject.SetActive(false);
    }

    private void DetectPlayer()
    {
        if (Physics.CheckSphere(transform.position, RayRadius, playerLayer))
            Player_DetectBrickAndPick.PickBricks(transform, brickColorMaterial);
    }

    private void updatePlayerAxisY()
    {
        RaycastHit hitInfo;
        if (Physics.BoxCast(transform.position, transform.lossyScale / 2, Vector3.back, out hitInfo, transform.localRotation, .2f))
            hitInfo.transform.position = new Vector3(hitInfo.transform.position.x, hitInfo.transform.position.y + .25f, hitInfo.transform.position.z + .05f);
    }

    public void PutNextBrick(Transform hitObject)
    {
        DetectBrickAndPick detectBrickAndPick_Player = hitObject.transform.GetComponent<DetectBrickAndPick>();
        Transform bricksOldParent = detectBrickAndPick_Player.PickBrickPoint;

        _Stairs.PutStairsOnPlace(hitObject, detectBrickAndPick_Player, bricksOldParent);
    }

    public void PutBricksOnStairs(Transform StairsParentObject, Material StairLastObject, Material PlayerLastObject)
    {
        putFirstStair = true;

        if (StairsParentObject.childCount == 0 && StairLastObject == PlayerLastObject)
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
