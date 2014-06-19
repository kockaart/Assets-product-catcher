using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Level of indirection for the depth image,
/// provides:
/// -a frames of depth image (no player information),
/// -an array representing which players are detected,
/// -a segmentation image for each player,
/// -bounds for the segmentation of each player.
/// </summary>
public class DepthWrapper: MonoBehaviour {

	public DeviceOrEmulator devOrEmu;
	private Kinect.KinectInterface kinect;
	public int lowest = 100;
	public int highest = 3000;
	public int left = 0;
	public int right= 320;
	public int top = 0;
	public int bottom = 240;
	public bool enableHomography = true;
	//to check if x or y coordinate moves only one or skips one because of the zoom
	int pycheck=0;
	int pxcheck=0;
	[HideInInspector]
	public bool l, r, start, hit = false;
	[HideInInspector]
	public bool rotatex = false;
	[HideInInspector]
	public bool rotatey = false;
	public int xDepth=0;
	public int yDepth=0;

	
	private struct frameData
	{
		public short[] depthImg;
		public bool[] players;
		public bool[,] segmentation;
		public int[,] bounds;
	}
	
	public int storedFrames = 1;
	
	private bool updatedSeqmentation = false;
	[HideInInspector] // Hides var below
	public bool newSeqmentation = false;
	
	private Queue frameQueue;
	
	/// <summary>
	/// Depth image for the latest frame
	/// </summary>
	[HideInInspector]
	public short[] depthImg;
	/// <summary>
	/// players[i] true iff i has been detected in the frame
	/// </summary>
	[HideInInspector]
	public bool[] players;
	/// <summary>
	/// Array of segmentation images [player, pixel]
	/// </summary>
	[HideInInspector]
	public bool[,] segmentations;
	/// <summary>
	/// Array of bounding boxes for each player (left, right, top, bottom)
	/// </summary>
	[HideInInspector]
	public int[,] bounds;
	
	// Use this for initialization
	void Awake() {
		DontDestroyOnLoad(transform.gameObject);
	}

