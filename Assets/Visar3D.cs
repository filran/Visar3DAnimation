// TODO
// # OK     Renderizar as lifelines
// # OK     Renderizar as mensagens
// # OK     Animar as mensagens de acordo com o Slider
// # OK     Mostrar e ocultar Lifelines de acordo com a exibição das mensagens
// # OK     Botão Animação Automática;
// # Colocar os botões Avançar e Voltar



using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Visar3D : MonoBehaviour {

    public GameObject LifelineGO;
    public Slider slider;
    public Button BtPlay;
    public Button BtNext;
    public Button BtPrevious;

    private SequenceDiagram sequence;
    private float CurrentValueSlider = 0;
    private bool btplay = true;

	// Use this for initialization
	void Start () {
        AddSequenceDiagram();

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
}
