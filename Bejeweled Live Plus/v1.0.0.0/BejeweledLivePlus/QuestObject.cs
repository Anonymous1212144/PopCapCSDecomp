using System;
using System.Collections.Generic;
using System.Linq;
using BejeweledLivePlus;
using BejeweledLivePlus.Misc;
using SexyFramework;
using SexyFramework.Graphics;
using SexyFramework.Misc;

public class QuestObject
{
	public QuestObject(QuestMenu theQuestMenu, int theId, int theCountCompleted)
	{
		this.mQuestMenu = theQuestMenu;
		this.mQuestObjIdx = theId;
		this.mQuestsCompleted = theCountCompleted;
		this.mGrayscaleBase = null;
		this.mGrayscaleAdd = null;
		this.mDrawTris = false;
		this.mDrawStreamerTris = false;
		this.Reset();
	}

	public void Reset()
	{
		this.DeleteGrayscale();
		this.ClearOrbs();
		this.ClearFx(true, true, true, true);
		this.mData = default(QuestObjectData);
		this.mGrayscaleAlpha.SetConstant(0.0);
		this.mTransitionStreamerMag.SetConstant(1.0);
		this.mTransitionStreamerColorShift.SetConstant(0.0);
		this.mGrayscaleAlphaExtra.SetConstant(0.0);
		this.mTransitionRumble.SetConstant(0.0);
		this.mBaseObjAlpha.SetConstant(0.0);
		this.mBaseObjAddAlpha.SetConstant(0.0);
		this.mStreamerMag.SetConstant(1.0);
		this.mStreamerColorShift.SetConstant(0.0);
		this.mBackgroundDarken.SetConstant(0.0);
		this.mMaskAlpha.SetConstant(0.0);
		this.mTransitionTicks = 0;
		this.mNewStreamerDelay = 0;
		this.mClearStreamersAt = 0;
		this.mClearTransStreamersAt = 0;
		this.mAlpha = 0.0;
		this.mUpdateCnt = 0;
		this.mStreamerTgt = 0;
		this.mStreamerStartMag = 0.0;
		this.mGrayscaleAlphaMult = 0.0;
		if (this.mQuestsCompleted >= BejeweledLivePlusAppConstants.QUESTS_PER_SET)
		{
			this.SetAnim(QuestObject.EAnimState.eAnim_Complete);
			return;
		}
		if (this.mQuestsCompleted >= BejeweledLivePlusAppConstants.QUESTS_REQUIRED_PER_SET)
		{
			this.SetAnim(QuestObject.EAnimState.eAnim_Revealed);
			return;
		}
		this.SetAnim(QuestObject.EAnimState.eAnim_Unrevealed);
	}

