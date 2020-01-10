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
    [HideInInspector] public bool isActivable = false;


    //BASIC BEHAVIOUR
    private Collider col;
    private Renderer rend;
    private int layerIdMovable;
    private Vector3 startLocalScale;
    

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
                    rend.material = MaterialManager.Instance.nonActivableMaterial;
                    break;
                case ActiveState.ACTIVABLE_UNDER:
                    if (movableCount == 0)
                        rend.material = MaterialManager.Instance.activableUnderMaterialNoMovables;
                    else
                        rend.material = MaterialManager.Instance.activableUnderMaterial;
                    break;
                case ActiveState.ACTIVABLE_OVER:
                    rend.material = MaterialManager.Instance.activableOverMaterial;
                    break;
                case ActiveState.ACTIVE:
                    rend.material = MaterialManager.Instance.activeMaterial;
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
            if (activeState == ActiveState.NON_ACTIVABLE)
            {
                return;
            }
            if (value >= minMovable && value <= maxMovable && delayLaunch != true && activeState != ActiveState.ACTIVE)
            {
                //isActivable = true;
                if (sustainUnableActive != null)
                    StopCoroutine(sustainUnableActive);
                activeState = ActiveState.ACTIVABLE_UNDER;
                //if (linkedArea == null || (linkedArea != null && linkedArea.IsActivable()))
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
                    //isActivable = false;
                    activeState = ActiveState.ACTIVABLE_UNDER;
                }
                //if (linkedArea != null)
                //    linkedArea.PingAreas();
            }
            if(value > maxMovable)
            {
                if (activeState == ActiveState.ACTIVE || delayLaunch == true)
                {
                    sustainUnableActive = StartCoroutine(SustainUnableActive(ActiveState.ACTIVABLE_OVER));
                }
                else
                {
                    //isActivable = false;
                    activeState = ActiveState.ACTIVABLE_OVER;
                }
                //if (linkedArea != null)
                //    linkedArea.PingAreas();
            }
        }
    }

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        startLocalScale = transform.localScale;

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
            if (linkedArea != null)
                linkedArea.PingAreas();
            for (int i = 0; i < 10; i++)
            {
                transform.DOShakePosition(delayBeforeActiveInSeconds / 10, 0.02f + (i*10/100), 100 + (i * 10));
                yield return new WaitForSeconds(delayBeforeActiveInSeconds / 10);
            }          
        }
        transform.DOScale(startLocalScale * 1.1f, 0.1f).OnComplete(() => { transform.DOScale(startLocalScale * 0.8f, 0.1f).OnComplete(() => { transform.DOScale(startLocalScale, 0.1f); }); });
        activeState = ActiveState.ACTIVE;
        delayLaunch = false;
        yield break;
    }

    public IEnumerator SustainUnableActive(ActiveState state)
    {
        if (launchDelayActive != null)
            StopCoroutine(launchDelayActive);
        delayLaunch = false;
        isActivable = false;

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
    }
    public void Disappear()
    {
        transform.DOMoveY(transform.position.y - 5, 1f).SetEase(Ease.InOutSine).OnComplete(() => { gameObject.SetActive(false); });
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerIdMovable)
        {
            movableCount++;

            other.GetComponent<MovableSound>().PlaySoundEnterArea();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layerIdMovable)
        {
            movableCount--;

            other.GetComponent<MovableSound>().PlaySoundExitArea();
        }
    }
}
