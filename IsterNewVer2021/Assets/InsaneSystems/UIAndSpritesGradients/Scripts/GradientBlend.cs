using UnityEngine;
using UnityEngine.UI;

namespace InsaneSystems.UIAndSpriteGradients
{
	public class GradientBlend : MonoBehaviour
	{
		[Tooltip("Blend time in seconds.")]
		[SerializeField] float blendTime = 2f;
		[SerializeField] bool blendAtSceneStart = false;
		[SerializeField] Color topGradientColor = Color.white;
		[SerializeField] Color bottomGradientColor = new Color(1f, 0.8f, 0.9f, 1f);

		Color defaultTopColor;
		Color defaultBotColor;

		float blendValue;

		bool isBlending;

		SpriteRenderer spriteRenderer;
		Image image;
		Material editedMaterial;

		void Start()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			image = GetComponent<Image>();

			if (spriteRenderer)
				editedMaterial = spriteRenderer.material;	
			else if (image)
				editedMaterial = image.material;
			else
				Debug.LogWarning("No UI Image or Sprite renderer added to object with GradientBlend script, it can't work without any of them!");

			if (editedMaterial)
			{
				defaultBotColor = editedMaterial.GetColor("_GradientBotColor");
				defaultTopColor = editedMaterial.GetColor("_GradientTopColor");
			}

			if (blendAtSceneStart)
				StartBlend();

			var gradientBlend = GetComponent<InsaneSystems.UIAndSpriteGradients.GradientBlend>();
			gradientBlend.StartBlend();
		}

		void Update()
		{
			float blendingSign = isBlending ? 1f : -1f;

			blendValue = Mathf.Clamp(blendValue + Time.deltaTime * blendingSign, 0f, blendTime);

			var blendedTopColor = Color.Lerp(defaultTopColor, topGradientColor, blendValue / blendTime);
			var blendedBotColor = Color.Lerp(defaultBotColor, bottomGradientColor, blendValue / blendTime);

			if (editedMaterial)
			{
				editedMaterial.SetColor("_GradientTopColor", blendedTopColor);
				editedMaterial.SetColor("_GradientBotColor", blendedBotColor);
			}
		}
 
		/// <summary>Starts animated blending from default material gradient to your own.</summary> 
		/// <param name="reset">This param meants that blending will start from zero value.</param>
		public void StartBlend(bool reset = true)
		{
			ResetBlend(reset);
			isBlending = true;
		}

		/// <summary>Allows to reset blending to default gradient colors.</summary>
		/// <param name="instant">Set it to true, if you want to reset colors instantly, without animation.</param>
		public void ResetBlend(bool instant)
		{
			if (instant)
				blendValue = 0;

			isBlending = false;
		}

		void OnDestroy()
		{
			if (editedMaterial)
			{
				editedMaterial.SetColor("_GradientTopColor", defaultTopColor);
				editedMaterial.SetColor("_GradientBotColor", defaultBotColor);
			}
		}
	}
}