//Este script deve ser aplicado ao LineRenderer

using UnityEngine;
using System.Collections;

public class AnimateMethod : MonoBehaviour {

    private LineRenderer Line;
    private float Dist = 0;
    private float Counter = 0;
    private float LineDrawSpeed = 6f;

    public Vector3 Origin;
    public Vector3 Destination;
    private bool Animate = false;
    private string Direction = "";

    // Use this for initialization
    void Start()
    {
        CriarLinerenderer();
    }

    void CriarLinerenderer()
    {
        Line = this.GetComponent<LineRenderer>();
        Line.SetPosition(0, Origin);
        Line.SetWidth(.2f, .2f);

        Line.SetPosition(1,Destination);
    }

    // Update is called once per frame
    void Update()
    {
        ExecutarAnimacao();
    }

    void ExecutarAnimacao()
    {
        if (Animate)
        {
            Dist = Vector3.Distance(Origin, Destination);

            if(Direction.Equals("right"))
            {
                if (Counter < Dist)
                {
                    Counter += .1f / LineDrawSpeed;
                    float x = Mathf.Lerp(0, Dist, Counter);
                    Vector3 pointAlonfLine = x * Vector3.Normalize(Destination - Origin) + Origin;
                    Line.SetPosition(1, pointAlonfLine);
                }
            }

            if (Direction.Equals("left"))
            {
                //if (Counter > 0)
                //{
                //    Counter -= .1f / LineDrawSpeed;
                //    float x = Mathf.Lerp(0, Dist, Counter);
                //    Vector3 pointAlonfLine = x * Vector3.Normalize(Destination);
                //    Line.SetPosition(1, pointAlonfLine);
                //}

                //this.Animate = false;
                //this.gameObject.SetActive(false);
            }
        }
    }

    public void Animar(string direction)
    {
        if (direction.Equals("left"))
        {
            this.Animate = true;
            this.Direction = direction;
        }

        if (direction.Equals("right"))
        {
            this.Animate = true;
            this.Direction = direction;
        }
    }
}
