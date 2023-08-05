using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public ParticleState particleState;
    public Transform lerpTransform;
    private void Update()
    {
        if (lerpTransform != null)
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(lerpTransform.position.x, lerpTransform.position.y + 1, lerpTransform.position.z), 2f*Time.deltaTime);
        else
            Destroy(this.gameObject);
    }
}
