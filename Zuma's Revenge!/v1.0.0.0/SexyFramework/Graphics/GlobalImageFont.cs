﻿using System;
using System.Collections.Generic;
using SexyFramework.Misc;

namespace SexyFramework.Graphics
{
	public static class GlobalImageFont
	{
		public static List<uint[]> GetFontPalettes()
		{
			if (GlobalImageFont.FONT_PALETTES == null)
			{
				GlobalImageFont.FONT_PALETTES = new List<uint[]>();
				GlobalImageFont.FONT_PALETTES.Add(new uint[]
				{
					16777215U, 16777215U, 16777215U, 16777215U, 33554431U, 33554431U, 33554431U, 33554431U, 33554431U, 33554431U,
					33554431U, 50331647U, 50331647U, 50331647U, 50331647U, 50331647U, 50331647U, 50331647U, 67108863U, 67108863U,
					67108863U, 67108863U, 67108863U, 67108863U, 67108863U, 83886079U, 83886079U, 83886079U, 83886079U, 83886079U,
					83886079U, 83886079U, 100663295U, 100663295U, 100663295U, 100663295U, 100663295U, 100663295U, 100663295U, 117440511U,
					117440511U, 117440511U, 117440511U, 117440511U, 117440511U, 117440511U, 134217727U, 134217727U, 134217727U, 134217727U,
					134217727U, 134217727U, 150994943U, 150994943U, 150994943U, 150994943U, 150994943U, 150994943U, 150994943U, 167772159U,
					167772159U, 167772159U, 167772159U, 167772159U, 167772159U, 167772159U, 184549375U, 184549375U, 184549375U, 184549375U,
					184549375U, 184549375U, 184549375U, 201326591U, 201326591U, 201326591U, 201326591U, 201326591U, 201326591U, 201326591U,
					218103807U, 218103807U, 218103807U, 218103807U, 218103807U, 218103807U, 218103807U, 234881023U, 234881023U, 234881023U,
					234881023U, 234881023U, 234881023U, 234881023U, 251658239U, 251658239U, 251658239U, 251658239U, 251658239U, 251658239U,
					251658239U, 268435455U, 268435455U, 268435455U, 268435455U, 268435455U, 285212671U, 285212671U, 285212671U, 301989887U,
					301989887U, 301989887U, 318767103U, 318767103U, 335544319U, 335544319U, 335544319U, 352321535U, 352321535U, 352321535U,
					369098751U, 369098751U, 369098751U, 385875967U, 385875967U, 402653183U, 402653183U, 419430399U, 419430399U, 436207615U,
					436207615U, 452984831U, 452984831U, 469762047U, 469762047U, 486539263U, 486539263U, 503316479U, 503316479U, 520093695U,
					536870911U, 536870911U, 553648127U, 553648127U, 570425343U, 570425343U, 587202559U, 587202559U, 603979775U, 603979775U,
					620756991U, 620756991U, 637534207U, 637534207U, 654311423U, 671088639U, 687865855U, 687865855U, 704643071U, 721420287U,
					738197503U, 738197503U, 754974719U, 771751935U, 788529151U, 788529151U, 805306367U, 822083583U, 838860799U, 855638015U,
					855638015U, 872415231U, 889192447U, 905969663U, 922746879U, 939524095U, 956301311U, 973078527U, 973078527U, 989855743U,
					1006632959U, 1023410175U, 1040187391U, 1056964607U, 1073741823U, 1073741823U, 1090519039U, 1107296255U, 1124073471U, 1140850687U,
					1157627903U, 1174405119U, 1191182335U, 1224736767U, 1241513983U, 1258291199U, 1275068415U, 1291845631U, 1308622847U, 1325400063U,
					1342177279U, 1358954495U, 1375731711U, 1409286143U, 1426063359U, 1442840575U, 1459617791U, 1476395007U, 1493172223U, 1509949439U,
					1543503871U, 1560281087U, 1593835519U, 1610612735U, 1644167167U, 1660944383U, 1694498815U, 1711276031U, 1744830463U, 1778384895U,
					1795162111U, 1828716543U, 1862270975U, 1895825407U, 1912602623U, 1946157055U, 1979711487U, 2013265919U, 2046820351U, 2080374783U,
					2113929215U, 2147483647U, 2181038079U, 2214592511U, 2264924159U, 2332033023U, 2382364671U, 2432696319U, 2499805183U, 2550136831U,
					2600468479U, 2650800127U, 2701131775U, 2751463423U, 2801795071U, 2852126719U, 2902458367U, 2952790015U, 3003121663U, 3187671039U,
					3372220415U, 3556769791U, 3741319167U, 3925868543U, 4110417919U, uint.MaxValue
				});
				GlobalImageFont.FONT_PALETTES.Add(new uint[]
				{
					16777215U, 16777215U, 16777215U, 16777215U, 33554431U, 33554431U, 33554431U, 33554431U, 50331647U, 50331647U,
					50331647U, 67108863U, 67108863U, 67108863U, 67108863U, 67108863U, 83886079U, 83886079U, 100663295U, 100663295U,
					100663295U, 100663295U, 100663295U, 100663295U, 117440511U, 134217727U, 134217727U, 134217727U, 134217727U, 134217727U,
					134217727U, 134217727U, 150994943U, 167772159U, 167772159U, 167772159U, 167772159U, 167772159U, 167772159U, 184549375U,
					184549375U, 201326591U, 201326591U, 201326591U, 201326591U, 201326591U, 218103807U, 218103807U, 218103807U, 234881023U,
					234881023U, 234881023U, 251658239U, 251658239U, 251658239U, 251658239U, 251658239U, 268435455U, 268435455U, 285212671U,
					285212671U, 285212671U, 285212671U, 285212671U, 285212671U, 301989887U, 318767103U, 318767103U, 318767103U, 318767103U,
					318767103U, 318767103U, 318767103U, 335544319U, 352321535U, 352321535U, 352321535U, 352321535U, 352321535U, 352321535U,
					369098751U, 369098751U, 385875967U, 385875967U, 385875967U, 385875967U, 385875967U, 402653183U, 402653183U, 402653183U,
					419430399U, 419430399U, 419430399U, 419430399U, 436207615U, 436207615U, 436207615U, 436207615U, 452984831U, 452984831U,
					452984831U, 469762047U, 469762047U, 469762047U, 469762047U, 469762047U, 486539263U, 503316479U, 503316479U, 520093695U,
					520093695U, 520093695U, 536870911U, 536870911U, 553648127U, 553648127U, 553648127U, 570425343U, 587202559U, 587202559U,
					603979775U, 603979775U, 603979775U, 620756991U, 620756991U, 637534207U, 637534207U, 654311423U, 654311423U, 671088639U,
					687865855U, 704643071U, 704643071U, 721420287U, 721420287U, 738197503U, 738197503U, 754974719U, 754974719U, 771751935U,
					788529151U, 788529151U, 805306367U, 805306367U, 822083583U, 838860799U, 855638015U, 855638015U, 872415231U, 872415231U,
					889192447U, 889192447U, 905969663U, 905969663U, 922746879U, 939524095U, 956301311U, 956301311U, 973078527U, 989855743U,
					1006632959U, 1006632959U, 1023410175U, 1040187391U, 1056964607U, 1073741823U, 1090519039U, 1107296255U, 1124073471U, 1140850687U,
					1140850687U, 1157627903U, 1174405119U, 1191182335U, 1207959551U, 1224736767U, 1241513983U, 1258291199U, 1258291199U, 1275068415U,
					1291845631U, 1308622847U, 1325400063U, 1342177279U, 1358954495U, 1358954495U, 1375731711U, 1392508927U, 1409286143U, 1426063359U,
					1442840575U, 1459617791U, 1476395007U, 1509949439U, 1526726655U, 1543503871U, 1560281087U, 1577058303U, 1593835519U, 1610612735U,
					1627389951U, 1644167167U, 1660944383U, 1694498815U, 1711276031U, 1728053247U, 1744830463U, 1761607679U, 1778384895U, 1795162111U,
					1828716543U, 1845493759U, 1862270975U, 1879048191U, 1912602623U, 1929379839U, 1962934271U, 1979711487U, 2013265919U, 2046820351U,
					2063597567U, 2097151999U, 2130706431U, 2147483647U, 2164260863U, 2197815295U, 2231369727U, 2264924159U, 2298478591U, 2332033023U,
					2365587455U, 2382364671U, 2415919103U, 2449473535U, 2499805183U, 2550136831U, 2600468479U, 2650800127U, 2701131775U, 2751463423U,
					2801795071U, 2852126719U, 2885681151U, 2936012799U, 2986344447U, 3019898879U, 3070230527U, 3120562175U, 3170893823U, 3321888767U,
					3489660927U, 3640655871U, 3808428031U, 3959422975U, 4127195135U, uint.MaxValue
				});
				GlobalImageFont.FONT_PALETTES.Add(new uint[]
				{
					16777215U, 16777215U, 16777215U, 16777215U, 33554431U, 50331647U, 50331647U, 50331647U, 67108863U, 67108863U,
					67108863U, 83886079U, 83886079U, 100663295U, 100663295U, 100663295U, 117440511U, 117440511U, 134217727U, 134217727U,
					134217727U, 150994943U, 150994943U, 150994943U, 167772159U, 184549375U, 184549375U, 184549375U, 184549375U, 201326591U,
					201326591U, 201326591U, 218103807U, 234881023U, 234881023U, 234881023U, 234881023U, 251658239U, 251658239U, 268435455U,
					268435455U, 285212671U, 285212671U, 285212671U, 285212671U, 301989887U, 318767103U, 318767103U, 318767103U, 335544319U,
					335544319U, 335544319U, 352321535U, 352321535U, 369098751U, 369098751U, 369098751U, 385875967U, 385875967U, 402653183U,
					402653183U, 402653183U, 419430399U, 419430399U, 419430399U, 436207615U, 452984831U, 452984831U, 452984831U, 452984831U,
					469762047U, 469762047U, 469762047U, 486539263U, 503316479U, 503316479U, 503316479U, 503316479U, 520093695U, 520093695U,
					536870911U, 536870911U, 553648127U, 553648127U, 553648127U, 553648127U, 570425343U, 587202559U, 587202559U, 587202559U,
					603979775U, 603979775U, 603979775U, 603979775U, 620756991U, 637534207U, 637534207U, 637534207U, 654311423U, 654311423U,
					654311423U, 671088639U, 671088639U, 687865855U, 687865855U, 687865855U, 704643071U, 721420287U, 721420287U, 738197503U,
					738197503U, 738197503U, 754974719U, 771751935U, 788529151U, 788529151U, 788529151U, 805306367U, 822083583U, 822083583U,
					838860799U, 838860799U, 838860799U, 855638015U, 872415231U, 889192447U, 889192447U, 905969663U, 905969663U, 922746879U,
					939524095U, 956301311U, 956301311U, 973078527U, 973078527U, 989855743U, 989855743U, 1006632959U, 1023410175U, 1040187391U,
					1056964607U, 1056964607U, 1073741823U, 1073741823U, 1090519039U, 1107296255U, 1124073471U, 1124073471U, 1140850687U, 1140850687U,
					1157627903U, 1157627903U, 1174405119U, 1191182335U, 1207959551U, 1224736767U, 1241513983U, 1241513983U, 1258291199U, 1275068415U,
					1291845631U, 1291845631U, 1308622847U, 1325400063U, 1342177279U, 1358954495U, 1375731711U, 1392508927U, 1409286143U, 1426063359U,
					1426063359U, 1442840575U, 1459617791U, 1476395007U, 1493172223U, 1509949439U, 1526726655U, 1543503871U, 1543503871U, 1560281087U,
					1577058303U, 1593835519U, 1610612735U, 1627389951U, 1644167167U, 1644167167U, 1660944383U, 1677721599U, 1694498815U, 1711276031U,
					1728053247U, 1744830463U, 1761607679U, 1795162111U, 1811939327U, 1828716543U, 1845493759U, 1862270975U, 1879048191U, 1895825407U,
					1912602623U, 1929379839U, 1946157055U, 1979711487U, 1996488703U, 2013265919U, 2030043135U, 2046820351U, 2063597567U, 2080374783U,
					2113929215U, 2130706431U, 2147483647U, 2164260863U, 2197815295U, 2214592511U, 2248146943U, 2264924159U, 2281701375U, 2315255807U,
					2332033023U, 2365587455U, 2399141887U, 2415919103U, 2432696319U, 2466250751U, 2499805183U, 2516582399U, 2550136831U, 2583691263U,
					2617245695U, 2634022911U, 2667577343U, 2701131775U, 2734686207U, 2785017855U, 2835349503U, 2868903935U, 2919235583U, 2969567231U,
					3003121663U, 3053453311U, 3087007743U, 3120562175U, 3170893823U, 3204448255U, 3254779903U, 3288334335U, 3338665983U, 3472883711U,
					3607101439U, 3741319167U, 3875536895U, 4009754623U, 4143972351U, uint.MaxValue
				});
				GlobalImageFont.FONT_PALETTES.Add(new uint[]
				{
					16777215U, 16777215U, 16777215U, 33554431U, 50331647U, 50331647U, 67108863U, 67108863U, 83886079U, 83886079U,
					83886079U, 100663295U, 117440511U, 117440511U, 134217727U, 134217727U, 150994943U, 150994943U, 167772159U, 167772159U,
					184549375U, 184549375U, 201326591U, 201326591U, 218103807U, 234881023U, 234881023U, 234881023U, 251658239U, 251658239U,
					268435455U, 268435455U, 285212671U, 301989887U, 301989887U, 301989887U, 318767103U, 318767103U, 335544319U, 352321535U,
					352321535U, 369098751U, 369098751U, 369098751U, 385875967U, 385875967U, 402653183U, 419430399U, 419430399U, 436207615U,
					436207615U, 436207615U, 452984831U, 469762047U, 469762047U, 486539263U, 486539263U, 503316479U, 503316479U, 520093695U,
					520093695U, 536870911U, 536870911U, 553648127U, 553648127U, 570425343U, 587202559U, 587202559U, 587202559U, 603979775U,
					603979775U, 620756991U, 620756991U, 637534207U, 654311423U, 654311423U, 654311423U, 671088639U, 671088639U, 687865855U,
					704643071U, 704643071U, 721420287U, 721420287U, 721420287U, 738197503U, 738197503U, 754974719U, 771751935U, 771751935U,
					788529151U, 788529151U, 788529151U, 805306367U, 822083583U, 822083583U, 838860799U, 838860799U, 855638015U, 855638015U,
					855638015U, 872415231U, 889192447U, 889192447U, 905969663U, 905969663U, 922746879U, 939524095U, 939524095U, 956301311U,
					956301311U, 973078527U, 989855743U, 989855743U, 1006632959U, 1023410175U, 1023410175U, 1040187391U, 1056964607U, 1056964607U,
					1073741823U, 1073741823U, 1090519039U, 1107296255U, 1107296255U, 1124073471U, 1140850687U, 1157627903U, 1157627903U, 1174405119U,
					1191182335U, 1207959551U, 1207959551U, 1224736767U, 1224736767U, 1241513983U, 1258291199U, 1275068415U, 1275068415U, 1291845631U,
					1308622847U, 1325400063U, 1342177279U, 1342177279U, 1358954495U, 1375731711U, 1392508927U, 1392508927U, 1409286143U, 1409286143U,
					1426063359U, 1442840575U, 1459617791U, 1459617791U, 1476395007U, 1493172223U, 1509949439U, 1526726655U, 1543503871U, 1560281087U,
					1577058303U, 1577058303U, 1593835519U, 1610612735U, 1627389951U, 1644167167U, 1660944383U, 1677721599U, 1694498815U, 1711276031U,
					1711276031U, 1728053247U, 1744830463U, 1761607679U, 1778384895U, 1795162111U, 1811939327U, 1828716543U, 1828716543U, 1845493759U,
					1862270975U, 1879048191U, 1895825407U, 1912602623U, 1929379839U, 1946157055U, 1962934271U, 1979711487U, 1996488703U, 2013265919U,
					2030043135U, 2046820351U, 2063597567U, 2080374783U, 2097151999U, 2113929215U, 2130706431U, 2147483647U, 2164260863U, 2181038079U,
					2197815295U, 2214592511U, 2231369727U, 2264924159U, 2281701375U, 2298478591U, 2315255807U, 2332033023U, 2348810239U, 2365587455U,
					2399141887U, 2415919103U, 2432696319U, 2449473535U, 2483027967U, 2499805183U, 2516582399U, 2533359615U, 2566914047U, 2583691263U,
					2600468479U, 2634022911U, 2667577343U, 2684354559U, 2701131775U, 2734686207U, 2751463423U, 2785017855U, 2801795071U, 2835349503U,
					2868903935U, 2885681151U, 2919235583U, 2936012799U, 2969567231U, 3019898879U, 3053453311U, 3087007743U, 3137339391U, 3170893823U,
					3204448255U, 3254779903U, 3288334335U, 3321888767U, 3355443199U, 3388997631U, 3422552063U, 3456106495U, 3506438143U, 3607101439U,
					3724541951U, 3841982463U, 3942645759U, 4060086271U, 4177526783U, uint.MaxValue
				});
				GlobalImageFont.FONT_PALETTES.Add(new uint[]
				{
					16777215U, 16777215U, 33554431U, 33554431U, 50331647U, 67108863U, 67108863U, 83886079U, 100663295U, 100663295U,
					117440511U, 134217727U, 134217727U, 150994943U, 150994943U, 167772159U, 184549375U, 184549375U, 201326591U, 218103807U,
					218103807U, 234881023U, 234881023U, 251658239U, 268435455U, 285212671U, 285212671U, 301989887U, 301989887U, 318767103U,
					318767103U, 335544319U, 352321535U, 369098751U, 369098751U, 385875967U, 385875967U, 402653183U, 402653183U, 419430399U,
					436207615U, 452984831U, 452984831U, 469762047U, 469762047U, 486539263U, 503316479U, 503316479U, 520093695U, 536870911U,
					536870911U, 553648127U, 570425343U, 570425343U, 587202559U, 587202559U, 603979775U, 620756991U, 620756991U, 637534207U,
					654311423U, 654311423U, 671088639U, 671088639U, 687865855U, 704643071U, 721420287U, 721420287U, 738197503U, 738197503U,
					754974719U, 754974719U, 771751935U, 788529151U, 805306367U, 805306367U, 822083583U, 822083583U, 838860799U, 838860799U,
					855638015U, 872415231U, 889192447U, 889192447U, 905969663U, 905969663U, 922746879U, 939524095U, 939524095U, 956301311U,
					973078527U, 973078527U, 989855743U, 989855743U, 1006632959U, 1023410175U, 1023410175U, 1040187391U, 1056964607U, 1056964607U,
					1073741823U, 1090519039U, 1090519039U, 1107296255U, 1107296255U, 1124073471U, 1140850687U, 1157627903U, 1157627903U, 1174405119U,
					1191182335U, 1191182335U, 1207959551U, 1224736767U, 1241513983U, 1241513983U, 1258291199U, 1275068415U, 1291845631U, 1291845631U,
					1308622847U, 1325400063U, 1325400063U, 1342177279U, 1358954495U, 1375731711U, 1375731711U, 1392508927U, 1409286143U, 1426063359U,
					1442840575U, 1459617791U, 1459617791U, 1476395007U, 1493172223U, 1509949439U, 1509949439U, 1526726655U, 1543503871U, 1560281087U,
					1577058303U, 1577058303U, 1593835519U, 1610612735U, 1627389951U, 1644167167U, 1660944383U, 1660944383U, 1677721599U, 1694498815U,
					1711276031U, 1711276031U, 1728053247U, 1744830463U, 1761607679U, 1778384895U, 1795162111U, 1795162111U, 1811939327U, 1828716543U,
					1845493759U, 1862270975U, 1879048191U, 1895825407U, 1912602623U, 1929379839U, 1946157055U, 1962934271U, 1979711487U, 1996488703U,
					1996488703U, 2013265919U, 2030043135U, 2046820351U, 2063597567U, 2080374783U, 2097151999U, 2113929215U, 2130706431U, 2147483647U,
					2164260863U, 2181038079U, 2197815295U, 2214592511U, 2231369727U, 2231369727U, 2248146943U, 2264924159U, 2281701375U, 2298478591U,
					2315255807U, 2332033023U, 2348810239U, 2382364671U, 2399141887U, 2415919103U, 2432696319U, 2449473535U, 2466250751U, 2483027967U,
					2499805183U, 2516582399U, 2533359615U, 2550136831U, 2566914047U, 2583691263U, 2600468479U, 2617245695U, 2634022911U, 2650800127U,
					2684354559U, 2701131775U, 2717908991U, 2734686207U, 2751463423U, 2768240639U, 2801795071U, 2818572287U, 2835349503U, 2868903935U,
					2885681151U, 2902458367U, 2936012799U, 2952790015U, 2969567231U, 2986344447U, 3019898879U, 3036676095U, 3070230527U, 3087007743U,
					3120562175U, 3137339391U, 3154116607U, 3187671039U, 3221225471U, 3254779903U, 3288334335U, 3321888767U, 3355443199U, 3388997631U,
					3422552063U, 3456106495U, 3472883711U, 3506438143U, 3539992575U, 3573547007U, 3607101439U, 3640655871U, 3674210303U, 3758096383U,
					3841982463U, 3925868543U, 4026531839U, 4110417919U, 4194303999U, uint.MaxValue
				});
				GlobalImageFont.FONT_PALETTES.Add(new uint[]
				{
					16777215U, 16777215U, 33554431U, 50331647U, 67108863U, 67108863U, 83886079U, 100663295U, 117440511U, 117440511U,
					134217727U, 150994943U, 167772159U, 167772159U, 184549375U, 201326591U, 218103807U, 218103807U, 234881023U, 251658239U,
					268435455U, 268435455U, 285212671U, 301989887U, 318767103U, 335544319U, 335544319U, 352321535U, 369098751U, 369098751U,
					385875967U, 402653183U, 419430399U, 436207615U, 436207615U, 452984831U, 469762047U, 469762047U, 486539263U, 503316479U,
					520093695U, 536870911U, 536870911U, 553648127U, 570425343U, 570425343U, 587202559U, 603979775U, 620756991U, 637534207U,
					637534207U, 654311423U, 671088639U, 687865855U, 687865855U, 704643071U, 721420287U, 738197503U, 738197503U, 754974719U,
					771751935U, 788529151U, 788529151U, 805306367U, 822083583U, 838860799U, 855638015U, 855638015U, 872415231U, 889192447U,
					889192447U, 905969663U, 922746879U, 939524095U, 956301311U, 956301311U, 973078527U, 989855743U, 989855743U, 1006632959U,
					1023410175U, 1040187391U, 1056964607U, 1056964607U, 1073741823U, 1090519039U, 1090519039U, 1107296255U, 1124073471U, 1140850687U,
					1157627903U, 1157627903U, 1174405119U, 1191182335U, 1207959551U, 1207959551U, 1224736767U, 1241513983U, 1258291199U, 1258291199U,
					1275068415U, 1291845631U, 1308622847U, 1308622847U, 1325400063U, 1342177279U, 1358954495U, 1375731711U, 1375731711U, 1392508927U,
					1409286143U, 1426063359U, 1442840575U, 1442840575U, 1459617791U, 1476395007U, 1493172223U, 1509949439U, 1526726655U, 1526726655U,
					1543503871U, 1560281087U, 1577058303U, 1593835519U, 1593835519U, 1610612735U, 1627389951U, 1644167167U, 1660944383U, 1677721599U,
					1694498815U, 1711276031U, 1711276031U, 1728053247U, 1744830463U, 1761607679U, 1778384895U, 1795162111U, 1795162111U, 1811939327U,
					1828716543U, 1845493759U, 1862270975U, 1879048191U, 1895825407U, 1912602623U, 1929379839U, 1929379839U, 1946157055U, 1962934271U,
					1979711487U, 1996488703U, 2013265919U, 2013265919U, 2030043135U, 2046820351U, 2063597567U, 2080374783U, 2097151999U, 2113929215U,
					2130706431U, 2147483647U, 2164260863U, 2181038079U, 2197815295U, 2214592511U, 2231369727U, 2248146943U, 2264924159U, 2281701375U,
					2281701375U, 2298478591U, 2315255807U, 2332033023U, 2348810239U, 2365587455U, 2382364671U, 2399141887U, 2415919103U, 2432696319U,
					2449473535U, 2466250751U, 2483027967U, 2499805183U, 2516582399U, 2533359615U, 2550136831U, 2566914047U, 2583691263U, 2600468479U,
					2617245695U, 2634022911U, 2650800127U, 2667577343U, 2684354559U, 2701131775U, 2717908991U, 2734686207U, 2751463423U, 2768240639U,
					2785017855U, 2801795071U, 2818572287U, 2835349503U, 2852126719U, 2868903935U, 2885681151U, 2902458367U, 2919235583U, 2936012799U,
					2969567231U, 2986344447U, 3003121663U, 3019898879U, 3036676095U, 3053453311U, 3070230527U, 3087007743U, 3120562175U, 3137339391U,
					3154116607U, 3170893823U, 3204448255U, 3221225471U, 3238002687U, 3254779903U, 3271557119U, 3305111551U, 3321888767U, 3338665983U,
					3372220415U, 3388997631U, 3405774847U, 3422552063U, 3456106495U, 3489660927U, 3506438143U, 3539992575U, 3573547007U, 3590324223U,
					3623878655U, 3657433087U, 3674210303U, 3707764735U, 3724541951U, 3758096383U, 3774873599U, 3808428031U, 3841982463U, 3892314111U,
					3959422975U, 4026531839U, 4093640703U, 4160749567U, 4227858431U, uint.MaxValue
				});
				GlobalImageFont.FONT_PALETTES.Add(new uint[]
				{
					16777215U, 16777215U, 33554431U, 50331647U, 67108863U, 83886079U, 100663295U, 117440511U, 134217727U, 134217727U,
					150994943U, 167772159U, 184549375U, 201326591U, 218103807U, 234881023U, 251658239U, 251658239U, 268435455U, 285212671U,
					301989887U, 318767103U, 335544319U, 352321535U, 369098751U, 385875967U, 385875967U, 402653183U, 419430399U, 436207615U,
					452984831U, 469762047U, 486539263U, 503316479U, 503316479U, 520093695U, 536870911U, 553648127U, 570425343U, 587202559U,
					603979775U, 620756991U, 620756991U, 637534207U, 654311423U, 671088639U, 687865855U, 704643071U, 721420287U, 738197503U,
					738197503U, 754974719U, 771751935U, 788529151U, 805306367U, 822083583U, 838860799U, 855638015U, 855638015U, 872415231U,
					889192447U, 905969663U, 922746879U, 939524095U, 956301311U, 973078527U, 989855743U, 989855743U, 1006632959U, 1023410175U,
					1040187391U, 1056964607U, 1073741823U, 1090519039U, 1107296255U, 1107296255U, 1124073471U, 1140850687U, 1157627903U, 1174405119U,
					1191182335U, 1207959551U, 1224736767U, 1224736767U, 1241513983U, 1258291199U, 1275068415U, 1291845631U, 1308622847U, 1325400063U,
					1342177279U, 1342177279U, 1358954495U, 1375731711U, 1392508927U, 1409286143U, 1426063359U, 1442840575U, 1459617791U, 1459617791U,
					1476395007U, 1493172223U, 1509949439U, 1526726655U, 1543503871U, 1560281087U, 1577058303U, 1593835519U, 1593835519U, 1610612735U,
					1627389951U, 1644167167U, 1660944383U, 1677721599U, 1694498815U, 1711276031U, 1728053247U, 1744830463U, 1761607679U, 1761607679U,
					1778384895U, 1795162111U, 1811939327U, 1828716543U, 1845493759U, 1862270975U, 1879048191U, 1895825407U, 1912602623U, 1929379839U,
					1946157055U, 1962934271U, 1962934271U, 1979711487U, 1996488703U, 2013265919U, 2030043135U, 2046820351U, 2063597567U, 2080374783U,
					2097151999U, 2113929215U, 2130706431U, 2147483647U, 2164260863U, 2181038079U, 2197815295U, 2197815295U, 2214592511U, 2231369727U,
					2248146943U, 2264924159U, 2281701375U, 2298478591U, 2315255807U, 2332033023U, 2348810239U, 2365587455U, 2382364671U, 2399141887U,
					2415919103U, 2432696319U, 2449473535U, 2466250751U, 2483027967U, 2499805183U, 2516582399U, 2533359615U, 2550136831U, 2566914047U,
					2566914047U, 2583691263U, 2600468479U, 2617245695U, 2634022911U, 2650800127U, 2667577343U, 2684354559U, 2701131775U, 2717908991U,
					2734686207U, 2751463423U, 2768240639U, 2785017855U, 2801795071U, 2818572287U, 2835349503U, 2852126719U, 2868903935U, 2885681151U,
					2902458367U, 2919235583U, 2936012799U, 2952790015U, 2969567231U, 2986344447U, 3003121663U, 3019898879U, 3036676095U, 3053453311U,
					3070230527U, 3087007743U, 3103784959U, 3120562175U, 3137339391U, 3154116607U, 3170893823U, 3187671039U, 3204448255U, 3221225471U,
					3254779903U, 3271557119U, 3288334335U, 3305111551U, 3321888767U, 3338665983U, 3355443199U, 3372220415U, 3388997631U, 3405774847U,
					3422552063U, 3439329279U, 3472883711U, 3489660927U, 3506438143U, 3523215359U, 3539992575U, 3556769791U, 3573547007U, 3590324223U,
					3623878655U, 3640655871U, 3657433087U, 3674210303U, 3690987519U, 3724541951U, 3741319167U, 3758096383U, 3791650815U, 3808428031U,
					3825205247U, 3858759679U, 3875536895U, 3892314111U, 3909091327U, 3942645759U, 3959422975U, 3976200191U, 4009754623U, 4043309055U,
					4076863487U, 4127195135U, 4160749567U, 4211081215U, 4244635647U, uint.MaxValue
				});
				GlobalImageFont.FONT_PALETTES.Add(new uint[]
				{
					16777215U, 33554431U, 50331647U, 67108863U, 83886079U, 100663295U, 117440511U, 134217727U, 150994943U, 167772159U,
					184549375U, 201326591U, 218103807U, 234881023U, 251658239U, 268435455U, 285212671U, 301989887U, 318767103U, 335544319U,
					352321535U, 369098751U, 385875967U, 402653183U, 419430399U, 436207615U, 452984831U, 469762047U, 486539263U, 503316479U,
					520093695U, 536870911U, 553648127U, 570425343U, 587202559U, 603979775U, 620756991U, 637534207U, 654311423U, 671088639U,
					687865855U, 704643071U, 721420287U, 738197503U, 754974719U, 771751935U, 788529151U, 805306367U, 822083583U, 838860799U,
					855638015U, 872415231U, 889192447U, 905969663U, 922746879U, 939524095U, 956301311U, 973078527U, 989855743U, 1006632959U,
					1023410175U, 1040187391U, 1056964607U, 1073741823U, 1090519039U, 1107296255U, 1124073471U, 1140850687U, 1157627903U, 1174405119U,
					1191182335U, 1207959551U, 1224736767U, 1241513983U, 1258291199U, 1275068415U, 1291845631U, 1308622847U, 1325400063U, 1342177279U,
					1358954495U, 1375731711U, 1392508927U, 1409286143U, 1426063359U, 1442840575U, 1459617791U, 1476395007U, 1493172223U, 1509949439U,
					1526726655U, 1543503871U, 1560281087U, 1577058303U, 1593835519U, 1610612735U, 1627389951U, 1644167167U, 1660944383U, 1677721599U,
					1694498815U, 1711276031U, 1728053247U, 1744830463U, 1761607679U, 1778384895U, 1795162111U, 1811939327U, 1828716543U, 1845493759U,
					1862270975U, 1879048191U, 1895825407U, 1912602623U, 1929379839U, 1946157055U, 1962934271U, 1979711487U, 1996488703U, 2013265919U,
					2030043135U, 2046820351U, 2063597567U, 2080374783U, 2097151999U, 2113929215U, 2130706431U, 2147483647U, 2164260863U, 2181038079U,
					2197815295U, 2214592511U, 2231369727U, 2248146943U, 2264924159U, 2281701375U, 2298478591U, 2315255807U, 2332033023U, 2348810239U,
					2365587455U, 2382364671U, 2399141887U, 2415919103U, 2432696319U, 2449473535U, 2466250751U, 2483027967U, 2499805183U, 2516582399U,
					2533359615U, 2550136831U, 2566914047U, 2583691263U, 2600468479U, 2617245695U, 2634022911U, 2650800127U, 2667577343U, 2684354559U,
					2701131775U, 2717908991U, 2734686207U, 2751463423U, 2768240639U, 2785017855U, 2801795071U, 2818572287U, 2835349503U, 2852126719U,
					2868903935U, 2885681151U, 2902458367U, 2919235583U, 2936012799U, 2952790015U, 2969567231U, 2986344447U, 3003121663U, 3019898879U,
					3036676095U, 3053453311U, 3070230527U, 3087007743U, 3103784959U, 3120562175U, 3137339391U, 3154116607U, 3170893823U, 3187671039U,
					3204448255U, 3221225471U, 3238002687U, 3254779903U, 3271557119U, 3288334335U, 3305111551U, 3321888767U, 3338665983U, 3355443199U,
					3372220415U, 3388997631U, 3405774847U, 3422552063U, 3439329279U, 3456106495U, 3472883711U, 3489660927U, 3506438143U, 3523215359U,
					3539992575U, 3556769791U, 3573547007U, 3590324223U, 3607101439U, 3623878655U, 3640655871U, 3657433087U, 3674210303U, 3690987519U,
					3707764735U, 3724541951U, 3741319167U, 3758096383U, 3774873599U, 3791650815U, 3808428031U, 3825205247U, 3841982463U, 3858759679U,
					3875536895U, 3892314111U, 3909091327U, 3925868543U, 3942645759U, 3959422975U, 3976200191U, 3992977407U, 4009754623U, 4026531839U,
					4043309055U, 4060086271U, 4076863487U, 4093640703U, 4110417919U, 4127195135U, 4143972351U, 4160749567U, 4177526783U, 4194303999U,
					4211081215U, 4227858431U, 4244635647U, 4261412863U, 4278190079U, uint.MaxValue
				});
			}
			return GlobalImageFont.FONT_PALETTES;
		}

