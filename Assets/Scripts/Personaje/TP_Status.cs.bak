using UnityEngine;
using System.Collections;

public class TP_Status : MonoBehaviour {

    //ATTRIBUTES

    //PUBLIC
    public static TP_Status Instance;



    //PRIVATE
	private bool isSinking;
    private int vida;
    private bool isDead;
    private bool isJumping;
    private bool isRejumping;
    private bool isTargetting;

    void Awake()
    {
        Instance = this;
        isJumping = isRejumping = false;
    }

	// Use this for initialization
	void Start () {
        vida = 100;
        isDead = false;
		isSinking = false;
	}

    public int GetVida(){ return vida; }

    public void SetVida(int num)
    {
        if (num == 0) isDead = true;
        else if (num <= 100) vida = num;
    }

    public void AddVida(int num)
    {
        if (vida + num > 100) vida = 100;
        else vida += num;
    }

    public void SubsVida(int num)
    {
        if (vida - num > 0) {
				vida -= num;
				isDead = false;
		} else {
				vida = 0;
				isDead = true;
				StartSinking ();
		}
    }

    public bool IsJumping() { return isJumping; }
	public bool IsDead() { return isDead; }
    public bool IsReJumping() { return isRejumping; }
    public bool IsTargetting() { return isTargetting; }

    public void SetJumping(bool value)
    {
        isJumping = value;
    }

    public void SetReJumping(bool value)
    {
        isRejumping = value;
    }

    public void SetTargetting(bool value)
    {
        isTargetting = value;
    }

	// Update is called once per frame
	void Update () {
		if(isSinking)
		{
			TP_Skills.Instance.player.transform.Translate (-Vector3.up * 0.25f * Time.deltaTime);

		}
	}

	public void StartSinking ()
	{
		isSinking = true;
		TP_Motor.Instance.gravity = -0.3f;
		StartCoroutine( CargarEscena( 0.5f ) );
	}
	
	IEnumerator CargarEscena( float t ) {
		yield return new WaitForSeconds( t );
		Application.LoadLevel(Application.loadedLevel);
	}
}
