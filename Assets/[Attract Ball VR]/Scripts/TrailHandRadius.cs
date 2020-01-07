using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailHandRadius : MonoBehaviour
{
    public GameObject particleTrail;
    public GameObject centerParticleTrail;
    void Start()
    {
        float handRadius = GetComponent<AttractRepulse>().handRadius;
        Transform leftHand = GetComponent<AttractRepulse>().leftHand;
        Transform rightHand = GetComponent<AttractRepulse>().rightHand;

        Instantiate(particleTrail, leftHand.position + (leftHand.forward * handRadius), Quaternion.identity, leftHand);
        Instantiate(particleTrail, rightHand.position + (rightHand.forward * handRadius), Quaternion.identity, rightHand);

        Instantiate(centerParticleTrail, leftHand.position + (leftHand.forward * handRadius), Quaternion.identity, leftHand);
        Instantiate(centerParticleTrail, rightHand.position + (rightHand.forward * handRadius), Quaternion.identity, rightHand);
    }

    void Update()
    {

    }
}
