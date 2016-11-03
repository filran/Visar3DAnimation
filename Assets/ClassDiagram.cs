using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClassDiagram : MonoBehaviour {

    #region PRIVATE VARS
    //origin    destination (Relacionamento entre as classes)
    private Dictionary<LineRenderer, Dictionary<GameObject, GameObject>> LineRenderes = new Dictionary<LineRenderer, Dictionary<GameObject, GameObject>>();
    private GameObject ClassGO; //Prefab
    private Material LineMaterial;
    #endregion

    #region PUBLIC VARS
    public Dictionary<Class, GameObject> Classes = new Dictionary<Class, GameObject>(); //key:class value:class like gameobject
    #endregion

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    #region PRIVATE METHODS
    void ConstruirObjetos()
    {
        Class c1 = new Class("08", "Lifeline Um");
        Class c2 = new Class("09", "Lifeline Dois");
        Class c3 = new Class("10", "Lifeline Três");

        GameObject c1GO = (GameObject)Instantiate(ClassGO, new Vector3(0, 4, 2), Quaternion.identity);
        GameObject c2GO = (GameObject)Instantiate(ClassGO, new Vector3(4, 3, 2), Quaternion.identity);
        GameObject c3GO = (GameObject)Instantiate(ClassGO, new Vector3(3, 0, 2), Quaternion.identity);

        c1GO.AddComponent<AnimateLifeline>();
        c2GO.AddComponent<AnimateLifeline>();
        c3GO.AddComponent<AnimateLifeline>();

        Classes.Add(c1, c1GO);
        Classes.Add(c2, c2GO);
        Classes.Add(c3, c3GO);

        c1.Relationship.Add("um" , c2);
        c1.Relationship.Add("dois", c3);
    }

    void ConstruirRelacionamentoEntreAsClasses()
    {
        foreach(KeyValuePair<Class,GameObject> c in Classes)
        {
            foreach (KeyValuePair<string, Class> r in c.Key.Relationship)
            {
                foreach(KeyValuePair<Class,GameObject> cc in Classes)
                {
                    if(r.Value.Equals(cc.Key))
                    {
                        GameObject lineGO = new GameObject("line");
                        lineGO.transform.parent = c.Value.transform;

                        LineRenderer lineRender = lineGO.AddComponent<LineRenderer>();
                        lineRender.SetWidth(.1f, .1f);
                        lineRender.SetPosition(0, c.Value.transform.position);
                        lineRender.SetPosition(1, cc.Value.transform.position);
                        lineRender.gameObject.GetComponent<Renderer>().material = LineMaterial;

                        Dictionary<GameObject, GameObject> pair = new Dictionary<GameObject, GameObject>();
                        pair.Add(c.Value,cc.Value);
                        LineRenderes.Add(lineRender,pair);
                    }
                }
            }
        }
    }

    void AplicarAplhaZeroEmTudo()
    {
        foreach (KeyValuePair<LineRenderer, Dictionary<GameObject, GameObject>> line in LineRenderes)
        {
            //Lines
            Material lineM = line.Key.GetComponent<Renderer>().material;
            lineM.color = new Color(lineM.color.r, lineM.color.r, lineM.color.b, 0f);
        }

        foreach(KeyValuePair<Class,GameObject> c in Classes)
        {
            //Cube
            Material cMat = c.Value.transform.FindChild("Cube").GetComponent<Renderer>().material;
            cMat.color = new Color(cMat.color.r, cMat.color.g, cMat.color.b, 0);

            //Text
            c.Value.transform.FindChild("Text").gameObject.SetActive(false);
        }
    }
    #endregion

    #region PUBLIC METHODS
    public void renderClassDiagram(GameObject classgo, Material linematerial)
    {
        this.ClassGO = classgo;
        this.LineMaterial = linematerial;

        ConstruirObjetos();
        ConstruirRelacionamentoEntreAsClasses();
        AplicarAplhaZeroEmTudo();
    }
    #endregion
}