	public void SetAnim(QuestObject.EAnimState theAnimIdx)
	{
		this.mAnimState = theAnimIdx;
		this.mStreamerStartMag = this.mData.mStreamerStartMag;
		switch (this.mAnimState)
		{
		case QuestObject.EAnimState.eAnim_Unrevealed:
		{
			BorderEffect borderEffect = new BorderEffect(ref this.mData.mMarkerInner, ref this.mData.mMarkerOuter, this.mData.mMarkerLength);
			borderEffect.mPhase.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b30,1,0.005,1,#         ~~"));
			borderEffect.mPhase.SetMode(1);
			borderEffect.mAlpha.SetConstant((double)BejeweledLivePlus.GlobalMembers.M(1f));
			borderEffect.mMultMagnitudeOuter.SetConstant((double)BejeweledLivePlus.GlobalMembers.M(0.7f));
			borderEffect.mMultMagnitudeInner.SetConstant((double)BejeweledLivePlus.GlobalMembers.M(0.8f));
			borderEffect.mId = 0;
			this.mBorderFx.Add(borderEffect);
			this.mTransitionStreamerMag.SetConstant(1.0);
			this.mGlintAlpha.SetConstant(0.0);
			this.mMaskAlpha.SetConstant(BejeweledLivePlus.GlobalMembers.M(0.3));
			if (BejeweledLivePlus.GlobalMembers.M(1) != 0)
			{
				borderEffect = new BorderEffect(ref this.mData.mMarkerInner, ref this.mData.mMarkerOuter, this.mData.mMarkerLength);
				borderEffect.mPhase.SetConstant(1.0);
				borderEffect.mFxOffsetX = BejeweledLivePlus.GlobalMembers.M(0);
				borderEffect.mFxOffsetY = BejeweledLivePlus.GlobalMembers.M(0);
				borderEffect.mAlpha.SetConstant(BejeweledLivePlus.GlobalMembers.M(1.0));
				borderEffect.mMultMagnitudeOuter.SetConstant(BejeweledLivePlus.GlobalMembers.M(0.7));
				borderEffect.mMultMagnitudeInner.SetConstant(BejeweledLivePlus.GlobalMembers.M(0.8));
				borderEffect.mId = 1;
				this.mBorderFx.Add(borderEffect);
			}
			this.mGrayscaleAlphaMult = 0.0;
			this.mBaseObjAlpha.SetConstant(0.0);
			this.mBaseObjAddAlpha.SetConstant(0.0);
			this.mStreamerTgt = BejeweledLivePlus.GlobalMembers.M(150);
			this.mGlintColor = new Color(0, 0);
			this.mClearStreamersAt = 0;
			this.mClearTransStreamersAt = 0;
			break;
		}
		case QuestObject.EAnimState.eAnim_Revealing:
		{
			this.mQuestMenu.mQuestButtonSoundCount = 0;
			this.ClearFx(true, false, true, true);
			this.mQuestMenu.AddDeferredSound(GlobalMembersResourcesWP.SOUND_QUESTMENU_RELICREVEALED_RUMBLE, BejeweledLivePlus.GlobalMembers.M(15), BejeweledLivePlus.GlobalMembers.M(1.0));
			this.mQuestMenu.AddDeferredSound(GlobalMembersResourcesWP.SOUND_QUESTMENU_RELICREVEALED_OBJECT, BejeweledLivePlus.GlobalMembers.M(70), BejeweledLivePlus.GlobalMembers.M(1.0));
			uint num = 0U;
			while ((ulong)num < (ulong)((long)this.mBorderFx.size<BorderEffect>()))
			{
				this.mBorderFx.ToArray()[(int)((UIntPtr)num)].mAlpha.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,1,0.01,2.5,~###     )~###   Z#### @#M%M"));
				num += 1U;
			}
			this.mGlintAlpha.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,0.5,0.01,2,####         ~~###"));
			this.mTransitionRumble.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,14,0.01,1.5,####o####s~###    >~###  r#### .#KZ[#####"));
			this.mGrayscaleAlpha.SetConstant(BejeweledLivePlus.GlobalMembers.M(1.0));
			this.mGrayscaleAlphaExtra.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,1,0.01,4,~###       C~###  ^####"));
			this.mBaseObjAlpha.SetConstant(0.0);
			this.mBaseObjAddAlpha.SetConstant(0.0);
			this.mTransitionStreamerCount.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0,40,0.01,2,#&KR ,+0l:    N~Sz(    I~###"));
			this.mTransitionStreamerMag.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,5,0.01,2.5,6###    3~####~### v####  X#### @####"));
			this.mBackgroundDarken.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,0.5,0.01,2.5,####    T#### v~### z~### X####"));
			this.mTransitionStreamerColorShift.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,1,0.01,2,####        O#### B~###2~###"));
			this.mStreamerColorShift.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0,1,0.01,2.5,####         ~~###"));
			this.mMaskAlpha.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0,0.3,0.01,2,~###       0~### )#### k####"));
			this.mGrayscaleAlphaMult = 0.0;
			this.mStreamerMag.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,5,0.01,2,6###   T~###  $~### ?#### G####u6###o6###"));
			this.mGlintColor = new Color(BejeweledLivePlus.GlobalMembers.M(16711935), BejeweledLivePlus.GlobalMembers.M(0));
			uint num2 = 0U;
			while ((ulong)num2 < (ulong)((long)this.mStreamers.size<StreamerEffect>()))
			{
				this.mStreamers.ToArray()[(int)((UIntPtr)num2)].mColor2 = BejeweledLivePlus.GlobalMembers.M(10505777);
				this.mStreamers.ToArray()[(int)((UIntPtr)num2)].mRotExtra.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,0.5,0.01,2.5,#########         ~~###"));
				num2 += 1U;
			}
			for (int i = 0; i <= BejeweledLivePlus.GlobalMembers.M(2); i++)
			{
				BorderEffect borderEffect2 = new BorderEffect(ref this.mData.mMarkerInner, ref this.mData.mMarkerOuter, this.mData.mMarkerLength);
				borderEffect2.mId = i;
				borderEffect2.mPhase.SetConstant(0.0);
				borderEffect2.mDelayCnt = BejeweledLivePlus.GlobalMembers.M(80) + BejeweledLivePlus.GlobalMembers.M(15) * i;
				if (i == 2)
				{
					borderEffect2.mAlpha.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0,1,0.01,1.5,#,=^   M~SAr     -~P## I#Pij"));
					borderEffect2.mMultMagnitudeOuter.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0.75,10,0.01,0.5,~n5y         ~####"));
					borderEffect2.mMultMagnitudeInner.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+1.0,12.0,0.01,0.5,~kNn         ~####"));
				}
				else
				{
					borderEffect2.mAlpha.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0,1,0.01,1.5,#,=^   M~SAr     -~P## I#Pij"));
					borderEffect2.mMultMagnitudeOuter.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0.75,10,0.01,0.5,~n5y         ~####"));
					borderEffect2.mMultMagnitudeInner.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+1.0,12.0,0.01,0.5,~kNn         ~####"));
				}
				borderEffect2.mBorderGlow = true;
				this.mTransitionBorderFx.Add(borderEffect2);
			}
			this.mTransitionTicks = BejeweledLivePlus.GlobalMembers.M(230);
			this.mNewStreamerDelay = BejeweledLivePlus.GlobalMembers.M(300);
			this.mStreamerTgt = BejeweledLivePlus.GlobalMembers.M(150);
			this.mClearStreamersAt = BejeweledLivePlus.GlobalMembers.M(150);
			this.mClearTransStreamersAt = BejeweledLivePlus.GlobalMembers.M(150);
			break;
		}
		case QuestObject.EAnimState.eAnim_Revealed:
			this.ClearFx(false, true, false, true);
			this.mNewStreamerDelay = 0;
			this.mGrayscaleAlphaMult = 1.0;
			this.mGrayscaleAlpha.SetConstant(1.0);
			this.mGrayscaleAlphaExtra.SetConstant(0.0);
			this.mBaseObjAlpha.SetConstant(0.0);
			this.mBaseObjAddAlpha.SetConstant(0.0);
			this.mMaskAlpha.SetConstant(0.0);
			this.mGlintAlpha.SetConstant(BejeweledLivePlus.GlobalMembers.M(0.5));
			this.mStreamerTgt = BejeweledLivePlus.GlobalMembers.M(150);
			this.mGlintColor = new Color(BejeweledLivePlus.GlobalMembers.M(16711935), BejeweledLivePlus.GlobalMembers.M(0));
			this.mClearStreamersAt = 0;
			this.mClearTransStreamersAt = 0;
			break;
		case QuestObject.EAnimState.eAnim_Completing:
		{
			this.mQuestMenu.mQuestButtonSoundCount = 0;
			this.ClearFx(true, false, true, true);
			this.mQuestMenu.AddDeferredSound(GlobalMembersResourcesWP.SOUND_QUESTMENU_RELICCOMPLETE_RUMBLE, BejeweledLivePlus.GlobalMembers.M(15), BejeweledLivePlus.GlobalMembers.M(1.0));
			this.mQuestMenu.AddDeferredSound(GlobalMembersResourcesWP.SOUND_QUESTMENU_RELICCOMPLETE_OBJECT, BejeweledLivePlus.GlobalMembers.M(280), BejeweledLivePlus.GlobalMembers.M(1.0));
			BorderEffect borderEffect3 = new BorderEffect(ref this.mData.mMarkerInner, ref this.mData.mMarkerOuter, this.mData.mMarkerLength);
			borderEffect3.mX = BejeweledLivePlus.GlobalMembers.M(0);
			borderEffect3.mY = BejeweledLivePlus.GlobalMembers.M(0);
			borderEffect3.mId = 0;
			borderEffect3.mDelayCnt = BejeweledLivePlus.GlobalMembers.M(300);
			this.mTransitionBorderFx.Add(borderEffect3);
			uint num3 = 0U;
			while ((ulong)num3 < (ulong)((long)this.mStreamers.size<StreamerEffect>()))
			{
				this.mStreamers.ToArray()[(int)((UIntPtr)num3)].mColor2 = BejeweledLivePlus.GlobalMembers.M(6644752);
				num3 += 1U;
			}
			this.mNewStreamerDelay = BejeweledLivePlus.GlobalMembers.M(300);
			borderEffect3 = new BorderEffect(ref this.mData.mMarkerInner, ref this.mData.mMarkerOuter, this.mData.mMarkerLength);
			borderEffect3.mPhase.SetConstant(BejeweledLivePlus.GlobalMembers.M(0.25));
			borderEffect3.mSortOrder = BejeweledLivePlus.GlobalMembers.M(1);
			borderEffect3.mMultMagnitudeOuter.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0,2,0.01,4,#### B#### .F###     UF###h#### 7#O:N"));
			borderEffect3.mAlpha.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0,1,0.01,4,####u#### b~###     8~###z#### 7#O:N"));
			this.mTransitionBorderFx.Add(borderEffect3);
			borderEffect3 = new BorderEffect(ref this.mData.mMarkerInner, ref this.mData.mMarkerOuter, this.mData.mMarkerLength);
			borderEffect3.mPhase.SetConstant(BejeweledLivePlus.GlobalMembers.M(0.25));
			borderEffect3.mSortOrder = BejeweledLivePlus.GlobalMembers.M(2);
			borderEffect3.mMultMagnitudeOuter.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0,1.25,0.01,4,####    k####7q###   Kq### T#O:N"));
			borderEffect3.mAlpha.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0,1,0.01,4,####    :#### #~###   1~### X#O:N"));
			this.mTransitionBorderFx.Add(borderEffect3);
			borderEffect3 = new BorderEffect(ref this.mData.mMarkerInner, ref this.mData.mMarkerOuter, this.mData.mMarkerLength);
			borderEffect3.mPhase.SetConstant(BejeweledLivePlus.GlobalMembers.M(0.25));
			borderEffect3.mSortOrder = BejeweledLivePlus.GlobalMembers.M(3);
			borderEffect3.mMultMagnitudeOuter.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0,1.25,0.01,4,####    *####   Y####X}###ll###xlO:N"));
			borderEffect3.mAlpha.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0,1,0.01,4,####    *####   Y####X}###ll###xlO:N"));
			this.mBorderFx.Add(borderEffect3);
			this.mTransitionRumble.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,10,0.01,4,#### q####    l~###_~tu: D#### @######P#######"));
			this.mStreamerMag.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,1,0.01,4,~### (####      6#### q~###s~###"));
			this.mStreamerColorShift.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,1,0.01,2,####        O#### B~###2~###"));
			this.mBackgroundDarken.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,0.5,0.01,4,#########   E~###     &~### X####"));
			this.mMaskAlpha.SetConstant(0.0);
			this.mGlintColor = new Color(BejeweledLivePlus.GlobalMembers.M(16777215), BejeweledLivePlus.GlobalMembers.M(0));
			this.mBaseObjAlpha.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,1,0.01,3,####   I####    k~### k~####~###"));
			this.mBaseObjAddAlpha.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,0.5,0.01,5,####   &####   [~###j~###  V####"));
			this.mGrayscaleAlpha.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,1,0.01,3,~###       =~###  d####"));
			this.mGrayscaleAlphaExtra.SetConstant(0.0);
			this.mGrayscaleAlphaMult = 1.0;
			this.mTransitionStreamerCount.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b+0,120,0.01,4,#&KQ  (52W)     I~####~Sz(  R~###"));
			this.mTransitionStreamerMag.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,5,0.01,3.5,6###  v~###   ,~###  <~### d####"));
			this.mTransitionStreamerColorShift.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,1,0.01,2.5,#########      r####  {~###2~###"));
			this.mTransitionTicks = BejeweledLivePlus.GlobalMembers.M(430);
			this.mNewStreamerDelay = BejeweledLivePlus.GlobalMembers.M(300);
			this.mStreamerTgt = BejeweledLivePlus.GlobalMembers.M(150);
			this.mClearStreamersAt = BejeweledLivePlus.GlobalMembers.M(0);
			this.mClearTransStreamersAt = BejeweledLivePlus.GlobalMembers.M(300);
			break;
		}
		case QuestObject.EAnimState.eAnim_Complete:
		{
			this.ClearFx(false, true, false, true);
			BorderEffect borderEffect4 = new BorderEffect(ref this.mData.mMarkerInner, ref this.mData.mMarkerOuter, this.mData.mMarkerLength);
			borderEffect4.mPhase.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b30,1,0.01,1,#         ~~"));
			borderEffect4.mId = 0;
			this.mBorderFx.Add(borderEffect4);
			this.mNewStreamerDelay = 0;
			this.mStreamerTgt = BejeweledLivePlus.GlobalMembers.M(70);
			this.mGlintColor = new Color(BejeweledLivePlus.GlobalMembers.M(16777215), BejeweledLivePlus.GlobalMembers.M(0));
			this.mMaskAlpha.SetConstant(0.0);
			this.mBaseObjAlpha.SetConstant(1.0);
			this.mGrayscaleAlphaMult = 0.0;
			this.mClearStreamersAt = 0;
			this.mClearTransStreamersAt = 0;
			break;
		}
		}
		if (this.mAnimState == QuestObject.EAnimState.eAnim_Revealing)
		{
			this.mQuestMenu.mQuestObjTransitionBtnHidePct.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,1,0.009375,3,#8E- q~###      2~hcW |#ArI"));
			return;
		}
		if (this.mAnimState == QuestObject.EAnimState.eAnim_Completing)
		{
			if (this.mQuestMenu.GetCompleteCount() == BejeweledLivePlusAppConstants.NUM_QUEST_SETS * BejeweledLivePlusAppConstants.QUESTS_PER_SET)
			{
				this.mQuestMenu.mQuestObjTransitionBtnHidePct.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,1,0.009375,3,#8E- q~###        0~ArI"));
				return;
			}
			this.mQuestMenu.mQuestObjTransitionBtnHidePct.SetCurve(BejeweledLivePlus.GlobalMembers.MP("b;0,1,0.01,5,#8E- 0~###      s~hcW |#ArI"));
		}
	}

