using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public float easing = 0.05f;
    public Vector2 minXY = Vector2.zero;
    static public GameObject POI; // ������ �� ������������ ������ 
    [Header("Set Dynamically")]
    public float camZ; // �������� ���������� Z ������
    void Awake()
    {
        camZ = this.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    void FixedUpdate()
    {
        /* //������������ ������ if �� ������� �������� ������
        if (POI == null) return;
        // �������� ������� ������������� �������
        Vector3 destination = POI.transform.position; */
        Vector3 destination;
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            // �������� ������� ������������� �������
            destination = POI.transform.position;
            // ���� ������������ ������ - ������, ���������, ��� �� �����������
            if (POI.tag == "Projectile")
            {
                //11 ���� �� ����� �� �����(�� ���� �� ���������)
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    // ������� �������� ��������� ���� ������ ������
                    POI = null;
                    //� ��������� �����
                    return;
                }
            }
        }
        // ���������� X � Y ������������ ����������


        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        // ������������� ���������� �������� destination.z ������ camZ, �����
        // ���������� ������ ��������
        destination.z = camZ;
        // ��������� ������ � ������� destination
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }
}
