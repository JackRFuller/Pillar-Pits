using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[ExecuteInEditMode]
public class LevelDesignCanvas : MonoBehaviour {

    public Image RulesOfThirdGrid;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.G))
        {
            ToggleGrid();
        }
	
	}
     
    void ToggleGrid()
    {
        if (RulesOfThirdGrid.enabled)
            RulesOfThirdGrid.enabled = false;
        else RulesOfThirdGrid.enabled = true;
    }
}
