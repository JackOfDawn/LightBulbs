using UnityEngine;
using System.Collections;

public class LightBulb : MonoBehaviour {

    [SerializeField]
    Material mOnMaterial;

    [SerializeField]
    Material mOffMaterial;

    [SerializeField]
    Material mSelectedMat;

    Renderer mRenderer;

    public Vector2 mPosition {get; private set;}
    
	// Use this for initialization
    public void Select()
    {
        if (mSelectedMat)
            mRenderer.material = mSelectedMat;
    }

    public void Init(Vector2 pos)
    {
        mRenderer = GetComponent<Renderer>();
        mPosition = pos;
        TurnOff();
    }

    public void SetState(bool state)
    {
        if (state)
            TurnOn();
        else
            TurnOff();
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
