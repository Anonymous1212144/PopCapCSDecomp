using System;
using System.Collections.Generic;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;
using SexyFramework.Sound;
using SexyFramework.Widget;

namespace BejeweledLivePlus.Misc
{
	public class Background : Widget, PopAnimListener
	{
		public virtual void LoadImageProc()
		{
			string text = this.mPath;
			string idByPath = GlobalMembers.gApp.mResourceManager.GetIdByPath(text);
			string fileDir = Common.GetFileDir(text);
			if (this.mPath.ToUpper().Contains(".PAM"))
			{
				idByPath = GlobalMembers.gApp.mResourceManager.GetIdByPath(fileDir + "\\flattenedpam");
				this.mResourceGroup = GlobalMembers.gApp.mResStreamsManager.GetGroupName((uint)GlobalMembers.gApp.mResStreamsManager.GetGroupForFile(fileDir + "\\flattenedpam"));
			}
			BejeweledLivePlusApp.LoadContent(this.mResourceGroup);
			this.mResourceImageRef = GlobalMembers.gApp.mResourceManager.GetImageRef(idByPath);
			this.mImage = this.mResourceImageRef.GetSharedImageRef();
			if (this.mImage.GetMemoryImage() == null)
			{
				this.mImage = GlobalMembers.gApp.mResourceManager.GetImageRef(idByPath).GetSharedImageRef();
				if (this.mImage.GetMemoryImage() != null)
				{
					this.mLoadedImages.Add(idByPath);
				}
				else
				{
					this.mImage = GlobalMembers.gApp.GetSharedImage(text);
				}
			}
			this.mStage++;
		}

		public static void LoadImageProcStatic(object theThis)
		{
			((Background)theThis).LoadImageProc();
		}

		public void LoadAnimProc()
		{
		}

		public static void LoadAnimProcStatic(object theThis)
		{
			((Background)theThis).LoadAnimProc();
		}

		public Background(string thePath, bool wantFlatImage)
			: this(thePath, wantFlatImage, true)
		{
		}

		public Background(string thePath)
			: this(thePath, true, true)
		{
		}

		public Background(string thePath, bool wantFlatImage, bool wantAnim)
		{
			this.mResourceGroup = "";
			this.mPath = thePath;
			this.mScoreWaitLevel = 0;
			this.mScoreWaitsPassed = 0;
			this.mAnim = null;
			this.mUpdateAcc = 0f;
			this.mAnimActive = false;
			this.mWantAnim = false;
			this.mStage = 0;
			this.mKeepFlatImage = false;
			this.mExtraImgScale = 1.0;
			this.mExtraDrawScale = 1.0;
			this.mHasRenderTargetFlatImage = false;
			this.mRenderTargetFlatImageDirty = false;
			this.mAllowRealign = false;
			this.mAllowRescale = false;
			this.mImageOverlayAlpha.SetConstant(1.0);
			this.mColor = Color.White;
			this.mNoParticles = false;
			if (wantFlatImage)
			{
				this.LoadImageProc();
				return;
			}
			this.mStage++;
		}

		public override void Dispose()
		{
			if (this.mAnim != null)
			{
				this.RemoveWidget(this.mAnim);
				if (this.mAnim != null)
				{
					this.mAnim.Dispose();
					this.mAnim = null;
				}
			}
			this.RemoveAllWidgets(true, false);
			for (int i = 0; i < this.mLoadedImages.Count; i++)
			{
				GlobalMembers.gApp.mResourceManager.DeleteImage(this.mLoadedImages[i]);
			}
			for (int j = 0; j < this.mLoadedSounds.Count; j++)
			{
				GlobalMembers.gApp.mResourceManager.DeleteSound(this.mLoadedSounds[j]);
			}
			for (int k = 0; k < this.mDirectLoadedSounds.Count; k++)
			{
				GlobalMembers.gApp.mSoundManager.ReleaseSound(this.mDirectLoadedSounds[k]);
			}
			this.mImage.Release();
			this.mResourceImageRef.Release();
			BejeweledLivePlusApp.UnloadContent(this.mResourceGroup);
			GlobalMembers.gApp.CleanSharedImages();
			base.Dispose();
		}