	public void DoAwardTransition()
	{
		if (this.mAnimState == QuestObject.EAnimState.eAnim_Unrevealed)
		{
			this.SetAnim(QuestObject.EAnimState.eAnim_Revealing);
		}
		if (this.mAnimState == QuestObject.EAnimState.eAnim_Revealed)
		{
			this.SetAnim(QuestObject.EAnimState.eAnim_Completing);
		}
	}

	public void ClearFx(bool theTransition, bool theNormal, bool theStreamers, bool theBorderFx)
	{
		if (theTransition)
		{
			if (theStreamers)
			{
				uint num = 0U;
				while ((ulong)num < (ulong)((long)Enumerable.Count<StreamerEffect>(this.mTransitionStreamers)))
				{
					this.mTransitionStreamers.ToArray()[(int)((UIntPtr)num)] = null;
					num += 1U;
				}
				this.mTransitionStreamers.Clear();
			}
			if (theBorderFx)
			{
				uint num2 = 0U;
				while ((ulong)num2 < (ulong)((long)Enumerable.Count<BorderEffect>(this.mTransitionBorderFx)))
				{
					this.mTransitionBorderFx.ToArray()[(int)((UIntPtr)num2)] = null;
					num2 += 1U;
				}
				this.mTransitionBorderFx.Clear();
			}
		}
		if (theNormal)
		{
			if (theStreamers)
			{
				uint num3 = 0U;
				while ((ulong)num3 < (ulong)((long)Enumerable.Count<StreamerEffect>(this.mStreamers)))
				{
					this.mStreamers.ToArray()[(int)((UIntPtr)num3)] = null;
					num3 += 1U;
				}
				this.mStreamers.Clear();
			}
			if (theBorderFx)
			{
				uint num4 = 0U;
				while ((ulong)num4 < (ulong)((long)Enumerable.Count<BorderEffect>(this.mBorderFx)))
				{
					this.mBorderFx.ToArray()[(int)((UIntPtr)num4)] = null;
					num4 += 1U;
				}
				this.mBorderFx.Clear();
			}
		}
	}

