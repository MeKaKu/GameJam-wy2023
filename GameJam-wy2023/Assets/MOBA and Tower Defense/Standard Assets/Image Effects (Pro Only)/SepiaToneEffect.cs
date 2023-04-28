using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Adjustments/Sepia Tone")]
public class SepiaToneEffect  {

	// Called by camera to apply image effect
	void OnRenderImage (RenderTexture source, RenderTexture destination) {
		// Graphics.Blit (source, destination, material);
	}
}
