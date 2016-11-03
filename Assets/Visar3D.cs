// TODO
// # OK     Renderizar as lifelines
// # OK     Renderizar as mensagens
// # OK     Animar as mensagens de acordo com o Slider
// # OK     Mostrar e ocultar Lifelines de acordo com a exibição das mensagens
// # Botão Animação Automática;



using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Visar3D : MonoBehaviour {

    public GameObject LifelineGO;
    public Slider slider;
    public Button BtPlay;

    private SequenceDiagram sequence;
    private float CurrentValueSlider = 0;
    private bool btplay = true;

	// Use this for initialization
	void Start () {
        AddSequenceDiagram();

        AddAcaoAoSlider();
        SetarValorMaximoDoSlider();

        AddAcaoAoBtPlay();
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
            if(btplay)
            {
                BtPlay.transform.FindChild("Text").GetComponent<Text>().text = "Pause";
                InvokeRepeating("MudarSliderValue", 0f, 1f);
            }
            else
            {
                BtPlay.transform.FindChild("Text").GetComponent<Text>().text = "Play";
                CancelInvoke();
            }

            if(slider.value.Equals(sequence.Methods.Count))
            {
                print("Replay?");
                InvokeRepeating("ResetarSliderValue", 0f, .01f);
            }          
        });
    }

    void MudarSliderValue()
    {
        if (slider.value.Equals(sequence.Methods.Count))
        {
            CancelInvoke();
            BtPlay.transform.FindChild("Text").GetComponent<Text>().text = "Replay";
            btplay = false;  
        }
        else
        {
            slider.value++;
        }
    }

    void ResetarSliderValue()
    {
        print("Resetando!");

        if(slider.value.Equals(0))
        {
            CancelInvoke();
            BtPlay.transform.FindChild("Text").GetComponent<Text>().text = "Play";
            InvokeRepeating("MudarSliderValue", 1f, 1f);
        }
        else
        {
            slider.value--;
        }        
    }
    #endregion
}
