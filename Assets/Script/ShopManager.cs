using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string[,] result = CsvLoader.LoadCsvBy2DimensionArray("Csv/My_Paradise_Shop");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
