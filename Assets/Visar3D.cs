// TODO
// # OK     Renderizar as lifelines
// # OK     Renderizar as mensagens
// # OK     Animar as mensagens de acordo com o Slider
// # OK     Mostrar e ocultar Lifelines de acordo com a exibição das mensagens
// # OK     Botão Animação Automática;
// # OK     Colocar os botões Avançar e Voltar

//Diagrama de Classes
// # Setar o Aplha das Classes, Relacionamento ente as Classes e Relacionamento entre Classes e Lifelines

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Visar3D : MonoBehaviour {

    public GameObject LifelineGO;
    public GameObject ClassGO;
    public Material LineMaterial;
    public Material RelationshipMaterial;
    public Slider slider;
    public Button BtPlay;
    public Button BtNext;
    public Button BtPrevious;

    private SequenceDiagram sequence;
    private ClassDiagram classdiagram;
    private float CurrentValueSlider = 0;
    private bool btplay = true;

    //Relacionamento entre                        classes e lifelines
    private Dictionary<LineRenderer, Dictionary<GameObject, GameObject>> LineRenderes;

	// Use this for initialization
	void Start () {
        LineRenderes = new Dictionary<LineRenderer, Dictionary<GameObject, GameObject>>();

        AddSequenceDiagram();
        AddClassDiagram();
        CriarRelacionamentoEntreClassesELifelines();

        AddAcaoAoSlider();
        SetarValorMaximoDoSlider();

        AddAcaoAoBtPlay();

        AddAcaoAoBtNext();
        AddAcaoAoBtPrevious();
	}
	
	// Update is called once per frame
	void Update () {

    }

    void AddSequenceDiagram()
    {
        sequence = this.gameObject.AddComponent<SequenceDiagram>();
        sequence.LifelineGO = LifelineGO;
        sequence.renderSequenceDiagram();
    }

    void AddClassDiagram()
    {
        classdiagram = this.gameObject.AddComponent<ClassDiagram>();
        classdiagram.renderClassDiagram(ClassGO, RelationshipMaterial);
    }

    #region Acoes para o Slider

    void AddAcaoAoSlider()
    {
        slider.onValueChanged.AddListener(delegate
        {
            if (DescobrirDirecaoDoSlider(slider.value).Equals("left"))
            {
                sequence.AnimarMetodo(CurrentValueSlider , "left");
                BtPlay.transform.FindChild("Text").GetComponent<Text>().text = "Play";
                btplay = true;
            }

            if (DescobrirDirecaoDoSlider(slider.value).Equals("right"))
            {
                sequence.AnimarMetodo(slider.value, "right");
            }

            if(slider.value.Equals(0))
            {
                sequence.AplicarAplhaZeroAPrimeiraLifeline();
            }

            CurrentValueSlider = slider.value;
        });
    }

    void SetarValorMaximoDoSlider()
    {
        slider.maxValue = sequence.Methods.Count;
    }

    string DescobrirDirecaoDoSlider(float value)
    {
        string r = "";

        if (value > CurrentValueSlider)
        {
            r = "right";
        }

        if (value < CurrentValueSlider)
        {
            r = "left";
        }

        return r;
    }

    #endregion

    #region Acoes para o Botão Play
    void AddAcaoAoBtPlay()
    {
        BtPlay.onClick.AddListener(delegate {
            if (slider.value.Equals(sequence.Methods.Count))
            {
                InvokeRepeating("ResetarSliderValue", 0f, .01f);
            }
            else
            {
                if (btplay)
                {
                    BtPlay.transform.FindChild("Text").GetComponent<Text>().text = "Pause";
                    btplay = false;
                    InvokeRepeating("MudarSliderValue", 0f, 1f);
                }
                else
                {
                    BtPlay.transform.FindChild("Text").GetComponent<Text>().text = "Play";
                    btplay = true;
                    CancelInvoke();
                }
            }
        });
    }

    void MudarSliderValue()
    {
        if (slider.value.Equals(sequence.Methods.Count))
        {
            CancelInvoke();
            BtPlay.transform.FindChild("Text").GetComponent<Text>().text = "Replay";
        }
        else
        {
            slider.value++;
        }
    }

    void ResetarSliderValue()
    {
        if(slider.value.Equals(0))
        {
            CancelInvoke();
            btplay = false;
            BtPlay.transform.FindChild("Text").GetComponent<Text>().text = "Pause";
            InvokeRepeating("MudarSliderValue", 1f, 1f);
        }
        else
        {
            slider.value--;
        }        
    }
    #endregion


    void AddAcaoAoBtNext()
    {
        BtNext.onClick.AddListener(delegate {
            slider.value++;
        });
    }

    void AddAcaoAoBtPrevious()
    {
        BtPrevious.onClick.AddListener(delegate
        {
            slider.value--;
        });
    }

    void CriarRelacionamentoEntreClassesELifelines()
    {
        foreach(KeyValuePair<Lifeline,GameObject> l in sequence.Lifelines)
        {
            foreach (KeyValuePair<Class, GameObject> c in classdiagram.Classes)
            {
                if(l.Key.Name.Equals(c.Key.Name))
                {
                    GameObject LineGO = new GameObject("line");
                    LineGO.transform.parent = l.Value.transform;
                    
                    LineRenderer LineRender = LineGO.AddComponent<LineRenderer>();
                    LineRender.SetWidth(.1f,.1f);
                    LineRender.SetPosition(0, l.Value.transform.position);
                    LineRender.SetPosition(1, c.Value.transform.position);

                    LineRender.GetComponent<Renderer>().material = LineMaterial;
                    LineRender.gameObject.AddComponent<AnimateLine>();

                    Dictionary<GameObject, GameObject> pair = new Dictionary<GameObject, GameObject>();
                    pair.Add(c.Value,l.Value);
                    LineRenderes.Add(LineRender, pair);
                }
            }
        }

        sequence.RelationshipClassesAndLifelines = LineRenderes;
    }
}
