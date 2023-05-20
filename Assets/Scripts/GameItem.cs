using System;
using UnityEngine;

public class GameItem : MonoBehaviour
{
	public int id;

	public string gameName;

	public string gameBundle;

	public string platform;

	public bool showCrossPromo;

	public int crossPromoGameID;

	public string crossPromoGameName;

	public string crossPromoGameBundle;

	public string crossPromoGameIOSID;

	public string crossPromoImageName;

	public string crossPromoGameOwnership;

	public string crossPromoGamePlatform;

	public string orientation;

	public string crossPromoAdType;

	public int isConsistent;

	public int updatedToLatestCrossPromo;

	public Sprite icon;

	public Sprite promoBanner;

	public string promoBannerImageName;

	public int gameRank;

	public int isTopGame;

	public GameItem(int i, string gn, string gb, string p, string ntu, string scp, int cpgid, string cpgn, string cpgb, string cpgiosid, string cpgin, string cpgown, string cpgp, string ori, string cpat, int p_isConsistent, int p_gameRank, int p_isTopGame)
	{
		this.id = i;
		this.gameName = gn;
		this.gameBundle = gb;
		this.platform = p;
		if (scp == "1")
		{
			this.showCrossPromo = true;
		}
		else
		{
			this.showCrossPromo = false;
		}
		this.crossPromoGameID = cpgid;
		this.crossPromoGameName = cpgn;
		this.crossPromoGameBundle = cpgb;
		this.crossPromoGameIOSID = cpgiosid;
		this.crossPromoImageName = cpgin;
		this.crossPromoGameOwnership = cpgown;
		this.crossPromoGamePlatform = cpgp;
		this.orientation = ori;
		this.crossPromoAdType = cpat;
		this.isConsistent = p_isConsistent;
		this.gameRank = p_gameRank;
		this.isTopGame = p_isTopGame;
	}
}
