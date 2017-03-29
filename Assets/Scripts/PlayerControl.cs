using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerControl : MonoBehaviour
{

    private Vector3 startPoint;

    private Vector3 endPoint;

    public TrailRenderer Trail;

	void Update () {

	    if (!GameScene.Instance.Game.GameRunning)
	    {
            return;
        }

	    if (Input.GetMouseButtonDown(0))
	    {
	        startPoint = Input.mousePosition;
	        Debug.Log(startPoint);
	    }

	    if (Input.GetMouseButton(0))
	    {
            Vector3 trailPosition = UIManager.UICamera.ScreenToWorldPoint(Input.mousePosition);
	        trailPosition.z = -10;
            Trail.gameObject.transform.position = trailPosition;
	    }

	    if (Input.GetMouseButtonUp(0))
	    {
	        endPoint = Input.mousePosition;
	        Vector3 start = UIManager.UICamera.ScreenToWorldPoint(startPoint);
	        Vector3 end = UIManager.UICamera.ScreenToWorldPoint(endPoint);
            List<SpriteSlicer2DSliceInfo> spriteSlicer2DSliceInfos = new List<SpriteSlicer2DSliceInfo>();
            SpriteSlicer2D.SliceAllSprites(start,end,true,ref spriteSlicer2DSliceInfos,LayerMask.GetMask(new string[]{"Cut"}));
	        for (int i = 0; i < spriteSlicer2DSliceInfos.Count; i++)
	        {
	            for (int j = 0; j < spriteSlicer2DSliceInfos[i].ChildObjects.Count; j++)
	            {
	                spriteSlicer2DSliceInfos[i].ChildObjects[j].layer = 8;
	            }
	        }
	    }
	}
}
