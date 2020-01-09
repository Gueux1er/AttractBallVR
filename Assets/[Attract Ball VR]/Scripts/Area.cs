using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public enum ActiveState
    {
        NON_ACTIVABLE,
        ACTIVABLE_UNDER,
        ACTIVABLE_OVER,
        ACTIVABLE_LINKED,
        ACTIVE
    }
    public enum LoopType
    {
        YOYO,
        LOOP
    }
    [Header("Parameters")]
    public int minMovable;
    public int maxMovable;
    public GameObject objectToTakeInAccount = null;

    [Header("Materials")]
    public Material nonActivableMaterial;
    public Material activableUnderMaterial;
    public Material activableOverMaterial;
    public Material activableLinkedMaterial;
    public Material activeMaterial;
   
    [HideInInspector] public bool isMoving
    {
        get { return isMoving; }
        set
        {
            switch (value)
            {
                case true: loopTween.Play();break;
                case false: loopTween.Pause();break;
            }
        }
    }
    [Header("Moving")]
    public bool startIsMoving;
    public LoopType loopType;
    public float timeLoopInSeconds;
    public List<Vector3> path = new List<Vector3>();
    private Tween loopTween;

    [Header("Sequence")]
    public List<Area> sequenceChildAreas = new List<Area>();
    public GameObject sequenceLink;
    [HideInInspector] public List<Area> sequenceParentAreas = new List<Area>();
    private bool alreadyParentActive = false;

    [Header("Holder")]
    public bool isHolder = true;

    [Header("Sustain")]
    public bool isSustain = false;
    public float timeSustainInSeconds = 3f;
    private Coroutine sustainUnableActive;

    [Header("Delay before active")]
    public float delayBeforeActiveInSeconds;
    private Coroutine launchDelayActive;
    private bool delayLaunch = false;

    [HideInInspector] public bool isLinked = false;
    [HideInInspector] public LinkedArea linkedArea;
    
    

    //BASIC BEHAVIOUR
    private Collider col;
    private Renderer rend;
    private int layerIdMovable;
    private CanvasArea canvasArea;
    

    private ActiveState _activeState;
    [HideInInspector] public ActiveState activeState
    {
        get { return _activeState; }
        set
        {
            _activeState = value;           
            switch (value)
            {
                case ActiveState.NON_ACTIVABLE:
                    rend.material = nonActivableMaterial;
                    break;
                case ActiveState.ACTIVABLE_UNDER:
                    rend.material = activableUnderMaterial;
                    break;
                case ActiveState.ACTIVABLE_OVER:
                    rend.material = activableOverMaterial;
                    break;
                case ActiveState.ACTIVE:
                    if (isLinked)
                        rend.material = activableLinkedMaterial;
                    else
                        rend.material = activeMaterial;
                    break;
                default: break;
            }          
        }
    }

    private int _movableCount;
    public int movableCount
    {
        get { return _movableCount; }
        set
        {
            _movableCount = value;           
            canvasArea.currentValueText.text = _movableCount.ToString();
            if (activeState == ActiveState.NON_ACTIVABLE)
            {
                return;
            }
            if (value >= minMovable && value <= maxMovable && delayLaunch != true)
            {
                if (sustainUnableActive != null)
                    StopCoroutine(sustainUnableActive);
                launchDelayActive = StartCoroutine(LaunchDelayActive());                              
            }
            if (value < minMovable)
            {
                if (activeState == ActiveState.ACTIVE || delayLaunch == true)
                {
                    sustainUnableActive = StartCoroutine(SustainUnableActive(ActiveState.ACTIVABLE_UNDER));
                }
                else
                {
                    activeState = ActiveState.ACTIVABLE_UNDER;
                }
            }
            if(value > maxMovable)
            {
                if (activeState == ActiveState.ACTIVE || delayLaunch == true)
                {
                    sustainUnableActive = StartCoroutine(SustainUnableActive(ActiveState.ACTIVABLE_OVER));
                }
                else
                {
                    activeState = ActiveState.ACTIVABLE_OVER;
                }
            }
        }
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        canvasArea = GetComponent<CanvasArea>();
        canvasArea.minValueText.text = minMovable.ToString();
        canvasArea.maxValueText.text = maxMovable.ToString();

        col = gameObject.GetComponent<Collider>();
        rend = gameObject.GetComponent<Renderer>();
        layerIdMovable = LayerMask.NameToLayer("Movable");
        activeState = ActiveState.ACTIVABLE_UNDER;

        foreach (Area aChildren in sequenceChildAreas)
        {           
            if (aChildren != null)
            {
                aChildren.sequenceParentAreas.Add(this);
                StartCoroutine(SetChildrenLink(aChildren));
                GameObject sLink = Instantiate(sequenceLink);
                sLink.GetComponent<LineLink>().a = this.gameObject;
                sLink.GetComponent<LineLink>().b = aChildren.gameObject;
            }            
        }
    }

    IEnumerator LaunchDelayActive()
    {
        if(delayBeforeActiveInSeconds > 0f)
        {
            delayLaunch = true;
            yield return new WaitForSeconds(delayBeforeActiveInSeconds);
        }
        Debug.Log("before set active");
        activeState = ActiveState.ACTIVE;
        Debug.Log("after set active");
        delayLaunch = false;
        yield break;
    }

    IEnumerator SustainUnableActive(ActiveState state)
    {
        Debug.Log("before stop coroutine");
        StopCoroutine(launchDelayActive);
        Debug.Log("after stop coroutine");
        delayLaunch = false;

        if (isSustain)
            yield return new WaitForSeconds(timeSustainInSeconds);

        activeState = state;
        yield break;
    }

    IEnumerator SetChildrenLink(Area a)
    {
        yield return new WaitForEndOfFrame();
        a.activeState = ActiveState.NON_ACTIVABLE;
        yield break;
    }

    void Update()
    {
        //sequence link
        if (sequenceParentAreas.Count > 0)
        {
            foreach (Area aParent in sequenceParentAreas)
            {
                if (aParent.activeState != ActiveState.ACTIVE)
                {
                    activeState = ActiveState.NON_ACTIVABLE;
                    alreadyParentActive = false;
                    break;
                }
                else if (aParent.activeState == ActiveState.ACTIVE && !alreadyParentActive)
                {
                    alreadyParentActive = true;
                    activeState = ActiveState.ACTIVABLE_UNDER;
                    movableCount = movableCount;
                    break;
                }
            }
        }

        if (isHolder)
        {
            Rigidbody[] rbs;
            Vector3 center = transform.position;
            if (GetRigidbodiesInArea(center, transform.localScale.x, out rbs))
            {
                AddExplosionForce(rbs, -1, transform.localScale.x, center);
            }
        }      
    }

    private void AddExplosionForce(Rigidbody[] input, float value, float radius, Vector3 center)
    {
        if (input.Length == 0.0f)
            return;
        foreach (Rigidbody rb in input)
        {
            rb.AddExplosionForce(value, center + Random.insideUnitSphere*0.6f, radius);
        }
    }

    private bool GetRigidbodiesInArea(Vector3 position, float radius, out Rigidbody[] result)
    {
        int layerId = LayerMask.NameToLayer("Movable");
        int layerMask = 1 << layerId;
        Collider[] cols = Physics.OverlapSphere(position, radius, layerMask);
        result = new Rigidbody[0];
        if (cols.Length == 0.0f)
            return false;
        result = new Rigidbody[cols.Length];
        for (int i = 0; i < cols.Length; ++i)
        {
            result[i] = cols[i].attachedRigidbody;
        }
        return true;
    }


    private void StartMoving()
    {
        loopTween.Pause();
        path.Add(transform.position);
        loopTween = transform.DOPath(path.ToArray(), timeLoopInSeconds).SetEase(Ease.Linear);
        if (loopType == LoopType.YOYO)
        {
            loopTween.SetLoops(-1, DG.Tweening.LoopType.Yoyo);
        }
        else if (loopType == LoopType.LOOP)
        {
            loopTween.SetLoops(-1);
        }
        isMoving = startIsMoving;
    }

    public void Appear()
    {
        gameObject.SetActive(true);
        transform.DOMoveY(transform.position.y + 5, 1f).SetEase(Ease.InOutSine).From().OnComplete(StartMoving);
        Debug.Log(gameObject.name + " appear");
    }
    public void Disappear()
    {
        transform.DOMoveY(transform.position.y - 5, 1f).SetEase(Ease.InOutSine).OnComplete(() => { gameObject.SetActive(false); });
        Debug.Log(gameObject.name + " disappear");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerIdMovable)
        {
            movableCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layerIdMovable)
        {
            movableCount--;
        }
    }
}
