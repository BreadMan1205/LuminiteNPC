using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace LuminiteNPC
{
	public class LuminiteNPC : Mod
	{
		public static LuminiteNPC Instance;

		public override void Load()
		{
			Instance = this;
		}

		public override void Unload()
		{
			Instance = null;
		}

		public LuminiteNPC()
		{
		}
	}
}