	public bool IsBusy()
	{
		return Enumerable.Count<FloatyOrb>(this.mFloatyOrbs) != 0 || this.mAnimState == QuestObject.EAnimState.eAnim_Revealing || this.mAnimState == QuestObject.EAnimState.eAnim_Completing;
	}

	public void Draw(Graphics g, int theX, int theY)
	{
	}

	public void DrawOverlay(Graphics g, int theX, int theY)
	{
	}

	public void Update()
	{
	}

	public void AddStreamer(bool theIsTransition)
	{
	}

	public void RefreshEmitters()
	{
		uint num = 0U;
		while ((ulong)num < (ulong)((long)this.mBorderFx.size<BorderEffect>()))
		{
			this.mBorderFx.ToArray()[(int)((UIntPtr)num)].RefreshEmitters();
			num += 1U;
		}
	}

	public void ClearOrbs()
	{
		uint num = 0U;
		while ((ulong)num < (ulong)((long)Enumerable.Count<FloatyOrb>(this.mFloatyOrbs)))
		{
			this.mFloatyOrbs.ToArray()[(int)((UIntPtr)num)] = null;
			num += 1U;
		}
		this.mFloatyOrbs.Clear();
	}

	public void DeleteGrayscale()
	{
		this.mGrayscaleBase = null;
		this.mGrayscaleBaseComplete = 0f;
		this.mGrayscaleAdd = null;
		this.mGrayscaleAddComplete = 0f;
	}

