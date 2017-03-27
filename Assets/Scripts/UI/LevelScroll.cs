using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LevelScroll : MonoBehaviour
{

    private Vector3 startPoint;

    private Vector3 endPoint;

    public Vector3 targetPos;

    public Vector3 startPos;

    public int CurrentPack = 1;

    public float distance;

    public float dir;

    public bool moveEnd;

    private bool click;

    private void Start()
    {
        targetPos = transform.localPosition;
        moveEnd = true;
    }

    void Update ()
	{
	    Ray ray = UIRootController.Ins.UICamera.ScreenPointToRay(Input.mousePosition);
	    bool inArea = Physics.Raycast(ray,float.PositiveInfinity,LayerMask.GetMask(new string[]{"UI"}));
	    if (inArea && moveEnd)
	    {
	        if (Input.GetMouseButtonDown(0))
	        {
	            startPoint = Input.mousePosition;
	            startPos = transform.localPosition;
	            click = true;

	        }

	        if (Input.GetMouseButton(0) && click)
	        {
	            endPoint = Input.mousePosition;
	            distance = Mathf.Abs(endPoint.x - startPoint.x);
                dir = Mathf.Sign(endPoint.x - startPoint.x);
	            transform.localPosition = startPos + new Vector3(distance*2, 0)*dir;
         
	        }

	        if (Input.GetMouseButtonUp(0) && click)
	        {
                if (distance > 50)
                {
                    int tempPack = CurrentPack - (int) dir*1;

                    if (tempPack > 0 && tempPack < 3)
                    {
                        CurrentPack = tempPack;
                        targetPos = startPos + new Vector3(800, 0)*dir;
                    }
                    else
                    {
                        targetPos = startPos;
                    }
                }
	            moveEnd = false;
	            click = false;
                DOTween.To(() => gameObject.transform.localPosition, (x) => gameObject.transform.localPosition = x,
                     targetPos, 0.4f).OnComplete(() =>
                     {
                         moveEnd = true;
                     });
	        }
	    }
	}
}
