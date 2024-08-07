using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class ShapeTemp : MonoBehaviour
{
    public bool IsEnengySource = false;
    bool _sGettingEnergy = false;
    public GameObject energySprite;
    public bool IsGettingEnergy
    {
        get { return _sGettingEnergy; }
        set
        {
            _sGettingEnergy = value;

            if (value == true || IsEnengySource == true)
            {

                foreach (SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>())
                {
                    r.color = Color.green;
                }
            }
            else
            {
                foreach (SpriteRenderer r in GetComponentsInChildren<SpriteRenderer>())
                {
                    r.color = Color.red;
                }
            }
        }

    }
    public List<connectors> Cpoints = new List<connectors>();
    private void Start() {
        if(IsEnengySource)
        {
            energySprite.SetActive(true);
        }
    }
    private void OnMouseDown()
    {
        StartCoroutine(RatateObject());


    }

    public IEnumerator RatateObject()
    {
        float startAngle = transform.eulerAngles.z;
        float endAngle = startAngle + 90;
        float elapsed = 0.0f;
        float duration = 0.1f;
        while (elapsed < duration)
        {
            float currentAngle = Mathf.Lerp(startAngle, endAngle, elapsed / duration);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, currentAngle);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the final angle is set
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, endAngle);
        CheckLoopsToGame();
    }
    public void CheckLoopsToGame()
    {
        FindObjectOfType<gameManager>().checkForLoops();
    }


    public void OnGettingCharge()
    {

        foreach (connectors connector in Cpoints)
        {

            // if (connector.Connectorcharged == false) {

            if (connector.connectedTo != null && gameManager.instance.powered.Contains(connector.connectedTo) == false)
            {
                gameManager.instance.powered.Add(connector.connectedTo);
                connector.connectedTo.GetCharge();
            }
            // }
        }

    }


}
