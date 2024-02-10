using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HoldButtonDetector : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float holdTime = 3f; // Bas�l� tutma s�resi (saniye)
    private bool pointerDown = false;
    private float pointerDownTimer = 0f;
    public static bool PowerShotUselable = false;

    public UnityEvent onHold;


    public void OnPointerDown(PointerEventData eventData)
    {
        if(PowerShotUselable == true)
        {

            PlayerSkills playerSkills = FindAnyObjectByType<PlayerSkills>();
            playerSkills.PowerShootCharging();
            pointerDown = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(PowerShotUselable == true)
        {

            PlayerSkills playerSkillss = FindAnyObjectByType<PlayerSkills>();
            playerSkillss.PowerShootRelease();
            Reset();
            PowerShotUselable = false;
        }
        
    }

    private void Update()
    {
        if (pointerDown)
        {
            pointerDownTimer += Time.deltaTime;

            if (pointerDownTimer >= holdTime)
            {
                if (onHold != null)
                    onHold.Invoke();

                Reset();
            }
        }
    }

    private void Reset()
    {
        pointerDown = false;
        pointerDownTimer = 0f;
    }
}
