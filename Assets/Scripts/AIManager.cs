using UnityEngine;

public class AIManager : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    private GameObject[] objects, targets;

    [SerializeField]
    private Transform rightHandPoint, leftHandPoint;

    private int number;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("targetPlace"))
        {
            ++number;
            anim.SetTrigger("throwRight");
            GameObject newObject = Instantiate(objects[number], rightHandPoint.transform.position, transform.rotation);
            newObject.GetComponent<Rigidbody>().AddForce(new Vector3(30, 350, 0));
            if (number == targets.Length - 1)
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (number < targets.Length)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targets[number].transform.position.x, 0, targets[number].transform.position.z), 0.1f);
            transform.rotation = Quaternion.Euler(0, targets[number].gameObject.transform.rotation.eulerAngles.y, 0);
        }
    }
}