using System;
using System.Collections.Generic;
using SexyFramework.Graphics;
using SexyFramework.Misc;
using SexyFramework.Resource;
using SexyFramework.Widget;

namespace BejeweledLivePlus
{
	public static class GlobalMembersResourcesWP
	{
		public static void InitResourceManager(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager, false);
		}

		public static void InitResourceManager(ResourceManager theManager, bool forceRun)
		{
			if (GlobalMembersResourcesWP.InitResourceManager_sAlreadyRun && !forceRun)
			{
				return;
			}
			GlobalMembersResourcesWP.InitResourceManager_sAlreadyRun = true;
			for (int i = 0; i < 1810; i++)
			{
				ResGlobalPtr resGlobalPtr = theManager.RegisterGlobalPtr(GlobalMembersResourcesWP.GetStringIdById(i));
				GlobalMembersResourcesWP.gResources[i] = resGlobalPtr;
				GlobalMembersResourcesWP.gImgOffsets[i] = theManager.GetImageOffset(GlobalMembersResourcesWP.GetStringIdById(i));
			}
		}

		public static Image LoadImageById(ResourceManager theManager, int theId)
		{
			if (theId == -1)
			{
				return null;
			}
			Image image = theManager.LoadImage(GlobalMembersResourcesWP.GetStringIdById(theId)).GetImage();
			lock (GlobalMembersResourcesWP.gVarToIdMapCrit)
			{
				GlobalMembersResourcesWP.gVarToIdMap.Add(image, theId);
			}
			GlobalMembersResourcesWP.gResources[theId].mResObject = image;
			return image;
		}

		public static void ReplaceImageById(ResourceManager theManager, int theId, Image theImage)
		{
			if (theId == -1)
			{
				return;
			}
			theManager.ReplaceImage(GlobalMembersResourcesWP.GetStringIdById(theId), theImage);
			lock (GlobalMembersResourcesWP.gVarToIdMapCrit)
			{
				GlobalMembersResourcesWP.gVarToIdMap.Add(theImage, theId);
			}
			GlobalMembersResourcesWP.gResources[theId].mResObject = theImage;
		}

		public static bool ExtractResourcesByName(ResourceManager theManager, string theName)
		{
			if (string.Compare(theName, "AwardGlow") == 0)
			{
				return GlobalMembersResourcesWP.ExtractAwardGlowResources(theManager);
			}
			if (string.Compare(theName, "AwardGlow_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractAwardGlow_480Resources(theManager);
			}
			if (string.Compare(theName, "AwardGlow_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractAwardGlow_960Resources(theManager);
			}
			if (string.Compare(theName, "Badges") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBadgesResources(theManager);
			}
			if (string.Compare(theName, "Badges_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBadges_480Resources(theManager);
			}
			if (string.Compare(theName, "Badges_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBadges_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ANNIHILATOR") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ANNIHILATORResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ANNIHILATOR_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ANNIHILATOR_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ANNIHILATOR_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ANNIHILATOR_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ANTE_UP") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ANTE_UPResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ANTE_UP_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ANTE_UP_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ANTE_UP_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ANTE_UP_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BEJEWELER") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BEJEWELERResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BEJEWELER_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BEJEWELER_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BEJEWELER_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BEJEWELER_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BLASTER") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BLASTERResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BLASTER_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BLASTER_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BLASTER_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BLASTER_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BRONZE") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BRONZEResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BRONZE_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BRONZE_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BRONZE_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BRONZE_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BUTTERFLY_BONANZA") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BUTTERFLY_BONANZAResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BUTTERFLY_BONANZA_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BUTTERFLY_BONANZA_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BUTTERFLY_BONANZA_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BUTTERFLY_BONANZA_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BUTTERFLY_MONARCH") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BUTTERFLY_MONARCHResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BUTTERFLY_MONARCH_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BUTTERFLY_MONARCH_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_BUTTERFLY_MONARCH_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_BUTTERFLY_MONARCH_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_CHAIN_REACTION") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_CHAIN_REACTIONResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_CHAIN_REACTION_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_CHAIN_REACTION_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_CHAIN_REACTION_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_CHAIN_REACTION_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_CHROMATIC") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_CHROMATICResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_CHROMATIC_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_CHROMATIC_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_CHROMATIC_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_CHROMATIC_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_DIAMOND_MINE") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_DIAMOND_MINEResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_DIAMOND_MINE_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_DIAMOND_MINE_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_DIAMOND_MINE_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_DIAMOND_MINE_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_DYNAMO") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_DYNAMOResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_DYNAMO_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_DYNAMO_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_DYNAMO_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_DYNAMO_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ELECTRIFIER") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ELECTRIFIERResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ELECTRIFIER_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ELECTRIFIER_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ELECTRIFIER_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ELECTRIFIER_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ELITE") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ELITEResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ELITE_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ELITE_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ELITE_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ELITE_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_GLACIAL_EXPLORER") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_GLACIAL_EXPLORERResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_GLACIAL_EXPLORER_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_GLACIAL_EXPLORER_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_GLACIAL_EXPLORER_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_GLACIAL_EXPLORER_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_GOLD") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_GOLDResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_GOLD_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_GOLD_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_GOLD_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_GOLD_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_HEROES_WELCOME") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_HEROES_WELCOMEResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_HEROES_WELCOME_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_HEROES_WELCOME_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_HEROES_WELCOME_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_HEROES_WELCOME_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_HIGH_VOLTAGE") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_HIGH_VOLTAGEResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_HIGH_VOLTAGE_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_HIGH_VOLTAGE_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_HIGH_VOLTAGE_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_HIGH_VOLTAGE_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ICE_BREAKER") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ICE_BREAKERResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ICE_BREAKER_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ICE_BREAKER_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_ICE_BREAKER_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_ICE_BREAKER_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_INFERNO") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_INFERNOResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_INFERNO_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_INFERNO_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_INFERNO_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_INFERNO_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_LEVELORD") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_LEVELORDResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_LEVELORD_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_LEVELORD_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_LEVELORD_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_LEVELORD_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_LUCKY_STREAK") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_LUCKY_STREAKResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_LUCKY_STREAK_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_LUCKY_STREAK_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_LUCKY_STREAK_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_LUCKY_STREAK_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_MILLIONAIRE") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_MILLIONAIREResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_MILLIONAIRE_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_MILLIONAIRE_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_MILLIONAIRE_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_MILLIONAIRE_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_PLATINUM") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_PLATINUMResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_PLATINUM_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_PLATINUM_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_PLATINUM_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_PLATINUM_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_RELIC_HUNTER") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_RELIC_HUNTERResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_RELIC_HUNTER_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_RELIC_HUNTER_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_RELIC_HUNTER_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_RELIC_HUNTER_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_SILVER") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_SILVERResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_SILVER_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_SILVER_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_SILVER_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_SILVER_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_STELLAR") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_STELLARResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_STELLAR_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_STELLAR_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_STELLAR_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_STELLAR_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_SUPERSTAR") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_SUPERSTARResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_SUPERSTAR_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_SUPERSTAR_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_SUPERSTAR_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_SUPERSTAR_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_THE_GAMBLER") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_THE_GAMBLERResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_THE_GAMBLER_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_THE_GAMBLER_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_THE_GAMBLER_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_THE_GAMBLER_960Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_TOP_SECRET") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_TOP_SECRETResources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_TOP_SECRET_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_TOP_SECRET_480Resources(theManager);
			}
			if (string.Compare(theName, "BADGES_BIG_TOP_SECRET_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractBADGES_BIG_TOP_SECRET_960Resources(theManager);
			}
			if (string.Compare(theName, "Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractCommonResources(theManager);
			}
			if (string.Compare(theName, "Common_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractCommon_480Resources(theManager);
			}
			if (string.Compare(theName, "Common_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractCommon_960Resources(theManager);
			}
			if (string.Compare(theName, "Common_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractCommon_CommonResources(theManager);
			}
			if (string.Compare(theName, "Common_DEDE") == 0)
			{
				return GlobalMembersResourcesWP.ExtractCommon_DEDEResources(theManager);
			}
			if (string.Compare(theName, "Common_ENUS") == 0)
			{
				return GlobalMembersResourcesWP.ExtractCommon_ENUSResources(theManager);
			}
			if (string.Compare(theName, "Common_ESES") == 0)
			{
				return GlobalMembersResourcesWP.ExtractCommon_ESESResources(theManager);
			}
			if (string.Compare(theName, "Common_FRFR") == 0)
			{
				return GlobalMembersResourcesWP.ExtractCommon_FRFRResources(theManager);
			}
			if (string.Compare(theName, "Common_ITIT") == 0)
			{
				return GlobalMembersResourcesWP.ExtractCommon_ITITResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_BridgeShroomCastle") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_BridgeShroomCastleResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_BridgeShroomCastle_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_BridgeShroomCastle_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_BridgeShroomCastle_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_BridgeShroomCastle_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_Cave") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_CaveResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_Cave_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_Cave_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_Cave_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_Cave_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_CrystalTowers") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_CrystalTowersResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_CrystalTowers_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_CrystalTowers_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_CrystalTowers_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_CrystalTowers_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_DaveCaveThing") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_DaveCaveThingResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_DaveCaveThing_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_DaveCaveThing_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_DaveCaveThing_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_DaveCaveThing_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_Desert") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_DesertResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_Desert_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_Desert_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_Desert_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_Desert_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_FloatingRockCity") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_FloatingRockCityResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_FloatingRockCity_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_FloatingRockCity_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_FloatingRockCity_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_FloatingRockCity_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_FlyingSailBoat") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_FlyingSailBoatResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_FlyingSailBoat_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_FlyingSailBoat_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_FlyingSailBoat_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_FlyingSailBoat_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_LanternPlantsWorld") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_LanternPlantsWorldResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_LanternPlantsWorld_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_LanternPlantsWorld_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_LanternPlantsWorld_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_LanternPlantsWorld_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_LionTowerCascade") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_LionTowerCascadeResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_LionTowerCascade_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_LionTowerCascade_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_LionTowerCascade_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_LionTowerCascade_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_LionTowerCascadeBfly") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_LionTowerCascadeBflyResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_LionTowerCascadeBfly_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_LionTowerCascadeBfly_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_LionTowerCascadeBfly_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_LionTowerCascadeBfly_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_PointyIcePath") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_PointyIcePathResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_PointyIcePath_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_PointyIcePath_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_PointyIcePath_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_PointyIcePath_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_Poker") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_PokerResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_Poker_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_Poker_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_Poker_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_Poker_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_SnowyCliffsCastle") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_SnowyCliffsCastleResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_SnowyCliffsCastle_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_SnowyCliffsCastle_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_SnowyCliffsCastle_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_SnowyCliffsCastle_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_TubeForestNight") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_TubeForestNightResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_TubeForestNight_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_TubeForestNight_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_TubeForestNight_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_TubeForestNight_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_WaterBubblesCity") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_WaterBubblesCityResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_WaterBubblesCity_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_WaterBubblesCity_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_WaterBubblesCity_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_WaterBubblesCity_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_WaterFallCliff") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_WaterFallCliffResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_WaterFallCliff_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_WaterFallCliff_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_WaterFallCliff_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_WaterFallCliff_960Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_WaterPathRuins") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_WaterPathRuinsResources(theManager);
			}
			if (string.Compare(theName, "FlatBG_WaterPathRuins_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_WaterPathRuins_480Resources(theManager);
			}
			if (string.Compare(theName, "FlatBG_WaterPathRuins_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractFlatBG_WaterPathRuins_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlay") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayResources(theManager);
			}
			if (string.Compare(theName, "GamePlay_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlay_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlay_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlay_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlay_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlay_CommonResources(theManager);
			}
			if (string.Compare(theName, "GamePlay_UI_Dig") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlay_UI_DigResources(theManager);
			}
			if (string.Compare(theName, "GamePlay_UI_Dig_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlay_UI_Dig_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlay_UI_Dig_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlay_UI_Dig_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlay_UI_Dig_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlay_UI_Dig_CommonResources(theManager);
			}
			if (string.Compare(theName, "GamePlay_UI_Normal") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlay_UI_NormalResources(theManager);
			}
			if (string.Compare(theName, "GamePlay_UI_Normal_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlay_UI_Normal_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlay_UI_Normal_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlay_UI_Normal_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Balance") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_BalanceResources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Balance_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Balance_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Balance_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Balance_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Butterfly") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_ButterflyResources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Butterfly_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Butterfly_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Butterfly_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Butterfly_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Butterfly_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Butterfly_CommonResources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Dig") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_DigResources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Dig_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Dig_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Dig_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Dig_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Filler") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_FillerResources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Filler_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Filler_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Filler_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Filler_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Inferno") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_InfernoResources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Inferno_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Inferno_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Inferno_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Inferno_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Inferno_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Inferno_CommonResources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Lightning") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_LightningResources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Lightning_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Lightning_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Lightning_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Lightning_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Poker") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_PokerResources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Poker_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Poker_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Poker_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Poker_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_TimeBomb") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_TimeBombResources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_TimeBomb_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_TimeBomb_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_TimeBomb_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_TimeBomb_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_TimeLimit") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_TimeLimitResources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_TimeLimit_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_TimeLimit_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_TimeLimit_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_TimeLimit_960Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Wallblast") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_WallblastResources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Wallblast_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Wallblast_480Resources(theManager);
			}
			if (string.Compare(theName, "GamePlayQuest_Wallblast_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGamePlayQuest_Wallblast_960Resources(theManager);
			}
			if (string.Compare(theName, "GiftGame") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGiftGameResources(theManager);
			}
			if (string.Compare(theName, "GiftGame_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGiftGame_480Resources(theManager);
			}
			if (string.Compare(theName, "GiftGame_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractGiftGame_960Resources(theManager);
			}
			if (string.Compare(theName, "Help_Basic") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_BasicResources(theManager);
			}
			if (string.Compare(theName, "Help_Basic_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Basic_480Resources(theManager);
			}
			if (string.Compare(theName, "Help_Basic_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Basic_960Resources(theManager);
			}
			if (string.Compare(theName, "Help_Basic_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Basic_CommonResources(theManager);
			}
			if (string.Compare(theName, "Help_Bfly") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_BflyResources(theManager);
			}
			if (string.Compare(theName, "Help_Bfly_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Bfly_480Resources(theManager);
			}
			if (string.Compare(theName, "Help_Bfly_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Bfly_960Resources(theManager);
			}
			if (string.Compare(theName, "Help_Bfly_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Bfly_CommonResources(theManager);
			}
			if (string.Compare(theName, "Help_DiamondMine") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_DiamondMineResources(theManager);
			}
			if (string.Compare(theName, "Help_DiamondMine_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_DiamondMine_480Resources(theManager);
			}
			if (string.Compare(theName, "Help_DiamondMine_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_DiamondMine_960Resources(theManager);
			}
			if (string.Compare(theName, "Help_DiamondMine_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_DiamondMine_CommonResources(theManager);
			}
			if (string.Compare(theName, "Help_IceStorm") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_IceStormResources(theManager);
			}
			if (string.Compare(theName, "Help_IceStorm_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_IceStorm_480Resources(theManager);
			}
			if (string.Compare(theName, "Help_IceStorm_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_IceStorm_960Resources(theManager);
			}
			if (string.Compare(theName, "Help_IceStorm_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_IceStorm_CommonResources(theManager);
			}
			if (string.Compare(theName, "Help_Lightning") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_LightningResources(theManager);
			}
			if (string.Compare(theName, "Help_Lightning_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Lightning_480Resources(theManager);
			}
			if (string.Compare(theName, "Help_Lightning_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Lightning_960Resources(theManager);
			}
			if (string.Compare(theName, "Help_Lightning_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Lightning_CommonResources(theManager);
			}
			if (string.Compare(theName, "Help_Poker") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_PokerResources(theManager);
			}
			if (string.Compare(theName, "Help_Poker_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Poker_480Resources(theManager);
			}
			if (string.Compare(theName, "Help_Poker_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Poker_960Resources(theManager);
			}
			if (string.Compare(theName, "Help_Poker_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Poker_CommonResources(theManager);
			}
			if (string.Compare(theName, "Help_Unused") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_UnusedResources(theManager);
			}
			if (string.Compare(theName, "Help_Unused_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Unused_480Resources(theManager);
			}
			if (string.Compare(theName, "Help_Unused_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Unused_960Resources(theManager);
			}
			if (string.Compare(theName, "Help_Unused_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHelp_Unused_CommonResources(theManager);
			}
			if (string.Compare(theName, "HiddenObject") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHiddenObjectResources(theManager);
			}
			if (string.Compare(theName, "HiddenObject_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHiddenObject_480Resources(theManager);
			}
			if (string.Compare(theName, "HiddenObject_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHiddenObject_960Resources(theManager);
			}
			if (string.Compare(theName, "HyperspaceWhirlpool_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_CommonResources(theManager);
			}
			if (string.Compare(theName, "HyperspaceWhirlpool_Common_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_Common_480Resources(theManager);
			}
			if (string.Compare(theName, "HyperspaceWhirlpool_Common_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_Common_960Resources(theManager);
			}
			if (string.Compare(theName, "HyperspaceWhirlpool_Normal") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_NormalResources(theManager);
			}
			if (string.Compare(theName, "HyperspaceWhirlpool_Normal_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_Normal_480Resources(theManager);
			}
			if (string.Compare(theName, "HyperspaceWhirlpool_Normal_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_Normal_960Resources(theManager);
			}
			if (string.Compare(theName, "HyperspaceWhirlpool_ZEN") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_ZENResources(theManager);
			}
			if (string.Compare(theName, "HyperspaceWhirlpool_ZEN_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_ZEN_480Resources(theManager);
			}
			if (string.Compare(theName, "HyperspaceWhirlpool_ZEN_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_ZEN_960Resources(theManager);
			}
			if (string.Compare(theName, "Ignored") == 0)
			{
				return GlobalMembersResourcesWP.ExtractIgnoredResources(theManager);
			}
			if (string.Compare(theName, "Ignored_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractIgnored_480Resources(theManager);
			}
			if (string.Compare(theName, "Ignored_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractIgnored_960Resources(theManager);
			}
			if (string.Compare(theName, "Init") == 0)
			{
				return GlobalMembersResourcesWP.ExtractInitResources(theManager);
			}
			if (string.Compare(theName, "Loader") == 0)
			{
				return GlobalMembersResourcesWP.ExtractLoaderResources(theManager);
			}
			if (string.Compare(theName, "Loader_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractLoader_480Resources(theManager);
			}
			if (string.Compare(theName, "Loader_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractLoader_960Resources(theManager);
			}
			if (string.Compare(theName, "MainMenu") == 0)
			{
				return GlobalMembersResourcesWP.ExtractMainMenuResources(theManager);
			}
			if (string.Compare(theName, "MainMenu_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractMainMenu_480Resources(theManager);
			}
			if (string.Compare(theName, "MainMenu_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractMainMenu_960Resources(theManager);
			}
			if (string.Compare(theName, "NoMatch") == 0)
			{
				return GlobalMembersResourcesWP.ExtractNoMatchResources(theManager);
			}
			if (string.Compare(theName, "NoMatch_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractNoMatch_480Resources(theManager);
			}
			if (string.Compare(theName, "NoMatch_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractNoMatch_960Resources(theManager);
			}
			if (string.Compare(theName, "NoMatch_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractNoMatch_CommonResources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_0") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_0Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_0_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_0_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_0_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_0_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_1") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_1Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_10") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_10Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_10_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_10_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_10_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_10_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_11") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_11Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_11_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_11_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_11_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_11_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_12") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_12Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_12_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_12_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_12_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_12_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_13") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_13Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_13_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_13_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_13_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_13_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_14") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_14Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_14_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_14_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_14_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_14_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_15") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_15Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_15_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_15_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_15_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_15_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_16") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_16Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_16_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_16_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_16_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_16_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_17") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_17Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_17_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_17_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_17_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_17_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_18") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_18Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_18_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_18_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_18_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_18_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_19") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_19Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_19_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_19_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_19_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_19_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_1_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_1_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_1_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_1_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_2") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_2Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_20") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_20Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_20_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_20_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_20_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_20_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_21") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_21Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_21_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_21_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_21_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_21_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_22") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_22Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_22_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_22_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_22_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_22_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_23") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_23Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_23_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_23_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_23_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_23_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_24") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_24Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_24_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_24_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_24_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_24_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_25") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_25Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_25_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_25_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_25_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_25_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_26") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_26Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_26_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_26_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_26_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_26_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_27") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_27Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_27_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_27_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_27_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_27_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_28") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_28Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_28_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_28_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_28_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_28_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_29") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_29Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_29_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_29_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_29_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_29_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_2_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_2_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_2_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_2_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_3") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_3Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_3_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_3_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_3_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_3_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_4") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_4Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_4_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_4_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_4_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_4_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_5") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_5Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_5_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_5_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_5_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_5_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_6") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_6Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_6_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_6_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_6_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_6_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_7") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_7Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_7_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_7_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_7_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_7_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_8") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_8Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_8_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_8_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_8_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_8_960Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_9") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_9Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_9_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_9_480Resources(theManager);
			}
			if (string.Compare(theName, "ProfilePic_9_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractProfilePic_9_960Resources(theManager);
			}
			if (string.Compare(theName, "QuestHelp") == 0)
			{
				return GlobalMembersResourcesWP.ExtractQuestHelpResources(theManager);
			}
			if (string.Compare(theName, "QuestHelp_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractQuestHelp_480Resources(theManager);
			}
			if (string.Compare(theName, "QuestHelp_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractQuestHelp_960Resources(theManager);
			}
			if (string.Compare(theName, "RateGame") == 0)
			{
				return GlobalMembersResourcesWP.ExtractRateGameResources(theManager);
			}
			if (string.Compare(theName, "RateGame_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractRateGame_480Resources(theManager);
			}
			if (string.Compare(theName, "RateGame_960") == 0)
			{
				return GlobalMembersResourcesWP.ExtractRateGame_960Resources(theManager);
			}
			if (string.Compare(theName, "ZenMode") == 0)
			{
				return GlobalMembersResourcesWP.ExtractZenModeResources(theManager);
			}
			if (string.Compare(theName, "ZenMode_Common") == 0)
			{
				return GlobalMembersResourcesWP.ExtractZenMode_CommonResources(theManager);
			}
			if (string.Compare(theName, "ZenMode_DEDE") == 0)
			{
				return GlobalMembersResourcesWP.ExtractZenMode_DEDEResources(theManager);
			}
			if (string.Compare(theName, "ZenMode_ENUS") == 0)
			{
				return GlobalMembersResourcesWP.ExtractZenMode_ENUSResources(theManager);
			}
			if (string.Compare(theName, "ZenMode_ESES") == 0)
			{
				return GlobalMembersResourcesWP.ExtractZenMode_ESESResources(theManager);
			}
			if (string.Compare(theName, "ZenMode_FRFR") == 0)
			{
				return GlobalMembersResourcesWP.ExtractZenMode_FRFRResources(theManager);
			}
			if (string.Compare(theName, "ZenMode_ITIT") == 0)
			{
				return GlobalMembersResourcesWP.ExtractZenMode_ITITResources(theManager);
			}
			if (string.Compare(theName, "ZenOptions") == 0)
			{
				return GlobalMembersResourcesWP.ExtractZenOptionsResources(theManager);
			}
			if (string.Compare(theName, "ZenOptions_480") == 0)
			{
				return GlobalMembersResourcesWP.ExtractZenOptions_480Resources(theManager);
			}
			return string.Compare(theName, "ZenOptions_960") == 0 && GlobalMembersResourcesWP.ExtractZenOptions_960Resources(theManager);
		}

		public static bool ExtractAwardGlowResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractAwardGlow_960Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractAwardGlow_480Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractAwardGlow_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_AWARD_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 210, "IMAGE_AWARD_GLOW", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractAwardGlow_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_AWARD_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 210, "IMAGE_AWARD_GLOW", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBadgesResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBadges_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBadges_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBadges_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_BADGES_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 32, "ATLASIMAGE_ATLAS_BADGES_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_ANNIHILATOR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1379, "IMAGE_BADGES_SMALL_ANNIHILATOR", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_ANTE_UP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1380, "IMAGE_BADGES_SMALL_ANTE_UP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BEJEWELER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1381, "IMAGE_BADGES_SMALL_BEJEWELER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BLASTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1382, "IMAGE_BADGES_SMALL_BLASTER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BUTTERFLY_BONANZA = GlobalMembersResourcesWP.GetImageThrow(theManager, 1383, "IMAGE_BADGES_SMALL_BUTTERFLY_BONANZA", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BUTTERFLY_MONARCH = GlobalMembersResourcesWP.GetImageThrow(theManager, 1384, "IMAGE_BADGES_SMALL_BUTTERFLY_MONARCH", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_CHAIN_REACTION = GlobalMembersResourcesWP.GetImageThrow(theManager, 1385, "IMAGE_BADGES_SMALL_CHAIN_REACTION", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_CHROMATIC = GlobalMembersResourcesWP.GetImageThrow(theManager, 1386, "IMAGE_BADGES_SMALL_CHROMATIC", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_DIAMOND_MINE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1387, "IMAGE_BADGES_SMALL_DIAMOND_MINE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_DYNAMO = GlobalMembersResourcesWP.GetImageThrow(theManager, 1388, "IMAGE_BADGES_SMALL_DYNAMO", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_ELECTRIFIER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1389, "IMAGE_BADGES_SMALL_ELECTRIFIER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_HIGH_VOLTAGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1390, "IMAGE_BADGES_SMALL_HIGH_VOLTAGE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_INFERNO = GlobalMembersResourcesWP.GetImageThrow(theManager, 1391, "IMAGE_BADGES_SMALL_INFERNO", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_LEVELORD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1392, "IMAGE_BADGES_SMALL_LEVELORD", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_LUCKY_STREAK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1393, "IMAGE_BADGES_SMALL_LUCKY_STREAK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_MILLIONAIRE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1394, "IMAGE_BADGES_SMALL_MILLIONAIRE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_RELIC_HUNTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1395, "IMAGE_BADGES_SMALL_RELIC_HUNTER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_RINGS = GlobalMembersResourcesWP.GetImageThrow(theManager, 1396, "IMAGE_BADGES_SMALL_RINGS", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_STELLAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1397, "IMAGE_BADGES_SMALL_STELLAR", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_SUPERSTAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1398, "IMAGE_BADGES_SMALL_SUPERSTAR", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_THE_GAMBLER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1399, "IMAGE_BADGES_SMALL_THE_GAMBLER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_UNKNOWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1400, "IMAGE_BADGES_SMALL_UNKNOWN", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBadges_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_BADGES_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 33, "ATLASIMAGE_ATLAS_BADGES_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_ANNIHILATOR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1379, "IMAGE_BADGES_SMALL_ANNIHILATOR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_ANTE_UP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1380, "IMAGE_BADGES_SMALL_ANTE_UP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BEJEWELER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1381, "IMAGE_BADGES_SMALL_BEJEWELER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BLASTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1382, "IMAGE_BADGES_SMALL_BLASTER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BUTTERFLY_BONANZA = GlobalMembersResourcesWP.GetImageThrow(theManager, 1383, "IMAGE_BADGES_SMALL_BUTTERFLY_BONANZA", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_BUTTERFLY_MONARCH = GlobalMembersResourcesWP.GetImageThrow(theManager, 1384, "IMAGE_BADGES_SMALL_BUTTERFLY_MONARCH", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_CHAIN_REACTION = GlobalMembersResourcesWP.GetImageThrow(theManager, 1385, "IMAGE_BADGES_SMALL_CHAIN_REACTION", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_CHROMATIC = GlobalMembersResourcesWP.GetImageThrow(theManager, 1386, "IMAGE_BADGES_SMALL_CHROMATIC", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_DIAMOND_MINE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1387, "IMAGE_BADGES_SMALL_DIAMOND_MINE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_DYNAMO = GlobalMembersResourcesWP.GetImageThrow(theManager, 1388, "IMAGE_BADGES_SMALL_DYNAMO", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_ELECTRIFIER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1389, "IMAGE_BADGES_SMALL_ELECTRIFIER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_HIGH_VOLTAGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1390, "IMAGE_BADGES_SMALL_HIGH_VOLTAGE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_INFERNO = GlobalMembersResourcesWP.GetImageThrow(theManager, 1391, "IMAGE_BADGES_SMALL_INFERNO", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_LEVELORD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1392, "IMAGE_BADGES_SMALL_LEVELORD", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_LUCKY_STREAK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1393, "IMAGE_BADGES_SMALL_LUCKY_STREAK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_MILLIONAIRE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1394, "IMAGE_BADGES_SMALL_MILLIONAIRE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_RELIC_HUNTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1395, "IMAGE_BADGES_SMALL_RELIC_HUNTER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_RINGS = GlobalMembersResourcesWP.GetImageThrow(theManager, 1396, "IMAGE_BADGES_SMALL_RINGS", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_STELLAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1397, "IMAGE_BADGES_SMALL_STELLAR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_SUPERSTAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1398, "IMAGE_BADGES_SMALL_SUPERSTAR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_THE_GAMBLER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1399, "IMAGE_BADGES_SMALL_THE_GAMBLER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_SMALL_UNKNOWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1400, "IMAGE_BADGES_SMALL_UNKNOWN", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ANNIHILATORResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_ANNIHILATOR_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_ANNIHILATOR_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ANNIHILATOR_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ANNIHILATOR = GlobalMembersResourcesWP.GetImageThrow(theManager, 181, "IMAGE_BADGES_BIG_ANNIHILATOR", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ANNIHILATOR_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ANNIHILATOR = GlobalMembersResourcesWP.GetImageThrow(theManager, 181, "IMAGE_BADGES_BIG_ANNIHILATOR", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ANTE_UPResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_ANTE_UP_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_ANTE_UP_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ANTE_UP_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ANTE_UP = GlobalMembersResourcesWP.GetImageThrow(theManager, 182, "IMAGE_BADGES_BIG_ANTE_UP", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ANTE_UP_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ANTE_UP = GlobalMembersResourcesWP.GetImageThrow(theManager, 182, "IMAGE_BADGES_BIG_ANTE_UP", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BEJEWELERResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_BEJEWELER_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_BEJEWELER_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BEJEWELER_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BEJEWELER = GlobalMembersResourcesWP.GetImageThrow(theManager, 183, "IMAGE_BADGES_BIG_BEJEWELER", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BEJEWELER_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BEJEWELER = GlobalMembersResourcesWP.GetImageThrow(theManager, 183, "IMAGE_BADGES_BIG_BEJEWELER", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BLASTERResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_BLASTER_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_BLASTER_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BLASTER_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BLASTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 184, "IMAGE_BADGES_BIG_BLASTER", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BLASTER_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BLASTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 184, "IMAGE_BADGES_BIG_BLASTER", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BRONZEResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_BRONZE_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_BRONZE_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BRONZE_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BRONZE = GlobalMembersResourcesWP.GetImageThrow(theManager, 185, "IMAGE_BADGES_BIG_BRONZE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BRONZE_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BRONZE = GlobalMembersResourcesWP.GetImageThrow(theManager, 185, "IMAGE_BADGES_BIG_BRONZE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BUTTERFLY_BONANZAResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_BUTTERFLY_BONANZA_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_BUTTERFLY_BONANZA_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BUTTERFLY_BONANZA_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BUTTERFLY_BONANZA = GlobalMembersResourcesWP.GetImageThrow(theManager, 186, "IMAGE_BADGES_BIG_BUTTERFLY_BONANZA", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BUTTERFLY_BONANZA_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BUTTERFLY_BONANZA = GlobalMembersResourcesWP.GetImageThrow(theManager, 186, "IMAGE_BADGES_BIG_BUTTERFLY_BONANZA", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BUTTERFLY_MONARCHResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_BUTTERFLY_MONARCH_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_BUTTERFLY_MONARCH_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BUTTERFLY_MONARCH_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BUTTERFLY_MONARCH = GlobalMembersResourcesWP.GetImageThrow(theManager, 187, "IMAGE_BADGES_BIG_BUTTERFLY_MONARCH", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_BUTTERFLY_MONARCH_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_BUTTERFLY_MONARCH = GlobalMembersResourcesWP.GetImageThrow(theManager, 187, "IMAGE_BADGES_BIG_BUTTERFLY_MONARCH", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_CHAIN_REACTIONResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_CHAIN_REACTION_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_CHAIN_REACTION_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_CHAIN_REACTION_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_CHAIN_REACTION = GlobalMembersResourcesWP.GetImageThrow(theManager, 206, "IMAGE_BADGES_BIG_CHAIN_REACTION", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_CHAIN_REACTION_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_CHAIN_REACTION = GlobalMembersResourcesWP.GetImageThrow(theManager, 206, "IMAGE_BADGES_BIG_CHAIN_REACTION", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_CHROMATICResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_CHROMATIC_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_CHROMATIC_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_CHROMATIC_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_CHROMATIC = GlobalMembersResourcesWP.GetImageThrow(theManager, 188, "IMAGE_BADGES_BIG_CHROMATIC", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_CHROMATIC_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_CHROMATIC = GlobalMembersResourcesWP.GetImageThrow(theManager, 188, "IMAGE_BADGES_BIG_CHROMATIC", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_DIAMOND_MINEResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_DIAMOND_MINE_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_DIAMOND_MINE_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_DIAMOND_MINE_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_DIAMOND_MINE = GlobalMembersResourcesWP.GetImageThrow(theManager, 189, "IMAGE_BADGES_BIG_DIAMOND_MINE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_DIAMOND_MINE_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_DIAMOND_MINE = GlobalMembersResourcesWP.GetImageThrow(theManager, 189, "IMAGE_BADGES_BIG_DIAMOND_MINE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_DYNAMOResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_DYNAMO_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_DYNAMO_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_DYNAMO_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_DYNAMO = GlobalMembersResourcesWP.GetImageThrow(theManager, 207, "IMAGE_BADGES_BIG_DYNAMO", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_DYNAMO_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_DYNAMO = GlobalMembersResourcesWP.GetImageThrow(theManager, 207, "IMAGE_BADGES_BIG_DYNAMO", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ELECTRIFIERResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_ELECTRIFIER_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_ELECTRIFIER_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ELECTRIFIER_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ELECTRIFIER = GlobalMembersResourcesWP.GetImageThrow(theManager, 190, "IMAGE_BADGES_BIG_ELECTRIFIER", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ELECTRIFIER_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ELECTRIFIER = GlobalMembersResourcesWP.GetImageThrow(theManager, 190, "IMAGE_BADGES_BIG_ELECTRIFIER", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ELITEResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_ELITE_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_ELITE_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ELITE_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ELITE = GlobalMembersResourcesWP.GetImageThrow(theManager, 191, "IMAGE_BADGES_BIG_ELITE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ELITE_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ELITE = GlobalMembersResourcesWP.GetImageThrow(theManager, 191, "IMAGE_BADGES_BIG_ELITE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_GLACIAL_EXPLORERResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_GLACIAL_EXPLORER_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_GLACIAL_EXPLORER_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_GLACIAL_EXPLORER_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_GLACIAL_EXPLORER = GlobalMembersResourcesWP.GetImageThrow(theManager, 192, "IMAGE_BADGES_BIG_GLACIAL_EXPLORER", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_GLACIAL_EXPLORER_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_GLACIAL_EXPLORER = GlobalMembersResourcesWP.GetImageThrow(theManager, 192, "IMAGE_BADGES_BIG_GLACIAL_EXPLORER", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_GOLDResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_GOLD_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_GOLD_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_GOLD_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_GOLD = GlobalMembersResourcesWP.GetImageThrow(theManager, 193, "IMAGE_BADGES_BIG_GOLD", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_GOLD_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_GOLD = GlobalMembersResourcesWP.GetImageThrow(theManager, 193, "IMAGE_BADGES_BIG_GOLD", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_HEROES_WELCOMEResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_HEROES_WELCOME_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_HEROES_WELCOME_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_HEROES_WELCOME_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_HEROES_WELCOME = GlobalMembersResourcesWP.GetImageThrow(theManager, 194, "IMAGE_BADGES_BIG_HEROES_WELCOME", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_HEROES_WELCOME_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_HEROES_WELCOME = GlobalMembersResourcesWP.GetImageThrow(theManager, 194, "IMAGE_BADGES_BIG_HEROES_WELCOME", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_HIGH_VOLTAGEResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_HIGH_VOLTAGE_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_HIGH_VOLTAGE_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_HIGH_VOLTAGE_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_HIGH_VOLTAGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 195, "IMAGE_BADGES_BIG_HIGH_VOLTAGE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_HIGH_VOLTAGE_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_HIGH_VOLTAGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 195, "IMAGE_BADGES_BIG_HIGH_VOLTAGE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ICE_BREAKERResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_ICE_BREAKER_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_ICE_BREAKER_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ICE_BREAKER_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ICE_BREAKER = GlobalMembersResourcesWP.GetImageThrow(theManager, 196, "IMAGE_BADGES_BIG_ICE_BREAKER", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_ICE_BREAKER_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_ICE_BREAKER = GlobalMembersResourcesWP.GetImageThrow(theManager, 196, "IMAGE_BADGES_BIG_ICE_BREAKER", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_INFERNOResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_INFERNO_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_INFERNO_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_INFERNO_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_INFERNO = GlobalMembersResourcesWP.GetImageThrow(theManager, 197, "IMAGE_BADGES_BIG_INFERNO", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_INFERNO_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_INFERNO = GlobalMembersResourcesWP.GetImageThrow(theManager, 197, "IMAGE_BADGES_BIG_INFERNO", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_LEVELORDResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_LEVELORD_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_LEVELORD_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_LEVELORD_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_LEVELORD = GlobalMembersResourcesWP.GetImageThrow(theManager, 198, "IMAGE_BADGES_BIG_LEVELORD", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_LEVELORD_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_LEVELORD = GlobalMembersResourcesWP.GetImageThrow(theManager, 198, "IMAGE_BADGES_BIG_LEVELORD", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_LUCKY_STREAKResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_LUCKY_STREAK_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_LUCKY_STREAK_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_LUCKY_STREAK_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_LUCKY_STREAK = GlobalMembersResourcesWP.GetImageThrow(theManager, 208, "IMAGE_BADGES_BIG_LUCKY_STREAK", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_LUCKY_STREAK_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_LUCKY_STREAK = GlobalMembersResourcesWP.GetImageThrow(theManager, 208, "IMAGE_BADGES_BIG_LUCKY_STREAK", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_MILLIONAIREResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_MILLIONAIRE_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_MILLIONAIRE_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_MILLIONAIRE_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_MILLIONAIRE = GlobalMembersResourcesWP.GetImageThrow(theManager, 209, "IMAGE_BADGES_BIG_MILLIONAIRE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_MILLIONAIRE_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_MILLIONAIRE = GlobalMembersResourcesWP.GetImageThrow(theManager, 209, "IMAGE_BADGES_BIG_MILLIONAIRE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_PLATINUMResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_PLATINUM_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_PLATINUM_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_PLATINUM_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_PLATINUM = GlobalMembersResourcesWP.GetImageThrow(theManager, 199, "IMAGE_BADGES_BIG_PLATINUM", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_PLATINUM_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_PLATINUM = GlobalMembersResourcesWP.GetImageThrow(theManager, 199, "IMAGE_BADGES_BIG_PLATINUM", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_RELIC_HUNTERResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_RELIC_HUNTER_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_RELIC_HUNTER_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_RELIC_HUNTER_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_RELIC_HUNTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 200, "IMAGE_BADGES_BIG_RELIC_HUNTER", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_RELIC_HUNTER_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_RELIC_HUNTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 200, "IMAGE_BADGES_BIG_RELIC_HUNTER", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_SILVERResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_SILVER_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_SILVER_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_SILVER_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_SILVER = GlobalMembersResourcesWP.GetImageThrow(theManager, 201, "IMAGE_BADGES_BIG_SILVER", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_SILVER_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_SILVER = GlobalMembersResourcesWP.GetImageThrow(theManager, 201, "IMAGE_BADGES_BIG_SILVER", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_STELLARResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_STELLAR_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_STELLAR_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_STELLAR_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_STELLAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 202, "IMAGE_BADGES_BIG_STELLAR", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_STELLAR_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_STELLAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 202, "IMAGE_BADGES_BIG_STELLAR", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_SUPERSTARResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_SUPERSTAR_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_SUPERSTAR_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_SUPERSTAR_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_SUPERSTAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 203, "IMAGE_BADGES_BIG_SUPERSTAR", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_SUPERSTAR_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_SUPERSTAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 203, "IMAGE_BADGES_BIG_SUPERSTAR", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_THE_GAMBLERResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_THE_GAMBLER_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_THE_GAMBLER_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_THE_GAMBLER_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_THE_GAMBLER = GlobalMembersResourcesWP.GetImageThrow(theManager, 204, "IMAGE_BADGES_BIG_THE_GAMBLER", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_THE_GAMBLER_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_THE_GAMBLER = GlobalMembersResourcesWP.GetImageThrow(theManager, 204, "IMAGE_BADGES_BIG_THE_GAMBLER", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_TOP_SECRETResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_TOP_SECRET_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractBADGES_BIG_TOP_SECRET_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_TOP_SECRET_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_TOP_SECRET = GlobalMembersResourcesWP.GetImageThrow(theManager, 205, "IMAGE_BADGES_BIG_TOP_SECRET", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBADGES_BIG_TOP_SECRET_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_BIG_TOP_SECRET = GlobalMembersResourcesWP.GetImageThrow(theManager, 205, "IMAGE_BADGES_BIG_TOP_SECRET", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractCommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractCommon_960Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractCommon_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurLocSet == 1145390149U && !GlobalMembersResourcesWP.ExtractCommon_DEDEResources(theManager))
				{
					return false;
				}
				if (theManager.mCurLocSet == 1162761555U && !GlobalMembersResourcesWP.ExtractCommon_ENUSResources(theManager))
				{
					return false;
				}
				if (theManager.mCurLocSet == 1163085139U && !GlobalMembersResourcesWP.ExtractCommon_ESESResources(theManager))
				{
					return false;
				}
				if (theManager.mCurLocSet == 1179797074U && !GlobalMembersResourcesWP.ExtractCommon_FRFRResources(theManager))
				{
					return false;
				}
				if (theManager.mCurLocSet == 1230260564U && !GlobalMembersResourcesWP.ExtractCommon_ITITResources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractCommon_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractCommon_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.FONT_HIRES_DIALOG = GlobalMembersResourcesWP.GetFontThrow(theManager, 0, "FONT_HIRES_DIALOG", 480, 0);
				GlobalMembersResourcesWP.FONT_LOWRES_DIALOG = GlobalMembersResourcesWP.GetFontThrow(theManager, 1, "FONT_LOWRES_DIALOG", 480, 0);
				GlobalMembersResourcesWP.FONT_HIRES_HEADER = GlobalMembersResourcesWP.GetFontThrow(theManager, 2, "FONT_HIRES_HEADER", 480, 0);
				GlobalMembersResourcesWP.FONT_LOWRES_HEADER = GlobalMembersResourcesWP.GetFontThrow(theManager, 3, "FONT_LOWRES_HEADER", 480, 0);
				GlobalMembersResourcesWP.FONT_HIRES_SUBHEADER = GlobalMembersResourcesWP.GetFontThrow(theManager, 4, "FONT_HIRES_SUBHEADER", 480, 0);
				GlobalMembersResourcesWP.FONT_LOWRES_SUBHEADER = GlobalMembersResourcesWP.GetFontThrow(theManager, 5, "FONT_LOWRES_SUBHEADER", 480, 0);
				GlobalMembersResourcesWP.FONT_HIRES_INGAME = GlobalMembersResourcesWP.GetFontThrow(theManager, 6, "FONT_HIRES_INGAME", 480, 0);
				GlobalMembersResourcesWP.FONT_LOWRES_INGAME = GlobalMembersResourcesWP.GetFontThrow(theManager, 7, "FONT_LOWRES_INGAME", 480, 0);
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_COMMON_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 34, "ATLASIMAGE_ATLAS_COMMON_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ANIMS_CARD_GEM_SPARKLE2_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 92, "IMAGE_ANIMS_CARD_GEM_SPARKLE2_0_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BUTTERFLY_HELP_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 93, "IMAGE_HELP_BUTTERFLY_HELP_0_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_CARD_GEM_SPARKLE2_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 94, "IMAGE_HELP_CARD_GEM_SPARKLE2_0_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_SPARKLE_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 95, "IMAGE_HELP_DIAMOND_SPARKLE_0_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_FLAMEGEM_HELP_0_FLAME63 = GlobalMembersResourcesWP.GetImageThrow(theManager, 96, "IMAGE_HELP_FLAMEGEM_HELP_0_FLAME63", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_FLAMEGEM_HELP_1_HELP_GREEN_NOSHDW = GlobalMembersResourcesWP.GetImageThrow(theManager, 97, "IMAGE_HELP_FLAMEGEM_HELP_1_HELP_GREEN_NOSHDW", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_FLAMEGEM_HELP_2_SPARKLET = GlobalMembersResourcesWP.GetImageThrow(theManager, 98, "IMAGE_HELP_FLAMEGEM_HELP_2_SPARKLET", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HELP_0_RING = GlobalMembersResourcesWP.GetImageThrow(theManager, 99, "IMAGE_HELP_ICESTORM_HELP_0_RING", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_STEAMPULSE_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 100, "IMAGE_HELP_LIGHTNING_STEAMPULSE_0_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_HELP_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 101, "IMAGE_HELP_STARGEM_HELP_0_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_HELP_1_STAR_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 102, "IMAGE_HELP_STARGEM_HELP_1_STAR_GLOW", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_HELP_2_CORONAGLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 103, "IMAGE_HELP_STARGEM_HELP_2_CORONAGLOW", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_HELP_3_HELP_GREEN_NOSHDW = GlobalMembersResourcesWP.GetImageThrow(theManager, 104, "IMAGE_HELP_STARGEM_HELP_3_HELP_GREEN_NOSHDW", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_CRYSTALRAYS_0_RAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 105, "IMAGE_PARTICLES_CRYSTALRAYS_0_RAY", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_CRYSTALBALL_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 106, "IMAGE_PARTICLES_CRYSTALBALL_0_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_QUEST_DIG_COLLECT_BASE_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 107, "IMAGE_PARTICLES_QUEST_DIG_COLLECT_BASE_0_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_QUEST_DIG_COLLECT_BASE_1_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 108, "IMAGE_PARTICLES_QUEST_DIG_COLLECT_BASE_1_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_QUEST_DIG_COLLECT_GOLD_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 109, "IMAGE_PARTICLES_QUEST_DIG_COLLECT_GOLD_0_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_QUEST_DIG_COLLECT_GOLD_1_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 110, "IMAGE_PARTICLES_QUEST_DIG_COLLECT_GOLD_1_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_SANDSTORM_DIG_0_SOFT_CLUMPY = GlobalMembersResourcesWP.GetImageThrow(theManager, 111, "IMAGE_PARTICLES_GEM_SANDSTORM_DIG_0_SOFT_CLUMPY", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_BADGE_UPGRADE_0_ICE = GlobalMembersResourcesWP.GetImageThrow(theManager, 112, "IMAGE_PARTICLES_BADGE_UPGRADE_0_ICE", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_BADGE_UPGRADE_1_BADGE_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 113, "IMAGE_PARTICLES_BADGE_UPGRADE_1_BADGE_GLOW", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_BADGE_UPGRADE_2_CERCLEM = GlobalMembersResourcesWP.GetImageThrow(theManager, 114, "IMAGE_PARTICLES_BADGE_UPGRADE_2_CERCLEM", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_BLOWING_SNOW_0_TEXTURE_01 = GlobalMembersResourcesWP.GetImageThrow(theManager, 115, "IMAGE_PARTICLES_BLOWING_SNOW_0_TEXTURE_01", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_BOARD_FLAME_EMBERS_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 116, "IMAGE_PARTICLES_BOARD_FLAME_EMBERS_0_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_CARD_GEM_SPARKLE_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 117, "IMAGE_PARTICLES_CARD_GEM_SPARKLE_0_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_COINSPARKLE_0_FLARE = GlobalMembersResourcesWP.GetImageThrow(theManager, 118, "IMAGE_PARTICLES_COINSPARKLE_0_FLARE", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_COUNTDOWNBAR_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 119, "IMAGE_PARTICLES_COUNTDOWNBAR_0_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_COUNTDOWNBAR_1_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 120, "IMAGE_PARTICLES_COUNTDOWNBAR_1_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 121, "IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_0_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_1_SNOWFLAKE = GlobalMembersResourcesWP.GetImageThrow(theManager, 122, "IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_1_SNOWFLAKE", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_2_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 123, "IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_2_ICECHUNK", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DANGERSNOW_SOFT_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 124, "IMAGE_PARTICLES_DANGERSNOW_SOFT_0_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DANGERSNOW_SOFT_1_SNOWFLAKE = GlobalMembersResourcesWP.GetImageThrow(theManager, 125, "IMAGE_PARTICLES_DANGERSNOW_SOFT_1_SNOWFLAKE", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DISCOBALL_0_DISCO_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 126, "IMAGE_PARTICLES_DISCOBALL_0_DISCO_GLOW", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DISCOBALL_1_DISCO_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 127, "IMAGE_PARTICLES_DISCOBALL_1_DISCO_GLOW", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DISCOBALL_2_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 128, "IMAGE_PARTICLES_DISCOBALL_2_BLURRED_SHARP_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_FIREGEM_HYPERSPACE_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 129, "IMAGE_PARTICLES_FIREGEM_HYPERSPACE_0_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_FLAME_CARD_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 130, "IMAGE_PARTICLES_FLAME_CARD_0_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_BLASTGEM_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 131, "IMAGE_PARTICLES_GEM_BLASTGEM_0_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_BUTTERFLY_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 132, "IMAGE_PARTICLES_GEM_BUTTERFLY_0_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_BUTTERFLY_CREATE_0_FLOWER = GlobalMembersResourcesWP.GetImageThrow(theManager, 133, "IMAGE_PARTICLES_GEM_BUTTERFLY_CREATE_0_FLOWER", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_BUTTERFLY_CREATE_1_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 134, "IMAGE_PARTICLES_GEM_BUTTERFLY_CREATE_1_BLURRED_SHARP_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_DIAMOND_SPARKLES_0_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 135, "IMAGE_PARTICLES_GEM_DIAMOND_SPARKLES_0_BLURRED_SHARP_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_FIRE_TRAIL_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 136, "IMAGE_PARTICLES_GEM_FIRE_TRAIL_0_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_GOLD_BLING_0_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 137, "IMAGE_PARTICLES_GEM_GOLD_BLING_0_BLURRED_SHARP_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_HINTFLASH_0_CERCLEM = GlobalMembersResourcesWP.GetImageThrow(theManager, 138, "IMAGE_PARTICLES_GEM_HINTFLASH_0_CERCLEM", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_HYPERCUBE_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 139, "IMAGE_PARTICLES_GEM_HYPERCUBE_0_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_ICE_TRAIL_0_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 140, "IMAGE_PARTICLES_GEM_ICE_TRAIL_0_BLURRED_SHARP_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_ICE_TRAIL_1_COMIC_SMOKE2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 141, "IMAGE_PARTICLES_GEM_ICE_TRAIL_1_COMIC_SMOKE2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_MULTIPLIER_0_RAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 142, "IMAGE_PARTICLES_GEM_MULTIPLIER_0_RAY", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_STARGEM_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 143, "IMAGE_PARTICLES_GEM_STARGEM_0_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_STARGEM_1_STAR_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 144, "IMAGE_PARTICLES_GEM_STARGEM_1_STAR_GLOW", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_STARGEM_2_CORONAGLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 145, "IMAGE_PARTICLES_GEM_STARGEM_2_CORONAGLOW", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_ICE_STORMY_0_SHARD = GlobalMembersResourcesWP.GetImageThrow(theManager, 146, "IMAGE_PARTICLES_ICE_STORMY_0_SHARD", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_LEVELBAR_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 147, "IMAGE_PARTICLES_LEVELBAR_0_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_LEVELBAR_1_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 148, "IMAGE_PARTICLES_LEVELBAR_1_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_SANDSTORM_COVER_0_SAND_PARTICLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 149, "IMAGE_PARTICLES_SANDSTORM_COVER_0_SAND_PARTICLE", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_SKULL_EXPLODE_0_DOT_STREAK_01 = GlobalMembersResourcesWP.GetImageThrow(theManager, 150, "IMAGE_PARTICLES_SKULL_EXPLODE_0_DOT_STREAK_01", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_SKULL_EXPLODE_1_SKULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 151, "IMAGE_PARTICLES_SKULL_EXPLODE_1_SKULL", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_SKULL_EXPLODE_2_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 152, "IMAGE_PARTICLES_SKULL_EXPLODE_2_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_SPEEDBOARD_FLAME_0_FLAME1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 153, "IMAGE_PARTICLES_SPEEDBOARD_FLAME_0_FLAME1", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_STAR_CARD_0_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 154, "IMAGE_PARTICLES_STAR_CARD_0_BLURRED_SHARP_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_STAR_CARD_1_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 155, "IMAGE_PARTICLES_STAR_CARD_1_SMALL_BLUR_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_STARBURST_0_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 156, "IMAGE_PARTICLES_STARBURST_0_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_WEIGHT_FIRE_0_TRUEFLAME7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 157, "IMAGE_PARTICLES_WEIGHT_FIRE_0_TRUEFLAME7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_WEIGHT_FIRE_1_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 158, "IMAGE_PARTICLES_WEIGHT_FIRE_1_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_WEIGHT_ICE_0_DIM_BLUR_CLOUD = GlobalMembersResourcesWP.GetImageThrow(theManager, 159, "IMAGE_PARTICLES_WEIGHT_ICE_0_DIM_BLUR_CLOUD", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_WEIGHT_ICE_1_ICESHARD_0000 = GlobalMembersResourcesWP.GetImageThrow(theManager, 160, "IMAGE_PARTICLES_WEIGHT_ICE_1_ICESHARD_0000", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIG_LINE_HIT_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 161, "IMAGE_QUEST_DIG_DIG_LINE_HIT_0_BASIC_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIG_LINE_HIT_MEGA_0_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 162, "IMAGE_QUEST_DIG_DIG_LINE_HIT_MEGA_0_BLURRED_SHARP_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIG_LINE_HIT_MEGA_1_DIAMOND_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 163, "IMAGE_QUEST_DIG_DIG_LINE_HIT_MEGA_1_DIAMOND_STAR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_LOGO = GlobalMembersResourcesWP.GetImageThrow(theManager, 698, "IMAGE_MAIN_MENU_LOGO", 480, 0);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_CLOUDS = GlobalMembersResourcesWP.GetImageThrow(theManager, 699, "IMAGE_MAIN_MENU_CLOUDS", 480, 0);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_BACKGROUND = GlobalMembersResourcesWP.GetImageThrow(theManager, 700, "IMAGE_MAIN_MENU_BACKGROUND", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_CLOSED_BUTTON = GlobalMembersResourcesWP.GetImageThrow(theManager, 705, "IMAGE_DASHBOARD_CLOSED_BUTTON", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_DASH_MAIN = GlobalMembersResourcesWP.GetImageThrow(theManager, 706, "IMAGE_DASHBOARD_DASH_MAIN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_DASH_TILE = GlobalMembersResourcesWP.GetImageThrow(theManager, 707, "IMAGE_DASHBOARD_DASH_TILE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_DM_OVERLAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 708, "IMAGE_DASHBOARD_DM_OVERLAY", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_ICE_OVERLAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 709, "IMAGE_DASHBOARD_ICE_OVERLAY", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_MENU_DOWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 710, "IMAGE_DASHBOARD_MENU_DOWN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_MENU_UP = GlobalMembersResourcesWP.GetImageThrow(theManager, 711, "IMAGE_DASHBOARD_MENU_UP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS0 = GlobalMembersResourcesWP.GetImageThrow(theManager, 742, "IMAGE_PPS0", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 743, "IMAGE_PPS1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 744, "IMAGE_PPS2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 745, "IMAGE_PPS3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 746, "IMAGE_PPS4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 747, "IMAGE_PPS5", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 748, "IMAGE_PPS6", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 749, "IMAGE_PPS7", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 750, "IMAGE_PPS8", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 751, "IMAGE_PPS9", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 752, "IMAGE_PPS10", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 753, "IMAGE_PPS11", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 754, "IMAGE_PPS12", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 755, "IMAGE_PPS13", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 756, "IMAGE_PPS14", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 757, "IMAGE_PPS15", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS16 = GlobalMembersResourcesWP.GetImageThrow(theManager, 758, "IMAGE_PPS16", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 759, "IMAGE_PPS17", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS18 = GlobalMembersResourcesWP.GetImageThrow(theManager, 760, "IMAGE_PPS18", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 761, "IMAGE_PPS19", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 762, "IMAGE_PPS20", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 763, "IMAGE_PPS21", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 764, "IMAGE_PPS22", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS23 = GlobalMembersResourcesWP.GetImageThrow(theManager, 765, "IMAGE_PPS23", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS24 = GlobalMembersResourcesWP.GetImageThrow(theManager, 766, "IMAGE_PPS24", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 767, "IMAGE_PPS25", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 768, "IMAGE_PPS26", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS27 = GlobalMembersResourcesWP.GetImageThrow(theManager, 769, "IMAGE_PPS27", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS28 = GlobalMembersResourcesWP.GetImageThrow(theManager, 770, "IMAGE_PPS28", 480, 0);
				GlobalMembersResourcesWP.IMAGE_PPS29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 771, "IMAGE_PPS29", 480, 0);
				GlobalMembersResourcesWP.IMAGE_TOOLTIP = GlobalMembersResourcesWP.GetImageThrow(theManager, 794, "IMAGE_TOOLTIP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_TOOLTIP_ARROW_ARROW_DOWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 795, "IMAGE_TOOLTIP_ARROW_ARROW_DOWN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_TOOLTIP_ARROW_ARROW_LEFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 796, "IMAGE_TOOLTIP_ARROW_ARROW_LEFT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_TOOLTIP_ARROW_ARROW_RIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 797, "IMAGE_TOOLTIP_ARROW_ARROW_RIGHT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_TOOLTIP_ARROW_ARROW_UP = GlobalMembersResourcesWP.GetImageThrow(theManager, 798, "IMAGE_TOOLTIP_ARROW_ARROW_UP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CRYSTALBALL_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 799, "IMAGE_CRYSTALBALL_SHADOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CRYSTALBALL_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 800, "IMAGE_CRYSTALBALL_GLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_ARROW_SWIPE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1321, "IMAGE_DIALOG_ARROW_SWIPE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_ARROW_SWIPEGLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1322, "IMAGE_DIALOG_ARROW_SWIPEGLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BLACK_BOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1323, "IMAGE_DIALOG_BLACK_BOX", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_DROPDOWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1324, "IMAGE_DIALOG_BUTTON_DROPDOWN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 1325, "IMAGE_DIALOG_BUTTON_FRAME", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_DIAMOND_MINE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1326, "IMAGE_DIALOG_BUTTON_FRAME_DIAMOND_MINE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_ICE_STORM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1327, "IMAGE_DIALOG_BUTTON_FRAME_ICE_STORM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_GAMECENTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1328, "IMAGE_DIALOG_BUTTON_GAMECENTER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_GAMECENTER_BG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1329, "IMAGE_DIALOG_BUTTON_GAMECENTER_BG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_LARGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1330, "IMAGE_DIALOG_BUTTON_LARGE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_SMALL_BG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1331, "IMAGE_DIALOG_BUTTON_SMALL_BG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_SMALL_BLUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1332, "IMAGE_DIALOG_BUTTON_SMALL_BLUE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1333, "IMAGE_DIALOG_CHECKBOX", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX_CHECKED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1334, "IMAGE_DIALOG_CHECKBOX_CHECKED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX_UNSELECTED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1335, "IMAGE_DIALOG_CHECKBOX_UNSELECTED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_BOX_INTERIOR_BG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1336, "IMAGE_DIALOG_DIALOG_BOX_INTERIOR_BG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_CORNER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1337, "IMAGE_DIALOG_DIALOG_CORNER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_SWIPE_BOTTOM_EDGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1338, "IMAGE_DIALOG_DIALOG_SWIPE_BOTTOM_EDGE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_SWIPE_TOP_EDGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1339, "IMAGE_DIALOG_DIALOG_SWIPE_TOP_EDGE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1340, "IMAGE_DIALOG_DIVIDER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1341, "IMAGE_DIALOG_DIVIDER_GEM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_HELP_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1342, "IMAGE_DIALOG_HELP_GLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_ICON_FLAME_LRG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1343, "IMAGE_DIALOG_ICON_FLAME_LRG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_ICON_HYPERCUBE_LRG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1344, "IMAGE_DIALOG_ICON_HYPERCUBE_LRG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_ICON_STAR_LRG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1345, "IMAGE_DIALOG_ICON_STAR_LRG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_BG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1346, "IMAGE_DIALOG_LISTBOX_BG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_BG_DARK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1347, "IMAGE_DIALOG_LISTBOX_BG_DARK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_FOOTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1348, "IMAGE_DIALOG_LISTBOX_FOOTER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_HEADER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1349, "IMAGE_DIALOG_LISTBOX_HEADER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1350, "IMAGE_DIALOG_LISTBOX_SHADOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_MINE_TILES_GEM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1351, "IMAGE_DIALOG_MINE_TILES_GEM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_MINE_TILES_GOLD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1352, "IMAGE_DIALOG_MINE_TILES_GOLD", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_MINE_TILES_TREASURE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1353, "IMAGE_DIALOG_MINE_TILES_TREASURE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1354, "IMAGE_DIALOG_PROGRESS_BAR", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_BG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1355, "IMAGE_DIALOG_PROGRESS_BAR_BG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_CROWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1356, "IMAGE_DIALOG_PROGRESS_BAR_CROWN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_FILL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1357, "IMAGE_DIALOG_PROGRESS_BAR_FILL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1358, "IMAGE_DIALOG_PROGRESS_BAR_GLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_REPLAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 1359, "IMAGE_DIALOG_REPLAY", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SCROLLBAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1360, "IMAGE_DIALOG_SCROLLBAR", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SFX_ICONS_MUSIC = GlobalMembersResourcesWP.GetImageThrow(theManager, 1361, "IMAGE_DIALOG_SFX_ICONS_MUSIC", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SFX_ICONS_MUSIC_UNSELECTED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1362, "IMAGE_DIALOG_SFX_ICONS_MUSIC_UNSELECTED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SFX_ICONS_SOUND = GlobalMembersResourcesWP.GetImageThrow(theManager, 1363, "IMAGE_DIALOG_SFX_ICONS_SOUND", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SFX_ICONS_SOUND_UNSELECTED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1364, "IMAGE_DIALOG_SFX_ICONS_SOUND_UNSELECTED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SFX_ICONS_VOICES = GlobalMembersResourcesWP.GetImageThrow(theManager, 1365, "IMAGE_DIALOG_SFX_ICONS_VOICES", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SFX_ICONS_VOICES_UNSELECTED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1366, "IMAGE_DIALOG_SFX_ICONS_VOICES_UNSELECTED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HANDLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1367, "IMAGE_DIALOG_SLIDER_BAR_HANDLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1368, "IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1369, "IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL_UNSE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1370, "IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL_UNSE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_UNSELECTE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1371, "IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_UNSELECTE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_VERTICAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1372, "IMAGE_DIALOG_SLIDER_BAR_VERTICAL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_VERTICAL_FILL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1373, "IMAGE_DIALOG_SLIDER_BAR_VERTICAL_FILL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_VERTICAL_FILL_UNSELE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1374, "IMAGE_DIALOG_SLIDER_BAR_VERTICAL_FILL_UNSELE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_VERTICAL_UNSELECTED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1375, "IMAGE_DIALOG_SLIDER_BAR_VERTICAL_UNSELECTED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_TEXTBOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1376, "IMAGE_DIALOG_TEXTBOX", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ALPHA_ALPHA_DOWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1377, "IMAGE_ALPHA_ALPHA_DOWN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ALPHA_ALPHA_UP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1378, "IMAGE_ALPHA_ALPHA_UP", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractCommon_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.FONT_HIRES_DIALOG = GlobalMembersResourcesWP.GetFontThrow(theManager, 0, "FONT_HIRES_DIALOG", 960, 0);
				GlobalMembersResourcesWP.FONT_LOWRES_DIALOG = GlobalMembersResourcesWP.GetFontThrow(theManager, 1, "FONT_LOWRES_DIALOG", 960, 0);
				GlobalMembersResourcesWP.FONT_HIRES_HEADER = GlobalMembersResourcesWP.GetFontThrow(theManager, 2, "FONT_HIRES_HEADER", 960, 0);
				GlobalMembersResourcesWP.FONT_LOWRES_HEADER = GlobalMembersResourcesWP.GetFontThrow(theManager, 3, "FONT_LOWRES_HEADER", 960, 0);
				GlobalMembersResourcesWP.FONT_HIRES_SUBHEADER = GlobalMembersResourcesWP.GetFontThrow(theManager, 4, "FONT_HIRES_SUBHEADER", 960, 0);
				GlobalMembersResourcesWP.FONT_LOWRES_SUBHEADER = GlobalMembersResourcesWP.GetFontThrow(theManager, 5, "FONT_LOWRES_SUBHEADER", 960, 0);
				GlobalMembersResourcesWP.FONT_HIRES_INGAME = GlobalMembersResourcesWP.GetFontThrow(theManager, 6, "FONT_HIRES_INGAME", 960, 0);
				GlobalMembersResourcesWP.FONT_LOWRES_INGAME = GlobalMembersResourcesWP.GetFontThrow(theManager, 7, "FONT_LOWRES_INGAME", 960, 0);
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_COMMON_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 35, "ATLASIMAGE_ATLAS_COMMON_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ANIMS_CARD_GEM_SPARKLE2_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 92, "IMAGE_ANIMS_CARD_GEM_SPARKLE2_0_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BUTTERFLY_HELP_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 93, "IMAGE_HELP_BUTTERFLY_HELP_0_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_CARD_GEM_SPARKLE2_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 94, "IMAGE_HELP_CARD_GEM_SPARKLE2_0_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_SPARKLE_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 95, "IMAGE_HELP_DIAMOND_SPARKLE_0_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_FLAMEGEM_HELP_0_FLAME63 = GlobalMembersResourcesWP.GetImageThrow(theManager, 96, "IMAGE_HELP_FLAMEGEM_HELP_0_FLAME63", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_FLAMEGEM_HELP_1_HELP_GREEN_NOSHDW = GlobalMembersResourcesWP.GetImageThrow(theManager, 97, "IMAGE_HELP_FLAMEGEM_HELP_1_HELP_GREEN_NOSHDW", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_FLAMEGEM_HELP_2_SPARKLET = GlobalMembersResourcesWP.GetImageThrow(theManager, 98, "IMAGE_HELP_FLAMEGEM_HELP_2_SPARKLET", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HELP_0_RING = GlobalMembersResourcesWP.GetImageThrow(theManager, 99, "IMAGE_HELP_ICESTORM_HELP_0_RING", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_STEAMPULSE_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 100, "IMAGE_HELP_LIGHTNING_STEAMPULSE_0_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_HELP_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 101, "IMAGE_HELP_STARGEM_HELP_0_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_HELP_1_STAR_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 102, "IMAGE_HELP_STARGEM_HELP_1_STAR_GLOW", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_HELP_2_CORONAGLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 103, "IMAGE_HELP_STARGEM_HELP_2_CORONAGLOW", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_HELP_3_HELP_GREEN_NOSHDW = GlobalMembersResourcesWP.GetImageThrow(theManager, 104, "IMAGE_HELP_STARGEM_HELP_3_HELP_GREEN_NOSHDW", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_CRYSTALRAYS_0_RAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 105, "IMAGE_PARTICLES_CRYSTALRAYS_0_RAY", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_CRYSTALBALL_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 106, "IMAGE_PARTICLES_CRYSTALBALL_0_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_QUEST_DIG_COLLECT_BASE_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 107, "IMAGE_PARTICLES_QUEST_DIG_COLLECT_BASE_0_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_QUEST_DIG_COLLECT_BASE_1_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 108, "IMAGE_PARTICLES_QUEST_DIG_COLLECT_BASE_1_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_QUEST_DIG_COLLECT_GOLD_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 109, "IMAGE_PARTICLES_QUEST_DIG_COLLECT_GOLD_0_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_QUEST_DIG_COLLECT_GOLD_1_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 110, "IMAGE_PARTICLES_QUEST_DIG_COLLECT_GOLD_1_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_SANDSTORM_DIG_0_SOFT_CLUMPY = GlobalMembersResourcesWP.GetImageThrow(theManager, 111, "IMAGE_PARTICLES_GEM_SANDSTORM_DIG_0_SOFT_CLUMPY", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_BADGE_UPGRADE_0_ICE = GlobalMembersResourcesWP.GetImageThrow(theManager, 112, "IMAGE_PARTICLES_BADGE_UPGRADE_0_ICE", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_BADGE_UPGRADE_1_BADGE_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 113, "IMAGE_PARTICLES_BADGE_UPGRADE_1_BADGE_GLOW", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_BADGE_UPGRADE_2_CERCLEM = GlobalMembersResourcesWP.GetImageThrow(theManager, 114, "IMAGE_PARTICLES_BADGE_UPGRADE_2_CERCLEM", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_BLOWING_SNOW_0_TEXTURE_01 = GlobalMembersResourcesWP.GetImageThrow(theManager, 115, "IMAGE_PARTICLES_BLOWING_SNOW_0_TEXTURE_01", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_BOARD_FLAME_EMBERS_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 116, "IMAGE_PARTICLES_BOARD_FLAME_EMBERS_0_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_CARD_GEM_SPARKLE_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 117, "IMAGE_PARTICLES_CARD_GEM_SPARKLE_0_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_COINSPARKLE_0_FLARE = GlobalMembersResourcesWP.GetImageThrow(theManager, 118, "IMAGE_PARTICLES_COINSPARKLE_0_FLARE", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_COUNTDOWNBAR_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 119, "IMAGE_PARTICLES_COUNTDOWNBAR_0_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_COUNTDOWNBAR_1_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 120, "IMAGE_PARTICLES_COUNTDOWNBAR_1_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 121, "IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_0_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_1_SNOWFLAKE = GlobalMembersResourcesWP.GetImageThrow(theManager, 122, "IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_1_SNOWFLAKE", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_2_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 123, "IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_2_ICECHUNK", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DANGERSNOW_SOFT_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 124, "IMAGE_PARTICLES_DANGERSNOW_SOFT_0_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DANGERSNOW_SOFT_1_SNOWFLAKE = GlobalMembersResourcesWP.GetImageThrow(theManager, 125, "IMAGE_PARTICLES_DANGERSNOW_SOFT_1_SNOWFLAKE", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DISCOBALL_0_DISCO_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 126, "IMAGE_PARTICLES_DISCOBALL_0_DISCO_GLOW", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DISCOBALL_1_DISCO_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 127, "IMAGE_PARTICLES_DISCOBALL_1_DISCO_GLOW", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_DISCOBALL_2_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 128, "IMAGE_PARTICLES_DISCOBALL_2_BLURRED_SHARP_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_FIREGEM_HYPERSPACE_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 129, "IMAGE_PARTICLES_FIREGEM_HYPERSPACE_0_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_FLAME_CARD_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 130, "IMAGE_PARTICLES_FLAME_CARD_0_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_BLASTGEM_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 131, "IMAGE_PARTICLES_GEM_BLASTGEM_0_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_BUTTERFLY_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 132, "IMAGE_PARTICLES_GEM_BUTTERFLY_0_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_BUTTERFLY_CREATE_0_FLOWER = GlobalMembersResourcesWP.GetImageThrow(theManager, 133, "IMAGE_PARTICLES_GEM_BUTTERFLY_CREATE_0_FLOWER", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_BUTTERFLY_CREATE_1_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 134, "IMAGE_PARTICLES_GEM_BUTTERFLY_CREATE_1_BLURRED_SHARP_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_DIAMOND_SPARKLES_0_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 135, "IMAGE_PARTICLES_GEM_DIAMOND_SPARKLES_0_BLURRED_SHARP_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_FIRE_TRAIL_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 136, "IMAGE_PARTICLES_GEM_FIRE_TRAIL_0_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_GOLD_BLING_0_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 137, "IMAGE_PARTICLES_GEM_GOLD_BLING_0_BLURRED_SHARP_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_HINTFLASH_0_CERCLEM = GlobalMembersResourcesWP.GetImageThrow(theManager, 138, "IMAGE_PARTICLES_GEM_HINTFLASH_0_CERCLEM", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_HYPERCUBE_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 139, "IMAGE_PARTICLES_GEM_HYPERCUBE_0_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_ICE_TRAIL_0_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 140, "IMAGE_PARTICLES_GEM_ICE_TRAIL_0_BLURRED_SHARP_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_ICE_TRAIL_1_COMIC_SMOKE2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 141, "IMAGE_PARTICLES_GEM_ICE_TRAIL_1_COMIC_SMOKE2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_MULTIPLIER_0_RAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 142, "IMAGE_PARTICLES_GEM_MULTIPLIER_0_RAY", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_STARGEM_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 143, "IMAGE_PARTICLES_GEM_STARGEM_0_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_STARGEM_1_STAR_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 144, "IMAGE_PARTICLES_GEM_STARGEM_1_STAR_GLOW", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_GEM_STARGEM_2_CORONAGLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 145, "IMAGE_PARTICLES_GEM_STARGEM_2_CORONAGLOW", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_ICE_STORMY_0_SHARD = GlobalMembersResourcesWP.GetImageThrow(theManager, 146, "IMAGE_PARTICLES_ICE_STORMY_0_SHARD", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_LEVELBAR_0_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 147, "IMAGE_PARTICLES_LEVELBAR_0_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_LEVELBAR_1_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 148, "IMAGE_PARTICLES_LEVELBAR_1_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_SANDSTORM_COVER_0_SAND_PARTICLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 149, "IMAGE_PARTICLES_SANDSTORM_COVER_0_SAND_PARTICLE", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_SKULL_EXPLODE_0_DOT_STREAK_01 = GlobalMembersResourcesWP.GetImageThrow(theManager, 150, "IMAGE_PARTICLES_SKULL_EXPLODE_0_DOT_STREAK_01", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_SKULL_EXPLODE_1_SKULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 151, "IMAGE_PARTICLES_SKULL_EXPLODE_1_SKULL", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_SKULL_EXPLODE_2_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 152, "IMAGE_PARTICLES_SKULL_EXPLODE_2_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_SPEEDBOARD_FLAME_0_FLAME1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 153, "IMAGE_PARTICLES_SPEEDBOARD_FLAME_0_FLAME1", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_STAR_CARD_0_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 154, "IMAGE_PARTICLES_STAR_CARD_0_BLURRED_SHARP_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_STAR_CARD_1_SMALL_BLUR_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 155, "IMAGE_PARTICLES_STAR_CARD_1_SMALL_BLUR_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_STARBURST_0_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 156, "IMAGE_PARTICLES_STARBURST_0_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_WEIGHT_FIRE_0_TRUEFLAME7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 157, "IMAGE_PARTICLES_WEIGHT_FIRE_0_TRUEFLAME7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_WEIGHT_FIRE_1_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 158, "IMAGE_PARTICLES_WEIGHT_FIRE_1_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_WEIGHT_ICE_0_DIM_BLUR_CLOUD = GlobalMembersResourcesWP.GetImageThrow(theManager, 159, "IMAGE_PARTICLES_WEIGHT_ICE_0_DIM_BLUR_CLOUD", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_PARTICLES_WEIGHT_ICE_1_ICESHARD_0000 = GlobalMembersResourcesWP.GetImageThrow(theManager, 160, "IMAGE_PARTICLES_WEIGHT_ICE_1_ICESHARD_0000", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIG_LINE_HIT_0_BASIC_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 161, "IMAGE_QUEST_DIG_DIG_LINE_HIT_0_BASIC_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIG_LINE_HIT_MEGA_0_BLURRED_SHARP_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 162, "IMAGE_QUEST_DIG_DIG_LINE_HIT_MEGA_0_BLURRED_SHARP_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIG_LINE_HIT_MEGA_1_DIAMOND_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 163, "IMAGE_QUEST_DIG_DIG_LINE_HIT_MEGA_1_DIAMOND_STAR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_LOGO = GlobalMembersResourcesWP.GetImageThrow(theManager, 698, "IMAGE_MAIN_MENU_LOGO", 960, 0);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_CLOUDS = GlobalMembersResourcesWP.GetImageThrow(theManager, 699, "IMAGE_MAIN_MENU_CLOUDS", 960, 0);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_BACKGROUND = GlobalMembersResourcesWP.GetImageThrow(theManager, 700, "IMAGE_MAIN_MENU_BACKGROUND", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_CLOSED_BUTTON = GlobalMembersResourcesWP.GetImageThrow(theManager, 705, "IMAGE_DASHBOARD_CLOSED_BUTTON", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_DASH_MAIN = GlobalMembersResourcesWP.GetImageThrow(theManager, 706, "IMAGE_DASHBOARD_DASH_MAIN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_DASH_TILE = GlobalMembersResourcesWP.GetImageThrow(theManager, 707, "IMAGE_DASHBOARD_DASH_TILE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_DM_OVERLAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 708, "IMAGE_DASHBOARD_DM_OVERLAY", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_ICE_OVERLAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 709, "IMAGE_DASHBOARD_ICE_OVERLAY", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_MENU_DOWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 710, "IMAGE_DASHBOARD_MENU_DOWN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DASHBOARD_MENU_UP = GlobalMembersResourcesWP.GetImageThrow(theManager, 711, "IMAGE_DASHBOARD_MENU_UP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS0 = GlobalMembersResourcesWP.GetImageThrow(theManager, 742, "IMAGE_PPS0", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 743, "IMAGE_PPS1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 744, "IMAGE_PPS2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 745, "IMAGE_PPS3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 746, "IMAGE_PPS4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 747, "IMAGE_PPS5", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 748, "IMAGE_PPS6", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 749, "IMAGE_PPS7", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 750, "IMAGE_PPS8", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 751, "IMAGE_PPS9", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 752, "IMAGE_PPS10", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 753, "IMAGE_PPS11", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 754, "IMAGE_PPS12", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 755, "IMAGE_PPS13", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 756, "IMAGE_PPS14", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 757, "IMAGE_PPS15", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS16 = GlobalMembersResourcesWP.GetImageThrow(theManager, 758, "IMAGE_PPS16", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 759, "IMAGE_PPS17", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS18 = GlobalMembersResourcesWP.GetImageThrow(theManager, 760, "IMAGE_PPS18", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 761, "IMAGE_PPS19", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 762, "IMAGE_PPS20", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 763, "IMAGE_PPS21", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 764, "IMAGE_PPS22", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS23 = GlobalMembersResourcesWP.GetImageThrow(theManager, 765, "IMAGE_PPS23", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS24 = GlobalMembersResourcesWP.GetImageThrow(theManager, 766, "IMAGE_PPS24", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 767, "IMAGE_PPS25", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 768, "IMAGE_PPS26", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS27 = GlobalMembersResourcesWP.GetImageThrow(theManager, 769, "IMAGE_PPS27", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS28 = GlobalMembersResourcesWP.GetImageThrow(theManager, 770, "IMAGE_PPS28", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PPS29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 771, "IMAGE_PPS29", 960, 0);
				GlobalMembersResourcesWP.IMAGE_TOOLTIP = GlobalMembersResourcesWP.GetImageThrow(theManager, 794, "IMAGE_TOOLTIP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_TOOLTIP_ARROW_ARROW_DOWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 795, "IMAGE_TOOLTIP_ARROW_ARROW_DOWN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_TOOLTIP_ARROW_ARROW_LEFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 796, "IMAGE_TOOLTIP_ARROW_ARROW_LEFT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_TOOLTIP_ARROW_ARROW_RIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 797, "IMAGE_TOOLTIP_ARROW_ARROW_RIGHT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_TOOLTIP_ARROW_ARROW_UP = GlobalMembersResourcesWP.GetImageThrow(theManager, 798, "IMAGE_TOOLTIP_ARROW_ARROW_UP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CRYSTALBALL_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 799, "IMAGE_CRYSTALBALL_SHADOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CRYSTALBALL_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 800, "IMAGE_CRYSTALBALL_GLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_ARROW_SWIPE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1321, "IMAGE_DIALOG_ARROW_SWIPE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_ARROW_SWIPEGLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1322, "IMAGE_DIALOG_ARROW_SWIPEGLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BLACK_BOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1323, "IMAGE_DIALOG_BLACK_BOX", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_DROPDOWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1324, "IMAGE_DIALOG_BUTTON_DROPDOWN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 1325, "IMAGE_DIALOG_BUTTON_FRAME", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_DIAMOND_MINE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1326, "IMAGE_DIALOG_BUTTON_FRAME_DIAMOND_MINE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_FRAME_ICE_STORM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1327, "IMAGE_DIALOG_BUTTON_FRAME_ICE_STORM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_GAMECENTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1328, "IMAGE_DIALOG_BUTTON_GAMECENTER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_GAMECENTER_BG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1329, "IMAGE_DIALOG_BUTTON_GAMECENTER_BG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_LARGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1330, "IMAGE_DIALOG_BUTTON_LARGE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_SMALL_BG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1331, "IMAGE_DIALOG_BUTTON_SMALL_BG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_BUTTON_SMALL_BLUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1332, "IMAGE_DIALOG_BUTTON_SMALL_BLUE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1333, "IMAGE_DIALOG_CHECKBOX", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX_CHECKED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1334, "IMAGE_DIALOG_CHECKBOX_CHECKED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_CHECKBOX_UNSELECTED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1335, "IMAGE_DIALOG_CHECKBOX_UNSELECTED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_BOX_INTERIOR_BG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1336, "IMAGE_DIALOG_DIALOG_BOX_INTERIOR_BG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_CORNER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1337, "IMAGE_DIALOG_DIALOG_CORNER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_SWIPE_BOTTOM_EDGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1338, "IMAGE_DIALOG_DIALOG_SWIPE_BOTTOM_EDGE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_DIALOG_SWIPE_TOP_EDGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1339, "IMAGE_DIALOG_DIALOG_SWIPE_TOP_EDGE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1340, "IMAGE_DIALOG_DIVIDER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_DIVIDER_GEM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1341, "IMAGE_DIALOG_DIVIDER_GEM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_HELP_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1342, "IMAGE_DIALOG_HELP_GLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_ICON_FLAME_LRG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1343, "IMAGE_DIALOG_ICON_FLAME_LRG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_ICON_HYPERCUBE_LRG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1344, "IMAGE_DIALOG_ICON_HYPERCUBE_LRG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_ICON_STAR_LRG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1345, "IMAGE_DIALOG_ICON_STAR_LRG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_BG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1346, "IMAGE_DIALOG_LISTBOX_BG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_BG_DARK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1347, "IMAGE_DIALOG_LISTBOX_BG_DARK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_FOOTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1348, "IMAGE_DIALOG_LISTBOX_FOOTER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_HEADER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1349, "IMAGE_DIALOG_LISTBOX_HEADER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_LISTBOX_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1350, "IMAGE_DIALOG_LISTBOX_SHADOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_MINE_TILES_GEM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1351, "IMAGE_DIALOG_MINE_TILES_GEM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_MINE_TILES_GOLD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1352, "IMAGE_DIALOG_MINE_TILES_GOLD", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_MINE_TILES_TREASURE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1353, "IMAGE_DIALOG_MINE_TILES_TREASURE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1354, "IMAGE_DIALOG_PROGRESS_BAR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_BG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1355, "IMAGE_DIALOG_PROGRESS_BAR_BG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_CROWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1356, "IMAGE_DIALOG_PROGRESS_BAR_CROWN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_FILL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1357, "IMAGE_DIALOG_PROGRESS_BAR_FILL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_PROGRESS_BAR_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1358, "IMAGE_DIALOG_PROGRESS_BAR_GLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_REPLAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 1359, "IMAGE_DIALOG_REPLAY", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SCROLLBAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1360, "IMAGE_DIALOG_SCROLLBAR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SFX_ICONS_MUSIC = GlobalMembersResourcesWP.GetImageThrow(theManager, 1361, "IMAGE_DIALOG_SFX_ICONS_MUSIC", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SFX_ICONS_MUSIC_UNSELECTED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1362, "IMAGE_DIALOG_SFX_ICONS_MUSIC_UNSELECTED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SFX_ICONS_SOUND = GlobalMembersResourcesWP.GetImageThrow(theManager, 1363, "IMAGE_DIALOG_SFX_ICONS_SOUND", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SFX_ICONS_SOUND_UNSELECTED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1364, "IMAGE_DIALOG_SFX_ICONS_SOUND_UNSELECTED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SFX_ICONS_VOICES = GlobalMembersResourcesWP.GetImageThrow(theManager, 1365, "IMAGE_DIALOG_SFX_ICONS_VOICES", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SFX_ICONS_VOICES_UNSELECTED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1366, "IMAGE_DIALOG_SFX_ICONS_VOICES_UNSELECTED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HANDLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1367, "IMAGE_DIALOG_SLIDER_BAR_HANDLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1368, "IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1369, "IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL_UNSE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1370, "IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL_UNSE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_UNSELECTE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1371, "IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_UNSELECTE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_VERTICAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1372, "IMAGE_DIALOG_SLIDER_BAR_VERTICAL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_VERTICAL_FILL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1373, "IMAGE_DIALOG_SLIDER_BAR_VERTICAL_FILL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_VERTICAL_FILL_UNSELE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1374, "IMAGE_DIALOG_SLIDER_BAR_VERTICAL_FILL_UNSELE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_SLIDER_BAR_VERTICAL_UNSELECTED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1375, "IMAGE_DIALOG_SLIDER_BAR_VERTICAL_UNSELECTED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIALOG_TEXTBOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1376, "IMAGE_DIALOG_TEXTBOX", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ALPHA_ALPHA_DOWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1377, "IMAGE_ALPHA_ALPHA_DOWN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ALPHA_ALPHA_UP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1378, "IMAGE_ALPHA_ALPHA_UP", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractCommon_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.PIEFFECT_ANIMS_CARD_GEM_SPARKLE2 = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1477, "PIEFFECT_ANIMS_CARD_GEM_SPARKLE2", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_HELP_BUTTERFLY_HELP = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1478, "PIEFFECT_HELP_BUTTERFLY_HELP", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_HELP_CARD_GEM_SPARKLE2 = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1479, "PIEFFECT_HELP_CARD_GEM_SPARKLE2", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_HELP_DIAMOND_SPARKLE = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1480, "PIEFFECT_HELP_DIAMOND_SPARKLE", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_HELP_FLAMEGEM_HELP = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1481, "PIEFFECT_HELP_FLAMEGEM_HELP", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_HELP_ICESTORM_HELP = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1482, "PIEFFECT_HELP_ICESTORM_HELP", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_HELP_LIGHTNING_STEAMPULSE = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1483, "PIEFFECT_HELP_LIGHTNING_STEAMPULSE", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_HELP_STARGEM_HELP = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1484, "PIEFFECT_HELP_STARGEM_HELP", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_CRYSTALBALL = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1485, "PIEFFECT_CRYSTALBALL", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_CRYSTALRAYS = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1486, "PIEFFECT_CRYSTALRAYS", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_COLLECT_BASE = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1487, "PIEFFECT_QUEST_DIG_COLLECT_BASE", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_COLLECT_GOLD = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1488, "PIEFFECT_QUEST_DIG_COLLECT_GOLD", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_SANDSTORM_DIG = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1489, "PIEFFECT_SANDSTORM_DIG", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_BADGE_UPGRADE = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1490, "PIEFFECT_BADGE_UPGRADE", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_BLASTGEM = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1491, "PIEFFECT_BLASTGEM", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_BLOWING_SNOW = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1492, "PIEFFECT_BLOWING_SNOW", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_BOARD_FLAME_EMBERS = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1493, "PIEFFECT_BOARD_FLAME_EMBERS", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_BUTTERFLY = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1494, "PIEFFECT_BUTTERFLY", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_BUTTERFLY_CREATE = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1495, "PIEFFECT_BUTTERFLY_CREATE", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_CARD_GEM_SPARKLE = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1496, "PIEFFECT_CARD_GEM_SPARKLE", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_COINSPARKLE = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1497, "PIEFFECT_COINSPARKLE", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_COUNTDOWNBAR = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1498, "PIEFFECT_COUNTDOWNBAR", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_DANGERSNOW_HARD_TOP = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1499, "PIEFFECT_DANGERSNOW_HARD_TOP", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_DANGERSNOW_SOFT = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1500, "PIEFFECT_DANGERSNOW_SOFT", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_DIAMOND_SPARKLES = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1501, "PIEFFECT_DIAMOND_SPARKLES", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_DISCOBALL = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1502, "PIEFFECT_DISCOBALL", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_FIRE_TRAIL = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1503, "PIEFFECT_FIRE_TRAIL", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_FIREGEM_HYPERSPACE = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1504, "PIEFFECT_FIREGEM_HYPERSPACE", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_FLAME_CARD = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1505, "PIEFFECT_FLAME_CARD", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_GOLD_BLING = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1506, "PIEFFECT_GOLD_BLING", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_HINTFLASH = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1507, "PIEFFECT_HINTFLASH", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_HYPERCUBE = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1508, "PIEFFECT_HYPERCUBE", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_ICE_STORMY = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1509, "PIEFFECT_ICE_STORMY", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_ICE_TRAIL = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1510, "PIEFFECT_ICE_TRAIL", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_LEVELBAR = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1511, "PIEFFECT_LEVELBAR", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_MULTIPLIER = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1512, "PIEFFECT_MULTIPLIER", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_SANDSTORM_COVER = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1513, "PIEFFECT_SANDSTORM_COVER", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_SKULL_EXPLODE = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1514, "PIEFFECT_SKULL_EXPLODE", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_SPEEDBOARD_FLAME = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1515, "PIEFFECT_SPEEDBOARD_FLAME", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_STAR_CARD = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1516, "PIEFFECT_STAR_CARD", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_STARBURST = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1517, "PIEFFECT_STARBURST", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_STARGEM = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1518, "PIEFFECT_STARGEM", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_WEIGHT_FIRE = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1519, "PIEFFECT_WEIGHT_FIRE", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_WEIGHT_ICE = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1520, "PIEFFECT_WEIGHT_ICE", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1521, "PIEFFECT_QUEST_DIG_DIG_LINE_HIT", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_QUEST_DIG_DIG_LINE_HIT_MEGA = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1522, "PIEFFECT_QUEST_DIG_DIG_LINE_HIT_MEGA", 0, 0);
				GlobalMembersResourcesWP.SOUND_BACKGROUND_CHANGE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1576, "SOUND_BACKGROUND_CHANGE", 0, 0);
				GlobalMembersResourcesWP.SOUND_MULTIPLIER_UP2_1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1577, "SOUND_MULTIPLIER_UP2_1", 0, 0);
				GlobalMembersResourcesWP.SOUND_MULTIPLIER_UP2_2 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1578, "SOUND_MULTIPLIER_UP2_2", 0, 0);
				GlobalMembersResourcesWP.SOUND_MULTIPLIER_UP2_3 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1579, "SOUND_MULTIPLIER_UP2_3", 0, 0);
				GlobalMembersResourcesWP.SOUND_MULTIPLIER_UP2_4 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1580, "SOUND_MULTIPLIER_UP2_4", 0, 0);
				GlobalMembersResourcesWP.SOUND_BUTTON_MOUSEOVER = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1581, "SOUND_BUTTON_MOUSEOVER", 0, 0);
				GlobalMembersResourcesWP.SOUND_BUTTON_MOUSELEAVE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1582, "SOUND_BUTTON_MOUSELEAVE", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUEST_MENU_BUTTON_MOUSEOVER1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1583, "SOUND_QUEST_MENU_BUTTON_MOUSEOVER1", 0, 0);
				GlobalMembersResourcesWP.SOUND_COMBO_1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1584, "SOUND_COMBO_1", 0, 0);
				GlobalMembersResourcesWP.SOUND_COMBO_2 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1585, "SOUND_COMBO_2", 0, 0);
				GlobalMembersResourcesWP.SOUND_COMBO_3 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1586, "SOUND_COMBO_3", 0, 0);
				GlobalMembersResourcesWP.SOUND_COMBO_4 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1587, "SOUND_COMBO_4", 0, 0);
				GlobalMembersResourcesWP.SOUND_COMBO_5 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1588, "SOUND_COMBO_5", 0, 0);
				GlobalMembersResourcesWP.SOUND_COMBO_6 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1589, "SOUND_COMBO_6", 0, 0);
				GlobalMembersResourcesWP.SOUND_COMBO_7 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1590, "SOUND_COMBO_7", 0, 0);
				GlobalMembersResourcesWP.SOUND_BADMOVE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1591, "SOUND_BADMOVE", 0, 0);
				GlobalMembersResourcesWP.SOUND_FIREWORK_CRACKLE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1592, "SOUND_FIREWORK_CRACKLE", 0, 0);
				GlobalMembersResourcesWP.SOUND_FIREWORK_LAUNCH = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1593, "SOUND_FIREWORK_LAUNCH", 0, 0);
				GlobalMembersResourcesWP.SOUND_FIREWORK_THUMP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1594, "SOUND_FIREWORK_THUMP", 0, 0);
				GlobalMembersResourcesWP.SOUND_GEM_HIT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1595, "SOUND_GEM_HIT", 0, 0);
				GlobalMembersResourcesWP.SOUND_PREBLAST = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1596, "SOUND_PREBLAST", 0, 0);
				GlobalMembersResourcesWP.SOUND_SELECT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1597, "SOUND_SELECT", 0, 0);
				GlobalMembersResourcesWP.SOUND_START_ROTATE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1598, "SOUND_START_ROTATE", 0, 0);
				GlobalMembersResourcesWP.SOUND_ALCHEMY_CONVERT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1599, "SOUND_ALCHEMY_CONVERT", 0, 0);
				GlobalMembersResourcesWP.SOUND_BACKTOMAIN = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1600, "SOUND_BACKTOMAIN", 0, 0);
				GlobalMembersResourcesWP.SOUND_BADGEAWARDED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1601, "SOUND_BADGEAWARDED", 0, 0);
				GlobalMembersResourcesWP.SOUND_BADGEFALL = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1602, "SOUND_BADGEFALL", 0, 0);
				GlobalMembersResourcesWP.SOUND_BOMB_APPEARS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1603, "SOUND_BOMB_APPEARS", 0, 0);
				GlobalMembersResourcesWP.SOUND_BOMB_EXPLODE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1604, "SOUND_BOMB_EXPLODE", 0, 0);
				GlobalMembersResourcesWP.SOUND_BREATH_IN = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1605, "SOUND_BREATH_IN", 0, 0);
				GlobalMembersResourcesWP.SOUND_BREATH_OUT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1606, "SOUND_BREATH_OUT", 0, 0);
				GlobalMembersResourcesWP.SOUND_BUTTERFLY_APPEAR = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1607, "SOUND_BUTTERFLY_APPEAR", 0, 0);
				GlobalMembersResourcesWP.SOUND_BUTTERFLY_DEATH1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1608, "SOUND_BUTTERFLY_DEATH1", 0, 0);
				GlobalMembersResourcesWP.SOUND_BUTTERFLYESCAPE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1609, "SOUND_BUTTERFLYESCAPE", 0, 0);
				GlobalMembersResourcesWP.SOUND_BUTTON_PRESS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1610, "SOUND_BUTTON_PRESS", 0, 0);
				GlobalMembersResourcesWP.SOUND_BUTTON_RELEASE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1611, "SOUND_BUTTON_RELEASE", 0, 0);
				GlobalMembersResourcesWP.SOUND_CARDDEAL = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1612, "SOUND_CARDDEAL", 0, 0);
				GlobalMembersResourcesWP.SOUND_CARDFLIP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1613, "SOUND_CARDFLIP", 0, 0);
				GlobalMembersResourcesWP.SOUND_CLICKFLYIN = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1614, "SOUND_CLICKFLYIN", 0, 0);
				GlobalMembersResourcesWP.SOUND_COIN_CREATED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1615, "SOUND_COIN_CREATED", 0, 0);
				GlobalMembersResourcesWP.SOUND_COINAPPEAR = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1616, "SOUND_COINAPPEAR", 0, 0);
				GlobalMembersResourcesWP.SOUND_COLD_WIND = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1617, "SOUND_COLD_WIND", 0, 0);
				GlobalMembersResourcesWP.SOUND_COUNTDOWN_WARNING = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1618, "SOUND_COUNTDOWN_WARNING", 0, 0);
				GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_ARTIFACT_SHOWCASE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1619, "SOUND_DIAMOND_MINE_ARTIFACT_SHOWCASE", 0, 0);
				GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_BIGSTONE_CRACKED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1620, "SOUND_DIAMOND_MINE_BIGSTONE_CRACKED", 0, 0);
				GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_DEATH = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1621, "SOUND_DIAMOND_MINE_DEATH", 0, 0);
				GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_DIG = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1622, "SOUND_DIAMOND_MINE_DIG", 0, 0);
				GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_DIG_LINE_HIT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1623, "SOUND_DIAMOND_MINE_DIG_LINE_HIT", 0, 0);
				GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_DIG_LINE_HIT_MEGA = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1624, "SOUND_DIAMOND_MINE_DIG_LINE_HIT_MEGA", 0, 0);
				GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_DIG_NOTIFY = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1625, "SOUND_DIAMOND_MINE_DIG_NOTIFY", 0, 0);
				GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_DIRT_CRACKED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1626, "SOUND_DIAMOND_MINE_DIRT_CRACKED", 0, 0);
				GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_STONE_CRACKED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1627, "SOUND_DIAMOND_MINE_STONE_CRACKED", 0, 0);
				GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_TREASUREFIND = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1628, "SOUND_DIAMOND_MINE_TREASUREFIND", 0, 0);
				GlobalMembersResourcesWP.SOUND_DIAMOND_MINE_TREASUREFIND_DIAMONDS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1629, "SOUND_DIAMOND_MINE_TREASUREFIND_DIAMONDS", 0, 0);
				GlobalMembersResourcesWP.SOUND_DOUBLESET = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1630, "SOUND_DOUBLESET", 0, 0);
				GlobalMembersResourcesWP.SOUND_EARTHQUAKE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1631, "SOUND_EARTHQUAKE", 0, 0);
				GlobalMembersResourcesWP.SOUND_ELECTRO_EXPLODE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1632, "SOUND_ELECTRO_EXPLODE", 0, 0);
				GlobalMembersResourcesWP.SOUND_ELECTRO_PATH = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1633, "SOUND_ELECTRO_PATH", 0, 0);
				GlobalMembersResourcesWP.SOUND_ELECTRO_PATH2 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1634, "SOUND_ELECTRO_PATH2", 0, 0);
				GlobalMembersResourcesWP.SOUND_FLAMEBONUS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1635, "SOUND_FLAMEBONUS", 0, 0);
				GlobalMembersResourcesWP.SOUND_FLAMELOOP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1636, "SOUND_FLAMELOOP", 0, 0);
				GlobalMembersResourcesWP.SOUND_FLAMESPEED1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1637, "SOUND_FLAMESPEED1", 0, 0);
				GlobalMembersResourcesWP.SOUND_GEM_COUNTDOWN_DESTROYED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1638, "SOUND_GEM_COUNTDOWN_DESTROYED", 0, 0);
				GlobalMembersResourcesWP.SOUND_GEM_SHATTERS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1639, "SOUND_GEM_SHATTERS", 0, 0);
				GlobalMembersResourcesWP.SOUND_HINT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1640, "SOUND_HINT", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERCUBE_CREATE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1641, "SOUND_HYPERCUBE_CREATE", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1642, "SOUND_HYPERSPACE", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1643, "SOUND_HYPERSPACE_GEM_LAND_1", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_2 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1644, "SOUND_HYPERSPACE_GEM_LAND_2", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_3 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1645, "SOUND_HYPERSPACE_GEM_LAND_3", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_4 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1646, "SOUND_HYPERSPACE_GEM_LAND_4", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_5 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1647, "SOUND_HYPERSPACE_GEM_LAND_5", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_6 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1648, "SOUND_HYPERSPACE_GEM_LAND_6", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_7 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1649, "SOUND_HYPERSPACE_GEM_LAND_7", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_ZEN_1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1650, "SOUND_HYPERSPACE_GEM_LAND_ZEN_1", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_ZEN_2 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1651, "SOUND_HYPERSPACE_GEM_LAND_ZEN_2", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_ZEN_3 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1652, "SOUND_HYPERSPACE_GEM_LAND_ZEN_3", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_ZEN_4 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1653, "SOUND_HYPERSPACE_GEM_LAND_ZEN_4", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_ZEN_5 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1654, "SOUND_HYPERSPACE_GEM_LAND_ZEN_5", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_ZEN_6 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1655, "SOUND_HYPERSPACE_GEM_LAND_ZEN_6", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_GEM_LAND_ZEN_7 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1656, "SOUND_HYPERSPACE_GEM_LAND_ZEN_7", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_SHATTER_1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1657, "SOUND_HYPERSPACE_SHATTER_1", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_SHATTER_2 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1658, "SOUND_HYPERSPACE_SHATTER_2", 0, 0);
				GlobalMembersResourcesWP.SOUND_HYPERSPACE_SHATTER_ZEN = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1659, "SOUND_HYPERSPACE_SHATTER_ZEN", 0, 0);
				GlobalMembersResourcesWP.SOUND_ICE_COLUMN_APPEARS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1660, "SOUND_ICE_COLUMN_APPEARS", 0, 0);
				GlobalMembersResourcesWP.SOUND_ICE_COLUMN_BREAK = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1661, "SOUND_ICE_COLUMN_BREAK", 0, 0);
				GlobalMembersResourcesWP.SOUND_ICE_STORM_COLUMNCOMBO = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1662, "SOUND_ICE_STORM_COLUMNCOMBO", 0, 0);
				GlobalMembersResourcesWP.SOUND_ICE_STORM_COLUMNCOMBO_MEGA = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1663, "SOUND_ICE_STORM_COLUMNCOMBO_MEGA", 0, 0);
				GlobalMembersResourcesWP.SOUND_ICE_STORM_FINAL_THUD = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1664, "SOUND_ICE_STORM_FINAL_THUD", 0, 0);
				GlobalMembersResourcesWP.SOUND_ICE_STORM_GAMEOVER = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1665, "SOUND_ICE_STORM_GAMEOVER", 0, 0);
				GlobalMembersResourcesWP.SOUND_ICE_STORM_MULTIPLER_UP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1666, "SOUND_ICE_STORM_MULTIPLER_UP", 0, 0);
				GlobalMembersResourcesWP.SOUND_ICE_STORM_STEAM_BUILD_UP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1667, "SOUND_ICE_STORM_STEAM_BUILD_UP", 0, 0);
				GlobalMembersResourcesWP.SOUND_ICE_STORM_STEAM_VALVE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1668, "SOUND_ICE_STORM_STEAM_VALVE", 0, 0);
				GlobalMembersResourcesWP.SOUND_ICE_STORM_WIND = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1669, "SOUND_ICE_STORM_WIND", 0, 0);
				GlobalMembersResourcesWP.SOUND_ICE_WARNING = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1670, "SOUND_ICE_WARNING", 0, 0);
				GlobalMembersResourcesWP.SOUND_LASERGEM_CREATED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1671, "SOUND_LASERGEM_CREATED", 0, 0);
				GlobalMembersResourcesWP.SOUND_LIGHTNING_ENERGIZE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1672, "SOUND_LIGHTNING_ENERGIZE", 0, 0);
				GlobalMembersResourcesWP.SOUND_LIGHTNING_HUMLOOP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1673, "SOUND_LIGHTNING_HUMLOOP", 0, 0);
				GlobalMembersResourcesWP.SOUND_LIGHTNING_TUBE_FILL_5 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1674, "SOUND_LIGHTNING_TUBE_FILL_5", 0, 0);
				GlobalMembersResourcesWP.SOUND_LIGHTNING_TUBE_FILL_10 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1675, "SOUND_LIGHTNING_TUBE_FILL_10", 0, 0);
				GlobalMembersResourcesWP.SOUND_MENUSPIN = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1676, "SOUND_MENUSPIN", 0, 0);
				GlobalMembersResourcesWP.SOUND_MULTIPLIER_APPEARS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1677, "SOUND_MULTIPLIER_APPEARS", 0, 0);
				GlobalMembersResourcesWP.SOUND_MULTIPLIER_HURRAHED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1678, "SOUND_MULTIPLIER_HURRAHED", 0, 0);
				GlobalMembersResourcesWP.SOUND_POKER_4OFAKIND = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1679, "SOUND_POKER_4OFAKIND", 0, 0);
				GlobalMembersResourcesWP.SOUND_POKER_FLUSH = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1680, "SOUND_POKER_FLUSH", 0, 0);
				GlobalMembersResourcesWP.SOUND_POKER_FULLHOUSE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1681, "SOUND_POKER_FULLHOUSE", 0, 0);
				GlobalMembersResourcesWP.SOUND_POKERCHIPS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1682, "SOUND_POKERCHIPS", 0, 0);
				GlobalMembersResourcesWP.SOUND_POKERSCORE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1683, "SOUND_POKERSCORE", 0, 0);
				GlobalMembersResourcesWP.SOUND_POWERGEM_CREATED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1684, "SOUND_POWERGEM_CREATED", 0, 0);
				GlobalMembersResourcesWP.SOUND_PULLEYS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1685, "SOUND_PULLEYS", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUEST_AWARD_WREATH = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1686, "SOUND_QUEST_AWARD_WREATH", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUEST_GET = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1687, "SOUND_QUEST_GET", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUEST_MENU_BUTTON1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1688, "SOUND_QUEST_MENU_BUTTON1", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUEST_NOTIFY = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1689, "SOUND_QUEST_NOTIFY", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUEST_ORB1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1690, "SOUND_QUEST_ORB1", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUEST_ORB3 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1691, "SOUND_QUEST_ORB3", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUEST_SANDSTORM_COVER = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1692, "SOUND_QUEST_SANDSTORM_COVER", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUEST_SANDSTORM_REVEAL = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1693, "SOUND_QUEST_SANDSTORM_REVEAL", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUESTMENU_RELICCOMPLETE_OBJECT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1694, "SOUND_QUESTMENU_RELICCOMPLETE_OBJECT", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUESTMENU_RELICCOMPLETE_RUMBLE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1695, "SOUND_QUESTMENU_RELICCOMPLETE_RUMBLE", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUESTMENU_RELICREVEALED_OBJECT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1696, "SOUND_QUESTMENU_RELICREVEALED_OBJECT", 0, 0);
				GlobalMembersResourcesWP.SOUND_QUESTMENU_RELICREVEALED_RUMBLE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1697, "SOUND_QUESTMENU_RELICREVEALED_RUMBLE", 0, 0);
				GlobalMembersResourcesWP.SOUND_RANK_COUNTUP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1698, "SOUND_RANK_COUNTUP", 0, 0);
				GlobalMembersResourcesWP.SOUND_RANKUP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1699, "SOUND_RANKUP", 0, 0);
				GlobalMembersResourcesWP.SOUND_REPLAY_POPUP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1700, "SOUND_REPLAY_POPUP", 0, 0);
				GlobalMembersResourcesWP.SOUND_REWIND = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1701, "SOUND_REWIND", 0, 0);
				GlobalMembersResourcesWP.SOUND_SANDSTORM_TREASURE_REVEAL = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1702, "SOUND_SANDSTORM_TREASURE_REVEAL", 0, 0);
				GlobalMembersResourcesWP.SOUND_SCRAMBLE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1703, "SOUND_SCRAMBLE", 0, 0);
				GlobalMembersResourcesWP.SOUND_SECRETMOUSEOVER1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1704, "SOUND_SECRETMOUSEOVER1", 0, 0);
				GlobalMembersResourcesWP.SOUND_SECRETMOUSEOVER2 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1705, "SOUND_SECRETMOUSEOVER2", 0, 0);
				GlobalMembersResourcesWP.SOUND_SECRETMOUSEOVER3 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1706, "SOUND_SECRETMOUSEOVER3", 0, 0);
				GlobalMembersResourcesWP.SOUND_SECRETMOUSEOVER4 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1707, "SOUND_SECRETMOUSEOVER4", 0, 0);
				GlobalMembersResourcesWP.SOUND_SECRETUNLOCKED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1708, "SOUND_SECRETUNLOCKED", 0, 0);
				GlobalMembersResourcesWP.SOUND_SIN500 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1709, "SOUND_SIN500", 0, 0);
				GlobalMembersResourcesWP.SOUND_SKULL_APPEAR = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1710, "SOUND_SKULL_APPEAR", 0, 0);
				GlobalMembersResourcesWP.SOUND_SKULL_BUSTED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1711, "SOUND_SKULL_BUSTED", 0, 0);
				GlobalMembersResourcesWP.SOUND_SKULL_BUSTER = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1712, "SOUND_SKULL_BUSTER", 0, 0);
				GlobalMembersResourcesWP.SOUND_SKULLCOIN_FLIP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1713, "SOUND_SKULLCOIN_FLIP", 0, 0);
				GlobalMembersResourcesWP.SOUND_SKULLCOINLANDS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1714, "SOUND_SKULLCOINLANDS", 0, 0);
				GlobalMembersResourcesWP.SOUND_SKULLCOINLOSE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1715, "SOUND_SKULLCOINLOSE", 0, 0);
				GlobalMembersResourcesWP.SOUND_SKULLCOINWIN = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1716, "SOUND_SKULLCOINWIN", 0, 0);
				GlobalMembersResourcesWP.SOUND_SLIDE_MOVE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1717, "SOUND_SLIDE_MOVE", 0, 0);
				GlobalMembersResourcesWP.SOUND_SLIDE_MOVE_SHORT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1718, "SOUND_SLIDE_MOVE_SHORT", 0, 0);
				GlobalMembersResourcesWP.SOUND_SLIDE_TOUCH = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1719, "SOUND_SLIDE_TOUCH", 0, 0);
				GlobalMembersResourcesWP.SOUND_SMALL_EXPLODE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1720, "SOUND_SMALL_EXPLODE", 0, 0);
				GlobalMembersResourcesWP.SOUND_SPEEDMATCH1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1721, "SOUND_SPEEDMATCH1", 0, 0);
				GlobalMembersResourcesWP.SOUND_SPEEDMATCH2 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1722, "SOUND_SPEEDMATCH2", 0, 0);
				GlobalMembersResourcesWP.SOUND_SPEEDMATCH3 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1723, "SOUND_SPEEDMATCH3", 0, 0);
				GlobalMembersResourcesWP.SOUND_SPEEDMATCH4 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1724, "SOUND_SPEEDMATCH4", 0, 0);
				GlobalMembersResourcesWP.SOUND_SPEEDMATCH5 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1725, "SOUND_SPEEDMATCH5", 0, 0);
				GlobalMembersResourcesWP.SOUND_SPEEDMATCH6 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1726, "SOUND_SPEEDMATCH6", 0, 0);
				GlobalMembersResourcesWP.SOUND_SPEEDMATCH7 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1727, "SOUND_SPEEDMATCH7", 0, 0);
				GlobalMembersResourcesWP.SOUND_SPEEDMATCH8 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1728, "SOUND_SPEEDMATCH8", 0, 0);
				GlobalMembersResourcesWP.SOUND_SPEEDMATCH9 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1729, "SOUND_SPEEDMATCH9", 0, 0);
				GlobalMembersResourcesWP.SOUND_TICK = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1730, "SOUND_TICK", 0, 0);
				GlobalMembersResourcesWP.SOUND_TIMEBOMBEXPLODE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1731, "SOUND_TIMEBOMBEXPLODE", 0, 0);
				GlobalMembersResourcesWP.SOUND_TIMEBONUS_5 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1732, "SOUND_TIMEBONUS_5", 0, 0);
				GlobalMembersResourcesWP.SOUND_TIMEBONUS_10 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1733, "SOUND_TIMEBONUS_10", 0, 0);
				GlobalMembersResourcesWP.SOUND_TIMEBONUS_APPEARS_5 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1734, "SOUND_TIMEBONUS_APPEARS_5", 0, 0);
				GlobalMembersResourcesWP.SOUND_TIMEBONUS_APPEARS_10 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1735, "SOUND_TIMEBONUS_APPEARS_10", 0, 0);
				GlobalMembersResourcesWP.SOUND_TOOLTIP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1736, "SOUND_TOOLTIP", 0, 0);
				GlobalMembersResourcesWP.SOUND_TOWER_HITS_TOP1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1737, "SOUND_TOWER_HITS_TOP1", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_CHECKOFF = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1756, "SOUND_ZEN_CHECKOFF", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_CHECKON = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1757, "SOUND_ZEN_CHECKON", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_COMBO_2 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1758, "SOUND_ZEN_COMBO_2", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_DROPDOWNBUTTON = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1759, "SOUND_ZEN_DROPDOWNBUTTON", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_MANTRA1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1760, "SOUND_ZEN_MANTRA1", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_MENUCLOSE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1761, "SOUND_ZEN_MENUCLOSE", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_MENUEXPAND = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1762, "SOUND_ZEN_MENUEXPAND", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_MENUOPEN = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1763, "SOUND_ZEN_MENUOPEN", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_MENUSHRINK = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1764, "SOUND_ZEN_MENUSHRINK", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_NECKLACE_1 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1765, "SOUND_ZEN_NECKLACE_1", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_NECKLACE_2 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1766, "SOUND_ZEN_NECKLACE_2", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_NECKLACE_3 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1767, "SOUND_ZEN_NECKLACE_3", 0, 0);
				GlobalMembersResourcesWP.SOUND_ZEN_NECKLACE_4 = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1768, "SOUND_ZEN_NECKLACE_4", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractCommon_DEDEResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_BADGES = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 8, "RESFILE_PROPERTIES_BADGES", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULT = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 9, "RESFILE_PROPERTIES_DEFAULT", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTFILENAMES = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 10, "RESFILE_PROPERTIES_DEFAULTFILENAMES", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTFRAMEWORK = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 11, "RESFILE_PROPERTIES_DEFAULTFRAMEWORK", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTQUEST = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 12, "RESFILE_PROPERTIES_DEFAULTQUEST", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTUICONSTANTS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 13, "RESFILE_PROPERTIES_DEFAULTUICONSTANTS", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_QUEST = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 15, "RESFILE_PROPERTIES_QUEST", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_RANKS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 16, "RESFILE_PROPERTIES_RANKS", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_SECRET = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 17, "RESFILE_PROPERTIES_SECRET", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_SPEED = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 18, "RESFILE_PROPERTIES_SPEED", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_TIPS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 19, "RESFILE_PROPERTIES_TIPS", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_AWESOME = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1738, "SOUND_VOICE_AWESOME", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_BLAZINGSPEED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1739, "SOUND_VOICE_BLAZINGSPEED", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_CHALLENGECOMPLETE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1740, "SOUND_VOICE_CHALLENGECOMPLETE", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_EXCELLENT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1741, "SOUND_VOICE_EXCELLENT", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_EXTRAORDINARY = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1742, "SOUND_VOICE_EXTRAORDINARY", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_GAMEOVER = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1743, "SOUND_VOICE_GAMEOVER", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_GETREADY = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1744, "SOUND_VOICE_GETREADY", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_GO = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1745, "SOUND_VOICE_GO", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_GOOD = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1746, "SOUND_VOICE_GOOD", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_GOODBYE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1747, "SOUND_VOICE_GOODBYE", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_LEVELCOMPLETE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1748, "SOUND_VOICE_LEVELCOMPLETE", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_NOMOREMOVES = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1749, "SOUND_VOICE_NOMOREMOVES", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_SPECTACULAR = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1750, "SOUND_VOICE_SPECTACULAR", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_THIRTYSECONDS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1751, "SOUND_VOICE_THIRTYSECONDS", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_TIMEUP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1752, "SOUND_VOICE_TIMEUP", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_UNBELIEVABLE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1753, "SOUND_VOICE_UNBELIEVABLE", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_WELCOMEBACK = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1754, "SOUND_VOICE_WELCOMEBACK", 0, 1145390149);
				GlobalMembersResourcesWP.SOUND_VOICE_WELCOMETOBEJEWELED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1755, "SOUND_VOICE_WELCOMETOBEJEWELED", 0, 1145390149);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractCommon_ENUSResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_BADGES = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 8, "RESFILE_PROPERTIES_BADGES", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULT = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 9, "RESFILE_PROPERTIES_DEFAULT", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTFILENAMES = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 10, "RESFILE_PROPERTIES_DEFAULTFILENAMES", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTFRAMEWORK = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 11, "RESFILE_PROPERTIES_DEFAULTFRAMEWORK", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTQUEST = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 12, "RESFILE_PROPERTIES_DEFAULTQUEST", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTUICONSTANTS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 13, "RESFILE_PROPERTIES_DEFAULTUICONSTANTS", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_QUEST = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 15, "RESFILE_PROPERTIES_QUEST", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_RANKS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 16, "RESFILE_PROPERTIES_RANKS", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_SECRET = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 17, "RESFILE_PROPERTIES_SECRET", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_SPEED = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 18, "RESFILE_PROPERTIES_SPEED", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_TIPS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 19, "RESFILE_PROPERTIES_TIPS", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_AWESOME = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1738, "SOUND_VOICE_AWESOME", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_BLAZINGSPEED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1739, "SOUND_VOICE_BLAZINGSPEED", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_CHALLENGECOMPLETE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1740, "SOUND_VOICE_CHALLENGECOMPLETE", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_EXCELLENT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1741, "SOUND_VOICE_EXCELLENT", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_EXTRAORDINARY = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1742, "SOUND_VOICE_EXTRAORDINARY", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_GAMEOVER = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1743, "SOUND_VOICE_GAMEOVER", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_GETREADY = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1744, "SOUND_VOICE_GETREADY", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_GO = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1745, "SOUND_VOICE_GO", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_GOOD = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1746, "SOUND_VOICE_GOOD", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_GOODBYE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1747, "SOUND_VOICE_GOODBYE", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_LEVELCOMPLETE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1748, "SOUND_VOICE_LEVELCOMPLETE", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_NOMOREMOVES = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1749, "SOUND_VOICE_NOMOREMOVES", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_SPECTACULAR = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1750, "SOUND_VOICE_SPECTACULAR", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_THIRTYSECONDS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1751, "SOUND_VOICE_THIRTYSECONDS", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_TIMEUP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1752, "SOUND_VOICE_TIMEUP", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_UNBELIEVABLE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1753, "SOUND_VOICE_UNBELIEVABLE", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_WELCOMEBACK = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1754, "SOUND_VOICE_WELCOMEBACK", 0, 1162761555);
				GlobalMembersResourcesWP.SOUND_VOICE_WELCOMETOBEJEWELED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1755, "SOUND_VOICE_WELCOMETOBEJEWELED", 0, 1162761555);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractCommon_ESESResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_BADGES = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 8, "RESFILE_PROPERTIES_BADGES", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULT = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 9, "RESFILE_PROPERTIES_DEFAULT", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTFILENAMES = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 10, "RESFILE_PROPERTIES_DEFAULTFILENAMES", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTFRAMEWORK = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 11, "RESFILE_PROPERTIES_DEFAULTFRAMEWORK", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTQUEST = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 12, "RESFILE_PROPERTIES_DEFAULTQUEST", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTUICONSTANTS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 13, "RESFILE_PROPERTIES_DEFAULTUICONSTANTS", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_QUEST = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 15, "RESFILE_PROPERTIES_QUEST", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_RANKS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 16, "RESFILE_PROPERTIES_RANKS", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_SECRET = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 17, "RESFILE_PROPERTIES_SECRET", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_SPEED = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 18, "RESFILE_PROPERTIES_SPEED", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_TIPS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 19, "RESFILE_PROPERTIES_TIPS", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_AWESOME = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1738, "SOUND_VOICE_AWESOME", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_BLAZINGSPEED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1739, "SOUND_VOICE_BLAZINGSPEED", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_CHALLENGECOMPLETE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1740, "SOUND_VOICE_CHALLENGECOMPLETE", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_EXCELLENT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1741, "SOUND_VOICE_EXCELLENT", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_EXTRAORDINARY = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1742, "SOUND_VOICE_EXTRAORDINARY", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_GAMEOVER = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1743, "SOUND_VOICE_GAMEOVER", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_GETREADY = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1744, "SOUND_VOICE_GETREADY", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_GO = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1745, "SOUND_VOICE_GO", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_GOOD = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1746, "SOUND_VOICE_GOOD", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_GOODBYE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1747, "SOUND_VOICE_GOODBYE", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_LEVELCOMPLETE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1748, "SOUND_VOICE_LEVELCOMPLETE", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_NOMOREMOVES = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1749, "SOUND_VOICE_NOMOREMOVES", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_SPECTACULAR = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1750, "SOUND_VOICE_SPECTACULAR", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_THIRTYSECONDS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1751, "SOUND_VOICE_THIRTYSECONDS", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_TIMEUP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1752, "SOUND_VOICE_TIMEUP", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_UNBELIEVABLE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1753, "SOUND_VOICE_UNBELIEVABLE", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_WELCOMEBACK = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1754, "SOUND_VOICE_WELCOMEBACK", 0, 1163085139);
				GlobalMembersResourcesWP.SOUND_VOICE_WELCOMETOBEJEWELED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1755, "SOUND_VOICE_WELCOMETOBEJEWELED", 0, 1163085139);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractCommon_FRFRResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_BADGES = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 8, "RESFILE_PROPERTIES_BADGES", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULT = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 9, "RESFILE_PROPERTIES_DEFAULT", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTFILENAMES = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 10, "RESFILE_PROPERTIES_DEFAULTFILENAMES", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTFRAMEWORK = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 11, "RESFILE_PROPERTIES_DEFAULTFRAMEWORK", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTQUEST = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 12, "RESFILE_PROPERTIES_DEFAULTQUEST", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTUICONSTANTS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 13, "RESFILE_PROPERTIES_DEFAULTUICONSTANTS", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_QUEST = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 15, "RESFILE_PROPERTIES_QUEST", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_RANKS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 16, "RESFILE_PROPERTIES_RANKS", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_SECRET = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 17, "RESFILE_PROPERTIES_SECRET", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_SPEED = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 18, "RESFILE_PROPERTIES_SPEED", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_TIPS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 19, "RESFILE_PROPERTIES_TIPS", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_AWESOME = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1738, "SOUND_VOICE_AWESOME", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_BLAZINGSPEED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1739, "SOUND_VOICE_BLAZINGSPEED", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_CHALLENGECOMPLETE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1740, "SOUND_VOICE_CHALLENGECOMPLETE", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_EXCELLENT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1741, "SOUND_VOICE_EXCELLENT", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_EXTRAORDINARY = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1742, "SOUND_VOICE_EXTRAORDINARY", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_GAMEOVER = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1743, "SOUND_VOICE_GAMEOVER", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_GETREADY = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1744, "SOUND_VOICE_GETREADY", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_GO = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1745, "SOUND_VOICE_GO", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_GOOD = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1746, "SOUND_VOICE_GOOD", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_GOODBYE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1747, "SOUND_VOICE_GOODBYE", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_LEVELCOMPLETE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1748, "SOUND_VOICE_LEVELCOMPLETE", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_NOMOREMOVES = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1749, "SOUND_VOICE_NOMOREMOVES", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_SPECTACULAR = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1750, "SOUND_VOICE_SPECTACULAR", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_THIRTYSECONDS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1751, "SOUND_VOICE_THIRTYSECONDS", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_TIMEUP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1752, "SOUND_VOICE_TIMEUP", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_UNBELIEVABLE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1753, "SOUND_VOICE_UNBELIEVABLE", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_WELCOMEBACK = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1754, "SOUND_VOICE_WELCOMEBACK", 0, 1179797074);
				GlobalMembersResourcesWP.SOUND_VOICE_WELCOMETOBEJEWELED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1755, "SOUND_VOICE_WELCOMETOBEJEWELED", 0, 1179797074);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractCommon_ITITResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_BADGES = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 8, "RESFILE_PROPERTIES_BADGES", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULT = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 9, "RESFILE_PROPERTIES_DEFAULT", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTFILENAMES = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 10, "RESFILE_PROPERTIES_DEFAULTFILENAMES", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTFRAMEWORK = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 11, "RESFILE_PROPERTIES_DEFAULTFRAMEWORK", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTQUEST = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 12, "RESFILE_PROPERTIES_DEFAULTQUEST", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_DEFAULTUICONSTANTS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 13, "RESFILE_PROPERTIES_DEFAULTUICONSTANTS", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_QUEST = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 15, "RESFILE_PROPERTIES_QUEST", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_RANKS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 16, "RESFILE_PROPERTIES_RANKS", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_SECRET = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 17, "RESFILE_PROPERTIES_SECRET", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_SPEED = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 18, "RESFILE_PROPERTIES_SPEED", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_PROPERTIES_TIPS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 19, "RESFILE_PROPERTIES_TIPS", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_AWESOME = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1738, "SOUND_VOICE_AWESOME", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_BLAZINGSPEED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1739, "SOUND_VOICE_BLAZINGSPEED", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_CHALLENGECOMPLETE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1740, "SOUND_VOICE_CHALLENGECOMPLETE", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_EXCELLENT = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1741, "SOUND_VOICE_EXCELLENT", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_EXTRAORDINARY = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1742, "SOUND_VOICE_EXTRAORDINARY", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_GAMEOVER = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1743, "SOUND_VOICE_GAMEOVER", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_GETREADY = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1744, "SOUND_VOICE_GETREADY", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_GO = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1745, "SOUND_VOICE_GO", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_GOOD = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1746, "SOUND_VOICE_GOOD", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_GOODBYE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1747, "SOUND_VOICE_GOODBYE", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_LEVELCOMPLETE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1748, "SOUND_VOICE_LEVELCOMPLETE", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_NOMOREMOVES = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1749, "SOUND_VOICE_NOMOREMOVES", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_SPECTACULAR = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1750, "SOUND_VOICE_SPECTACULAR", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_THIRTYSECONDS = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1751, "SOUND_VOICE_THIRTYSECONDS", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_TIMEUP = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1752, "SOUND_VOICE_TIMEUP", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_UNBELIEVABLE = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1753, "SOUND_VOICE_UNBELIEVABLE", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_WELCOMEBACK = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1754, "SOUND_VOICE_WELCOMEBACK", 0, 1230260564);
				GlobalMembersResourcesWP.SOUND_VOICE_WELCOMETOBEJEWELED = GlobalMembersResourcesWP.GetSoundThrow(theManager, 1755, "SOUND_VOICE_WELCOMETOBEJEWELED", 0, 1230260564);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_BridgeShroomCastleResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_BridgeShroomCastle_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_BridgeShroomCastle_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_BridgeShroomCastle_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_BRIDGE_SHROOM_CASTLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 172, "IMAGE_BACKGROUNDS_BRIDGE_SHROOM_CASTLE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_BridgeShroomCastle_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_BRIDGE_SHROOM_CASTLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 172, "IMAGE_BACKGROUNDS_BRIDGE_SHROOM_CASTLE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_CaveResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_Cave_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_Cave_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_Cave_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_CAVE = GlobalMembersResourcesWP.GetImageThrow(theManager, 180, "IMAGE_BACKGROUNDS_CAVE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_Cave_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_CAVE = GlobalMembersResourcesWP.GetImageThrow(theManager, 180, "IMAGE_BACKGROUNDS_CAVE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_CrystalTowersResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_CrystalTowers_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_CrystalTowers_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_CrystalTowers_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_CRYSTALTOWERS = GlobalMembersResourcesWP.GetImageThrow(theManager, 173, "IMAGE_BACKGROUNDS_CRYSTALTOWERS", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_CrystalTowers_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_CRYSTALTOWERS = GlobalMembersResourcesWP.GetImageThrow(theManager, 173, "IMAGE_BACKGROUNDS_CRYSTALTOWERS", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_DaveCaveThingResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_DaveCaveThing_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_DaveCaveThing_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_DaveCaveThing_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_DAVE_CAVE_THING = GlobalMembersResourcesWP.GetImageThrow(theManager, 164, "IMAGE_BACKGROUNDS_DAVE_CAVE_THING", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_DaveCaveThing_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_DAVE_CAVE_THING = GlobalMembersResourcesWP.GetImageThrow(theManager, 164, "IMAGE_BACKGROUNDS_DAVE_CAVE_THING", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_DesertResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_Desert_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_Desert_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_Desert_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_DESERT = GlobalMembersResourcesWP.GetImageThrow(theManager, 165, "IMAGE_BACKGROUNDS_DESERT", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_Desert_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_DESERT = GlobalMembersResourcesWP.GetImageThrow(theManager, 165, "IMAGE_BACKGROUNDS_DESERT", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_FloatingRockCityResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_FloatingRockCity_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_FloatingRockCity_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_FloatingRockCity_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_FLOATING_ROCK_CITY = GlobalMembersResourcesWP.GetImageThrow(theManager, 166, "IMAGE_BACKGROUNDS_FLOATING_ROCK_CITY", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_FloatingRockCity_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_FLOATING_ROCK_CITY = GlobalMembersResourcesWP.GetImageThrow(theManager, 166, "IMAGE_BACKGROUNDS_FLOATING_ROCK_CITY", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_FlyingSailBoatResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_FlyingSailBoat_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_FlyingSailBoat_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_FlyingSailBoat_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_FLYING_SAIL_BOAT = GlobalMembersResourcesWP.GetImageThrow(theManager, 174, "IMAGE_BACKGROUNDS_FLYING_SAIL_BOAT", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_FlyingSailBoat_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_FLYING_SAIL_BOAT = GlobalMembersResourcesWP.GetImageThrow(theManager, 174, "IMAGE_BACKGROUNDS_FLYING_SAIL_BOAT", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_LanternPlantsWorldResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_LanternPlantsWorld_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_LanternPlantsWorld_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_LanternPlantsWorld_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_LANTERN_PLANTS_WORLD = GlobalMembersResourcesWP.GetImageThrow(theManager, 175, "IMAGE_BACKGROUNDS_LANTERN_PLANTS_WORLD", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_LanternPlantsWorld_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_LANTERN_PLANTS_WORLD = GlobalMembersResourcesWP.GetImageThrow(theManager, 175, "IMAGE_BACKGROUNDS_LANTERN_PLANTS_WORLD", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_LionTowerCascadeResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_LionTowerCascade_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_LionTowerCascade_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_LionTowerCascade_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_LION_TOWER_CASCADE = GlobalMembersResourcesWP.GetImageThrow(theManager, 176, "IMAGE_BACKGROUNDS_LION_TOWER_CASCADE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_LionTowerCascade_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_LION_TOWER_CASCADE = GlobalMembersResourcesWP.GetImageThrow(theManager, 176, "IMAGE_BACKGROUNDS_LION_TOWER_CASCADE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_LionTowerCascadeBflyResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_LionTowerCascadeBfly_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_LionTowerCascadeBfly_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_LionTowerCascadeBfly_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_LION_TOWER_CASCADE_BFLY = GlobalMembersResourcesWP.GetImageThrow(theManager, 177, "IMAGE_BACKGROUNDS_LION_TOWER_CASCADE_BFLY", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_LionTowerCascadeBfly_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_LION_TOWER_CASCADE_BFLY = GlobalMembersResourcesWP.GetImageThrow(theManager, 177, "IMAGE_BACKGROUNDS_LION_TOWER_CASCADE_BFLY", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_PointyIcePathResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_PointyIcePath_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_PointyIcePath_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_PointyIcePath_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_POINTY_ICE_PATH = GlobalMembersResourcesWP.GetImageThrow(theManager, 179, "IMAGE_BACKGROUNDS_POINTY_ICE_PATH", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_PointyIcePath_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_POINTY_ICE_PATH = GlobalMembersResourcesWP.GetImageThrow(theManager, 179, "IMAGE_BACKGROUNDS_POINTY_ICE_PATH", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_PokerResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_Poker_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_Poker_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_Poker_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_POKER = GlobalMembersResourcesWP.GetImageThrow(theManager, 178, "IMAGE_BACKGROUNDS_POKER", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_Poker_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_POKER = GlobalMembersResourcesWP.GetImageThrow(theManager, 178, "IMAGE_BACKGROUNDS_POKER", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_SnowyCliffsCastleResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_SnowyCliffsCastle_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_SnowyCliffsCastle_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_SnowyCliffsCastle_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_SNOWY_CLIFFS_CASTLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 167, "IMAGE_BACKGROUNDS_SNOWY_CLIFFS_CASTLE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_SnowyCliffsCastle_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_SNOWY_CLIFFS_CASTLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 167, "IMAGE_BACKGROUNDS_SNOWY_CLIFFS_CASTLE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_TubeForestNightResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_TubeForestNight_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_TubeForestNight_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_TubeForestNight_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_TUBE_FOREST_NIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 168, "IMAGE_BACKGROUNDS_TUBE_FOREST_NIGHT", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_TubeForestNight_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_TUBE_FOREST_NIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 168, "IMAGE_BACKGROUNDS_TUBE_FOREST_NIGHT", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_WaterBubblesCityResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_WaterBubblesCity_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_WaterBubblesCity_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_WaterBubblesCity_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_WATER_BUBBLES_CITY = GlobalMembersResourcesWP.GetImageThrow(theManager, 169, "IMAGE_BACKGROUNDS_WATER_BUBBLES_CITY", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_WaterBubblesCity_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_WATER_BUBBLES_CITY = GlobalMembersResourcesWP.GetImageThrow(theManager, 169, "IMAGE_BACKGROUNDS_WATER_BUBBLES_CITY", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_WaterFallCliffResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_WaterFallCliff_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_WaterFallCliff_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_WaterFallCliff_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_WATER_FALL_CLIFF = GlobalMembersResourcesWP.GetImageThrow(theManager, 170, "IMAGE_BACKGROUNDS_WATER_FALL_CLIFF", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_WaterFallCliff_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_WATER_FALL_CLIFF = GlobalMembersResourcesWP.GetImageThrow(theManager, 170, "IMAGE_BACKGROUNDS_WATER_FALL_CLIFF", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_WaterPathRuinsResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractFlatBG_WaterPathRuins_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractFlatBG_WaterPathRuins_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_WaterPathRuins_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_WATER_PATH_RUINS = GlobalMembersResourcesWP.GetImageThrow(theManager, 171, "IMAGE_BACKGROUNDS_WATER_PATH_RUINS", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractFlatBG_WaterPathRuins_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BACKGROUNDS_WATER_PATH_RUINS = GlobalMembersResourcesWP.GetImageThrow(theManager, 171, "IMAGE_BACKGROUNDS_WATER_PATH_RUINS", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlay_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlay_960Resources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractGamePlay_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlay_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAY_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 36, "ATLASIMAGE_ATLAS_GAMEPLAY_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 802, "IMAGE_GEMS_RED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_WHITE = GlobalMembersResourcesWP.GetImageThrow(theManager, 803, "IMAGE_GEMS_WHITE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_GREEN = GlobalMembersResourcesWP.GetImageThrow(theManager, 804, "IMAGE_GEMS_GREEN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_YELLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 805, "IMAGE_GEMS_YELLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_PURPLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 806, "IMAGE_GEMS_PURPLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_ORANGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 807, "IMAGE_GEMS_ORANGE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_BLUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 808, "IMAGE_GEMS_BLUE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 809, "IMAGE_GEMSSHADOW_RED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_WHITE = GlobalMembersResourcesWP.GetImageThrow(theManager, 810, "IMAGE_GEMSSHADOW_WHITE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_GREEN = GlobalMembersResourcesWP.GetImageThrow(theManager, 811, "IMAGE_GEMSSHADOW_GREEN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_YELLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 812, "IMAGE_GEMSSHADOW_YELLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_PURPLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 813, "IMAGE_GEMSSHADOW_PURPLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_ORANGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 814, "IMAGE_GEMSSHADOW_ORANGE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_BLUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 815, "IMAGE_GEMSSHADOW_BLUE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 816, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME1", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 817, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 818, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 819, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 820, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 821, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 822, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 823, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 824, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 825, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 826, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME11", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_FLAMEGEM_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 827, "IMAGE_FLAMEGEMCREATION_FLAMEGEM_BLUR", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_FLAMEGEM_FLASH_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 828, "IMAGE_FLAMEGEMCREATION_FLAMEGEM_FLASH_1", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_FLAMEGEM_FLASH_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 829, "IMAGE_FLAMEGEMCREATION_FLAMEGEM_FLASH_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_FLAMEGEM_LARGE_RING = GlobalMembersResourcesWP.GetImageThrow(theManager, 830, "IMAGE_FLAMEGEMCREATION_FLAMEGEM_LARGE_RING", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_FLAMEGEM_RING_OF_FLAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 831, "IMAGE_FLAMEGEMCREATION_FLAMEGEM_RING_OF_FLAME", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMEXPLODE_FLAMEEXPLODETEST_LAYER_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 832, "IMAGE_FLAMEGEMEXPLODE_FLAMEEXPLODETEST_LAYER_1", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_BOOM_NOVA = GlobalMembersResourcesWP.GetImageThrow(theManager, 833, "IMAGE_BOOM_NOVA", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BOOM_NUKE = GlobalMembersResourcesWP.GetImageThrow(theManager, 834, "IMAGE_BOOM_NUKE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BOARD_IRIS = GlobalMembersResourcesWP.GetImageThrow(theManager, 835, "IMAGE_BOARD_IRIS", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_SHADOWED = GlobalMembersResourcesWP.GetImageThrow(theManager, 836, "IMAGE_GEMS_SHADOWED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEM_FRUIT_SPARK = GlobalMembersResourcesWP.GetImageThrow(theManager, 837, "IMAGE_GEM_FRUIT_SPARK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SMOKE = GlobalMembersResourcesWP.GetImageThrow(theManager, 838, "IMAGE_SMOKE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DRIP = GlobalMembersResourcesWP.GetImageThrow(theManager, 839, "IMAGE_DRIP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_FX_STEAM = GlobalMembersResourcesWP.GetImageThrow(theManager, 840, "IMAGE_FX_STEAM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SPARKLET = GlobalMembersResourcesWP.GetImageThrow(theManager, 841, "IMAGE_SPARKLET", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DIAMOND_MINE_TEXT_CYCLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 842, "IMAGE_DIAMOND_MINE_TEXT_CYCLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ELECTROTEX = GlobalMembersResourcesWP.GetImageThrow(theManager, 843, "IMAGE_ELECTROTEX", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ELECTROTEX_CENTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 844, "IMAGE_ELECTROTEX_CENTER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERFLARELINE = GlobalMembersResourcesWP.GetImageThrow(theManager, 845, "IMAGE_HYPERFLARELINE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERFLARERING = GlobalMembersResourcesWP.GetImageThrow(theManager, 846, "IMAGE_HYPERFLARERING", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SELECTOR = GlobalMembersResourcesWP.GetImageThrow(theManager, 847, "IMAGE_SELECTOR", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HINTARROW = GlobalMembersResourcesWP.GetImageThrow(theManager, 848, "IMAGE_HINTARROW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DANGERBORDERLEFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 849, "IMAGE_DANGERBORDERLEFT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DANGERBORDERUP = GlobalMembersResourcesWP.GetImageThrow(theManager, 850, "IMAGE_DANGERBORDERUP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERCUBE_COLORGLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 851, "IMAGE_HYPERCUBE_COLORGLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERCUBE_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 852, "IMAGE_HYPERCUBE_FRAME", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SHADER_TEST = GlobalMembersResourcesWP.GetImageThrow(theManager, 853, "IMAGE_SHADER_TEST", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING = GlobalMembersResourcesWP.GetImageThrow(theManager, 854, "IMAGE_LIGHTNING", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GRITTYBLURRY = GlobalMembersResourcesWP.GetImageThrow(theManager, 855, "IMAGE_GRITTYBLURRY", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_CENTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 856, "IMAGE_LIGHTNING_CENTER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_TEX = GlobalMembersResourcesWP.GetImageThrow(theManager, 857, "IMAGE_LIGHTNING_TEX", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SPARKLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 858, "IMAGE_SPARKLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SM_SHARDS = GlobalMembersResourcesWP.GetImageThrow(theManager, 859, "IMAGE_SM_SHARDS", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SM_SHARDS_OUTLINE = GlobalMembersResourcesWP.GetImageThrow(theManager, 860, "IMAGE_SM_SHARDS_OUTLINE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_FIREPARTICLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 861, "IMAGE_FIREPARTICLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 862, "IMAGE_GEMSNORMAL_RED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_WHITE = GlobalMembersResourcesWP.GetImageThrow(theManager, 863, "IMAGE_GEMSNORMAL_WHITE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_GREEN = GlobalMembersResourcesWP.GetImageThrow(theManager, 864, "IMAGE_GEMSNORMAL_GREEN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_YELLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 865, "IMAGE_GEMSNORMAL_YELLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_PURPLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 866, "IMAGE_GEMSNORMAL_PURPLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_ORANGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 867, "IMAGE_GEMSNORMAL_ORANGE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_BLUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 868, "IMAGE_GEMSNORMAL_BLUE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlay_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAY_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 37, "ATLASIMAGE_ATLAS_GAMEPLAY_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 802, "IMAGE_GEMS_RED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_WHITE = GlobalMembersResourcesWP.GetImageThrow(theManager, 803, "IMAGE_GEMS_WHITE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_GREEN = GlobalMembersResourcesWP.GetImageThrow(theManager, 804, "IMAGE_GEMS_GREEN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_YELLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 805, "IMAGE_GEMS_YELLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_PURPLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 806, "IMAGE_GEMS_PURPLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_ORANGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 807, "IMAGE_GEMS_ORANGE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_BLUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 808, "IMAGE_GEMS_BLUE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 809, "IMAGE_GEMSSHADOW_RED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_WHITE = GlobalMembersResourcesWP.GetImageThrow(theManager, 810, "IMAGE_GEMSSHADOW_WHITE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_GREEN = GlobalMembersResourcesWP.GetImageThrow(theManager, 811, "IMAGE_GEMSSHADOW_GREEN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_YELLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 812, "IMAGE_GEMSSHADOW_YELLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_PURPLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 813, "IMAGE_GEMSSHADOW_PURPLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_ORANGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 814, "IMAGE_GEMSSHADOW_ORANGE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSSHADOW_BLUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 815, "IMAGE_GEMSSHADOW_BLUE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 816, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME1", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 817, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 818, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 819, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 820, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 821, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 822, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 823, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 824, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 825, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 826, "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME11", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_FLAMEGEM_BLUR = GlobalMembersResourcesWP.GetImageThrow(theManager, 827, "IMAGE_FLAMEGEMCREATION_FLAMEGEM_BLUR", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_FLAMEGEM_FLASH_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 828, "IMAGE_FLAMEGEMCREATION_FLAMEGEM_FLASH_1", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_FLAMEGEM_FLASH_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 829, "IMAGE_FLAMEGEMCREATION_FLAMEGEM_FLASH_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_FLAMEGEM_LARGE_RING = GlobalMembersResourcesWP.GetImageThrow(theManager, 830, "IMAGE_FLAMEGEMCREATION_FLAMEGEM_LARGE_RING", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMCREATION_FLAMEGEM_RING_OF_FLAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 831, "IMAGE_FLAMEGEMCREATION_FLAMEGEM_RING_OF_FLAME", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_FLAMEGEMEXPLODE_FLAMEEXPLODETEST_LAYER_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 832, "IMAGE_FLAMEGEMEXPLODE_FLAMEEXPLODETEST_LAYER_1", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_BOOM_NOVA = GlobalMembersResourcesWP.GetImageThrow(theManager, 833, "IMAGE_BOOM_NOVA", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BOOM_NUKE = GlobalMembersResourcesWP.GetImageThrow(theManager, 834, "IMAGE_BOOM_NUKE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BOARD_IRIS = GlobalMembersResourcesWP.GetImageThrow(theManager, 835, "IMAGE_BOARD_IRIS", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMS_SHADOWED = GlobalMembersResourcesWP.GetImageThrow(theManager, 836, "IMAGE_GEMS_SHADOWED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEM_FRUIT_SPARK = GlobalMembersResourcesWP.GetImageThrow(theManager, 837, "IMAGE_GEM_FRUIT_SPARK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SMOKE = GlobalMembersResourcesWP.GetImageThrow(theManager, 838, "IMAGE_SMOKE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DRIP = GlobalMembersResourcesWP.GetImageThrow(theManager, 839, "IMAGE_DRIP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_FX_STEAM = GlobalMembersResourcesWP.GetImageThrow(theManager, 840, "IMAGE_FX_STEAM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SPARKLET = GlobalMembersResourcesWP.GetImageThrow(theManager, 841, "IMAGE_SPARKLET", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DIAMOND_MINE_TEXT_CYCLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 842, "IMAGE_DIAMOND_MINE_TEXT_CYCLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ELECTROTEX = GlobalMembersResourcesWP.GetImageThrow(theManager, 843, "IMAGE_ELECTROTEX", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ELECTROTEX_CENTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 844, "IMAGE_ELECTROTEX_CENTER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERFLARELINE = GlobalMembersResourcesWP.GetImageThrow(theManager, 845, "IMAGE_HYPERFLARELINE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERFLARERING = GlobalMembersResourcesWP.GetImageThrow(theManager, 846, "IMAGE_HYPERFLARERING", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SELECTOR = GlobalMembersResourcesWP.GetImageThrow(theManager, 847, "IMAGE_SELECTOR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HINTARROW = GlobalMembersResourcesWP.GetImageThrow(theManager, 848, "IMAGE_HINTARROW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DANGERBORDERLEFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 849, "IMAGE_DANGERBORDERLEFT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DANGERBORDERUP = GlobalMembersResourcesWP.GetImageThrow(theManager, 850, "IMAGE_DANGERBORDERUP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERCUBE_COLORGLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 851, "IMAGE_HYPERCUBE_COLORGLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERCUBE_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 852, "IMAGE_HYPERCUBE_FRAME", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SHADER_TEST = GlobalMembersResourcesWP.GetImageThrow(theManager, 853, "IMAGE_SHADER_TEST", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING = GlobalMembersResourcesWP.GetImageThrow(theManager, 854, "IMAGE_LIGHTNING", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GRITTYBLURRY = GlobalMembersResourcesWP.GetImageThrow(theManager, 855, "IMAGE_GRITTYBLURRY", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_CENTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 856, "IMAGE_LIGHTNING_CENTER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_TEX = GlobalMembersResourcesWP.GetImageThrow(theManager, 857, "IMAGE_LIGHTNING_TEX", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SPARKLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 858, "IMAGE_SPARKLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SM_SHARDS = GlobalMembersResourcesWP.GetImageThrow(theManager, 859, "IMAGE_SM_SHARDS", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SM_SHARDS_OUTLINE = GlobalMembersResourcesWP.GetImageThrow(theManager, 860, "IMAGE_SM_SHARDS_OUTLINE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_FIREPARTICLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 861, "IMAGE_FIREPARTICLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 862, "IMAGE_GEMSNORMAL_RED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_WHITE = GlobalMembersResourcesWP.GetImageThrow(theManager, 863, "IMAGE_GEMSNORMAL_WHITE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_GREEN = GlobalMembersResourcesWP.GetImageThrow(theManager, 864, "IMAGE_GEMSNORMAL_GREEN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_YELLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 865, "IMAGE_GEMSNORMAL_YELLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_PURPLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 866, "IMAGE_GEMSNORMAL_PURPLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_ORANGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 867, "IMAGE_GEMSNORMAL_ORANGE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMSNORMAL_BLUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 868, "IMAGE_GEMSNORMAL_BLUE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlay_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.POPANIM_FLAMEGEMCREATION = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1550, "POPANIM_FLAMEGEMCREATION", 0, 0);
				GlobalMembersResourcesWP.POPANIM_FLAMEGEMEXPLODE = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1551, "POPANIM_FLAMEGEMEXPLODE", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlay_UI_DigResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlay_UI_Dig_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlay_UI_Dig_960Resources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractGamePlay_UI_Dig_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlay_UI_Dig_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAY_UI_DIG_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 38, "ATLASIMAGE_ATLAS_GAMEPLAY_UI_DIG_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND = GlobalMembersResourcesWP.GetImageThrow(theManager, 869, "IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 870, "IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_LEVEL = GlobalMembersResourcesWP.GetImageThrow(theManager, 871, "IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_LEVEL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_METER = GlobalMembersResourcesWP.GetImageThrow(theManager, 872, "IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_METER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_HUD_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 873, "IMAGE_INGAMEUI_DIAMOND_MINE_HUD_SHADOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_BACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 874, "IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_BACK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 875, "IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_SCORE_BAR_BACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 876, "IMAGE_INGAMEUI_DIAMOND_MINE_SCORE_BAR_BACK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_TIMER = GlobalMembersResourcesWP.GetImageThrow(theManager, 877, "IMAGE_INGAMEUI_DIAMOND_MINE_TIMER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_113X114 = GlobalMembersResourcesWP.GetImageThrow(theManager, 878, "IMAGE_QUEST_DIG_COGS_COGS_113X114", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_165X165 = GlobalMembersResourcesWP.GetImageThrow(theManager, 879, "IMAGE_QUEST_DIG_COGS_COGS_165X165", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_166X166 = GlobalMembersResourcesWP.GetImageThrow(theManager, 880, "IMAGE_QUEST_DIG_COGS_COGS_166X166", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_202X202 = GlobalMembersResourcesWP.GetImageThrow(theManager, 881, "IMAGE_QUEST_DIG_COGS_COGS_202X202", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_63X64 = GlobalMembersResourcesWP.GetImageThrow(theManager, 882, "IMAGE_QUEST_DIG_COGS_COGS_63X64", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_63X64_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 883, "IMAGE_QUEST_DIG_COGS_COGS_63X64_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_89X90 = GlobalMembersResourcesWP.GetImageThrow(theManager, 884, "IMAGE_QUEST_DIG_COGS_COGS_89X90", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_90X90 = GlobalMembersResourcesWP.GetImageThrow(theManager, 885, "IMAGE_QUEST_DIG_COGS_COGS_90X90", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_96X96 = GlobalMembersResourcesWP.GetImageThrow(theManager, 886, "IMAGE_QUEST_DIG_COGS_COGS_96X96", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_96X96_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 887, "IMAGE_QUEST_DIG_COGS_COGS_96X96_2", 480, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlay_UI_Dig_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAY_UI_DIG_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 39, "ATLASIMAGE_ATLAS_GAMEPLAY_UI_DIG_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND = GlobalMembersResourcesWP.GetImageThrow(theManager, 869, "IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 870, "IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_LEVEL = GlobalMembersResourcesWP.GetImageThrow(theManager, 871, "IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_LEVEL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_METER = GlobalMembersResourcesWP.GetImageThrow(theManager, 872, "IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_METER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_HUD_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 873, "IMAGE_INGAMEUI_DIAMOND_MINE_HUD_SHADOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_BACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 874, "IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_BACK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 875, "IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_SCORE_BAR_BACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 876, "IMAGE_INGAMEUI_DIAMOND_MINE_SCORE_BAR_BACK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_DIAMOND_MINE_TIMER = GlobalMembersResourcesWP.GetImageThrow(theManager, 877, "IMAGE_INGAMEUI_DIAMOND_MINE_TIMER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_113X114 = GlobalMembersResourcesWP.GetImageThrow(theManager, 878, "IMAGE_QUEST_DIG_COGS_COGS_113X114", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_165X165 = GlobalMembersResourcesWP.GetImageThrow(theManager, 879, "IMAGE_QUEST_DIG_COGS_COGS_165X165", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_166X166 = GlobalMembersResourcesWP.GetImageThrow(theManager, 880, "IMAGE_QUEST_DIG_COGS_COGS_166X166", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_202X202 = GlobalMembersResourcesWP.GetImageThrow(theManager, 881, "IMAGE_QUEST_DIG_COGS_COGS_202X202", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_63X64 = GlobalMembersResourcesWP.GetImageThrow(theManager, 882, "IMAGE_QUEST_DIG_COGS_COGS_63X64", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_63X64_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 883, "IMAGE_QUEST_DIG_COGS_COGS_63X64_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_89X90 = GlobalMembersResourcesWP.GetImageThrow(theManager, 884, "IMAGE_QUEST_DIG_COGS_COGS_89X90", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_90X90 = GlobalMembersResourcesWP.GetImageThrow(theManager, 885, "IMAGE_QUEST_DIG_COGS_COGS_90X90", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_96X96 = GlobalMembersResourcesWP.GetImageThrow(theManager, 886, "IMAGE_QUEST_DIG_COGS_COGS_96X96", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_COGS_COGS_96X96_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 887, "IMAGE_QUEST_DIG_COGS_COGS_96X96_2", 960, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlay_UI_Dig_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.POPANIM_QUEST_DIG_COGS = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1552, "POPANIM_QUEST_DIG_COGS", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlay_UI_NormalResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlay_UI_Normal_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlay_UI_Normal_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlay_UI_Normal_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAY_UI_NORMAL_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 40, "ATLASIMAGE_ATLAS_GAMEPLAY_UI_NORMAL_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 1091, "IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_PROGRESS_BAR_BACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1092, "IMAGE_INGAMEUI_PROGRESS_BAR_BACK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_PROGRESS_BAR_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 1093, "IMAGE_INGAMEUI_PROGRESS_BAR_FRAME", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON = GlobalMembersResourcesWP.GetImageThrow(theManager, 1094, "IMAGE_INGAMEUI_REPLAY_BUTTON", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LEVELBAR_ENDPIECE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1095, "IMAGE_LEVELBAR_ENDPIECE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlay_UI_Normal_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAY_UI_NORMAL_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 41, "ATLASIMAGE_ATLAS_GAMEPLAY_UI_NORMAL_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 1091, "IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_PROGRESS_BAR_BACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1092, "IMAGE_INGAMEUI_PROGRESS_BAR_BACK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_PROGRESS_BAR_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 1093, "IMAGE_INGAMEUI_PROGRESS_BAR_FRAME", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_REPLAY_BUTTON = GlobalMembersResourcesWP.GetImageThrow(theManager, 1094, "IMAGE_INGAMEUI_REPLAY_BUTTON", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LEVELBAR_ENDPIECE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1095, "IMAGE_LEVELBAR_ENDPIECE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_BalanceResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Balance_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Balance_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Balance_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BALANCE_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 42, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BALANCE_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_BALANCE_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 1096, "IMAGE_QUEST_BALANCE_FRAME", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BALANCE_RIG_ARROW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1097, "IMAGE_BALANCE_RIG_ARROW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BALANCE_RIG_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1098, "IMAGE_BALANCE_RIG_GLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BALANCE_RIG_SIDE_CHAIN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1099, "IMAGE_BALANCE_RIG_SIDE_CHAIN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BALANCE_RIG_TOP_CHAIN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1100, "IMAGE_BALANCE_RIG_TOP_CHAIN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BALANCE_RIG_WHEEL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1101, "IMAGE_BALANCE_RIG_WHEEL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BALANCE_RIG_WHEEL_SPOKES = GlobalMembersResourcesWP.GetImageThrow(theManager, 1102, "IMAGE_BALANCE_RIG_WHEEL_SPOKES", 480, 0);
				GlobalMembersResourcesWP.IMAGE_WEIGHT_CAP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1104, "IMAGE_WEIGHT_CAP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_WEIGHT_FILL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1105, "IMAGE_WEIGHT_FILL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_WEIGHT_GLASS_BACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1106, "IMAGE_WEIGHT_GLASS_BACK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_WEIGHT_GLASS_FRONT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1107, "IMAGE_WEIGHT_GLASS_FRONT", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Balance_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BALANCE_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 43, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BALANCE_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_BALANCE_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 1096, "IMAGE_QUEST_BALANCE_FRAME", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BALANCE_RIG_ARROW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1097, "IMAGE_BALANCE_RIG_ARROW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BALANCE_RIG_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1098, "IMAGE_BALANCE_RIG_GLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BALANCE_RIG_SIDE_CHAIN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1099, "IMAGE_BALANCE_RIG_SIDE_CHAIN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BALANCE_RIG_TOP_CHAIN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1100, "IMAGE_BALANCE_RIG_TOP_CHAIN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BALANCE_RIG_WHEEL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1101, "IMAGE_BALANCE_RIG_WHEEL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BALANCE_RIG_WHEEL_SPOKES = GlobalMembersResourcesWP.GetImageThrow(theManager, 1102, "IMAGE_BALANCE_RIG_WHEEL_SPOKES", 960, 0);
				GlobalMembersResourcesWP.IMAGE_WEIGHT_CAP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1104, "IMAGE_WEIGHT_CAP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_WEIGHT_FILL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1105, "IMAGE_WEIGHT_FILL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_WEIGHT_GLASS_BACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1106, "IMAGE_WEIGHT_GLASS_BACK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_WEIGHT_GLASS_FRONT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1107, "IMAGE_WEIGHT_GLASS_FRONT", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_ButterflyResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Butterfly_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Butterfly_960Resources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractGamePlayQuest_Butterfly_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Butterfly_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BUTTERFLY_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 44, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BUTTERFLY_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BUTTERFLY_BODY = GlobalMembersResourcesWP.GetImageThrow(theManager, 903, "IMAGE_BUTTERFLY_BODY", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BUTTERFLY_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 904, "IMAGE_BUTTERFLY_SHADOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BUTTERFLY_WINGS = GlobalMembersResourcesWP.GetImageThrow(theManager, 905, "IMAGE_BUTTERFLY_WINGS", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_BOTTOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 906, "IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_BOTTOM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_TOP = GlobalMembersResourcesWP.GetImageThrow(theManager, 907, "IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_TOP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_BUTTERFLY = GlobalMembersResourcesWP.GetImageThrow(theManager, 908, "IMAGE_INGAMEUI_BUTTERFLIES_BUTTERFLY", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_BG = GlobalMembersResourcesWP.GetImageThrow(theManager, 909, "IMAGE_INGAMEUI_BUTTERFLIES_SCORE_BG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 910, "IMAGE_INGAMEUI_BUTTERFLIES_SCORE_FRAME", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_WEB = GlobalMembersResourcesWP.GetImageThrow(theManager, 911, "IMAGE_INGAMEUI_BUTTERFLIES_WEB", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_26X50 = GlobalMembersResourcesWP.GetImageThrow(theManager, 912, "IMAGE_ANIMS_SPIDER_SPIDER_26X50", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_26X52 = GlobalMembersResourcesWP.GetImageThrow(theManager, 913, "IMAGE_ANIMS_SPIDER_SPIDER_26X52", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_27X42 = GlobalMembersResourcesWP.GetImageThrow(theManager, 914, "IMAGE_ANIMS_SPIDER_SPIDER_27X42", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_27X42_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 915, "IMAGE_ANIMS_SPIDER_SPIDER_27X42_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_28X50 = GlobalMembersResourcesWP.GetImageThrow(theManager, 916, "IMAGE_ANIMS_SPIDER_SPIDER_28X50", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_29X41 = GlobalMembersResourcesWP.GetImageThrow(theManager, 917, "IMAGE_ANIMS_SPIDER_SPIDER_29X41", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_31X41 = GlobalMembersResourcesWP.GetImageThrow(theManager, 918, "IMAGE_ANIMS_SPIDER_SPIDER_31X41", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_33X51 = GlobalMembersResourcesWP.GetImageThrow(theManager, 919, "IMAGE_ANIMS_SPIDER_SPIDER_33X51", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_35X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 920, "IMAGE_ANIMS_SPIDER_SPIDER_35X34", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_38X21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 921, "IMAGE_ANIMS_SPIDER_SPIDER_38X21", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_40X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 922, "IMAGE_ANIMS_SPIDER_SPIDER_40X36", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_41X46 = GlobalMembersResourcesWP.GetImageThrow(theManager, 923, "IMAGE_ANIMS_SPIDER_SPIDER_41X46", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_42X40 = GlobalMembersResourcesWP.GetImageThrow(theManager, 924, "IMAGE_ANIMS_SPIDER_SPIDER_42X40", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_44X27 = GlobalMembersResourcesWP.GetImageThrow(theManager, 925, "IMAGE_ANIMS_SPIDER_SPIDER_44X27", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_46X35 = GlobalMembersResourcesWP.GetImageThrow(theManager, 926, "IMAGE_ANIMS_SPIDER_SPIDER_46X35", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_47X39 = GlobalMembersResourcesWP.GetImageThrow(theManager, 927, "IMAGE_ANIMS_SPIDER_SPIDER_47X39", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_53X41 = GlobalMembersResourcesWP.GetImageThrow(theManager, 928, "IMAGE_ANIMS_SPIDER_SPIDER_53X41", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_55X49 = GlobalMembersResourcesWP.GetImageThrow(theManager, 929, "IMAGE_ANIMS_SPIDER_SPIDER_55X49", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_57X33 = GlobalMembersResourcesWP.GetImageThrow(theManager, 930, "IMAGE_ANIMS_SPIDER_SPIDER_57X33", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_61X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 931, "IMAGE_ANIMS_SPIDER_SPIDER_61X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_63X53 = GlobalMembersResourcesWP.GetImageThrow(theManager, 932, "IMAGE_ANIMS_SPIDER_SPIDER_63X53", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_72X38 = GlobalMembersResourcesWP.GetImageThrow(theManager, 933, "IMAGE_ANIMS_SPIDER_SPIDER_72X38", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_84X108 = GlobalMembersResourcesWP.GetImageThrow(theManager, 934, "IMAGE_ANIMS_SPIDER_SPIDER_84X108", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_87X102 = GlobalMembersResourcesWP.GetImageThrow(theManager, 935, "IMAGE_ANIMS_SPIDER_SPIDER_87X102", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_91X336 = GlobalMembersResourcesWP.GetImageThrow(theManager, 936, "IMAGE_ANIMS_SPIDER_SPIDER_91X336", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_137X175 = GlobalMembersResourcesWP.GetImageThrow(theManager, 937, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_137X175", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_144X162 = GlobalMembersResourcesWP.GetImageThrow(theManager, 938, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_144X162", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_149X204 = GlobalMembersResourcesWP.GetImageThrow(theManager, 939, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_149X204", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_20X476 = GlobalMembersResourcesWP.GetImageThrow(theManager, 940, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_20X476", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_34X62 = GlobalMembersResourcesWP.GetImageThrow(theManager, 941, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_34X62", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_40X67 = GlobalMembersResourcesWP.GetImageThrow(theManager, 942, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_40X67", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_41X65 = GlobalMembersResourcesWP.GetImageThrow(theManager, 943, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_41X65", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_43X65 = GlobalMembersResourcesWP.GetImageThrow(theManager, 944, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_43X65", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_45X67 = GlobalMembersResourcesWP.GetImageThrow(theManager, 945, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_45X67", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_46X53 = GlobalMembersResourcesWP.GetImageThrow(theManager, 946, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_46X53", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_49X57 = GlobalMembersResourcesWP.GetImageThrow(theManager, 947, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_49X57", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_51X60 = GlobalMembersResourcesWP.GetImageThrow(theManager, 948, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_51X60", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_59X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 949, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_59X34", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_64X53 = GlobalMembersResourcesWP.GetImageThrow(theManager, 950, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_64X53", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_69X40 = GlobalMembersResourcesWP.GetImageThrow(theManager, 951, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_69X40", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_71X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 952, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_71X36", 480, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Butterfly_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BUTTERFLY_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 45, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BUTTERFLY_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BUTTERFLY_BODY = GlobalMembersResourcesWP.GetImageThrow(theManager, 903, "IMAGE_BUTTERFLY_BODY", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BUTTERFLY_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 904, "IMAGE_BUTTERFLY_SHADOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BUTTERFLY_WINGS = GlobalMembersResourcesWP.GetImageThrow(theManager, 905, "IMAGE_BUTTERFLY_WINGS", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_BOTTOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 906, "IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_BOTTOM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_TOP = GlobalMembersResourcesWP.GetImageThrow(theManager, 907, "IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_TOP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_BUTTERFLY = GlobalMembersResourcesWP.GetImageThrow(theManager, 908, "IMAGE_INGAMEUI_BUTTERFLIES_BUTTERFLY", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_BG = GlobalMembersResourcesWP.GetImageThrow(theManager, 909, "IMAGE_INGAMEUI_BUTTERFLIES_SCORE_BG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_SCORE_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 910, "IMAGE_INGAMEUI_BUTTERFLIES_SCORE_FRAME", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_BUTTERFLIES_WEB = GlobalMembersResourcesWP.GetImageThrow(theManager, 911, "IMAGE_INGAMEUI_BUTTERFLIES_WEB", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_26X50 = GlobalMembersResourcesWP.GetImageThrow(theManager, 912, "IMAGE_ANIMS_SPIDER_SPIDER_26X50", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_26X52 = GlobalMembersResourcesWP.GetImageThrow(theManager, 913, "IMAGE_ANIMS_SPIDER_SPIDER_26X52", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_27X42 = GlobalMembersResourcesWP.GetImageThrow(theManager, 914, "IMAGE_ANIMS_SPIDER_SPIDER_27X42", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_27X42_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 915, "IMAGE_ANIMS_SPIDER_SPIDER_27X42_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_28X50 = GlobalMembersResourcesWP.GetImageThrow(theManager, 916, "IMAGE_ANIMS_SPIDER_SPIDER_28X50", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_29X41 = GlobalMembersResourcesWP.GetImageThrow(theManager, 917, "IMAGE_ANIMS_SPIDER_SPIDER_29X41", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_31X41 = GlobalMembersResourcesWP.GetImageThrow(theManager, 918, "IMAGE_ANIMS_SPIDER_SPIDER_31X41", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_33X51 = GlobalMembersResourcesWP.GetImageThrow(theManager, 919, "IMAGE_ANIMS_SPIDER_SPIDER_33X51", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_35X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 920, "IMAGE_ANIMS_SPIDER_SPIDER_35X34", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_38X21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 921, "IMAGE_ANIMS_SPIDER_SPIDER_38X21", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_40X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 922, "IMAGE_ANIMS_SPIDER_SPIDER_40X36", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_41X46 = GlobalMembersResourcesWP.GetImageThrow(theManager, 923, "IMAGE_ANIMS_SPIDER_SPIDER_41X46", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_42X40 = GlobalMembersResourcesWP.GetImageThrow(theManager, 924, "IMAGE_ANIMS_SPIDER_SPIDER_42X40", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_44X27 = GlobalMembersResourcesWP.GetImageThrow(theManager, 925, "IMAGE_ANIMS_SPIDER_SPIDER_44X27", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_46X35 = GlobalMembersResourcesWP.GetImageThrow(theManager, 926, "IMAGE_ANIMS_SPIDER_SPIDER_46X35", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_47X39 = GlobalMembersResourcesWP.GetImageThrow(theManager, 927, "IMAGE_ANIMS_SPIDER_SPIDER_47X39", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_53X41 = GlobalMembersResourcesWP.GetImageThrow(theManager, 928, "IMAGE_ANIMS_SPIDER_SPIDER_53X41", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_55X49 = GlobalMembersResourcesWP.GetImageThrow(theManager, 929, "IMAGE_ANIMS_SPIDER_SPIDER_55X49", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_57X33 = GlobalMembersResourcesWP.GetImageThrow(theManager, 930, "IMAGE_ANIMS_SPIDER_SPIDER_57X33", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_61X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 931, "IMAGE_ANIMS_SPIDER_SPIDER_61X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_63X53 = GlobalMembersResourcesWP.GetImageThrow(theManager, 932, "IMAGE_ANIMS_SPIDER_SPIDER_63X53", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_72X38 = GlobalMembersResourcesWP.GetImageThrow(theManager, 933, "IMAGE_ANIMS_SPIDER_SPIDER_72X38", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_84X108 = GlobalMembersResourcesWP.GetImageThrow(theManager, 934, "IMAGE_ANIMS_SPIDER_SPIDER_84X108", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_87X102 = GlobalMembersResourcesWP.GetImageThrow(theManager, 935, "IMAGE_ANIMS_SPIDER_SPIDER_87X102", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_SPIDER_SPIDER_91X336 = GlobalMembersResourcesWP.GetImageThrow(theManager, 936, "IMAGE_ANIMS_SPIDER_SPIDER_91X336", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_137X175 = GlobalMembersResourcesWP.GetImageThrow(theManager, 937, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_137X175", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_144X162 = GlobalMembersResourcesWP.GetImageThrow(theManager, 938, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_144X162", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_149X204 = GlobalMembersResourcesWP.GetImageThrow(theManager, 939, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_149X204", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_20X476 = GlobalMembersResourcesWP.GetImageThrow(theManager, 940, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_20X476", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_34X62 = GlobalMembersResourcesWP.GetImageThrow(theManager, 941, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_34X62", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_40X67 = GlobalMembersResourcesWP.GetImageThrow(theManager, 942, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_40X67", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_41X65 = GlobalMembersResourcesWP.GetImageThrow(theManager, 943, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_41X65", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_43X65 = GlobalMembersResourcesWP.GetImageThrow(theManager, 944, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_43X65", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_45X67 = GlobalMembersResourcesWP.GetImageThrow(theManager, 945, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_45X67", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_46X53 = GlobalMembersResourcesWP.GetImageThrow(theManager, 946, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_46X53", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_49X57 = GlobalMembersResourcesWP.GetImageThrow(theManager, 947, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_49X57", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_51X60 = GlobalMembersResourcesWP.GetImageThrow(theManager, 948, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_51X60", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_59X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 949, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_59X34", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_64X53 = GlobalMembersResourcesWP.GetImageThrow(theManager, 950, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_64X53", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_69X40 = GlobalMembersResourcesWP.GetImageThrow(theManager, 951, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_69X40", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_71X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 952, "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_71X36", 960, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Butterfly_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.POPANIM_ANIMS_SPIDER = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1553, "POPANIM_ANIMS_SPIDER", 0, 0);
				GlobalMembersResourcesWP.POPANIM_ANIMS_LARGE_SPIDER = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1554, "POPANIM_ANIMS_LARGE_SPIDER", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_DigResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Dig_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Dig_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Dig_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_DIG_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 46, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_DIG_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1147, "IMAGE_QUEST_DIG_BOARD_NUGGETPART1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1148, "IMAGE_QUEST_DIG_BOARD_NUGGETPART2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1149, "IMAGE_QUEST_DIG_BOARD_NUGGETPART3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1150, "IMAGE_QUEST_DIG_BOARD_NUGGETPART4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1151, "IMAGE_QUEST_DIG_BOARD_NUGGETPART5", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1152, "IMAGE_QUEST_DIG_BOARD_NUGGETPART6", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1153, "IMAGE_QUEST_DIG_BOARD_NUGGETPART7", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1154, "IMAGE_QUEST_DIG_BOARD_NUGGETPART8", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1155, "IMAGE_QUEST_DIG_BOARD_NUGGETPART9", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1156, "IMAGE_QUEST_DIG_BOARD_NUGGETPART10", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET1_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1157, "IMAGE_QUEST_DIG_BOARD_NUGGET1_1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET1_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1158, "IMAGE_QUEST_DIG_BOARD_NUGGET1_2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET1_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1159, "IMAGE_QUEST_DIG_BOARD_NUGGET1_3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET2_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1160, "IMAGE_QUEST_DIG_BOARD_NUGGET2_1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET2_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1161, "IMAGE_QUEST_DIG_BOARD_NUGGET2_2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET2_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1162, "IMAGE_QUEST_DIG_BOARD_NUGGET2_3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET3_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1163, "IMAGE_QUEST_DIG_BOARD_NUGGET3_1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET3_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1164, "IMAGE_QUEST_DIG_BOARD_NUGGET3_2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET3_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1165, "IMAGE_QUEST_DIG_BOARD_NUGGET3_3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET4_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1166, "IMAGE_QUEST_DIG_BOARD_NUGGET4_1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET4_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1167, "IMAGE_QUEST_DIG_BOARD_NUGGET4_2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET4_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1168, "IMAGE_QUEST_DIG_BOARD_NUGGET4_3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET5_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1169, "IMAGE_QUEST_DIG_BOARD_NUGGET5_1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET5_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1170, "IMAGE_QUEST_DIG_BOARD_NUGGET5_2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET5_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1171, "IMAGE_QUEST_DIG_BOARD_NUGGET5_3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_BOTTOM_OVERLAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 1172, "IMAGE_QUEST_DIG_BOARD_BOTTOM_OVERLAY", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1173, "IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM_HIGHLIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1174, "IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM_HIGHLIGHT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM_HIGHLIGHT_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1175, "IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM_HIGHLIGHT_SHADOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_FULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1176, "IMAGE_QUEST_DIG_BOARD_CENTER_FULL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_LEFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1177, "IMAGE_QUEST_DIG_BOARD_CENTER_LEFT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_LEFT_HIGHLIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1178, "IMAGE_QUEST_DIG_BOARD_CENTER_LEFT_HIGHLIGHT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_LEFT_HIGHLIGHT_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1179, "IMAGE_QUEST_DIG_BOARD_CENTER_LEFT_HIGHLIGHT_SHADOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1180, "IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT__HIGHLIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1181, "IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT__HIGHLIGHT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT_HIGHLIGHT_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1182, "IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT_HIGHLIGHT_SHADOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_TOP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1183, "IMAGE_QUEST_DIG_BOARD_CENTER_TOP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_TOP_HIGHLIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1184, "IMAGE_QUEST_DIG_BOARD_CENTER_TOP_HIGHLIGHT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1185, "IMAGE_QUEST_DIG_BOARD_DIAMOND1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1186, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1187, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1188, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1189, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_DIRT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1190, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_DIRT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1191, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1192, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1193, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1194, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1195, "IMAGE_QUEST_DIG_BOARD_DIAMOND2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1196, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1197, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1198, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1199, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_DIRT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1200, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_DIRT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1201, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1202, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1203, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1204, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1205, "IMAGE_QUEST_DIG_BOARD_DIAMOND3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1206, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1207, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1208, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_DIRT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1209, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_DIRT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1210, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1211, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1212, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1213, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1214, "IMAGE_QUEST_DIG_BOARD_DIAMOND4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1215, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1216, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1217, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1218, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_DIRT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1219, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_DIRT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1220, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1221, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1222, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1223, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_GOLDGROUP1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1224, "IMAGE_QUEST_DIG_BOARD_GOLDGROUP1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_GOLDGROUP2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1225, "IMAGE_QUEST_DIG_BOARD_GOLDGROUP2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_GOLDGROUP3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1226, "IMAGE_QUEST_DIG_BOARD_GOLDGROUP3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_GRASS = GlobalMembersResourcesWP.GetImageThrow(theManager, 1227, "IMAGE_QUEST_DIG_BOARD_GRASS", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_GRASS_LEFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1228, "IMAGE_QUEST_DIG_BOARD_GRASS_LEFT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_GRASS_RIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1229, "IMAGE_QUEST_DIG_BOARD_GRASS_RIGHT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_HYPERCUBE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1230, "IMAGE_QUEST_DIG_BOARD_HYPERCUBE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_ABICUS_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1231, "IMAGE_QUEST_DIG_BOARD_ITEM_ABICUS_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_ANVIL_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1232, "IMAGE_QUEST_DIG_BOARD_ITEM_ANVIL_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_ASTROLABE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1233, "IMAGE_QUEST_DIG_BOARD_ITEM_ASTROLABE_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_AXE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1234, "IMAGE_QUEST_DIG_BOARD_ITEM_AXE_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BELL_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1235, "IMAGE_QUEST_DIG_BOARD_ITEM_BELL_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BJORN_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1236, "IMAGE_QUEST_DIG_BOARD_ITEM_BJORN_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BOOK_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1237, "IMAGE_QUEST_DIG_BOARD_ITEM_BOOK_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BOOTS_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1238, "IMAGE_QUEST_DIG_BOARD_ITEM_BOOTS_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BOWARROW_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1239, "IMAGE_QUEST_DIG_BOARD_ITEM_BOWARROW_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BOWL_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1240, "IMAGE_QUEST_DIG_BOARD_ITEM_BOWL_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BRUSH_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1241, "IMAGE_QUEST_DIG_BOARD_ITEM_BRUSH_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_CLOCK_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1242, "IMAGE_QUEST_DIG_BOARD_ITEM_CLOCK_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_COMB_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1243, "IMAGE_QUEST_DIG_BOARD_ITEM_COMB_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_CREST_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1244, "IMAGE_QUEST_DIG_BOARD_ITEM_CREST_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_DAGGER_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1245, "IMAGE_QUEST_DIG_BOARD_ITEM_DAGGER_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_DISH_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1246, "IMAGE_QUEST_DIG_BOARD_ITEM_DISH_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_DMGEM_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1247, "IMAGE_QUEST_DIG_BOARD_ITEM_DMGEM_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_FLUTE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1248, "IMAGE_QUEST_DIG_BOARD_ITEM_FLUTE_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_FORK_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1249, "IMAGE_QUEST_DIG_BOARD_ITEM_FORK_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_FROG_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1250, "IMAGE_QUEST_DIG_BOARD_ITEM_FROG_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_GAUNTLET_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1251, "IMAGE_QUEST_DIG_BOARD_ITEM_GAUNTLET_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_GEAR_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1252, "IMAGE_QUEST_DIG_BOARD_ITEM_GEAR_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_HAMMER_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1253, "IMAGE_QUEST_DIG_BOARD_ITEM_HAMMER_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_HARP_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1254, "IMAGE_QUEST_DIG_BOARD_ITEM_HARP_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_HELMET_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1255, "IMAGE_QUEST_DIG_BOARD_ITEM_HELMET_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_HORN_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1256, "IMAGE_QUEST_DIG_BOARD_ITEM_HORN_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_HORSE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1257, "IMAGE_QUEST_DIG_BOARD_ITEM_HORSE_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_HORSESHOE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1258, "IMAGE_QUEST_DIG_BOARD_ITEM_HORSESHOE_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_KEY_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1259, "IMAGE_QUEST_DIG_BOARD_ITEM_KEY_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_LAMP_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1260, "IMAGE_QUEST_DIG_BOARD_ITEM_LAMP_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_MACE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1261, "IMAGE_QUEST_DIG_BOARD_ITEM_MACE_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_MAGNIFYINGGLASS_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1262, "IMAGE_QUEST_DIG_BOARD_ITEM_MAGNIFYINGGLASS_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_MASK_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1263, "IMAGE_QUEST_DIG_BOARD_ITEM_MASK_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_POT_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1264, "IMAGE_QUEST_DIG_BOARD_ITEM_POT_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_SCROLL_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1265, "IMAGE_QUEST_DIG_BOARD_ITEM_SCROLL_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_SCYTHE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1266, "IMAGE_QUEST_DIG_BOARD_ITEM_SCYTHE_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_SEXTANT_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1267, "IMAGE_QUEST_DIG_BOARD_ITEM_SEXTANT_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_SPOON_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1268, "IMAGE_QUEST_DIG_BOARD_ITEM_SPOON_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_STAFF_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1269, "IMAGE_QUEST_DIG_BOARD_ITEM_STAFF_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_STIRRUP_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1270, "IMAGE_QUEST_DIG_BOARD_ITEM_STIRRUP_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_TELESCOPE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1271, "IMAGE_QUEST_DIG_BOARD_ITEM_TELESCOPE_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_TONGS_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1272, "IMAGE_QUEST_DIG_BOARD_ITEM_TONGS_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_TRIDENT_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1273, "IMAGE_QUEST_DIG_BOARD_ITEM_TRIDENT_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_TROWEL_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1274, "IMAGE_QUEST_DIG_BOARD_ITEM_TROWEL_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_URN_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1275, "IMAGE_QUEST_DIG_BOARD_ITEM_URN_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_VASE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1276, "IMAGE_QUEST_DIG_BOARD_ITEM_VASE_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_PEBBLES1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1277, "IMAGE_QUEST_DIG_BOARD_PEBBLES1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_PEBBLES2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1278, "IMAGE_QUEST_DIG_BOARD_PEBBLES2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_PEBBLES3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1279, "IMAGE_QUEST_DIG_BOARD_PEBBLES3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1280, "IMAGE_QUEST_DIG_BOARD_STR1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1281, "IMAGE_QUEST_DIG_BOARD_STR2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1282, "IMAGE_QUEST_DIG_BOARD_STR3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1283, "IMAGE_QUEST_DIG_BOARD_STR4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIRT_OVERLAY1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1284, "IMAGE_QUEST_DIG_DIRT_OVERLAY1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIRT_OVERLAY2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1285, "IMAGE_QUEST_DIG_DIRT_OVERLAY2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIRT_OVERLAY3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1286, "IMAGE_QUEST_DIG_DIRT_OVERLAY3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIRT_UNDERLAY1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1287, "IMAGE_QUEST_DIG_DIRT_UNDERLAY1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIRT_UNDERLAY2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1288, "IMAGE_QUEST_DIG_DIRT_UNDERLAY2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIRT_UNDERLAY3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1289, "IMAGE_QUEST_DIG_DIRT_UNDERLAY3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1290, "IMAGE_QUEST_DIG_GLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIAMONDPART = GlobalMembersResourcesWP.GetImageThrow(theManager, 1291, "IMAGE_QUEST_DIG_DIAMONDPART", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_STREAK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1292, "IMAGE_QUEST_DIG_STREAK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_WALLROCKS_LARGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1293, "IMAGE_WALLROCKS_LARGE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_WALLROCKS_MEDIUM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1294, "IMAGE_WALLROCKS_MEDIUM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_WALLROCKS_SMALL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1295, "IMAGE_WALLROCKS_SMALL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_WALLROCKS_SMALL_BROWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1296, "IMAGE_WALLROCKS_SMALL_BROWN", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Dig_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_DIG_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 47, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_DIG_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1147, "IMAGE_QUEST_DIG_BOARD_NUGGETPART1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1148, "IMAGE_QUEST_DIG_BOARD_NUGGETPART2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1149, "IMAGE_QUEST_DIG_BOARD_NUGGETPART3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1150, "IMAGE_QUEST_DIG_BOARD_NUGGETPART4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1151, "IMAGE_QUEST_DIG_BOARD_NUGGETPART5", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1152, "IMAGE_QUEST_DIG_BOARD_NUGGETPART6", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1153, "IMAGE_QUEST_DIG_BOARD_NUGGETPART7", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1154, "IMAGE_QUEST_DIG_BOARD_NUGGETPART8", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1155, "IMAGE_QUEST_DIG_BOARD_NUGGETPART9", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGETPART10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1156, "IMAGE_QUEST_DIG_BOARD_NUGGETPART10", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET1_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1157, "IMAGE_QUEST_DIG_BOARD_NUGGET1_1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET1_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1158, "IMAGE_QUEST_DIG_BOARD_NUGGET1_2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET1_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1159, "IMAGE_QUEST_DIG_BOARD_NUGGET1_3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET2_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1160, "IMAGE_QUEST_DIG_BOARD_NUGGET2_1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET2_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1161, "IMAGE_QUEST_DIG_BOARD_NUGGET2_2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET2_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1162, "IMAGE_QUEST_DIG_BOARD_NUGGET2_3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET3_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1163, "IMAGE_QUEST_DIG_BOARD_NUGGET3_1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET3_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1164, "IMAGE_QUEST_DIG_BOARD_NUGGET3_2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET3_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1165, "IMAGE_QUEST_DIG_BOARD_NUGGET3_3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET4_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1166, "IMAGE_QUEST_DIG_BOARD_NUGGET4_1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET4_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1167, "IMAGE_QUEST_DIG_BOARD_NUGGET4_2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET4_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1168, "IMAGE_QUEST_DIG_BOARD_NUGGET4_3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET5_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1169, "IMAGE_QUEST_DIG_BOARD_NUGGET5_1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET5_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1170, "IMAGE_QUEST_DIG_BOARD_NUGGET5_2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_NUGGET5_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1171, "IMAGE_QUEST_DIG_BOARD_NUGGET5_3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_BOTTOM_OVERLAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 1172, "IMAGE_QUEST_DIG_BOARD_BOTTOM_OVERLAY", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1173, "IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM_HIGHLIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1174, "IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM_HIGHLIGHT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM_HIGHLIGHT_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1175, "IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM_HIGHLIGHT_SHADOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_FULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1176, "IMAGE_QUEST_DIG_BOARD_CENTER_FULL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_LEFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1177, "IMAGE_QUEST_DIG_BOARD_CENTER_LEFT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_LEFT_HIGHLIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1178, "IMAGE_QUEST_DIG_BOARD_CENTER_LEFT_HIGHLIGHT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_LEFT_HIGHLIGHT_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1179, "IMAGE_QUEST_DIG_BOARD_CENTER_LEFT_HIGHLIGHT_SHADOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1180, "IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT__HIGHLIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1181, "IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT__HIGHLIGHT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT_HIGHLIGHT_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1182, "IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT_HIGHLIGHT_SHADOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_TOP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1183, "IMAGE_QUEST_DIG_BOARD_CENTER_TOP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_CENTER_TOP_HIGHLIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1184, "IMAGE_QUEST_DIG_BOARD_CENTER_TOP_HIGHLIGHT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1185, "IMAGE_QUEST_DIG_BOARD_DIAMOND1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1186, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1187, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1188, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1189, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_DIRT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1190, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_DIRT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1191, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1192, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1193, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1194, "IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1195, "IMAGE_QUEST_DIG_BOARD_DIAMOND2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1196, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1197, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1198, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1199, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_DIRT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1200, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_DIRT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1201, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1202, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1203, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1204, "IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1205, "IMAGE_QUEST_DIG_BOARD_DIAMOND3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1206, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1207, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1208, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_DIRT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1209, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_DIRT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1210, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1211, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1212, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1213, "IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1214, "IMAGE_QUEST_DIG_BOARD_DIAMOND4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1215, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1216, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1217, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1218, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_DIRT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1219, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_DIRT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1220, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1221, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1222, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1223, "IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_GOLDGROUP1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1224, "IMAGE_QUEST_DIG_BOARD_GOLDGROUP1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_GOLDGROUP2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1225, "IMAGE_QUEST_DIG_BOARD_GOLDGROUP2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_GOLDGROUP3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1226, "IMAGE_QUEST_DIG_BOARD_GOLDGROUP3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_GRASS = GlobalMembersResourcesWP.GetImageThrow(theManager, 1227, "IMAGE_QUEST_DIG_BOARD_GRASS", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_GRASS_LEFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1228, "IMAGE_QUEST_DIG_BOARD_GRASS_LEFT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_GRASS_RIGHT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1229, "IMAGE_QUEST_DIG_BOARD_GRASS_RIGHT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_HYPERCUBE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1230, "IMAGE_QUEST_DIG_BOARD_HYPERCUBE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_ABICUS_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1231, "IMAGE_QUEST_DIG_BOARD_ITEM_ABICUS_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_ANVIL_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1232, "IMAGE_QUEST_DIG_BOARD_ITEM_ANVIL_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_ASTROLABE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1233, "IMAGE_QUEST_DIG_BOARD_ITEM_ASTROLABE_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_AXE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1234, "IMAGE_QUEST_DIG_BOARD_ITEM_AXE_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BELL_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1235, "IMAGE_QUEST_DIG_BOARD_ITEM_BELL_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BJORN_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1236, "IMAGE_QUEST_DIG_BOARD_ITEM_BJORN_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BOOK_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1237, "IMAGE_QUEST_DIG_BOARD_ITEM_BOOK_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BOOTS_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1238, "IMAGE_QUEST_DIG_BOARD_ITEM_BOOTS_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BOWARROW_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1239, "IMAGE_QUEST_DIG_BOARD_ITEM_BOWARROW_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BOWL_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1240, "IMAGE_QUEST_DIG_BOARD_ITEM_BOWL_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_BRUSH_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1241, "IMAGE_QUEST_DIG_BOARD_ITEM_BRUSH_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_CLOCK_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1242, "IMAGE_QUEST_DIG_BOARD_ITEM_CLOCK_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_COMB_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1243, "IMAGE_QUEST_DIG_BOARD_ITEM_COMB_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_CREST_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1244, "IMAGE_QUEST_DIG_BOARD_ITEM_CREST_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_DAGGER_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1245, "IMAGE_QUEST_DIG_BOARD_ITEM_DAGGER_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_DISH_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1246, "IMAGE_QUEST_DIG_BOARD_ITEM_DISH_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_DMGEM_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1247, "IMAGE_QUEST_DIG_BOARD_ITEM_DMGEM_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_FLUTE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1248, "IMAGE_QUEST_DIG_BOARD_ITEM_FLUTE_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_FORK_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1249, "IMAGE_QUEST_DIG_BOARD_ITEM_FORK_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_FROG_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1250, "IMAGE_QUEST_DIG_BOARD_ITEM_FROG_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_GAUNTLET_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1251, "IMAGE_QUEST_DIG_BOARD_ITEM_GAUNTLET_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_GEAR_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1252, "IMAGE_QUEST_DIG_BOARD_ITEM_GEAR_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_HAMMER_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1253, "IMAGE_QUEST_DIG_BOARD_ITEM_HAMMER_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_HARP_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1254, "IMAGE_QUEST_DIG_BOARD_ITEM_HARP_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_HELMET_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1255, "IMAGE_QUEST_DIG_BOARD_ITEM_HELMET_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_HORN_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1256, "IMAGE_QUEST_DIG_BOARD_ITEM_HORN_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_HORSE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1257, "IMAGE_QUEST_DIG_BOARD_ITEM_HORSE_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_HORSESHOE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1258, "IMAGE_QUEST_DIG_BOARD_ITEM_HORSESHOE_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_KEY_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1259, "IMAGE_QUEST_DIG_BOARD_ITEM_KEY_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_LAMP_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1260, "IMAGE_QUEST_DIG_BOARD_ITEM_LAMP_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_MACE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1261, "IMAGE_QUEST_DIG_BOARD_ITEM_MACE_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_MAGNIFYINGGLASS_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1262, "IMAGE_QUEST_DIG_BOARD_ITEM_MAGNIFYINGGLASS_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_MASK_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1263, "IMAGE_QUEST_DIG_BOARD_ITEM_MASK_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_POT_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1264, "IMAGE_QUEST_DIG_BOARD_ITEM_POT_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_SCROLL_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1265, "IMAGE_QUEST_DIG_BOARD_ITEM_SCROLL_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_SCYTHE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1266, "IMAGE_QUEST_DIG_BOARD_ITEM_SCYTHE_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_SEXTANT_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1267, "IMAGE_QUEST_DIG_BOARD_ITEM_SEXTANT_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_SPOON_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1268, "IMAGE_QUEST_DIG_BOARD_ITEM_SPOON_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_STAFF_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1269, "IMAGE_QUEST_DIG_BOARD_ITEM_STAFF_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_STIRRUP_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1270, "IMAGE_QUEST_DIG_BOARD_ITEM_STIRRUP_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_TELESCOPE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1271, "IMAGE_QUEST_DIG_BOARD_ITEM_TELESCOPE_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_TONGS_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1272, "IMAGE_QUEST_DIG_BOARD_ITEM_TONGS_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_TRIDENT_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1273, "IMAGE_QUEST_DIG_BOARD_ITEM_TRIDENT_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_TROWEL_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1274, "IMAGE_QUEST_DIG_BOARD_ITEM_TROWEL_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_URN_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1275, "IMAGE_QUEST_DIG_BOARD_ITEM_URN_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_ITEM_VASE_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1276, "IMAGE_QUEST_DIG_BOARD_ITEM_VASE_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_PEBBLES1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1277, "IMAGE_QUEST_DIG_BOARD_PEBBLES1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_PEBBLES2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1278, "IMAGE_QUEST_DIG_BOARD_PEBBLES2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_PEBBLES3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1279, "IMAGE_QUEST_DIG_BOARD_PEBBLES3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1280, "IMAGE_QUEST_DIG_BOARD_STR1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1281, "IMAGE_QUEST_DIG_BOARD_STR2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1282, "IMAGE_QUEST_DIG_BOARD_STR3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_BOARD_STR4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1283, "IMAGE_QUEST_DIG_BOARD_STR4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIRT_OVERLAY1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1284, "IMAGE_QUEST_DIG_DIRT_OVERLAY1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIRT_OVERLAY2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1285, "IMAGE_QUEST_DIG_DIRT_OVERLAY2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIRT_OVERLAY3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1286, "IMAGE_QUEST_DIG_DIRT_OVERLAY3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIRT_UNDERLAY1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1287, "IMAGE_QUEST_DIG_DIRT_UNDERLAY1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIRT_UNDERLAY2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1288, "IMAGE_QUEST_DIG_DIRT_UNDERLAY2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIRT_UNDERLAY3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1289, "IMAGE_QUEST_DIG_DIRT_UNDERLAY3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1290, "IMAGE_QUEST_DIG_GLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_DIAMONDPART = GlobalMembersResourcesWP.GetImageThrow(theManager, 1291, "IMAGE_QUEST_DIG_DIAMONDPART", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_DIG_STREAK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1292, "IMAGE_QUEST_DIG_STREAK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_WALLROCKS_LARGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1293, "IMAGE_WALLROCKS_LARGE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_WALLROCKS_MEDIUM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1294, "IMAGE_WALLROCKS_MEDIUM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_WALLROCKS_SMALL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1295, "IMAGE_WALLROCKS_SMALL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_WALLROCKS_SMALL_BROWN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1296, "IMAGE_WALLROCKS_SMALL_BROWN", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_FillerResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Filler_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Filler_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Filler_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_FILLER_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 48, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_FILLER_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_MYSTERY_CIRCLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1318, "IMAGE_MYSTERY_CIRCLE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Filler_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_FILLER_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 49, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_FILLER_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_MYSTERY_CIRCLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1318, "IMAGE_MYSTERY_CIRCLE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_InfernoResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Inferno_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Inferno_960Resources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractGamePlayQuest_Inferno_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Inferno_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_INFERNO_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 50, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_INFERNO_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_ICE_BOTTOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 955, "IMAGE_INGAMEUI_ICE_STORM_ICE_BOTTOM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_ICE_LIQUID = GlobalMembersResourcesWP.GetImageThrow(theManager, 956, "IMAGE_INGAMEUI_ICE_STORM_ICE_LIQUID", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_ICE_METER = GlobalMembersResourcesWP.GetImageThrow(theManager, 957, "IMAGE_INGAMEUI_ICE_STORM_ICE_METER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_ICE_METER_ICE = GlobalMembersResourcesWP.GetImageThrow(theManager, 958, "IMAGE_INGAMEUI_ICE_STORM_ICE_METER_ICE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_ICE_METER_PIPE = GlobalMembersResourcesWP.GetImageThrow(theManager, 959, "IMAGE_INGAMEUI_ICE_STORM_ICE_METER_PIPE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_MULTIPLIER = GlobalMembersResourcesWP.GetImageThrow(theManager, 960, "IMAGE_INGAMEUI_ICE_STORM_MULTIPLIER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_TOP_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 961, "IMAGE_INGAMEUI_ICE_STORM_TOP_FRAME", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_AQUAFRESH = GlobalMembersResourcesWP.GetImageThrow(theManager, 962, "IMAGE_ANIMS_COLUMN1_AQUAFRESH", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_AQUAFRESH2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 963, "IMAGE_ANIMS_COLUMN1_AQUAFRESH2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_AQUAFRESHRED = GlobalMembersResourcesWP.GetImageThrow(theManager, 964, "IMAGE_ANIMS_COLUMN1_AQUAFRESHRED", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 965, "IMAGE_ANIMS_COLUMN1_COLUMN1", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK01 = GlobalMembersResourcesWP.GetImageThrow(theManager, 966, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK01", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK02 = GlobalMembersResourcesWP.GetImageThrow(theManager, 967, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK02", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK03 = GlobalMembersResourcesWP.GetImageThrow(theManager, 968, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK03", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK04 = GlobalMembersResourcesWP.GetImageThrow(theManager, 969, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK04", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK05 = GlobalMembersResourcesWP.GetImageThrow(theManager, 970, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK05", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK06 = GlobalMembersResourcesWP.GetImageThrow(theManager, 971, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK06", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK07 = GlobalMembersResourcesWP.GetImageThrow(theManager, 972, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK07", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK08 = GlobalMembersResourcesWP.GetImageThrow(theManager, 973, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK08", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK09 = GlobalMembersResourcesWP.GetImageThrow(theManager, 974, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK09", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 975, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 976, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK11", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 977, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK12", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 978, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK13", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 979, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK14", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 980, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK15", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK16 = GlobalMembersResourcesWP.GetImageThrow(theManager, 981, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK16", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 982, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK17", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK18 = GlobalMembersResourcesWP.GetImageThrow(theManager, 983, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK18", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 984, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK19", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 985, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK20", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 986, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK21", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 987, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK22", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK23 = GlobalMembersResourcesWP.GetImageThrow(theManager, 988, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK23", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK24 = GlobalMembersResourcesWP.GetImageThrow(theManager, 989, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK24", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 990, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK25", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 991, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK26", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK27 = GlobalMembersResourcesWP.GetImageThrow(theManager, 992, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK27", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK28 = GlobalMembersResourcesWP.GetImageThrow(theManager, 993, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK28", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 994, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK29", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 995, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK30", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK1A = GlobalMembersResourcesWP.GetImageThrow(theManager, 996, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK1A", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK1B = GlobalMembersResourcesWP.GetImageThrow(theManager, 997, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK1B", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK2A = GlobalMembersResourcesWP.GetImageThrow(theManager, 998, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK2A", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK2B = GlobalMembersResourcesWP.GetImageThrow(theManager, 999, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK2B", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK3A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1000, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK3A", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK3B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1001, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK3B", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK4A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1002, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK4A", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK4B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1003, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK4B", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK5A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1004, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK5A", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK5B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1005, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK5B", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1006, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH1", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1007, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1008, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1009, "IMAGE_ANIMS_COLUMN1_COLUMN1_GLOW", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_GLOW_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1010, "IMAGE_ANIMS_COLUMN1_COLUMN1_GLOW_RED", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_METER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1011, "IMAGE_ANIMS_COLUMN1_COLUMN1_METER", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_METER_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1012, "IMAGE_ANIMS_COLUMN1_COLUMN1_METER_RED", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_PULSE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1013, "IMAGE_ANIMS_COLUMN1_COLUMN1_PULSE", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_SUPERCRACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1014, "IMAGE_ANIMS_COLUMN1_COLUMN1_SUPERCRACK", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_SUPERCRACK_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1015, "IMAGE_ANIMS_COLUMN1_COLUMN1_SUPERCRACK_RED", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_FBOMB_SMALL_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1016, "IMAGE_ANIMS_COLUMN1_FBOMB_SMALL_0_ICECHUNK", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_FBOMB_SMALL_1_TWIRL_SOFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1017, "IMAGE_ANIMS_COLUMN1_FBOMB_SMALL_1_TWIRL_SOFT", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_SHATTERLEFT_SMALL_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1018, "IMAGE_ANIMS_COLUMN1_SHATTERLEFT_SMALL_0_ICECHUNK", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_SHATTERRIGHT_SMALL_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1019, "IMAGE_ANIMS_COLUMN1_SHATTERRIGHT_SMALL_0_ICECHUNK", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_SNOWCRUSH_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1020, "IMAGE_ANIMS_COLUMN1_SNOWCRUSH_0_ICECHUNK", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_AQUAFRESH = GlobalMembersResourcesWP.GetImageThrow(theManager, 1021, "IMAGE_ANIMS_COLUMN2_AQUAFRESH", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_AQUAFRESH2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1022, "IMAGE_ANIMS_COLUMN2_AQUAFRESH2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_AQUAFRESHRED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1023, "IMAGE_ANIMS_COLUMN2_AQUAFRESHRED", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1024, "IMAGE_ANIMS_COLUMN2_COLUMN2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK01 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1025, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK01", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK02 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1026, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK02", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK03 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1027, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK03", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK04 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1028, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK04", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK05 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1029, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK05", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK06 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1030, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK06", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK07 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1031, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK07", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK08 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1032, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK08", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK09 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1033, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK09", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1034, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1035, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK11", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1036, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK12", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1037, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK13", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1038, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK14", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1039, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK15", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK16 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1040, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK16", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1041, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK17", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK18 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1042, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK18", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1043, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK19", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1044, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK20", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1045, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK21", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1046, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK22", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK23 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1047, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK23", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK24 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1048, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK24", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1049, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK25", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1050, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK26", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK27 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1051, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK27", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK28 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1052, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK28", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1053, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK29", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1054, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK30", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK1A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1055, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK1A", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK1B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1056, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK1B", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK2A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1057, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK2A", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK2B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1058, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK2B", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK3A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1059, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK3A", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK3B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1060, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK3B", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK4A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1061, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK4A", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK4B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1062, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK4B", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK5A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1063, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK5A", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK5B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1064, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK5B", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1065, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH1", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1066, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1067, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1068, "IMAGE_ANIMS_COLUMN2_COLUMN2_GLOW", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_GLOW_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1069, "IMAGE_ANIMS_COLUMN2_COLUMN2_GLOW_RED", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_METER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1070, "IMAGE_ANIMS_COLUMN2_COLUMN2_METER", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_METER_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1071, "IMAGE_ANIMS_COLUMN2_COLUMN2_METER_RED", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_PULSE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1072, "IMAGE_ANIMS_COLUMN2_COLUMN2_PULSE", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_SUPERCRACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1073, "IMAGE_ANIMS_COLUMN2_COLUMN2_SUPERCRACK", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_SUPERCRACK_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1074, "IMAGE_ANIMS_COLUMN2_COLUMN2_SUPERCRACK_RED", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_FBOMB_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1075, "IMAGE_ANIMS_COLUMN2_FBOMB_0_ICECHUNK", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_FBOMB_1_TWIRL_SOFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1076, "IMAGE_ANIMS_COLUMN2_FBOMB_1_TWIRL_SOFT", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_SHATTERLEFT_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1077, "IMAGE_ANIMS_COLUMN2_SHATTERLEFT_0_ICECHUNK", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_SHATTERRIGHT_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1078, "IMAGE_ANIMS_COLUMN2_SHATTERRIGHT_0_ICECHUNK", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_SNOWCRUSH_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1079, "IMAGE_ANIMS_COLUMN2_SNOWCRUSH_0_ICECHUNK", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_FROSTPANIC_FROSTPANIC_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1080, "IMAGE_ANIMS_FROSTPANIC_FROSTPANIC_RED", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_FROSTPANIC_FROSTPANIC_SKULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1081, "IMAGE_ANIMS_FROSTPANIC_FROSTPANIC_SKULL", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_FROST_BOTTOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1082, "IMAGE_QUEST_INFERNO_LAVA_FROST_BOTTOM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_FROST_LOSE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1083, "IMAGE_QUEST_INFERNO_LAVA_FROST_LOSE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_FROST_TOP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1084, "IMAGE_QUEST_INFERNO_LAVA_FROST_TOP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_FROST_TOP_UNDER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1085, "IMAGE_QUEST_INFERNO_LAVA_FROST_TOP_UNDER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1086, "IMAGE_QUEST_INFERNO_LAVA_ICECHUNK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_MOUNTAINDOUBLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1087, "IMAGE_QUEST_INFERNO_LAVA_MOUNTAINDOUBLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_MOUNTAINSINGLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1088, "IMAGE_QUEST_INFERNO_LAVA_MOUNTAINSINGLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_SNOWFLAKE_PARTICLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1089, "IMAGE_QUEST_INFERNO_LAVA_SNOWFLAKE_PARTICLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_UI_TOP_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 1090, "IMAGE_QUEST_INFERNO_LAVA_UI_TOP_FRAME", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Inferno_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_INFERNO_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 51, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_INFERNO_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_ICE_BOTTOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 955, "IMAGE_INGAMEUI_ICE_STORM_ICE_BOTTOM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_ICE_LIQUID = GlobalMembersResourcesWP.GetImageThrow(theManager, 956, "IMAGE_INGAMEUI_ICE_STORM_ICE_LIQUID", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_ICE_METER = GlobalMembersResourcesWP.GetImageThrow(theManager, 957, "IMAGE_INGAMEUI_ICE_STORM_ICE_METER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_ICE_METER_ICE = GlobalMembersResourcesWP.GetImageThrow(theManager, 958, "IMAGE_INGAMEUI_ICE_STORM_ICE_METER_ICE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_ICE_METER_PIPE = GlobalMembersResourcesWP.GetImageThrow(theManager, 959, "IMAGE_INGAMEUI_ICE_STORM_ICE_METER_PIPE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_MULTIPLIER = GlobalMembersResourcesWP.GetImageThrow(theManager, 960, "IMAGE_INGAMEUI_ICE_STORM_MULTIPLIER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_ICE_STORM_TOP_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 961, "IMAGE_INGAMEUI_ICE_STORM_TOP_FRAME", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_AQUAFRESH = GlobalMembersResourcesWP.GetImageThrow(theManager, 962, "IMAGE_ANIMS_COLUMN1_AQUAFRESH", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_AQUAFRESH2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 963, "IMAGE_ANIMS_COLUMN1_AQUAFRESH2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_AQUAFRESHRED = GlobalMembersResourcesWP.GetImageThrow(theManager, 964, "IMAGE_ANIMS_COLUMN1_AQUAFRESHRED", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 965, "IMAGE_ANIMS_COLUMN1_COLUMN1", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK01 = GlobalMembersResourcesWP.GetImageThrow(theManager, 966, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK01", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK02 = GlobalMembersResourcesWP.GetImageThrow(theManager, 967, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK02", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK03 = GlobalMembersResourcesWP.GetImageThrow(theManager, 968, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK03", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK04 = GlobalMembersResourcesWP.GetImageThrow(theManager, 969, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK04", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK05 = GlobalMembersResourcesWP.GetImageThrow(theManager, 970, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK05", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK06 = GlobalMembersResourcesWP.GetImageThrow(theManager, 971, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK06", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK07 = GlobalMembersResourcesWP.GetImageThrow(theManager, 972, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK07", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK08 = GlobalMembersResourcesWP.GetImageThrow(theManager, 973, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK08", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK09 = GlobalMembersResourcesWP.GetImageThrow(theManager, 974, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK09", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 975, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 976, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK11", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 977, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK12", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 978, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK13", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 979, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK14", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 980, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK15", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK16 = GlobalMembersResourcesWP.GetImageThrow(theManager, 981, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK16", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 982, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK17", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK18 = GlobalMembersResourcesWP.GetImageThrow(theManager, 983, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK18", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 984, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK19", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 985, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK20", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 986, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK21", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 987, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK22", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK23 = GlobalMembersResourcesWP.GetImageThrow(theManager, 988, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK23", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK24 = GlobalMembersResourcesWP.GetImageThrow(theManager, 989, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK24", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 990, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK25", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 991, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK26", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK27 = GlobalMembersResourcesWP.GetImageThrow(theManager, 992, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK27", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK28 = GlobalMembersResourcesWP.GetImageThrow(theManager, 993, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK28", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 994, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK29", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 995, "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK30", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK1A = GlobalMembersResourcesWP.GetImageThrow(theManager, 996, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK1A", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK1B = GlobalMembersResourcesWP.GetImageThrow(theManager, 997, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK1B", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK2A = GlobalMembersResourcesWP.GetImageThrow(theManager, 998, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK2A", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK2B = GlobalMembersResourcesWP.GetImageThrow(theManager, 999, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK2B", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK3A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1000, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK3A", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK3B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1001, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK3B", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK4A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1002, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK4A", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK4B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1003, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK4B", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK5A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1004, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK5A", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK5B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1005, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK5B", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1006, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH1", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1007, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1008, "IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1009, "IMAGE_ANIMS_COLUMN1_COLUMN1_GLOW", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_GLOW_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1010, "IMAGE_ANIMS_COLUMN1_COLUMN1_GLOW_RED", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_METER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1011, "IMAGE_ANIMS_COLUMN1_COLUMN1_METER", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_METER_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1012, "IMAGE_ANIMS_COLUMN1_COLUMN1_METER_RED", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_PULSE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1013, "IMAGE_ANIMS_COLUMN1_COLUMN1_PULSE", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_SUPERCRACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1014, "IMAGE_ANIMS_COLUMN1_COLUMN1_SUPERCRACK", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_COLUMN1_SUPERCRACK_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1015, "IMAGE_ANIMS_COLUMN1_COLUMN1_SUPERCRACK_RED", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_FBOMB_SMALL_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1016, "IMAGE_ANIMS_COLUMN1_FBOMB_SMALL_0_ICECHUNK", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_FBOMB_SMALL_1_TWIRL_SOFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1017, "IMAGE_ANIMS_COLUMN1_FBOMB_SMALL_1_TWIRL_SOFT", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_SHATTERLEFT_SMALL_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1018, "IMAGE_ANIMS_COLUMN1_SHATTERLEFT_SMALL_0_ICECHUNK", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_SHATTERRIGHT_SMALL_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1019, "IMAGE_ANIMS_COLUMN1_SHATTERRIGHT_SMALL_0_ICECHUNK", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN1_SNOWCRUSH_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1020, "IMAGE_ANIMS_COLUMN1_SNOWCRUSH_0_ICECHUNK", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_AQUAFRESH = GlobalMembersResourcesWP.GetImageThrow(theManager, 1021, "IMAGE_ANIMS_COLUMN2_AQUAFRESH", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_AQUAFRESH2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1022, "IMAGE_ANIMS_COLUMN2_AQUAFRESH2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_AQUAFRESHRED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1023, "IMAGE_ANIMS_COLUMN2_AQUAFRESHRED", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1024, "IMAGE_ANIMS_COLUMN2_COLUMN2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK01 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1025, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK01", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK02 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1026, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK02", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK03 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1027, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK03", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK04 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1028, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK04", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK05 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1029, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK05", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK06 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1030, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK06", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK07 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1031, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK07", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK08 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1032, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK08", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK09 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1033, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK09", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1034, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1035, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK11", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1036, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK12", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1037, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK13", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1038, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK14", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1039, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK15", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK16 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1040, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK16", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1041, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK17", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK18 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1042, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK18", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1043, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK19", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1044, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK20", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1045, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK21", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1046, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK22", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK23 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1047, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK23", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK24 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1048, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK24", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1049, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK25", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1050, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK26", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK27 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1051, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK27", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK28 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1052, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK28", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1053, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK29", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1054, "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK30", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK1A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1055, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK1A", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK1B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1056, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK1B", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK2A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1057, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK2A", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK2B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1058, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK2B", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK3A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1059, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK3A", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK3B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1060, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK3B", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK4A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1061, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK4A", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK4B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1062, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK4B", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK5A = GlobalMembersResourcesWP.GetImageThrow(theManager, 1063, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK5A", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK5B = GlobalMembersResourcesWP.GetImageThrow(theManager, 1064, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK5B", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1065, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH1", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1066, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1067, "IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1068, "IMAGE_ANIMS_COLUMN2_COLUMN2_GLOW", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_GLOW_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1069, "IMAGE_ANIMS_COLUMN2_COLUMN2_GLOW_RED", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_METER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1070, "IMAGE_ANIMS_COLUMN2_COLUMN2_METER", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_METER_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1071, "IMAGE_ANIMS_COLUMN2_COLUMN2_METER_RED", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_PULSE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1072, "IMAGE_ANIMS_COLUMN2_COLUMN2_PULSE", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_SUPERCRACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1073, "IMAGE_ANIMS_COLUMN2_COLUMN2_SUPERCRACK", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_COLUMN2_SUPERCRACK_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1074, "IMAGE_ANIMS_COLUMN2_COLUMN2_SUPERCRACK_RED", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_FBOMB_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1075, "IMAGE_ANIMS_COLUMN2_FBOMB_0_ICECHUNK", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_FBOMB_1_TWIRL_SOFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1076, "IMAGE_ANIMS_COLUMN2_FBOMB_1_TWIRL_SOFT", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_SHATTERLEFT_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1077, "IMAGE_ANIMS_COLUMN2_SHATTERLEFT_0_ICECHUNK", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_SHATTERRIGHT_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1078, "IMAGE_ANIMS_COLUMN2_SHATTERRIGHT_0_ICECHUNK", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_COLUMN2_SNOWCRUSH_0_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1079, "IMAGE_ANIMS_COLUMN2_SNOWCRUSH_0_ICECHUNK", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_FROSTPANIC_FROSTPANIC_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1080, "IMAGE_ANIMS_FROSTPANIC_FROSTPANIC_RED", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_FROSTPANIC_FROSTPANIC_SKULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1081, "IMAGE_ANIMS_FROSTPANIC_FROSTPANIC_SKULL", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_FROST_BOTTOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1082, "IMAGE_QUEST_INFERNO_LAVA_FROST_BOTTOM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_FROST_LOSE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1083, "IMAGE_QUEST_INFERNO_LAVA_FROST_LOSE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_FROST_TOP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1084, "IMAGE_QUEST_INFERNO_LAVA_FROST_TOP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_FROST_TOP_UNDER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1085, "IMAGE_QUEST_INFERNO_LAVA_FROST_TOP_UNDER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_ICECHUNK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1086, "IMAGE_QUEST_INFERNO_LAVA_ICECHUNK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_MOUNTAINDOUBLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1087, "IMAGE_QUEST_INFERNO_LAVA_MOUNTAINDOUBLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_MOUNTAINSINGLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1088, "IMAGE_QUEST_INFERNO_LAVA_MOUNTAINSINGLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_SNOWFLAKE_PARTICLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1089, "IMAGE_QUEST_INFERNO_LAVA_SNOWFLAKE_PARTICLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_LAVA_UI_TOP_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 1090, "IMAGE_QUEST_INFERNO_LAVA_UI_TOP_FRAME", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Inferno_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.POPANIM_ANIMS_COLUMN1 = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1555, "POPANIM_ANIMS_COLUMN1", 0, 0);
				GlobalMembersResourcesWP.POPANIM_ANIMS_COLUMN2 = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1556, "POPANIM_ANIMS_COLUMN2", 0, 0);
				GlobalMembersResourcesWP.POPANIM_ANIMS_FROSTPANIC = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1557, "POPANIM_ANIMS_FROSTPANIC", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_LightningResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Lightning_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Lightning_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Lightning_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_LIGHTNING_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 52, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_LIGHTNING_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 888, "IMAGE_LIGHTNING_GEMNUMS_RED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_WHITE = GlobalMembersResourcesWP.GetImageThrow(theManager, 889, "IMAGE_LIGHTNING_GEMNUMS_WHITE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_GREEN = GlobalMembersResourcesWP.GetImageThrow(theManager, 890, "IMAGE_LIGHTNING_GEMNUMS_GREEN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_YELLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 891, "IMAGE_LIGHTNING_GEMNUMS_YELLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_PURPLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 892, "IMAGE_LIGHTNING_GEMNUMS_PURPLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_ORANGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 893, "IMAGE_LIGHTNING_GEMNUMS_ORANGE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_BLUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 894, "IMAGE_LIGHTNING_GEMNUMS_BLUE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_CLEAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 895, "IMAGE_LIGHTNING_GEMNUMS_CLEAR", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GEMOUTLINES = GlobalMembersResourcesWP.GetImageThrow(theManager, 896, "IMAGE_GEMOUTLINES", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_BOARD_SEPARATOR_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 897, "IMAGE_INGAMEUI_LIGHTNING_BOARD_SEPARATOR_FRAME", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER = GlobalMembersResourcesWP.GetImageThrow(theManager, 898, "IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_MULTIPLIER = GlobalMembersResourcesWP.GetImageThrow(theManager, 899, "IMAGE_INGAMEUI_LIGHTNING_MULTIPLIER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 900, "IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 901, "IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_TIMER = GlobalMembersResourcesWP.GetImageThrow(theManager, 902, "IMAGE_INGAMEUI_LIGHTNING_TIMER", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Lightning_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_LIGHTNING_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 53, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_LIGHTNING_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_RED = GlobalMembersResourcesWP.GetImageThrow(theManager, 888, "IMAGE_LIGHTNING_GEMNUMS_RED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_WHITE = GlobalMembersResourcesWP.GetImageThrow(theManager, 889, "IMAGE_LIGHTNING_GEMNUMS_WHITE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_GREEN = GlobalMembersResourcesWP.GetImageThrow(theManager, 890, "IMAGE_LIGHTNING_GEMNUMS_GREEN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_YELLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 891, "IMAGE_LIGHTNING_GEMNUMS_YELLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_PURPLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 892, "IMAGE_LIGHTNING_GEMNUMS_PURPLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_ORANGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 893, "IMAGE_LIGHTNING_GEMNUMS_ORANGE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_BLUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 894, "IMAGE_LIGHTNING_GEMNUMS_BLUE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LIGHTNING_GEMNUMS_CLEAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 895, "IMAGE_LIGHTNING_GEMNUMS_CLEAR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GEMOUTLINES = GlobalMembersResourcesWP.GetImageThrow(theManager, 896, "IMAGE_GEMOUTLINES", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_BOARD_SEPARATOR_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 897, "IMAGE_INGAMEUI_LIGHTNING_BOARD_SEPARATOR_FRAME", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER = GlobalMembersResourcesWP.GetImageThrow(theManager, 898, "IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_MULTIPLIER = GlobalMembersResourcesWP.GetImageThrow(theManager, 899, "IMAGE_INGAMEUI_LIGHTNING_MULTIPLIER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 900, "IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 901, "IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAMEUI_LIGHTNING_TIMER = GlobalMembersResourcesWP.GetImageThrow(theManager, 902, "IMAGE_INGAMEUI_LIGHTNING_TIMER", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_PokerResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Poker_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Poker_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Poker_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_POKER_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 54, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_POKER_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAME_POKER_BOARD_SEPERATOR_FRAME_BOTTOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1111, "IMAGE_INGAME_POKER_BOARD_SEPERATOR_FRAME_BOTTOM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAME_POKER_BOARD_SEPERATOR_FRAME_TOP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1112, "IMAGE_INGAME_POKER_BOARD_SEPERATOR_FRAME_TOP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAME_POKER_HAND = GlobalMembersResourcesWP.GetImageThrow(theManager, 1113, "IMAGE_INGAME_POKER_HAND", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAME_POKER_HAND_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1114, "IMAGE_INGAME_POKER_HAND_GLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_INGAME_POKER_HAND_SKULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1115, "IMAGE_INGAME_POKER_HAND_SKULL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_BAR_SKULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1116, "IMAGE_POKER_BAR_SKULL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_BKG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1117, "IMAGE_POKER_BKG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_DECK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1118, "IMAGE_POKER_DECK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_DECK_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1119, "IMAGE_POKER_DECK_SHADOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_LARGE_SKULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1120, "IMAGE_POKER_LARGE_SKULL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_LIGHT_LIT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1121, "IMAGE_POKER_LIGHT_LIT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_LIGHT_UNLIT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1122, "IMAGE_POKER_LIGHT_UNLIT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_LONG_BKG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1123, "IMAGE_POKER_LONG_BKG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SCORE_BKG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1124, "IMAGE_POKER_SCORE_BKG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SCORE_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1125, "IMAGE_POKER_SCORE_GLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1126, "IMAGE_POKER_SKULL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL_BAR_COVER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1127, "IMAGE_POKER_SKULL_BAR_COVER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL_CRUSHER_BAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1128, "IMAGE_POKER_SKULL_CRUSHER_BAR", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL_CRUSHER_BKG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1129, "IMAGE_POKER_SKULL_CRUSHER_BKG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL_CRUSHER_BORDER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1130, "IMAGE_POKER_SKULL_CRUSHER_BORDER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL_CRUSHER_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1131, "IMAGE_POKER_SKULL_CRUSHER_GLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL_SLASH = GlobalMembersResourcesWP.GetImageThrow(theManager, 1132, "IMAGE_POKER_SKULL_SLASH", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SLASH_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1133, "IMAGE_POKER_SLASH_SHADOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_BACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1134, "IMAGE_CARDS_BACK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_DECK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1135, "IMAGE_CARDS_DECK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_DECK_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1136, "IMAGE_CARDS_DECK_SHADOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_FACE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1137, "IMAGE_CARDS_FACE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_FRONT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1138, "IMAGE_CARDS_FRONT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1139, "IMAGE_CARDS_SHADOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_SMALL_FACE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1140, "IMAGE_CARDS_SMALL_FACE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_WILD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1141, "IMAGE_CARDS_WILD", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SKULL_COIN_SET1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1142, "IMAGE_SKULL_COIN_SET1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SKULL_COIN_SET2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1143, "IMAGE_SKULL_COIN_SET2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SKULL_COIN_SET3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1144, "IMAGE_SKULL_COIN_SET3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SKULL_COIN_SET4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1145, "IMAGE_SKULL_COIN_SET4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SKULL_COIN_SIDE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1146, "IMAGE_SKULL_COIN_SIDE", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Poker_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_POKER_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 55, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_POKER_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAME_POKER_BOARD_SEPERATOR_FRAME_BOTTOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1111, "IMAGE_INGAME_POKER_BOARD_SEPERATOR_FRAME_BOTTOM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAME_POKER_BOARD_SEPERATOR_FRAME_TOP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1112, "IMAGE_INGAME_POKER_BOARD_SEPERATOR_FRAME_TOP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAME_POKER_HAND = GlobalMembersResourcesWP.GetImageThrow(theManager, 1113, "IMAGE_INGAME_POKER_HAND", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAME_POKER_HAND_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1114, "IMAGE_INGAME_POKER_HAND_GLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_INGAME_POKER_HAND_SKULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1115, "IMAGE_INGAME_POKER_HAND_SKULL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_BAR_SKULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1116, "IMAGE_POKER_BAR_SKULL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_BKG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1117, "IMAGE_POKER_BKG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_DECK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1118, "IMAGE_POKER_DECK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_DECK_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1119, "IMAGE_POKER_DECK_SHADOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_LARGE_SKULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1120, "IMAGE_POKER_LARGE_SKULL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_LIGHT_LIT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1121, "IMAGE_POKER_LIGHT_LIT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_LIGHT_UNLIT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1122, "IMAGE_POKER_LIGHT_UNLIT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_LONG_BKG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1123, "IMAGE_POKER_LONG_BKG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SCORE_BKG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1124, "IMAGE_POKER_SCORE_BKG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SCORE_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1125, "IMAGE_POKER_SCORE_GLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1126, "IMAGE_POKER_SKULL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL_BAR_COVER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1127, "IMAGE_POKER_SKULL_BAR_COVER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL_CRUSHER_BAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1128, "IMAGE_POKER_SKULL_CRUSHER_BAR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL_CRUSHER_BKG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1129, "IMAGE_POKER_SKULL_CRUSHER_BKG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL_CRUSHER_BORDER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1130, "IMAGE_POKER_SKULL_CRUSHER_BORDER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL_CRUSHER_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1131, "IMAGE_POKER_SKULL_CRUSHER_GLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SKULL_SLASH = GlobalMembersResourcesWP.GetImageThrow(theManager, 1132, "IMAGE_POKER_SKULL_SLASH", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SLASH_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1133, "IMAGE_POKER_SLASH_SHADOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_BACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1134, "IMAGE_CARDS_BACK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_DECK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1135, "IMAGE_CARDS_DECK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_DECK_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1136, "IMAGE_CARDS_DECK_SHADOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_FACE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1137, "IMAGE_CARDS_FACE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_FRONT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1138, "IMAGE_CARDS_FRONT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_SHADOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1139, "IMAGE_CARDS_SHADOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_SMALL_FACE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1140, "IMAGE_CARDS_SMALL_FACE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CARDS_WILD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1141, "IMAGE_CARDS_WILD", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SKULL_COIN_SET1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1142, "IMAGE_SKULL_COIN_SET1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SKULL_COIN_SET2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1143, "IMAGE_SKULL_COIN_SET2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SKULL_COIN_SET3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1144, "IMAGE_SKULL_COIN_SET3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SKULL_COIN_SET4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1145, "IMAGE_SKULL_COIN_SET4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SKULL_COIN_SIDE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1146, "IMAGE_SKULL_COIN_SIDE", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_TimeBombResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_TimeBomb_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_TimeBomb_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_TimeBomb_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_TIMEBOMB_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 56, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_TIMEBOMB_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BOMBGEMS = GlobalMembersResourcesWP.GetImageThrow(theManager, 1108, "IMAGE_BOMBGEMS", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BOMBGLOWS_DANGERGLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1109, "IMAGE_BOMBGLOWS_DANGERGLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BOMBGLOWS_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1110, "IMAGE_BOMBGLOWS_GLOW", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_TimeBomb_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_TIMEBOMB_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 57, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_TIMEBOMB_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BOMBGEMS = GlobalMembersResourcesWP.GetImageThrow(theManager, 1108, "IMAGE_BOMBGEMS", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BOMBGLOWS_DANGERGLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1109, "IMAGE_BOMBGLOWS_DANGERGLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BOMBGLOWS_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1110, "IMAGE_BOMBGLOWS_GLOW", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_TimeLimitResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_TimeLimit_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_TimeLimit_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_TimeLimit_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_QUEST_LEAD2GOLD_BLACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1297, "IMAGE_QUEST_LEAD2GOLD_BLACK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_LEAD2GOLD_LEAD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1298, "IMAGE_QUEST_LEAD2GOLD_LEAD", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_LEAD2GOLD_GOLD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1299, "IMAGE_QUEST_LEAD2GOLD_GOLD", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_TimeLimit_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_QUEST_LEAD2GOLD_BLACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1297, "IMAGE_QUEST_LEAD2GOLD_BLACK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_LEAD2GOLD_LEAD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1298, "IMAGE_QUEST_LEAD2GOLD_LEAD", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_LEAD2GOLD_GOLD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1299, "IMAGE_QUEST_LEAD2GOLD_GOLD", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_WallblastResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Wallblast_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGamePlayQuest_Wallblast_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Wallblast_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_WALLBLAST_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 58, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_WALLBLAST_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_WALLBLAST_BOARD_MASK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1319, "IMAGE_QUEST_WALLBLAST_BOARD_MASK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_WALLBLAST_BOARD_WALL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1320, "IMAGE_QUEST_WALLBLAST_BOARD_WALL", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGamePlayQuest_Wallblast_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GAMEPLAYQUEST_WALLBLAST_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 59, "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_WALLBLAST_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_WALLBLAST_BOARD_MASK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1319, "IMAGE_QUEST_WALLBLAST_BOARD_MASK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_WALLBLAST_BOARD_WALL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1320, "IMAGE_QUEST_WALLBLAST_BOARD_WALL", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGiftGameResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractGiftGame_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractGiftGame_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGiftGame_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GIFTGAME_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 60, "ATLASIMAGE_ATLAS_GIFTGAME_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GIFTTHEGAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 696, "IMAGE_GIFTTHEGAME", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractGiftGame_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_GIFTGAME_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 61, "ATLASIMAGE_ATLAS_GIFTGAME_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GIFTTHEGAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 696, "IMAGE_GIFTTHEGAME", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_BasicResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractHelp_Basic_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractHelp_Basic_960Resources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractHelp_Basic_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Basic_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_BASIC_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 62, "ATLASIMAGE_ATLAS_HELP_BASIC_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 211, "IMAGE_HELP_SWAP3_SWAP3_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 212, "IMAGE_HELP_SWAP3_SWAP3_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 213, "IMAGE_HELP_SWAP3_SWAP3_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 214, "IMAGE_HELP_SWAP3_SWAP3_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 215, "IMAGE_HELP_SWAP3_SWAP3_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_384X384 = GlobalMembersResourcesWP.GetImageThrow(theManager, 216, "IMAGE_HELP_SWAP3_SWAP3_384X384", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 217, "IMAGE_HELP_SWAP3_SWAP3_50X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 218, "IMAGE_HELP_MATCH4_MATCH4_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 219, "IMAGE_HELP_MATCH4_MATCH4_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 220, "IMAGE_HELP_MATCH4_MATCH4_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 221, "IMAGE_HELP_MATCH4_MATCH4_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 222, "IMAGE_HELP_MATCH4_MATCH4_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 223, "IMAGE_HELP_MATCH4_MATCH4_128X128_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 224, "IMAGE_HELP_MATCH4_MATCH4_128X128_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_291X384 = GlobalMembersResourcesWP.GetImageThrow(theManager, 225, "IMAGE_HELP_MATCH4_MATCH4_291X384", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 226, "IMAGE_HELP_MATCH4_MATCH4_50X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 227, "IMAGE_HELP_STARGEM_STARGEM_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 228, "IMAGE_HELP_STARGEM_STARGEM_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 229, "IMAGE_HELP_STARGEM_STARGEM_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 230, "IMAGE_HELP_STARGEM_STARGEM_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 231, "IMAGE_HELP_STARGEM_STARGEM_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 232, "IMAGE_HELP_STARGEM_STARGEM_128X128_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 233, "IMAGE_HELP_STARGEM_STARGEM_128X128_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 234, "IMAGE_HELP_STARGEM_STARGEM_128X128_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_291X384 = GlobalMembersResourcesWP.GetImageThrow(theManager, 235, "IMAGE_HELP_STARGEM_STARGEM_291X384", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 236, "IMAGE_HELP_STARGEM_STARGEM_50X43", 480, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Basic_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_BASIC_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 63, "ATLASIMAGE_ATLAS_HELP_BASIC_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 211, "IMAGE_HELP_SWAP3_SWAP3_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 212, "IMAGE_HELP_SWAP3_SWAP3_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 213, "IMAGE_HELP_SWAP3_SWAP3_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 214, "IMAGE_HELP_SWAP3_SWAP3_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 215, "IMAGE_HELP_SWAP3_SWAP3_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_384X384 = GlobalMembersResourcesWP.GetImageThrow(theManager, 216, "IMAGE_HELP_SWAP3_SWAP3_384X384", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SWAP3_SWAP3_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 217, "IMAGE_HELP_SWAP3_SWAP3_50X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 218, "IMAGE_HELP_MATCH4_MATCH4_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 219, "IMAGE_HELP_MATCH4_MATCH4_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 220, "IMAGE_HELP_MATCH4_MATCH4_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 221, "IMAGE_HELP_MATCH4_MATCH4_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 222, "IMAGE_HELP_MATCH4_MATCH4_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 223, "IMAGE_HELP_MATCH4_MATCH4_128X128_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 224, "IMAGE_HELP_MATCH4_MATCH4_128X128_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_291X384 = GlobalMembersResourcesWP.GetImageThrow(theManager, 225, "IMAGE_HELP_MATCH4_MATCH4_291X384", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_MATCH4_MATCH4_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 226, "IMAGE_HELP_MATCH4_MATCH4_50X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 227, "IMAGE_HELP_STARGEM_STARGEM_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 228, "IMAGE_HELP_STARGEM_STARGEM_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 229, "IMAGE_HELP_STARGEM_STARGEM_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 230, "IMAGE_HELP_STARGEM_STARGEM_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 231, "IMAGE_HELP_STARGEM_STARGEM_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 232, "IMAGE_HELP_STARGEM_STARGEM_128X128_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 233, "IMAGE_HELP_STARGEM_STARGEM_128X128_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 234, "IMAGE_HELP_STARGEM_STARGEM_128X128_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_291X384 = GlobalMembersResourcesWP.GetImageThrow(theManager, 235, "IMAGE_HELP_STARGEM_STARGEM_291X384", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_STARGEM_STARGEM_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 236, "IMAGE_HELP_STARGEM_STARGEM_50X43", 960, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Basic_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.POPANIM_HELP_SWAP3 = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1531, "POPANIM_HELP_SWAP3", 0, 0);
				GlobalMembersResourcesWP.POPANIM_HELP_MATCH4 = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1532, "POPANIM_HELP_MATCH4", 0, 0);
				GlobalMembersResourcesWP.POPANIM_HELP_STARGEM = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1533, "POPANIM_HELP_STARGEM", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_BflyResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractHelp_Bfly_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractHelp_Bfly_960Resources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractHelp_Bfly_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Bfly_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_BFLY_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 64, "ATLASIMAGE_ATLAS_HELP_BFLY_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 467, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 468, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 469, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 470, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 471, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 472, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_384X384 = GlobalMembersResourcesWP.GetImageThrow(theManager, 473, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_384X384", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_390X519 = GlobalMembersResourcesWP.GetImageThrow(theManager, 474, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_390X519", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_42X88 = GlobalMembersResourcesWP.GetImageThrow(theManager, 475, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_42X88", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 476, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_50X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_52X117 = GlobalMembersResourcesWP.GetImageThrow(theManager, 477, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_52X117", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_53X95 = GlobalMembersResourcesWP.GetImageThrow(theManager, 478, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_53X95", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 479, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 480, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 481, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 482, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 483, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 484, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 485, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 486, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_389X70 = GlobalMembersResourcesWP.GetImageThrow(theManager, 487, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_389X70", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_390X519 = GlobalMembersResourcesWP.GetImageThrow(theManager, 488, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_390X519", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 489, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_50X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_82X164 = GlobalMembersResourcesWP.GetImageThrow(theManager, 490, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_82X164", 480, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Bfly_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_BFLY_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 65, "ATLASIMAGE_ATLAS_HELP_BFLY_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 467, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 468, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 469, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 470, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 471, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 472, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_384X384 = GlobalMembersResourcesWP.GetImageThrow(theManager, 473, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_384X384", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_390X519 = GlobalMembersResourcesWP.GetImageThrow(theManager, 474, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_390X519", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_42X88 = GlobalMembersResourcesWP.GetImageThrow(theManager, 475, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_42X88", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 476, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_50X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_52X117 = GlobalMembersResourcesWP.GetImageThrow(theManager, 477, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_52X117", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_53X95 = GlobalMembersResourcesWP.GetImageThrow(theManager, 478, "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_53X95", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 479, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 480, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 481, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 482, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 483, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 484, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 485, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 486, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_389X70 = GlobalMembersResourcesWP.GetImageThrow(theManager, 487, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_389X70", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_390X519 = GlobalMembersResourcesWP.GetImageThrow(theManager, 488, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_390X519", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 489, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_50X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_82X164 = GlobalMembersResourcesWP.GetImageThrow(theManager, 490, "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_82X164", 960, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Bfly_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.POPANIM_HELP_BFLY_MATCH = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1540, "POPANIM_HELP_BFLY_MATCH", 0, 0);
				GlobalMembersResourcesWP.POPANIM_HELP_BFLY_SPIDER = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1541, "POPANIM_HELP_BFLY_SPIDER", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_DiamondMineResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractHelp_DiamondMine_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractHelp_DiamondMine_960Resources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractHelp_DiamondMine_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_DiamondMine_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_DIAMONDMINE_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 66, "ATLASIMAGE_ATLAS_HELP_DIAMONDMINE_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_11X14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 237, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_11X14", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 238, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 239, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 240, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 241, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 242, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 243, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 244, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 245, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_13X15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 246, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_13X15", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_18X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 247, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_18X34", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_20X15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 248, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_20X15", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_22X22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 249, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_22X22", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_27X29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 250, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_27X29", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104 = GlobalMembersResourcesWP.GetImageThrow(theManager, 251, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 252, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 253, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_292X384 = GlobalMembersResourcesWP.GetImageThrow(theManager, 254, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_292X384", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_30X35 = GlobalMembersResourcesWP.GetImageThrow(theManager, 255, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_30X35", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_34X31 = GlobalMembersResourcesWP.GetImageThrow(theManager, 256, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_34X31", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 257, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_50X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 258, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 259, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 260, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 261, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 262, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 263, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 264, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 265, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_13X15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 266, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_13X15", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_20X15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 267, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_20X15", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_258X338 = GlobalMembersResourcesWP.GetImageThrow(theManager, 268, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_258X338", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 269, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_50X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 270, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X1", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 271, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 272, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X13", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 273, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X17", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 274, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X20", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 275, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X22", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 276, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X26", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X31 = GlobalMembersResourcesWP.GetImageThrow(theManager, 277, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X31", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 278, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X36", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X40 = GlobalMembersResourcesWP.GetImageThrow(theManager, 279, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X40", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X42 = GlobalMembersResourcesWP.GetImageThrow(theManager, 280, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X42", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X46 = GlobalMembersResourcesWP.GetImageThrow(theManager, 281, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X46", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X51 = GlobalMembersResourcesWP.GetImageThrow(theManager, 282, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X51", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X56 = GlobalMembersResourcesWP.GetImageThrow(theManager, 283, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X56", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66 = GlobalMembersResourcesWP.GetImageThrow(theManager, 284, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 285, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 286, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X71 = GlobalMembersResourcesWP.GetImageThrow(theManager, 287, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X71", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X74 = GlobalMembersResourcesWP.GetImageThrow(theManager, 288, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X74", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X81 = GlobalMembersResourcesWP.GetImageThrow(theManager, 289, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X81", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X81_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 290, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X81_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_83X81 = GlobalMembersResourcesWP.GetImageThrow(theManager, 291, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_83X81", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_83X81_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 292, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_83X81_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_11X14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 293, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_11X14", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 294, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 295, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 296, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 297, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 298, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 299, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 300, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_13X15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 301, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_13X15", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_20X15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 302, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_20X15", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_24X29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 303, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_24X29", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_27X26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 304, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_27X26", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_291X104 = GlobalMembersResourcesWP.GetImageThrow(theManager, 305, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_291X104", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_291X104_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 306, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_291X104_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_292X384 = GlobalMembersResourcesWP.GetImageThrow(theManager, 307, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_292X384", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 308, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_50X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_54X41 = GlobalMembersResourcesWP.GetImageThrow(theManager, 309, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_54X41", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_78X82 = GlobalMembersResourcesWP.GetImageThrow(theManager, 310, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_78X82", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_78X82_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 311, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_78X82_2", 480, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_DiamondMine_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_DIAMONDMINE_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 67, "ATLASIMAGE_ATLAS_HELP_DIAMONDMINE_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_11X14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 237, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_11X14", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 238, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 239, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 240, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 241, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 242, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 243, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 244, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 245, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_13X15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 246, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_13X15", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_18X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 247, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_18X34", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_20X15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 248, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_20X15", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_22X22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 249, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_22X22", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_27X29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 250, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_27X29", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104 = GlobalMembersResourcesWP.GetImageThrow(theManager, 251, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 252, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 253, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_292X384 = GlobalMembersResourcesWP.GetImageThrow(theManager, 254, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_292X384", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_30X35 = GlobalMembersResourcesWP.GetImageThrow(theManager, 255, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_30X35", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_34X31 = GlobalMembersResourcesWP.GetImageThrow(theManager, 256, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_34X31", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 257, "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_50X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 258, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 259, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 260, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 261, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 262, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 263, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 264, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 265, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_13X15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 266, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_13X15", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_20X15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 267, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_20X15", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_258X338 = GlobalMembersResourcesWP.GetImageThrow(theManager, 268, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_258X338", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 269, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_50X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 270, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X1", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 271, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 272, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X13", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 273, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X17", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 274, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X20", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 275, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X22", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 276, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X26", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X31 = GlobalMembersResourcesWP.GetImageThrow(theManager, 277, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X31", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 278, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X36", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X40 = GlobalMembersResourcesWP.GetImageThrow(theManager, 279, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X40", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X42 = GlobalMembersResourcesWP.GetImageThrow(theManager, 280, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X42", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X46 = GlobalMembersResourcesWP.GetImageThrow(theManager, 281, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X46", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X51 = GlobalMembersResourcesWP.GetImageThrow(theManager, 282, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X51", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X56 = GlobalMembersResourcesWP.GetImageThrow(theManager, 283, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X56", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66 = GlobalMembersResourcesWP.GetImageThrow(theManager, 284, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 285, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 286, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X71 = GlobalMembersResourcesWP.GetImageThrow(theManager, 287, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X71", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X74 = GlobalMembersResourcesWP.GetImageThrow(theManager, 288, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X74", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X81 = GlobalMembersResourcesWP.GetImageThrow(theManager, 289, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X81", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X81_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 290, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X81_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_83X81 = GlobalMembersResourcesWP.GetImageThrow(theManager, 291, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_83X81", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_83X81_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 292, "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_83X81_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_11X14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 293, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_11X14", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 294, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 295, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 296, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 297, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 298, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 299, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 300, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_13X15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 301, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_13X15", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_20X15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 302, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_20X15", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_24X29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 303, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_24X29", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_27X26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 304, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_27X26", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_291X104 = GlobalMembersResourcesWP.GetImageThrow(theManager, 305, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_291X104", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_291X104_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 306, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_291X104_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_292X384 = GlobalMembersResourcesWP.GetImageThrow(theManager, 307, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_292X384", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 308, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_50X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_54X41 = GlobalMembersResourcesWP.GetImageThrow(theManager, 309, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_54X41", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_78X82 = GlobalMembersResourcesWP.GetImageThrow(theManager, 310, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_78X82", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_78X82_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 311, "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_78X82_2", 960, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_DiamondMine_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.POPANIM_HELP_DIAMOND_MATCH = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1534, "POPANIM_HELP_DIAMOND_MATCH", 0, 0);
				GlobalMembersResourcesWP.POPANIM_HELP_DIAMOND_ADVANCE = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1535, "POPANIM_HELP_DIAMOND_ADVANCE", 0, 0);
				GlobalMembersResourcesWP.POPANIM_HELP_DIAMOND_GOLD = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1536, "POPANIM_HELP_DIAMOND_GOLD", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_IceStormResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractHelp_IceStorm_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractHelp_IceStorm_960Resources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractHelp_IceStorm_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_IceStorm_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_ICESTORM_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 68, "ATLASIMAGE_ATLAS_HELP_ICESTORM_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 491, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 492, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 493, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 494, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 495, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 496, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 497, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 498, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_393X502 = GlobalMembersResourcesWP.GetImageThrow(theManager, 499, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_393X502", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 500, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_50X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_88X278 = GlobalMembersResourcesWP.GetImageThrow(theManager, 501, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_88X278", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_106X234 = GlobalMembersResourcesWP.GetImageThrow(theManager, 502, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_106X234", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_111X55 = GlobalMembersResourcesWP.GetImageThrow(theManager, 503, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_111X55", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 504, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 505, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 506, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 507, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 508, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 509, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 510, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 511, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 512, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 513, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 514, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_11", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 515, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_12", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 516, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_13", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 517, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 518, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 519, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 520, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 521, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 522, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 523, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 524, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 525, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 526, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 527, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_394X502 = GlobalMembersResourcesWP.GetImageThrow(theManager, 528, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_394X502", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_85X68 = GlobalMembersResourcesWP.GetImageThrow(theManager, 529, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_85X68", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_8X10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 530, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_8X10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_118X44 = GlobalMembersResourcesWP.GetImageThrow(theManager, 531, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_118X44", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 532, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 533, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 534, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 535, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 536, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 537, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 538, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 539, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271 = GlobalMembersResourcesWP.GetImageThrow(theManager, 540, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 541, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 542, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 543, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 544, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 545, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 546, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 547, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 548, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 549, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 550, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_11", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 551, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_12", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 552, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_13", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 553, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_14", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 554, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_15", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_16 = GlobalMembersResourcesWP.GetImageThrow(theManager, 555, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_16", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 556, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_17", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_18 = GlobalMembersResourcesWP.GetImageThrow(theManager, 557, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_18", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 558, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_19", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 559, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_20", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 560, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_21", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 561, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_22", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_23 = GlobalMembersResourcesWP.GetImageThrow(theManager, 562, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_23", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_393X502 = GlobalMembersResourcesWP.GetImageThrow(theManager, 563, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_393X502", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 564, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_50X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_83X38 = GlobalMembersResourcesWP.GetImageThrow(theManager, 565, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_83X38", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_88X278 = GlobalMembersResourcesWP.GetImageThrow(theManager, 566, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_88X278", 480, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_IceStorm_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_ICESTORM_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 69, "ATLASIMAGE_ATLAS_HELP_ICESTORM_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 491, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 492, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 493, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 494, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 495, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 496, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 497, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 498, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_393X502 = GlobalMembersResourcesWP.GetImageThrow(theManager, 499, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_393X502", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 500, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_50X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_88X278 = GlobalMembersResourcesWP.GetImageThrow(theManager, 501, "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_88X278", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_106X234 = GlobalMembersResourcesWP.GetImageThrow(theManager, 502, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_106X234", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_111X55 = GlobalMembersResourcesWP.GetImageThrow(theManager, 503, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_111X55", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 504, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 505, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 506, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 507, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 508, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 509, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 510, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 511, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 512, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 513, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 514, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_11", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 515, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_12", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 516, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_13", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 517, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 518, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 519, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 520, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 521, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 522, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 523, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 524, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 525, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 526, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 527, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_394X502 = GlobalMembersResourcesWP.GetImageThrow(theManager, 528, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_394X502", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_85X68 = GlobalMembersResourcesWP.GetImageThrow(theManager, 529, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_85X68", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_8X10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 530, "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_8X10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_118X44 = GlobalMembersResourcesWP.GetImageThrow(theManager, 531, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_118X44", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 532, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 533, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 534, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 535, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 536, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 537, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 538, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 539, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271 = GlobalMembersResourcesWP.GetImageThrow(theManager, 540, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 541, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 542, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 543, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 544, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 545, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 546, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 547, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 548, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 549, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 550, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_11", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 551, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_12", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 552, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_13", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 553, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_14", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 554, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_15", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_16 = GlobalMembersResourcesWP.GetImageThrow(theManager, 555, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_16", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 556, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_17", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_18 = GlobalMembersResourcesWP.GetImageThrow(theManager, 557, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_18", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 558, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_19", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 559, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_20", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 560, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_21", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 561, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_22", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_23 = GlobalMembersResourcesWP.GetImageThrow(theManager, 562, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_23", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_393X502 = GlobalMembersResourcesWP.GetImageThrow(theManager, 563, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_393X502", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 564, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_50X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_83X38 = GlobalMembersResourcesWP.GetImageThrow(theManager, 565, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_83X38", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_88X278 = GlobalMembersResourcesWP.GetImageThrow(theManager, 566, "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_88X278", 960, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_IceStorm_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.POPANIM_HELP_ICESTORM_HORIZ = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1542, "POPANIM_HELP_ICESTORM_HORIZ", 0, 0);
				GlobalMembersResourcesWP.POPANIM_HELP_ICESTORM_METER = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1543, "POPANIM_HELP_ICESTORM_METER", 0, 0);
				GlobalMembersResourcesWP.POPANIM_HELP_ICESTORM_VERT = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1544, "POPANIM_HELP_ICESTORM_VERT", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_LightningResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractHelp_Lightning_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractHelp_Lightning_960Resources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractHelp_Lightning_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Lightning_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_LIGHTNING_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 70, "ATLASIMAGE_ATLAS_HELP_LIGHTNING_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 312, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 313, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 314, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 315, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 316, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 317, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 318, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_13X52 = GlobalMembersResourcesWP.GetImageThrow(theManager, 319, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_13X52", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_18X19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 320, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_18X19", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_19X25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 321, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_19X25", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 322, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 323, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 324, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 325, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 326, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 327, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 328, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_23X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 329, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_23X34", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_25X37 = GlobalMembersResourcesWP.GetImageThrow(theManager, 330, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_25X37", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_27X54 = GlobalMembersResourcesWP.GetImageThrow(theManager, 331, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_27X54", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_35X14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 332, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_35X14", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_383X504 = GlobalMembersResourcesWP.GetImageThrow(theManager, 333, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_383X504", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84 = GlobalMembersResourcesWP.GetImageThrow(theManager, 334, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 335, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 336, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 337, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 338, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 339, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 340, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 341, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_48X25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 342, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_48X25", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 343, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_50X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88 = GlobalMembersResourcesWP.GetImageThrow(theManager, 344, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 345, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 346, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 347, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_74X56 = GlobalMembersResourcesWP.GetImageThrow(theManager, 348, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_74X56", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90 = GlobalMembersResourcesWP.GetImageThrow(theManager, 349, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 350, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 351, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 352, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 353, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 354, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 355, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 356, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 357, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 358, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 359, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_11", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 360, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_12", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_93X62 = GlobalMembersResourcesWP.GetImageThrow(theManager, 361, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_93X62", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 362, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 363, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 364, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 365, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 366, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_13X52 = GlobalMembersResourcesWP.GetImageThrow(theManager, 367, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_13X52", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_147X93 = GlobalMembersResourcesWP.GetImageThrow(theManager, 368, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_147X93", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_161X68 = GlobalMembersResourcesWP.GetImageThrow(theManager, 369, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_161X68", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_168X72 = GlobalMembersResourcesWP.GetImageThrow(theManager, 370, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_168X72", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_168X79 = GlobalMembersResourcesWP.GetImageThrow(theManager, 371, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_168X79", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_18X19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 372, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_18X19", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_19X25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 373, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_19X25", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 374, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 375, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 376, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_23X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 377, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_23X34", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_25X37 = GlobalMembersResourcesWP.GetImageThrow(theManager, 378, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_25X37", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 379, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 380, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 381, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 382, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 383, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 384, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 385, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 386, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 387, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 388, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_27X54 = GlobalMembersResourcesWP.GetImageThrow(theManager, 389, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_27X54", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232 = GlobalMembersResourcesWP.GetImageThrow(theManager, 390, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 391, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 392, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 393, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 394, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_317X342 = GlobalMembersResourcesWP.GetImageThrow(theManager, 395, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_317X342", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_31X57 = GlobalMembersResourcesWP.GetImageThrow(theManager, 396, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_31X57", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_35X14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 397, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_35X14", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_383X504 = GlobalMembersResourcesWP.GetImageThrow(theManager, 398, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_383X504", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84 = GlobalMembersResourcesWP.GetImageThrow(theManager, 399, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 400, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 401, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 402, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 403, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 404, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 405, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 406, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_48X25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 407, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_48X25", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_49X69 = GlobalMembersResourcesWP.GetImageThrow(theManager, 408, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_49X69", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88 = GlobalMembersResourcesWP.GetImageThrow(theManager, 409, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 410, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 411, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 412, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_74X56 = GlobalMembersResourcesWP.GetImageThrow(theManager, 413, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_74X56", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_74X56_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 414, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_74X56_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_75X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 415, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_75X34", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_75X35 = GlobalMembersResourcesWP.GetImageThrow(theManager, 416, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_75X35", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_83X66 = GlobalMembersResourcesWP.GetImageThrow(theManager, 417, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_83X66", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_83X66_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 418, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_83X66_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_8X10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 419, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_8X10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_93X62 = GlobalMembersResourcesWP.GetImageThrow(theManager, 420, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_93X62", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 421, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 422, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 423, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 424, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_168X72 = GlobalMembersResourcesWP.GetImageThrow(theManager, 425, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_168X72", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_168X79 = GlobalMembersResourcesWP.GetImageThrow(theManager, 426, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_168X79", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 427, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 428, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 429, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 430, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 431, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 432, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 433, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 434, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 435, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 436, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 437, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 438, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 439, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 440, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 441, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 442, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 443, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 444, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 445, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 446, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 447, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 448, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 449, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 450, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_272X153 = GlobalMembersResourcesWP.GetImageThrow(theManager, 451, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_272X153", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_27X35 = GlobalMembersResourcesWP.GetImageThrow(theManager, 452, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_27X35", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_27X35_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 453, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_27X35_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_31X57 = GlobalMembersResourcesWP.GetImageThrow(theManager, 454, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_31X57", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_32X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 455, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_32X34", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_32X34_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 456, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_32X34_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_383X504 = GlobalMembersResourcesWP.GetImageThrow(theManager, 457, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_383X504", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_383X504_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 458, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_383X504_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 459, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 460, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 461, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 462, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 463, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_75X35 = GlobalMembersResourcesWP.GetImageThrow(theManager, 464, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_75X35", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_83X66 = GlobalMembersResourcesWP.GetImageThrow(theManager, 465, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_83X66", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_8X10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 466, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_8X10", 480, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Lightning_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_LIGHTNING_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 71, "ATLASIMAGE_ATLAS_HELP_LIGHTNING_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 312, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 313, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 314, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 315, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 316, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 317, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 318, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_13X52 = GlobalMembersResourcesWP.GetImageThrow(theManager, 319, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_13X52", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_18X19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 320, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_18X19", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_19X25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 321, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_19X25", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 322, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 323, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 324, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 325, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 326, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 327, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 328, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_23X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 329, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_23X34", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_25X37 = GlobalMembersResourcesWP.GetImageThrow(theManager, 330, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_25X37", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_27X54 = GlobalMembersResourcesWP.GetImageThrow(theManager, 331, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_27X54", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_35X14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 332, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_35X14", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_383X504 = GlobalMembersResourcesWP.GetImageThrow(theManager, 333, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_383X504", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84 = GlobalMembersResourcesWP.GetImageThrow(theManager, 334, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 335, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 336, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 337, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 338, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 339, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 340, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 341, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_48X25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 342, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_48X25", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 343, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_50X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88 = GlobalMembersResourcesWP.GetImageThrow(theManager, 344, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 345, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 346, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 347, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_74X56 = GlobalMembersResourcesWP.GetImageThrow(theManager, 348, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_74X56", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90 = GlobalMembersResourcesWP.GetImageThrow(theManager, 349, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 350, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 351, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 352, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 353, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 354, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 355, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 356, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 357, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 358, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 359, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_11", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 360, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_12", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_93X62 = GlobalMembersResourcesWP.GetImageThrow(theManager, 361, "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_93X62", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 362, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 363, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 364, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 365, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 366, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_13X52 = GlobalMembersResourcesWP.GetImageThrow(theManager, 367, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_13X52", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_147X93 = GlobalMembersResourcesWP.GetImageThrow(theManager, 368, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_147X93", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_161X68 = GlobalMembersResourcesWP.GetImageThrow(theManager, 369, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_161X68", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_168X72 = GlobalMembersResourcesWP.GetImageThrow(theManager, 370, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_168X72", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_168X79 = GlobalMembersResourcesWP.GetImageThrow(theManager, 371, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_168X79", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_18X19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 372, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_18X19", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_19X25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 373, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_19X25", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 374, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 375, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 376, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_23X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 377, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_23X34", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_25X37 = GlobalMembersResourcesWP.GetImageThrow(theManager, 378, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_25X37", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 379, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 380, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 381, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 382, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 383, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 384, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 385, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 386, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 387, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 388, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_27X54 = GlobalMembersResourcesWP.GetImageThrow(theManager, 389, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_27X54", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232 = GlobalMembersResourcesWP.GetImageThrow(theManager, 390, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 391, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 392, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 393, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 394, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_317X342 = GlobalMembersResourcesWP.GetImageThrow(theManager, 395, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_317X342", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_31X57 = GlobalMembersResourcesWP.GetImageThrow(theManager, 396, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_31X57", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_35X14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 397, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_35X14", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_383X504 = GlobalMembersResourcesWP.GetImageThrow(theManager, 398, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_383X504", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84 = GlobalMembersResourcesWP.GetImageThrow(theManager, 399, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 400, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 401, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 402, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 403, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 404, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 405, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 406, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_48X25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 407, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_48X25", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_49X69 = GlobalMembersResourcesWP.GetImageThrow(theManager, 408, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_49X69", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88 = GlobalMembersResourcesWP.GetImageThrow(theManager, 409, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 410, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 411, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 412, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_74X56 = GlobalMembersResourcesWP.GetImageThrow(theManager, 413, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_74X56", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_74X56_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 414, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_74X56_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_75X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 415, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_75X34", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_75X35 = GlobalMembersResourcesWP.GetImageThrow(theManager, 416, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_75X35", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_83X66 = GlobalMembersResourcesWP.GetImageThrow(theManager, 417, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_83X66", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_83X66_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 418, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_83X66_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_8X10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 419, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_8X10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_93X62 = GlobalMembersResourcesWP.GetImageThrow(theManager, 420, "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_93X62", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 421, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 422, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 423, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 424, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_168X72 = GlobalMembersResourcesWP.GetImageThrow(theManager, 425, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_168X72", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_168X79 = GlobalMembersResourcesWP.GetImageThrow(theManager, 426, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_168X79", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 427, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 428, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 429, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 430, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 431, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 432, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 433, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 434, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 435, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 436, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 437, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 438, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 439, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 440, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 441, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 442, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 443, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 444, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 445, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 446, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 447, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 448, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 449, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 450, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_272X153 = GlobalMembersResourcesWP.GetImageThrow(theManager, 451, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_272X153", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_27X35 = GlobalMembersResourcesWP.GetImageThrow(theManager, 452, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_27X35", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_27X35_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 453, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_27X35_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_31X57 = GlobalMembersResourcesWP.GetImageThrow(theManager, 454, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_31X57", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_32X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 455, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_32X34", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_32X34_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 456, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_32X34_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_383X504 = GlobalMembersResourcesWP.GetImageThrow(theManager, 457, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_383X504", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_383X504_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 458, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_383X504_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 459, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 460, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 461, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 462, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 463, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_75X35 = GlobalMembersResourcesWP.GetImageThrow(theManager, 464, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_75X35", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_83X66 = GlobalMembersResourcesWP.GetImageThrow(theManager, 465, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_83X66", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_8X10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 466, "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_8X10", 960, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Lightning_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.POPANIM_HELP_LIGHTNING_MATCH = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1537, "POPANIM_HELP_LIGHTNING_MATCH", 0, 0);
				GlobalMembersResourcesWP.POPANIM_HELP_LIGHTNING_TIME = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1538, "POPANIM_HELP_LIGHTNING_TIME", 0, 0);
				GlobalMembersResourcesWP.POPANIM_HELP_LIGHTNING_SPEED = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1539, "POPANIM_HELP_LIGHTNING_SPEED", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_PokerResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractHelp_Poker_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractHelp_Poker_960Resources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractHelp_Poker_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Poker_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_POKER_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 72, "ATLASIMAGE_ATLAS_HELP_POKER_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 567, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 568, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 569, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 570, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 571, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 572, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 573, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 574, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_383X510 = GlobalMembersResourcesWP.GetImageThrow(theManager, 575, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_383X510", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 576, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_50X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 577, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_148X158 = GlobalMembersResourcesWP.GetImageThrow(theManager, 578, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_148X158", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_152X48 = GlobalMembersResourcesWP.GetImageThrow(theManager, 579, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_152X48", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_168X60 = GlobalMembersResourcesWP.GetImageThrow(theManager, 580, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_168X60", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_206X38 = GlobalMembersResourcesWP.GetImageThrow(theManager, 581, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_206X38", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_240X52 = GlobalMembersResourcesWP.GetImageThrow(theManager, 582, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_240X52", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 583, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 584, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 585, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 586, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 587, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 588, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 589, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 590, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 591, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 592, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_44X38 = GlobalMembersResourcesWP.GetImageThrow(theManager, 593, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_44X38", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_464X687 = GlobalMembersResourcesWP.GetImageThrow(theManager, 594, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_464X687", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_61X35 = GlobalMembersResourcesWP.GetImageThrow(theManager, 595, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_61X35", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_75X76 = GlobalMembersResourcesWP.GetImageThrow(theManager, 596, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_75X76", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_81X110 = GlobalMembersResourcesWP.GetImageThrow(theManager, 597, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_81X110", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_81X110_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 598, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_81X110_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_8X10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 599, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_8X10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 600, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 601, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 602, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 603, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_138X73 = GlobalMembersResourcesWP.GetImageThrow(theManager, 604, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_138X73", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_148X158 = GlobalMembersResourcesWP.GetImageThrow(theManager, 605, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_148X158", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_152X48 = GlobalMembersResourcesWP.GetImageThrow(theManager, 606, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_152X48", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_206X38 = GlobalMembersResourcesWP.GetImageThrow(theManager, 607, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_206X38", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 608, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 609, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 610, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 611, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 612, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 613, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 614, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 615, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 616, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 617, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_358X63 = GlobalMembersResourcesWP.GetImageThrow(theManager, 618, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_358X63", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_464X687 = GlobalMembersResourcesWP.GetImageThrow(theManager, 619, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_464X687", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_81X110 = GlobalMembersResourcesWP.GetImageThrow(theManager, 620, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_81X110", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_81X110_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 621, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_81X110_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_8X10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 622, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_8X10", 480, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Poker_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_POKER_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 73, "ATLASIMAGE_ATLAS_HELP_POKER_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 567, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 568, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 569, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 570, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 571, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 572, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 573, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 574, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_383X510 = GlobalMembersResourcesWP.GetImageThrow(theManager, 575, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_383X510", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_MATCH_POKER_MATCH_50X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 576, "IMAGE_HELP_POKER_MATCH_POKER_MATCH_50X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 577, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_148X158 = GlobalMembersResourcesWP.GetImageThrow(theManager, 578, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_148X158", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_152X48 = GlobalMembersResourcesWP.GetImageThrow(theManager, 579, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_152X48", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_168X60 = GlobalMembersResourcesWP.GetImageThrow(theManager, 580, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_168X60", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_206X38 = GlobalMembersResourcesWP.GetImageThrow(theManager, 581, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_206X38", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_240X52 = GlobalMembersResourcesWP.GetImageThrow(theManager, 582, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_240X52", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 583, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 584, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 585, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 586, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 587, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 588, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 589, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 590, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 591, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 592, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_44X38 = GlobalMembersResourcesWP.GetImageThrow(theManager, 593, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_44X38", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_464X687 = GlobalMembersResourcesWP.GetImageThrow(theManager, 594, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_464X687", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_61X35 = GlobalMembersResourcesWP.GetImageThrow(theManager, 595, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_61X35", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_75X76 = GlobalMembersResourcesWP.GetImageThrow(theManager, 596, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_75X76", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_81X110 = GlobalMembersResourcesWP.GetImageThrow(theManager, 597, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_81X110", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_81X110_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 598, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_81X110_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_8X10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 599, "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_8X10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128 = GlobalMembersResourcesWP.GetImageThrow(theManager, 600, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 601, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 602, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 603, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_138X73 = GlobalMembersResourcesWP.GetImageThrow(theManager, 604, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_138X73", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_148X158 = GlobalMembersResourcesWP.GetImageThrow(theManager, 605, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_148X158", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_152X48 = GlobalMembersResourcesWP.GetImageThrow(theManager, 606, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_152X48", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_206X38 = GlobalMembersResourcesWP.GetImageThrow(theManager, 607, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_206X38", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 608, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 609, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 610, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 611, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 612, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 613, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 614, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 615, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 616, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 617, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_358X63 = GlobalMembersResourcesWP.GetImageThrow(theManager, 618, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_358X63", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_464X687 = GlobalMembersResourcesWP.GetImageThrow(theManager, 619, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_464X687", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_81X110 = GlobalMembersResourcesWP.GetImageThrow(theManager, 620, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_81X110", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_81X110_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 621, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_81X110_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_8X10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 622, "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_8X10", 960, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Poker_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.POPANIM_HELP_POKER_MATCH = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1545, "POPANIM_HELP_POKER_MATCH", 0, 0);
				GlobalMembersResourcesWP.POPANIM_HELP_POKER_SKULL_CLEAR = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1546, "POPANIM_HELP_POKER_SKULL_CLEAR", 0, 0);
				GlobalMembersResourcesWP.POPANIM_HELP_POKER_SKULLHAND = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1547, "POPANIM_HELP_POKER_SKULLHAND", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_UnusedResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractHelp_Unused_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractHelp_Unused_960Resources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractHelp_Unused_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Unused_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_UNUSED_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 74, "ATLASIMAGE_ATLAS_HELP_UNUSED_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 623, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 624, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 625, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 626, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 627, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 628, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 629, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 630, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 631, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 632, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 633, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_11", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 634, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_12", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 635, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_13", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 636, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 637, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 638, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 639, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 640, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 641, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 642, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 643, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 644, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 645, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 646, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 647, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 648, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 649, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 650, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 651, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 652, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 653, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 654, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 655, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 656, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 657, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 658, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_32X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 659, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_32X34", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_374X346 = GlobalMembersResourcesWP.GetImageThrow(theManager, 660, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_374X346", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_38X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 661, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_38X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_38X43_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 662, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_38X43_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_156X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 663, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_156X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 664, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 665, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 666, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 667, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 668, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 669, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_227X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 670, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_227X30", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 671, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 672, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 673, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 674, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 675, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 676, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 677, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_7", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 678, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_8", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 679, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_9", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 680, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_10", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_32X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 681, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_32X34", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_374X346 = GlobalMembersResourcesWP.GetImageThrow(theManager, 682, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_374X346", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 683, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 684, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_2", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 685, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_3", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 686, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_4", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 687, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_5", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 688, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_6", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 689, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_7", 480, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Unused_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HELP_UNUSED_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 75, "ATLASIMAGE_ATLAS_HELP_UNUSED_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 623, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 624, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 625, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 626, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 627, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 628, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 629, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 630, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 631, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 632, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 633, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_11", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 634, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_12", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 635, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_13", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 636, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 637, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 638, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 639, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 640, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 641, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 642, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 643, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 644, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 645, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 646, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 647, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 648, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 649, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 650, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 651, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 652, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 653, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 654, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 655, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 656, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 657, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 658, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_32X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 659, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_32X34", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_374X346 = GlobalMembersResourcesWP.GetImageThrow(theManager, 660, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_374X346", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_38X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 661, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_38X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_38X43_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 662, "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_38X43_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_156X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 663, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_156X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 664, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 665, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 666, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 667, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 668, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 669, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_227X30 = GlobalMembersResourcesWP.GetImageThrow(theManager, 670, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_227X30", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36 = GlobalMembersResourcesWP.GetImageThrow(theManager, 671, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 672, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 673, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 674, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 675, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 676, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 677, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_7", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 678, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_8", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 679, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_9", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 680, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_10", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_32X34 = GlobalMembersResourcesWP.GetImageThrow(theManager, 681, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_32X34", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_374X346 = GlobalMembersResourcesWP.GetImageThrow(theManager, 682, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_374X346", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43 = GlobalMembersResourcesWP.GetImageThrow(theManager, 683, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 684, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_2", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 685, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_3", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 686, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_4", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 687, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_5", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 688, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_6", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 689, "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_7", 960, 0, true);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHelp_Unused_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.POPANIM_HELP_SPEEDBONUS = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1548, "POPANIM_HELP_SPEEDBONUS", 0, 0);
				GlobalMembersResourcesWP.POPANIM_HELP_SPEEDBONUS2 = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1549, "POPANIM_HELP_SPEEDBONUS2", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHiddenObjectResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractHiddenObject_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractHiddenObject_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHiddenObject_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HIDDENOBJECT_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 76, "ATLASIMAGE_ATLAS_HIDDENOBJECT_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_BOTTOM_SAND = GlobalMembersResourcesWP.GetImageThrow(theManager, 1300, "IMAGE_QUEST_HIDDENOBJECT_BOARD_BOTTOM_SAND", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1301, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1302, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1303, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_PLAQUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1304, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_PLAQUE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1305, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1306, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1307, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_PLAQUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1308, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_PLAQUE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1309, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1310, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1311, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1312, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_PLAQUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1313, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_PLAQUE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_PLAQUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1314, "IMAGE_QUEST_HIDDENOBJECT_BOARD_PLAQUE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_SAND_MASK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1315, "IMAGE_QUEST_HIDDENOBJECT_BOARD_SAND_MASK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_SAND_MASK_SOFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1316, "IMAGE_QUEST_HIDDENOBJECT_BOARD_SAND_MASK_SOFT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_TOP_SAND = GlobalMembersResourcesWP.GetImageThrow(theManager, 1317, "IMAGE_QUEST_HIDDENOBJECT_BOARD_TOP_SAND", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHiddenObject_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_HIDDENOBJECT_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 77, "ATLASIMAGE_ATLAS_HIDDENOBJECT_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_BOTTOM_SAND = GlobalMembersResourcesWP.GetImageThrow(theManager, 1300, "IMAGE_QUEST_HIDDENOBJECT_BOARD_BOTTOM_SAND", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1301, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1302, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1303, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_PLAQUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1304, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_PLAQUE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1305, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1306, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1307, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_PLAQUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1308, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_PLAQUE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1309, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1310, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1311, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1312, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_PLAQUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1313, "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_PLAQUE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_PLAQUE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1314, "IMAGE_QUEST_HIDDENOBJECT_BOARD_PLAQUE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_SAND_MASK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1315, "IMAGE_QUEST_HIDDENOBJECT_BOARD_SAND_MASK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_SAND_MASK_SOFT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1316, "IMAGE_QUEST_HIDDENOBJECT_BOARD_SAND_MASK_SOFT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_HIDDENOBJECT_BOARD_TOP_SAND = GlobalMembersResourcesWP.GetImageThrow(theManager, 1317, "IMAGE_QUEST_HIDDENOBJECT_BOARD_TOP_SAND", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHyperspaceWhirlpool_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_Common_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_Common_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHyperspaceWhirlpool_Common_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_INITIAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 772, "IMAGE_HYPERSPACE_WHIRLPOOL_INITIAL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_FIRERING = GlobalMembersResourcesWP.GetImageThrow(theManager, 773, "IMAGE_HYPERSPACE_WHIRLPOOL_FIRERING", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_TUNNELEND = GlobalMembersResourcesWP.GetImageThrow(theManager, 774, "IMAGE_HYPERSPACE_WHIRLPOOL_TUNNELEND", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 775, "IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE_COVER = GlobalMembersResourcesWP.GetImageThrow(theManager, 776, "IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE_COVER", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHyperspaceWhirlpool_Common_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_INITIAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 772, "IMAGE_HYPERSPACE_WHIRLPOOL_INITIAL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_FIRERING = GlobalMembersResourcesWP.GetImageThrow(theManager, 773, "IMAGE_HYPERSPACE_WHIRLPOOL_FIRERING", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_TUNNELEND = GlobalMembersResourcesWP.GetImageThrow(theManager, 774, "IMAGE_HYPERSPACE_WHIRLPOOL_TUNNELEND", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 775, "IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE_COVER = GlobalMembersResourcesWP.GetImageThrow(theManager, 776, "IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE_COVER", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHyperspaceWhirlpool_NormalResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_Normal_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_Normal_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHyperspaceWhirlpool_Normal_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_NORMAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 777, "IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_NORMAL", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHyperspaceWhirlpool_Normal_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_NORMAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 777, "IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_NORMAL", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHyperspaceWhirlpool_ZENResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_ZEN_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractHyperspaceWhirlpool_ZEN_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHyperspaceWhirlpool_ZEN_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_ZEN = GlobalMembersResourcesWP.GetImageThrow(theManager, 778, "IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_ZEN", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractHyperspaceWhirlpool_ZEN_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_ZEN = GlobalMembersResourcesWP.GetImageThrow(theManager, 778, "IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_ZEN", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractIgnoredResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractIgnored_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractIgnored_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractIgnored_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_IGNORED_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 78, "ATLASIMAGE_ATLAS_IGNORED_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SCORE_BOARD = GlobalMembersResourcesWP.GetImageThrow(theManager, 697, "IMAGE_POKER_SCORE_BOARD", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_COLUMN2_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 953, "IMAGE_QUEST_INFERNO_COLUMN2_GLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_COLUMN1_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 954, "IMAGE_QUEST_INFERNO_COLUMN1_GLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_WEIGHT_FILL_MASK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1103, "IMAGE_WEIGHT_FILL_MASK", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractIgnored_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_IGNORED_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 79, "ATLASIMAGE_ATLAS_IGNORED_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_POKER_SCORE_BOARD = GlobalMembersResourcesWP.GetImageThrow(theManager, 697, "IMAGE_POKER_SCORE_BOARD", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_COLUMN2_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 953, "IMAGE_QUEST_INFERNO_COLUMN2_GLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUEST_INFERNO_COLUMN1_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 954, "IMAGE_QUEST_INFERNO_COLUMN1_GLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_WEIGHT_FILL_MASK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1103, "IMAGE_WEIGHT_FILL_MASK", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractInitResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.EFFECT_BADGE_GRAYSCALE = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1560, "EFFECT_BADGE_GRAYSCALE", 0, 0);
				GlobalMembersResourcesWP.EFFECT_BOARD_3D = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1561, "EFFECT_BOARD_3D", 0, 0);
				GlobalMembersResourcesWP.EFFECT_FRAME_INTERP = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1562, "EFFECT_FRAME_INTERP", 0, 0);
				GlobalMembersResourcesWP.EFFECT_GEM_3D = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1563, "EFFECT_GEM_3D", 0, 0);
				GlobalMembersResourcesWP.EFFECT_GEM_LIGHT = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1564, "EFFECT_GEM_LIGHT", 0, 0);
				GlobalMembersResourcesWP.EFFECT_GEM_SUN = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1565, "EFFECT_GEM_SUN", 0, 0);
				GlobalMembersResourcesWP.EFFECT_GRAYSCALE = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1566, "EFFECT_GRAYSCALE", 0, 0);
				GlobalMembersResourcesWP.EFFECT_GRAYSCALE_COLORIZE = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1567, "EFFECT_GRAYSCALE_COLORIZE", 0, 0);
				GlobalMembersResourcesWP.EFFECT_MASK = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1568, "EFFECT_MASK", 0, 0);
				GlobalMembersResourcesWP.EFFECT_MERGE_COLOR_ALPHA = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1569, "EFFECT_MERGE_COLOR_ALPHA", 0, 0);
				GlobalMembersResourcesWP.EFFECT_REWIND = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1570, "EFFECT_REWIND", 0, 0);
				GlobalMembersResourcesWP.EFFECT_SCREEN_DISTORT = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1571, "EFFECT_SCREEN_DISTORT", 0, 0);
				GlobalMembersResourcesWP.EFFECT_TUBE_3D = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1572, "EFFECT_TUBE_3D", 0, 0);
				GlobalMembersResourcesWP.EFFECT_TUBECAP_3D = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1573, "EFFECT_TUBECAP_3D", 0, 0);
				GlobalMembersResourcesWP.EFFECT_UNDER_DIALOG = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1574, "EFFECT_UNDER_DIALOG", 0, 0);
				GlobalMembersResourcesWP.EFFECT_WAVE = GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, 1575, "EFFECT_WAVE", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractLoaderResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractLoader_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractLoader_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractLoader_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_LOADER_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 80, "ATLASIMAGE_ATLAS_LOADER_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LOADER_WHITEDOT = GlobalMembersResourcesWP.GetImageThrow(theManager, 691, "IMAGE_LOADER_WHITEDOT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LOADER_POPCAP_BLACK_TM = GlobalMembersResourcesWP.GetImageThrow(theManager, 692, "IMAGE_LOADER_POPCAP_BLACK_TM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LOADER_POPCAP_LOADER_POPCAP = GlobalMembersResourcesWP.GetImageThrow(theManager, 693, "IMAGE_LOADER_POPCAP_LOADER_POPCAP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_LOADER_POPCAP_WHITE_GERMAN_REGISTERED = GlobalMembersResourcesWP.GetImageThrow(theManager, 694, "IMAGE_LOADER_POPCAP_WHITE_GERMAN_REGISTERED", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractLoader_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_LOADER_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 81, "ATLASIMAGE_ATLAS_LOADER_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LOADER_WHITEDOT = GlobalMembersResourcesWP.GetImageThrow(theManager, 691, "IMAGE_LOADER_WHITEDOT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LOADER_POPCAP_BLACK_TM = GlobalMembersResourcesWP.GetImageThrow(theManager, 692, "IMAGE_LOADER_POPCAP_BLACK_TM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LOADER_POPCAP_LOADER_POPCAP = GlobalMembersResourcesWP.GetImageThrow(theManager, 693, "IMAGE_LOADER_POPCAP_LOADER_POPCAP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LOADER_POPCAP_WHITE_GERMAN_REGISTERED = GlobalMembersResourcesWP.GetImageThrow(theManager, 694, "IMAGE_LOADER_POPCAP_WHITE_GERMAN_REGISTERED", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractMainMenuResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractMainMenu_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractMainMenu_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractMainMenu_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_MAINMENU_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 82, "ATLASIMAGE_ATLAS_MAINMENU_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_PILLARL = GlobalMembersResourcesWP.GetImageThrow(theManager, 701, "IMAGE_MAIN_MENU_PILLARL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_PILLARR = GlobalMembersResourcesWP.GetImageThrow(theManager, 702, "IMAGE_MAIN_MENU_PILLARR", 480, 0);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_WHITE_GERMAN_REGISTERED = GlobalMembersResourcesWP.GetImageThrow(theManager, 703, "IMAGE_MAIN_MENU_WHITE_GERMAN_REGISTERED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_WHITE_TM = GlobalMembersResourcesWP.GetImageThrow(theManager, 704, "IMAGE_MAIN_MENU_WHITE_TM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CRYSTALBALL = GlobalMembersResourcesWP.GetImageThrow(theManager, 801, "IMAGE_CRYSTALBALL", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractMainMenu_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_MAINMENU_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 83, "ATLASIMAGE_ATLAS_MAINMENU_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_PILLARL = GlobalMembersResourcesWP.GetImageThrow(theManager, 701, "IMAGE_MAIN_MENU_PILLARL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_PILLARR = GlobalMembersResourcesWP.GetImageThrow(theManager, 702, "IMAGE_MAIN_MENU_PILLARR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_WHITE_GERMAN_REGISTERED = GlobalMembersResourcesWP.GetImageThrow(theManager, 703, "IMAGE_MAIN_MENU_WHITE_GERMAN_REGISTERED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_MAIN_MENU_WHITE_TM = GlobalMembersResourcesWP.GetImageThrow(theManager, 704, "IMAGE_MAIN_MENU_WHITE_TM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CRYSTALBALL = GlobalMembersResourcesWP.GetImageThrow(theManager, 801, "IMAGE_CRYSTALBALL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ARROW_01 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1769, "IMAGE_ARROW_01", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ARROW_02 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1770, "IMAGE_ARROW_02", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ARROW_03 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1771, "IMAGE_ARROW_03", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ARROW_04 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1772, "IMAGE_ARROW_04", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ARROW_05 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1773, "IMAGE_ARROW_05", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ARROW_06 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1774, "IMAGE_ARROW_07", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ARROW_07 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1775, "IMAGE_ARROW_08", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ARROW_08 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1776, "IMAGE_ARROW_09", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ARROW_09 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1777, "IMAGE_ARROW_09", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ARROW_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1778, "IMAGE_ARROW_10", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ARROW_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1779, "IMAGE_ARROW_GLOW", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractBadgesGrayIconResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_BEJEWELER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1780, "IMAGE_BADGES_GRAY_ICON_BEJEWELER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_BLASTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1781, "IMAGE_BADGES_GRAY_ICON_BLASTER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_BTF_BONANZA = GlobalMembersResourcesWP.GetImageThrow(theManager, 1782, "IMAGE_BADGES_GRAY_ICON_BTF_BONANZA", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_BTF_MONARCH = GlobalMembersResourcesWP.GetImageThrow(theManager, 1783, "IMAGE_BADGES_GRAY_ICON_BTF_MONARCH", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_CHAINREACTION = GlobalMembersResourcesWP.GetImageThrow(theManager, 1784, "IMAGE_BADGES_GRAY_ICON_CHAINREACTION", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_CHROMATIC = GlobalMembersResourcesWP.GetImageThrow(theManager, 1785, "IMAGE_BADGES_GRAY_ICON_CHROMATIC", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_DIAMONDMINE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1786, "IMAGE_BADGES_GRAY_ICON_DIAMONDMINE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_ELECTRIFIER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1787, "IMAGE_BADGES_GRAY_ICON_ELECTRIFIER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_HIGHVOLTAGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1788, "IMAGE_BADGES_GRAY_ICON_HIGHVOLTAGE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_LEVELORD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1789, "IMAGE_BADGES_GRAY_ICON_LEVELORD", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_LUCKYSTREAK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1790, "IMAGE_BADGES_GRAY_ICON_LUCKYSTREAK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_RELICHUNTER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1791, "IMAGE_BADGES_GRAY_ICON_RELICHUNTER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_STELLAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1792, "IMAGE_BADGES_GRAY_ICON_STELLAR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BADGES_GRAY_ICON_SUPERSTAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1793, "IMAGE_BADGES_GRAY_ICON_SUPERSTAR", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractLRLoadingResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_LR_LOADING_01 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1794, "IMAGE_LR_LOADING_01", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LR_LOADING_02 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1795, "IMAGE_LR_LOADING_01", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LR_LOADING_03 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1796, "IMAGE_LR_LOADING_01", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LR_LOADING_04 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1797, "IMAGE_LR_LOADING_01", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LR_LOADING_05 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1798, "IMAGE_LR_LOADING_01", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LR_LOADING_06 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1799, "IMAGE_LR_LOADING_01", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LR_LOADING_07 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1800, "IMAGE_LR_LOADING_01", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LR_LOADING_08 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1801, "IMAGE_LR_LOADING_01", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LR_LOADING_09 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1802, "IMAGE_LR_LOADING_01", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LR_LOADING_10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1803, "IMAGE_LR_LOADING_01", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LR_LOADING_11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1804, "IMAGE_LR_LOADING_01", 960, 0);
				GlobalMembersResourcesWP.IMAGE_LR_LOADING_12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1805, "IMAGE_LR_LOADING_01", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractNoMatchResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractNoMatch_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractNoMatch_960Resources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractNoMatch_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractNoMatch_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_NOMATCH_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 84, "ATLASIMAGE_ATLAS_NOMATCH_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ANGRYBOMB = GlobalMembersResourcesWP.GetImageThrow(theManager, 1401, "IMAGE_ANGRYBOMB", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ANIMS_100CREST_100CREST = GlobalMembersResourcesWP.GetImageThrow(theManager, 1402, "IMAGE_ANIMS_100CREST_100CREST", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_BOARDSHATTER_BOTTOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1403, "IMAGE_ANIMS_BOARDSHATTER_BOTTOM", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_BOARDSHATTER_GRID = GlobalMembersResourcesWP.GetImageThrow(theManager, 1404, "IMAGE_ANIMS_BOARDSHATTER_GRID", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_BOARDSHATTER_TOP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1405, "IMAGE_ANIMS_BOARDSHATTER_TOP", 480, 0, true);
				GlobalMembersResourcesWP.IMAGE_BOOM_BOARD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1406, "IMAGE_BOOM_BOARD", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BOOM_CRATER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1407, "IMAGE_BOOM_CRATER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BOOM_FBOTTOM_WIDGET = GlobalMembersResourcesWP.GetImageThrow(theManager, 1408, "IMAGE_BOOM_FBOTTOM_WIDGET", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BOOM_FGRIDBAR_BOT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1409, "IMAGE_BOOM_FGRIDBAR_BOT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BOOM_FGRIDBAR_TOP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1410, "IMAGE_BOOM_FGRIDBAR_TOP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BOOM_FTOP_WIDGET = GlobalMembersResourcesWP.GetImageThrow(theManager, 1411, "IMAGE_BOOM_FTOP_WIDGET", 480, 0);
				GlobalMembersResourcesWP.IMAGE_BROWSER_BACKBTN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1412, "IMAGE_BROWSER_BACKBTN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CHECKPOINT_MARKER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1413, "IMAGE_CHECKPOINT_MARKER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_BKG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1414, "IMAGE_CLOCK_BKG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_FACE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1415, "IMAGE_CLOCK_FACE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_FILL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1416, "IMAGE_CLOCK_FILL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1417, "IMAGE_CLOCK_GEAR1", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1418, "IMAGE_CLOCK_GEAR2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1419, "IMAGE_CLOCK_GEAR3", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1420, "IMAGE_CLOCK_GEAR4", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1421, "IMAGE_CLOCK_GEAR5", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1422, "IMAGE_CLOCK_GEAR6", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1423, "IMAGE_CLOCK_GEAR7", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1424, "IMAGE_CLOCK_GEAR8", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1425, "IMAGE_CLOCK_GEAR9", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GLARE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1426, "IMAGE_CLOCK_GLARE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_SPOKE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1427, "IMAGE_CLOCK_SPOKE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DETONATOR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1428, "IMAGE_DETONATOR", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DETONATOR_MOUSEOVER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1429, "IMAGE_DETONATOR_MOUSEOVER", 480, 0);
				GlobalMembersResourcesWP.IMAGE_DOOMGEM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1430, "IMAGE_DOOMGEM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_BAR__PINK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1431, "IMAGE_GAMEOVER_BAR__PINK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_BAR_ORANGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1432, "IMAGE_GAMEOVER_BAR_ORANGE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_BAR_YELLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1433, "IMAGE_GAMEOVER_BAR_YELLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_BOX_ORANGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1434, "IMAGE_GAMEOVER_BOX_ORANGE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_BOX_PINK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1435, "IMAGE_GAMEOVER_BOX_PINK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_BOX_YELLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1436, "IMAGE_GAMEOVER_BOX_YELLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DARKER_BOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1437, "IMAGE_GAMEOVER_DARKER_BOX", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DARKEST_BOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1438, "IMAGE_GAMEOVER_DARKEST_BOX", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIALOG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1439, "IMAGE_GAMEOVER_DIALOG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIG_BAR_GEMS = GlobalMembersResourcesWP.GetImageThrow(theManager, 1440, "IMAGE_GAMEOVER_DIG_BAR_GEMS", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIG_BAR_GOLD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1441, "IMAGE_GAMEOVER_DIG_BAR_GOLD", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIG_BAR_TREASURE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1442, "IMAGE_GAMEOVER_DIG_BAR_TREASURE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIG_BOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1443, "IMAGE_GAMEOVER_DIG_BOX", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_HORIZONTAL_BAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1444, "IMAGE_GAMEOVER_HORIZONTAL_BAR", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_HORIZONTAL_BAR_OVERLAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 1445, "IMAGE_GAMEOVER_HORIZONTAL_BAR_OVERLAY", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_FLAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 1446, "IMAGE_GAMEOVER_ICON_FLAME", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_FLAME_LRG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1447, "IMAGE_GAMEOVER_ICON_FLAME_LRG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_HYPERCUBE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1448, "IMAGE_GAMEOVER_ICON_HYPERCUBE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_HYPERCUBE_LRG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1449, "IMAGE_GAMEOVER_ICON_HYPERCUBE_LRG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_LIGHTNING = GlobalMembersResourcesWP.GetImageThrow(theManager, 1450, "IMAGE_GAMEOVER_ICON_LIGHTNING", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1451, "IMAGE_GAMEOVER_ICON_STAR", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_STAR_LRG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1452, "IMAGE_GAMEOVER_ICON_STAR_LRG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_LIGHT_BOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1453, "IMAGE_GAMEOVER_LIGHT_BOX", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_LINE_SINGLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1454, "IMAGE_GAMEOVER_LINE_SINGLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_LINES = GlobalMembersResourcesWP.GetImageThrow(theManager, 1455, "IMAGE_GAMEOVER_LINES", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_GRAPH = GlobalMembersResourcesWP.GetImageThrow(theManager, 1456, "IMAGE_GAMEOVER_SECTION_GRAPH", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_LABEL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1457, "IMAGE_GAMEOVER_SECTION_LABEL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_SMALL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1458, "IMAGE_GAMEOVER_SECTION_SMALL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_STAMP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1459, "IMAGE_GAMEOVER_STAMP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GREENQUESTION = GlobalMembersResourcesWP.GetImageThrow(theManager, 1460, "IMAGE_GREENQUESTION", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GRIDPAINT_BLANK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1461, "IMAGE_GRIDPAINT_BLANK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_GRIDPAINT_FILLED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1462, "IMAGE_GRIDPAINT_FILLED", 480, 0);
				GlobalMembersResourcesWP.IMAGE_MENU_ARROW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1463, "IMAGE_MENU_ARROW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUESTOBJ_FINAL_GLOW_TRANS = GlobalMembersResourcesWP.GetImageThrow(theManager, 1464, "IMAGE_QUESTOBJ_FINAL_GLOW_TRANS", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUESTOBJ_FINAL_GLOW_TRANS2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1465, "IMAGE_QUESTOBJ_FINAL_GLOW_TRANS2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUESTOBJ_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1466, "IMAGE_QUESTOBJ_GLOW", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUESTOBJ_GLOW_FINAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1467, "IMAGE_QUESTOBJ_GLOW_FINAL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUESTOBJ_GLOW_FX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1468, "IMAGE_QUESTOBJ_GLOW_FX", 480, 0);
				GlobalMembersResourcesWP.IMAGE_QUESTOBJ_GLOW2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1469, "IMAGE_QUESTOBJ_GLOW2", 480, 0);
				GlobalMembersResourcesWP.IMAGE_RANKUP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1470, "IMAGE_RANKUP", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SOLID_BLACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1471, "IMAGE_SOLID_BLACK", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SPARKLE_FAT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1472, "IMAGE_SPARKLE_FAT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SPARKLET_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1473, "IMAGE_SPARKLET_BIG", 480, 0);
				GlobalMembersResourcesWP.IMAGE_SPARKLET_FAT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1474, "IMAGE_SPARKLET_FAT", 480, 0);
				GlobalMembersResourcesWP.IMAGE_TRANSPARENT_HOLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1475, "IMAGE_TRANSPARENT_HOLE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_VERTICAL_STREAK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1476, "IMAGE_VERTICAL_STREAK", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractNoMatch_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_NOMATCH_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 85, "ATLASIMAGE_ATLAS_NOMATCH_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ANGRYBOMB = GlobalMembersResourcesWP.GetImageThrow(theManager, 1401, "IMAGE_ANGRYBOMB", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ANIMS_100CREST_100CREST = GlobalMembersResourcesWP.GetImageThrow(theManager, 1402, "IMAGE_ANIMS_100CREST_100CREST", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_BOARDSHATTER_BOTTOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1403, "IMAGE_ANIMS_BOARDSHATTER_BOTTOM", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_BOARDSHATTER_GRID = GlobalMembersResourcesWP.GetImageThrow(theManager, 1404, "IMAGE_ANIMS_BOARDSHATTER_GRID", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_ANIMS_BOARDSHATTER_TOP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1405, "IMAGE_ANIMS_BOARDSHATTER_TOP", 960, 0, true);
				GlobalMembersResourcesWP.IMAGE_BOOM_BOARD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1406, "IMAGE_BOOM_BOARD", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BOOM_CRATER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1407, "IMAGE_BOOM_CRATER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BOOM_FBOTTOM_WIDGET = GlobalMembersResourcesWP.GetImageThrow(theManager, 1408, "IMAGE_BOOM_FBOTTOM_WIDGET", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BOOM_FGRIDBAR_BOT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1409, "IMAGE_BOOM_FGRIDBAR_BOT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BOOM_FGRIDBAR_TOP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1410, "IMAGE_BOOM_FGRIDBAR_TOP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BOOM_FTOP_WIDGET = GlobalMembersResourcesWP.GetImageThrow(theManager, 1411, "IMAGE_BOOM_FTOP_WIDGET", 960, 0);
				GlobalMembersResourcesWP.IMAGE_BROWSER_BACKBTN = GlobalMembersResourcesWP.GetImageThrow(theManager, 1412, "IMAGE_BROWSER_BACKBTN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CHECKPOINT_MARKER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1413, "IMAGE_CHECKPOINT_MARKER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_BKG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1414, "IMAGE_CLOCK_BKG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_FACE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1415, "IMAGE_CLOCK_FACE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_FILL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1416, "IMAGE_CLOCK_FILL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1417, "IMAGE_CLOCK_GEAR1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1418, "IMAGE_CLOCK_GEAR2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1419, "IMAGE_CLOCK_GEAR3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1420, "IMAGE_CLOCK_GEAR4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1421, "IMAGE_CLOCK_GEAR5", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1422, "IMAGE_CLOCK_GEAR6", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1423, "IMAGE_CLOCK_GEAR7", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1424, "IMAGE_CLOCK_GEAR8", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GEAR9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1425, "IMAGE_CLOCK_GEAR9", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_GLARE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1426, "IMAGE_CLOCK_GLARE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_SPOKE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1427, "IMAGE_CLOCK_SPOKE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DETONATOR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1428, "IMAGE_DETONATOR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DETONATOR_MOUSEOVER = GlobalMembersResourcesWP.GetImageThrow(theManager, 1429, "IMAGE_DETONATOR_MOUSEOVER", 960, 0);
				GlobalMembersResourcesWP.IMAGE_DOOMGEM = GlobalMembersResourcesWP.GetImageThrow(theManager, 1430, "IMAGE_DOOMGEM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_BAR__PINK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1431, "IMAGE_GAMEOVER_BAR__PINK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_BAR_ORANGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1432, "IMAGE_GAMEOVER_BAR_ORANGE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_BAR_YELLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1433, "IMAGE_GAMEOVER_BAR_YELLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_BOX_ORANGE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1434, "IMAGE_GAMEOVER_BOX_ORANGE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_BOX_PINK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1435, "IMAGE_GAMEOVER_BOX_PINK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_BOX_YELLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1436, "IMAGE_GAMEOVER_BOX_YELLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DARKER_BOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1437, "IMAGE_GAMEOVER_DARKER_BOX", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DARKEST_BOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1438, "IMAGE_GAMEOVER_DARKEST_BOX", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIALOG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1439, "IMAGE_GAMEOVER_DIALOG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIG_BAR_GEMS = GlobalMembersResourcesWP.GetImageThrow(theManager, 1440, "IMAGE_GAMEOVER_DIG_BAR_GEMS", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIG_BAR_GOLD = GlobalMembersResourcesWP.GetImageThrow(theManager, 1441, "IMAGE_GAMEOVER_DIG_BAR_GOLD", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIG_BAR_TREASURE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1442, "IMAGE_GAMEOVER_DIG_BAR_TREASURE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_DIG_BOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1443, "IMAGE_GAMEOVER_DIG_BOX", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_HORIZONTAL_BAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1444, "IMAGE_GAMEOVER_HORIZONTAL_BAR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_HORIZONTAL_BAR_OVERLAY = GlobalMembersResourcesWP.GetImageThrow(theManager, 1445, "IMAGE_GAMEOVER_HORIZONTAL_BAR_OVERLAY", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_FLAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 1446, "IMAGE_GAMEOVER_ICON_FLAME", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_FLAME_LRG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1447, "IMAGE_GAMEOVER_ICON_FLAME_LRG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_HYPERCUBE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1448, "IMAGE_GAMEOVER_ICON_HYPERCUBE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_HYPERCUBE_LRG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1449, "IMAGE_GAMEOVER_ICON_HYPERCUBE_LRG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_LIGHTNING = GlobalMembersResourcesWP.GetImageThrow(theManager, 1450, "IMAGE_GAMEOVER_ICON_LIGHTNING", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_STAR = GlobalMembersResourcesWP.GetImageThrow(theManager, 1451, "IMAGE_GAMEOVER_ICON_STAR", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_ICON_STAR_LRG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1452, "IMAGE_GAMEOVER_ICON_STAR_LRG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_LIGHT_BOX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1453, "IMAGE_GAMEOVER_LIGHT_BOX", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_LINE_SINGLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1454, "IMAGE_GAMEOVER_LINE_SINGLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_LINES = GlobalMembersResourcesWP.GetImageThrow(theManager, 1455, "IMAGE_GAMEOVER_LINES", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_GRAPH = GlobalMembersResourcesWP.GetImageThrow(theManager, 1456, "IMAGE_GAMEOVER_SECTION_GRAPH", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_LABEL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1457, "IMAGE_GAMEOVER_SECTION_LABEL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_SECTION_SMALL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1458, "IMAGE_GAMEOVER_SECTION_SMALL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GAMEOVER_STAMP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1459, "IMAGE_GAMEOVER_STAMP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GREENQUESTION = GlobalMembersResourcesWP.GetImageThrow(theManager, 1460, "IMAGE_GREENQUESTION", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GRIDPAINT_BLANK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1461, "IMAGE_GRIDPAINT_BLANK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_GRIDPAINT_FILLED = GlobalMembersResourcesWP.GetImageThrow(theManager, 1462, "IMAGE_GRIDPAINT_FILLED", 960, 0);
				GlobalMembersResourcesWP.IMAGE_MENU_ARROW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1463, "IMAGE_MENU_ARROW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUESTOBJ_FINAL_GLOW_TRANS = GlobalMembersResourcesWP.GetImageThrow(theManager, 1464, "IMAGE_QUESTOBJ_FINAL_GLOW_TRANS", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUESTOBJ_FINAL_GLOW_TRANS2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1465, "IMAGE_QUESTOBJ_FINAL_GLOW_TRANS2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUESTOBJ_GLOW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1466, "IMAGE_QUESTOBJ_GLOW", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUESTOBJ_GLOW_FINAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 1467, "IMAGE_QUESTOBJ_GLOW_FINAL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUESTOBJ_GLOW_FX = GlobalMembersResourcesWP.GetImageThrow(theManager, 1468, "IMAGE_QUESTOBJ_GLOW_FX", 960, 0);
				GlobalMembersResourcesWP.IMAGE_QUESTOBJ_GLOW2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1469, "IMAGE_QUESTOBJ_GLOW2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_RANKUP = GlobalMembersResourcesWP.GetImageThrow(theManager, 1470, "IMAGE_RANKUP", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SOLID_BLACK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1471, "IMAGE_SOLID_BLACK", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SPARKLE_FAT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1472, "IMAGE_SPARKLE_FAT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SPARKLET_BIG = GlobalMembersResourcesWP.GetImageThrow(theManager, 1473, "IMAGE_SPARKLET_BIG", 960, 0);
				GlobalMembersResourcesWP.IMAGE_SPARKLET_FAT = GlobalMembersResourcesWP.GetImageThrow(theManager, 1474, "IMAGE_SPARKLET_FAT", 960, 0);
				GlobalMembersResourcesWP.IMAGE_TRANSPARENT_HOLE = GlobalMembersResourcesWP.GetImageThrow(theManager, 1475, "IMAGE_TRANSPARENT_HOLE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_VERTICAL_STREAK = GlobalMembersResourcesWP.GetImageThrow(theManager, 1476, "IMAGE_VERTICAL_STREAK", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractNoMatch_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.PIEFFECT_ANIMS_COLUMN1_FBOMB_SMALL = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1523, "PIEFFECT_ANIMS_COLUMN1_FBOMB_SMALL", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_ANIMS_COLUMN1_SHATTERLEFT_SMALL = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1524, "PIEFFECT_ANIMS_COLUMN1_SHATTERLEFT_SMALL", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_ANIMS_COLUMN1_SHATTERRIGHT_SMALL = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1525, "PIEFFECT_ANIMS_COLUMN1_SHATTERRIGHT_SMALL", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_ANIMS_COLUMN1_SNOWCRUSH = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1526, "PIEFFECT_ANIMS_COLUMN1_SNOWCRUSH", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_ANIMS_COLUMN2_FBOMB = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1527, "PIEFFECT_ANIMS_COLUMN2_FBOMB", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_ANIMS_COLUMN2_SHATTERLEFT = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1528, "PIEFFECT_ANIMS_COLUMN2_SHATTERLEFT", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_ANIMS_COLUMN2_SHATTERRIGHT = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1529, "PIEFFECT_ANIMS_COLUMN2_SHATTERRIGHT", 0, 0);
				GlobalMembersResourcesWP.PIEFFECT_ANIMS_COLUMN2_SNOWCRUSH = GlobalMembersResourcesWP.GetPIEffectThrow(theManager, 1530, "PIEFFECT_ANIMS_COLUMN2_SNOWCRUSH", 0, 0);
				GlobalMembersResourcesWP.POPANIM_ANIMS_100CREST = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1558, "POPANIM_ANIMS_100CREST", 0, 0);
				GlobalMembersResourcesWP.POPANIM_ANIMS_BOARDSHATTER = GlobalMembersResourcesWP.GetPopAnimThrow(theManager, 1559, "POPANIM_ANIMS_BOARDSHATTER", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_0Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_0_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_0_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_0_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP0 = GlobalMembersResourcesWP.GetImageThrow(theManager, 712, "IMAGE_PP0", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_0_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP0 = GlobalMembersResourcesWP.GetImageThrow(theManager, 712, "IMAGE_PP0", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 713, "IMAGE_PP1", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 714, "IMAGE_PP2", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 715, "IMAGE_PP3", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 716, "IMAGE_PP4", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 717, "IMAGE_PP5", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 718, "IMAGE_PP6", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 719, "IMAGE_PP7", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 720, "IMAGE_PP8", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 721, "IMAGE_PP9", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 722, "IMAGE_PP10", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 723, "IMAGE_PP11", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 724, "IMAGE_PP12", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 725, "IMAGE_PP13", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 726, "IMAGE_PP14", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 727, "IMAGE_PP15", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP16 = GlobalMembersResourcesWP.GetImageThrow(theManager, 728, "IMAGE_PP16", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 729, "IMAGE_PP17", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP18 = GlobalMembersResourcesWP.GetImageThrow(theManager, 730, "IMAGE_PP18", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 731, "IMAGE_PP19", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 732, "IMAGE_PP20", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 733, "IMAGE_PP21", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 734, "IMAGE_PP22", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP23 = GlobalMembersResourcesWP.GetImageThrow(theManager, 735, "IMAGE_PP23", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP24 = GlobalMembersResourcesWP.GetImageThrow(theManager, 736, "IMAGE_PP24", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 737, "IMAGE_PP25", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 738, "IMAGE_PP26", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP27 = GlobalMembersResourcesWP.GetImageThrow(theManager, 739, "IMAGE_PP27", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP28 = GlobalMembersResourcesWP.GetImageThrow(theManager, 740, "IMAGE_PP28", 960, 0);
				GlobalMembersResourcesWP.IMAGE_PP29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 741, "IMAGE_PP29", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_1Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_1_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_1_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_10Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_10_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_10_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_10_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 722, "IMAGE_PP10", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_10_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP10 = GlobalMembersResourcesWP.GetImageThrow(theManager, 722, "IMAGE_PP10", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_11Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_11_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_11_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_11_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 723, "IMAGE_PP11", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_11_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP11 = GlobalMembersResourcesWP.GetImageThrow(theManager, 723, "IMAGE_PP11", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_12Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_12_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_12_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_12_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 724, "IMAGE_PP12", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_12_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP12 = GlobalMembersResourcesWP.GetImageThrow(theManager, 724, "IMAGE_PP12", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_13Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_13_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_13_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_13_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 725, "IMAGE_PP13", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_13_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP13 = GlobalMembersResourcesWP.GetImageThrow(theManager, 725, "IMAGE_PP13", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_14Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_14_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_14_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_14_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 726, "IMAGE_PP14", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_14_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP14 = GlobalMembersResourcesWP.GetImageThrow(theManager, 726, "IMAGE_PP14", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_15Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_15_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_15_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_15_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 727, "IMAGE_PP15", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_15_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP15 = GlobalMembersResourcesWP.GetImageThrow(theManager, 727, "IMAGE_PP15", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_16Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_16_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_16_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_16_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP16 = GlobalMembersResourcesWP.GetImageThrow(theManager, 728, "IMAGE_PP16", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_16_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP16 = GlobalMembersResourcesWP.GetImageThrow(theManager, 728, "IMAGE_PP16", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_17Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_17_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_17_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_17_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 729, "IMAGE_PP17", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_17_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP17 = GlobalMembersResourcesWP.GetImageThrow(theManager, 729, "IMAGE_PP17", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_18Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_18_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_18_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_18_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP18 = GlobalMembersResourcesWP.GetImageThrow(theManager, 730, "IMAGE_PP18", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_18_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP18 = GlobalMembersResourcesWP.GetImageThrow(theManager, 730, "IMAGE_PP18", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_19Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_19_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_19_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_19_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 731, "IMAGE_PP19", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_19_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP19 = GlobalMembersResourcesWP.GetImageThrow(theManager, 731, "IMAGE_PP19", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_1_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 713, "IMAGE_PP1", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_1_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP1 = GlobalMembersResourcesWP.GetImageThrow(theManager, 713, "IMAGE_PP1", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_2Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_2_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_2_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_20Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_20_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_20_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_20_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 732, "IMAGE_PP20", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_20_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP20 = GlobalMembersResourcesWP.GetImageThrow(theManager, 732, "IMAGE_PP20", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_21Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_21_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_21_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_21_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 733, "IMAGE_PP21", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_21_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP21 = GlobalMembersResourcesWP.GetImageThrow(theManager, 733, "IMAGE_PP21", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_22Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_22_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_22_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_22_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 734, "IMAGE_PP22", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_22_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP22 = GlobalMembersResourcesWP.GetImageThrow(theManager, 734, "IMAGE_PP22", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_23Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_23_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_23_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_23_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP23 = GlobalMembersResourcesWP.GetImageThrow(theManager, 735, "IMAGE_PP23", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_23_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP23 = GlobalMembersResourcesWP.GetImageThrow(theManager, 735, "IMAGE_PP23", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_24Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_24_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_24_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_24_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP24 = GlobalMembersResourcesWP.GetImageThrow(theManager, 736, "IMAGE_PP24", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_24_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP24 = GlobalMembersResourcesWP.GetImageThrow(theManager, 736, "IMAGE_PP24", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_25Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_25_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_25_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_25_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 737, "IMAGE_PP25", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_25_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP25 = GlobalMembersResourcesWP.GetImageThrow(theManager, 737, "IMAGE_PP25", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_26Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_26_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_26_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_26_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 738, "IMAGE_PP26", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_26_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP26 = GlobalMembersResourcesWP.GetImageThrow(theManager, 738, "IMAGE_PP26", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_27Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_27_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_27_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_27_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP27 = GlobalMembersResourcesWP.GetImageThrow(theManager, 739, "IMAGE_PP27", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_27_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP27 = GlobalMembersResourcesWP.GetImageThrow(theManager, 739, "IMAGE_PP27", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_28Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_28_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_28_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_28_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP28 = GlobalMembersResourcesWP.GetImageThrow(theManager, 740, "IMAGE_PP28", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_28_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP28 = GlobalMembersResourcesWP.GetImageThrow(theManager, 740, "IMAGE_PP28", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_29Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_29_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_29_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_29_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 741, "IMAGE_PP29", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_29_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP29 = GlobalMembersResourcesWP.GetImageThrow(theManager, 741, "IMAGE_PP29", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_2_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 714, "IMAGE_PP2", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_2_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP2 = GlobalMembersResourcesWP.GetImageThrow(theManager, 714, "IMAGE_PP2", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_3Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_3_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_3_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_3_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 715, "IMAGE_PP3", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_3_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP3 = GlobalMembersResourcesWP.GetImageThrow(theManager, 715, "IMAGE_PP3", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_4Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_4_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_4_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_4_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 716, "IMAGE_PP4", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_4_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP4 = GlobalMembersResourcesWP.GetImageThrow(theManager, 716, "IMAGE_PP4", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_5Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_5_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_5_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_5_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 717, "IMAGE_PP5", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_5_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP5 = GlobalMembersResourcesWP.GetImageThrow(theManager, 717, "IMAGE_PP5", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_6Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_6_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_6_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_6_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 718, "IMAGE_PP6", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_6_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP6 = GlobalMembersResourcesWP.GetImageThrow(theManager, 718, "IMAGE_PP6", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_7Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_7_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_7_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_7_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 719, "IMAGE_PP7", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_7_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP7 = GlobalMembersResourcesWP.GetImageThrow(theManager, 719, "IMAGE_PP7", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_8Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_8_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_8_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_8_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 720, "IMAGE_PP8", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_8_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP8 = GlobalMembersResourcesWP.GetImageThrow(theManager, 720, "IMAGE_PP8", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_9Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractProfilePic_9_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractProfilePic_9_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_9_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 721, "IMAGE_PP9", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractProfilePic_9_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.IMAGE_PP9 = GlobalMembersResourcesWP.GetImageThrow(theManager, 721, "IMAGE_PP9", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractQuestHelpResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractQuestHelp_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractQuestHelp_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractQuestHelp_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_QUESTHELP_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 86, "ATLASIMAGE_ATLAS_QUESTHELP_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_ICON = GlobalMembersResourcesWP.GetImageThrow(theManager, 690, "IMAGE_CLOCK_ICON", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractQuestHelp_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_QUESTHELP_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 87, "ATLASIMAGE_ATLAS_QUESTHELP_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_CLOCK_ICON = GlobalMembersResourcesWP.GetImageThrow(theManager, 690, "IMAGE_CLOCK_ICON", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractRateGameResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractRateGame_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractRateGame_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractRateGame_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_RATEGAME_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 88, "ATLASIMAGE_ATLAS_RATEGAME_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_RATETHEGAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 695, "IMAGE_RATETHEGAME", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractRateGame_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_RATEGAME_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 89, "ATLASIMAGE_ATLAS_RATEGAME_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_RATETHEGAME = GlobalMembersResourcesWP.GetImageThrow(theManager, 695, "IMAGE_RATETHEGAME", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractZenModeResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurLocSet == 1145390149U && !GlobalMembersResourcesWP.ExtractZenMode_DEDEResources(theManager))
				{
					return false;
				}
				if (theManager.mCurLocSet == 1162761555U && !GlobalMembersResourcesWP.ExtractZenMode_ENUSResources(theManager))
				{
					return false;
				}
				if (theManager.mCurLocSet == 1163085139U && !GlobalMembersResourcesWP.ExtractZenMode_ESESResources(theManager))
				{
					return false;
				}
				if (theManager.mCurLocSet == 1179797074U && !GlobalMembersResourcesWP.ExtractZenMode_FRFRResources(theManager))
				{
					return false;
				}
				if (theManager.mCurLocSet == 1230260564U && !GlobalMembersResourcesWP.ExtractZenMode_ITITResources(theManager))
				{
					return false;
				}
				if (!GlobalMembersResourcesWP.ExtractZenMode_CommonResources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractZenMode_CommonResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.RESFILE_AMBIENT_COASTAL = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 26, "RESFILE_AMBIENT_COASTAL", 0, 0);
				GlobalMembersResourcesWP.RESFILE_AMBIENT_CRICKETS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 27, "RESFILE_AMBIENT_CRICKETS", 0, 0);
				GlobalMembersResourcesWP.RESFILE_AMBIENT_FOREST = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 28, "RESFILE_AMBIENT_FOREST", 0, 0);
				GlobalMembersResourcesWP.RESFILE_AMBIENT_OCEAN_SURF = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 29, "RESFILE_AMBIENT_OCEAN_SURF", 0, 0);
				GlobalMembersResourcesWP.RESFILE_AMBIENT_RAIN_LEAVES = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 30, "RESFILE_AMBIENT_RAIN_LEAVES", 0, 0);
				GlobalMembersResourcesWP.RESFILE_AMBIENT_WATERFALL = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 31, "RESFILE_AMBIENT_WATERFALL", 0, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractZenMode_DEDEResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_GENERAL = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 20, "RESFILE_AFFIRMATIONS_GENERAL", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_POSITIVE_THINKING = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 21, "RESFILE_AFFIRMATIONS_POSITIVE_THINKING", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_PROSPERITY = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 22, "RESFILE_AFFIRMATIONS_PROSPERITY", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_QUIT_BAD_HABITS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 23, "RESFILE_AFFIRMATIONS_QUIT_BAD_HABITS", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_SELF_CONFIDENCE = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 24, "RESFILE_AFFIRMATIONS_SELF_CONFIDENCE", 0, 1145390149);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_WEIGHT_LOSS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 25, "RESFILE_AFFIRMATIONS_WEIGHT_LOSS", 0, 1145390149);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractZenMode_ENUSResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_GENERAL = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 20, "RESFILE_AFFIRMATIONS_GENERAL", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_POSITIVE_THINKING = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 21, "RESFILE_AFFIRMATIONS_POSITIVE_THINKING", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_PROSPERITY = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 22, "RESFILE_AFFIRMATIONS_PROSPERITY", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_QUIT_BAD_HABITS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 23, "RESFILE_AFFIRMATIONS_QUIT_BAD_HABITS", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_SELF_CONFIDENCE = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 24, "RESFILE_AFFIRMATIONS_SELF_CONFIDENCE", 0, 1162761555);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_WEIGHT_LOSS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 25, "RESFILE_AFFIRMATIONS_WEIGHT_LOSS", 0, 1162761555);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractZenMode_ESESResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_GENERAL = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 20, "RESFILE_AFFIRMATIONS_GENERAL", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_POSITIVE_THINKING = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 21, "RESFILE_AFFIRMATIONS_POSITIVE_THINKING", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_PROSPERITY = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 22, "RESFILE_AFFIRMATIONS_PROSPERITY", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_QUIT_BAD_HABITS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 23, "RESFILE_AFFIRMATIONS_QUIT_BAD_HABITS", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_SELF_CONFIDENCE = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 24, "RESFILE_AFFIRMATIONS_SELF_CONFIDENCE", 0, 1163085139);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_WEIGHT_LOSS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 25, "RESFILE_AFFIRMATIONS_WEIGHT_LOSS", 0, 1163085139);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractZenMode_FRFRResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_GENERAL = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 20, "RESFILE_AFFIRMATIONS_GENERAL", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_POSITIVE_THINKING = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 21, "RESFILE_AFFIRMATIONS_POSITIVE_THINKING", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_PROSPERITY = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 22, "RESFILE_AFFIRMATIONS_PROSPERITY", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_QUIT_BAD_HABITS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 23, "RESFILE_AFFIRMATIONS_QUIT_BAD_HABITS", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_SELF_CONFIDENCE = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 24, "RESFILE_AFFIRMATIONS_SELF_CONFIDENCE", 0, 1179797074);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_WEIGHT_LOSS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 25, "RESFILE_AFFIRMATIONS_WEIGHT_LOSS", 0, 1179797074);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractZenMode_ITITResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_GENERAL = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 20, "RESFILE_AFFIRMATIONS_GENERAL", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_POSITIVE_THINKING = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 21, "RESFILE_AFFIRMATIONS_POSITIVE_THINKING", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_PROSPERITY = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 22, "RESFILE_AFFIRMATIONS_PROSPERITY", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_QUIT_BAD_HABITS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 23, "RESFILE_AFFIRMATIONS_QUIT_BAD_HABITS", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_SELF_CONFIDENCE = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 24, "RESFILE_AFFIRMATIONS_SELF_CONFIDENCE", 0, 1230260564);
				GlobalMembersResourcesWP.RESFILE_AFFIRMATIONS_WEIGHT_LOSS = GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, 25, "RESFILE_AFFIRMATIONS_WEIGHT_LOSS", 0, 1230260564);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractZenOptionsResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				if (theManager.mCurArtRes == 480 && !GlobalMembersResourcesWP.ExtractZenOptions_480Resources(theManager))
				{
					return false;
				}
				if (theManager.mCurArtRes == 960 && !GlobalMembersResourcesWP.ExtractZenOptions_960Resources(theManager))
				{
					return false;
				}
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractZenOptions_480Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_ZENOPTIONS_480_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 90, "ATLASIMAGE_ATLAS_ZENOPTIONS_480_00", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_AMBIENT_NONE = GlobalMembersResourcesWP.GetImageThrow(theManager, 779, "IMAGE_ZEN_OPTIONS_AMBIENT_NONE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_COASTAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 780, "IMAGE_ZEN_OPTIONS_COASTAL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_CRICKETS = GlobalMembersResourcesWP.GetImageThrow(theManager, 781, "IMAGE_ZEN_OPTIONS_CRICKETS", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_FOREST = GlobalMembersResourcesWP.GetImageThrow(theManager, 782, "IMAGE_ZEN_OPTIONS_FOREST", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_GENERAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 783, "IMAGE_ZEN_OPTIONS_GENERAL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_MANTRA_NONE = GlobalMembersResourcesWP.GetImageThrow(theManager, 784, "IMAGE_ZEN_OPTIONS_MANTRA_NONE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_OCEAN_SURF = GlobalMembersResourcesWP.GetImageThrow(theManager, 785, "IMAGE_ZEN_OPTIONS_OCEAN_SURF", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_POSITIVE_THINKING = GlobalMembersResourcesWP.GetImageThrow(theManager, 786, "IMAGE_ZEN_OPTIONS_POSITIVE_THINKING", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_PROSPERITY = GlobalMembersResourcesWP.GetImageThrow(theManager, 787, "IMAGE_ZEN_OPTIONS_PROSPERITY", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_QUIT_BAD_HABITS = GlobalMembersResourcesWP.GetImageThrow(theManager, 788, "IMAGE_ZEN_OPTIONS_QUIT_BAD_HABITS", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_RAIN = GlobalMembersResourcesWP.GetImageThrow(theManager, 789, "IMAGE_ZEN_OPTIONS_RAIN", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_RANDOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 790, "IMAGE_ZEN_OPTIONS_RANDOM", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_SELF_CONFIDENCE = GlobalMembersResourcesWP.GetImageThrow(theManager, 791, "IMAGE_ZEN_OPTIONS_SELF_CONFIDENCE", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_WATERFALL = GlobalMembersResourcesWP.GetImageThrow(theManager, 792, "IMAGE_ZEN_OPTIONS_WATERFALL", 480, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_WEIGHT_LOSS = GlobalMembersResourcesWP.GetImageThrow(theManager, 793, "IMAGE_ZEN_OPTIONS_WEIGHT_LOSS", 480, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractZenOptions_960Resources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_ATLAS_ZENOPTIONS_960_00 = GlobalMembersResourcesWP.GetImageThrow(theManager, 91, "ATLASIMAGE_ATLAS_ZENOPTIONS_960_00", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_AMBIENT_NONE = GlobalMembersResourcesWP.GetImageThrow(theManager, 779, "IMAGE_ZEN_OPTIONS_AMBIENT_NONE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_COASTAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 780, "IMAGE_ZEN_OPTIONS_COASTAL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_CRICKETS = GlobalMembersResourcesWP.GetImageThrow(theManager, 781, "IMAGE_ZEN_OPTIONS_CRICKETS", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_FOREST = GlobalMembersResourcesWP.GetImageThrow(theManager, 782, "IMAGE_ZEN_OPTIONS_FOREST", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_GENERAL = GlobalMembersResourcesWP.GetImageThrow(theManager, 783, "IMAGE_ZEN_OPTIONS_GENERAL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_MANTRA_NONE = GlobalMembersResourcesWP.GetImageThrow(theManager, 784, "IMAGE_ZEN_OPTIONS_MANTRA_NONE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_OCEAN_SURF = GlobalMembersResourcesWP.GetImageThrow(theManager, 785, "IMAGE_ZEN_OPTIONS_OCEAN_SURF", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_POSITIVE_THINKING = GlobalMembersResourcesWP.GetImageThrow(theManager, 786, "IMAGE_ZEN_OPTIONS_POSITIVE_THINKING", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_PROSPERITY = GlobalMembersResourcesWP.GetImageThrow(theManager, 787, "IMAGE_ZEN_OPTIONS_PROSPERITY", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_QUIT_BAD_HABITS = GlobalMembersResourcesWP.GetImageThrow(theManager, 788, "IMAGE_ZEN_OPTIONS_QUIT_BAD_HABITS", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_RAIN = GlobalMembersResourcesWP.GetImageThrow(theManager, 789, "IMAGE_ZEN_OPTIONS_RAIN", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_RANDOM = GlobalMembersResourcesWP.GetImageThrow(theManager, 790, "IMAGE_ZEN_OPTIONS_RANDOM", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_SELF_CONFIDENCE = GlobalMembersResourcesWP.GetImageThrow(theManager, 791, "IMAGE_ZEN_OPTIONS_SELF_CONFIDENCE", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_WATERFALL = GlobalMembersResourcesWP.GetImageThrow(theManager, 792, "IMAGE_ZEN_OPTIONS_WATERFALL", 960, 0);
				GlobalMembersResourcesWP.IMAGE_ZEN_OPTIONS_WEIGHT_LOSS = GlobalMembersResourcesWP.GetImageThrow(theManager, 793, "IMAGE_ZEN_OPTIONS_WEIGHT_LOSS", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static bool ExtractAtlasExResources(ResourceManager theManager)
		{
			GlobalMembersResourcesWP.InitResourceManager(theManager);
			try
			{
				GlobalMembersResourcesWP.ATLASIMAGE_EX_ARROW = GlobalMembersResourcesWP.GetImageThrow(theManager, 1806, "ATLASIMAGE_EX_ARROW", 960, 0);
				GlobalMembersResourcesWP.ATLASIMAGE_EX_HELP_LIGHTNING_01 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1807, "ATLASIMAGE_EX_HELP_LIGHTNING_01", 960, 0);
				GlobalMembersResourcesWP.ATLASIMAGE_EX_HELP_LIGHTNING_02 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1808, "ATLASIMAGE_EX_HELP_LIGHTNING_02", 960, 0);
				GlobalMembersResourcesWP.ATLASIMAGE_EX_HELP_LIGHTNING_03 = GlobalMembersResourcesWP.GetImageThrow(theManager, 1809, "ATLASIMAGE_EX_HELP_LIGHTNING_03", 960, 0);
			}
			catch (ResourceManagerException)
			{
				return false;
			}
			return true;
		}

		public static Image GetImageById(int theId)
		{
			if (theId == -1)
			{
				return null;
			}
			return (Image)GlobalMembersResourcesWP.gResources[theId].mResObject;
		}

		public static Font GetFontById(int theId)
		{
			if (theId == -1)
			{
				return null;
			}
			return (Font)GlobalMembersResourcesWP.gResources[theId].mResObject;
		}

		public static int GetSoundById(int theId)
		{
			if (theId == -1)
			{
				return -1;
			}
			return (int)GlobalMembersResourcesWP.gResources[theId].mResObject;
		}

		public static GenericResFile GetGenericResFileById(int theId)
		{
			if (theId == -1)
			{
				return null;
			}
			return (GenericResFile)GlobalMembersResourcesWP.gResources[theId].mResObject;
		}

		public static float ImgXOfs(int theId)
		{
			if (theId == -1)
			{
				return 0f;
			}
			Point point = GlobalMembersResourcesWP.gImgOffsets[theId];
			return (float)GlobalMembersResourcesWP.gImgOffsets[theId].mX * 1.25f;
		}

		public static float ImgYOfs(int theId)
		{
			if (theId == -1)
			{
				return 0f;
			}
			Point point = GlobalMembersResourcesWP.gImgOffsets[theId];
			return (float)GlobalMembersResourcesWP.gImgOffsets[theId].mY * 1.25f;
		}

		public static float ImgXOfs(ResourceId theId)
		{
			return GlobalMembersResourcesWP.ImgXOfs((int)theId);
		}

		public static float ImgYOfs(ResourceId theId)
		{
			return GlobalMembersResourcesWP.ImgYOfs((int)theId);
		}

		public static Image GetImageRefById(int theId)
		{
			return (Image)GlobalMembersResourcesWP.gResources[theId].mResObject;
		}

		public static Font GetFontRefById(int theId)
		{
			return (Font)GlobalMembersResourcesWP.gResources[theId].mResObject;
		}

		public static int GetSoundRefById(int theId)
		{
			return (int)GlobalMembersResourcesWP.gResources[theId].mResObject;
		}

		public static GenericResFile GetGenericResFileRefById(int theId)
		{
			return (GenericResFile)GlobalMembersResourcesWP.gResources[theId].mResObject;
		}

		public static ResourceId GetIdByImage(Image theImage)
		{
			return GlobalMembersResourcesWP.GetIdByVariable(theImage);
		}

		public static ResourceId GetIdByFont(Font theFont)
		{
			return GlobalMembersResourcesWP.GetIdByVariable(theFont);
		}

		public static ResourceId GetIdBySound(int theSound)
		{
			return GlobalMembersResourcesWP.GetIdByVariable((IntPtr)theSound);
		}

		public static ResourceId GetIdByGenericResFile(GenericResFile theFile)
		{
			return GlobalMembersResourcesWP.GetIdByVariable(theFile);
		}

		public static string GetStringIdById(int theId)
		{
			switch (theId)
			{
			case 0:
				return "FONT_HIRES_DIALOG";
			case 1:
				return "FONT_LOWRES_DIALOG";
			case 2:
				return "FONT_HIRES_HEADER";
			case 3:
				return "FONT_LOWRES_HEADER";
			case 4:
				return "FONT_HIRES_SUBHEADER";
			case 5:
				return "FONT_LOWRES_SUBHEADER";
			case 6:
				return "FONT_HIRES_INGAME";
			case 7:
				return "FONT_LOWRES_INGAME";
			case 8:
				return "RESFILE_PROPERTIES_BADGES";
			case 9:
				return "RESFILE_PROPERTIES_DEFAULT";
			case 10:
				return "RESFILE_PROPERTIES_DEFAULTFILENAMES";
			case 11:
				return "RESFILE_PROPERTIES_DEFAULTFRAMEWORK";
			case 12:
				return "RESFILE_PROPERTIES_DEFAULTQUEST";
			case 13:
				return "RESFILE_PROPERTIES_DEFAULTUICONSTANTS";
			case 14:
				return "RESFILE_PROPERTIES_MUSIC";
			case 15:
				return "RESFILE_PROPERTIES_QUEST";
			case 16:
				return "RESFILE_PROPERTIES_RANKS";
			case 17:
				return "RESFILE_PROPERTIES_SECRET";
			case 18:
				return "RESFILE_PROPERTIES_SPEED";
			case 19:
				return "RESFILE_PROPERTIES_TIPS";
			case 20:
				return "RESFILE_AFFIRMATIONS_GENERAL";
			case 21:
				return "RESFILE_AFFIRMATIONS_POSITIVE_THINKING";
			case 22:
				return "RESFILE_AFFIRMATIONS_PROSPERITY";
			case 23:
				return "RESFILE_AFFIRMATIONS_QUIT_BAD_HABITS";
			case 24:
				return "RESFILE_AFFIRMATIONS_SELF_CONFIDENCE";
			case 25:
				return "RESFILE_AFFIRMATIONS_WEIGHT_LOSS";
			case 26:
				return "RESFILE_AMBIENT_COASTAL";
			case 27:
				return "RESFILE_AMBIENT_CRICKETS";
			case 28:
				return "RESFILE_AMBIENT_FOREST";
			case 29:
				return "RESFILE_AMBIENT_OCEAN_SURF";
			case 30:
				return "RESFILE_AMBIENT_RAIN_LEAVES";
			case 31:
				return "RESFILE_AMBIENT_WATERFALL";
			case 32:
				return "ATLASIMAGE_ATLAS_BADGES_480_00";
			case 33:
				return "ATLASIMAGE_ATLAS_BADGES_960_00";
			case 34:
				return "ATLASIMAGE_ATLAS_COMMON_480_00";
			case 35:
				return "ATLASIMAGE_ATLAS_COMMON_960_00";
			case 36:
				return "ATLASIMAGE_ATLAS_GAMEPLAY_480_00";
			case 37:
				return "ATLASIMAGE_ATLAS_GAMEPLAY_960_00";
			case 38:
				return "ATLASIMAGE_ATLAS_GAMEPLAY_UI_DIG_480_00";
			case 39:
				return "ATLASIMAGE_ATLAS_GAMEPLAY_UI_DIG_960_00";
			case 40:
				return "ATLASIMAGE_ATLAS_GAMEPLAY_UI_NORMAL_480_00";
			case 41:
				return "ATLASIMAGE_ATLAS_GAMEPLAY_UI_NORMAL_960_00";
			case 42:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BALANCE_480_00";
			case 43:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BALANCE_960_00";
			case 44:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BUTTERFLY_480_00";
			case 45:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BUTTERFLY_960_00";
			case 46:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_DIG_480_00";
			case 47:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_DIG_960_00";
			case 48:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_FILLER_480_00";
			case 49:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_FILLER_960_00";
			case 50:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_INFERNO_480_00";
			case 51:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_INFERNO_960_00";
			case 52:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_LIGHTNING_480_00";
			case 53:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_LIGHTNING_960_00";
			case 54:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_POKER_480_00";
			case 55:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_POKER_960_00";
			case 56:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_TIMEBOMB_480_00";
			case 57:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_TIMEBOMB_960_00";
			case 58:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_WALLBLAST_480_00";
			case 59:
				return "ATLASIMAGE_ATLAS_GAMEPLAYQUEST_WALLBLAST_960_00";
			case 60:
				return "ATLASIMAGE_ATLAS_GIFTGAME_480_00";
			case 61:
				return "ATLASIMAGE_ATLAS_GIFTGAME_960_00";
			case 62:
				return "ATLASIMAGE_ATLAS_HELP_BASIC_480_00";
			case 63:
				return "ATLASIMAGE_ATLAS_HELP_BASIC_960_00";
			case 64:
				return "ATLASIMAGE_ATLAS_HELP_BFLY_480_00";
			case 65:
				return "ATLASIMAGE_ATLAS_HELP_BFLY_960_00";
			case 66:
				return "ATLASIMAGE_ATLAS_HELP_DIAMONDMINE_480_00";
			case 67:
				return "ATLASIMAGE_ATLAS_HELP_DIAMONDMINE_960_00";
			case 68:
				return "ATLASIMAGE_ATLAS_HELP_ICESTORM_480_00";
			case 69:
				return "ATLASIMAGE_ATLAS_HELP_ICESTORM_960_00";
			case 70:
				return "ATLASIMAGE_ATLAS_HELP_LIGHTNING_480_00";
			case 71:
				return "ATLASIMAGE_ATLAS_HELP_LIGHTNING_960_00";
			case 72:
				return "ATLASIMAGE_ATLAS_HELP_POKER_480_00";
			case 73:
				return "ATLASIMAGE_ATLAS_HELP_POKER_960_00";
			case 74:
				return "ATLASIMAGE_ATLAS_HELP_UNUSED_480_00";
			case 75:
				return "ATLASIMAGE_ATLAS_HELP_UNUSED_960_00";
			case 76:
				return "ATLASIMAGE_ATLAS_HIDDENOBJECT_480_00";
			case 77:
				return "ATLASIMAGE_ATLAS_HIDDENOBJECT_960_00";
			case 78:
				return "ATLASIMAGE_ATLAS_IGNORED_480_00";
			case 79:
				return "ATLASIMAGE_ATLAS_IGNORED_960_00";
			case 80:
				return "ATLASIMAGE_ATLAS_LOADER_480_00";
			case 81:
				return "ATLASIMAGE_ATLAS_LOADER_960_00";
			case 82:
				return "ATLASIMAGE_ATLAS_MAINMENU_480_00";
			case 83:
				return "ATLASIMAGE_ATLAS_MAINMENU_960_00";
			case 84:
				return "ATLASIMAGE_ATLAS_NOMATCH_480_00";
			case 85:
				return "ATLASIMAGE_ATLAS_NOMATCH_960_00";
			case 86:
				return "ATLASIMAGE_ATLAS_QUESTHELP_480_00";
			case 87:
				return "ATLASIMAGE_ATLAS_QUESTHELP_960_00";
			case 88:
				return "ATLASIMAGE_ATLAS_RATEGAME_480_00";
			case 89:
				return "ATLASIMAGE_ATLAS_RATEGAME_960_00";
			case 90:
				return "ATLASIMAGE_ATLAS_ZENOPTIONS_480_00";
			case 91:
				return "ATLASIMAGE_ATLAS_ZENOPTIONS_960_00";
			case 92:
				return "IMAGE_ANIMS_CARD_GEM_SPARKLE2_0_SMALL_BLUR_STAR";
			case 93:
				return "IMAGE_HELP_BUTTERFLY_HELP_0_SMALL_BLUR_STAR";
			case 94:
				return "IMAGE_HELP_CARD_GEM_SPARKLE2_0_SMALL_BLUR_STAR";
			case 95:
				return "IMAGE_HELP_DIAMOND_SPARKLE_0_SMALL_BLUR_STAR";
			case 96:
				return "IMAGE_HELP_FLAMEGEM_HELP_0_FLAME63";
			case 97:
				return "IMAGE_HELP_FLAMEGEM_HELP_1_HELP_GREEN_NOSHDW";
			case 98:
				return "IMAGE_HELP_FLAMEGEM_HELP_2_SPARKLET";
			case 99:
				return "IMAGE_HELP_ICESTORM_HELP_0_RING";
			case 100:
				return "IMAGE_HELP_LIGHTNING_STEAMPULSE_0_BASIC_BLUR";
			case 101:
				return "IMAGE_HELP_STARGEM_HELP_0_SMALL_BLUR_STAR";
			case 102:
				return "IMAGE_HELP_STARGEM_HELP_1_STAR_GLOW";
			case 103:
				return "IMAGE_HELP_STARGEM_HELP_2_CORONAGLOW";
			case 104:
				return "IMAGE_HELP_STARGEM_HELP_3_HELP_GREEN_NOSHDW";
			case 105:
				return "IMAGE_PARTICLES_CRYSTALRAYS_0_RAY";
			case 106:
				return "IMAGE_PARTICLES_CRYSTALBALL_0_BASIC_BLUR";
			case 107:
				return "IMAGE_PARTICLES_QUEST_DIG_COLLECT_BASE_0_SMALL_BLUR_STAR";
			case 108:
				return "IMAGE_PARTICLES_QUEST_DIG_COLLECT_BASE_1_BASIC_BLUR";
			case 109:
				return "IMAGE_PARTICLES_QUEST_DIG_COLLECT_GOLD_0_SMALL_BLUR_STAR";
			case 110:
				return "IMAGE_PARTICLES_QUEST_DIG_COLLECT_GOLD_1_BASIC_BLUR";
			case 111:
				return "IMAGE_PARTICLES_GEM_SANDSTORM_DIG_0_SOFT_CLUMPY";
			case 112:
				return "IMAGE_PARTICLES_BADGE_UPGRADE_0_ICE";
			case 113:
				return "IMAGE_PARTICLES_BADGE_UPGRADE_1_BADGE_GLOW";
			case 114:
				return "IMAGE_PARTICLES_BADGE_UPGRADE_2_CERCLEM";
			case 115:
				return "IMAGE_PARTICLES_BLOWING_SNOW_0_TEXTURE_01";
			case 116:
				return "IMAGE_PARTICLES_BOARD_FLAME_EMBERS_0_BASIC_BLUR";
			case 117:
				return "IMAGE_PARTICLES_CARD_GEM_SPARKLE_0_SMALL_BLUR_STAR";
			case 118:
				return "IMAGE_PARTICLES_COINSPARKLE_0_FLARE";
			case 119:
				return "IMAGE_PARTICLES_COUNTDOWNBAR_0_SMALL_BLUR_STAR";
			case 120:
				return "IMAGE_PARTICLES_COUNTDOWNBAR_1_BASIC_BLUR";
			case 121:
				return "IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_0_BASIC_BLUR";
			case 122:
				return "IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_1_SNOWFLAKE";
			case 123:
				return "IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_2_ICECHUNK";
			case 124:
				return "IMAGE_PARTICLES_DANGERSNOW_SOFT_0_BASIC_BLUR";
			case 125:
				return "IMAGE_PARTICLES_DANGERSNOW_SOFT_1_SNOWFLAKE";
			case 126:
				return "IMAGE_PARTICLES_DISCOBALL_0_DISCO_GLOW";
			case 127:
				return "IMAGE_PARTICLES_DISCOBALL_1_DISCO_GLOW";
			case 128:
				return "IMAGE_PARTICLES_DISCOBALL_2_BLURRED_SHARP_STAR";
			case 129:
				return "IMAGE_PARTICLES_FIREGEM_HYPERSPACE_0_BASIC_BLUR";
			case 130:
				return "IMAGE_PARTICLES_FLAME_CARD_0_BASIC_BLUR";
			case 131:
				return "IMAGE_PARTICLES_GEM_BLASTGEM_0_BASIC_BLUR";
			case 132:
				return "IMAGE_PARTICLES_GEM_BUTTERFLY_0_SMALL_BLUR_STAR";
			case 133:
				return "IMAGE_PARTICLES_GEM_BUTTERFLY_CREATE_0_FLOWER";
			case 134:
				return "IMAGE_PARTICLES_GEM_BUTTERFLY_CREATE_1_BLURRED_SHARP_STAR";
			case 135:
				return "IMAGE_PARTICLES_GEM_DIAMOND_SPARKLES_0_BLURRED_SHARP_STAR";
			case 136:
				return "IMAGE_PARTICLES_GEM_FIRE_TRAIL_0_BASIC_BLUR";
			case 137:
				return "IMAGE_PARTICLES_GEM_GOLD_BLING_0_BLURRED_SHARP_STAR";
			case 138:
				return "IMAGE_PARTICLES_GEM_HINTFLASH_0_CERCLEM";
			case 139:
				return "IMAGE_PARTICLES_GEM_HYPERCUBE_0_BASIC_BLUR";
			case 140:
				return "IMAGE_PARTICLES_GEM_ICE_TRAIL_0_BLURRED_SHARP_STAR";
			case 141:
				return "IMAGE_PARTICLES_GEM_ICE_TRAIL_1_COMIC_SMOKE2";
			case 142:
				return "IMAGE_PARTICLES_GEM_MULTIPLIER_0_RAY";
			case 143:
				return "IMAGE_PARTICLES_GEM_STARGEM_0_SMALL_BLUR_STAR";
			case 144:
				return "IMAGE_PARTICLES_GEM_STARGEM_1_STAR_GLOW";
			case 145:
				return "IMAGE_PARTICLES_GEM_STARGEM_2_CORONAGLOW";
			case 146:
				return "IMAGE_PARTICLES_ICE_STORMY_0_SHARD";
			case 147:
				return "IMAGE_PARTICLES_LEVELBAR_0_SMALL_BLUR_STAR";
			case 148:
				return "IMAGE_PARTICLES_LEVELBAR_1_BASIC_BLUR";
			case 149:
				return "IMAGE_PARTICLES_SANDSTORM_COVER_0_SAND_PARTICLE";
			case 150:
				return "IMAGE_PARTICLES_SKULL_EXPLODE_0_DOT_STREAK_01";
			case 151:
				return "IMAGE_PARTICLES_SKULL_EXPLODE_1_SKULL";
			case 152:
				return "IMAGE_PARTICLES_SKULL_EXPLODE_2_SMALL_BLUR_STAR";
			case 153:
				return "IMAGE_PARTICLES_SPEEDBOARD_FLAME_0_FLAME1";
			case 154:
				return "IMAGE_PARTICLES_STAR_CARD_0_BLURRED_SHARP_STAR";
			case 155:
				return "IMAGE_PARTICLES_STAR_CARD_1_SMALL_BLUR_STAR";
			case 156:
				return "IMAGE_PARTICLES_STARBURST_0_STAR";
			case 157:
				return "IMAGE_PARTICLES_WEIGHT_FIRE_0_TRUEFLAME7";
			case 158:
				return "IMAGE_PARTICLES_WEIGHT_FIRE_1_BASIC_BLUR";
			case 159:
				return "IMAGE_PARTICLES_WEIGHT_ICE_0_DIM_BLUR_CLOUD";
			case 160:
				return "IMAGE_PARTICLES_WEIGHT_ICE_1_ICESHARD_0000";
			case 161:
				return "IMAGE_QUEST_DIG_DIG_LINE_HIT_0_BASIC_BLUR";
			case 162:
				return "IMAGE_QUEST_DIG_DIG_LINE_HIT_MEGA_0_BLURRED_SHARP_STAR";
			case 163:
				return "IMAGE_QUEST_DIG_DIG_LINE_HIT_MEGA_1_DIAMOND_STAR";
			case 164:
				return "IMAGE_BACKGROUNDS_DAVE_CAVE_THING";
			case 165:
				return "IMAGE_BACKGROUNDS_DESERT";
			case 166:
				return "IMAGE_BACKGROUNDS_FLOATING_ROCK_CITY";
			case 167:
				return "IMAGE_BACKGROUNDS_SNOWY_CLIFFS_CASTLE";
			case 168:
				return "IMAGE_BACKGROUNDS_TUBE_FOREST_NIGHT";
			case 169:
				return "IMAGE_BACKGROUNDS_WATER_BUBBLES_CITY";
			case 170:
				return "IMAGE_BACKGROUNDS_WATER_FALL_CLIFF";
			case 171:
				return "IMAGE_BACKGROUNDS_WATER_PATH_RUINS";
			case 172:
				return "IMAGE_BACKGROUNDS_BRIDGE_SHROOM_CASTLE";
			case 173:
				return "IMAGE_BACKGROUNDS_CRYSTALTOWERS";
			case 174:
				return "IMAGE_BACKGROUNDS_FLYING_SAIL_BOAT";
			case 175:
				return "IMAGE_BACKGROUNDS_LANTERN_PLANTS_WORLD";
			case 176:
				return "IMAGE_BACKGROUNDS_LION_TOWER_CASCADE";
			case 177:
				return "IMAGE_BACKGROUNDS_LION_TOWER_CASCADE_BFLY";
			case 178:
				return "IMAGE_BACKGROUNDS_POKER";
			case 179:
				return "IMAGE_BACKGROUNDS_POINTY_ICE_PATH";
			case 180:
				return "IMAGE_BACKGROUNDS_CAVE";
			case 181:
				return "IMAGE_BADGES_BIG_ANNIHILATOR";
			case 182:
				return "IMAGE_BADGES_BIG_ANTE_UP";
			case 183:
				return "IMAGE_BADGES_BIG_BEJEWELER";
			case 184:
				return "IMAGE_BADGES_BIG_BLASTER";
			case 185:
				return "IMAGE_BADGES_BIG_BRONZE";
			case 186:
				return "IMAGE_BADGES_BIG_BUTTERFLY_BONANZA";
			case 187:
				return "IMAGE_BADGES_BIG_BUTTERFLY_MONARCH";
			case 188:
				return "IMAGE_BADGES_BIG_CHROMATIC";
			case 189:
				return "IMAGE_BADGES_BIG_DIAMOND_MINE";
			case 190:
				return "IMAGE_BADGES_BIG_ELECTRIFIER";
			case 191:
				return "IMAGE_BADGES_BIG_ELITE";
			case 192:
				return "IMAGE_BADGES_BIG_GLACIAL_EXPLORER";
			case 193:
				return "IMAGE_BADGES_BIG_GOLD";
			case 194:
				return "IMAGE_BADGES_BIG_HEROES_WELCOME";
			case 195:
				return "IMAGE_BADGES_BIG_HIGH_VOLTAGE";
			case 196:
				return "IMAGE_BADGES_BIG_ICE_BREAKER";
			case 197:
				return "IMAGE_BADGES_BIG_INFERNO";
			case 198:
				return "IMAGE_BADGES_BIG_LEVELORD";
			case 199:
				return "IMAGE_BADGES_BIG_PLATINUM";
			case 200:
				return "IMAGE_BADGES_BIG_RELIC_HUNTER";
			case 201:
				return "IMAGE_BADGES_BIG_SILVER";
			case 202:
				return "IMAGE_BADGES_BIG_STELLAR";
			case 203:
				return "IMAGE_BADGES_BIG_SUPERSTAR";
			case 204:
				return "IMAGE_BADGES_BIG_THE_GAMBLER";
			case 205:
				return "IMAGE_BADGES_BIG_TOP_SECRET";
			case 206:
				return "IMAGE_BADGES_BIG_CHAIN_REACTION";
			case 207:
				return "IMAGE_BADGES_BIG_DYNAMO";
			case 208:
				return "IMAGE_BADGES_BIG_LUCKY_STREAK";
			case 209:
				return "IMAGE_BADGES_BIG_MILLIONAIRE";
			case 210:
				return "IMAGE_AWARD_GLOW";
			case 211:
				return "IMAGE_HELP_SWAP3_SWAP3_128X128";
			case 212:
				return "IMAGE_HELP_SWAP3_SWAP3_128X128_2";
			case 213:
				return "IMAGE_HELP_SWAP3_SWAP3_128X128_3";
			case 214:
				return "IMAGE_HELP_SWAP3_SWAP3_128X128_4";
			case 215:
				return "IMAGE_HELP_SWAP3_SWAP3_128X128_5";
			case 216:
				return "IMAGE_HELP_SWAP3_SWAP3_384X384";
			case 217:
				return "IMAGE_HELP_SWAP3_SWAP3_50X43";
			case 218:
				return "IMAGE_HELP_MATCH4_MATCH4_128X128";
			case 219:
				return "IMAGE_HELP_MATCH4_MATCH4_128X128_2";
			case 220:
				return "IMAGE_HELP_MATCH4_MATCH4_128X128_3";
			case 221:
				return "IMAGE_HELP_MATCH4_MATCH4_128X128_4";
			case 222:
				return "IMAGE_HELP_MATCH4_MATCH4_128X128_5";
			case 223:
				return "IMAGE_HELP_MATCH4_MATCH4_128X128_6";
			case 224:
				return "IMAGE_HELP_MATCH4_MATCH4_128X128_7";
			case 225:
				return "IMAGE_HELP_MATCH4_MATCH4_291X384";
			case 226:
				return "IMAGE_HELP_MATCH4_MATCH4_50X43";
			case 227:
				return "IMAGE_HELP_STARGEM_STARGEM_128X128";
			case 228:
				return "IMAGE_HELP_STARGEM_STARGEM_128X128_2";
			case 229:
				return "IMAGE_HELP_STARGEM_STARGEM_128X128_3";
			case 230:
				return "IMAGE_HELP_STARGEM_STARGEM_128X128_4";
			case 231:
				return "IMAGE_HELP_STARGEM_STARGEM_128X128_5";
			case 232:
				return "IMAGE_HELP_STARGEM_STARGEM_128X128_6";
			case 233:
				return "IMAGE_HELP_STARGEM_STARGEM_128X128_7";
			case 234:
				return "IMAGE_HELP_STARGEM_STARGEM_128X128_8";
			case 235:
				return "IMAGE_HELP_STARGEM_STARGEM_291X384";
			case 236:
				return "IMAGE_HELP_STARGEM_STARGEM_50X43";
			case 237:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_11X14";
			case 238:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128";
			case 239:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_2";
			case 240:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_3";
			case 241:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_4";
			case 242:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_5";
			case 243:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_6";
			case 244:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_7";
			case 245:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_8";
			case 246:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_13X15";
			case 247:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_18X34";
			case 248:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_20X15";
			case 249:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_22X22";
			case 250:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_27X29";
			case 251:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104";
			case 252:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104_2";
			case 253:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104_3";
			case 254:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_292X384";
			case 255:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_30X35";
			case 256:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_34X31";
			case 257:
				return "IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_50X43";
			case 258:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128";
			case 259:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_2";
			case 260:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_3";
			case 261:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_4";
			case 262:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_5";
			case 263:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_6";
			case 264:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_7";
			case 265:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_8";
			case 266:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_13X15";
			case 267:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_20X15";
			case 268:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_258X338";
			case 269:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_50X43";
			case 270:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X1";
			case 271:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X9";
			case 272:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X13";
			case 273:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X17";
			case 274:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X20";
			case 275:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X22";
			case 276:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X26";
			case 277:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X31";
			case 278:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X36";
			case 279:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X40";
			case 280:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X42";
			case 281:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X46";
			case 282:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X51";
			case 283:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X56";
			case 284:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66";
			case 285:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66_2";
			case 286:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66_3";
			case 287:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X71";
			case 288:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X74";
			case 289:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X81";
			case 290:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X81_2";
			case 291:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_83X81";
			case 292:
				return "IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_83X81_2";
			case 293:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_11X14";
			case 294:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128";
			case 295:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_2";
			case 296:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_3";
			case 297:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_4";
			case 298:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_5";
			case 299:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_6";
			case 300:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_7";
			case 301:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_13X15";
			case 302:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_20X15";
			case 303:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_24X29";
			case 304:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_27X26";
			case 305:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_291X104";
			case 306:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_291X104_2";
			case 307:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_292X384";
			case 308:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_50X43";
			case 309:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_54X41";
			case 310:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_78X82";
			case 311:
				return "IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_78X82_2";
			case 312:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128";
			case 313:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_2";
			case 314:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_3";
			case 315:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_4";
			case 316:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_5";
			case 317:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_6";
			case 318:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_7";
			case 319:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_13X52";
			case 320:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_18X19";
			case 321:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_19X25";
			case 322:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30";
			case 323:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_2";
			case 324:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_3";
			case 325:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_4";
			case 326:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_5";
			case 327:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_6";
			case 328:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_7";
			case 329:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_23X34";
			case 330:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_25X37";
			case 331:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_27X54";
			case 332:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_35X14";
			case 333:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_383X504";
			case 334:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84";
			case 335:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_2";
			case 336:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_3";
			case 337:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_4";
			case 338:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_5";
			case 339:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_6";
			case 340:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_7";
			case 341:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_8";
			case 342:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_48X25";
			case 343:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_50X43";
			case 344:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88";
			case 345:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_2";
			case 346:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_3";
			case 347:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_4";
			case 348:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_74X56";
			case 349:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90";
			case 350:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_2";
			case 351:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_3";
			case 352:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_4";
			case 353:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_5";
			case 354:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_6";
			case 355:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_7";
			case 356:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_8";
			case 357:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_9";
			case 358:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_10";
			case 359:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_11";
			case 360:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_12";
			case 361:
				return "IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_93X62";
			case 362:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128";
			case 363:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_2";
			case 364:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_3";
			case 365:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_4";
			case 366:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_5";
			case 367:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_13X52";
			case 368:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_147X93";
			case 369:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_161X68";
			case 370:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_168X72";
			case 371:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_168X79";
			case 372:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_18X19";
			case 373:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_19X25";
			case 374:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30";
			case 375:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30_2";
			case 376:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30_3";
			case 377:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_23X34";
			case 378:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_25X37";
			case 379:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36";
			case 380:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_2";
			case 381:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_3";
			case 382:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_4";
			case 383:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_5";
			case 384:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_6";
			case 385:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_7";
			case 386:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_8";
			case 387:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_9";
			case 388:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_10";
			case 389:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_27X54";
			case 390:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232";
			case 391:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_2";
			case 392:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_3";
			case 393:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_4";
			case 394:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_5";
			case 395:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_317X342";
			case 396:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_31X57";
			case 397:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_35X14";
			case 398:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_383X504";
			case 399:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84";
			case 400:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_2";
			case 401:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_3";
			case 402:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_4";
			case 403:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_5";
			case 404:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_6";
			case 405:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_7";
			case 406:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_8";
			case 407:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_48X25";
			case 408:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_49X69";
			case 409:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88";
			case 410:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_2";
			case 411:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_3";
			case 412:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_4";
			case 413:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_74X56";
			case 414:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_74X56_2";
			case 415:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_75X34";
			case 416:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_75X35";
			case 417:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_83X66";
			case 418:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_83X66_2";
			case 419:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_8X10";
			case 420:
				return "IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_93X62";
			case 421:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43";
			case 422:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_2";
			case 423:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_3";
			case 424:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_4";
			case 425:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_168X72";
			case 426:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_168X79";
			case 427:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30";
			case 428:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_2";
			case 429:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_3";
			case 430:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_4";
			case 431:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_5";
			case 432:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_6";
			case 433:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_7";
			case 434:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_8";
			case 435:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30";
			case 436:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_2";
			case 437:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_3";
			case 438:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_4";
			case 439:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_5";
			case 440:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_6";
			case 441:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36";
			case 442:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_2";
			case 443:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_3";
			case 444:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_4";
			case 445:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_5";
			case 446:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_6";
			case 447:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_7";
			case 448:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_8";
			case 449:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_9";
			case 450:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_10";
			case 451:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_272X153";
			case 452:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_27X35";
			case 453:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_27X35_2";
			case 454:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_31X57";
			case 455:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_32X34";
			case 456:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_32X34_2";
			case 457:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_383X504";
			case 458:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_383X504_2";
			case 459:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43";
			case 460:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_2";
			case 461:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_3";
			case 462:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_4";
			case 463:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_5";
			case 464:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_75X35";
			case 465:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_83X66";
			case 466:
				return "IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_8X10";
			case 467:
				return "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128";
			case 468:
				return "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_2";
			case 469:
				return "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_3";
			case 470:
				return "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_4";
			case 471:
				return "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_5";
			case 472:
				return "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_6";
			case 473:
				return "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_384X384";
			case 474:
				return "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_390X519";
			case 475:
				return "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_42X88";
			case 476:
				return "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_50X43";
			case 477:
				return "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_52X117";
			case 478:
				return "IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_53X95";
			case 479:
				return "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128";
			case 480:
				return "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_2";
			case 481:
				return "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_3";
			case 482:
				return "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_4";
			case 483:
				return "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_5";
			case 484:
				return "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_6";
			case 485:
				return "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_7";
			case 486:
				return "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_8";
			case 487:
				return "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_389X70";
			case 488:
				return "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_390X519";
			case 489:
				return "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_50X43";
			case 490:
				return "IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_82X164";
			case 491:
				return "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128";
			case 492:
				return "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_2";
			case 493:
				return "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_3";
			case 494:
				return "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_4";
			case 495:
				return "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_5";
			case 496:
				return "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_6";
			case 497:
				return "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_7";
			case 498:
				return "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_8";
			case 499:
				return "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_393X502";
			case 500:
				return "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_50X43";
			case 501:
				return "IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_88X278";
			case 502:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_106X234";
			case 503:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_111X55";
			case 504:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20";
			case 505:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_2";
			case 506:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_3";
			case 507:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_4";
			case 508:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_5";
			case 509:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_6";
			case 510:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_7";
			case 511:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_8";
			case 512:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_9";
			case 513:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_10";
			case 514:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_11";
			case 515:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_12";
			case 516:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_13";
			case 517:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_128X128";
			case 518:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36";
			case 519:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_2";
			case 520:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_3";
			case 521:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_4";
			case 522:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_5";
			case 523:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_6";
			case 524:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_7";
			case 525:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_8";
			case 526:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_9";
			case 527:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_10";
			case 528:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_394X502";
			case 529:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_85X68";
			case 530:
				return "IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_8X10";
			case 531:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_118X44";
			case 532:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128";
			case 533:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_2";
			case 534:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_3";
			case 535:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_4";
			case 536:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_5";
			case 537:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_6";
			case 538:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_7";
			case 539:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_8";
			case 540:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271";
			case 541:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_2";
			case 542:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_3";
			case 543:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_4";
			case 544:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_5";
			case 545:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_6";
			case 546:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_7";
			case 547:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_8";
			case 548:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_9";
			case 549:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_10";
			case 550:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_11";
			case 551:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_12";
			case 552:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_13";
			case 553:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_14";
			case 554:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_15";
			case 555:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_16";
			case 556:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_17";
			case 557:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_18";
			case 558:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_19";
			case 559:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_20";
			case 560:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_21";
			case 561:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_22";
			case 562:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_23";
			case 563:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_393X502";
			case 564:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_50X43";
			case 565:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_83X38";
			case 566:
				return "IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_88X278";
			case 567:
				return "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128";
			case 568:
				return "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_2";
			case 569:
				return "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_3";
			case 570:
				return "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_4";
			case 571:
				return "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_5";
			case 572:
				return "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_6";
			case 573:
				return "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_7";
			case 574:
				return "IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_8";
			case 575:
				return "IMAGE_HELP_POKER_MATCH_POKER_MATCH_383X510";
			case 576:
				return "IMAGE_HELP_POKER_MATCH_POKER_MATCH_50X43";
			case 577:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_128X128";
			case 578:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_148X158";
			case 579:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_152X48";
			case 580:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_168X60";
			case 581:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_206X38";
			case 582:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_240X52";
			case 583:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36";
			case 584:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_2";
			case 585:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_3";
			case 586:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_4";
			case 587:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_5";
			case 588:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_6";
			case 589:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_7";
			case 590:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_8";
			case 591:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_9";
			case 592:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_10";
			case 593:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_44X38";
			case 594:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_464X687";
			case 595:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_61X35";
			case 596:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_75X76";
			case 597:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_81X110";
			case 598:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_81X110_2";
			case 599:
				return "IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_8X10";
			case 600:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128";
			case 601:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_2";
			case 602:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_3";
			case 603:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_4";
			case 604:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_138X73";
			case 605:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_148X158";
			case 606:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_152X48";
			case 607:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_206X38";
			case 608:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36";
			case 609:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_2";
			case 610:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_3";
			case 611:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_4";
			case 612:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_5";
			case 613:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_6";
			case 614:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_7";
			case 615:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_8";
			case 616:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_9";
			case 617:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_10";
			case 618:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_358X63";
			case 619:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_464X687";
			case 620:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_81X110";
			case 621:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_81X110_2";
			case 622:
				return "IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_8X10";
			case 623:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43";
			case 624:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_2";
			case 625:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_3";
			case 626:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_4";
			case 627:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_5";
			case 628:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_6";
			case 629:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_7";
			case 630:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_8";
			case 631:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_9";
			case 632:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_10";
			case 633:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_11";
			case 634:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_12";
			case 635:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_13";
			case 636:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30";
			case 637:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_2";
			case 638:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_3";
			case 639:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_4";
			case 640:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_5";
			case 641:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_6";
			case 642:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30";
			case 643:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_2";
			case 644:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_3";
			case 645:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_4";
			case 646:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_5";
			case 647:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_6";
			case 648:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_7";
			case 649:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36";
			case 650:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_2";
			case 651:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_3";
			case 652:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_4";
			case 653:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_5";
			case 654:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_6";
			case 655:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_7";
			case 656:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_8";
			case 657:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_9";
			case 658:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_10";
			case 659:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_32X34";
			case 660:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_374X346";
			case 661:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_38X43";
			case 662:
				return "IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_38X43_2";
			case 663:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_156X43";
			case 664:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30";
			case 665:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_2";
			case 666:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_3";
			case 667:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_4";
			case 668:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_5";
			case 669:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_6";
			case 670:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_227X30";
			case 671:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36";
			case 672:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_2";
			case 673:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_3";
			case 674:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_4";
			case 675:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_5";
			case 676:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_6";
			case 677:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_7";
			case 678:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_8";
			case 679:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_9";
			case 680:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_10";
			case 681:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_32X34";
			case 682:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_374X346";
			case 683:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43";
			case 684:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_2";
			case 685:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_3";
			case 686:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_4";
			case 687:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_5";
			case 688:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_6";
			case 689:
				return "IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_7";
			case 690:
				return "IMAGE_CLOCK_ICON";
			case 691:
				return "IMAGE_LOADER_WHITEDOT";
			case 692:
				return "IMAGE_LOADER_POPCAP_BLACK_TM";
			case 693:
				return "IMAGE_LOADER_POPCAP_LOADER_POPCAP";
			case 694:
				return "IMAGE_LOADER_POPCAP_WHITE_GERMAN_REGISTERED";
			case 695:
				return "IMAGE_RATETHEGAME";
			case 696:
				return "IMAGE_GIFTTHEGAME";
			case 697:
				return "IMAGE_POKER_SCORE_BOARD";
			case 698:
				return "IMAGE_MAIN_MENU_LOGO";
			case 699:
				return "IMAGE_MAIN_MENU_CLOUDS";
			case 700:
				return "IMAGE_MAIN_MENU_BACKGROUND";
			case 701:
				return "IMAGE_MAIN_MENU_PILLARL";
			case 702:
				return "IMAGE_MAIN_MENU_PILLARR";
			case 703:
				return "IMAGE_MAIN_MENU_WHITE_GERMAN_REGISTERED";
			case 704:
				return "IMAGE_MAIN_MENU_WHITE_TM";
			case 705:
				return "IMAGE_DASHBOARD_CLOSED_BUTTON";
			case 706:
				return "IMAGE_DASHBOARD_DASH_MAIN";
			case 707:
				return "IMAGE_DASHBOARD_DASH_TILE";
			case 708:
				return "IMAGE_DASHBOARD_DM_OVERLAY";
			case 709:
				return "IMAGE_DASHBOARD_ICE_OVERLAY";
			case 710:
				return "IMAGE_DASHBOARD_MENU_DOWN";
			case 711:
				return "IMAGE_DASHBOARD_MENU_UP";
			case 712:
				return "IMAGE_PP0";
			case 713:
				return "IMAGE_PP1";
			case 714:
				return "IMAGE_PP2";
			case 715:
				return "IMAGE_PP3";
			case 716:
				return "IMAGE_PP4";
			case 717:
				return "IMAGE_PP5";
			case 718:
				return "IMAGE_PP6";
			case 719:
				return "IMAGE_PP7";
			case 720:
				return "IMAGE_PP8";
			case 721:
				return "IMAGE_PP9";
			case 722:
				return "IMAGE_PP10";
			case 723:
				return "IMAGE_PP11";
			case 724:
				return "IMAGE_PP12";
			case 725:
				return "IMAGE_PP13";
			case 726:
				return "IMAGE_PP14";
			case 727:
				return "IMAGE_PP15";
			case 728:
				return "IMAGE_PP16";
			case 729:
				return "IMAGE_PP17";
			case 730:
				return "IMAGE_PP18";
			case 731:
				return "IMAGE_PP19";
			case 732:
				return "IMAGE_PP20";
			case 733:
				return "IMAGE_PP21";
			case 734:
				return "IMAGE_PP22";
			case 735:
				return "IMAGE_PP23";
			case 736:
				return "IMAGE_PP24";
			case 737:
				return "IMAGE_PP25";
			case 738:
				return "IMAGE_PP26";
			case 739:
				return "IMAGE_PP27";
			case 740:
				return "IMAGE_PP28";
			case 741:
				return "IMAGE_PP29";
			case 742:
				return "IMAGE_PPS0";
			case 743:
				return "IMAGE_PPS1";
			case 744:
				return "IMAGE_PPS2";
			case 745:
				return "IMAGE_PPS3";
			case 746:
				return "IMAGE_PPS4";
			case 747:
				return "IMAGE_PPS5";
			case 748:
				return "IMAGE_PPS6";
			case 749:
				return "IMAGE_PPS7";
			case 750:
				return "IMAGE_PPS8";
			case 751:
				return "IMAGE_PPS9";
			case 752:
				return "IMAGE_PPS10";
			case 753:
				return "IMAGE_PPS11";
			case 754:
				return "IMAGE_PPS12";
			case 755:
				return "IMAGE_PPS13";
			case 756:
				return "IMAGE_PPS14";
			case 757:
				return "IMAGE_PPS15";
			case 758:
				return "IMAGE_PPS16";
			case 759:
				return "IMAGE_PPS17";
			case 760:
				return "IMAGE_PPS18";
			case 761:
				return "IMAGE_PPS19";
			case 762:
				return "IMAGE_PPS20";
			case 763:
				return "IMAGE_PPS21";
			case 764:
				return "IMAGE_PPS22";
			case 765:
				return "IMAGE_PPS23";
			case 766:
				return "IMAGE_PPS24";
			case 767:
				return "IMAGE_PPS25";
			case 768:
				return "IMAGE_PPS26";
			case 769:
				return "IMAGE_PPS27";
			case 770:
				return "IMAGE_PPS28";
			case 771:
				return "IMAGE_PPS29";
			case 772:
				return "IMAGE_HYPERSPACE_WHIRLPOOL_INITIAL";
			case 773:
				return "IMAGE_HYPERSPACE_WHIRLPOOL_FIRERING";
			case 774:
				return "IMAGE_HYPERSPACE_WHIRLPOOL_TUNNELEND";
			case 775:
				return "IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE";
			case 776:
				return "IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE_COVER";
			case 777:
				return "IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_NORMAL";
			case 778:
				return "IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_ZEN";
			case 779:
				return "IMAGE_ZEN_OPTIONS_AMBIENT_NONE";
			case 780:
				return "IMAGE_ZEN_OPTIONS_COASTAL";
			case 781:
				return "IMAGE_ZEN_OPTIONS_CRICKETS";
			case 782:
				return "IMAGE_ZEN_OPTIONS_FOREST";
			case 783:
				return "IMAGE_ZEN_OPTIONS_GENERAL";
			case 784:
				return "IMAGE_ZEN_OPTIONS_MANTRA_NONE";
			case 785:
				return "IMAGE_ZEN_OPTIONS_OCEAN_SURF";
			case 786:
				return "IMAGE_ZEN_OPTIONS_POSITIVE_THINKING";
			case 787:
				return "IMAGE_ZEN_OPTIONS_PROSPERITY";
			case 788:
				return "IMAGE_ZEN_OPTIONS_QUIT_BAD_HABITS";
			case 789:
				return "IMAGE_ZEN_OPTIONS_RAIN";
			case 790:
				return "IMAGE_ZEN_OPTIONS_RANDOM";
			case 791:
				return "IMAGE_ZEN_OPTIONS_SELF_CONFIDENCE";
			case 792:
				return "IMAGE_ZEN_OPTIONS_WATERFALL";
			case 793:
				return "IMAGE_ZEN_OPTIONS_WEIGHT_LOSS";
			case 794:
				return "IMAGE_TOOLTIP";
			case 795:
				return "IMAGE_TOOLTIP_ARROW_ARROW_DOWN";
			case 796:
				return "IMAGE_TOOLTIP_ARROW_ARROW_LEFT";
			case 797:
				return "IMAGE_TOOLTIP_ARROW_ARROW_RIGHT";
			case 798:
				return "IMAGE_TOOLTIP_ARROW_ARROW_UP";
			case 799:
				return "IMAGE_CRYSTALBALL_SHADOW";
			case 800:
				return "IMAGE_CRYSTALBALL_GLOW";
			case 801:
				return "IMAGE_CRYSTALBALL";
			case 802:
				return "IMAGE_GEMS_RED";
			case 803:
				return "IMAGE_GEMS_WHITE";
			case 804:
				return "IMAGE_GEMS_GREEN";
			case 805:
				return "IMAGE_GEMS_YELLOW";
			case 806:
				return "IMAGE_GEMS_PURPLE";
			case 807:
				return "IMAGE_GEMS_ORANGE";
			case 808:
				return "IMAGE_GEMS_BLUE";
			case 809:
				return "IMAGE_GEMSSHADOW_RED";
			case 810:
				return "IMAGE_GEMSSHADOW_WHITE";
			case 811:
				return "IMAGE_GEMSSHADOW_GREEN";
			case 812:
				return "IMAGE_GEMSSHADOW_YELLOW";
			case 813:
				return "IMAGE_GEMSSHADOW_PURPLE";
			case 814:
				return "IMAGE_GEMSSHADOW_ORANGE";
			case 815:
				return "IMAGE_GEMSSHADOW_BLUE";
			case 816:
				return "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME1";
			case 817:
				return "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME2";
			case 818:
				return "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME3";
			case 819:
				return "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME4";
			case 820:
				return "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME5";
			case 821:
				return "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME6";
			case 822:
				return "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME7";
			case 823:
				return "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME8";
			case 824:
				return "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME9";
			case 825:
				return "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME10";
			case 826:
				return "IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME11";
			case 827:
				return "IMAGE_FLAMEGEMCREATION_FLAMEGEM_BLUR";
			case 828:
				return "IMAGE_FLAMEGEMCREATION_FLAMEGEM_FLASH_1";
			case 829:
				return "IMAGE_FLAMEGEMCREATION_FLAMEGEM_FLASH_2";
			case 830:
				return "IMAGE_FLAMEGEMCREATION_FLAMEGEM_LARGE_RING";
			case 831:
				return "IMAGE_FLAMEGEMCREATION_FLAMEGEM_RING_OF_FLAME";
			case 832:
				return "IMAGE_FLAMEGEMEXPLODE_FLAMEEXPLODETEST_LAYER_1";
			case 833:
				return "IMAGE_BOOM_NOVA";
			case 834:
				return "IMAGE_BOOM_NUKE";
			case 835:
				return "IMAGE_BOARD_IRIS";
			case 836:
				return "IMAGE_GEMS_SHADOWED";
			case 837:
				return "IMAGE_GEM_FRUIT_SPARK";
			case 838:
				return "IMAGE_SMOKE";
			case 839:
				return "IMAGE_DRIP";
			case 840:
				return "IMAGE_FX_STEAM";
			case 841:
				return "IMAGE_SPARKLET";
			case 842:
				return "IMAGE_DIAMOND_MINE_TEXT_CYCLE";
			case 843:
				return "IMAGE_ELECTROTEX";
			case 844:
				return "IMAGE_ELECTROTEX_CENTER";
			case 845:
				return "IMAGE_HYPERFLARELINE";
			case 846:
				return "IMAGE_HYPERFLARERING";
			case 847:
				return "IMAGE_SELECTOR";
			case 848:
				return "IMAGE_HINTARROW";
			case 849:
				return "IMAGE_DANGERBORDERLEFT";
			case 850:
				return "IMAGE_DANGERBORDERUP";
			case 851:
				return "IMAGE_HYPERCUBE_COLORGLOW";
			case 852:
				return "IMAGE_HYPERCUBE_FRAME";
			case 853:
				return "IMAGE_SHADER_TEST";
			case 854:
				return "IMAGE_LIGHTNING";
			case 855:
				return "IMAGE_GRITTYBLURRY";
			case 856:
				return "IMAGE_LIGHTNING_CENTER";
			case 857:
				return "IMAGE_LIGHTNING_TEX";
			case 858:
				return "IMAGE_SPARKLE";
			case 859:
				return "IMAGE_SM_SHARDS";
			case 860:
				return "IMAGE_SM_SHARDS_OUTLINE";
			case 861:
				return "IMAGE_FIREPARTICLE";
			case 862:
				return "IMAGE_GEMSNORMAL_RED";
			case 863:
				return "IMAGE_GEMSNORMAL_WHITE";
			case 864:
				return "IMAGE_GEMSNORMAL_GREEN";
			case 865:
				return "IMAGE_GEMSNORMAL_YELLOW";
			case 866:
				return "IMAGE_GEMSNORMAL_PURPLE";
			case 867:
				return "IMAGE_GEMSNORMAL_ORANGE";
			case 868:
				return "IMAGE_GEMSNORMAL_BLUE";
			case 869:
				return "IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND";
			case 870:
				return "IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME";
			case 871:
				return "IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_LEVEL";
			case 872:
				return "IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_METER";
			case 873:
				return "IMAGE_INGAMEUI_DIAMOND_MINE_HUD_SHADOW";
			case 874:
				return "IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_BACK";
			case 875:
				return "IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME";
			case 876:
				return "IMAGE_INGAMEUI_DIAMOND_MINE_SCORE_BAR_BACK";
			case 877:
				return "IMAGE_INGAMEUI_DIAMOND_MINE_TIMER";
			case 878:
				return "IMAGE_QUEST_DIG_COGS_COGS_113X114";
			case 879:
				return "IMAGE_QUEST_DIG_COGS_COGS_165X165";
			case 880:
				return "IMAGE_QUEST_DIG_COGS_COGS_166X166";
			case 881:
				return "IMAGE_QUEST_DIG_COGS_COGS_202X202";
			case 882:
				return "IMAGE_QUEST_DIG_COGS_COGS_63X64";
			case 883:
				return "IMAGE_QUEST_DIG_COGS_COGS_63X64_2";
			case 884:
				return "IMAGE_QUEST_DIG_COGS_COGS_89X90";
			case 885:
				return "IMAGE_QUEST_DIG_COGS_COGS_90X90";
			case 886:
				return "IMAGE_QUEST_DIG_COGS_COGS_96X96";
			case 887:
				return "IMAGE_QUEST_DIG_COGS_COGS_96X96_2";
			case 888:
				return "IMAGE_LIGHTNING_GEMNUMS_RED";
			case 889:
				return "IMAGE_LIGHTNING_GEMNUMS_WHITE";
			case 890:
				return "IMAGE_LIGHTNING_GEMNUMS_GREEN";
			case 891:
				return "IMAGE_LIGHTNING_GEMNUMS_YELLOW";
			case 892:
				return "IMAGE_LIGHTNING_GEMNUMS_PURPLE";
			case 893:
				return "IMAGE_LIGHTNING_GEMNUMS_ORANGE";
			case 894:
				return "IMAGE_LIGHTNING_GEMNUMS_BLUE";
			case 895:
				return "IMAGE_LIGHTNING_GEMNUMS_CLEAR";
			case 896:
				return "IMAGE_GEMOUTLINES";
			case 897:
				return "IMAGE_INGAMEUI_LIGHTNING_BOARD_SEPARATOR_FRAME";
			case 898:
				return "IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER";
			case 899:
				return "IMAGE_INGAMEUI_LIGHTNING_MULTIPLIER";
			case 900:
				return "IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK";
			case 901:
				return "IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME";
			case 902:
				return "IMAGE_INGAMEUI_LIGHTNING_TIMER";
			case 903:
				return "IMAGE_BUTTERFLY_BODY";
			case 904:
				return "IMAGE_BUTTERFLY_SHADOW";
			case 905:
				return "IMAGE_BUTTERFLY_WINGS";
			case 906:
				return "IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_BOTTOM";
			case 907:
				return "IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_TOP";
			case 908:
				return "IMAGE_INGAMEUI_BUTTERFLIES_BUTTERFLY";
			case 909:
				return "IMAGE_INGAMEUI_BUTTERFLIES_SCORE_BG";
			case 910:
				return "IMAGE_INGAMEUI_BUTTERFLIES_SCORE_FRAME";
			case 911:
				return "IMAGE_INGAMEUI_BUTTERFLIES_WEB";
			case 912:
				return "IMAGE_ANIMS_SPIDER_SPIDER_26X50";
			case 913:
				return "IMAGE_ANIMS_SPIDER_SPIDER_26X52";
			case 914:
				return "IMAGE_ANIMS_SPIDER_SPIDER_27X42";
			case 915:
				return "IMAGE_ANIMS_SPIDER_SPIDER_27X42_2";
			case 916:
				return "IMAGE_ANIMS_SPIDER_SPIDER_28X50";
			case 917:
				return "IMAGE_ANIMS_SPIDER_SPIDER_29X41";
			case 918:
				return "IMAGE_ANIMS_SPIDER_SPIDER_31X41";
			case 919:
				return "IMAGE_ANIMS_SPIDER_SPIDER_33X51";
			case 920:
				return "IMAGE_ANIMS_SPIDER_SPIDER_35X34";
			case 921:
				return "IMAGE_ANIMS_SPIDER_SPIDER_38X21";
			case 922:
				return "IMAGE_ANIMS_SPIDER_SPIDER_40X36";
			case 923:
				return "IMAGE_ANIMS_SPIDER_SPIDER_41X46";
			case 924:
				return "IMAGE_ANIMS_SPIDER_SPIDER_42X40";
			case 925:
				return "IMAGE_ANIMS_SPIDER_SPIDER_44X27";
			case 926:
				return "IMAGE_ANIMS_SPIDER_SPIDER_46X35";
			case 927:
				return "IMAGE_ANIMS_SPIDER_SPIDER_47X39";
			case 928:
				return "IMAGE_ANIMS_SPIDER_SPIDER_53X41";
			case 929:
				return "IMAGE_ANIMS_SPIDER_SPIDER_55X49";
			case 930:
				return "IMAGE_ANIMS_SPIDER_SPIDER_57X33";
			case 931:
				return "IMAGE_ANIMS_SPIDER_SPIDER_61X43";
			case 932:
				return "IMAGE_ANIMS_SPIDER_SPIDER_63X53";
			case 933:
				return "IMAGE_ANIMS_SPIDER_SPIDER_72X38";
			case 934:
				return "IMAGE_ANIMS_SPIDER_SPIDER_84X108";
			case 935:
				return "IMAGE_ANIMS_SPIDER_SPIDER_87X102";
			case 936:
				return "IMAGE_ANIMS_SPIDER_SPIDER_91X336";
			case 937:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_137X175";
			case 938:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_144X162";
			case 939:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_149X204";
			case 940:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_20X476";
			case 941:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_34X62";
			case 942:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_40X67";
			case 943:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_41X65";
			case 944:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_43X65";
			case 945:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_45X67";
			case 946:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_46X53";
			case 947:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_49X57";
			case 948:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_51X60";
			case 949:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_59X34";
			case 950:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_64X53";
			case 951:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_69X40";
			case 952:
				return "IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_71X36";
			case 953:
				return "IMAGE_QUEST_INFERNO_COLUMN2_GLOW";
			case 954:
				return "IMAGE_QUEST_INFERNO_COLUMN1_GLOW";
			case 955:
				return "IMAGE_INGAMEUI_ICE_STORM_ICE_BOTTOM";
			case 956:
				return "IMAGE_INGAMEUI_ICE_STORM_ICE_LIQUID";
			case 957:
				return "IMAGE_INGAMEUI_ICE_STORM_ICE_METER";
			case 958:
				return "IMAGE_INGAMEUI_ICE_STORM_ICE_METER_ICE";
			case 959:
				return "IMAGE_INGAMEUI_ICE_STORM_ICE_METER_PIPE";
			case 960:
				return "IMAGE_INGAMEUI_ICE_STORM_MULTIPLIER";
			case 961:
				return "IMAGE_INGAMEUI_ICE_STORM_TOP_FRAME";
			case 962:
				return "IMAGE_ANIMS_COLUMN1_AQUAFRESH";
			case 963:
				return "IMAGE_ANIMS_COLUMN1_AQUAFRESH2";
			case 964:
				return "IMAGE_ANIMS_COLUMN1_AQUAFRESHRED";
			case 965:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1";
			case 966:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK01";
			case 967:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK02";
			case 968:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK03";
			case 969:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK04";
			case 970:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK05";
			case 971:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK06";
			case 972:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK07";
			case 973:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK08";
			case 974:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK09";
			case 975:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK10";
			case 976:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK11";
			case 977:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK12";
			case 978:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK13";
			case 979:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK14";
			case 980:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK15";
			case 981:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK16";
			case 982:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK17";
			case 983:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK18";
			case 984:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK19";
			case 985:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK20";
			case 986:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK21";
			case 987:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK22";
			case 988:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK23";
			case 989:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK24";
			case 990:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK25";
			case 991:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK26";
			case 992:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK27";
			case 993:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK28";
			case 994:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK29";
			case 995:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK30";
			case 996:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK1A";
			case 997:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK1B";
			case 998:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK2A";
			case 999:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK2B";
			case 1000:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK3A";
			case 1001:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK3B";
			case 1002:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK4A";
			case 1003:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK4B";
			case 1004:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK5A";
			case 1005:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK5B";
			case 1006:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH1";
			case 1007:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH2";
			case 1008:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH3";
			case 1009:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_GLOW";
			case 1010:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_GLOW_RED";
			case 1011:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_METER";
			case 1012:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_METER_RED";
			case 1013:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_PULSE";
			case 1014:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_SUPERCRACK";
			case 1015:
				return "IMAGE_ANIMS_COLUMN1_COLUMN1_SUPERCRACK_RED";
			case 1016:
				return "IMAGE_ANIMS_COLUMN1_FBOMB_SMALL_0_ICECHUNK";
			case 1017:
				return "IMAGE_ANIMS_COLUMN1_FBOMB_SMALL_1_TWIRL_SOFT";
			case 1018:
				return "IMAGE_ANIMS_COLUMN1_SHATTERLEFT_SMALL_0_ICECHUNK";
			case 1019:
				return "IMAGE_ANIMS_COLUMN1_SHATTERRIGHT_SMALL_0_ICECHUNK";
			case 1020:
				return "IMAGE_ANIMS_COLUMN1_SNOWCRUSH_0_ICECHUNK";
			case 1021:
				return "IMAGE_ANIMS_COLUMN2_AQUAFRESH";
			case 1022:
				return "IMAGE_ANIMS_COLUMN2_AQUAFRESH2";
			case 1023:
				return "IMAGE_ANIMS_COLUMN2_AQUAFRESHRED";
			case 1024:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2";
			case 1025:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK01";
			case 1026:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK02";
			case 1027:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK03";
			case 1028:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK04";
			case 1029:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK05";
			case 1030:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK06";
			case 1031:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK07";
			case 1032:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK08";
			case 1033:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK09";
			case 1034:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK10";
			case 1035:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK11";
			case 1036:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK12";
			case 1037:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK13";
			case 1038:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK14";
			case 1039:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK15";
			case 1040:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK16";
			case 1041:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK17";
			case 1042:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK18";
			case 1043:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK19";
			case 1044:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK20";
			case 1045:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK21";
			case 1046:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK22";
			case 1047:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK23";
			case 1048:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK24";
			case 1049:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK25";
			case 1050:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK26";
			case 1051:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK27";
			case 1052:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK28";
			case 1053:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK29";
			case 1054:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK30";
			case 1055:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK1A";
			case 1056:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK1B";
			case 1057:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK2A";
			case 1058:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK2B";
			case 1059:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK3A";
			case 1060:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK3B";
			case 1061:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK4A";
			case 1062:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK4B";
			case 1063:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK5A";
			case 1064:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK5B";
			case 1065:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH1";
			case 1066:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH2";
			case 1067:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH3";
			case 1068:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_GLOW";
			case 1069:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_GLOW_RED";
			case 1070:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_METER";
			case 1071:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_METER_RED";
			case 1072:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_PULSE";
			case 1073:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_SUPERCRACK";
			case 1074:
				return "IMAGE_ANIMS_COLUMN2_COLUMN2_SUPERCRACK_RED";
			case 1075:
				return "IMAGE_ANIMS_COLUMN2_FBOMB_0_ICECHUNK";
			case 1076:
				return "IMAGE_ANIMS_COLUMN2_FBOMB_1_TWIRL_SOFT";
			case 1077:
				return "IMAGE_ANIMS_COLUMN2_SHATTERLEFT_0_ICECHUNK";
			case 1078:
				return "IMAGE_ANIMS_COLUMN2_SHATTERRIGHT_0_ICECHUNK";
			case 1079:
				return "IMAGE_ANIMS_COLUMN2_SNOWCRUSH_0_ICECHUNK";
			case 1080:
				return "IMAGE_ANIMS_FROSTPANIC_FROSTPANIC_RED";
			case 1081:
				return "IMAGE_ANIMS_FROSTPANIC_FROSTPANIC_SKULL";
			case 1082:
				return "IMAGE_QUEST_INFERNO_LAVA_FROST_BOTTOM";
			case 1083:
				return "IMAGE_QUEST_INFERNO_LAVA_FROST_LOSE";
			case 1084:
				return "IMAGE_QUEST_INFERNO_LAVA_FROST_TOP";
			case 1085:
				return "IMAGE_QUEST_INFERNO_LAVA_FROST_TOP_UNDER";
			case 1086:
				return "IMAGE_QUEST_INFERNO_LAVA_ICECHUNK";
			case 1087:
				return "IMAGE_QUEST_INFERNO_LAVA_MOUNTAINDOUBLE";
			case 1088:
				return "IMAGE_QUEST_INFERNO_LAVA_MOUNTAINSINGLE";
			case 1089:
				return "IMAGE_QUEST_INFERNO_LAVA_SNOWFLAKE_PARTICLE";
			case 1090:
				return "IMAGE_QUEST_INFERNO_LAVA_UI_TOP_FRAME";
			case 1091:
				return "IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME";
			case 1092:
				return "IMAGE_INGAMEUI_PROGRESS_BAR_BACK";
			case 1093:
				return "IMAGE_INGAMEUI_PROGRESS_BAR_FRAME";
			case 1094:
				return "IMAGE_INGAMEUI_REPLAY_BUTTON";
			case 1095:
				return "IMAGE_LEVELBAR_ENDPIECE";
			case 1096:
				return "IMAGE_QUEST_BALANCE_FRAME";
			case 1097:
				return "IMAGE_BALANCE_RIG_ARROW";
			case 1098:
				return "IMAGE_BALANCE_RIG_GLOW";
			case 1099:
				return "IMAGE_BALANCE_RIG_SIDE_CHAIN";
			case 1100:
				return "IMAGE_BALANCE_RIG_TOP_CHAIN";
			case 1101:
				return "IMAGE_BALANCE_RIG_WHEEL";
			case 1102:
				return "IMAGE_BALANCE_RIG_WHEEL_SPOKES";
			case 1103:
				return "IMAGE_WEIGHT_FILL_MASK";
			case 1104:
				return "IMAGE_WEIGHT_CAP";
			case 1105:
				return "IMAGE_WEIGHT_FILL";
			case 1106:
				return "IMAGE_WEIGHT_GLASS_BACK";
			case 1107:
				return "IMAGE_WEIGHT_GLASS_FRONT";
			case 1108:
				return "IMAGE_BOMBGEMS";
			case 1109:
				return "IMAGE_BOMBGLOWS_DANGERGLOW";
			case 1110:
				return "IMAGE_BOMBGLOWS_GLOW";
			case 1111:
				return "IMAGE_INGAME_POKER_BOARD_SEPERATOR_FRAME_BOTTOM";
			case 1112:
				return "IMAGE_INGAME_POKER_BOARD_SEPERATOR_FRAME_TOP";
			case 1113:
				return "IMAGE_INGAME_POKER_HAND";
			case 1114:
				return "IMAGE_INGAME_POKER_HAND_GLOW";
			case 1115:
				return "IMAGE_INGAME_POKER_HAND_SKULL";
			case 1116:
				return "IMAGE_POKER_BAR_SKULL";
			case 1117:
				return "IMAGE_POKER_BKG";
			case 1118:
				return "IMAGE_POKER_DECK";
			case 1119:
				return "IMAGE_POKER_DECK_SHADOW";
			case 1120:
				return "IMAGE_POKER_LARGE_SKULL";
			case 1121:
				return "IMAGE_POKER_LIGHT_LIT";
			case 1122:
				return "IMAGE_POKER_LIGHT_UNLIT";
			case 1123:
				return "IMAGE_POKER_LONG_BKG";
			case 1124:
				return "IMAGE_POKER_SCORE_BKG";
			case 1125:
				return "IMAGE_POKER_SCORE_GLOW";
			case 1126:
				return "IMAGE_POKER_SKULL";
			case 1127:
				return "IMAGE_POKER_SKULL_BAR_COVER";
			case 1128:
				return "IMAGE_POKER_SKULL_CRUSHER_BAR";
			case 1129:
				return "IMAGE_POKER_SKULL_CRUSHER_BKG";
			case 1130:
				return "IMAGE_POKER_SKULL_CRUSHER_BORDER";
			case 1131:
				return "IMAGE_POKER_SKULL_CRUSHER_GLOW";
			case 1132:
				return "IMAGE_POKER_SKULL_SLASH";
			case 1133:
				return "IMAGE_POKER_SLASH_SHADOW";
			case 1134:
				return "IMAGE_CARDS_BACK";
			case 1135:
				return "IMAGE_CARDS_DECK";
			case 1136:
				return "IMAGE_CARDS_DECK_SHADOW";
			case 1137:
				return "IMAGE_CARDS_FACE";
			case 1138:
				return "IMAGE_CARDS_FRONT";
			case 1139:
				return "IMAGE_CARDS_SHADOW";
			case 1140:
				return "IMAGE_CARDS_SMALL_FACE";
			case 1141:
				return "IMAGE_CARDS_WILD";
			case 1142:
				return "IMAGE_SKULL_COIN_SET1";
			case 1143:
				return "IMAGE_SKULL_COIN_SET2";
			case 1144:
				return "IMAGE_SKULL_COIN_SET3";
			case 1145:
				return "IMAGE_SKULL_COIN_SET4";
			case 1146:
				return "IMAGE_SKULL_COIN_SIDE";
			case 1147:
				return "IMAGE_QUEST_DIG_BOARD_NUGGETPART1";
			case 1148:
				return "IMAGE_QUEST_DIG_BOARD_NUGGETPART2";
			case 1149:
				return "IMAGE_QUEST_DIG_BOARD_NUGGETPART3";
			case 1150:
				return "IMAGE_QUEST_DIG_BOARD_NUGGETPART4";
			case 1151:
				return "IMAGE_QUEST_DIG_BOARD_NUGGETPART5";
			case 1152:
				return "IMAGE_QUEST_DIG_BOARD_NUGGETPART6";
			case 1153:
				return "IMAGE_QUEST_DIG_BOARD_NUGGETPART7";
			case 1154:
				return "IMAGE_QUEST_DIG_BOARD_NUGGETPART8";
			case 1155:
				return "IMAGE_QUEST_DIG_BOARD_NUGGETPART9";
			case 1156:
				return "IMAGE_QUEST_DIG_BOARD_NUGGETPART10";
			case 1157:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET1_1";
			case 1158:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET1_2";
			case 1159:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET1_3";
			case 1160:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET2_1";
			case 1161:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET2_2";
			case 1162:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET2_3";
			case 1163:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET3_1";
			case 1164:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET3_2";
			case 1165:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET3_3";
			case 1166:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET4_1";
			case 1167:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET4_2";
			case 1168:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET4_3";
			case 1169:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET5_1";
			case 1170:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET5_2";
			case 1171:
				return "IMAGE_QUEST_DIG_BOARD_NUGGET5_3";
			case 1172:
				return "IMAGE_QUEST_DIG_BOARD_BOTTOM_OVERLAY";
			case 1173:
				return "IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM";
			case 1174:
				return "IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM_HIGHLIGHT";
			case 1175:
				return "IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM_HIGHLIGHT_SHADOW";
			case 1176:
				return "IMAGE_QUEST_DIG_BOARD_CENTER_FULL";
			case 1177:
				return "IMAGE_QUEST_DIG_BOARD_CENTER_LEFT";
			case 1178:
				return "IMAGE_QUEST_DIG_BOARD_CENTER_LEFT_HIGHLIGHT";
			case 1179:
				return "IMAGE_QUEST_DIG_BOARD_CENTER_LEFT_HIGHLIGHT_SHADOW";
			case 1180:
				return "IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT";
			case 1181:
				return "IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT__HIGHLIGHT";
			case 1182:
				return "IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT_HIGHLIGHT_SHADOW";
			case 1183:
				return "IMAGE_QUEST_DIG_BOARD_CENTER_TOP";
			case 1184:
				return "IMAGE_QUEST_DIG_BOARD_CENTER_TOP_HIGHLIGHT";
			case 1185:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND1";
			case 1186:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND1_1";
			case 1187:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND1_2";
			case 1188:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND1_3";
			case 1189:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND1_4";
			case 1190:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND1_DIRT";
			case 1191:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART1";
			case 1192:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART2";
			case 1193:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART3";
			case 1194:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART4";
			case 1195:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND2";
			case 1196:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND2_1";
			case 1197:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND2_2";
			case 1198:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND2_3";
			case 1199:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND2_4";
			case 1200:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND2_DIRT";
			case 1201:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART1";
			case 1202:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART2";
			case 1203:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART3";
			case 1204:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART4";
			case 1205:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND3";
			case 1206:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND3_1";
			case 1207:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND3_2";
			case 1208:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND3_3";
			case 1209:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND3_DIRT";
			case 1210:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART1";
			case 1211:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART2";
			case 1212:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART3";
			case 1213:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART4";
			case 1214:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND4";
			case 1215:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND4_1";
			case 1216:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND4_2";
			case 1217:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND4_3";
			case 1218:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND4_4";
			case 1219:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND4_DIRT";
			case 1220:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART1";
			case 1221:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART2";
			case 1222:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART3";
			case 1223:
				return "IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART4";
			case 1224:
				return "IMAGE_QUEST_DIG_BOARD_GOLDGROUP1";
			case 1225:
				return "IMAGE_QUEST_DIG_BOARD_GOLDGROUP2";
			case 1226:
				return "IMAGE_QUEST_DIG_BOARD_GOLDGROUP3";
			case 1227:
				return "IMAGE_QUEST_DIG_BOARD_GRASS";
			case 1228:
				return "IMAGE_QUEST_DIG_BOARD_GRASS_LEFT";
			case 1229:
				return "IMAGE_QUEST_DIG_BOARD_GRASS_RIGHT";
			case 1230:
				return "IMAGE_QUEST_DIG_BOARD_HYPERCUBE";
			case 1231:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_ABICUS_BIG";
			case 1232:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_ANVIL_BIG";
			case 1233:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_ASTROLABE_BIG";
			case 1234:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_AXE_BIG";
			case 1235:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_BELL_BIG";
			case 1236:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_BJORN_BIG";
			case 1237:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_BOOK_BIG";
			case 1238:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_BOOTS_BIG";
			case 1239:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_BOWARROW_BIG";
			case 1240:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_BOWL_BIG";
			case 1241:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_BRUSH_BIG";
			case 1242:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_CLOCK_BIG";
			case 1243:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_COMB_BIG";
			case 1244:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_CREST_BIG";
			case 1245:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_DAGGER_BIG";
			case 1246:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_DISH_BIG";
			case 1247:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_DMGEM_BIG";
			case 1248:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_FLUTE_BIG";
			case 1249:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_FORK_BIG";
			case 1250:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_FROG_BIG";
			case 1251:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_GAUNTLET_BIG";
			case 1252:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_GEAR_BIG";
			case 1253:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_HAMMER_BIG";
			case 1254:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_HARP_BIG";
			case 1255:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_HELMET_BIG";
			case 1256:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_HORN_BIG";
			case 1257:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_HORSE_BIG";
			case 1258:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_HORSESHOE_BIG";
			case 1259:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_KEY_BIG";
			case 1260:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_LAMP_BIG";
			case 1261:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_MACE_BIG";
			case 1262:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_MAGNIFYINGGLASS_BIG";
			case 1263:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_MASK_BIG";
			case 1264:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_POT_BIG";
			case 1265:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_SCROLL_BIG";
			case 1266:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_SCYTHE_BIG";
			case 1267:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_SEXTANT_BIG";
			case 1268:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_SPOON_BIG";
			case 1269:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_STAFF_BIG";
			case 1270:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_STIRRUP_BIG";
			case 1271:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_TELESCOPE_BIG";
			case 1272:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_TONGS_BIG";
			case 1273:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_TRIDENT_BIG";
			case 1274:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_TROWEL_BIG";
			case 1275:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_URN_BIG";
			case 1276:
				return "IMAGE_QUEST_DIG_BOARD_ITEM_VASE_BIG";
			case 1277:
				return "IMAGE_QUEST_DIG_BOARD_PEBBLES1";
			case 1278:
				return "IMAGE_QUEST_DIG_BOARD_PEBBLES2";
			case 1279:
				return "IMAGE_QUEST_DIG_BOARD_PEBBLES3";
			case 1280:
				return "IMAGE_QUEST_DIG_BOARD_STR1";
			case 1281:
				return "IMAGE_QUEST_DIG_BOARD_STR2";
			case 1282:
				return "IMAGE_QUEST_DIG_BOARD_STR3";
			case 1283:
				return "IMAGE_QUEST_DIG_BOARD_STR4";
			case 1284:
				return "IMAGE_QUEST_DIG_DIRT_OVERLAY1";
			case 1285:
				return "IMAGE_QUEST_DIG_DIRT_OVERLAY2";
			case 1286:
				return "IMAGE_QUEST_DIG_DIRT_OVERLAY3";
			case 1287:
				return "IMAGE_QUEST_DIG_DIRT_UNDERLAY1";
			case 1288:
				return "IMAGE_QUEST_DIG_DIRT_UNDERLAY2";
			case 1289:
				return "IMAGE_QUEST_DIG_DIRT_UNDERLAY3";
			case 1290:
				return "IMAGE_QUEST_DIG_GLOW";
			case 1291:
				return "IMAGE_QUEST_DIG_DIAMONDPART";
			case 1292:
				return "IMAGE_QUEST_DIG_STREAK";
			case 1293:
				return "IMAGE_WALLROCKS_LARGE";
			case 1294:
				return "IMAGE_WALLROCKS_MEDIUM";
			case 1295:
				return "IMAGE_WALLROCKS_SMALL";
			case 1296:
				return "IMAGE_WALLROCKS_SMALL_BROWN";
			case 1297:
				return "IMAGE_QUEST_LEAD2GOLD_BLACK";
			case 1298:
				return "IMAGE_QUEST_LEAD2GOLD_LEAD";
			case 1299:
				return "IMAGE_QUEST_LEAD2GOLD_GOLD";
			case 1300:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_BOTTOM_SAND";
			case 1301:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ1";
			case 1302:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ2";
			case 1303:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ3";
			case 1304:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_PLAQUE";
			case 1305:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ1";
			case 1306:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ2";
			case 1307:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ3";
			case 1308:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_PLAQUE";
			case 1309:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ1";
			case 1310:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ2";
			case 1311:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ3";
			case 1312:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ4";
			case 1313:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_PLAQUE";
			case 1314:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_PLAQUE";
			case 1315:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_SAND_MASK";
			case 1316:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_SAND_MASK_SOFT";
			case 1317:
				return "IMAGE_QUEST_HIDDENOBJECT_BOARD_TOP_SAND";
			case 1318:
				return "IMAGE_MYSTERY_CIRCLE";
			case 1319:
				return "IMAGE_QUEST_WALLBLAST_BOARD_MASK";
			case 1320:
				return "IMAGE_QUEST_WALLBLAST_BOARD_WALL";
			case 1321:
				return "IMAGE_DIALOG_ARROW_SWIPE";
			case 1322:
				return "IMAGE_DIALOG_ARROW_SWIPEGLOW";
			case 1323:
				return "IMAGE_DIALOG_BLACK_BOX";
			case 1324:
				return "IMAGE_DIALOG_BUTTON_DROPDOWN";
			case 1325:
				return "IMAGE_DIALOG_BUTTON_FRAME";
			case 1326:
				return "IMAGE_DIALOG_BUTTON_FRAME_DIAMOND_MINE";
			case 1327:
				return "IMAGE_DIALOG_BUTTON_FRAME_ICE_STORM";
			case 1328:
				return "IMAGE_DIALOG_BUTTON_GAMECENTER";
			case 1329:
				return "IMAGE_DIALOG_BUTTON_GAMECENTER_BG";
			case 1330:
				return "IMAGE_DIALOG_BUTTON_LARGE";
			case 1331:
				return "IMAGE_DIALOG_BUTTON_SMALL_BG";
			case 1332:
				return "IMAGE_DIALOG_BUTTON_SMALL_BLUE";
			case 1333:
				return "IMAGE_DIALOG_CHECKBOX";
			case 1334:
				return "IMAGE_DIALOG_CHECKBOX_CHECKED";
			case 1335:
				return "IMAGE_DIALOG_CHECKBOX_UNSELECTED";
			case 1336:
				return "IMAGE_DIALOG_DIALOG_BOX_INTERIOR_BG";
			case 1337:
				return "IMAGE_DIALOG_DIALOG_CORNER";
			case 1338:
				return "IMAGE_DIALOG_DIALOG_SWIPE_BOTTOM_EDGE";
			case 1339:
				return "IMAGE_DIALOG_DIALOG_SWIPE_TOP_EDGE";
			case 1340:
				return "IMAGE_DIALOG_DIVIDER";
			case 1341:
				return "IMAGE_DIALOG_DIVIDER_GEM";
			case 1342:
				return "IMAGE_DIALOG_HELP_GLOW";
			case 1343:
				return "IMAGE_DIALOG_ICON_FLAME_LRG";
			case 1344:
				return "IMAGE_DIALOG_ICON_HYPERCUBE_LRG";
			case 1345:
				return "IMAGE_DIALOG_ICON_STAR_LRG";
			case 1346:
				return "IMAGE_DIALOG_LISTBOX_BG";
			case 1347:
				return "IMAGE_DIALOG_LISTBOX_BG_DARK";
			case 1348:
				return "IMAGE_DIALOG_LISTBOX_FOOTER";
			case 1349:
				return "IMAGE_DIALOG_LISTBOX_HEADER";
			case 1350:
				return "IMAGE_DIALOG_LISTBOX_SHADOW";
			case 1351:
				return "IMAGE_DIALOG_MINE_TILES_GEM";
			case 1352:
				return "IMAGE_DIALOG_MINE_TILES_GOLD";
			case 1353:
				return "IMAGE_DIALOG_MINE_TILES_TREASURE";
			case 1354:
				return "IMAGE_DIALOG_PROGRESS_BAR";
			case 1355:
				return "IMAGE_DIALOG_PROGRESS_BAR_BG";
			case 1356:
				return "IMAGE_DIALOG_PROGRESS_BAR_CROWN";
			case 1357:
				return "IMAGE_DIALOG_PROGRESS_BAR_FILL";
			case 1358:
				return "IMAGE_DIALOG_PROGRESS_BAR_GLOW";
			case 1359:
				return "IMAGE_DIALOG_REPLAY";
			case 1360:
				return "IMAGE_DIALOG_SCROLLBAR";
			case 1361:
				return "IMAGE_DIALOG_SFX_ICONS_MUSIC";
			case 1362:
				return "IMAGE_DIALOG_SFX_ICONS_MUSIC_UNSELECTED";
			case 1363:
				return "IMAGE_DIALOG_SFX_ICONS_SOUND";
			case 1364:
				return "IMAGE_DIALOG_SFX_ICONS_SOUND_UNSELECTED";
			case 1365:
				return "IMAGE_DIALOG_SFX_ICONS_VOICES";
			case 1366:
				return "IMAGE_DIALOG_SFX_ICONS_VOICES_UNSELECTED";
			case 1367:
				return "IMAGE_DIALOG_SLIDER_BAR_HANDLE";
			case 1368:
				return "IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL";
			case 1369:
				return "IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL";
			case 1370:
				return "IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL_UNSE";
			case 1371:
				return "IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_UNSELECTE";
			case 1372:
				return "IMAGE_DIALOG_SLIDER_BAR_VERTICAL";
			case 1373:
				return "IMAGE_DIALOG_SLIDER_BAR_VERTICAL_FILL";
			case 1374:
				return "IMAGE_DIALOG_SLIDER_BAR_VERTICAL_FILL_UNSELE";
			case 1375:
				return "IMAGE_DIALOG_SLIDER_BAR_VERTICAL_UNSELECTED";
			case 1376:
				return "IMAGE_DIALOG_TEXTBOX";
			case 1377:
				return "IMAGE_ALPHA_ALPHA_DOWN";
			case 1378:
				return "IMAGE_ALPHA_ALPHA_UP";
			case 1379:
				return "IMAGE_BADGES_SMALL_ANNIHILATOR";
			case 1380:
				return "IMAGE_BADGES_SMALL_ANTE_UP";
			case 1381:
				return "IMAGE_BADGES_SMALL_BEJEWELER";
			case 1382:
				return "IMAGE_BADGES_SMALL_BLASTER";
			case 1383:
				return "IMAGE_BADGES_SMALL_BUTTERFLY_BONANZA";
			case 1384:
				return "IMAGE_BADGES_SMALL_BUTTERFLY_MONARCH";
			case 1385:
				return "IMAGE_BADGES_SMALL_CHAIN_REACTION";
			case 1386:
				return "IMAGE_BADGES_SMALL_CHROMATIC";
			case 1387:
				return "IMAGE_BADGES_SMALL_DIAMOND_MINE";
			case 1388:
				return "IMAGE_BADGES_SMALL_DYNAMO";
			case 1389:
				return "IMAGE_BADGES_SMALL_ELECTRIFIER";
			case 1390:
				return "IMAGE_BADGES_SMALL_HIGH_VOLTAGE";
			case 1391:
				return "IMAGE_BADGES_SMALL_INFERNO";
			case 1392:
				return "IMAGE_BADGES_SMALL_LEVELORD";
			case 1393:
				return "IMAGE_BADGES_SMALL_LUCKY_STREAK";
			case 1394:
				return "IMAGE_BADGES_SMALL_MILLIONAIRE";
			case 1395:
				return "IMAGE_BADGES_SMALL_RELIC_HUNTER";
			case 1396:
				return "IMAGE_BADGES_SMALL_RINGS";
			case 1397:
				return "IMAGE_BADGES_SMALL_STELLAR";
			case 1398:
				return "IMAGE_BADGES_SMALL_SUPERSTAR";
			case 1399:
				return "IMAGE_BADGES_SMALL_THE_GAMBLER";
			case 1400:
				return "IMAGE_BADGES_SMALL_UNKNOWN";
			case 1401:
				return "IMAGE_ANGRYBOMB";
			case 1402:
				return "IMAGE_ANIMS_100CREST_100CREST";
			case 1403:
				return "IMAGE_ANIMS_BOARDSHATTER_BOTTOM";
			case 1404:
				return "IMAGE_ANIMS_BOARDSHATTER_GRID";
			case 1405:
				return "IMAGE_ANIMS_BOARDSHATTER_TOP";
			case 1406:
				return "IMAGE_BOOM_BOARD";
			case 1407:
				return "IMAGE_BOOM_CRATER";
			case 1408:
				return "IMAGE_BOOM_FBOTTOM_WIDGET";
			case 1409:
				return "IMAGE_BOOM_FGRIDBAR_BOT";
			case 1410:
				return "IMAGE_BOOM_FGRIDBAR_TOP";
			case 1411:
				return "IMAGE_BOOM_FTOP_WIDGET";
			case 1412:
				return "IMAGE_BROWSER_BACKBTN";
			case 1413:
				return "IMAGE_CHECKPOINT_MARKER";
			case 1414:
				return "IMAGE_CLOCK_BKG";
			case 1415:
				return "IMAGE_CLOCK_FACE";
			case 1416:
				return "IMAGE_CLOCK_FILL";
			case 1417:
				return "IMAGE_CLOCK_GEAR1";
			case 1418:
				return "IMAGE_CLOCK_GEAR2";
			case 1419:
				return "IMAGE_CLOCK_GEAR3";
			case 1420:
				return "IMAGE_CLOCK_GEAR4";
			case 1421:
				return "IMAGE_CLOCK_GEAR5";
			case 1422:
				return "IMAGE_CLOCK_GEAR6";
			case 1423:
				return "IMAGE_CLOCK_GEAR7";
			case 1424:
				return "IMAGE_CLOCK_GEAR8";
			case 1425:
				return "IMAGE_CLOCK_GEAR9";
			case 1426:
				return "IMAGE_CLOCK_GLARE";
			case 1427:
				return "IMAGE_CLOCK_SPOKE";
			case 1428:
				return "IMAGE_DETONATOR";
			case 1429:
				return "IMAGE_DETONATOR_MOUSEOVER";
			case 1430:
				return "IMAGE_DOOMGEM";
			case 1431:
				return "IMAGE_GAMEOVER_BAR__PINK";
			case 1432:
				return "IMAGE_GAMEOVER_BAR_ORANGE";
			case 1433:
				return "IMAGE_GAMEOVER_BAR_YELLOW";
			case 1434:
				return "IMAGE_GAMEOVER_BOX_ORANGE";
			case 1435:
				return "IMAGE_GAMEOVER_BOX_PINK";
			case 1436:
				return "IMAGE_GAMEOVER_BOX_YELLOW";
			case 1437:
				return "IMAGE_GAMEOVER_DARKER_BOX";
			case 1438:
				return "IMAGE_GAMEOVER_DARKEST_BOX";
			case 1439:
				return "IMAGE_GAMEOVER_DIALOG";
			case 1440:
				return "IMAGE_GAMEOVER_DIG_BAR_GEMS";
			case 1441:
				return "IMAGE_GAMEOVER_DIG_BAR_GOLD";
			case 1442:
				return "IMAGE_GAMEOVER_DIG_BAR_TREASURE";
			case 1443:
				return "IMAGE_GAMEOVER_DIG_BOX";
			case 1444:
				return "IMAGE_GAMEOVER_HORIZONTAL_BAR";
			case 1445:
				return "IMAGE_GAMEOVER_HORIZONTAL_BAR_OVERLAY";
			case 1446:
				return "IMAGE_GAMEOVER_ICON_FLAME";
			case 1447:
				return "IMAGE_GAMEOVER_ICON_FLAME_LRG";
			case 1448:
				return "IMAGE_GAMEOVER_ICON_HYPERCUBE";
			case 1449:
				return "IMAGE_GAMEOVER_ICON_HYPERCUBE_LRG";
			case 1450:
				return "IMAGE_GAMEOVER_ICON_LIGHTNING";
			case 1451:
				return "IMAGE_GAMEOVER_ICON_STAR";
			case 1452:
				return "IMAGE_GAMEOVER_ICON_STAR_LRG";
			case 1453:
				return "IMAGE_GAMEOVER_LIGHT_BOX";
			case 1454:
				return "IMAGE_GAMEOVER_LINE_SINGLE";
			case 1455:
				return "IMAGE_GAMEOVER_LINES";
			case 1456:
				return "IMAGE_GAMEOVER_SECTION_GRAPH";
			case 1457:
				return "IMAGE_GAMEOVER_SECTION_LABEL";
			case 1458:
				return "IMAGE_GAMEOVER_SECTION_SMALL";
			case 1459:
				return "IMAGE_GAMEOVER_STAMP";
			case 1460:
				return "IMAGE_GREENQUESTION";
			case 1461:
				return "IMAGE_GRIDPAINT_BLANK";
			case 1462:
				return "IMAGE_GRIDPAINT_FILLED";
			case 1463:
				return "IMAGE_MENU_ARROW";
			case 1464:
				return "IMAGE_QUESTOBJ_FINAL_GLOW_TRANS";
			case 1465:
				return "IMAGE_QUESTOBJ_FINAL_GLOW_TRANS2";
			case 1466:
				return "IMAGE_QUESTOBJ_GLOW";
			case 1467:
				return "IMAGE_QUESTOBJ_GLOW_FINAL";
			case 1468:
				return "IMAGE_QUESTOBJ_GLOW_FX";
			case 1469:
				return "IMAGE_QUESTOBJ_GLOW2";
			case 1470:
				return "IMAGE_RANKUP";
			case 1471:
				return "IMAGE_SOLID_BLACK";
			case 1472:
				return "IMAGE_SPARKLE_FAT";
			case 1473:
				return "IMAGE_SPARKLET_BIG";
			case 1474:
				return "IMAGE_SPARKLET_FAT";
			case 1475:
				return "IMAGE_TRANSPARENT_HOLE";
			case 1476:
				return "IMAGE_VERTICAL_STREAK";
			case 1477:
				return "PIEFFECT_ANIMS_CARD_GEM_SPARKLE2";
			case 1478:
				return "PIEFFECT_HELP_BUTTERFLY_HELP";
			case 1479:
				return "PIEFFECT_HELP_CARD_GEM_SPARKLE2";
			case 1480:
				return "PIEFFECT_HELP_DIAMOND_SPARKLE";
			case 1481:
				return "PIEFFECT_HELP_FLAMEGEM_HELP";
			case 1482:
				return "PIEFFECT_HELP_ICESTORM_HELP";
			case 1483:
				return "PIEFFECT_HELP_LIGHTNING_STEAMPULSE";
			case 1484:
				return "PIEFFECT_HELP_STARGEM_HELP";
			case 1485:
				return "PIEFFECT_CRYSTALBALL";
			case 1486:
				return "PIEFFECT_CRYSTALRAYS";
			case 1487:
				return "PIEFFECT_QUEST_DIG_COLLECT_BASE";
			case 1488:
				return "PIEFFECT_QUEST_DIG_COLLECT_GOLD";
			case 1489:
				return "PIEFFECT_SANDSTORM_DIG";
			case 1490:
				return "PIEFFECT_BADGE_UPGRADE";
			case 1491:
				return "PIEFFECT_BLASTGEM";
			case 1492:
				return "PIEFFECT_BLOWING_SNOW";
			case 1493:
				return "PIEFFECT_BOARD_FLAME_EMBERS";
			case 1494:
				return "PIEFFECT_BUTTERFLY";
			case 1495:
				return "PIEFFECT_BUTTERFLY_CREATE";
			case 1496:
				return "PIEFFECT_CARD_GEM_SPARKLE";
			case 1497:
				return "PIEFFECT_COINSPARKLE";
			case 1498:
				return "PIEFFECT_COUNTDOWNBAR";
			case 1499:
				return "PIEFFECT_DANGERSNOW_HARD_TOP";
			case 1500:
				return "PIEFFECT_DANGERSNOW_SOFT";
			case 1501:
				return "PIEFFECT_DIAMOND_SPARKLES";
			case 1502:
				return "PIEFFECT_DISCOBALL";
			case 1503:
				return "PIEFFECT_FIRE_TRAIL";
			case 1504:
				return "PIEFFECT_FIREGEM_HYPERSPACE";
			case 1505:
				return "PIEFFECT_FLAME_CARD";
			case 1506:
				return "PIEFFECT_GOLD_BLING";
			case 1507:
				return "PIEFFECT_HINTFLASH";
			case 1508:
				return "PIEFFECT_HYPERCUBE";
			case 1509:
				return "PIEFFECT_ICE_STORMY";
			case 1510:
				return "PIEFFECT_ICE_TRAIL";
			case 1511:
				return "PIEFFECT_LEVELBAR";
			case 1512:
				return "PIEFFECT_MULTIPLIER";
			case 1513:
				return "PIEFFECT_SANDSTORM_COVER";
			case 1514:
				return "PIEFFECT_SKULL_EXPLODE";
			case 1515:
				return "PIEFFECT_SPEEDBOARD_FLAME";
			case 1516:
				return "PIEFFECT_STAR_CARD";
			case 1517:
				return "PIEFFECT_STARBURST";
			case 1518:
				return "PIEFFECT_STARGEM";
			case 1519:
				return "PIEFFECT_WEIGHT_FIRE";
			case 1520:
				return "PIEFFECT_WEIGHT_ICE";
			case 1521:
				return "PIEFFECT_QUEST_DIG_DIG_LINE_HIT";
			case 1522:
				return "PIEFFECT_QUEST_DIG_DIG_LINE_HIT_MEGA";
			case 1523:
				return "PIEFFECT_ANIMS_COLUMN1_FBOMB_SMALL";
			case 1524:
				return "PIEFFECT_ANIMS_COLUMN1_SHATTERLEFT_SMALL";
			case 1525:
				return "PIEFFECT_ANIMS_COLUMN1_SHATTERRIGHT_SMALL";
			case 1526:
				return "PIEFFECT_ANIMS_COLUMN1_SNOWCRUSH";
			case 1527:
				return "PIEFFECT_ANIMS_COLUMN2_FBOMB";
			case 1528:
				return "PIEFFECT_ANIMS_COLUMN2_SHATTERLEFT";
			case 1529:
				return "PIEFFECT_ANIMS_COLUMN2_SHATTERRIGHT";
			case 1530:
				return "PIEFFECT_ANIMS_COLUMN2_SNOWCRUSH";
			case 1531:
				return "POPANIM_HELP_SWAP3";
			case 1532:
				return "POPANIM_HELP_MATCH4";
			case 1533:
				return "POPANIM_HELP_STARGEM";
			case 1534:
				return "POPANIM_HELP_DIAMOND_MATCH";
			case 1535:
				return "POPANIM_HELP_DIAMOND_ADVANCE";
			case 1536:
				return "POPANIM_HELP_DIAMOND_GOLD";
			case 1537:
				return "POPANIM_HELP_LIGHTNING_MATCH";
			case 1538:
				return "POPANIM_HELP_LIGHTNING_TIME";
			case 1539:
				return "POPANIM_HELP_LIGHTNING_SPEED";
			case 1540:
				return "POPANIM_HELP_BFLY_MATCH";
			case 1541:
				return "POPANIM_HELP_BFLY_SPIDER";
			case 1542:
				return "POPANIM_HELP_ICESTORM_HORIZ";
			case 1543:
				return "POPANIM_HELP_ICESTORM_METER";
			case 1544:
				return "POPANIM_HELP_ICESTORM_VERT";
			case 1545:
				return "POPANIM_HELP_POKER_MATCH";
			case 1546:
				return "POPANIM_HELP_POKER_SKULL_CLEAR";
			case 1547:
				return "POPANIM_HELP_POKER_SKULLHAND";
			case 1548:
				return "POPANIM_HELP_SPEEDBONUS";
			case 1549:
				return "POPANIM_HELP_SPEEDBONUS2";
			case 1550:
				return "POPANIM_FLAMEGEMCREATION";
			case 1551:
				return "POPANIM_FLAMEGEMEXPLODE";
			case 1552:
				return "POPANIM_QUEST_DIG_COGS";
			case 1553:
				return "POPANIM_ANIMS_SPIDER";
			case 1554:
				return "POPANIM_ANIMS_LARGE_SPIDER";
			case 1555:
				return "POPANIM_ANIMS_COLUMN1";
			case 1556:
				return "POPANIM_ANIMS_COLUMN2";
			case 1557:
				return "POPANIM_ANIMS_FROSTPANIC";
			case 1558:
				return "POPANIM_ANIMS_100CREST";
			case 1559:
				return "POPANIM_ANIMS_BOARDSHATTER";
			case 1560:
				return "EFFECT_BADGE_GRAYSCALE";
			case 1561:
				return "EFFECT_BOARD_3D";
			case 1562:
				return "EFFECT_FRAME_INTERP";
			case 1563:
				return "EFFECT_GEM_3D";
			case 1564:
				return "EFFECT_GEM_LIGHT";
			case 1565:
				return "EFFECT_GEM_SUN";
			case 1566:
				return "EFFECT_GRAYSCALE";
			case 1567:
				return "EFFECT_GRAYSCALE_COLORIZE";
			case 1568:
				return "EFFECT_MASK";
			case 1569:
				return "EFFECT_MERGE_COLOR_ALPHA";
			case 1570:
				return "EFFECT_REWIND";
			case 1571:
				return "EFFECT_SCREEN_DISTORT";
			case 1572:
				return "EFFECT_TUBE_3D";
			case 1573:
				return "EFFECT_TUBECAP_3D";
			case 1574:
				return "EFFECT_UNDER_DIALOG";
			case 1575:
				return "EFFECT_WAVE";
			case 1576:
				return "SOUND_BACKGROUND_CHANGE";
			case 1577:
				return "SOUND_MULTIPLIER_UP2_1";
			case 1578:
				return "SOUND_MULTIPLIER_UP2_2";
			case 1579:
				return "SOUND_MULTIPLIER_UP2_3";
			case 1580:
				return "SOUND_MULTIPLIER_UP2_4";
			case 1581:
				return "SOUND_BUTTON_MOUSEOVER";
			case 1582:
				return "SOUND_BUTTON_MOUSELEAVE";
			case 1583:
				return "SOUND_QUEST_MENU_BUTTON_MOUSEOVER1";
			case 1584:
				return "SOUND_COMBO_1";
			case 1585:
				return "SOUND_COMBO_2";
			case 1586:
				return "SOUND_COMBO_3";
			case 1587:
				return "SOUND_COMBO_4";
			case 1588:
				return "SOUND_COMBO_5";
			case 1589:
				return "SOUND_COMBO_6";
			case 1590:
				return "SOUND_COMBO_7";
			case 1591:
				return "SOUND_BADMOVE";
			case 1592:
				return "SOUND_FIREWORK_CRACKLE";
			case 1593:
				return "SOUND_FIREWORK_LAUNCH";
			case 1594:
				return "SOUND_FIREWORK_THUMP";
			case 1595:
				return "SOUND_GEM_HIT";
			case 1596:
				return "SOUND_PREBLAST";
			case 1597:
				return "SOUND_SELECT";
			case 1598:
				return "SOUND_START_ROTATE";
			case 1599:
				return "SOUND_ALCHEMY_CONVERT";
			case 1600:
				return "SOUND_BACKTOMAIN";
			case 1601:
				return "SOUND_BADGEAWARDED";
			case 1602:
				return "SOUND_BADGEFALL";
			case 1603:
				return "SOUND_BOMB_APPEARS";
			case 1604:
				return "SOUND_BOMB_EXPLODE";
			case 1605:
				return "SOUND_BREATH_IN";
			case 1606:
				return "SOUND_BREATH_OUT";
			case 1607:
				return "SOUND_BUTTERFLY_APPEAR";
			case 1608:
				return "SOUND_BUTTERFLY_DEATH1";
			case 1609:
				return "SOUND_BUTTERFLYESCAPE";
			case 1610:
				return "SOUND_BUTTON_PRESS";
			case 1611:
				return "SOUND_BUTTON_RELEASE";
			case 1612:
				return "SOUND_CARDDEAL";
			case 1613:
				return "SOUND_CARDFLIP";
			case 1614:
				return "SOUND_CLICKFLYIN";
			case 1615:
				return "SOUND_COIN_CREATED";
			case 1616:
				return "SOUND_COINAPPEAR";
			case 1617:
				return "SOUND_COLD_WIND";
			case 1618:
				return "SOUND_COUNTDOWN_WARNING";
			case 1619:
				return "SOUND_DIAMOND_MINE_ARTIFACT_SHOWCASE";
			case 1620:
				return "SOUND_DIAMOND_MINE_BIGSTONE_CRACKED";
			case 1621:
				return "SOUND_DIAMOND_MINE_DEATH";
			case 1622:
				return "SOUND_DIAMOND_MINE_DIG";
			case 1623:
				return "SOUND_DIAMOND_MINE_DIG_LINE_HIT";
			case 1624:
				return "SOUND_DIAMOND_MINE_DIG_LINE_HIT_MEGA";
			case 1625:
				return "SOUND_DIAMOND_MINE_DIG_NOTIFY";
			case 1626:
				return "SOUND_DIAMOND_MINE_DIRT_CRACKED";
			case 1627:
				return "SOUND_DIAMOND_MINE_STONE_CRACKED";
			case 1628:
				return "SOUND_DIAMOND_MINE_TREASUREFIND";
			case 1629:
				return "SOUND_DIAMOND_MINE_TREASUREFIND_DIAMONDS";
			case 1630:
				return "SOUND_DOUBLESET";
			case 1631:
				return "SOUND_EARTHQUAKE";
			case 1632:
				return "SOUND_ELECTRO_EXPLODE";
			case 1633:
				return "SOUND_ELECTRO_PATH";
			case 1634:
				return "SOUND_ELECTRO_PATH2";
			case 1635:
				return "SOUND_FLAMEBONUS";
			case 1636:
				return "SOUND_FLAMELOOP";
			case 1637:
				return "SOUND_FLAMESPEED1";
			case 1638:
				return "SOUND_GEM_COUNTDOWN_DESTROYED";
			case 1639:
				return "SOUND_GEM_SHATTERS";
			case 1640:
				return "SOUND_HINT";
			case 1641:
				return "SOUND_HYPERCUBE_CREATE";
			case 1642:
				return "SOUND_HYPERSPACE";
			case 1643:
				return "SOUND_HYPERSPACE_GEM_LAND_1";
			case 1644:
				return "SOUND_HYPERSPACE_GEM_LAND_2";
			case 1645:
				return "SOUND_HYPERSPACE_GEM_LAND_3";
			case 1646:
				return "SOUND_HYPERSPACE_GEM_LAND_4";
			case 1647:
				return "SOUND_HYPERSPACE_GEM_LAND_5";
			case 1648:
				return "SOUND_HYPERSPACE_GEM_LAND_6";
			case 1649:
				return "SOUND_HYPERSPACE_GEM_LAND_7";
			case 1650:
				return "SOUND_HYPERSPACE_GEM_LAND_ZEN_1";
			case 1651:
				return "SOUND_HYPERSPACE_GEM_LAND_ZEN_2";
			case 1652:
				return "SOUND_HYPERSPACE_GEM_LAND_ZEN_3";
			case 1653:
				return "SOUND_HYPERSPACE_GEM_LAND_ZEN_4";
			case 1654:
				return "SOUND_HYPERSPACE_GEM_LAND_ZEN_5";
			case 1655:
				return "SOUND_HYPERSPACE_GEM_LAND_ZEN_6";
			case 1656:
				return "SOUND_HYPERSPACE_GEM_LAND_ZEN_7";
			case 1657:
				return "SOUND_HYPERSPACE_SHATTER_1";
			case 1658:
				return "SOUND_HYPERSPACE_SHATTER_2";
			case 1659:
				return "SOUND_HYPERSPACE_SHATTER_ZEN";
			case 1660:
				return "SOUND_ICE_COLUMN_APPEARS";
			case 1661:
				return "SOUND_ICE_COLUMN_BREAK";
			case 1662:
				return "SOUND_ICE_STORM_COLUMNCOMBO";
			case 1663:
				return "SOUND_ICE_STORM_COLUMNCOMBO_MEGA";
			case 1664:
				return "SOUND_ICE_STORM_FINAL_THUD";
			case 1665:
				return "SOUND_ICE_STORM_GAMEOVER";
			case 1666:
				return "SOUND_ICE_STORM_MULTIPLER_UP";
			case 1667:
				return "SOUND_ICE_STORM_STEAM_BUILD_UP";
			case 1668:
				return "SOUND_ICE_STORM_STEAM_VALVE";
			case 1669:
				return "SOUND_ICE_STORM_WIND";
			case 1670:
				return "SOUND_ICE_WARNING";
			case 1671:
				return "SOUND_LASERGEM_CREATED";
			case 1672:
				return "SOUND_LIGHTNING_ENERGIZE";
			case 1673:
				return "SOUND_LIGHTNING_HUMLOOP";
			case 1674:
				return "SOUND_LIGHTNING_TUBE_FILL_5";
			case 1675:
				return "SOUND_LIGHTNING_TUBE_FILL_10";
			case 1676:
				return "SOUND_MENUSPIN";
			case 1677:
				return "SOUND_MULTIPLIER_APPEARS";
			case 1678:
				return "SOUND_MULTIPLIER_HURRAHED";
			case 1679:
				return "SOUND_POKER_4OFAKIND";
			case 1680:
				return "SOUND_POKER_FLUSH";
			case 1681:
				return "SOUND_POKER_FULLHOUSE";
			case 1682:
				return "SOUND_POKERCHIPS";
			case 1683:
				return "SOUND_POKERSCORE";
			case 1684:
				return "SOUND_POWERGEM_CREATED";
			case 1685:
				return "SOUND_PULLEYS";
			case 1686:
				return "SOUND_QUEST_AWARD_WREATH";
			case 1687:
				return "SOUND_QUEST_GET";
			case 1688:
				return "SOUND_QUEST_MENU_BUTTON1";
			case 1689:
				return "SOUND_QUEST_NOTIFY";
			case 1690:
				return "SOUND_QUEST_ORB1";
			case 1691:
				return "SOUND_QUEST_ORB3";
			case 1692:
				return "SOUND_QUEST_SANDSTORM_COVER";
			case 1693:
				return "SOUND_QUEST_SANDSTORM_REVEAL";
			case 1694:
				return "SOUND_QUESTMENU_RELICCOMPLETE_OBJECT";
			case 1695:
				return "SOUND_QUESTMENU_RELICCOMPLETE_RUMBLE";
			case 1696:
				return "SOUND_QUESTMENU_RELICREVEALED_OBJECT";
			case 1697:
				return "SOUND_QUESTMENU_RELICREVEALED_RUMBLE";
			case 1698:
				return "SOUND_RANK_COUNTUP";
			case 1699:
				return "SOUND_RANKUP";
			case 1700:
				return "SOUND_REPLAY_POPUP";
			case 1701:
				return "SOUND_REWIND";
			case 1702:
				return "SOUND_SANDSTORM_TREASURE_REVEAL";
			case 1703:
				return "SOUND_SCRAMBLE";
			case 1704:
				return "SOUND_SECRETMOUSEOVER1";
			case 1705:
				return "SOUND_SECRETMOUSEOVER2";
			case 1706:
				return "SOUND_SECRETMOUSEOVER3";
			case 1707:
				return "SOUND_SECRETMOUSEOVER4";
			case 1708:
				return "SOUND_SECRETUNLOCKED";
			case 1709:
				return "SOUND_SIN500";
			case 1710:
				return "SOUND_SKULL_APPEAR";
			case 1711:
				return "SOUND_SKULL_BUSTED";
			case 1712:
				return "SOUND_SKULL_BUSTER";
			case 1713:
				return "SOUND_SKULLCOIN_FLIP";
			case 1714:
				return "SOUND_SKULLCOINLANDS";
			case 1715:
				return "SOUND_SKULLCOINLOSE";
			case 1716:
				return "SOUND_SKULLCOINWIN";
			case 1717:
				return "SOUND_SLIDE_MOVE";
			case 1718:
				return "SOUND_SLIDE_MOVE_SHORT";
			case 1719:
				return "SOUND_SLIDE_TOUCH";
			case 1720:
				return "SOUND_SMALL_EXPLODE";
			case 1721:
				return "SOUND_SPEEDMATCH1";
			case 1722:
				return "SOUND_SPEEDMATCH2";
			case 1723:
				return "SOUND_SPEEDMATCH3";
			case 1724:
				return "SOUND_SPEEDMATCH4";
			case 1725:
				return "SOUND_SPEEDMATCH5";
			case 1726:
				return "SOUND_SPEEDMATCH6";
			case 1727:
				return "SOUND_SPEEDMATCH7";
			case 1728:
				return "SOUND_SPEEDMATCH8";
			case 1729:
				return "SOUND_SPEEDMATCH9";
			case 1730:
				return "SOUND_TICK";
			case 1731:
				return "SOUND_TIMEBOMBEXPLODE";
			case 1732:
				return "SOUND_TIMEBONUS_5";
			case 1733:
				return "SOUND_TIMEBONUS_10";
			case 1734:
				return "SOUND_TIMEBONUS_APPEARS_5";
			case 1735:
				return "SOUND_TIMEBONUS_APPEARS_10";
			case 1736:
				return "SOUND_TOOLTIP";
			case 1737:
				return "SOUND_TOWER_HITS_TOP1";
			case 1738:
				return "SOUND_VOICE_AWESOME";
			case 1739:
				return "SOUND_VOICE_BLAZINGSPEED";
			case 1740:
				return "SOUND_VOICE_CHALLENGECOMPLETE";
			case 1741:
				return "SOUND_VOICE_EXCELLENT";
			case 1742:
				return "SOUND_VOICE_EXTRAORDINARY";
			case 1743:
				return "SOUND_VOICE_GAMEOVER";
			case 1744:
				return "SOUND_VOICE_GETREADY";
			case 1745:
				return "SOUND_VOICE_GO";
			case 1746:
				return "SOUND_VOICE_GOOD";
			case 1747:
				return "SOUND_VOICE_GOODBYE";
			case 1748:
				return "SOUND_VOICE_LEVELCOMPLETE";
			case 1749:
				return "SOUND_VOICE_NOMOREMOVES";
			case 1750:
				return "SOUND_VOICE_SPECTACULAR";
			case 1751:
				return "SOUND_VOICE_THIRTYSECONDS";
			case 1752:
				return "SOUND_VOICE_TIMEUP";
			case 1753:
				return "SOUND_VOICE_UNBELIEVABLE";
			case 1754:
				return "SOUND_VOICE_WELCOMEBACK";
			case 1755:
				return "SOUND_VOICE_WELCOMETOBEJEWELED";
			case 1756:
				return "SOUND_ZEN_CHECKOFF";
			case 1757:
				return "SOUND_ZEN_CHECKON";
			case 1758:
				return "SOUND_ZEN_COMBO_2";
			case 1759:
				return "SOUND_ZEN_DROPDOWNBUTTON";
			case 1760:
				return "SOUND_ZEN_MANTRA1";
			case 1761:
				return "SOUND_ZEN_MENUCLOSE";
			case 1762:
				return "SOUND_ZEN_MENUEXPAND";
			case 1763:
				return "SOUND_ZEN_MENUOPEN";
			case 1764:
				return "SOUND_ZEN_MENUSHRINK";
			case 1765:
				return "SOUND_ZEN_NECKLACE_1";
			case 1766:
				return "SOUND_ZEN_NECKLACE_2";
			case 1767:
				return "SOUND_ZEN_NECKLACE_3";
			case 1768:
				return "SOUND_ZEN_NECKLACE_4";
			case 1769:
				return "IMAGE_ARROW_01";
			case 1770:
				return "IMAGE_ARROW_02";
			case 1771:
				return "IMAGE_ARROW_03";
			case 1772:
				return "IMAGE_ARROW_04";
			case 1773:
				return "IMAGE_ARROW_05";
			case 1774:
				return "IMAGE_ARROW_06";
			case 1775:
				return "IMAGE_ARROW_07";
			case 1776:
				return "IMAGE_ARROW_08";
			case 1777:
				return "IMAGE_ARROW_09";
			case 1778:
				return "IMAGE_ARROW_10";
			case 1779:
				return "IMAGE_ARROW_GLOW";
			default:
				return "";
			}
		}

		public static ResourceId GetIdByStringId(string theStringId)
		{
			if (GlobalMembersResourcesWP.GetIdByStringId_aMap.Count == 0)
			{
				for (int i = 0; i < 1810; i++)
				{
					GlobalMembersResourcesWP.GetIdByStringId_aMap[GlobalMembersResourcesWP.GetStringIdById(i)] = i;
				}
			}
			int result;
			if (GlobalMembersResourcesWP.GetIdByStringId_aMap.TryGetValue(theStringId, ref result))
			{
				return (ResourceId)result;
			}
			return ResourceId.RESOURCE_ID_INVALID;
		}

		internal static Image GetImageThrow(ResourceManager theManager, int theId, string theStringId, int artRes, int localeId)
		{
			return GlobalMembersResourcesWP.GetImageThrow(theManager, theId, theStringId, artRes, localeId, false);
		}

		internal static Image GetImageThrow(ResourceManager theManager, int theId, string theStringId, int artRes)
		{
			return GlobalMembersResourcesWP.GetImageThrow(theManager, theId, theStringId, artRes, 0, false);
		}

		internal static Image GetImageThrow(ResourceManager theManager, int theId, string theStringId)
		{
			return GlobalMembersResourcesWP.GetImageThrow(theManager, theId, theStringId, 0, 0, false);
		}

		internal static Image GetImageThrow(ResourceManager theManager, int theId, string theStringId, int artRes, int localeId, bool optional)
		{
			return Res.GetImageByID((ResourceId)theId);
		}

		internal static Font GetFontThrow(ResourceManager theManager, int theId, string theStringId, int artRes)
		{
			return GlobalMembersResourcesWP.GetFontThrow(theManager, theId, theStringId, artRes, 0);
		}

		internal static Font GetFontThrow(ResourceManager theManager, int theId, string theStringId)
		{
			return GlobalMembersResourcesWP.GetFontThrow(theManager, theId, theStringId, 0, 0);
		}

		internal static Font GetFontThrow(ResourceManager theManager, int theId, string theStringId, int artRes, int localeId)
		{
			return Res.GetFontByID((ResourceId)theId);
		}

		internal static int GetSoundThrow(ResourceManager theManager, int theId, string theStringId, int artRes)
		{
			return GlobalMembersResourcesWP.GetSoundThrow(theManager, theId, theStringId, artRes, 0);
		}

		internal static int GetSoundThrow(ResourceManager theManager, int theId, string theStringId)
		{
			return GlobalMembersResourcesWP.GetSoundThrow(theManager, theId, theStringId, 0, 0);
		}

		internal static int GetSoundThrow(ResourceManager theManager, int theId, string theStringId, int artRes, int localeId)
		{
			return Res.GetSoundByID((ResourceId)theId);
		}

		internal static PopAnim GetPopAnimThrow(ResourceManager theManager, int theId, string theStringId, int artRes)
		{
			return GlobalMembersResourcesWP.GetPopAnimThrow(theManager, theId, theStringId, artRes, 0);
		}

		internal static PopAnim GetPopAnimThrow(ResourceManager theManager, int theId, string theStringId)
		{
			return GlobalMembersResourcesWP.GetPopAnimThrow(theManager, theId, theStringId, 0, 0);
		}

		internal static PopAnim GetPopAnimThrow(ResourceManager theManager, int theId, string theStringId, int artRes, int localeId)
		{
			return Res.GetPopAnimByID((ResourceId)theId);
		}

		internal static PIEffect GetPIEffectThrow(ResourceManager theManager, int theId, string theStringId, int artRes)
		{
			return GlobalMembersResourcesWP.GetPIEffectThrow(theManager, theId, theStringId, artRes, 0);
		}

		internal static PIEffect GetPIEffectThrow(ResourceManager theManager, int theId, string theStringId)
		{
			return GlobalMembersResourcesWP.GetPIEffectThrow(theManager, theId, theStringId, 0, 0);
		}

		internal static PIEffect GetPIEffectThrow(ResourceManager theManager, int theId, string theStringId, int artRes, int localeId)
		{
			return Res.GetPIEffectByID((ResourceId)theId);
		}

		internal static RenderEffectDefinition GetRenderEffectThrow(ResourceManager theManager, int theId, string theStringId, int artRes)
		{
			return GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, theId, theStringId, artRes, 0);
		}

		internal static RenderEffectDefinition GetRenderEffectThrow(ResourceManager theManager, int theId, string theStringId)
		{
			return GlobalMembersResourcesWP.GetRenderEffectThrow(theManager, theId, theStringId, 0, 0);
		}

		internal static RenderEffectDefinition GetRenderEffectThrow(ResourceManager theManager, int theId, string theStringId, int artRes, int localeId)
		{
			return null;
		}

		internal static GenericResFile GetGenericResFileThrow(ResourceManager theManager, int theId, string theStringId, int artRes)
		{
			return GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, theId, theStringId, artRes, 0);
		}

		internal static GenericResFile GetGenericResFileThrow(ResourceManager theManager, int theId, string theStringId)
		{
			return GlobalMembersResourcesWP.GetGenericResFileThrow(theManager, theId, theStringId, 0, 0);
		}

		internal static GenericResFile GetGenericResFileThrow(ResourceManager theManager, int theId, string theStringId, int artRes, int localeId)
		{
			GenericResFile genericResFileThrow = theManager.GetGenericResFileThrow(theStringId);
			lock (GlobalMembersResourcesWP.gVarToIdMapCrit)
			{
				if (genericResFileThrow != null)
				{
					GlobalMembersResourcesWP.gVarToIdMap.Add(genericResFileThrow, theId);
				}
			}
			return genericResFileThrow;
		}

		internal static ResourceId GetIdByVariable(object theVariable)
		{
			if (theVariable == null)
			{
				return ResourceId.RESOURCE_ID_INVALID;
			}
			ResourceId result;
			lock (GlobalMembersResourcesWP.gVarToIdMapCrit)
			{
				int num;
				if (GlobalMembersResourcesWP.gVarToIdMap.TryGetValue(theVariable, ref num))
				{
					result = (ResourceId)num;
				}
				else
				{
					result = ResourceId.RESOURCE_ID_INVALID;
				}
			}
			return result;
		}

		private static bool InitResourceManager_sAlreadyRun = false;

		private static Dictionary<string, int> GetIdByStringId_aMap = new Dictionary<string, int>();

		internal static Dictionary<object, int> gVarToIdMap = new Dictionary<object, int>();

		internal static Point[] gImgOffsets = new Point[1810];

		public static object gVarToIdMapCrit = new object();

		public static Image IMAGE_AWARD_GLOW;

		public static Image ATLASIMAGE_ATLAS_BADGES_480_00;

		public static Image IMAGE_BADGES_SMALL_ANNIHILATOR;

		public static Image IMAGE_BADGES_SMALL_ANTE_UP;

		public static Image IMAGE_BADGES_SMALL_BEJEWELER;

		public static Image IMAGE_BADGES_SMALL_BLASTER;

		public static Image IMAGE_BADGES_SMALL_BUTTERFLY_BONANZA;

		public static Image IMAGE_BADGES_SMALL_BUTTERFLY_MONARCH;

		public static Image IMAGE_BADGES_SMALL_CHAIN_REACTION;

		public static Image IMAGE_BADGES_SMALL_CHROMATIC;

		public static Image IMAGE_BADGES_SMALL_DIAMOND_MINE;

		public static Image IMAGE_BADGES_SMALL_DYNAMO;

		public static Image IMAGE_BADGES_SMALL_ELECTRIFIER;

		public static Image IMAGE_BADGES_SMALL_HIGH_VOLTAGE;

		public static Image IMAGE_BADGES_SMALL_INFERNO;

		public static Image IMAGE_BADGES_SMALL_LEVELORD;

		public static Image IMAGE_BADGES_SMALL_LUCKY_STREAK;

		public static Image IMAGE_BADGES_SMALL_MILLIONAIRE;

		public static Image IMAGE_BADGES_SMALL_RELIC_HUNTER;

		public static Image IMAGE_BADGES_SMALL_RINGS;

		public static Image IMAGE_BADGES_SMALL_STELLAR;

		public static Image IMAGE_BADGES_SMALL_SUPERSTAR;

		public static Image IMAGE_BADGES_SMALL_THE_GAMBLER;

		public static Image IMAGE_BADGES_SMALL_UNKNOWN;

		public static Image ATLASIMAGE_ATLAS_BADGES_960_00;

		public static Image IMAGE_BADGES_BIG_ANNIHILATOR;

		public static Image IMAGE_BADGES_BIG_ANTE_UP;

		public static Image IMAGE_BADGES_BIG_BEJEWELER;

		public static Image IMAGE_BADGES_BIG_BLASTER;

		public static Image IMAGE_BADGES_BIG_BRONZE;

		public static Image IMAGE_BADGES_BIG_BUTTERFLY_BONANZA;

		public static Image IMAGE_BADGES_BIG_BUTTERFLY_MONARCH;

		public static Image IMAGE_BADGES_BIG_CHAIN_REACTION;

		public static Image IMAGE_BADGES_BIG_CHROMATIC;

		public static Image IMAGE_BADGES_BIG_DIAMOND_MINE;

		public static Image IMAGE_BADGES_BIG_DYNAMO;

		public static Image IMAGE_BADGES_BIG_ELECTRIFIER;

		public static Image IMAGE_BADGES_BIG_ELITE;

		public static Image IMAGE_BADGES_BIG_GLACIAL_EXPLORER;

		public static Image IMAGE_BADGES_BIG_GOLD;

		public static Image IMAGE_BADGES_BIG_HEROES_WELCOME;

		public static Image IMAGE_BADGES_BIG_HIGH_VOLTAGE;

		public static Image IMAGE_BADGES_BIG_ICE_BREAKER;

		public static Image IMAGE_BADGES_BIG_INFERNO;

		public static Image IMAGE_BADGES_BIG_LEVELORD;

		public static Image IMAGE_BADGES_BIG_LUCKY_STREAK;

		public static Image IMAGE_BADGES_BIG_MILLIONAIRE;

		public static Image IMAGE_BADGES_BIG_PLATINUM;

		public static Image IMAGE_BADGES_BIG_RELIC_HUNTER;

		public static Image IMAGE_BADGES_BIG_SILVER;

		public static Image IMAGE_BADGES_BIG_STELLAR;

		public static Image IMAGE_BADGES_BIG_SUPERSTAR;

		public static Image IMAGE_BADGES_BIG_THE_GAMBLER;

		public static Image IMAGE_BADGES_BIG_TOP_SECRET;

		public static Font FONT_HIRES_DIALOG;

		public static Font FONT_LOWRES_DIALOG;

		public static Font FONT_HIRES_HEADER;

		public static Font FONT_LOWRES_HEADER;

		public static Font FONT_HIRES_SUBHEADER;

		public static Font FONT_LOWRES_SUBHEADER;

		public static Font FONT_HIRES_INGAME;

		public static Font FONT_LOWRES_INGAME;

		public static Image ATLASIMAGE_ATLAS_COMMON_480_00;

		public static Image IMAGE_ANIMS_CARD_GEM_SPARKLE2_0_SMALL_BLUR_STAR;

		public static Image IMAGE_HELP_BUTTERFLY_HELP_0_SMALL_BLUR_STAR;

		public static Image IMAGE_HELP_CARD_GEM_SPARKLE2_0_SMALL_BLUR_STAR;

		public static Image IMAGE_HELP_DIAMOND_SPARKLE_0_SMALL_BLUR_STAR;

		public static Image IMAGE_HELP_FLAMEGEM_HELP_0_FLAME63;

		public static Image IMAGE_HELP_FLAMEGEM_HELP_1_HELP_GREEN_NOSHDW;

		public static Image IMAGE_HELP_FLAMEGEM_HELP_2_SPARKLET;

		public static Image IMAGE_HELP_ICESTORM_HELP_0_RING;

		public static Image IMAGE_HELP_LIGHTNING_STEAMPULSE_0_BASIC_BLUR;

		public static Image IMAGE_HELP_STARGEM_HELP_0_SMALL_BLUR_STAR;

		public static Image IMAGE_HELP_STARGEM_HELP_1_STAR_GLOW;

		public static Image IMAGE_HELP_STARGEM_HELP_2_CORONAGLOW;

		public static Image IMAGE_HELP_STARGEM_HELP_3_HELP_GREEN_NOSHDW;

		public static Image IMAGE_PARTICLES_CRYSTALRAYS_0_RAY;

		public static Image IMAGE_PARTICLES_CRYSTALBALL_0_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_QUEST_DIG_COLLECT_BASE_0_SMALL_BLUR_STAR;

		public static Image IMAGE_PARTICLES_QUEST_DIG_COLLECT_BASE_1_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_QUEST_DIG_COLLECT_GOLD_0_SMALL_BLUR_STAR;

		public static Image IMAGE_PARTICLES_QUEST_DIG_COLLECT_GOLD_1_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_GEM_SANDSTORM_DIG_0_SOFT_CLUMPY;

		public static Image IMAGE_PARTICLES_BADGE_UPGRADE_0_ICE;

		public static Image IMAGE_PARTICLES_BADGE_UPGRADE_1_BADGE_GLOW;

		public static Image IMAGE_PARTICLES_BADGE_UPGRADE_2_CERCLEM;

		public static Image IMAGE_PARTICLES_BLOWING_SNOW_0_TEXTURE_01;

		public static Image IMAGE_PARTICLES_BOARD_FLAME_EMBERS_0_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_CARD_GEM_SPARKLE_0_SMALL_BLUR_STAR;

		public static Image IMAGE_PARTICLES_COINSPARKLE_0_FLARE;

		public static Image IMAGE_PARTICLES_COUNTDOWNBAR_0_SMALL_BLUR_STAR;

		public static Image IMAGE_PARTICLES_COUNTDOWNBAR_1_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_0_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_1_SNOWFLAKE;

		public static Image IMAGE_PARTICLES_DANGERSNOW_HARD_TOP_2_ICECHUNK;

		public static Image IMAGE_PARTICLES_DANGERSNOW_SOFT_0_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_DANGERSNOW_SOFT_1_SNOWFLAKE;

		public static Image IMAGE_PARTICLES_DISCOBALL_0_DISCO_GLOW;

		public static Image IMAGE_PARTICLES_DISCOBALL_1_DISCO_GLOW;

		public static Image IMAGE_PARTICLES_DISCOBALL_2_BLURRED_SHARP_STAR;

		public static Image IMAGE_PARTICLES_FIREGEM_HYPERSPACE_0_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_FLAME_CARD_0_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_GEM_BLASTGEM_0_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_GEM_BUTTERFLY_0_SMALL_BLUR_STAR;

		public static Image IMAGE_PARTICLES_GEM_BUTTERFLY_CREATE_0_FLOWER;

		public static Image IMAGE_PARTICLES_GEM_BUTTERFLY_CREATE_1_BLURRED_SHARP_STAR;

		public static Image IMAGE_PARTICLES_GEM_DIAMOND_SPARKLES_0_BLURRED_SHARP_STAR;

		public static Image IMAGE_PARTICLES_GEM_FIRE_TRAIL_0_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_GEM_GOLD_BLING_0_BLURRED_SHARP_STAR;

		public static Image IMAGE_PARTICLES_GEM_HINTFLASH_0_CERCLEM;

		public static Image IMAGE_PARTICLES_GEM_HYPERCUBE_0_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_GEM_ICE_TRAIL_0_BLURRED_SHARP_STAR;

		public static Image IMAGE_PARTICLES_GEM_ICE_TRAIL_1_COMIC_SMOKE2;

		public static Image IMAGE_PARTICLES_GEM_MULTIPLIER_0_RAY;

		public static Image IMAGE_PARTICLES_GEM_STARGEM_0_SMALL_BLUR_STAR;

		public static Image IMAGE_PARTICLES_GEM_STARGEM_1_STAR_GLOW;

		public static Image IMAGE_PARTICLES_GEM_STARGEM_2_CORONAGLOW;

		public static Image IMAGE_PARTICLES_ICE_STORMY_0_SHARD;

		public static Image IMAGE_PARTICLES_LEVELBAR_0_SMALL_BLUR_STAR;

		public static Image IMAGE_PARTICLES_LEVELBAR_1_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_SANDSTORM_COVER_0_SAND_PARTICLE;

		public static Image IMAGE_PARTICLES_SKULL_EXPLODE_0_DOT_STREAK_01;

		public static Image IMAGE_PARTICLES_SKULL_EXPLODE_1_SKULL;

		public static Image IMAGE_PARTICLES_SKULL_EXPLODE_2_SMALL_BLUR_STAR;

		public static Image IMAGE_PARTICLES_SPEEDBOARD_FLAME_0_FLAME1;

		public static Image IMAGE_PARTICLES_STAR_CARD_0_BLURRED_SHARP_STAR;

		public static Image IMAGE_PARTICLES_STAR_CARD_1_SMALL_BLUR_STAR;

		public static Image IMAGE_PARTICLES_STARBURST_0_STAR;

		public static Image IMAGE_PARTICLES_WEIGHT_FIRE_0_TRUEFLAME7;

		public static Image IMAGE_PARTICLES_WEIGHT_FIRE_1_BASIC_BLUR;

		public static Image IMAGE_PARTICLES_WEIGHT_ICE_0_DIM_BLUR_CLOUD;

		public static Image IMAGE_PARTICLES_WEIGHT_ICE_1_ICESHARD_0000;

		public static Image IMAGE_QUEST_DIG_DIG_LINE_HIT_0_BASIC_BLUR;

		public static Image IMAGE_QUEST_DIG_DIG_LINE_HIT_MEGA_0_BLURRED_SHARP_STAR;

		public static Image IMAGE_QUEST_DIG_DIG_LINE_HIT_MEGA_1_DIAMOND_STAR;

		public static Image IMAGE_MAIN_MENU_LOGO;

		public static Image IMAGE_MAIN_MENU_CLOUDS;

		public static Image IMAGE_MAIN_MENU_BACKGROUND;

		public static Image IMAGE_DASHBOARD_CLOSED_BUTTON;

		public static Image IMAGE_DASHBOARD_DASH_MAIN;

		public static Image IMAGE_DASHBOARD_DASH_TILE;

		public static Image IMAGE_DASHBOARD_DM_OVERLAY;

		public static Image IMAGE_DASHBOARD_ICE_OVERLAY;

		public static Image IMAGE_DASHBOARD_MENU_DOWN;

		public static Image IMAGE_DASHBOARD_MENU_UP;

		public static Image IMAGE_PPS0;

		public static Image IMAGE_PPS1;

		public static Image IMAGE_PPS2;

		public static Image IMAGE_PPS3;

		public static Image IMAGE_PPS4;

		public static Image IMAGE_PPS5;

		public static Image IMAGE_PPS6;

		public static Image IMAGE_PPS7;

		public static Image IMAGE_PPS8;

		public static Image IMAGE_PPS9;

		public static Image IMAGE_PPS10;

		public static Image IMAGE_PPS11;

		public static Image IMAGE_PPS12;

		public static Image IMAGE_PPS13;

		public static Image IMAGE_PPS14;

		public static Image IMAGE_PPS15;

		public static Image IMAGE_PPS16;

		public static Image IMAGE_PPS17;

		public static Image IMAGE_PPS18;

		public static Image IMAGE_PPS19;

		public static Image IMAGE_PPS20;

		public static Image IMAGE_PPS21;

		public static Image IMAGE_PPS22;

		public static Image IMAGE_PPS23;

		public static Image IMAGE_PPS24;

		public static Image IMAGE_PPS25;

		public static Image IMAGE_PPS26;

		public static Image IMAGE_PPS27;

		public static Image IMAGE_PPS28;

		public static Image IMAGE_PPS29;

		public static Image IMAGE_TOOLTIP;

		public static Image IMAGE_TOOLTIP_ARROW_ARROW_DOWN;

		public static Image IMAGE_TOOLTIP_ARROW_ARROW_LEFT;

		public static Image IMAGE_TOOLTIP_ARROW_ARROW_RIGHT;

		public static Image IMAGE_TOOLTIP_ARROW_ARROW_UP;

		public static Image IMAGE_CRYSTALBALL_SHADOW;

		public static Image IMAGE_CRYSTALBALL_GLOW;

		public static Image IMAGE_DIALOG_ARROW_SWIPE;

		public static Image IMAGE_DIALOG_ARROW_SWIPEGLOW;

		public static Image IMAGE_DIALOG_BLACK_BOX;

		public static Image IMAGE_DIALOG_BUTTON_DROPDOWN;

		public static Image IMAGE_DIALOG_BUTTON_FRAME;

		public static Image IMAGE_DIALOG_BUTTON_FRAME_DIAMOND_MINE;

		public static Image IMAGE_DIALOG_BUTTON_FRAME_ICE_STORM;

		public static Image IMAGE_DIALOG_BUTTON_GAMECENTER;

		public static Image IMAGE_DIALOG_BUTTON_GAMECENTER_BG;

		public static Image IMAGE_DIALOG_BUTTON_LARGE;

		public static Image IMAGE_DIALOG_BUTTON_SMALL_BG;

		public static Image IMAGE_DIALOG_BUTTON_SMALL_BLUE;

		public static Image IMAGE_DIALOG_CHECKBOX;

		public static Image IMAGE_DIALOG_CHECKBOX_CHECKED;

		public static Image IMAGE_DIALOG_CHECKBOX_UNSELECTED;

		public static Image IMAGE_DIALOG_DIALOG_BOX_INTERIOR_BG;

		public static Image IMAGE_DIALOG_DIALOG_CORNER;

		public static Image IMAGE_DIALOG_DIALOG_SWIPE_BOTTOM_EDGE;

		public static Image IMAGE_DIALOG_DIALOG_SWIPE_TOP_EDGE;

		public static Image IMAGE_DIALOG_DIVIDER;

		public static Image IMAGE_DIALOG_DIVIDER_GEM;

		public static Image IMAGE_DIALOG_HELP_GLOW;

		public static Image IMAGE_DIALOG_ICON_FLAME_LRG;

		public static Image IMAGE_DIALOG_ICON_HYPERCUBE_LRG;

		public static Image IMAGE_DIALOG_ICON_STAR_LRG;

		public static Image IMAGE_DIALOG_LISTBOX_BG;

		public static Image IMAGE_DIALOG_LISTBOX_BG_DARK;

		public static Image IMAGE_DIALOG_LISTBOX_FOOTER;

		public static Image IMAGE_DIALOG_LISTBOX_HEADER;

		public static Image IMAGE_DIALOG_LISTBOX_SHADOW;

		public static Image IMAGE_DIALOG_MINE_TILES_GEM;

		public static Image IMAGE_DIALOG_MINE_TILES_GOLD;

		public static Image IMAGE_DIALOG_MINE_TILES_TREASURE;

		public static Image IMAGE_DIALOG_PROGRESS_BAR;

		public static Image IMAGE_DIALOG_PROGRESS_BAR_BG;

		public static Image IMAGE_DIALOG_PROGRESS_BAR_CROWN;

		public static Image IMAGE_DIALOG_PROGRESS_BAR_FILL;

		public static Image IMAGE_DIALOG_PROGRESS_BAR_GLOW;

		public static Image IMAGE_DIALOG_REPLAY;

		public static Image IMAGE_DIALOG_SCROLLBAR;

		public static Image IMAGE_DIALOG_SFX_ICONS_MUSIC;

		public static Image IMAGE_DIALOG_SFX_ICONS_MUSIC_UNSELECTED;

		public static Image IMAGE_DIALOG_SFX_ICONS_SOUND;

		public static Image IMAGE_DIALOG_SFX_ICONS_SOUND_UNSELECTED;

		public static Image IMAGE_DIALOG_SFX_ICONS_VOICES;

		public static Image IMAGE_DIALOG_SFX_ICONS_VOICES_UNSELECTED;

		public static Image IMAGE_DIALOG_SLIDER_BAR_HANDLE;

		public static Image IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL;

		public static Image IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL;

		public static Image IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_FILL_UNSE;

		public static Image IMAGE_DIALOG_SLIDER_BAR_HORIZONTAL_UNSELECTE;

		public static Image IMAGE_DIALOG_SLIDER_BAR_VERTICAL;

		public static Image IMAGE_DIALOG_SLIDER_BAR_VERTICAL_FILL;

		public static Image IMAGE_DIALOG_SLIDER_BAR_VERTICAL_FILL_UNSELE;

		public static Image IMAGE_DIALOG_SLIDER_BAR_VERTICAL_UNSELECTED;

		public static Image IMAGE_DIALOG_TEXTBOX;

		public static Image IMAGE_ALPHA_ALPHA_DOWN;

		public static Image IMAGE_ALPHA_ALPHA_UP;

		public static Image ATLASIMAGE_ATLAS_COMMON_960_00;

		public static GenericResFile RESFILE_PROPERTIES_MUSIC;

		public static PIEffect PIEFFECT_ANIMS_CARD_GEM_SPARKLE2;

		public static PIEffect PIEFFECT_HELP_BUTTERFLY_HELP;

		public static PIEffect PIEFFECT_HELP_CARD_GEM_SPARKLE2;

		public static PIEffect PIEFFECT_HELP_DIAMOND_SPARKLE;

		public static PIEffect PIEFFECT_HELP_FLAMEGEM_HELP;

		public static PIEffect PIEFFECT_HELP_ICESTORM_HELP;

		public static PIEffect PIEFFECT_HELP_LIGHTNING_STEAMPULSE;

		public static PIEffect PIEFFECT_HELP_STARGEM_HELP;

		public static PIEffect PIEFFECT_CRYSTALBALL;

		public static PIEffect PIEFFECT_CRYSTALRAYS;

		public static PIEffect PIEFFECT_QUEST_DIG_COLLECT_BASE;

		public static PIEffect PIEFFECT_QUEST_DIG_COLLECT_GOLD;

		public static PIEffect PIEFFECT_SANDSTORM_DIG;

		public static PIEffect PIEFFECT_BADGE_UPGRADE;

		public static PIEffect PIEFFECT_BLASTGEM;

		public static PIEffect PIEFFECT_BLOWING_SNOW;

		public static PIEffect PIEFFECT_BOARD_FLAME_EMBERS;

		public static PIEffect PIEFFECT_BUTTERFLY;

		public static PIEffect PIEFFECT_BUTTERFLY_CREATE;

		public static PIEffect PIEFFECT_CARD_GEM_SPARKLE;

		public static PIEffect PIEFFECT_COINSPARKLE;

		public static PIEffect PIEFFECT_COUNTDOWNBAR;

		public static PIEffect PIEFFECT_DANGERSNOW_HARD_TOP;

		public static PIEffect PIEFFECT_DANGERSNOW_SOFT;

		public static PIEffect PIEFFECT_DIAMOND_SPARKLES;

		public static PIEffect PIEFFECT_DISCOBALL;

		public static PIEffect PIEFFECT_FIRE_TRAIL;

		public static PIEffect PIEFFECT_FIREGEM_HYPERSPACE;

		public static PIEffect PIEFFECT_FLAME_CARD;

		public static PIEffect PIEFFECT_GOLD_BLING;

		public static PIEffect PIEFFECT_HINTFLASH;

		public static PIEffect PIEFFECT_HYPERCUBE;

		public static PIEffect PIEFFECT_ICE_STORMY;

		public static PIEffect PIEFFECT_ICE_TRAIL;

		public static PIEffect PIEFFECT_LEVELBAR;

		public static PIEffect PIEFFECT_MULTIPLIER;

		public static PIEffect PIEFFECT_SANDSTORM_COVER;

		public static PIEffect PIEFFECT_SKULL_EXPLODE;

		public static PIEffect PIEFFECT_SPEEDBOARD_FLAME;

		public static PIEffect PIEFFECT_STAR_CARD;

		public static PIEffect PIEFFECT_STARBURST;

		public static PIEffect PIEFFECT_STARGEM;

		public static PIEffect PIEFFECT_WEIGHT_FIRE;

		public static PIEffect PIEFFECT_WEIGHT_ICE;

		public static PIEffect PIEFFECT_QUEST_DIG_DIG_LINE_HIT;

		public static PIEffect PIEFFECT_QUEST_DIG_DIG_LINE_HIT_MEGA;

		public static int SOUND_BACKGROUND_CHANGE;

		public static int SOUND_MULTIPLIER_UP2_1;

		public static int SOUND_MULTIPLIER_UP2_2;

		public static int SOUND_MULTIPLIER_UP2_3;

		public static int SOUND_MULTIPLIER_UP2_4;

		public static int SOUND_BUTTON_MOUSEOVER;

		public static int SOUND_BUTTON_MOUSELEAVE;

		public static int SOUND_QUEST_MENU_BUTTON_MOUSEOVER1;

		public static int SOUND_COMBO_1;

		public static int SOUND_COMBO_2;

		public static int SOUND_COMBO_3;

		public static int SOUND_COMBO_4;

		public static int SOUND_COMBO_5;

		public static int SOUND_COMBO_6;

		public static int SOUND_COMBO_7;

		public static int SOUND_BADMOVE;

		public static int SOUND_FIREWORK_CRACKLE;

		public static int SOUND_FIREWORK_LAUNCH;

		public static int SOUND_FIREWORK_THUMP;

		public static int SOUND_GEM_HIT;

		public static int SOUND_PREBLAST;

		public static int SOUND_SELECT;

		public static int SOUND_START_ROTATE;

		public static int SOUND_ALCHEMY_CONVERT;

		public static int SOUND_BACKTOMAIN;

		public static int SOUND_BADGEAWARDED;

		public static int SOUND_BADGEFALL;

		public static int SOUND_BOMB_APPEARS;

		public static int SOUND_BOMB_EXPLODE;

		public static int SOUND_BREATH_IN;

		public static int SOUND_BREATH_OUT;

		public static int SOUND_BUTTERFLY_APPEAR;

		public static int SOUND_BUTTERFLY_DEATH1;

		public static int SOUND_BUTTERFLYESCAPE;

		public static int SOUND_BUTTON_PRESS;

		public static int SOUND_BUTTON_RELEASE;

		public static int SOUND_CARDDEAL;

		public static int SOUND_CARDFLIP;

		public static int SOUND_CLICKFLYIN;

		public static int SOUND_COIN_CREATED;

		public static int SOUND_COINAPPEAR;

		public static int SOUND_COLD_WIND;

		public static int SOUND_COUNTDOWN_WARNING;

		public static int SOUND_DIAMOND_MINE_ARTIFACT_SHOWCASE;

		public static int SOUND_DIAMOND_MINE_BIGSTONE_CRACKED;

		public static int SOUND_DIAMOND_MINE_DEATH;

		public static int SOUND_DIAMOND_MINE_DIG;

		public static int SOUND_DIAMOND_MINE_DIG_LINE_HIT;

		public static int SOUND_DIAMOND_MINE_DIG_LINE_HIT_MEGA;

		public static int SOUND_DIAMOND_MINE_DIG_NOTIFY;

		public static int SOUND_DIAMOND_MINE_DIRT_CRACKED;

		public static int SOUND_DIAMOND_MINE_STONE_CRACKED;

		public static int SOUND_DIAMOND_MINE_TREASUREFIND;

		public static int SOUND_DIAMOND_MINE_TREASUREFIND_DIAMONDS;

		public static int SOUND_DOUBLESET;

		public static int SOUND_EARTHQUAKE;

		public static int SOUND_ELECTRO_EXPLODE;

		public static int SOUND_ELECTRO_PATH;

		public static int SOUND_ELECTRO_PATH2;

		public static int SOUND_FLAMEBONUS;

		public static int SOUND_FLAMELOOP;

		public static int SOUND_FLAMESPEED1;

		public static int SOUND_GEM_COUNTDOWN_DESTROYED;

		public static int SOUND_GEM_SHATTERS;

		public static int SOUND_HINT;

		public static int SOUND_HYPERCUBE_CREATE;

		public static int SOUND_HYPERSPACE;

		public static int SOUND_HYPERSPACE_GEM_LAND_1;

		public static int SOUND_HYPERSPACE_GEM_LAND_2;

		public static int SOUND_HYPERSPACE_GEM_LAND_3;

		public static int SOUND_HYPERSPACE_GEM_LAND_4;

		public static int SOUND_HYPERSPACE_GEM_LAND_5;

		public static int SOUND_HYPERSPACE_GEM_LAND_6;

		public static int SOUND_HYPERSPACE_GEM_LAND_7;

		public static int SOUND_HYPERSPACE_GEM_LAND_ZEN_1;

		public static int SOUND_HYPERSPACE_GEM_LAND_ZEN_2;

		public static int SOUND_HYPERSPACE_GEM_LAND_ZEN_3;

		public static int SOUND_HYPERSPACE_GEM_LAND_ZEN_4;

		public static int SOUND_HYPERSPACE_GEM_LAND_ZEN_5;

		public static int SOUND_HYPERSPACE_GEM_LAND_ZEN_6;

		public static int SOUND_HYPERSPACE_GEM_LAND_ZEN_7;

		public static int SOUND_HYPERSPACE_SHATTER_1;

		public static int SOUND_HYPERSPACE_SHATTER_2;

		public static int SOUND_HYPERSPACE_SHATTER_ZEN;

		public static int SOUND_ICE_COLUMN_APPEARS;

		public static int SOUND_ICE_COLUMN_BREAK;

		public static int SOUND_ICE_STORM_COLUMNCOMBO;

		public static int SOUND_ICE_STORM_COLUMNCOMBO_MEGA;

		public static int SOUND_ICE_STORM_FINAL_THUD;

		public static int SOUND_ICE_STORM_GAMEOVER;

		public static int SOUND_ICE_STORM_MULTIPLER_UP;

		public static int SOUND_ICE_STORM_STEAM_BUILD_UP;

		public static int SOUND_ICE_STORM_STEAM_VALVE;

		public static int SOUND_ICE_STORM_WIND;

		public static int SOUND_ICE_WARNING;

		public static int SOUND_LASERGEM_CREATED;

		public static int SOUND_LIGHTNING_ENERGIZE;

		public static int SOUND_LIGHTNING_HUMLOOP;

		public static int SOUND_LIGHTNING_TUBE_FILL_5;

		public static int SOUND_LIGHTNING_TUBE_FILL_10;

		public static int SOUND_MENUSPIN;

		public static int SOUND_MULTIPLIER_APPEARS;

		public static int SOUND_MULTIPLIER_HURRAHED;

		public static int SOUND_POKER_4OFAKIND;

		public static int SOUND_POKER_FLUSH;

		public static int SOUND_POKER_FULLHOUSE;

		public static int SOUND_POKERCHIPS;

		public static int SOUND_POKERSCORE;

		public static int SOUND_POWERGEM_CREATED;

		public static int SOUND_PULLEYS;

		public static int SOUND_QUEST_AWARD_WREATH;

		public static int SOUND_QUEST_GET;

		public static int SOUND_QUEST_MENU_BUTTON1;

		public static int SOUND_QUEST_NOTIFY;

		public static int SOUND_QUEST_ORB1;

		public static int SOUND_QUEST_ORB3;

		public static int SOUND_QUEST_SANDSTORM_COVER;

		public static int SOUND_QUEST_SANDSTORM_REVEAL;

		public static int SOUND_QUESTMENU_RELICCOMPLETE_OBJECT;

		public static int SOUND_QUESTMENU_RELICCOMPLETE_RUMBLE;

		public static int SOUND_QUESTMENU_RELICREVEALED_OBJECT;

		public static int SOUND_QUESTMENU_RELICREVEALED_RUMBLE;

		public static int SOUND_RANK_COUNTUP;

		public static int SOUND_RANKUP;

		public static int SOUND_REPLAY_POPUP;

		public static int SOUND_REWIND;

		public static int SOUND_SANDSTORM_TREASURE_REVEAL;

		public static int SOUND_SCRAMBLE;

		public static int SOUND_SECRETMOUSEOVER1;

		public static int SOUND_SECRETMOUSEOVER2;

		public static int SOUND_SECRETMOUSEOVER3;

		public static int SOUND_SECRETMOUSEOVER4;

		public static int SOUND_SECRETUNLOCKED;

		public static int SOUND_SIN500;

		public static int SOUND_SKULL_APPEAR;

		public static int SOUND_SKULL_BUSTED;

		public static int SOUND_SKULL_BUSTER;

		public static int SOUND_SKULLCOIN_FLIP;

		public static int SOUND_SKULLCOINLANDS;

		public static int SOUND_SKULLCOINLOSE;

		public static int SOUND_SKULLCOINWIN;

		public static int SOUND_SLIDE_MOVE;

		public static int SOUND_SLIDE_MOVE_SHORT;

		public static int SOUND_SLIDE_TOUCH;

		public static int SOUND_SMALL_EXPLODE;

		public static int SOUND_SPEEDMATCH1;

		public static int SOUND_SPEEDMATCH2;

		public static int SOUND_SPEEDMATCH3;

		public static int SOUND_SPEEDMATCH4;

		public static int SOUND_SPEEDMATCH5;

		public static int SOUND_SPEEDMATCH6;

		public static int SOUND_SPEEDMATCH7;

		public static int SOUND_SPEEDMATCH8;

		public static int SOUND_SPEEDMATCH9;

		public static int SOUND_TICK;

		public static int SOUND_TIMEBOMBEXPLODE;

		public static int SOUND_TIMEBONUS_5;

		public static int SOUND_TIMEBONUS_10;

		public static int SOUND_TIMEBONUS_APPEARS_5;

		public static int SOUND_TIMEBONUS_APPEARS_10;

		public static int SOUND_TOOLTIP;

		public static int SOUND_TOWER_HITS_TOP1;

		public static int SOUND_ZEN_CHECKOFF;

		public static int SOUND_ZEN_CHECKON;

		public static int SOUND_ZEN_COMBO_2;

		public static int SOUND_ZEN_DROPDOWNBUTTON;

		public static int SOUND_ZEN_MANTRA1;

		public static int SOUND_ZEN_MENUCLOSE;

		public static int SOUND_ZEN_MENUEXPAND;

		public static int SOUND_ZEN_MENUOPEN;

		public static int SOUND_ZEN_MENUSHRINK;

		public static int SOUND_ZEN_NECKLACE_1;

		public static int SOUND_ZEN_NECKLACE_2;

		public static int SOUND_ZEN_NECKLACE_3;

		public static int SOUND_ZEN_NECKLACE_4;

		public static GenericResFile RESFILE_PROPERTIES_BADGES;

		public static GenericResFile RESFILE_PROPERTIES_DEFAULT;

		public static GenericResFile RESFILE_PROPERTIES_DEFAULTFILENAMES;

		public static GenericResFile RESFILE_PROPERTIES_DEFAULTFRAMEWORK;

		public static GenericResFile RESFILE_PROPERTIES_DEFAULTQUEST;

		public static GenericResFile RESFILE_PROPERTIES_DEFAULTUICONSTANTS;

		public static GenericResFile RESFILE_PROPERTIES_QUEST;

		public static GenericResFile RESFILE_PROPERTIES_RANKS;

		public static GenericResFile RESFILE_PROPERTIES_SECRET;

		public static GenericResFile RESFILE_PROPERTIES_SPEED;

		public static GenericResFile RESFILE_PROPERTIES_TIPS;

		public static int SOUND_VOICE_AWESOME;

		public static int SOUND_VOICE_BLAZINGSPEED;

		public static int SOUND_VOICE_CHALLENGECOMPLETE;

		public static int SOUND_VOICE_EXCELLENT;

		public static int SOUND_VOICE_EXTRAORDINARY;

		public static int SOUND_VOICE_GAMEOVER;

		public static int SOUND_VOICE_GETREADY;

		public static int SOUND_VOICE_GO;

		public static int SOUND_VOICE_GOOD;

		public static int SOUND_VOICE_GOODBYE;

		public static int SOUND_VOICE_LEVELCOMPLETE;

		public static int SOUND_VOICE_NOMOREMOVES;

		public static int SOUND_VOICE_SPECTACULAR;

		public static int SOUND_VOICE_THIRTYSECONDS;

		public static int SOUND_VOICE_TIMEUP;

		public static int SOUND_VOICE_UNBELIEVABLE;

		public static int SOUND_VOICE_WELCOMEBACK;

		public static int SOUND_VOICE_WELCOMETOBEJEWELED;

		public static Image IMAGE_BACKGROUNDS_BRIDGE_SHROOM_CASTLE;

		public static Image IMAGE_BACKGROUNDS_CAVE;

		public static Image IMAGE_BACKGROUNDS_CRYSTALTOWERS;

		public static Image IMAGE_BACKGROUNDS_DAVE_CAVE_THING;

		public static Image IMAGE_BACKGROUNDS_DESERT;

		public static Image IMAGE_BACKGROUNDS_FLOATING_ROCK_CITY;

		public static Image IMAGE_BACKGROUNDS_FLYING_SAIL_BOAT;

		public static Image IMAGE_BACKGROUNDS_LANTERN_PLANTS_WORLD;

		public static Image IMAGE_BACKGROUNDS_LION_TOWER_CASCADE;

		public static Image IMAGE_BACKGROUNDS_LION_TOWER_CASCADE_BFLY;

		public static Image IMAGE_BACKGROUNDS_POINTY_ICE_PATH;

		public static Image IMAGE_BACKGROUNDS_POKER;

		public static Image IMAGE_BACKGROUNDS_SNOWY_CLIFFS_CASTLE;

		public static Image IMAGE_BACKGROUNDS_TUBE_FOREST_NIGHT;

		public static Image IMAGE_BACKGROUNDS_WATER_BUBBLES_CITY;

		public static Image IMAGE_BACKGROUNDS_WATER_FALL_CLIFF;

		public static Image IMAGE_BACKGROUNDS_WATER_PATH_RUINS;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAY_480_00;

		public static Image IMAGE_GEMS_RED;

		public static Image IMAGE_GEMS_WHITE;

		public static Image IMAGE_GEMS_GREEN;

		public static Image IMAGE_GEMS_YELLOW;

		public static Image IMAGE_GEMS_PURPLE;

		public static Image IMAGE_GEMS_ORANGE;

		public static Image IMAGE_GEMS_BLUE;

		public static Image IMAGE_GEMSSHADOW_RED;

		public static Image IMAGE_GEMSSHADOW_WHITE;

		public static Image IMAGE_GEMSSHADOW_GREEN;

		public static Image IMAGE_GEMSSHADOW_YELLOW;

		public static Image IMAGE_GEMSSHADOW_PURPLE;

		public static Image IMAGE_GEMSSHADOW_ORANGE;

		public static Image IMAGE_GEMSSHADOW_BLUE;

		public static Image IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME1;

		public static Image IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME2;

		public static Image IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME3;

		public static Image IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME4;

		public static Image IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME5;

		public static Image IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME6;

		public static Image IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME7;

		public static Image IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME8;

		public static Image IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME9;

		public static Image IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME10;

		public static Image IMAGE_FLAMEGEMCREATION_ANIMATED_FLAME_CELLS02SM_FLAME11;

		public static Image IMAGE_FLAMEGEMCREATION_FLAMEGEM_BLUR;

		public static Image IMAGE_FLAMEGEMCREATION_FLAMEGEM_FLASH_1;

		public static Image IMAGE_FLAMEGEMCREATION_FLAMEGEM_FLASH_2;

		public static Image IMAGE_FLAMEGEMCREATION_FLAMEGEM_LARGE_RING;

		public static Image IMAGE_FLAMEGEMCREATION_FLAMEGEM_RING_OF_FLAME;

		public static Image IMAGE_FLAMEGEMEXPLODE_FLAMEEXPLODETEST_LAYER_1;

		public static Image IMAGE_BOOM_NOVA;

		public static Image IMAGE_BOOM_NUKE;

		public static Image IMAGE_BOARD_IRIS;

		public static Image IMAGE_GEMS_SHADOWED;

		public static Image IMAGE_GEM_FRUIT_SPARK;

		public static Image IMAGE_SMOKE;

		public static Image IMAGE_DRIP;

		public static Image IMAGE_FX_STEAM;

		public static Image IMAGE_SPARKLET;

		public static Image IMAGE_DIAMOND_MINE_TEXT_CYCLE;

		public static Image IMAGE_ELECTROTEX;

		public static Image IMAGE_ELECTROTEX_CENTER;

		public static Image IMAGE_HYPERFLARELINE;

		public static Image IMAGE_HYPERFLARERING;

		public static Image IMAGE_SELECTOR;

		public static Image IMAGE_HINTARROW;

		public static Image IMAGE_DANGERBORDERLEFT;

		public static Image IMAGE_DANGERBORDERUP;

		public static Image IMAGE_HYPERCUBE_COLORGLOW;

		public static Image IMAGE_HYPERCUBE_FRAME;

		public static Image IMAGE_SHADER_TEST;

		public static Image IMAGE_LIGHTNING;

		public static Image IMAGE_GRITTYBLURRY;

		public static Image IMAGE_LIGHTNING_CENTER;

		public static Image IMAGE_LIGHTNING_TEX;

		public static Image IMAGE_SPARKLE;

		public static Image IMAGE_SM_SHARDS;

		public static Image IMAGE_SM_SHARDS_OUTLINE;

		public static Image IMAGE_FIREPARTICLE;

		public static Image IMAGE_GEMSNORMAL_RED;

		public static Image IMAGE_GEMSNORMAL_WHITE;

		public static Image IMAGE_GEMSNORMAL_GREEN;

		public static Image IMAGE_GEMSNORMAL_YELLOW;

		public static Image IMAGE_GEMSNORMAL_PURPLE;

		public static Image IMAGE_GEMSNORMAL_ORANGE;

		public static Image IMAGE_GEMSNORMAL_BLUE;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAY_960_00;

		public static PopAnim POPANIM_FLAMEGEMCREATION;

		public static PopAnim POPANIM_FLAMEGEMEXPLODE;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAY_UI_DIG_480_00;

		public static Image IMAGE_INGAMEUI_DIAMOND_MINE_BACKGROUND;

		public static Image IMAGE_INGAMEUI_DIAMOND_MINE_BOARD_SEPERATOR_FRAME;

		public static Image IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_LEVEL;

		public static Image IMAGE_INGAMEUI_DIAMOND_MINE_DEPTH_METER;

		public static Image IMAGE_INGAMEUI_DIAMOND_MINE_HUD_SHADOW;

		public static Image IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_BACK;

		public static Image IMAGE_INGAMEUI_DIAMOND_MINE_PROGRESS_BAR_FRAME;

		public static Image IMAGE_INGAMEUI_DIAMOND_MINE_SCORE_BAR_BACK;

		public static Image IMAGE_INGAMEUI_DIAMOND_MINE_TIMER;

		public static Image IMAGE_QUEST_DIG_COGS_COGS_113X114;

		public static Image IMAGE_QUEST_DIG_COGS_COGS_165X165;

		public static Image IMAGE_QUEST_DIG_COGS_COGS_166X166;

		public static Image IMAGE_QUEST_DIG_COGS_COGS_202X202;

		public static Image IMAGE_QUEST_DIG_COGS_COGS_63X64;

		public static Image IMAGE_QUEST_DIG_COGS_COGS_63X64_2;

		public static Image IMAGE_QUEST_DIG_COGS_COGS_89X90;

		public static Image IMAGE_QUEST_DIG_COGS_COGS_90X90;

		public static Image IMAGE_QUEST_DIG_COGS_COGS_96X96;

		public static Image IMAGE_QUEST_DIG_COGS_COGS_96X96_2;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAY_UI_DIG_960_00;

		public static PopAnim POPANIM_QUEST_DIG_COGS;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAY_UI_NORMAL_480_00;

		public static Image IMAGE_INGAMEUI_BOARD_SEPERATOR_FRAME;

		public static Image IMAGE_INGAMEUI_PROGRESS_BAR_BACK;

		public static Image IMAGE_INGAMEUI_PROGRESS_BAR_FRAME;

		public static Image IMAGE_INGAMEUI_REPLAY_BUTTON;

		public static Image IMAGE_LEVELBAR_ENDPIECE;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAY_UI_NORMAL_960_00;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BALANCE_480_00;

		public static Image IMAGE_QUEST_BALANCE_FRAME;

		public static Image IMAGE_BALANCE_RIG_ARROW;

		public static Image IMAGE_BALANCE_RIG_GLOW;

		public static Image IMAGE_BALANCE_RIG_SIDE_CHAIN;

		public static Image IMAGE_BALANCE_RIG_TOP_CHAIN;

		public static Image IMAGE_BALANCE_RIG_WHEEL;

		public static Image IMAGE_BALANCE_RIG_WHEEL_SPOKES;

		public static Image IMAGE_WEIGHT_CAP;

		public static Image IMAGE_WEIGHT_FILL;

		public static Image IMAGE_WEIGHT_GLASS_BACK;

		public static Image IMAGE_WEIGHT_GLASS_FRONT;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BALANCE_960_00;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BUTTERFLY_480_00;

		public static Image IMAGE_BUTTERFLY_BODY;

		public static Image IMAGE_BUTTERFLY_SHADOW;

		public static Image IMAGE_BUTTERFLY_WINGS;

		public static Image IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_BOTTOM;

		public static Image IMAGE_INGAMEUI_BUTTERFLIES_BOARD_SEPERATOR_FRAME_TOP;

		public static Image IMAGE_INGAMEUI_BUTTERFLIES_BUTTERFLY;

		public static Image IMAGE_INGAMEUI_BUTTERFLIES_SCORE_BG;

		public static Image IMAGE_INGAMEUI_BUTTERFLIES_SCORE_FRAME;

		public static Image IMAGE_INGAMEUI_BUTTERFLIES_WEB;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_26X50;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_26X52;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_27X42;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_27X42_2;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_28X50;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_29X41;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_31X41;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_33X51;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_35X34;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_38X21;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_40X36;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_41X46;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_42X40;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_44X27;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_46X35;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_47X39;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_53X41;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_55X49;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_57X33;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_61X43;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_63X53;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_72X38;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_84X108;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_87X102;

		public static Image IMAGE_ANIMS_SPIDER_SPIDER_91X336;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_137X175;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_144X162;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_149X204;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_20X476;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_34X62;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_40X67;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_41X65;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_43X65;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_45X67;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_46X53;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_49X57;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_51X60;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_59X34;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_64X53;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_69X40;

		public static Image IMAGE_ANIMS_LARGE_SPIDER_LARGE_SPIDER_71X36;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_BUTTERFLY_960_00;

		public static PopAnim POPANIM_ANIMS_SPIDER;

		public static PopAnim POPANIM_ANIMS_LARGE_SPIDER;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_DIG_480_00;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGETPART1;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGETPART2;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGETPART3;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGETPART4;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGETPART5;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGETPART6;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGETPART7;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGETPART8;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGETPART9;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGETPART10;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET1_1;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET1_2;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET1_3;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET2_1;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET2_2;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET2_3;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET3_1;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET3_2;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET3_3;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET4_1;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET4_2;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET4_3;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET5_1;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET5_2;

		public static Image IMAGE_QUEST_DIG_BOARD_NUGGET5_3;

		public static Image IMAGE_QUEST_DIG_BOARD_BOTTOM_OVERLAY;

		public static Image IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM;

		public static Image IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM_HIGHLIGHT;

		public static Image IMAGE_QUEST_DIG_BOARD_CENTER_BOTTOM_HIGHLIGHT_SHADOW;

		public static Image IMAGE_QUEST_DIG_BOARD_CENTER_FULL;

		public static Image IMAGE_QUEST_DIG_BOARD_CENTER_LEFT;

		public static Image IMAGE_QUEST_DIG_BOARD_CENTER_LEFT_HIGHLIGHT;

		public static Image IMAGE_QUEST_DIG_BOARD_CENTER_LEFT_HIGHLIGHT_SHADOW;

		public static Image IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT;

		public static Image IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT__HIGHLIGHT;

		public static Image IMAGE_QUEST_DIG_BOARD_CENTER_RIGHT_HIGHLIGHT_SHADOW;

		public static Image IMAGE_QUEST_DIG_BOARD_CENTER_TOP;

		public static Image IMAGE_QUEST_DIG_BOARD_CENTER_TOP_HIGHLIGHT;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND1;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND1_1;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND1_2;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND1_3;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND1_4;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND1_DIRT;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART1;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART2;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART3;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND1_PART4;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND2;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND2_1;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND2_2;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND2_3;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND2_4;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND2_DIRT;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART1;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART2;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART3;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND2_PART4;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND3;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND3_1;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND3_2;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND3_3;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND3_DIRT;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART1;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART2;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART3;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND3_PART4;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND4;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND4_1;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND4_2;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND4_3;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND4_4;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND4_DIRT;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART1;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART2;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART3;

		public static Image IMAGE_QUEST_DIG_BOARD_DIAMOND4_PART4;

		public static Image IMAGE_QUEST_DIG_BOARD_GOLDGROUP1;

		public static Image IMAGE_QUEST_DIG_BOARD_GOLDGROUP2;

		public static Image IMAGE_QUEST_DIG_BOARD_GOLDGROUP3;

		public static Image IMAGE_QUEST_DIG_BOARD_GRASS;

		public static Image IMAGE_QUEST_DIG_BOARD_GRASS_LEFT;

		public static Image IMAGE_QUEST_DIG_BOARD_GRASS_RIGHT;

		public static Image IMAGE_QUEST_DIG_BOARD_HYPERCUBE;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_ABICUS_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_ANVIL_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_ASTROLABE_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_AXE_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_BELL_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_BJORN_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_BOOK_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_BOOTS_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_BOWARROW_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_BOWL_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_BRUSH_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_CLOCK_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_COMB_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_CREST_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_DAGGER_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_DISH_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_DMGEM_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_FLUTE_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_FORK_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_FROG_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_GAUNTLET_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_GEAR_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_HAMMER_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_HARP_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_HELMET_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_HORN_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_HORSE_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_HORSESHOE_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_KEY_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_LAMP_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_MACE_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_MAGNIFYINGGLASS_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_MASK_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_POT_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_SCROLL_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_SCYTHE_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_SEXTANT_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_SPOON_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_STAFF_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_STIRRUP_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_TELESCOPE_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_TONGS_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_TRIDENT_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_TROWEL_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_URN_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_ITEM_VASE_BIG;

		public static Image IMAGE_QUEST_DIG_BOARD_PEBBLES1;

		public static Image IMAGE_QUEST_DIG_BOARD_PEBBLES2;

		public static Image IMAGE_QUEST_DIG_BOARD_PEBBLES3;

		public static Image IMAGE_QUEST_DIG_BOARD_STR1;

		public static Image IMAGE_QUEST_DIG_BOARD_STR2;

		public static Image IMAGE_QUEST_DIG_BOARD_STR3;

		public static Image IMAGE_QUEST_DIG_BOARD_STR4;

		public static Image IMAGE_QUEST_DIG_DIRT_OVERLAY1;

		public static Image IMAGE_QUEST_DIG_DIRT_OVERLAY2;

		public static Image IMAGE_QUEST_DIG_DIRT_OVERLAY3;

		public static Image IMAGE_QUEST_DIG_DIRT_UNDERLAY1;

		public static Image IMAGE_QUEST_DIG_DIRT_UNDERLAY2;

		public static Image IMAGE_QUEST_DIG_DIRT_UNDERLAY3;

		public static Image IMAGE_QUEST_DIG_GLOW;

		public static Image IMAGE_QUEST_DIG_DIAMONDPART;

		public static Image IMAGE_QUEST_DIG_STREAK;

		public static Image IMAGE_WALLROCKS_LARGE;

		public static Image IMAGE_WALLROCKS_MEDIUM;

		public static Image IMAGE_WALLROCKS_SMALL;

		public static Image IMAGE_WALLROCKS_SMALL_BROWN;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_DIG_960_00;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_FILLER_480_00;

		public static Image IMAGE_MYSTERY_CIRCLE;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_FILLER_960_00;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_INFERNO_480_00;

		public static Image IMAGE_INGAMEUI_ICE_STORM_ICE_BOTTOM;

		public static Image IMAGE_INGAMEUI_ICE_STORM_ICE_LIQUID;

		public static Image IMAGE_INGAMEUI_ICE_STORM_ICE_METER;

		public static Image IMAGE_INGAMEUI_ICE_STORM_ICE_METER_ICE;

		public static Image IMAGE_INGAMEUI_ICE_STORM_ICE_METER_PIPE;

		public static Image IMAGE_INGAMEUI_ICE_STORM_MULTIPLIER;

		public static Image IMAGE_INGAMEUI_ICE_STORM_TOP_FRAME;

		public static Image IMAGE_ANIMS_COLUMN1_AQUAFRESH;

		public static Image IMAGE_ANIMS_COLUMN1_AQUAFRESH2;

		public static Image IMAGE_ANIMS_COLUMN1_AQUAFRESHRED;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK01;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK02;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK03;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK04;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK05;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK06;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK07;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK08;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK09;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK10;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK11;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK12;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK13;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK14;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK15;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK16;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK17;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK18;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK19;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK20;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK21;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK22;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK23;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK24;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK25;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK26;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK27;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK28;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK29;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CHUNK30;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK1A;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK1B;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK2A;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK2B;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK3A;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK3B;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK4A;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK4B;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK5A;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRACK5B;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH1;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH2;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_CRUSH3;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_GLOW;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_GLOW_RED;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_METER;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_METER_RED;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_PULSE;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_SUPERCRACK;

		public static Image IMAGE_ANIMS_COLUMN1_COLUMN1_SUPERCRACK_RED;

		public static Image IMAGE_ANIMS_COLUMN1_FBOMB_SMALL_0_ICECHUNK;

		public static Image IMAGE_ANIMS_COLUMN1_FBOMB_SMALL_1_TWIRL_SOFT;

		public static Image IMAGE_ANIMS_COLUMN1_SHATTERLEFT_SMALL_0_ICECHUNK;

		public static Image IMAGE_ANIMS_COLUMN1_SHATTERRIGHT_SMALL_0_ICECHUNK;

		public static Image IMAGE_ANIMS_COLUMN1_SNOWCRUSH_0_ICECHUNK;

		public static Image IMAGE_ANIMS_COLUMN2_AQUAFRESH;

		public static Image IMAGE_ANIMS_COLUMN2_AQUAFRESH2;

		public static Image IMAGE_ANIMS_COLUMN2_AQUAFRESHRED;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK01;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK02;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK03;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK04;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK05;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK06;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK07;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK08;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK09;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK10;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK11;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK12;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK13;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK14;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK15;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK16;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK17;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK18;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK19;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK20;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK21;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK22;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK23;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK24;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK25;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK26;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK27;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK28;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK29;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CHUNK30;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK1A;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK1B;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK2A;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK2B;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK3A;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK3B;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK4A;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK4B;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK5A;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRACK5B;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH1;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH2;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_CRUSH3;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_GLOW;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_GLOW_RED;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_METER;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_METER_RED;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_PULSE;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_SUPERCRACK;

		public static Image IMAGE_ANIMS_COLUMN2_COLUMN2_SUPERCRACK_RED;

		public static Image IMAGE_ANIMS_COLUMN2_FBOMB_0_ICECHUNK;

		public static Image IMAGE_ANIMS_COLUMN2_FBOMB_1_TWIRL_SOFT;

		public static Image IMAGE_ANIMS_COLUMN2_SHATTERLEFT_0_ICECHUNK;

		public static Image IMAGE_ANIMS_COLUMN2_SHATTERRIGHT_0_ICECHUNK;

		public static Image IMAGE_ANIMS_COLUMN2_SNOWCRUSH_0_ICECHUNK;

		public static Image IMAGE_ANIMS_FROSTPANIC_FROSTPANIC_RED;

		public static Image IMAGE_ANIMS_FROSTPANIC_FROSTPANIC_SKULL;

		public static Image IMAGE_QUEST_INFERNO_LAVA_FROST_BOTTOM;

		public static Image IMAGE_QUEST_INFERNO_LAVA_FROST_LOSE;

		public static Image IMAGE_QUEST_INFERNO_LAVA_FROST_TOP;

		public static Image IMAGE_QUEST_INFERNO_LAVA_FROST_TOP_UNDER;

		public static Image IMAGE_QUEST_INFERNO_LAVA_ICECHUNK;

		public static Image IMAGE_QUEST_INFERNO_LAVA_MOUNTAINDOUBLE;

		public static Image IMAGE_QUEST_INFERNO_LAVA_MOUNTAINSINGLE;

		public static Image IMAGE_QUEST_INFERNO_LAVA_SNOWFLAKE_PARTICLE;

		public static Image IMAGE_QUEST_INFERNO_LAVA_UI_TOP_FRAME;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_INFERNO_960_00;

		public static PopAnim POPANIM_ANIMS_COLUMN1;

		public static PopAnim POPANIM_ANIMS_COLUMN2;

		public static PopAnim POPANIM_ANIMS_FROSTPANIC;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_LIGHTNING_480_00;

		public static Image IMAGE_LIGHTNING_GEMNUMS_RED;

		public static Image IMAGE_LIGHTNING_GEMNUMS_WHITE;

		public static Image IMAGE_LIGHTNING_GEMNUMS_GREEN;

		public static Image IMAGE_LIGHTNING_GEMNUMS_YELLOW;

		public static Image IMAGE_LIGHTNING_GEMNUMS_PURPLE;

		public static Image IMAGE_LIGHTNING_GEMNUMS_ORANGE;

		public static Image IMAGE_LIGHTNING_GEMNUMS_BLUE;

		public static Image IMAGE_LIGHTNING_GEMNUMS_CLEAR;

		public static Image IMAGE_GEMOUTLINES;

		public static Image IMAGE_INGAMEUI_LIGHTNING_BOARD_SEPARATOR_FRAME;

		public static Image IMAGE_INGAMEUI_LIGHTNING_EXTRA_TIME_METER;

		public static Image IMAGE_INGAMEUI_LIGHTNING_MULTIPLIER;

		public static Image IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_BACK;

		public static Image IMAGE_INGAMEUI_LIGHTNING_PROGRESS_BAR_FRAME;

		public static Image IMAGE_INGAMEUI_LIGHTNING_TIMER;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_LIGHTNING_960_00;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_POKER_480_00;

		public static Image IMAGE_INGAME_POKER_BOARD_SEPERATOR_FRAME_BOTTOM;

		public static Image IMAGE_INGAME_POKER_BOARD_SEPERATOR_FRAME_TOP;

		public static Image IMAGE_INGAME_POKER_HAND;

		public static Image IMAGE_INGAME_POKER_HAND_GLOW;

		public static Image IMAGE_INGAME_POKER_HAND_SKULL;

		public static Image IMAGE_POKER_BAR_SKULL;

		public static Image IMAGE_POKER_BKG;

		public static Image IMAGE_POKER_DECK;

		public static Image IMAGE_POKER_DECK_SHADOW;

		public static Image IMAGE_POKER_LARGE_SKULL;

		public static Image IMAGE_POKER_LIGHT_LIT;

		public static Image IMAGE_POKER_LIGHT_UNLIT;

		public static Image IMAGE_POKER_LONG_BKG;

		public static Image IMAGE_POKER_SCORE_BKG;

		public static Image IMAGE_POKER_SCORE_GLOW;

		public static Image IMAGE_POKER_SKULL;

		public static Image IMAGE_POKER_SKULL_BAR_COVER;

		public static Image IMAGE_POKER_SKULL_CRUSHER_BAR;

		public static Image IMAGE_POKER_SKULL_CRUSHER_BKG;

		public static Image IMAGE_POKER_SKULL_CRUSHER_BORDER;

		public static Image IMAGE_POKER_SKULL_CRUSHER_GLOW;

		public static Image IMAGE_POKER_SKULL_SLASH;

		public static Image IMAGE_POKER_SLASH_SHADOW;

		public static Image IMAGE_CARDS_BACK;

		public static Image IMAGE_CARDS_DECK;

		public static Image IMAGE_CARDS_DECK_SHADOW;

		public static Image IMAGE_CARDS_FACE;

		public static Image IMAGE_CARDS_FRONT;

		public static Image IMAGE_CARDS_SHADOW;

		public static Image IMAGE_CARDS_SMALL_FACE;

		public static Image IMAGE_CARDS_WILD;

		public static Image IMAGE_SKULL_COIN_SET1;

		public static Image IMAGE_SKULL_COIN_SET2;

		public static Image IMAGE_SKULL_COIN_SET3;

		public static Image IMAGE_SKULL_COIN_SET4;

		public static Image IMAGE_SKULL_COIN_SIDE;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_POKER_960_00;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_TIMEBOMB_480_00;

		public static Image IMAGE_BOMBGEMS;

		public static Image IMAGE_BOMBGLOWS_DANGERGLOW;

		public static Image IMAGE_BOMBGLOWS_GLOW;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_TIMEBOMB_960_00;

		public static Image IMAGE_QUEST_LEAD2GOLD_BLACK;

		public static Image IMAGE_QUEST_LEAD2GOLD_LEAD;

		public static Image IMAGE_QUEST_LEAD2GOLD_GOLD;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_WALLBLAST_480_00;

		public static Image IMAGE_QUEST_WALLBLAST_BOARD_MASK;

		public static Image IMAGE_QUEST_WALLBLAST_BOARD_WALL;

		public static Image ATLASIMAGE_ATLAS_GAMEPLAYQUEST_WALLBLAST_960_00;

		public static Image ATLASIMAGE_ATLAS_GIFTGAME_480_00;

		public static Image IMAGE_GIFTTHEGAME;

		public static Image ATLASIMAGE_ATLAS_GIFTGAME_960_00;

		public static Image ATLASIMAGE_ATLAS_HELP_BASIC_480_00;

		public static Image IMAGE_HELP_SWAP3_SWAP3_128X128;

		public static Image IMAGE_HELP_SWAP3_SWAP3_128X128_2;

		public static Image IMAGE_HELP_SWAP3_SWAP3_128X128_3;

		public static Image IMAGE_HELP_SWAP3_SWAP3_128X128_4;

		public static Image IMAGE_HELP_SWAP3_SWAP3_128X128_5;

		public static Image IMAGE_HELP_SWAP3_SWAP3_384X384;

		public static Image IMAGE_HELP_SWAP3_SWAP3_50X43;

		public static Image IMAGE_HELP_MATCH4_MATCH4_128X128;

		public static Image IMAGE_HELP_MATCH4_MATCH4_128X128_2;

		public static Image IMAGE_HELP_MATCH4_MATCH4_128X128_3;

		public static Image IMAGE_HELP_MATCH4_MATCH4_128X128_4;

		public static Image IMAGE_HELP_MATCH4_MATCH4_128X128_5;

		public static Image IMAGE_HELP_MATCH4_MATCH4_128X128_6;

		public static Image IMAGE_HELP_MATCH4_MATCH4_128X128_7;

		public static Image IMAGE_HELP_MATCH4_MATCH4_291X384;

		public static Image IMAGE_HELP_MATCH4_MATCH4_50X43;

		public static Image IMAGE_HELP_STARGEM_STARGEM_128X128;

		public static Image IMAGE_HELP_STARGEM_STARGEM_128X128_2;

		public static Image IMAGE_HELP_STARGEM_STARGEM_128X128_3;

		public static Image IMAGE_HELP_STARGEM_STARGEM_128X128_4;

		public static Image IMAGE_HELP_STARGEM_STARGEM_128X128_5;

		public static Image IMAGE_HELP_STARGEM_STARGEM_128X128_6;

		public static Image IMAGE_HELP_STARGEM_STARGEM_128X128_7;

		public static Image IMAGE_HELP_STARGEM_STARGEM_128X128_8;

		public static Image IMAGE_HELP_STARGEM_STARGEM_291X384;

		public static Image IMAGE_HELP_STARGEM_STARGEM_50X43;

		public static Image ATLASIMAGE_ATLAS_HELP_BASIC_960_00;

		public static PopAnim POPANIM_HELP_SWAP3;

		public static PopAnim POPANIM_HELP_MATCH4;

		public static PopAnim POPANIM_HELP_STARGEM;

		public static Image ATLASIMAGE_ATLAS_HELP_BFLY_480_00;

		public static Image IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128;

		public static Image IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_2;

		public static Image IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_3;

		public static Image IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_4;

		public static Image IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_5;

		public static Image IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_128X128_6;

		public static Image IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_384X384;

		public static Image IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_390X519;

		public static Image IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_42X88;

		public static Image IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_50X43;

		public static Image IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_52X117;

		public static Image IMAGE_HELP_BFLY_MATCH_BFLY_MATCH_53X95;

		public static Image IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128;

		public static Image IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_2;

		public static Image IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_3;

		public static Image IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_4;

		public static Image IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_5;

		public static Image IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_6;

		public static Image IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_7;

		public static Image IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_128X128_8;

		public static Image IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_389X70;

		public static Image IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_390X519;

		public static Image IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_50X43;

		public static Image IMAGE_HELP_BFLY_SPIDER_BFLY_SPIDER_82X164;

		public static Image ATLASIMAGE_ATLAS_HELP_BFLY_960_00;

		public static PopAnim POPANIM_HELP_BFLY_MATCH;

		public static PopAnim POPANIM_HELP_BFLY_SPIDER;

		public static Image ATLASIMAGE_ATLAS_HELP_DIAMONDMINE_480_00;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_11X14;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_2;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_3;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_4;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_5;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_6;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_7;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_128X128_8;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_13X15;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_18X34;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_20X15;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_22X22;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_27X29;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104_2;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_291X104_3;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_292X384;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_30X35;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_34X31;

		public static Image IMAGE_HELP_DIAMOND_MATCH_DIAMOND_MATCH_50X43;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_2;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_3;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_4;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_5;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_6;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_7;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_128X128_8;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_13X15;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_20X15;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_258X338;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_50X43;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X1;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X9;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X13;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X17;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X20;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X22;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X26;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X31;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X36;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X40;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X42;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X46;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X51;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X56;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66_2;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X66_3;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X71;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X74;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X81;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_512X81_2;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_83X81;

		public static Image IMAGE_HELP_DIAMOND_ADVANCE_DIAMOND_ADVANCE_83X81_2;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_11X14;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_2;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_3;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_4;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_5;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_6;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_128X128_7;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_13X15;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_20X15;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_24X29;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_27X26;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_291X104;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_291X104_2;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_292X384;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_50X43;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_54X41;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_78X82;

		public static Image IMAGE_HELP_DIAMOND_GOLD_DIAMOND_GOLD_78X82_2;

		public static Image ATLASIMAGE_ATLAS_HELP_DIAMONDMINE_960_00;

		public static PopAnim POPANIM_HELP_DIAMOND_MATCH;

		public static PopAnim POPANIM_HELP_DIAMOND_ADVANCE;

		public static PopAnim POPANIM_HELP_DIAMOND_GOLD;

		public static Image ATLASIMAGE_ATLAS_HELP_ICESTORM_480_00;

		public static Image IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128;

		public static Image IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_2;

		public static Image IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_3;

		public static Image IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_4;

		public static Image IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_5;

		public static Image IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_6;

		public static Image IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_7;

		public static Image IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_128X128_8;

		public static Image IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_393X502;

		public static Image IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_50X43;

		public static Image IMAGE_HELP_ICESTORM_HORIZ_ICESTORM_HORIZ_88X278;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_106X234;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_111X55;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_2;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_3;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_4;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_5;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_6;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_7;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_8;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_9;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_10;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_11;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_12;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_113X20_13;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_128X128;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_2;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_3;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_4;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_5;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_6;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_7;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_8;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_9;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_26X36_10;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_394X502;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_85X68;

		public static Image IMAGE_HELP_ICESTORM_METER_ICESTORM_METER_8X10;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_118X44;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_2;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_3;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_4;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_5;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_6;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_7;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_128X128_8;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_2;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_3;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_4;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_5;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_6;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_7;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_8;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_9;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_10;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_11;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_12;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_13;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_14;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_15;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_16;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_17;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_18;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_19;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_20;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_21;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_22;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_162X271_23;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_393X502;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_50X43;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_83X38;

		public static Image IMAGE_HELP_ICESTORM_VERT_ICESTORM_VERT_88X278;

		public static Image ATLASIMAGE_ATLAS_HELP_ICESTORM_960_00;

		public static PopAnim POPANIM_HELP_ICESTORM_HORIZ;

		public static PopAnim POPANIM_HELP_ICESTORM_METER;

		public static PopAnim POPANIM_HELP_ICESTORM_VERT;

		public static Image ATLASIMAGE_ATLAS_HELP_LIGHTNING_480_00;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_2;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_3;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_4;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_5;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_6;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_128X128_7;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_13X52;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_18X19;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_19X25;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_2;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_3;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_4;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_5;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_6;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_21X30_7;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_23X34;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_25X37;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_27X54;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_35X14;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_383X504;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_2;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_3;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_4;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_5;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_6;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_7;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_384X84_8;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_48X25;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_50X43;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_2;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_3;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_71X88_4;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_74X56;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_2;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_3;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_4;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_5;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_6;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_7;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_8;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_9;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_10;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_11;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_83X90_12;

		public static Image IMAGE_HELP_LIGHTNING_MATCH_LIGHTNING_MATCH_93X62;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_2;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_3;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_4;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_128X128_5;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_13X52;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_147X93;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_161X68;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_168X72;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_168X79;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_18X19;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_19X25;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30_2;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_21X30_3;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_23X34;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_25X37;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_2;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_3;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_4;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_5;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_6;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_7;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_8;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_9;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_26X36_10;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_27X54;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_2;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_3;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_4;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_300X232_5;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_317X342;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_31X57;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_35X14;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_383X504;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_2;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_3;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_4;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_5;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_6;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_7;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_384X84_8;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_48X25;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_49X69;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_2;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_3;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_71X88_4;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_74X56;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_74X56_2;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_75X34;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_75X35;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_83X66;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_83X66_2;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_8X10;

		public static Image IMAGE_HELP_LIGHTNING_TIME_LIGHTNING_TIME_93X62;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_2;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_3;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_156X43_4;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_168X72;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_168X79;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_2;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_3;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_4;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_5;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_6;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_7;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_21X30_8;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_2;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_3;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_4;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_5;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_227X30_6;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_2;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_3;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_4;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_5;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_6;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_7;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_8;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_9;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_26X36_10;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_272X153;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_27X35;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_27X35_2;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_31X57;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_32X34;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_32X34_2;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_383X504;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_383X504_2;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_2;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_3;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_4;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_38X43_5;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_75X35;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_83X66;

		public static Image IMAGE_HELP_LIGHTNING_SPEED_LIGHTNING_SPEED_8X10;

		public static Image ATLASIMAGE_ATLAS_HELP_LIGHTNING_960_00;

		public static PopAnim POPANIM_HELP_LIGHTNING_MATCH;

		public static PopAnim POPANIM_HELP_LIGHTNING_TIME;

		public static PopAnim POPANIM_HELP_LIGHTNING_SPEED;

		public static Image ATLASIMAGE_ATLAS_HELP_POKER_480_00;

		public static Image IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128;

		public static Image IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_2;

		public static Image IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_3;

		public static Image IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_4;

		public static Image IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_5;

		public static Image IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_6;

		public static Image IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_7;

		public static Image IMAGE_HELP_POKER_MATCH_POKER_MATCH_128X128_8;

		public static Image IMAGE_HELP_POKER_MATCH_POKER_MATCH_383X510;

		public static Image IMAGE_HELP_POKER_MATCH_POKER_MATCH_50X43;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_128X128;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_148X158;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_152X48;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_168X60;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_206X38;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_240X52;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_2;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_3;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_4;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_5;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_6;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_7;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_8;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_9;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_26X36_10;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_44X38;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_464X687;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_61X35;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_75X76;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_81X110;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_81X110_2;

		public static Image IMAGE_HELP_POKER_SKULL_CLEAR_POKER_SKULL_CLEAR_8X10;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_2;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_3;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_128X128_4;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_138X73;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_148X158;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_152X48;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_206X38;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_2;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_3;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_4;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_5;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_6;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_7;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_8;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_9;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_26X36_10;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_358X63;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_464X687;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_81X110;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_81X110_2;

		public static Image IMAGE_HELP_POKER_SKULLHAND_POKER_SKULLHAND_8X10;

		public static Image ATLASIMAGE_ATLAS_HELP_POKER_960_00;

		public static PopAnim POPANIM_HELP_POKER_MATCH;

		public static PopAnim POPANIM_HELP_POKER_SKULL_CLEAR;

		public static PopAnim POPANIM_HELP_POKER_SKULLHAND;

		public static Image ATLASIMAGE_ATLAS_HELP_UNUSED_480_00;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_2;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_3;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_4;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_5;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_6;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_7;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_8;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_9;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_10;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_11;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_12;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_156X43_13;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_2;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_3;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_4;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_5;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_21X30_6;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_2;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_3;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_4;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_5;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_6;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_227X30_7;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_2;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_3;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_4;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_5;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_6;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_7;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_8;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_9;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_26X36_10;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_32X34;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_374X346;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_38X43;

		public static Image IMAGE_HELP_SPEEDBONUS_SPEEDBONUS_38X43_2;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_156X43;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_2;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_3;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_4;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_5;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_21X30_6;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_227X30;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_2;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_3;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_4;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_5;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_6;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_7;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_8;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_9;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_26X36_10;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_32X34;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_374X346;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_2;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_3;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_4;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_5;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_6;

		public static Image IMAGE_HELP_SPEEDBONUS2_SPEEDBONUS2_38X43_7;

		public static Image ATLASIMAGE_ATLAS_HELP_UNUSED_960_00;

		public static PopAnim POPANIM_HELP_SPEEDBONUS;

		public static PopAnim POPANIM_HELP_SPEEDBONUS2;

		public static Image ATLASIMAGE_ATLAS_HIDDENOBJECT_480_00;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_BOTTOM_SAND;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ1;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ2;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_OBJ3;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L1_PLAQUE;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ1;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ2;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_OBJ3;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L2_PLAQUE;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ1;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ2;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ3;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_OBJ4;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_L3_PLAQUE;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_PLAQUE;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_SAND_MASK;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_SAND_MASK_SOFT;

		public static Image IMAGE_QUEST_HIDDENOBJECT_BOARD_TOP_SAND;

		public static Image ATLASIMAGE_ATLAS_HIDDENOBJECT_960_00;

		public static Image IMAGE_HYPERSPACE_WHIRLPOOL_INITIAL;

		public static Image IMAGE_HYPERSPACE_WHIRLPOOL_FIRERING;

		public static Image IMAGE_HYPERSPACE_WHIRLPOOL_TUNNELEND;

		public static Image IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE;

		public static Image IMAGE_HYPERSPACE_WHIRLPOOL_BLACK_HOLE_COVER;

		public static Image IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_NORMAL;

		public static Image IMAGE_HYPERSPACE_WHIRLPOOL_HYPERSPACE_ZEN;

		public static Image ATLASIMAGE_ATLAS_IGNORED_480_00;

		public static Image IMAGE_POKER_SCORE_BOARD;

		public static Image IMAGE_QUEST_INFERNO_COLUMN2_GLOW;

		public static Image IMAGE_QUEST_INFERNO_COLUMN1_GLOW;

		public static Image IMAGE_WEIGHT_FILL_MASK;

		public static Image ATLASIMAGE_ATLAS_IGNORED_960_00;

		public static RenderEffectDefinition EFFECT_BADGE_GRAYSCALE;

		public static RenderEffectDefinition EFFECT_BOARD_3D;

		public static RenderEffectDefinition EFFECT_FRAME_INTERP;

		public static RenderEffectDefinition EFFECT_GEM_3D;

		public static RenderEffectDefinition EFFECT_GEM_LIGHT;

		public static RenderEffectDefinition EFFECT_GEM_SUN;

		public static RenderEffectDefinition EFFECT_GRAYSCALE;

		public static RenderEffectDefinition EFFECT_GRAYSCALE_COLORIZE;

		public static RenderEffectDefinition EFFECT_MASK;

		public static RenderEffectDefinition EFFECT_MERGE_COLOR_ALPHA;

		public static RenderEffectDefinition EFFECT_REWIND;

		public static RenderEffectDefinition EFFECT_SCREEN_DISTORT;

		public static RenderEffectDefinition EFFECT_TUBE_3D;

		public static RenderEffectDefinition EFFECT_TUBECAP_3D;

		public static RenderEffectDefinition EFFECT_UNDER_DIALOG;

		public static RenderEffectDefinition EFFECT_WAVE;

		public static Image ATLASIMAGE_ATLAS_LOADER_480_00;

		public static Image IMAGE_LOADER_WHITEDOT;

		public static Image IMAGE_LOADER_POPCAP_BLACK_TM;

		public static Image IMAGE_LOADER_POPCAP_LOADER_POPCAP;

		public static Image IMAGE_LOADER_POPCAP_WHITE_GERMAN_REGISTERED;

		public static Image ATLASIMAGE_ATLAS_LOADER_960_00;

		public static Image ATLASIMAGE_ATLAS_MAINMENU_480_00;

		public static Image IMAGE_MAIN_MENU_PILLARL;

		public static Image IMAGE_MAIN_MENU_PILLARR;

		public static Image IMAGE_MAIN_MENU_WHITE_GERMAN_REGISTERED;

		public static Image IMAGE_MAIN_MENU_WHITE_TM;

		public static Image IMAGE_CRYSTALBALL;

		public static Image IMAGE_ARROW_01;

		public static Image IMAGE_ARROW_02;

		public static Image IMAGE_ARROW_03;

		public static Image IMAGE_ARROW_04;

		public static Image IMAGE_ARROW_05;

		public static Image IMAGE_ARROW_06;

		public static Image IMAGE_ARROW_07;

		public static Image IMAGE_ARROW_08;

		public static Image IMAGE_ARROW_09;

		public static Image IMAGE_ARROW_10;

		public static Image IMAGE_ARROW_GLOW;

		public static Image ATLASIMAGE_ATLAS_MAINMENU_960_00;

		public static Image IMAGE_BADGES_GRAY_ICON_BEJEWELER;

		public static Image IMAGE_BADGES_GRAY_ICON_BLASTER;

		public static Image IMAGE_BADGES_GRAY_ICON_BTF_BONANZA;

		public static Image IMAGE_BADGES_GRAY_ICON_BTF_MONARCH;

		public static Image IMAGE_BADGES_GRAY_ICON_CHAINREACTION;

		public static Image IMAGE_BADGES_GRAY_ICON_CHROMATIC;

		public static Image IMAGE_BADGES_GRAY_ICON_DIAMONDMINE;

		public static Image IMAGE_BADGES_GRAY_ICON_ELECTRIFIER;

		public static Image IMAGE_BADGES_GRAY_ICON_HIGHVOLTAGE;

		public static Image IMAGE_BADGES_GRAY_ICON_LEVELORD;

		public static Image IMAGE_BADGES_GRAY_ICON_LUCKYSTREAK;

		public static Image IMAGE_BADGES_GRAY_ICON_RELICHUNTER;

		public static Image IMAGE_BADGES_GRAY_ICON_STELLAR;

		public static Image IMAGE_BADGES_GRAY_ICON_SUPERSTAR;

		public static Image IMAGE_LR_LOADING_01;

		public static Image IMAGE_LR_LOADING_02;

		public static Image IMAGE_LR_LOADING_03;

		public static Image IMAGE_LR_LOADING_04;

		public static Image IMAGE_LR_LOADING_05;

		public static Image IMAGE_LR_LOADING_06;

		public static Image IMAGE_LR_LOADING_07;

		public static Image IMAGE_LR_LOADING_08;

		public static Image IMAGE_LR_LOADING_09;

		public static Image IMAGE_LR_LOADING_10;

		public static Image IMAGE_LR_LOADING_11;

		public static Image IMAGE_LR_LOADING_12;

		public static Image ATLASIMAGE_ATLAS_NOMATCH_480_00;

		public static Image IMAGE_ANGRYBOMB;

		public static Image IMAGE_ANIMS_100CREST_100CREST;

		public static Image IMAGE_ANIMS_BOARDSHATTER_BOTTOM;

		public static Image IMAGE_ANIMS_BOARDSHATTER_GRID;

		public static Image IMAGE_ANIMS_BOARDSHATTER_TOP;

		public static Image IMAGE_BOOM_BOARD;

		public static Image IMAGE_BOOM_CRATER;

		public static Image IMAGE_BOOM_FBOTTOM_WIDGET;

		public static Image IMAGE_BOOM_FGRIDBAR_BOT;

		public static Image IMAGE_BOOM_FGRIDBAR_TOP;

		public static Image IMAGE_BOOM_FTOP_WIDGET;

		public static Image IMAGE_BROWSER_BACKBTN;

		public static Image IMAGE_CHECKPOINT_MARKER;

		public static Image IMAGE_CLOCK_BKG;

		public static Image IMAGE_CLOCK_FACE;

		public static Image IMAGE_CLOCK_FILL;

		public static Image IMAGE_CLOCK_GEAR1;

		public static Image IMAGE_CLOCK_GEAR2;

		public static Image IMAGE_CLOCK_GEAR3;

		public static Image IMAGE_CLOCK_GEAR4;

		public static Image IMAGE_CLOCK_GEAR5;

		public static Image IMAGE_CLOCK_GEAR6;

		public static Image IMAGE_CLOCK_GEAR7;

		public static Image IMAGE_CLOCK_GEAR8;

		public static Image IMAGE_CLOCK_GEAR9;

		public static Image IMAGE_CLOCK_GLARE;

		public static Image IMAGE_CLOCK_SPOKE;

		public static Image IMAGE_DETONATOR;

		public static Image IMAGE_DETONATOR_MOUSEOVER;

		public static Image IMAGE_DOOMGEM;

		public static Image IMAGE_GAMEOVER_BAR__PINK;

		public static Image IMAGE_GAMEOVER_BAR_ORANGE;

		public static Image IMAGE_GAMEOVER_BAR_YELLOW;

		public static Image IMAGE_GAMEOVER_BOX_ORANGE;

		public static Image IMAGE_GAMEOVER_BOX_PINK;

		public static Image IMAGE_GAMEOVER_BOX_YELLOW;

		public static Image IMAGE_GAMEOVER_DARKER_BOX;

		public static Image IMAGE_GAMEOVER_DARKEST_BOX;

		public static Image IMAGE_GAMEOVER_DIALOG;

		public static Image IMAGE_GAMEOVER_DIG_BAR_GEMS;

		public static Image IMAGE_GAMEOVER_DIG_BAR_GOLD;

		public static Image IMAGE_GAMEOVER_DIG_BAR_TREASURE;

		public static Image IMAGE_GAMEOVER_DIG_BOX;

		public static Image IMAGE_GAMEOVER_HORIZONTAL_BAR;

		public static Image IMAGE_GAMEOVER_HORIZONTAL_BAR_OVERLAY;

		public static Image IMAGE_GAMEOVER_ICON_FLAME;

		public static Image IMAGE_GAMEOVER_ICON_FLAME_LRG;

		public static Image IMAGE_GAMEOVER_ICON_HYPERCUBE;

		public static Image IMAGE_GAMEOVER_ICON_HYPERCUBE_LRG;

		public static Image IMAGE_GAMEOVER_ICON_LIGHTNING;

		public static Image IMAGE_GAMEOVER_ICON_STAR;

		public static Image IMAGE_GAMEOVER_ICON_STAR_LRG;

		public static Image IMAGE_GAMEOVER_LIGHT_BOX;

		public static Image IMAGE_GAMEOVER_LINE_SINGLE;

		public static Image IMAGE_GAMEOVER_LINES;

		public static Image IMAGE_GAMEOVER_SECTION_GRAPH;

		public static Image IMAGE_GAMEOVER_SECTION_LABEL;

		public static Image IMAGE_GAMEOVER_SECTION_SMALL;

		public static Image IMAGE_GAMEOVER_STAMP;

		public static Image IMAGE_GREENQUESTION;

		public static Image IMAGE_GRIDPAINT_BLANK;

		public static Image IMAGE_GRIDPAINT_FILLED;

		public static Image IMAGE_MENU_ARROW;

		public static Image IMAGE_QUESTOBJ_FINAL_GLOW_TRANS;

		public static Image IMAGE_QUESTOBJ_FINAL_GLOW_TRANS2;

		public static Image IMAGE_QUESTOBJ_GLOW;

		public static Image IMAGE_QUESTOBJ_GLOW_FINAL;

		public static Image IMAGE_QUESTOBJ_GLOW_FX;

		public static Image IMAGE_QUESTOBJ_GLOW2;

		public static Image IMAGE_RANKUP;

		public static Image IMAGE_SOLID_BLACK;

		public static Image IMAGE_SPARKLE_FAT;

		public static Image IMAGE_SPARKLET_BIG;

		public static Image IMAGE_SPARKLET_FAT;

		public static Image IMAGE_TRANSPARENT_HOLE;

		public static Image IMAGE_VERTICAL_STREAK;

		public static Image ATLASIMAGE_ATLAS_NOMATCH_960_00;

		public static PIEffect PIEFFECT_ANIMS_COLUMN1_FBOMB_SMALL;

		public static PIEffect PIEFFECT_ANIMS_COLUMN1_SHATTERLEFT_SMALL;

		public static PIEffect PIEFFECT_ANIMS_COLUMN1_SHATTERRIGHT_SMALL;

		public static PIEffect PIEFFECT_ANIMS_COLUMN1_SNOWCRUSH;

		public static PIEffect PIEFFECT_ANIMS_COLUMN2_FBOMB;

		public static PIEffect PIEFFECT_ANIMS_COLUMN2_SHATTERLEFT;

		public static PIEffect PIEFFECT_ANIMS_COLUMN2_SHATTERRIGHT;

		public static PIEffect PIEFFECT_ANIMS_COLUMN2_SNOWCRUSH;

		public static PopAnim POPANIM_ANIMS_100CREST;

		public static PopAnim POPANIM_ANIMS_BOARDSHATTER;

		public static Image IMAGE_PP0;

		public static Image IMAGE_PP10;

		public static Image IMAGE_PP11;

		public static Image IMAGE_PP12;

		public static Image IMAGE_PP13;

		public static Image IMAGE_PP14;

		public static Image IMAGE_PP15;

		public static Image IMAGE_PP16;

		public static Image IMAGE_PP17;

		public static Image IMAGE_PP18;

		public static Image IMAGE_PP19;

		public static Image IMAGE_PP1;

		public static Image IMAGE_PP20;

		public static Image IMAGE_PP21;

		public static Image IMAGE_PP22;

		public static Image IMAGE_PP23;

		public static Image IMAGE_PP24;

		public static Image IMAGE_PP25;

		public static Image IMAGE_PP26;

		public static Image IMAGE_PP27;

		public static Image IMAGE_PP28;

		public static Image IMAGE_PP29;

		public static Image IMAGE_PP2;

		public static Image IMAGE_PP3;

		public static Image IMAGE_PP4;

		public static Image IMAGE_PP5;

		public static Image IMAGE_PP6;

		public static Image IMAGE_PP7;

		public static Image IMAGE_PP8;

		public static Image IMAGE_PP9;

		public static Image ATLASIMAGE_ATLAS_QUESTHELP_480_00;

		public static Image IMAGE_CLOCK_ICON;

		public static Image ATLASIMAGE_ATLAS_QUESTHELP_960_00;

		public static Image ATLASIMAGE_ATLAS_RATEGAME_480_00;

		public static Image IMAGE_RATETHEGAME;

		public static Image ATLASIMAGE_ATLAS_RATEGAME_960_00;

		public static GenericResFile RESFILE_AMBIENT_COASTAL;

		public static GenericResFile RESFILE_AMBIENT_CRICKETS;

		public static GenericResFile RESFILE_AMBIENT_FOREST;

		public static GenericResFile RESFILE_AMBIENT_OCEAN_SURF;

		public static GenericResFile RESFILE_AMBIENT_RAIN_LEAVES;

		public static GenericResFile RESFILE_AMBIENT_WATERFALL;

		public static GenericResFile RESFILE_AFFIRMATIONS_GENERAL;

		public static GenericResFile RESFILE_AFFIRMATIONS_POSITIVE_THINKING;

		public static GenericResFile RESFILE_AFFIRMATIONS_PROSPERITY;

		public static GenericResFile RESFILE_AFFIRMATIONS_QUIT_BAD_HABITS;

		public static GenericResFile RESFILE_AFFIRMATIONS_SELF_CONFIDENCE;

		public static GenericResFile RESFILE_AFFIRMATIONS_WEIGHT_LOSS;

		public static Image ATLASIMAGE_ATLAS_ZENOPTIONS_480_00;

		public static Image IMAGE_ZEN_OPTIONS_AMBIENT_NONE;

		public static Image IMAGE_ZEN_OPTIONS_COASTAL;

		public static Image IMAGE_ZEN_OPTIONS_CRICKETS;

		public static Image IMAGE_ZEN_OPTIONS_FOREST;

		public static Image IMAGE_ZEN_OPTIONS_GENERAL;

		public static Image IMAGE_ZEN_OPTIONS_MANTRA_NONE;

		public static Image IMAGE_ZEN_OPTIONS_OCEAN_SURF;

		public static Image IMAGE_ZEN_OPTIONS_POSITIVE_THINKING;

		public static Image IMAGE_ZEN_OPTIONS_PROSPERITY;

		public static Image IMAGE_ZEN_OPTIONS_QUIT_BAD_HABITS;

		public static Image IMAGE_ZEN_OPTIONS_RAIN;

		public static Image IMAGE_ZEN_OPTIONS_RANDOM;

		public static Image IMAGE_ZEN_OPTIONS_SELF_CONFIDENCE;

		public static Image IMAGE_ZEN_OPTIONS_WATERFALL;

		public static Image IMAGE_ZEN_OPTIONS_WEIGHT_LOSS;

		public static Image ATLASIMAGE_ATLAS_ZENOPTIONS_960_00;

		public static Image ATLASIMAGE_EX_ARROW;

		public static Image ATLASIMAGE_EX_HELP_LIGHTNING_01;

		public static Image ATLASIMAGE_EX_HELP_LIGHTNING_02;

		public static Image ATLASIMAGE_EX_HELP_LIGHTNING_03;

		internal static ResGlobalPtr[] gResources = new ResGlobalPtr[1810];

		internal static uint[] gResourceLocales = new uint[] { 1145390149U, 1162761555U, 1163085139U, 1179797074U, 1230260564U, 0U };
	}
}
