using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessController : MonoBehaviour
{
    //Need a reset stock material script one in the mill
    [SerializeField] private HintTrigger hintTriggerFlash;
    //[SerializeField] private ButtonReset reset;
    [SerializeField] private List<OperationManager> operations;
    [SerializeField] private RestartStock restartStock;
    [SerializeField] private RendererTrigger stockTrigger;
    [SerializeField] private List<GameObject> dummyStock;
    [SerializeField] public float animSpeed;

    private int m_index;
    private OperationManager controller;

    void Awake()
    {
        controller = null;
        foreach (OperationManager operation in operations)
        {
            operation.ActivateAnimator(false);
        }
        foreach (GameObject obj in dummyStock)
        {
            obj.SetActive(false);
        }
    }

    public void ChangeAnimator(int index)
    {
        bool prevState = false;
        OperationManager prevController = controller;
        if (controller == null || controller.Done)
        {
            if (controller != null)
            {
                foreach (GameObject obj in dummyStock)
                {
                    obj.SetActive(false);
                }
                prevState = controller.Done;
                restartStock.ResetStock(m_index);
                controller.ActivateAnimator(false);

                //stockTrigger.gameObject.SetActive(true);
                //stockTrigger.ResetStock();

            }
            if (index >= 0 && index < operations.Count)
            {
                controller = operations[index];
                controller.ResetAnim(prevState, prevController);
                m_index = index;
                hintTriggerFlash.SetAnimIndex();
                dummyStock[index].SetActive(true);
                //reset.ResetDRO();
            }
        }
    }

    public void StartCollsion()
    {
        if (controller != null)
        {
            controller.SetAnimSpeed(0);
        }
    }

    public void EndCollision()
    {
        if (controller != null)
        {
            controller.SetAnimSpeed(animSpeed);
        }
    }

    public int Index
    {
        get => m_index;
    }

    public OperationManager Operation
    {
        get => controller;
    }


}
