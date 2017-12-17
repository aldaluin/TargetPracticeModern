// script for draw black texture after fpc death
using UnityEngine;
using System.Collections;

namespace CharlieAssets.Dev
{

public class Health_BlackTexture : MonoBehaviour
//
//  Edited cmcb 2017/05/27
//
//  When triggered, fades the screen to black
//

{
    private Color color_fill = Color.black;
	private float change_speed = 0f;
    private Texture color_texture;
    private float transparency_fill = 0f;

    void Awake()
    {
        Texture2D null_texture = new Texture2D(1, 1) as Texture2D;
        null_texture.SetPixel(0, 0, Color.black);
        null_texture.Apply();

        color_texture = null_texture;
    }


    void Update()
    {
        color_fill.a = transparency_fill;

        if (transparency_fill > 1)
        {
            transparency_fill = 1;
        }

        if (transparency_fill < 0)
        {
            transparency_fill = 0;
        }

        transparency_fill += Time.deltaTime * change_speed;
    }

    void OnGUI()
    {
        GUI.color = color_fill;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), color_texture, ScaleMode.StretchToFill, true);
    }

	public void Trigger() {
		change_speed = 1;
	}

}}
