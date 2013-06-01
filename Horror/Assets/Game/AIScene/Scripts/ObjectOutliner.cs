using UnityEngine;
using System.Collections;

public class ObjectOutliner : MonoBehaviour {

    public GameObject target;

    public Shader shader1;
    public Shader shader2;
    public float outlineSize = 0.01f;
    public float distanceToAct = 2;
    public Color outlineColor = Color.white;
    private bool alreadyNear = false;

	// Use this for initialization
	void Start () {

        shader1 = Shader.Find("Diffuse");
        shader2 = Shader.Find("Outlined/Silhouetted Diffuse");
	
	}
	
	// Update is called once per frame
	void Update () {

        float distance = Vector3.Distance(this.transform.position, target.transform.position);

        if (distance <= distanceToAct)
        {
            if (!alreadyNear)
            {
                alreadyNear = true;
                this.renderer.material.shader = shader2;
                this.renderer.material.SetFloat("_Outline", outlineSize);
                this.renderer.material.SetColor("_OutlineColor", outlineColor);
            }
        }
        else
        {
            alreadyNear = false;
            this.renderer.material.shader = shader1;
        }

	}
}
