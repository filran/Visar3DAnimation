using UnityEngine;
using System.Collections;

public class AnimateLifeline : MonoBehaviour {

    private float Lerp = 0;
    private Color AlpaOne;
    private Color AlpZero;
    private bool Animate = false;
    private string Direction;
    private GameObject LifelineDestination;

    public float Speed = 2f;
    public Renderer Rend;
    


	// Use this for initialization
	void Start () {
        Rend = this.transform.FindChild("Cube").GetComponent<Renderer>();
        AlpaOne = new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, 1);
        AlpZero = new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, 0);
	}
	
	// Update is called once per frame
	void Update () {
        ExectuarAnimacao();
    }

    #region PRIVATE METHODS

    void ExectuarAnimacao()
    {
        if (Animate)
        {
            Lerp += Speed * Time.deltaTime;

            if (Direction.Equals("right"))
            {
                Rend.material.color = Color.Lerp(new Color(Rend.material.color.r, Rend.material.color.g, Rend.material.color.b, Rend.material.color.a), AlpaOne, Lerp);            

                if (Rend.material.color.a.Equals(1))
                {
                    this.Animate = false;
                }
            }

            if (Direction.Equals("left"))
            {
                Rend.material.color = Color.Lerp(AlpaOne, AlpZero, Lerp);

                if (Rend.material.color.a.Equals(0))
                {
                    this.Animate = false;
                }
            }
        }
        else
        {
            Lerp = 0;
        }
    }


    #endregion

    #region PUBLIC METHODS

    public void Animar(string direction)
    {
        this.Animate = true;
        this.Direction = direction;
    }

    #endregion
}