	public bool CheckForTransition()
	{
		bool flag = Enumerable.Count<FloatyOrb>(this.mFloatyOrbs) == 0;
		if (flag)
		{
			if (this.mAnimState == QuestObject.EAnimState.eAnim_Unrevealed)
			{
				if (this.mQuestsCompleted >= BejeweledLivePlusAppConstants.QUESTS_REQUIRED_PER_SET)
				{
					this.SetAnim(QuestObject.EAnimState.eAnim_Revealing);
					return true;
				}
			}
			else if (this.mAnimState == QuestObject.EAnimState.eAnim_Revealing)
			{
				this.mTransitionTicks--;
				if (this.mTransitionTicks <= 0)
				{
					this.SetAnim(QuestObject.EAnimState.eAnim_Revealed);
					return true;
				}
			}
			else if (this.mAnimState == QuestObject.EAnimState.eAnim_Revealed)
			{
				if (this.mQuestsCompleted >= BejeweledLivePlusAppConstants.QUESTS_OPTIONAL_PER_SET + BejeweledLivePlusAppConstants.QUESTS_REQUIRED_PER_SET)
				{
					this.SetAnim(QuestObject.EAnimState.eAnim_Completing);
					return true;
				}
			}
			else if (this.mAnimState == QuestObject.EAnimState.eAnim_Completing)
			{
				this.mTransitionTicks--;
				if (this.mTransitionTicks <= 0)
				{
					this.SetAnim(QuestObject.EAnimState.eAnim_Complete);
					return true;
				}
			}
		}
		return false;
	}

