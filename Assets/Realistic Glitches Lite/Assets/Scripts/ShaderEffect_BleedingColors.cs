using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderEffect_BleedingColors : MonoBehaviour {

	public Shader BleedingColors;
	public float intensity = 3;
	public float shift = 0.5f;
	private Material material;

	// Creates a private material used to the effect
	void Awake ()
	{
		//material = new Material( Shader.Find("Hidden/BleedingColors") );	Shader.Find ne fonctionne pas a l'export
		material = new Material (BleedingColors);
	}

	// Postprocess the image
	void OnRenderImage (RenderTexture source, RenderTexture destination)
	{
		material.SetFloat("_Intensity", intensity);
		material.SetFloat("_ValueX", shift);
		Graphics.Blit (source, destination, material);
	}
}
