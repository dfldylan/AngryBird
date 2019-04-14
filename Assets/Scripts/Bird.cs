using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bird : MonoBehaviour {

	public float maxDis = 3;
	private bool isClick = false;
	protected Rigidbody2D rb;
    public GameObject boom;
    [HideInInspector]
    public SpringJoint2D sj;
    protected TestMyTrail trail;
    [HideInInspector]
    public bool controlable;
    private bool Skillenable;
    public float smooth = 3;

    public LineRenderer left;
	public LineRenderer right;
	public Transform rightPos;
	public Transform leftPos;

    public AudioClip select;
    public AudioClip fly;

    public Sprite hurt;
    protected SpriteRenderer render;

	private void Awake(){
		sj = GetComponent<SpringJoint2D> ();
		rb = GetComponent<Rigidbody2D> ();
        trail = GetComponent<TestMyTrail>();
        controlable = false;
        render = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Skillenable = false;
    }

    private void OnMouseDown(){
        if(controlable==true){
            AudioPlay(select);
            isClick = true;
            rb.isKinematic = true;
            left.enabled = true;
            right.enabled = true;
        }
    }

	private void OnMouseUp(){
        if(controlable==true){
            isClick = false;
            rb.isKinematic = false;
            Invoke("Fly", 0.1f);
            left.enabled = false;
            right.enabled = false;
            controlable = false;
        }
    }

    private void Update(){
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (isClick)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(0, 0, Camera.main.transform.position.z);
            if (Vector3.Distance(pos, rightPos.position) > maxDis)
            {
                Vector3 pos_normal = (pos - rightPos.position).normalized;
                pos = pos_normal * maxDis + rightPos.position;
            }
            //rb.MovePosition (pos);
            transform.position = pos;
            Line();
        }

        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
                                                    new Vector3(Mathf.Clamp(posX, 0, 11), Camera.main.transform.position.y, Camera.main.transform.position.z),
                                                    smooth * Time.deltaTime);
        if(Skillenable == true){
            if(Input.GetMouseButtonDown(0)){
                ShowSkill();
                Skillenable = false;
            }
        }

    }

    private void Fly(){
        AudioPlay(fly);
		sj.enabled = false;
        trail.StartTrail();
        Invoke("Dead", 5);
        Skillenable = true;
    }

    private void Line(){
		right.SetPosition (0, rightPos.position);
		right.SetPosition (1, transform.position);
		left.SetPosition (0, leftPos.position);
		left.SetPosition (1, transform.position);
	}

    protected virtual void Dead(){
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instant.birds.Remove(this);
        GameManager._instant.Next();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(controlable == false){
            trail.ClearTrail();
        }
        Skillenable = false;
    }

    private void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    public virtual void ShowSkill()
    {

    }

    public void Hurt()
    {
        render.sprite = hurt;
    }
}
