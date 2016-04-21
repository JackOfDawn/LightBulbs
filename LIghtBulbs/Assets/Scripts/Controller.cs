using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    public class Light
    {
        public bool isOn { get; private set; }

        public Light()
        {
            isOn = false;
        }

        public void SwitchState()
        {
            isOn = !isOn;
        }
    };
    
    public enum SelectionState
    {
        FirstSelection,
        SecondSelection,
        UpdateLights
    };

    public GameObject Bulbs;

    const int numRows = 10;
    const int numColumns = 10;

    public Vector3 topLeft = new Vector3(-5, -5, 0);
    public float offset = 1.0f;

    public Light[,] mBulbStates;
    public LightBulb[,] mModels;

    SelectionState mSelectionState;
    Vector2 firstPoint;
    Vector2 SecondPoint;


	void Start () 
    {
        mSelectionState = SelectionState.FirstSelection;
        mModels = new LightBulb[numRows,numColumns];
        mBulbStates = new Light[numRows,numColumns];
        GenerateLightBulbs();
	}

    void GenerateLightBulbs()
    {
        for(int y = 0; y < numRows; ++y)
            for(int x = 0; x < numColumns; ++x)
            {
                LightBulb newLight = ((GameObject)Instantiate(Bulbs, topLeft + new Vector3(offset * x, offset * y, 0), Quaternion.identity)).GetComponent<LightBulb>();
                newLight.Init(new Vector2(x,y));
                Debug.Log(newLight.mPosition);
                mModels[y, x] = newLight;
                mBulbStates[y, x] = new Light();
            }
    }
	
	// Update is called once per frame
	void Update () 
    {
	     if(Input.GetMouseButtonDown(0))
         {
             Debug.Log("Mouse Is Down");
             var hitInfo = new RaycastHit();
             if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo))
             {
                 if(hitInfo.transform.gameObject.CompareTag("LightBulb"))
                 {
                     switch (mSelectionState)
                     {
                         case SelectionState.FirstSelection:
                             firstPoint = hitInfo.transform.GetComponent<LightBulb>().mPosition;
                             Debug.Log(firstPoint);
                             mSelectionState = SelectionState.SecondSelection;
                             break;
                         case SelectionState.SecondSelection:
                             SecondPoint = hitInfo.transform.GetComponent<LightBulb>().mPosition;
                             Debug.Log(SecondPoint);
                             mSelectionState = SelectionState.UpdateLights;
                             break;
                     }
                 }
             }
         }

         if (mSelectionState == SelectionState.UpdateLights)
             UpdateLights();
	}

    void UpdateLights()
    {
        //find left info
        int leftX = (int)Mathf.Min(firstPoint.x, SecondPoint.x);
        int n = (int)Mathf.Abs(firstPoint.x - SecondPoint.x);
        //find top info
        int topY = (int)Mathf.Min(firstPoint.y, SecondPoint.y);
        int m = (int)Mathf.Abs(firstPoint.y - SecondPoint.y);

        for(int y = 0; y < numRows; ++y)
            for(int x = 0; x < numColumns; ++x)
            {
                if(CheckInRange(x, leftX, n) && CheckInRange(y, topY, m))
                {
                    //The bulb should switch states
                    mBulbStates[y, x].SwitchState();
                    mModels[y,x].SetState(mBulbStates[y,x].isOn);
                }

            }
        mSelectionState = SelectionState.FirstSelection;

    }

    bool CheckInRange(int point, int low, int space)
    {
        return point >= low && point <= low + space;
    }
}
