using System;

public class Param
{
	public CrossPromoGame game;

	public int index;

	public CrossPromoAdType crossPromoAdType;

	public bool isConsistent;

	public Param(CrossPromoGame game, int index, CrossPromoAdType p_cpat, bool p_isConsistent)
	{
		this.game = game;
		this.index = index;
		this.crossPromoAdType = p_cpat;
		this.isConsistent = p_isConsistent;
	}
}
