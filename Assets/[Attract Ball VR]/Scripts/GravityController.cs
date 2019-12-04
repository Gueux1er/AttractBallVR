//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GravityController : MonoBehaviour
//{
//    public enum eTypeRangeReverseGravity { everything, range }
//    public eTypeRangeReverseGravity typeRangeReverseGravity;

//    public float rangeReverseGravity;

//    [Header("Input")]
//    public SteamVR_Action_Boolean reverseGravityInput;
//    public SteamVR_Action_Boolean stopGravityInput;
//    public SteamVR_Input_Sources reverseGravityHand;
//    public SteamVR_Input_Sources stopGravityHand;

//    private GameObject[] allMovableObject;
//    private List<Rigidbody> rigidbodies;
//    private List<GravityAgent> gravityAgents;
//    private Vector3 baseGravity;
//    private Transform handTransform;

//    private void Awake()
//    {
//        baseGravity = Physics.gravity;



//        var goArray = Object.FindObjectsOfType(typeof(GameObject)) as GameObject[];
//        var goList = new System.Collections.Generic.List<GameObject>();

//        for (int i = 0; i < goArray.Length; i++)
//        {
//            if (goArray[i].layer == 9)
//            {
//                goList.Add(goArray[i]);
//            }
//        }

//        allMovableObject = goList.ToArray();



//        rigidbodies = new List<Rigidbody>();
//        gravityAgents = new List<GravityAgent>();

//        for (int i = 0; i < allMovableObject.Length; ++i)
//        {
//            rigidbodies.Add(allMovableObject[i].GetComponent<Rigidbody>());
//            rigidbodies[i].gameObject.AddComponent<GravityAgent>();
//            gravityAgents.Add(rigidbodies[i].GetComponent<GravityAgent>());
//        }

//        reverseGravityInput.AddOnStateDownListener(ReverseGravity, reverseGravityHand);
//        stopGravityInput.AddOnStateDownListener(StopGravity, stopGravityHand);

//        handTransform = FindObjectOfType<SteamVR_Behaviour_Pose>().transform;
//    }

//    private void ReverseGravity(SteamVR_Action_Boolean fromInput, SteamVR_Input_Sources fromSource)
//    {
//        // Everything
//        if (typeRangeReverseGravity == eTypeRangeReverseGravity.everything)
//        {
//            baseGravity = -baseGravity;
//            Physics.gravity = baseGravity;
//        }
//        // By range
//        else if (typeRangeReverseGravity == eTypeRangeReverseGravity.range)
//        {
//            for (int i = 0; i < gravityAgents.Count; ++i)
//            {
//                gravityAgents[i].ReverseGravity(Vector3.Distance(rigidbodies[i].transform.position, handTransform.position) <= rangeReverseGravity);
//            }
//        }
//    }

//    private void StopGravity(SteamVR_Action_Boolean fromInput, SteamVR_Input_Sources fromSource)
//    {
//        // Everything
//        if (typeRangeReverseGravity == eTypeRangeReverseGravity.everything)
//        {
//            for (int i = 0; i < rigidbodies.Count; ++i)
//            {
//                rigidbodies[i].useGravity = !rigidbodies[i].useGravity;
//            }
//        }
//        // By range
//        else if (typeRangeReverseGravity == eTypeRangeReverseGravity.range)
//        {
//            for (int i = 0; i < rigidbodies.Count; ++i)
//            {
//                if (Vector3.Distance(rigidbodies[i].transform.position, handTransform.position) <= rangeReverseGravity)
//                {
//                    rigidbodies[i].useGravity = false;
//                }
//                else if (Vector3.Distance(rigidbodies[i].transform.position, handTransform.position) > rangeReverseGravity)
//                {
//                    rigidbodies[i].useGravity = true;
//                }
//            }
//        }
//    }
//}