	public bool IsComplete()
	{
		return this.mAnimState >= QuestObject.EAnimState.eAnim_Completing;
	}

	public float UpdateGrayscales(float theUpdatePct)
	{
		QuestObject.ImageConfig[] array = new QuestObject.ImageConfig[]
		{
			new QuestObject.ImageConfig(this.mGrayscaleBase, this.mGrayscaleBaseComplete, BejeweledLivePlus.GlobalMembers.M(2f), BejeweledLivePlus.GlobalMembers.M(0.5f)),
			new QuestObject.ImageConfig(this.mGrayscaleAdd, this.mGrayscaleAddComplete, BejeweledLivePlus.GlobalMembers.M(1.1f), BejeweledLivePlus.GlobalMembers.M(4f))
		};
		for (int i = 0; i < 2; i++)
		{
			QuestObject.ImageConfig imageConfig = array[i];
			if (theUpdatePct > 0f && imageConfig.mCompletePct < 1f)
			{
				MemoryImage memoryImage = (MemoryImage)GlobalMembersResourcesWP.GetImageById(this.mData.mObjImageId);
				if (imageConfig.mDestImage != null)
				{
					imageConfig.mDestImage = new MemoryImage();
					imageConfig.mDestImage.mPurgeBits = true;
					imageConfig.mDestImage.SetImageMode(true, true);
					imageConfig.mDestImage.Create(memoryImage.GetWidth(), memoryImage.GetHeight());
				}
				float num = Math.Min(1f - imageConfig.mCompletePct, theUpdatePct);
				this.GenGrayscaleImage(memoryImage, imageConfig.mDestImage, imageConfig.mIntensity, imageConfig.mContrast, imageConfig.mCompletePct, imageConfig.mCompletePct + num);
				imageConfig.mCompletePct += num;
				theUpdatePct -= num;
				float mCompletePct = imageConfig.mCompletePct;
			}
		}
		return theUpdatePct;
	}

