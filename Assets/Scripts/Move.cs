using System.Collections;
using UnityEngine;
using TMPro;
using System;

public class Move : MonoBehaviour
{
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float endPoint=0.11f;
    [SerializeField] private float rotationSpeed = 150.0f; // Karakterin dönme hýzý 
    [SerializeField] private float transformForward=0.01f;
    [SerializeField] private float transformDownSpeed=0.01f;
    //private TMP_Text testerText;
    private float transformDown = 0f;
    private Transform referenceObject;
    private float currentRotation = 0;
    private void Start()
    {
        //invoke eklenebilir (Önerin dediði)

        /*testerText = FindObjectOfType<TMP_Text>();
        if (testerText == null)
        {
            Debug.LogError("TMP_Text bulunamadý!");
        }*/


        //startPosition.z = transform.position.z;
        startPosition.y = transform.position.y;
        transform.position = new Vector3(startPosition.x,startPosition.y,startPosition.z);
    }
    void Update()
    {  
        if (currentRotation < 360)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.forward, rotationAmount);
            currentRotation += rotationAmount;
            //testerText.text = transform.position.ToString();
        }
        else
        {
            //testerText.text = transform.position.ToString();
            MoveForward(); 
            currentRotation = 0;
            //testerText.text = transform.position.ToString();
        }

    }
    void MoveForward()
    {
        if (transform.position.x >= endPoint||transform.position.x<=(-endPoint))
        { 
            transformForward = -transformForward;
            transform.position = transform.position + new Vector3(transformForward, 0f, -transformDownSpeed);
            //transform.position = new Vector3(startPosition.x, (transform.position.y), (startPosition.z-transformDown));
            //transformDown += transformDownSpeed;
        }
        else
        { 
            transform.position = transform.position + new Vector3(transformForward, 0f, 0f);
        }
    }
}
