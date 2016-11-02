// TODO
// # OK     Renderizar as lifelines
// # OK     renderizar as mensagens
// # criar os botoes de interface com os botoes
// # quando clicar nos botoes, animar as mensagens 



using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Visar3D : MonoBehaviour {

    public GameObject LifelineGO;
    public Slider slider;

    private SequenceDiagram sequence;

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
            sequence.AnimarMetodo(slider.value);
        });
    }

    void SetarValorMaximoDoSlider()
    {
        slider.maxValue = sequence.Methods.Count;
    }
}
