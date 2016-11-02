// TODO
// # OK     Renderizar as lifelines
// # OK     Renderizar as mensagens
// # OK     Animar as mensagens de acordo com o Slider
// # Mostrar e ocultar Lifelines de acordo com a exibição das mensagens



using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Visar3D : MonoBehaviour {

    public GameObject LifelineGO;
    public Slider slider;

    private SequenceDiagram sequence;
    private float CurrentValueSlider = 0;

	// Use this for initialization
	void Start () {
        AddSequenceDiagram();

        AddAcaoAoSlider();

        SetarValorMaximoDoSlider();
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

    void AddAcaoAoSlider()
    {
        slider.onValueChanged.AddListener(delegate
        {
            if (DescobrirDirecaoDoSlider(slider.value).Equals("left"))
            {
                sequence.AnimarMetodo(CurrentValueSlider , "left");
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
}
