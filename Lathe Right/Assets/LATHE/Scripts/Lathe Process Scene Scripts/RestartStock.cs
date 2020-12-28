using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartStock : MonoBehaviour
{
    [SerializeField] private GameObject stockPrefab;
    [SerializeField] private List<GameObject> stock;
    [SerializeField] private List<GameObject> destruction;
    private Vector3 pos, scale;
    private Quaternion rot;

    // Start is called before the first frame update
    public void ResetStock(int index)
    {
        pos = destruction[index].gameObject.transform.localPosition;
        rot = destruction[index].gameObject.transform.localRotation;
        scale = destruction[index].gameObject.transform.localScale;
        Destroy(destruction[index]);
        GameObject newStock = Instantiate(stockPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newStock.transform.parent = stock[index].gameObject.transform;
        newStock.transform.localRotation = rot;
        newStock.transform.localPosition = pos;
        newStock.transform.localScale = scale;
        destruction[index] = newStock;
    }
}
