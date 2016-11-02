using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SequenceDiagram : MonoBehaviour {

    public GameObject LifelineGO;

    public List<Lifeline> ListLifeline = new List<Lifeline>();
    public Dictionary<Lifeline, GameObject> Lifelines = new Dictionary<Lifeline, GameObject>();
    public Dictionary<Method, GameObject> Methods = new Dictionary<Method, GameObject>();

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #region PRIVATE METHODS
    void ConstruirObjetosEmCena()
    {
        Lifeline l1 = new Lifeline("01","Lifeline Um");
        Lifeline l2 = new Lifeline("02", "Lifeline Dois");
        Lifeline l3 = new Lifeline("03", "Lifeline Três");
        Lifeline l4 = new Lifeline("04", "Lifeline Quatro");

        Method m1 = new Method("05", "Message Um");
        Method m2 = new Method("06", "Message Dois");
        Method m3 = new Method("07", "Message Três");

        m1.IdTarget = "02";
        m1.PtStartY = 2f;

        m2.IdTarget = "03";
        m2.PtStartY = 1f;

        m3.IdTarget = "04";
        m3.PtStartY = 0f;

        m1.Seqno = 1;
        m2.Seqno = 2;
        m3.Seqno = 3;

        l1.Methods.Add(m1);
        l2.Methods.Add(m2);
        l3.Methods.Add(m3);

        ListLifeline.Add(l1);
        ListLifeline.Add(l2);
        ListLifeline.Add(l3);
        ListLifeline.Add(l4);
    }

    void renderMethods()
    {
        foreach (KeyValuePair<Lifeline, GameObject> l in Lifelines)
        {
            foreach (Method m in l.Key.Methods)
            {
                GameObject mGO = new GameObject("method_" + m.Name);
                
                LineRenderer line = mGO.AddComponent<LineRenderer>();
                line.gameObject.SetActive(false);
                line.SetWidth(.15f, .15f);
                line.SetPosition(0, new Vector3(l.Value.transform.position.x, m.PtStartY, l.Value.transform.position.z));
                line.SetPosition(1, new Vector3(l.Value.transform.position.x, m.PtStartY, l.Value.transform.position.z));

                //Encontra a Lifeline de destino
                foreach (KeyValuePair<Lifeline, GameObject> ll in Lifelines)
                {
                    if (ll.Key.Id.Equals(m.IdTarget))
                    {
                        //line.SetPosition(1, new Vector3(ll.Value.transform.position.x, m.PtStartY, ll.Value.transform.position.z));

                        //Adiciona a classe responsável por animar a mensagem
                        AnimateMethod animateMethod = line.gameObject.AddComponent<AnimateMethod>();
                        animateMethod.Origin = new Vector3(l.Value.transform.position.x, m.PtStartY, l.Value.transform.position.z);
                        animateMethod.Destination = new Vector3(ll.Value.transform.position.x, m.PtStartY, ll.Value.transform.position.z);
                    }
                }

                Methods.Add(m, mGO);
            }
        }
    }
    #endregion

    #region PUBLIC METHODS
    public void renderSequenceDiagram()
    {
        ConstruirObjetosEmCena();

        int count = 0;

        //Instanciar Lifelines
        foreach(Lifeline l in ListLifeline)
        {
            GameObject lGO = (GameObject)Instantiate(LifelineGO, new Vector3(count, 0, 0), Quaternion.identity);
            lGO.name = l.Name;

            //Aplha Material 0
            Material lGOcolor = lGO.transform.FindChild("Cube").GetComponent<Renderer>().material;
            lGOcolor.color = new Color(lGOcolor.color.r, lGOcolor.color.g, lGOcolor.color.b, 0);

            //Animate Lifenlie
            AnimateLifeline animateLifeline = lGO.AddComponent<AnimateLifeline>();

            Lifelines.Add(l, lGO);            
            count += 2;
        }

        renderMethods();
    }
    
    public void AnimarMetodo(float value , string direction)
    {
        foreach(KeyValuePair<Lifeline , GameObject> l in Lifelines)
        {
            foreach(Method m in l.Key.Methods)
            {
                GameObject mGO = Methods[m];

                if (m.Seqno.Equals((int)value))
                {
                    //Animar Método
                    mGO.SetActive(true);
                    mGO.GetComponent<AnimateMethod>().Animar(direction);

                    //Animar Lifeline
                    l.Value.GetComponent<AnimateLifeline>().Animar(direction);
                }
            }
        }



        //foreach (KeyValuePair<Method, GameObject> m in Methods)
        //{
        //    if (m.Key.Seqno.Equals((int)value))
        //    {
        //        //Animar Método
        //        m.Value.SetActive(true);
        //        m.Value.GetComponent<AnimateMethod>().Animar(direction);

        //        //Animar Lifeline
        //        //Achar a Lifeline que possui esse método
        //        //Ativar a animação da Lifeline de acordo com a direção do Slider
        //    }
        //}
    }
    #endregion
}