		public override void Draw(Graphics g)
		{
			bool flag = false;
			bool flag2 = this.mColor != Color.White;
			if (flag2)
			{
				g.SetColor(this.mColor);
				g.PushColorMult();
				g.SetColor(Color.White);
			}
			Graphics3D graphics3D = g.Get3D();
			if (graphics3D != null && this.mAllowRescale)
			{
				SexyTransform2D theTransform = new SexyTransform2D(true);
				theTransform.Scale(1.12f, 1.12f);
				graphics3D.PushTransform(theTransform);
			}
			if (this.mAnim != null && this.mAnim.mLoaded)
			{
				this.mAnim.Draw(g);
				flag = true;
			}
			if (this.mImage.GetMemoryImage() != null && this.mImageOverlayAlpha > 0.0)
			{
				g.SetColorizeImages(true);
				g.SetColor(this.mImageOverlayAlpha);
				if (this.mImage.mHeight == this.mHeight)
				{
					if (this.mHasRenderTargetFlatImage && this.mImage.GetImage().GetRenderData() == null)
					{
						this.GetBackgroundImage();
					}
					if (this.mAllowRealign)
					{
						g.DrawImage(this.mImage.GetImage(), (GlobalMembers.S(1920) - this.mImage.mWidth) / 2, 0);
					}
					else
					{
						g.DrawImage(this.mImage.GetImage(), 0, 0);
					}
				}
				else
				{
					int num = this.mHeight / this.mImage.mHeight;
					g.DrawImage(this.mImage.GetImage(), 0, 0, this.mWidth, this.mHeight);
				}
				flag = true;
			}
			if (!flag)
			{
				g.SetColor(new Color(64, 0, 0));
				g.FillRect(0, 0, this.mWidth * 2, this.mHeight);
			}
			if (flag2)
			{
				g.PopColorMult();
			}
			if (this.mFlash > 0.0)
			{
				g.PushState();
				g.SetDrawMode(Graphics.DrawMode.Additive);
				g.SetColor(this.mFlash);
				g.FillRect(0, 0, this.mWidth, this.mHeight);
				g.PopState();
			}
			if (graphics3D != null && this.mAllowRescale)
			{
				graphics3D.PopTransform();
			}
		}

