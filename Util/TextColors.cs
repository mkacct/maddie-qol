using Microsoft.Xna.Framework;

namespace MaddieQoL.Util;

public static class TextColors {

	public static readonly Color PlayerChat = Color.White;
	public static readonly Color Event = new(50, 255, 130);
	public static readonly Color Boss = new(175, 75, 255);
	public static readonly Color PlayerDeath = new(225, 25, 25);
	public static readonly Color NPCDeath = new(255, 25, 25);
	public static readonly Color NPCArrival = new(50, 125, 255);
	public static readonly Color Status = new(255, 240, 20);
	public static readonly Color NPCParty = new(255, 0, 160);

	public static readonly Color PlayerEmote = new(204, 102, 0);
	public static readonly Color PartyRed = new(204, 51, 51);
	public static readonly Color PartyGreen = new(59, 218, 85);
	public static readonly Color PartyBlue = new(59, 149, 218);
	public static readonly Color PartyYellow = new(242, 221, 100);
	public static readonly Color PartyPink = new(224, 100, 242);
	public static Color CommandOutput => Status;
	public static Color DeathCount => NPCDeath;

	public static readonly Color Disabled = new(100, 100, 100);

}
