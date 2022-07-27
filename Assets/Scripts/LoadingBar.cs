using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    private Slider slider;
    private float targetProgress = 0;
    [SerializeField] private float fillSpeed = 0.5f;
    private bool loading = false;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.slider.value < targetProgress)
        {
            this.slider.value += Time.deltaTime * fillSpeed;
            this.slider.value = Mathf.Min(1, this.slider.value);
        }
        else
        {
            this.loading = false;
        }
    }

    public void LoadBar()
    {
        this.loading = true;
        this.slider.value = 0;
        this.targetProgress = 1;
    }

    public bool IsLoaded()
    {
        return this.slider.value >= 1;
    }

    public bool IsLoading()
    {
        return this.loading;
    }

    public void Unload()
    {
        this.loading = false;
        this.slider.value = 0;
        this.targetProgress = 0;
    }
}
