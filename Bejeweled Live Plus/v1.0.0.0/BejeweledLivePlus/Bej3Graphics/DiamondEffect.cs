using System;
using System.Collections.Generic;
using BejeweledLivePlus.Misc;
using SexyFramework.Graphics;

namespace BejeweledLivePlus.Bej3Graphics
{
	public class DiamondEffect : IDisposable
	{
		public DiamondEffect(int theDiamondId)
		{
			int[] array = new int[] { 1185, 1195, 1205, 1214 };
			int[] array2 = new int[] { 1190, 1200, 1209, 1219 };
			int[,] array3 = new int[,]
			{
				{ 4, 1186, 1187, 1188, 1189 },
				{ 4, 1196, 1197, 1198, 1199 },
				{ 3, 1206, 1207, 1208, -1 },
				{ 4, 1215, 1216, 1217, 1218 }
			};
			int[,] array4 = new int[,]
			{
				{ 4, 1191, 1192, 1193, 1194 },
				{ 4, 1201, 1202, 1203, 1204 },
				{ 4, 1210, 1211, 1212, 1213 },
				{ 4, 1220, 1221, 1222, 1223 }
			};
			this.mDiamondId = theDiamondId;
			this.mParticleCount = 0;
			theDiamondId--;
			this.mBaseImg = array[this.mDiamondId - 1];
			this.mDirtImg = array2[this.mDiamondId - 1];
			for (int i = 1; i <= array3[theDiamondId, 0]; i++)
			{
				DiamondEffect.SubImage subImage = new DiamondEffect.SubImage();
				subImage.mImageId = array3[theDiamondId, i];
				this.mSubImgs.Add(subImage);
			}
			this.mParticleCount = array4[theDiamondId, 0];
			for (int j = 0; j < this.mSubImgs.Count; j++)
			{
				this.mSubImgs[j].mSparkles.Capacity = 64;
			}
		}

		public void Dispose()
		{
		}

		public void Update()
		{
			for (int i = 0; i < this.mSubImgs.Count; i++)
			{
				int j = 0;
				while (j < this.mSubImgs[i].mSparkles.Count)
				{
					if (this.mSubImgs[i].mSparkles[j].mUpdateCnt >= GlobalMembers.M(100))
					{
						this.mSubImgs[i].mSparkles[j] = this.mSubImgs[i].mSparkles[this.mSubImgs[i].mSparkles.Count - 1];
						this.mSubImgs[i].mSparkles.RemoveAt(this.mSubImgs[i].mSparkles.Count - 1);
					}
					else
					{
						this.mSubImgs[i].mSparkles[j].mUpdateCnt++;
						j++;
					}
				}
				if (Common.Rand() % GlobalMembers.M(15) == 0)
				{
					DiamondEffect.Sparkle sparkle = new DiamondEffect.Sparkle(Common.Rand(1f), Common.Rand(1f), 0);
					this.mSubImgs[i].mSparkles.Add(sparkle);
				}
			}
		}

		public void Draw(Graphics g, int theX, int theY, bool theDrawParticles)
		{
			if (!theDrawParticles)
			{
				return;
			}
			g.DrawImage(GlobalMembersResourcesWP.GetImageById(this.mBaseImg), theX, theY);
		}

		public void DrawDirt(Graphics g, int theX, int theY)
		{
			if (this.mDirtImg != -1)
			{
				g.DrawImage(GlobalMembersResourcesWP.GetImageById(this.mDirtImg), theX, theY);
			}
		}

		public int GetExplodeSubId(int theRandSeed)
		{
			int[,] array = new int[,]
			{
				{ 1191, 1192, 1193, 1194 },
				{ 1201, 1202, 1203, 1204 },
				{ 1210, 1211, 1212, 1213 },
				{ 1220, 1221, 1222, 1223 }
			};
			return array[this.mDiamondId - 1, theRandSeed % this.mParticleCount];
		}

		public List<DiamondEffect.SubImage> mSubImgs = new List<DiamondEffect.SubImage>();

		public int mBaseImg;

		public int mDirtImg;

		public int mDiamondId;

		public int mParticleCount;

		public class Sparkle
		{
			public Sparkle(float x, float y, int updateCnt)
			{
				this.mX = x;
				this.mY = y;
				this.mUpdateCnt = updateCnt;
			}

			public float mX;

			public float mY;

			public int mUpdateCnt;
		}

		public class SubImage
		{
			public List<DiamondEffect.Sparkle> mSparkles = new List<DiamondEffect.Sparkle>();

			public int mImageId;
		}
	}
}