	void Start () {
		kinect = devOrEmu.getKinect();
		//allocate space to store the data of storedFrames frames.
		frameQueue = new Queue(storedFrames);
		for(int ii = 0; ii < storedFrames; ii++){	
			frameData frame = new frameData();
			frame.depthImg = new short[320 * 240];
			frame.players = new bool[Kinect.Constants.NuiSkeletonCount];
			frame.segmentation = new bool[Kinect.Constants.NuiSkeletonCount,320*240];
			frame.bounds = new int[Kinect.Constants.NuiSkeletonCount,4];
			frameQueue.Enqueue(frame);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void LateUpdate()
	{
		updatedSeqmentation = false;
		newSeqmentation = false;
	}
	/// <summary>
	/// First call per frame checks if there is a new depth image and updates,
	/// returns true if there is new data
	/// Subsequent calls do nothing have the same return as the first call.
	/// </summary>
	/// <returns>
	/// A <see cref="System.Boolean"/>
	/// </returns>
	public bool pollDepth()
	{
		//Debug.Log("" + updatedSeqmentation + " " + newSeqmentation);
		if (!updatedSeqmentation)
		{
			updatedSeqmentation = true;
			if (kinect.pollDepth())
			{
				newSeqmentation = true;
				frameData frame = (frameData)frameQueue.Dequeue();
				depthImg = frame.depthImg;
				players = frame.players;
				segmentations = frame.segmentation;
				bounds = frame.bounds;
				frameQueue.Enqueue(frame);
				processDepth();
			}
		}
		return newSeqmentation;
	}
	
	private void processDepth()
	{
		for(int player = 0; player < Kinect.Constants.NuiSkeletonCount; player++)
		{
			//clear players
			players[player] = false;
			//clear old segmentation images
			for(int ii = 0; ii < 320 * 240; ii++)
			{
				segmentations[player,ii] = false;
			}
			//clear old bounds
			for(int ii = 0; ii < 4; ii++)
			{
				bounds[player,ii] = 0;
			}
		}
		//temp image array to fill up with new point data
		short[] homographyImg = new short[320 * 240];
		//-min and max depth values
		short tmin = Convert.ToInt16(lowest);
		short tmax = Convert.ToInt16(highest);
		for(int ii = 0; ii < 320 * 240; ii++)
		{
			//get x and y coords
			int xx = ii % 320;
			int yy = ii / 320;
			//extract the depth and player
			depthImg[ii] = (short)(kinect.getDepth()[ii] >> 3);

			int player = (kinect.getDepth()[ii] & 0x07) - 1;
			if (player > 0)
			{
				if (!players[player])
				{
					players[player] = true;
					segmentations[player,ii] = true;
					bounds[player,0] = xx;
					bounds[player,1] = xx;
					bounds[player,2] = yy;
					bounds[player,3] = yy;
				}
				else
				{
					segmentations[player,ii] = true;
					bounds[player,0] = Mathf.Min(bounds[player,0],xx);
					bounds[player,1] = Mathf.Max(bounds[player,1],xx);
					bounds[player,2] = Mathf.Min(bounds[player,2],yy);
					bounds[player,3] = Mathf.Max(bounds[player,3],yy);
				}
			}

			if (enableHomography)
			{
				if (xx < right & xx > left & yy < bottom & yy > top)
				{
					int py= 240*(yy-top)/(bottom-top);
					int px=320*(xx-left)/(right-left);
					int pxy = 320*py+px;
					homographyImg[pxy]=depthImg[ii];

					//check if missed a line
					if(pycheck<(py-1))
					{
						for(int x = 0; x < 320; x++)
						{
							//to fill the new empty line with the previous
							int a=320*(py-1)+x;
							int b=320*(py-2)+x;
							homographyImg[a]=homographyImg[b];
					
						}
					}
					pycheck=py;

					//check if missed a column
					if(py==238)
					{
						if(pxcheck<(px-1))
						{
							for(int y = 0; y < 240; y++)
							{
								//to fill the new empty column with the previous
								int a=320*y+px-1;
								int b=320*y+px-2;
								homographyImg[a]=homographyImg[b];	
							}
						}
						pxcheck=px;
					}
				}
			}
		}
		if (enableHomography)
		{
			for(int pxy = 0; pxy < (320 * 239); pxy++)
			{
				//get x and y coords
				int px = pxy % 320;
				int py = pxy / 320;
				if(rotatex==false && rotatey==false)
				{
					depthImg[pxy] = homographyImg[pxy];
				}
				
				if(rotatex==true && rotatey==false)
				{
					depthImg[px+(239-py)*320] = homographyImg[pxy];
				}
				
				if(rotatey==true)
				{
					if(rotatex==true)
					{
						depthImg[(320-px)+(239-py)*320-1] = homographyImg[pxy];
					}
					else
					{
						depthImg[(320-px)+py*320] = homographyImg[pxy];
					}
				}
			}

			l=false;
			r=false;
			start=false;
			hit=false;
			for(int ii = 0; ii < 320 * 240; ii++)
			{
				//get x and y coords
				int xx = ii % 320;
				int yy = ii / 320;
				// yd*(y+1)/240*xd*(x+1)/320
				int temp=xDepth*(yy+1)/240+yDepth*(xx+1)/320;
				depthImg[ii]=(short)(depthImg[ii]-temp);
								
				//-limit depth values and set for 0
				if (depthImg[ii] < tmin) depthImg[ii] = 0;
				else if (depthImg[ii] > tmax) depthImg[ii] = 0;
				
				//check if point is tracked, "white" in the 320x240 matrix
				if (depthImg[ii] >0 & xx > 270)
				{
					if (yy<60){
						start=true;
					}
					if (60<yy & yy<120){
						l=true;
					}
					if (120<yy & yy<180){
						r=true;
					}
					if (180<yy){
						hit=true;
					}
				}
			}
		}
	}
}