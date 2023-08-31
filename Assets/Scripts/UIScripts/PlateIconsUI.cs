using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;


    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
    }

    private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        int currentIconAmount = transform.childCount;
        int counter = 0;
        foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTemplate.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
        }
        foreach (Transform child in transform)
        {
            if (child == iconTemplate)
            {
                iconTemplate.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(plateKitchenObject.GetKitchenObjectSOList()[0]);
                continue;
            }
            Destroy(child.gameObject);
            counter++;
            if (counter == currentIconAmount)
            {
                break;
            }
        }
    }
}