	public void GenGrayscaleImage(MemoryImage theImage, MemoryImage theOutImage)
	{
		this.GenGrayscaleImage(theImage, theOutImage, 1f, 1f, 1f, 1f);
	}

	public void GenGrayscaleImage(MemoryImage theImage, MemoryImage theOutImage, float theIntensity)
	{
		this.GenGrayscaleImage(theImage, theOutImage, theIntensity, 1f, 1f, 1f);
	}

	public void GenGrayscaleImage(MemoryImage theImage, MemoryImage theOutImage, float theIntensity, float theContrast)
	{
		this.GenGrayscaleImage(theImage, theOutImage, theIntensity, theContrast, 1f, 1f);
	}

	public void GenGrayscaleImage(MemoryImage theImage, MemoryImage theOutImage, float theIntensity, float theContrast, float theStartPct)
	{
		this.GenGrayscaleImage(theImage, theOutImage, theIntensity, theContrast, theStartPct, 1f);
	}

	public void GenGrayscaleImage(MemoryImage theImage, MemoryImage theOutImage, float theIntensity, float theContrast, float theStartPct, float theTgtPct)
	{
		uint[] bits = theImage.GetBits();
		uint[] bits2 = theOutImage.GetBits();
		int num = theImage.GetWidth() * theImage.GetHeight();
		int num2 = (int)(theStartPct * (float)num);
		int num3 = (int)(theTgtPct * (float)num);
		for (int i = num2; i < num3; i++)
		{
			int num4 = (int)(((bits[i] >> 16) & 255U) * 0.3 * (double)theIntensity) + (int)(((bits[i] >> 8) & 255U) * 0.59 * (double)theIntensity) + (int)((bits[i] & 255U) * 0.11 * (double)theIntensity);
			if (theContrast != 1f)
			{
				num4 = (int)(Math.Pow((double)((float)num4 / 255f), (double)theContrast) * 255.0);
			}
			num4 = Math.Min(255, num4);
			bits2[i] = (uint)((ulong)((bits[i] & 4278190080U) | (uint)((uint)num4 << 16) | (uint)((uint)num4 << 8)) | (ulong)((long)num4));
		}
	}

	private bool FindImgOrComplain(string theImgName, ref int outId)
	{
		outId = (int)GlobalMembersResourcesWP.GetIdByStringId(theImgName);
		return outId != -1;
	}

	public void DrawGrayscaleImage(Graphics g, double theBaseAlpha, double theAddAlpha, double theGemAlpha)
	{
		this.UpdateGrayscales(10f);
		if (this.mGrayscaleAddComplete != 1f && this.mGrayscaleBaseComplete != 1f)
		{
			return;
		}
		g.SetColorizeImages(true);
		if (theBaseAlpha > 0.0)
		{
			g.SetColor(new Color(BejeweledLivePlus.GlobalMembers.M(11167487), (int)(theBaseAlpha * 255.0)));
			Utils.DrawImageCentered(g, this.mGrayscaleBase, (float)BejeweledLivePlus.GlobalMembers.S(this.mData.mArtOffsetX), (float)BejeweledLivePlus.GlobalMembers.S(this.mData.mArtOffsetY));
		}
		if (theAddAlpha > 0.0)
		{
			g.SetDrawMode(Graphics.DrawMode.Additive);
			g.SetColor(new Color(BejeweledLivePlus.GlobalMembers.M(14527231), (int)(theAddAlpha * 255.0)));
			Utils.DrawImageCentered(g, this.mGrayscaleAdd, (float)BejeweledLivePlus.GlobalMembers.S(this.mData.mArtOffsetX), (float)BejeweledLivePlus.GlobalMembers.S(this.mData.mArtOffsetY));
			g.SetDrawMode(Graphics.DrawMode.Normal);
		}
		g.SetColor(new Color(-1));
		g.SetColorizeImages(false);
		if (theGemAlpha > 0.0)
		{
			this.TranslateToPsd(g);
			g.SetColor(new Color(BejeweledLivePlus.GlobalMembers.M(16777215), (int)(theGemAlpha * 255.0)));
			for (int i = 0; i < BejeweledLivePlusAppConstants.QUESTS_REQUIRED_PER_SET; i++)
			{
				string theImgName = string.Format("IMAGE_QUESTOBJECT_{0}_GREEN_GLASS_{1}", BejeweledLivePlus.GlobalMembers.gObjectImgs[this.mQuestObjIdx], i + 1);
				int theId = 0;
				if (this.FindImgOrComplain(theImgName, ref theId))
				{
					g.DrawImage(GlobalMembersResourcesWP.GetImageById(theId), (int)BejeweledLivePlus.GlobalMembers.S(GlobalMembersResourcesWP.ImgXOfs(theId)), (int)BejeweledLivePlus.GlobalMembers.S(GlobalMembersResourcesWP.ImgYOfs(theId)));
				}
			}
		}
	}