		public override void Update()
		{
			base.Update();
			if (GlobalMembers.gIs3D && GlobalMembers.gApp.mAnimateBackground && this.mStage == 1 && (this.mVisible || this.mParent == null))
			{
				this.mStage++;
				this.LoadAnimProc();
			}
			bool flag = false;
			if (this.mAnim != null && !this.mAnimActive && this.mVisible && GlobalMembers.gApp.mLoaded && this.mWantAnim)
			{
				flag = true;
				this.mAnim.Play();
				this.mAnim.Resize(0, 0, this.mWidth, this.mHeight);
				if (this.mImage.GetMemoryImage() != null)
				{
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBACKGROUND_UPDATE_SPEED, this.mUpdateSpeed);
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBACKGROUND_IMAGE_OVERLAY_ALPHA, this.mImageOverlayAlpha);
				}
				else
				{
					this.mUpdateSpeed.SetConstant(1.0);
				}
				this.mAnimActive = true;
			}
			if (!this.mHasRenderTargetFlatImage && this.mAnim != null && !this.mKeepFlatImage && this.mImageOverlayAlpha == 0.0)
			{
				this.mImage.Release();
				this.mResourceImageRef.Release();
			}
			if (this.mAnimActive)
			{
				this.mRenderTargetFlatImageDirty = true;
				bool mVSyncUpdates = GlobalMembers.gSexyApp.mVSyncUpdates;
				GlobalMembers.gSexyApp.mVSyncUpdates = true;
				this.mAnim.UpdateF((float)this.mUpdateSpeed);
				this.mUpdateAcc += (float)this.mUpdateSpeed;
				if (this.mUpdateAcc < 1f && flag)
				{
					this.mUpdateAcc = 1f;
				}
				while (this.mUpdateAcc >= 1f)
				{
					this.mAnim.Update();
					this.mUpdateAcc -= 1f;
				}
				GlobalMembers.gSexyApp.mVSyncUpdates = mVSyncUpdates;
				if (this.mUpdateSpeed > 0.0)
				{
					this.MarkDirty();
				}
				if (!GlobalMembers.gApp.mAnimateBackground)
				{
					this.mStage = 0;
					this.mAnimActive = false;
					this.RemoveWidget(this.mAnim);
					if (this.mAnim != null)
					{
						this.mAnim.Dispose();
						this.mAnim = null;
					}
					this.mAnim = null;
					this.LoadImageProc();
					this.mImageOverlayAlpha.SetConstant(1.0);
				}
			}
		}

		public virtual SharedImageRef GetBackgroundImage(bool wait)
		{
			return this.GetBackgroundImage(wait, true);
		}

		public virtual SharedImageRef GetBackgroundImage()
		{
			return this.GetBackgroundImage(true, true);
		}

		public virtual SharedImageRef GetBackgroundImage(bool wait, bool removeAnim)
		{
			if (this.mImage.GetMemoryImage() == null || this.mHasRenderTargetFlatImage)
			{
				if (this.mHasRenderTargetFlatImage && this.mImage.GetImage().GetRenderData() == null)
				{
					this.mRenderTargetFlatImageDirty = true;
				}
				if (this.mImage.GetMemoryImage() == null || this.mRenderTargetFlatImageDirty)
				{
					if (this.mAnim != null)
					{
						if (this.mImage.mUnsharedImage != null)
						{
							this.mImage.mUnsharedImage.Dispose();
						}
						this.mImage.mUnsharedImage = null;
						this.mImage.mUnsharedImage = new DeviceImage();
						this.mImage.GetMemoryImage().AddImageFlags(16U);
						this.mImage.GetMemoryImage().mIsVolatile = true;
						this.mImage.GetMemoryImage().SetImageMode(true, true);
						this.mImage.GetMemoryImage().Create(this.mWidth, this.mHeight);
						this.mImage.GetMemoryImage().CreateRenderData();
						this.mHasRenderTargetFlatImage = true;
						this.mRenderTargetFlatImageDirty = false;
						Graphics g = new Graphics(this.mImage.GetImage());
						new Color(0, 0, 0, 255);
						this.mAnim.Draw(g);
						if (removeAnim)
						{
							if (this.mAnim != null)
							{
								this.mAnim.Dispose();
								this.mAnim = null;
							}
							this.mAnim = null;
							this.mAnimActive = false;
							this.mImageOverlayAlpha.SetConstant(1.0);
						}
					}
					else
					{
						if (this.mHasRenderTargetFlatImage)
						{
							this.mHasRenderTargetFlatImage = false;
						}
						this.LoadImageProc();
					}
				}
			}
			return new SharedImageRef(this.mImage);
		}

		public void PrepBackgroundImage()
		{
			SharedImageRef backgroundImage = this.GetBackgroundImage(true, false);
			if (backgroundImage.GetMemoryImage() != null)
			{
				backgroundImage.GetMemoryImage().CreateRenderData();
			}
		}

		public PopAnim GetPopAnim()
		{
			return this.GetPopAnim(true);
		}

		public PopAnim GetPopAnim(bool wait)
		{
			if (this.mAnim == null)
			{
				if (!wait)
				{
					return null;
				}
				if (this.mStage == 1)
				{
					this.mStage++;
					this.LoadAnimProc();
				}
			}
			return this.mAnim;
		}

		public int GetSoundId(string theSampleName)
		{
			string idByPath = GlobalMembers.gApp.mResourceManager.GetIdByPath("sounds\\backgrounds\\" + theSampleName);
			if (idByPath.Length == 0)
			{
				idByPath = GlobalMembers.gApp.mResourceManager.GetIdByPath("sounds\\" + theSampleName);
			}
			int num = GlobalMembers.gApp.mResourceManager.GetSound(idByPath);
			if (num == -1)
			{
				num = GlobalMembers.gApp.mResourceManager.LoadSound(idByPath);
				if (num != -1)
				{
					this.mLoadedSounds.Add(idByPath);
				}
				else
				{
					num = GlobalMembers.gApp.mSoundManager.LoadSound("sounds\\backgrounds\\" + theSampleName);
					if (num != -1)
					{
						this.mDirectLoadedSounds.Add(num);
					}
					else
					{
						num = GlobalMembers.gApp.mSoundManager.LoadSound("sounds\\" + theSampleName);
						if (num != -1)
						{
							this.mDirectLoadedSounds.Add(num);
						}
					}
				}
			}
			return num;
		}

		public void PrecacheResources(PASpriteDef theSpriteDef)
		{
			for (int i = 0; i < theSpriteDef.mFrames.Length; i++)
			{
				PAFrame paframe = theSpriteDef.mFrames[i];
				for (int j = 0; j < paframe.mCommandVector.Length; j++)
				{
					PACommand pacommand = paframe.mCommandVector[j];
					if (pacommand.mCommand.ToUpper() == "PLAYSAMPLE")
					{
						int num = pacommand.mParam.IndexOf(',');
						if (num == -1)
						{
							num = pacommand.mParam.Length;
						}
						string theSampleName = pacommand.mParam.Substring(0, num).Trim();
						this.GetSoundId(theSampleName);
					}
				}
			}
		}

		public virtual void PopAnimPlaySample(string theSampleName, int thePan, double theVolume, double theNumSteps)
		{
			int num = this.GetSoundId(theSampleName);
			if (num == -1)
			{
				num = GlobalMembersResourcesWP.SOUND_START_ROTATE;
			}
			SoundInstance soundInstance = GlobalMembers.gApp.mSoundManager.GetSoundInstance(num);
			if (soundInstance != null)
			{
				soundInstance.SetVolume(theVolume);
				soundInstance.SetPan(thePan);
				soundInstance.AdjustPitch(theNumSteps);
				soundInstance.Play(false, true);
			}
		}

		public virtual bool PopAnimCommand(int theId, PASpriteInst thePASpriteInst, string theCommand, string theParam)
		{
			if (this.mNoParticles && string.Compare(theCommand, "addparticleeffect", 5) == 0)
			{
				return true;
			}
			if (string.Compare(theCommand, "waitForScore", 5) == 0)
			{
				if (this.mScoreWaitLevel > this.mScoreWaitsPassed)
				{
					GlobalMembers.gApp.PlaySample(GlobalMembersResourcesWP.SOUND_BACKGROUND_CHANGE);
					this.mScoreWaitsPassed++;
					GlobalMembers.gApp.mCurveValCache.GetCurvedVal(PreCalculatedCurvedValManager.CURVED_VAL_ID.eBACKGROUND_FLASH, this.mFlash);
				}
				else
				{
					thePASpriteInst.mFrameNum -= 1f;
				}
			}
			else if (string.Compare(theCommand, "waitForever", 5) == 0)
			{
				thePASpriteInst.mFrameNum -= 1f;
			}
			return false;
		}

		public PIEffect PopAnimLoadParticleEffect(string theEffectName)
		{
			throw new NotImplementedException();
		}

		public bool PopAnimObjectPredraw(int theId, Graphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Color theColor)
		{
			throw new NotImplementedException();
		}

		public bool PopAnimObjectPostdraw(int theId, Graphics g, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Color theColor)
		{
			throw new NotImplementedException();
		}

		public ImagePredrawResult PopAnimImagePredraw(int theId, PASpriteInst theSpriteInst, PAObjectInst theObjectInst, PATransform theTransform, Image theImage, Graphics g, int theDrawCount)
		{
			throw new NotImplementedException();
		}

		public void PopAnimStopped(int theId)
		{
			throw new NotImplementedException();
		}

		public void PopAnimCommand(int theId, string theCommand, string theParam)
		{
			throw new NotImplementedException();
		}

		public bool mNoParticles;

		public string mPath = string.Empty;

		public SharedRenderTarget mSharedRenderTarget = new SharedRenderTarget();

		public ResourceRef mResourceImageRef = new ResourceRef();

		public SharedImageRef mImage = new SharedImageRef();

		public BkgPopAnim mAnim;

		public bool mAnimActive;

		public bool mWantAnim;

		public bool mKeepFlatImage;

		public bool mHasRenderTargetFlatImage;

		public bool mRenderTargetFlatImageDirty = true;

		public double mExtraImgScale;

		public double mExtraDrawScale;

		public bool mAllowRealign;

		public bool mAllowRescale;

		public int mStage;

		public List<string> mLoadedImages = new List<string>();

		public List<string> mLoadedSounds = new List<string>();

		public List<int> mDirectLoadedSounds = new List<int>();

		public int mScoreWaitLevel;

		public int mScoreWaitsPassed;

		public float mUpdateAcc;

		public CurvedVal mUpdateSpeed = new CurvedVal();

		public CurvedVal mImageOverlayAlpha = new CurvedVal();

		public CurvedVal mFlash = new CurvedVal();

		public Color mColor = default(Color);

		public string mResourceGroup = string.Empty;
	}
}
