using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBreak : MonoBehaviour
{
    [SerializeField] List<GameObject> BlockList;
    Animator animator;
    [SerializeField] Transform HeroPos;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            BlockList.Add(child.gameObject);
            animator = child.GetComponent<Animator>();
            animator.speed = 0;
        }

        if (GameObject.Find("Hero"))
        {
            HeroPos = GameObject.Find("Hero").GetComponent<Transform>().transform;
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if ((HeroPos.position.x > BlockList[0].transform.position.x - 0.1f) && (HeroPos.position.x < BlockList[0].transform.position.x + 0.1f))
        {
            StartCoroutine(AnimationActive());
        }
    }

    IEnumerator AnimationActive()
    {
        for (int i = 0; i < BlockList.Count; i++)
        {
            animator = BlockList[i].GetComponent<Animator>();
            animator.speed = 2;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