	public bool WantGrayscale()
	{
		return this.mAnimState == QuestObject.EAnimState.eAnim_Revealed || this.mAnimState == QuestObject.EAnimState.eAnim_Revealing || (this.mAnimState == QuestObject.EAnimState.eAnim_Unrevealed && this.mQuestsCompleted == BejeweledLivePlusAppConstants.QUESTS_REQUIRED_PER_SET - 1);
	}

	public void TranslateToPsd(Graphics g)
	{
		this.mData = default(QuestObjectData);
		Image imageById = GlobalMembersResourcesWP.GetImageById(this.mData.mObjImageId);
		g.Translate((int)BejeweledLivePlus.GlobalMembers.S((float)this.mData.mArtOffsetX - GlobalMembersResourcesWP.ImgXOfs(this.mData.mObjImageId)) - imageById.GetWidth() / 2, (int)BejeweledLivePlus.GlobalMembers.S((float)this.mData.mArtOffsetY - GlobalMembersResourcesWP.ImgYOfs(this.mData.mObjImageId)) - imageById.GetHeight() / 2);
	}

	public QuestObject.EAnimState mAnimState;

	public CurvedVal mTransitionStreamerCount;

	public CurvedVal mTransitionRumble;

	public CurvedVal mGrayscaleAlpha;

	public CurvedVal mGrayscaleAlphaExtra;

	public CurvedVal mBaseObjAlpha;

	public CurvedVal mBaseObjAddAlpha;

	public CurvedVal mTransitionStreamerMag;

	public CurvedVal mStreamerMag;

	public CurvedVal mBackgroundDarken;

	public CurvedVal mTransitionStreamerColorShift;

	public CurvedVal mStreamerColorShift;

	public CurvedVal mGlintAlpha;

	public CurvedVal mStreamerExtraRot;

	public CurvedVal mMaskAlpha;

	public QuestMenu mQuestMenu;

	public double mGrayscaleAlphaMult;

	public int mTransitionTicks;

	public int mNewStreamerDelay;

	public int mStreamerTgt;

	public int mClearStreamersAt;

	public int mClearTransStreamersAt;

	public int mUpdateCnt;

	public Point mRumbleOff;

	public Color mGlintColor;

	public double mAlpha;

	public double mStreamerStartMag;

	public QuestObjectData mData;

	public bool mDrawTris;

	public bool mDrawStreamerTris;

	public MemoryImage mGrayscaleBase;

	public MemoryImage mGrayscaleAdd;

	public float mGrayscaleBaseComplete;

	public float mGrayscaleAddComplete;

	public List<FloatyOrb> mFloatyOrbs = new List<FloatyOrb>();

	public int mQuestsCompleted;

	public List<BorderEffect> mBorderFx = new List<BorderEffect>();

	public List<BorderEffect> mTransitionBorderFx = new List<BorderEffect>();

	public List<StreamerEffect> mTransitionStreamers = new List<StreamerEffect>();

	public List<StreamerEffect> mStreamers = new List<StreamerEffect>();

	private int mQuestObjIdx;

	private double mTransitionGlowAlpha;

	public enum EAnimState
	{
		eAnim_Unrevealed,
		eAnim_Revealing,
		eAnim_Revealed,
		eAnim_Completing,
		eAnim_Complete
	}

	public struct ImageConfig
	{
		public ImageConfig(MemoryImage img, float cmp, float iten, float com)
		{
			this.mDestImage = img;
			this.mCompletePct = cmp;
			this.mIntensity = iten;
			this.mContrast = com;
		}

		public MemoryImage mDestImage;

		public float mCompletePct;

		public float mIntensity;

		public float mContrast;
	}
}
