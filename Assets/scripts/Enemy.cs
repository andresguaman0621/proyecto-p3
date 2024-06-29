using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float delay = 3.0f; // Tiempo de espera en segundos
    private Animator animator;
    private bool isTouched = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StartAnimationAfterDelay());
    }

    IEnumerator StartAnimationAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("Jogging", true); // Asegúrate de que el parámetro en el Animator se llame "Jogging"
    }

    void Update()
    {
        CheckTouchInput();
    }

    void CheckTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform == transform)
                    {
                        isTouched = true;
                        Destroy(gameObject);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    isTouched = true;
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "sensor")
        {
            Debug.Log("SE MURIO");
            Destroy(gameObject);
        }
    }


}