		public static void AddRenderCommand(RenderCommand cmd, int orderedZ)
		{
			if (GlobalImageFont.gRenderCMDList[orderedZ] == null)
			{
				GlobalImageFont.gRenderCMDList[orderedZ] = new List<RenderCommand>();
			}
			GlobalImageFont.gRenderCMDList[orderedZ].Add(cmd);
		}

		public static RenderCommand AllocRenderCommand()
		{
			return GlobalImageFont.gRenderCMDPool.Alloc();
		}

		public static void DrawAllRenderCommand(Graphics g, bool AlphaCorrectionEnabled)
		{
			for (int i = 0; i < GlobalImageFont.gRenderCMDList.Length; i++)
			{
				List<RenderCommand> list = GlobalImageFont.gRenderCMDList[i];
				if (list != null)
				{
					for (int j = 0; j < list.Count; j++)
					{
						RenderCommand renderCommand = list[j];
						if (renderCommand.mFontLayer != null)
						{
							int drawMode = g.GetDrawMode();
							if (renderCommand.mMode != -1 && renderCommand.mMode != drawMode)
							{
								g.SetDrawMode(renderCommand.mMode);
							}
							g.SetColor(renderCommand.mColor);
							int num = renderCommand.mColor.mRed * 19660 + renderCommand.mColor.mGreen * 38666 + renderCommand.mColor.mBlue * 7208 >> 21;
							if (renderCommand.mFontLayer.mUseAlphaCorrection && renderCommand.mFontLayer.mBaseFontLayer.mUseAlphaCorrection && AlphaCorrectionEnabled && num != 7)
							{
								MemoryImage memoryImage = renderCommand.mFontLayer.mScaledImages[0].GetMemoryImage();
								if (g.Is3D())
								{
									bool flag = false;
									if (flag)
									{
										if (memoryImage == null || memoryImage.mColorTable == null || memoryImage.mColorTable[254] != GlobalImageFont.FONT_PALETTES[0][254])
										{
											memoryImage = renderCommand.mFontLayer.GenerateAlphaCorrectedImage(0).GetMemoryImage();
										}
									}
									else
									{
										MemoryImage memoryImage2 = renderCommand.mFontLayer.mScaledImages[num].GetMemoryImage();
										if (memoryImage2 == null || memoryImage2.mColorTable == null || memoryImage2.mColorTable[254] != GlobalImageFont.FONT_PALETTES[num][254])
										{
											memoryImage2 = renderCommand.mFontLayer.GenerateAlphaCorrectedImage(num).GetMemoryImage();
										}
										g.DrawImage(memoryImage2, renderCommand.mDest[0], renderCommand.mDest[1], new Rect(renderCommand.mSrc[0], renderCommand.mSrc[1], renderCommand.mSrc[2], renderCommand.mSrc[3]));
									}
								}
								else
								{
									if (memoryImage == null || memoryImage.mColorTable == null)
									{
										memoryImage = renderCommand.mFontLayer.GenerateAlphaCorrectedImage(0).GetMemoryImage();
									}
									if (memoryImage.mColorTable[254] != GlobalImageFont.FONT_PALETTES[num][254])
									{
										Array.Copy(memoryImage.mColorTable, GlobalImageFont.FONT_PALETTES[num], 1024);
										if (memoryImage.mNativeAlphaData != null)
										{
											for (int k = 0; k < 256; k++)
											{
												uint num2 = GlobalImageFont.FONT_PALETTES[num][k] >> 24;
												memoryImage.mNativeAlphaData[k] = (num2 << 24) | (num2 << 16) | (num2 << 8) | num2;
											}
										}
									}
									g.DrawImage(memoryImage, renderCommand.mDest[0], renderCommand.mDest[1], new Rect(renderCommand.mSrc[0], renderCommand.mSrc[1], renderCommand.mSrc[2], renderCommand.mSrc[3]));
								}
							}
							else
							{
								g.DrawImage(renderCommand.mFontLayer.mScaledImages[7].GetImage(), renderCommand.mDest[0], renderCommand.mDest[1], new Rect(renderCommand.mSrc[0], renderCommand.mSrc[1], renderCommand.mSrc[2], renderCommand.mSrc[3]));
							}
							g.SetDrawMode(drawMode);
						}
					}
				}
			}
		}

		public static void ClearRenderCommand()
		{
			for (int i = 0; i < GlobalImageFont.gRenderCMDList.Length; i++)
			{
				List<RenderCommand> list = GlobalImageFont.gRenderCMDList[i];
				if (list != null)
				{
					list.Clear();
				}
			}
		}

		public static RenderCommand[] GetRenderCommandPool()
		{
			if (GlobalImageFont.gRenderCommandPool == null)
			{
				GlobalImageFont.gRenderCommandPool = new RenderCommand[1024];
				for (int i = 0; i < 1024; i++)
				{
					GlobalImageFont.gRenderCommandPool[i] = new RenderCommand();
				}
			}
			return GlobalImageFont.gRenderCommandPool;
		}

		internal const int POOL_SIZE = 1024;

		public static List<uint[]> FONT_PALETTES = null;

		internal static RenderCommand[] gRenderCommandPool = null;

		public static List<RenderCommand>[] gRenderCMDList = new List<RenderCommand>[256];

		private static ObjectPool<RenderCommand> gRenderCMDPool = new ObjectPool<RenderCommand>(500);
	}
}
