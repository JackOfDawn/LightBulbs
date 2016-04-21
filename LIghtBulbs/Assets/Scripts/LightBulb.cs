using UnityEngine;
using System.Collections;

public class LightBulb : MonoBehaviour {

    [SerializeField]
    Material mOnMaterial;

    [SerializeField]
    Material mOffMaterial;

    Renderer mRenderer;
    
	// Use this for initialization
	void Start () 
    {
        mRenderer = GetComponent<Renderer>();
	}

    void TurnOn()
    {
        if(mOnMaterial)
            mRenderer.material = mOnMaterial;
    }

    void TurnOff()
    {
        if(mOffMaterial)
            mRenderer.material = mOffMaterial;
    }
}
