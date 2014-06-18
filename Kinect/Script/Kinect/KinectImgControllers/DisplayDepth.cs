using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class DisplayDepth : MonoBehaviour {
	
	public DepthWrapper dw;
	//Component dw = GameObject.Find("KinectPrefab").GetComponent<DepthWrapper>();
	[HideInInspector]
	public Texture2D tex;
	private float c, d, e, f;
	
//	void Awake() {
//		DontDestroyOnLoad(transform.gameObject);
//	}
	
	// Use this for initialization
	void Start () {
		tex = new Texture2D(320,240,TextureFormat.ARGB32,false);
	
		renderer.material.mainTexture = tex;
		//		guiTexture.texture = tex;
	}
	
	
	//	 Update is called once per frame
	void Update () {
		if (dw.pollDepth())
		{
			
			e=Screen.width;
			f=Screen.height;
			c=e/f;
			//transform.localScale = new Vector3(c, d, 1);
			tex.SetPixels32(convertDepthToColor(dw.depthImg));
			//tex.SetPixels32(convertPlayersToCutout(dw.segmentations));
			tex.Apply();
		//	renderer.material.SetTextureScale("_MainTex", new Vector2(scaleX, scaleY));
			renderer.material.mainTextureScale = new Vector2(-1, 1);
		}
	}

	
	private Color32[] convertDepthToColor(short[] depthBuf)
	{
//		for (int pix = 0; pix < depthBuf.Length; pix++)
//		{
//			depthBuf
//		}
		
		Color32[] img = new Color32[depthBuf.Length];
		for (int pix = 0; pix < depthBuf.Length; pix++)
		{
			img[pix].r = (byte)(256-depthBuf[pix] / 16);
			img[pix].g = (byte)(256-depthBuf[pix] / 24);
			img[pix].b = (byte)(256-depthBuf[pix] / 32);
		}
		return img;
	}
	
	private Color32[] convertPlayersToCutout(bool[,] players)
	{
		Color32[] img = new Color32[320*240];
		for (int pix = 0; pix < 320*240; pix++)
		{
			if(players[0,pix]|players[1,pix]|players[2,pix]|players[3,pix]|players[4,pix]|players[5,pix])
			{
				img[pix].a = (byte)255;
			} else {
				img[pix].a = (byte)0;
			}
		}
		return img;
	}
}