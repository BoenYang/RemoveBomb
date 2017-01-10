using UnityEngine;
using System.Collections;

public class PlayBackground : MonoBehaviour
{

    public int BgIndex = 0;

    public bool DestroyOnDisable = true;

    public bool InterruptAll;

	// Use this for initialization
	void OnEnable () {
	    if (!AudioManager.ins.IsBgAudioPlaying || InterruptAll)
        {
            AudioManager.ins.PlayBackground(BgIndex, true);
        }
	}

    private void OnDisable()
    {
        if (DestroyOnDisable && AudioManager.ins != null)
        {
            AudioManager.ins.StopBackground();
        }
    }

}
