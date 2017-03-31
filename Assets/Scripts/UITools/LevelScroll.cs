using UnityEngine;
using System.Collections;
using DG.Tweening;

public class LevelScroll : MonoBehaviour
{
    public Transform PageRoot;

    public int PageCount = 2;

    public float MinPageFlipDistance;

    public int PageDistance = 720;

    private Vector3 touchStartPos;

    private Vector3 touchCurPos;

    private Vector3 targetPos;

    private Vector3 startPos;

    private Vector3 originPos;

    //翻页方向
    private float flipDir;

    private bool isMoveEnd;

    private int curPageIndex = 0;

    private bool click;

    private void Start()
    {
        originPos = PageRoot.localPosition;
        isMoveEnd = true;
    }

    void Update ()
	{
		Ray ray = UIManager.UICamera.ScreenPointToRay(Input.mousePosition);
	    bool inArea = Physics.Raycast(ray,float.PositiveInfinity,LayerMask.GetMask(new string[]{"UI"}));
	    if (inArea && isMoveEnd)
	    {
	        if (Input.GetMouseButtonDown(0))        //鼠标按下
	        {
	            touchStartPos = Input.mousePosition;
	            startPos = PageRoot.localPosition;
	            click = true;
	        }

	        if (Input.GetMouseButton(0) && click)   //鼠标移动
	        {
	            touchCurPos = Input.mousePosition;
	            MinPageFlipDistance = Mathf.Abs(touchCurPos.x - touchStartPos.x);
                flipDir = Mathf.Sign(touchCurPos.x - touchStartPos.x);
				PageRoot.localPosition = startPos + new Vector3(MinPageFlipDistance, 0)*flipDir;
            }

	        if (Input.GetMouseButtonUp(0) && click) //鼠标抬起
	        {
                if (MinPageFlipDistance > 50)
                {
                    int movePageIndex = curPageIndex - (int) flipDir;
                    if (movePageIndex >= 1 && movePageIndex <= PageCount)  
                    {
                        curPageIndex = movePageIndex;
                        targetPos = startPos + new Vector3(PageDistance, 0)*flipDir;
                    }
                    else
                    {
                        targetPos = startPos;
                    }
                }
	            click = false;

                isMoveEnd = false;
                PageRoot.DOLocalMove(targetPos, 0.4f).OnComplete(() =>
	            {
	                isMoveEnd = true;
	            });
	        }
	    }
	}

    public void SetShowPage(int pageIndex,bool useAnimation = false)
    {
        if (curPageIndex == pageIndex)
        {
            return;
        }

        curPageIndex = pageIndex;
        targetPos = originPos + (pageIndex - 1) * new Vector3(PageDistance, 0);
        if (useAnimation)
        {
            isMoveEnd = false;
            PageRoot.DOLocalMove(targetPos, 0.4f).OnComplete(() =>
            {
                isMoveEnd = true;
            });
        }
        else
        {
            PageRoot.transform.GetComponent<RectTransform>().anchoredPosition = targetPos;
        }
    }
}
