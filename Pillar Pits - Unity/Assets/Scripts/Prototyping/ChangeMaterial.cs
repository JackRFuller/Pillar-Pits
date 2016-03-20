using UnityEngine;
using System.Collections;

public class ChangeMaterial : MonoBehaviour {

    public Material[] materials;
    private int materialID = 0;

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.M))
        {
            ChangeMaterialOnGeometry();
        }
	
	}

    void ChangeMaterialOnGeometry()
    {
        materialID++;
        if(materialID == materials.Length)
        {
            materialID = 0;
        }

        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).GetComponent<MeshRenderer>())
                transform.GetChild(i).GetComponent<MeshRenderer>().material = materials[materialID];
        }
    }
}
