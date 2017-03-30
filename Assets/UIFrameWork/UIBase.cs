﻿using UnityEngine;

public class UIBase : MonoBehaviour
{
	[System.NonSerialized]
    public string UIName;

    public virtual void OnInit()
    {

    }

    public virtual void OnBeginOpen()
    {

    }

    public virtual void OnRefresh()
    {

    }

    public virtual void OnEndOpen()
    {

    }

    public virtual void OnBeginClose()
    {

    }

    public virtual void OnEndClose()
    {

    }

    public virtual void OnResume()
    {

    }

    public virtual void OnStop()
    {

    }

    public virtual void ClosePanel()
    {
        UIManager.ClosePanel(UIName);
    }


    protected void DispatchMsg(string msgType, UIMsg msg = null)
    {
        UIManager.DispatchMsg(msgType,msg);
    }

    protected void AddMsgListener(string msgType,UIMsgCallback callBack)
    {
        UIManager.AddListener(msgType,callBack);
    }
}
