using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

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
        if (orbsControl.DestroyedOrbs == 2 && !open&& SceneManager.GetActiveScene().name == "Dessert")
        {
            DessertNewPath();
        }
        if (orbsControl.DestroyedOrbs == 3 && !open && SceneManager.GetActiveScene().name == "Castle")
        {
            CastleNewPath();
        }
        if (orbsControl.DestroyedOrbs == 4 && !open && SceneManager.GetActiveScene().name == "Map3")
        {
            Map3NewPath();
        }
    }

    void DessertNewPath()
    {
        open = true;
        FollowPlayer.enabled = false;
        StartCoroutine(DessertNewPathEvent());
    }

    void CastleNewPath()
    {
        open = true;
        FollowPlayer.enabled = false;
        StartCoroutine(CastleNewPathEvent());
    }

    void Map3NewPath()
    {
        open = true;
        FollowPlayer.enabled = false;
        StartCoroutine(Map3NewPathEvent());
    }

    IEnumerator DessertNewPathEvent()
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

    IEnumerator CastleNewPathEvent()
    {
        light2D.enabled = true;
        cameraTransform.position = new Vector3(186.95f, -81.07f, cameraTransform.position.z);
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

    IEnumerator Map3NewPathEvent()
    {
        light2D.enabled = true;
        cameraTransform.position = new Vector3(79f, -23.28f, cameraTransform.position.z);
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