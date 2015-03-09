using UnityEditor;
using UnityEngine;
using System.Collections;

public enum SkillTypes { noSkill, tractionBeam, liftingHook, blackHole }

public class TP_Skills : MonoBehaviour {

    //ATTRIBUTES

    //PUBLIC
    public static TP_Skills Instance;
    private GameObject _object;
    private GameObject _beamobject;
    private GameObject _tractorobject;
    private Vector3 _hitpoint;
    private bool _beam = false;
    private bool _tractor = false;
    private float _lateral = 0f;
    private float _horizontal = 0f;

    public float speed = 0.02f;
    public GameObject player;
    public GameObject righthand;
    public GameObject lefthand;
    private SkillTypes enabledSkill = SkillTypes.noSkill; 

    //PRIVATE


    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
		_beam = false;
	}
	
	void FixedUpdate()	
	{
		if (_beam) 
        {
			if (Input.GetKey(KeyCode.Keypad6)) 
			{
				_lateral=_lateral+speed;
			}
			if (Input.GetKey(KeyCode.Keypad4)) 
			{
				_lateral=_lateral-speed;
			}
			if (Input.GetKey(KeyCode.Keypad8)) 
			{
				_horizontal=_horizontal+speed;
			}
			if (Input.GetKey(KeyCode.Keypad5)) 
			{
				_horizontal=_horizontal-speed;
			}

			Intertia script = _beamobject.GetComponent("Intertia") as Intertia;
			script.offset_lateral=_lateral;
			script.offset_horizontal=_horizontal;

			//actualizamos la posicion del rayo
			//GameObject hand = GameObject.Find("Beta:RightHand");
			LightningBolt script2 = righthand.GetComponent("LightningBolt") as LightningBolt;
			script2.target=_beamobject.transform.position;
			//fin actualzacion posicion del rayo

			if (Input.GetMouseButton(0)) 
			{ 
				script.energy=script.energy+20*Time.deltaTime;
			}

			if (Input.GetMouseButtonUp (1)) 
			{ 
				if (script.energy<0) script.energy=0;
				throwobject(script.energy);
			}
		}
	}
	// Update is called once per frame
	void Update () {
	
		switch (enabledSkill)
		{
		case SkillTypes.blackHole:
			if (Input.GetMouseButtonDown (0)) {
				activateblackhole();
			}
			break;
        case SkillTypes.tractionBeam:

			if (!_beam){
				if (Input.GetMouseButtonDown (0)) {
					activatetractorbeam();
					}
			}
			else
			{
				deactivatetractorbeam();
			}
			break;
		
		case SkillTypes.liftingHook:
			if (!_tractor){
				if (Input.GetMouseButtonDown (0)) {
					activateliftingHook();
				}
			}
			else
			{
				deactivateliftingHook();
			}
			break;
		default: break;
		}	
	}

	void OnGUI()
	{
		if (_beam)
        {			
			Intertia script = _beamobject.GetComponent("Intertia") as Intertia;
			string temp=script.energy.ToString();
			GUI.Label (new Rect (10, 10, 150, 20), "Push Force: "+temp);
		}
	}

    public void ActivateSkill(SkillTypes skill)
    {
        enabledSkill = skill;
    }

	private void deactivatetractorbeam()
	{

		Destroy (_beamobject.GetComponent("Intertia"));
		LightningBolt script = righthand.GetComponent("LightningBolt") as LightningBolt;
		script.active=false;
		_beam=false;
		_lateral=0;
		_horizontal=0;
		enabledSkill = SkillTypes.noSkill;
	}

	private void activatetractorbeam()
	{
		//Iluminate (Color.black,"Beamer");
		Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 1000,(1<<LayerMask.NameToLayer("Beamer"))))
		{
			if (hit.collider.gameObject.tag == "Beamer"){
				if(hit.collider.gameObject.GetComponent<Intertia>() == null)
				{
					_horizontal = 1f;
					_lateral = 0.7f;
					_beamobject=hit.collider.gameObject;
					_beamobject.AddComponent ("Intertia");	
					Intertia inertiascript = _beamobject.GetComponent("Intertia") as Intertia;
					inertiascript.player=player;
					//GameObject hand = GameObject.Find("Beta:RightHand");
					LightningBolt script = righthand.GetComponent("LightningBolt") as LightningBolt;
					script.target=hit.collider.gameObject.transform.position;
					script.active=true;
					_beam=true;
				}
			}
		}
		enabledSkill = SkillTypes.noSkill;
	}

	private void activateliftingHook()
	{
		//Iluminate (Color.black,"Tractor");
		Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 1000))
		{
			if (hit.collider.gameObject.tag == "Tractor"){
				if(hit.collider.gameObject.GetComponent<InverseInertia>() == null)
				{
					player.AddComponent("InverseInertia");
					InverseInertia script = player.GetComponent("InverseInertia") as InverseInertia;
					script.hitpoint=hit.point;
					_hitpoint=hit.point;
					_tractorobject=hit.collider.gameObject;
					_tractor=true;
					LightningBolt script2 = lefthand.GetComponent("LightningBolt") as LightningBolt;
					script2.target=hit.point;
					script2.active=true;
				}
			}
		}
		enabledSkill = SkillTypes.noSkill;
	}
	private void deactivateliftingHook()
	{
		_tractor=false;
		InverseInertia script = player.GetComponent("InverseInertia") as InverseInertia;
		Destroy(script);

		LightningBolt script2 = lefthand.GetComponent("LightningBolt") as LightningBolt;
		script2.active=false;

		enabledSkill = SkillTypes.noSkill;
	}

	private void activateblackhole()
	{
		Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 100))
		{
			_object = new GameObject("BlackHole");
			_object.AddComponent ("Blackhole");
			_object.transform.position = hit.point + new Vector3 (0,0.5f,0);
			
		}
		enabledSkill = SkillTypes.noSkill;
	}

	private void throwobject(float energy)
	{
		//GameObject Beta = GameObject.Find("Beta");
		//_beamobject.rigidbody.AddForce(player.transform.forward * energy);
		
		//miramos si el raton en este momento hace hit sobre algun objeto, si es asi lanzamos en esa direccion
		Ray ray =Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 500))
		{

			Vector3 direction=(hit.collider.transform.position-_beamobject.transform.position);
			direction.Normalize();

			if (_beamobject.transform!=hit.transform)
			{
				//Debug.DrawLine (hit.point, _beamobject.transform.position, Color.red);
				_beamobject.rigidbody.AddForce(direction*energy,ForceMode.Impulse);
			}
		}
		else//si no hace hit marcamos una distancia de 100 y apuntamos alli lanzando en esa direccion.
		{
			//Physics.Raycast(ray, out hit, 100);
			_beamobject.rigidbody.AddForce(ray.direction.normalized * energy);
		}
		deactivatetractorbeam();
	}
		
}
	