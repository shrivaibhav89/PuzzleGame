using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class connectors : MonoBehaviour
{
    public connectors connectedTo;
    public bool Connectorcharged = false;


    public ShapeTemp myShapeTemp;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.GetComponentInParent<ShapeTemp>() != null)
        {
            ShapeTemp st = collision.gameObject.GetComponentInParent<ShapeTemp>();
            connectedTo = collision.GetComponent<connectors>();
           /* if (st.IsGettingEnergy == true || st.IsEnengySource)
            {
                
                GetCharge();
            }*/
          //  Debug.LogError("Trigger" + collision.gameObject.name);
        }
        

    }

    public void GetCharge() {
        Connectorcharged = true;
        myShapeTemp.IsGettingEnergy = true;
        myShapeTemp.OnGettingCharge();
    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        connectedTo = null;

       /* if (Connectorcharged == true)
        {
            myShapeTemp.IsGettingEnergy = false;
            Connectorcharged = false;
        }*/
    }
}
