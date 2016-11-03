//Este script deve ser aplicado ao LineRenderer

using UnityEngine;
using System.Collections;

public class AnimateMethod : MonoBehaviour {

    private LineRenderer Line;
    private float Dist = 0;
    private float Counter = 0;
    private float LineDrawSpeed = 2f;
    private GameObject LifelineOrigin;
    private GameObject LifelineDestination;
    private GameObject Classe;

    public Vector3 Origin;
    public Vector3 Destination;
    private bool Animate = false;
    private string Direction = "";

    // Use this for initialization
    void Start()
    {
        Line = this.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        ExecutarAnimacao();
    }

    #region PRIVATE METHODS

    void ExecutarAnimacao()
    {
        if (Animate)
        {
            Dist = Vector3.Distance(Origin, Destination);

            # region Direita e Esquerda
            if (Direction.Equals("right"))
            {
                if (Counter < Dist)
                {
                    Counter += .1f / LineDrawSpeed;
                    
                    //Animation Lifeline
                    LifelineOrigin.GetComponent<AnimateLifeline>().Animar(this.Direction);
                    LifelineDestination.GetComponent<AnimateLifeline>().Animar(this.Direction);

                    //Animation Classe
                    Classe.GetComponent<AnimateClass>().Animar(this.Direction);
                }
            }

            if (Direction.Equals("left"))
            {
                if (Counter > 0)
                {
                    Counter -= .1f / LineDrawSpeed;

                    //Animation Lifeline
                    LifelineDestination.GetComponent<AnimateLifeline>().Animar(this.Direction);

                    //Animation Classe
                    Classe.GetComponent<AnimateClass>().Animar(this.Direction);
                }
            }
            #endregion

            //Animation Method
            float x = Mathf.Lerp(0, Dist, Counter);
            Vector3 pointAlonfLine = x * Vector3.Normalize(Destination - Origin) + Origin;
            Line.SetPosition(1, pointAlonfLine);
        }
    }
    #endregion

    #region PUBLIC METHODS
    public void Animar(string direction, GameObject lifelineOrigin, GameObject lifelineDestination, GameObject classe)
    {
        this.Animate = true;
        this.Direction = direction;
        this.LifelineOrigin = lifelineOrigin;
        this.LifelineDestination = lifelineDestination;
        this.Classe = classe;
    }
    #endregion





    
}
