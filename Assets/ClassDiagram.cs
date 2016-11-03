using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClassDiagram : MonoBehaviour {

    #region PRIVATE VARS
    //origin    destination (Relacionamento entre as classes)
    private Dictionary<LineRenderer, Dictionary<GameObject, GameObject>> LineRenderes = new Dictionary<LineRenderer, Dictionary<GameObject, GameObject>>();
    #endregion

    #region PUBLIC VARS
    public Dictionary<Class, GameObject> Classes = new Dictionary<Class, GameObject>(); //key:class value:class like gameobject
    public GameObject ClassGO; //Prefab
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

        Classes.Add(c1, c1GO);
        Classes.Add(c2, c2GO);
        Classes.Add(c3, c3GO);

        c1.Relationship.Add("um" , c2);
        c1.Relationship.Add("dois", c3);

        ConstruirRelacionamentoEntreAsClasses();
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
                    }
                }
            }
        }
    }
    #endregion

    #region PUBLIC METHODS
    public void renderClassDiagram()
    {
        ConstruirObjetos();
        ConstruirRelacionamentoEntreAsClasses();
    }
    #endregion
}
