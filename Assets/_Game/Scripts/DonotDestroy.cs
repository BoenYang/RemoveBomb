using UnityEngine;
using System.Collections;

public class DonotDestroy : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
