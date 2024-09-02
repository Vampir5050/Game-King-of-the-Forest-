using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Transform graundCheckTransform = null;
    

    float horizontalInput;
    float verticalInput;
    Rigidbody rigidbodyComponent;
    bool jumpKeyWasPressed;
    Transform Hero_Tr;
    float Hero_speed = 0.03f;
    int moneyCount;
    GameObject moneyCountText;


    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        Hero_Tr = GetComponent<Transform>();

        moneyCountText = GameObject.Find("MoneyCountText");
        
            moneyCount = 0;
            moneyCountText.GetComponent<TextMeshProUGUI>().text = "Монет: " + moneyCount.ToString();
        //moneyCountText.GetComponent<Text>().text = "Монет: " + moneyCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
       
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        if (Input.GetMouseButton(0)) //0 - ЛКМ, 1-ПКМ, 2 - колёсеко
        {
            Ray ray = transform.GetChild(5).GetComponent<Camera>().ScreenPointToRay(Input.mousePosition); //по положению курсора мыши на экране определяется некая точка в 3d пространстве
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit)) //hit - точка пересечения луча с каким-то физическим объектом
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.layer == 7)
                {
                    moneyCount++;
                    moneyCountText.GetComponent<TextMeshProUGUI>().text = "Монет: " + moneyCount.ToString();
                    Destroy(hit.collider.gameObject);
                }
            }
        }

    }

    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput * 2, rigidbodyComponent.velocity.y, verticalInput * 2);

        if (Physics.OverlapSphere(graundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }
        if (jumpKeyWasPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;

        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            moneyCount++;
            moneyCountText.GetComponent<TextMeshProUGUI>().text = "Монет: " + moneyCount.ToString();
            Destroy(collision.gameObject);

        }
    }


    

}
