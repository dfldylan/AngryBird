using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird {

    public List<Pig> list = new List<Pig>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            list.Add(collision.gameObject.GetComponent<Pig>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            list.Remove(collision.gameObject.GetComponent<Pig>());
        }
    }

    public override void ShowSkill()
    {
        base.ShowSkill();
        if (list != null && list.Count > 0){
            for (int i = 0; i < list.Count; i++){
                list[i].Dead();
            }
        }
        Clear();
    }

    private void Clear(){
        rb.velocity = Vector3.zero;
        render.enabled = false;
        Instantiate(boom, transform.position, Quaternion.identity);
        GetComponent<Collider2D>().enabled = false;
        trail.ClearTrail();
    }

    protected override void Dead()
    {
        Destroy(gameObject);
        GameManager._instant.birds.Remove(this);
        GameManager._instant.Next();
    }
}
