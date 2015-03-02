﻿using UnityEngine;
using System.Collections;

public enum Skills { Follow = 1, Libre, Orbit, Dios, Puntos, Cinema, Targetting }

public class TP_Camera : MonoBehaviour
{

    public static TP_Camera Instance;
    public Transform[] points = new Transform[5];
    private int currentPointIndex;
    private Transform currentPoint;

    public Transform targettingPoint;
    public float targettingSmooth;
    public float rotateSmooth;

    private bool cameraPosChanged;
    public GameObject objetivo;
    public float velX, velY;
    public float velOrbitX;
    public float smoothX, smoothY;
    float angleRotated = 0;

    float x, y;
    Vector3 offset;

    public float distancia;
    float distanciaMin, distanciaMax;

    public bool godMode;
    public Skills modoCamara = Skills.Follow;

    void Awake()
    {
        Instance = this;
    }


    // Use this for initialization
    void Start()
    {
        x = transform.eulerAngles.x;
        godMode = false;
        cameraPosChanged = false;
    }

    void Update()
    {
        if (modoCamara != Skills.Dios) godMode = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        switch (modoCamara)
        {
            case Skills.Follow:

                x += Input.GetAxis("Horizontal") * velX * distancia * Time.deltaTime;
                //y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                //y = ClampAngle(y, yMinLimit, yMaxLimit);
                y = 15f; // cambio manual de la inclinación

                Quaternion rotation = Quaternion.Euler(y, x, 0);

                //distancia = Mathf.Clamp(distancia - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

                Vector3 negDistance = new Vector3(0.0f, 0.0f, -distancia);
                Vector3 position = rotation * negDistance + objetivo.transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smoothX * Time.deltaTime);
                transform.position = Vector3.Slerp(transform.position, position, 5 * Time.deltaTime);

                //TESTING
                offset = Camera.main.transform.position - objetivo.transform.position;
                break;

            case Skills.Libre:

                //y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                //y = ClampAngle(y, yMinLimit, yMaxLimit);
                y = 30f; // cambio manual de la inclinación

                rotation = Quaternion.Euler(y, x, 0);

                //distancia = Mathf.Clamp(distancia - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

                negDistance = new Vector3(0.0f, 0.0f, -distancia);
                position = rotation * negDistance + objetivo.transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smoothX * Time.deltaTime);
                transform.position = Vector3.Slerp(transform.position, position, 5 * Time.deltaTime);

                break;

            case Skills.Orbit:

                x += Input.GetAxis("Mouse X") * velOrbitX * distancia * Time.deltaTime;
                //y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                //y = ClampAngle(y, yMinLimit, yMaxLimit);
                y = 30f; // cambio manual de la inclinación

                rotation = Quaternion.Euler(y, x, 0);

                //distancia = Mathf.Clamp(distancia - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

                negDistance = new Vector3(0.0f, 0.0f, -distancia);
                position = rotation * negDistance + objetivo.transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, smoothX * Time.deltaTime);
                transform.position = Vector3.Slerp(transform.position, position, 5 * Time.deltaTime);
                break;

            case Skills.Dios:

                godMode = true;
                Vector3 angles = transform.localEulerAngles;
                float temp = angles.x;
                angles.x = 0f;
                transform.rotation = Quaternion.Euler(angles);
                transform.Translate(Input.GetAxisRaw("Horizontal") * 10f * Time.deltaTime, 0f, Input.GetAxisRaw("Vertical") * 10f * Time.deltaTime);
                angles.x += temp;

                angleRotated += Input.GetAxisRaw("Mouse Y") * 90f * Time.deltaTime;
                angleRotated = Mathf.Clamp(angleRotated, -60, 30);
                Debug.Log("Angle Rotated: " + angleRotated);

                if (angleRotated < 30 && angleRotated > -60) angles.x += Input.GetAxisRaw("Mouse Y") * 90f * Time.deltaTime;
                angles.y += Input.GetAxisRaw("Mouse X") * 60f * Time.deltaTime;
                angles.z = 0f;

                transform.rotation = Quaternion.Euler(angles);

                break;

            case Skills.Puntos:
                //Punto de visualización en el mapa
                if (Input.GetKeyUp(KeyCode.KeypadPlus))
                {
                    cameraPosChanged = true;
                    if (currentPointIndex == 4) currentPointIndex = 0;
                    else ++currentPointIndex;
                }
                else if (Input.GetKeyUp(KeyCode.KeypadMinus))
                {
                    cameraPosChanged = true;
                    if (currentPointIndex == 0) currentPointIndex = 4;
                    else --currentPointIndex;
                }

                //actualizo posición de cámara
                switch (cameraPosChanged)
                {
                    case true:
                        cameraPosChanged = false;
                        this.transform.position = points[currentPointIndex].position;
                        this.transform.rotation = points[currentPointIndex].rotation;
                        break;
                    case false: break;
                }
                break;

            case Skills.Cinema:

                //codigo de movimiento de cámara aqui
                break;

            case Skills.Targetting:

                transform.position = Vector3.Slerp(transform.position, targettingPoint.position, targettingSmooth * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, targettingPoint.rotation, rotateSmooth * Time.deltaTime);
                break;

        }
    }
}
