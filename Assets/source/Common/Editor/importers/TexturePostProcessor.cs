using UnityEngine;
using UnityEditor;

public class TexturePostProcessor : AssetPostprocessor
{
	void OnPostprocessTexture(Texture2D texture)
	{
		TextureImporter importer = assetImporter as TextureImporter;
		
		importer.spritePixelsPerUnit = Common.PixelsInUnit();
		importer.filterMode = FilterMode.Point;
		importer.mipmapEnabled = false;
		importer.textureFormat = TextureImporterFormat.RGBA32;
		
		//Object asset = AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Texture2D));
	}
}