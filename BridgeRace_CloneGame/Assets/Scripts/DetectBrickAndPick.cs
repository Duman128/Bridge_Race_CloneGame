using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBrickAndPick : MonoBehaviour
{
    public Transform PickBrickPoint;
    public List<GameObject> Bricks;

    private void Awake()
    {
        Bricks = new List<GameObject>();
    }

    public void PickBricks(Transform BrickTransform)
    {
        Bricks.Add(BrickTransform.gameObject);

        float BrickPosY = PickBrickPoint.position.y + (Bricks.Count * 0.32f);

        Vector3 newBrickPoint = new Vector3(PickBrickPoint.position.x, BrickPosY, PickBrickPoint.position.z);

        BrickTransform.GetComponent<BrickManager>().enabled = false;
        BrickTransform.position = newBrickPoint;
        BrickTransform.parent = PickBrickPoint;
    }
}
