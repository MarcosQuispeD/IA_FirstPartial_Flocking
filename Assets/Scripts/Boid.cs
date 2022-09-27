using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Vector3 _velocity;
    public float maxSpeed;
    public float maxForce;

    [Header("Flocking")]
    public float viewRadius;
    //public float separationRadius;
    [Range(0f, 3f)]
    public float separationWeight = 1;
    [Range(0f, 2f)]
    public float alignmentWeight = 1;
    [Range(0f, 2f)]
    public float cohesionWeight = 1;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.AddBoid(this);

        Vector3 randomVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * maxSpeed;
        AddForce(randomVector);
    }

    // Update is called once per frame
    void Update()
    {
        AddForce(Separation() * separationWeight);
        AddForce(Cohesion() * cohesionWeight);
        AddForce(Alignment() * alignmentWeight);
        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity;
        CheckBounds();
    }

    Vector3 Separation()
    {
        Vector3 desired = Vector3.zero;

        foreach (var boid in GameManager.instance.allBoids)
        {
            Vector3 dist = boid.transform.position - transform.position;
            if (dist.magnitude <= viewRadius)
                desired += dist;
        }
        if (desired == Vector3.zero) return desired;
        desired = -desired;

        return CalculateSteering(desired);
    }

    Vector3 Alignment()
    {
        Vector3 desired = Vector3.zero;
        int count = 0;
        foreach (var item in GameManager.instance.allBoids)
        {
            if (item == this) continue;
            if (Vector3.Distance(item.transform.position, transform.position) <= viewRadius)
            {
                desired += item._velocity;
                count++;
            }
        }
        if (count == 0) return desired;
        desired /= count;

        return CalculateSteering(desired);
    }

    Vector3 Cohesion()
    {
        Vector3 desired = Vector3.zero;
        int count = 0;
        foreach (var boid in GameManager.instance.allBoids)
        {
            if (boid == this) continue;
            if (Vector3.Distance(transform.position, boid.transform.position) <= viewRadius)
            {
                desired += boid.transform.position;
                count++;
            }
        }
        if (count == 0) return desired;
        desired /= count;
        desired -= transform.position;

        return CalculateSteering(desired);
    }

    Vector3 CalculateSteering(Vector3 desired)
    {
        return Vector3.ClampMagnitude((desired.normalized * maxSpeed) - _velocity, maxForce);
    }

    void CheckBounds()
    {
        transform.position = GameManager.instance.ApplyBounds(transform.position);
    }

    void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, maxSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        // Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere(transform.position, separationRadius);
    }
}
