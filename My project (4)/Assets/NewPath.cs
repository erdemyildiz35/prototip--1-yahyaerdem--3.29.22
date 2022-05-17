using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;

public class NewPath : MonoBehaviour
{
    [SerializeField] OrbsControl orbsControl;
    [SerializeField] bool open = false;
    [SerializeField] Transform cameraTransform;
    [SerializeField] CameraFollowPlayer FollowPlayer;
    [SerializeField] TilemapRenderer tileMapRenderer;
    [SerializeField] Light2D light2D;
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("Orbs"))
        {
            orbsControl = GameObject.Find("Orbs").GetComponent<OrbsControl>();
        }
        cameraTransform = GameObject.Find("Main Camera").GetComponent<Transform>();
        FollowPlayer = GameObject.Find("Main Camera").GetComponent<CameraFollowPlayer>();
        tileMapRenderer = gameObject.GetComponent<TilemapRenderer>();
        light2D = GameObject.Find("Main Camera").transform.GetChild(0).GetComponent<Light2D>();
        light2D.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (orbsControl.DestroyedOrbs == 2 && !open)
        {
            OpenNewPath();
        }
    }

    void OpenNewPath()
    {
        open = true;
        FollowPlayer.enabled = false;
        StartCoroutine(NewPathEvent());
    }

    IEnumerator NewPathEvent()
    {
        light2D.enabled = true;
        cameraTransform.position = new Vector3(79f, -19.13f, cameraTransform.position.z);
        yield return new WaitForSeconds(1f);

        tileMapRenderer.enabled = false;
        yield return new WaitForSeconds(1f);

        FollowPlayer.SmoothSpeed /= 2;
        FollowPlayer.enabled = true;
        yield return new WaitForSeconds(1f);

        FollowPlayer.SmoothSpeed *= 2;
        light2D.enabled = false;
        gameObject.SetActive(false);
    }
}
