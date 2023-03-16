using System.Collections;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public static Stairs _stairs;

    public Transform StairsParentObject;
   
    [SerializeField] private Transform detectPoint;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Vector3 detectArea;
    public int stairCount;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(detectPoint.position, new Vector3(detectPoint.position.x, detectPoint.position.y, detectPoint.position.z - .5f));
    }
    
    private void Start()
    {
        _stairs = this;
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

        if (Physics.BoxCast(detectPoint.position, detectArea,Vector3.back, out hitInfo,Quaternion.identity,.5f))
        {
            detectBrickAndPick_Player = hitInfo.transform.GetComponent<DetectBrickAndPick>();
            bricksOldParent = detectBrickAndPick_Player.PickBrickPoint;

            PutStairsOnPlace(hitInfo.transform, detectBrickAndPick_Player, bricksOldParent);
        }
    }

    public void PutStairsOnPlace(Transform hitInfo, DetectBrickAndPick detectBrickAndPick_Player, Transform bricksOldParent)
    {
        Material playerLastBrick = detectBrickAndPick_Player.Bricks[detectBrickAndPick_Player.Bricks.Count - 1].GetComponent<MeshRenderer>().material;//Ýlk Brick olmadýðý için boþ oluyor ve hata üretiyor DÜZELT!!!!
        Material stairLastBrick = StairsParentObject.GetChild(StairsParentObject.childCount - 1).GetComponent<MeshRenderer>().material;

        if (detectBrickAndPick_Player == null)
            return;

        else if (bricksOldParent.childCount == 0 || StairsParentObject.childCount == stairCount)
            return;

        else
        {
            detectBrickAndPick_Player.Bricks[detectBrickAndPick_Player.Bricks.Count - 1].GetComponent<BrickManager>().PutBricksOnStairs(StairsParentObject, stairLastBrick, playerLastBrick); //Fix Position Added Bricks Fonc
            
            detectBrickAndPick_Player.Bricks[detectBrickAndPick_Player.Bricks.Count - 1].GetComponent<Collider>().isTrigger = false;
            detectBrickAndPick_Player.Bricks.Remove(detectBrickAndPick_Player.Bricks[detectBrickAndPick_Player.Bricks.Count - 1]);
            this.enabled = false;
        }
    }
}
