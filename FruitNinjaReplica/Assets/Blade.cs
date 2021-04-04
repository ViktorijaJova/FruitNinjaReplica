using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public GameObject bladeTRailPrefab;
    public float minCuttingVelocity = 0.001f;
    Vector2 previousPosition;
    bool isCutting = false;
    Camera cam;
    Rigidbody2D rb;
    GameObject currentbladetrail;
    CircleCollider2D circleCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        circleCollider = GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCutting();

         }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCutting();
        }
        if (isCutting)
        {
            UpdateCut();
        }
    }

    void UpdateCut()
    {
        Vector2 newPosition = cam.ScreenToWorldPoint(Input.mousePosition);
        rb.position = newPosition;

        float velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;

        if (velocity > minCuttingVelocity)
        {
            circleCollider.enabled = true;
        }

        else
        {
            circleCollider.enabled = false;
        }
        previousPosition = newPosition;
    }

    void StartCutting()
    {
        isCutting = true;
     currentbladetrail  =
            Instantiate(bladeTRailPrefab, transform);
        previousPosition = cam.ScreenToWorldPoint(Input.mousePosition); 
        
        circleCollider.enabled = false;
    }



    void StopCutting()
    {
        isCutting = false;
        currentbladetrail.transform.SetParent(null);
        Destroy(currentbladetrail, 2f);
        circleCollider.enabled = false;
    
    }
}
