using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageDisplay : MonoBehaviour
{
        //Text damages variables
    public Text text;
    public float lifeTime = 15f;
    public float minDistance = 1f;
    public float maxDistance = 3f;
    public Vector3 finalSize = new Vector3(0.02f, 0.02f, 0.01f);

    private Vector3 initPosition;
    private Vector3 targetPosition;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(2 * transform.position - Camera.main.transform.position);

        //float direction = Random.rotation.eulerAngles.z;
        initPosition = transform.position;
        float dist = Random.Range(minDistance, maxDistance);
        targetPosition = initPosition + (Quaternion.Euler(0, 0, 45) * new Vector3(dist, dist, 0f));
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;

        float fraction = lifeTime / 2f;

        if (timer > lifeTime)
        {
            Destroy(gameObject);
        }else if (timer > fraction)
        {
            text.color = Color.Lerp(text.color, Color.clear, (timer - fraction) / (lifeTime - fraction));
        }
        
        transform.position = Vector3.Lerp(initPosition, targetPosition, Mathf.Sin(timer / lifeTime));
        transform.localScale = Vector3.Lerp(Vector3.zero, finalSize, Mathf.Sin(timer / lifeTime));
        
    }

    public void SetDamageText(float damage)
    {
        text.text = damage.ToString();
    }

    public void SetColor(Color color)
    {
        text.color = color;
    }
}
