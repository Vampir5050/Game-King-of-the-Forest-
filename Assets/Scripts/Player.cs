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
    public int moneyCount;
    float MouseX;
    float MoveSpeed = 0.05f;

    bool jumpKeyWasPressed;

   public GameObject moneyCountText;
    Transform Hero_Tr;
    Rigidbody rigidbodyComponent;
    public AudioSource CoinSource;


    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
        Hero_Tr = GetComponent<Transform>();

        moneyCountText = GameObject.Find("MoneyCountText");
        moneyCount = 0;
        moneyCountText.GetComponent<TextMeshProUGUI>().text = "Монет: " + moneyCount.ToString();


        
    }

    // Update is called once per frame
    void Update()
    {
        

        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");
        MouseX = Input.GetAxis("Mouse X");


        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }
        MovePlayer();

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
        //rigidbodyComponent.velocity = new Vector3(horizontalInput * 2, rigidbodyComponent.velocity.y, verticalInput * 2);
        
        Hero_Tr.Rotate(0, MouseX, 0);

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
            CoinSource.Play();
            Destroy(collision.gameObject);

        }
    }

    private void MovePlayer()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Hero_Tr.Translate(0, 0, MoveSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Hero_Tr.Translate(0, 0, -MoveSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Hero_Tr.Translate(MoveSpeed, 0, 0);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Hero_Tr.Translate(-MoveSpeed, 0, 0);
        }
    }


    

}
