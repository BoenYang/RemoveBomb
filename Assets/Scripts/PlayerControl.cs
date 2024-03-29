﻿using UnityEngine;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{

    private Vector3 startPoint;

    private Vector3 endPoint;

    public TrailRenderer Trail;

    private bool click = false;

	void Update () {

		if (!GameScene.Instance.Game.GameRunning || GameScene.Instance.Game.GamePaused)
		{
		    click = false;
            return;
        }

	    if (Input.GetMouseButtonDown(0))
	    {
	        startPoint = Input.mousePosition;
            UIManager.DispatchMsg("TouchDown",startPoint);
	        click = true;
	        //Trail.Clear();
	    }

	    if (Input.GetMouseButton(0))
	    {
			Vector3 trailPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	        trailPosition.z = 1;
            Trail.gameObject.transform.position = trailPosition;
            UIManager.DispatchMsg("TouchMove", Input.mousePosition);
        }

	    if (Input.GetMouseButtonUp(0) && click)
	    {
	        click = false;
	        endPoint = Input.mousePosition;

            UIManager.DispatchMsg("TouchUp", endPoint);

            Vector3 start = Camera.main.ScreenToWorldPoint(startPoint);
			Vector3 end = Camera.main.ScreenToWorldPoint(endPoint);
            List<SpriteSlicer2DSliceInfo> spriteSlicer2DSliceInfos = new List<SpriteSlicer2DSliceInfo>();
            SpriteSlicer2D.SliceAllSprites(start,end,true,ref spriteSlicer2DSliceInfos,LayerMask.GetMask(new string[]{"Cut"}));
	        for (int i = 0; i < spriteSlicer2DSliceInfos.Count; i++)
	        {
	            for (int j = 0; j < spriteSlicer2DSliceInfos[i].ChildObjects.Count; j++)
	            {
	                SpriteSlicer2DSliceInfo sliceInfo = spriteSlicer2DSliceInfos[i];
	                GameObject childObject = sliceInfo.ChildObjects[j];
	                Rigidbody2D childRigidBody = childObject.GetComponent<Rigidbody2D>();
                    Vector2 childPos = new Vector2(childRigidBody.worldCenterOfMass.x,childRigidBody.worldCenterOfMass.y);

                    Vector2 cutLineMidPos = (sliceInfo.SliceEnterWorldPosition - sliceInfo.SliceExitWorldPosition)/2f;
	                Vector2 cutLineDir = (sliceInfo.SliceExitWorldPosition - sliceInfo.SliceEnterWorldPosition).normalized;

	                float dir = (childPos.y - sliceInfo.SliceExitWorldPosition.y)*
	                            (sliceInfo.SliceEnterWorldPosition.x - sliceInfo.SliceExitWorldPosition.x) -
	                            (sliceInfo.SliceEnterWorldPosition.y - sliceInfo.SliceExitWorldPosition.y)*
	                            (childPos.x - sliceInfo.SliceExitWorldPosition.x);
                    
	                float angle = Vector2.Angle(cutLineDir, Vector2.right);

	                if (dir > 0)
	                {
	                    angle = angle + 90;
	                }
	                else
	                {
	                    angle = angle - 90;
	                }

                    Vector2 normal = new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad),Mathf.Sin(angle*Mathf.Deg2Rad));

                    //childRigidBody.AddForceAtPosition(childRigidBody.mass * normal * 0.5f ,cutLineMidPos,ForceMode2D.Impulse);

                    childObject.layer = 8;
                    MeshRenderer sr = childObject.GetComponent<MeshRenderer>();
	                sr.sortingLayerName = "Default";
	                sr.sortingOrder = 5;
	            }
	        }
	    }
	}
}
