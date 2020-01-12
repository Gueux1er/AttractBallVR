using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailHandRadius : MonoBehaviour
{
    public GameObject particleTrail;
    public GameObject centerParticleTrail;
    IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        float distanceCenterAttraction = GetComponent<AttractRepulse>().distanceCenterAttraction;
        Transform leftHand = GetComponent<AttractRepulse>().leftHand;
        Transform rightHand = GetComponent<AttractRepulse>().rightHand;

        Instantiate(particleTrail, leftHand.position + (leftHand.forward * distanceCenterAttraction), Quaternion.identity, leftHand);
        Instantiate(particleTrail, rightHand.position + (rightHand.forward * distanceCenterAttraction), Quaternion.identity, rightHand);

        Instantiate(centerParticleTrail, leftHand.position + (leftHand.forward * distanceCenterAttraction), Quaternion.identity, leftHand);
        Instantiate(centerParticleTrail, rightHand.position + (rightHand.forward * distanceCenterAttraction), Quaternion.identity, rightHand);
    }

    void Update()
    {

    }
}
