using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Linq;
using Terraria.GameContent.Events;
using Terraria.World.Generation;

namespace LuminiteNPC.NPCs
{
	[AutoloadHead]
	public class ShadowCreature : ModNPC
	{
		public override string Texture => "LuminiteNPC/NPCs/LuminiteNPC";
		
		public override string[] AltTextures => new[] { "LuminiteNPC/NPCs/LuminiteNPC_Alt_1" };

		public override bool Autoload(ref string name) 
		{
			name = "Luminite Merchant";
			return mod.Properties.Autoload;
		}

		public override void SetStaticDefaults() 
		{
			DisplayName.SetDefault("Luminite Merchant");
			Main.npcFrameCount[npc.type] = 25;
			NPCID.Sets.ExtraFramesCount[npc.type] = 9;
			NPCID.Sets.AttackFrameCount[npc.type] = 4;
			NPCID.Sets.DangerDetectRange[npc.type] = 1000;
			NPCID.Sets.AttackType[npc.type] = 0;
			NPCID.Sets.AttackTime[npc.type] = 60;
			NPCID.Sets.AttackAverageChance[npc.type] = 66;
			NPCID.Sets.HatOffsetY[npc.type] = 4;
		}

		public override void SetDefaults() 
		{
			npc.townNPC = true;
			npc.friendly = true;
			npc.width = 18;
			npc.height = 40;
			npc.aiStyle = 7;
			npc.damage = 50;
			npc.defense = 35;
			npc.lifeMax = 750;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath1;
			npc.knockBackResist = 0.5f;
			animationType = NPCID.Guide;
		}

		public override bool CanTownNPCSpawn(int numTownNPCs, int money) 
		{		
			for (int k = 0; k < 255; k++) 
			{
				Player player = Main.player[k];
				if (!player.active) 
				{
					continue;
				}

				foreach (Item item in player.inventory) 
				{
					if (item.type == ItemID.LunarBar) 
					{
						return true;
					}
				}
			}
			return false;
		}

		public override string TownNPCName() 
		{
			switch (WorldGen.genRand.Next(4)) 
			{
				case 0:
					return "Jackson";
				case 1:
					return "Goku";
				case 2:
					return "Leonard";
				default:
					return "Sean";
			}
		}

		public override void FindFrame(int frameHeight) 
		{
			/*npc.frame.Width = 40;
			if (((int)Main.time / 10) % 2 == 0)
			{
				npc.frame.X = 40;
			}
			else
			{
				npc.frame.X = 0;
			}*/
		}

		public override string GetChat() 
		{
			int Guide = NPC.FindFirstNPC(NPCID.Guide);
			if (Guide >= 0 && Main.rand.NextBool(4)) 
			{
				return "That guy " + Main.npc[Guide].GivenName + " is no help at all. Why do you keep him around?";
			} 
			switch (Main.rand.Next(4)) 
			{
				case 0:
					return "...";
				case 1:
					return "Subscribe to PewDiePie";
				case 2:
					return "You are pretty ruthless. Have you tried talking to any of these monsters?";
				default:
					return "What do you want to buy?";
			}
		}
		
		public override void SetChatButtons(ref string button, ref string button2) 
		{
			button = Language.GetTextValue("LegacyInterface.28");
		}
		
		public override void OnChatButtonClicked(bool firstButton, ref bool shop)
		{
			if(firstButton)
			{
				shop = true;
			}
		}

		public bool SacredToolsDownedAraghur
		{
        get { return SacredTools.ModdedWorld.FlariumSpawns; }
        }
		
		public override void SetupShop(Chest shop, ref int nextSlot) 
		{
			shop.item[nextSlot].SetDefaults(ItemID.LunarBar);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.FragmentSolar);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.FragmentNebula);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.FragmentVortex);
			nextSlot++;
			shop.item[nextSlot].SetDefaults(ItemID.FragmentStardust);
			nextSlot++;
			Mod sacredTools = ModLoader.GetMod("SacredTools");
            if (sacredTools != null)
            {
                shop.item[nextSlot].SetDefaults(sacredTools.ItemType("FragmentNova"));
                nextSlot++;
				if (SacredToolsDownedAraghur)
					{
					shop.item[nextSlot].SetDefaults (ModLoader.GetMod("SacredTools").ItemType("FragmentBlight"));
					nextSlot++;
					shop.item[nextSlot].SetDefaults (ModLoader.GetMod("SacredTools").ItemType("FragmentHatred"));
					nextSlot++;
					}
            }
			shop.item[nextSlot].SetDefaults(ItemID.SuperHealingPotion);
			nextSlot++;
		}

		public override bool CanGoToStatue(bool toKingStatue) 
		{
			return true;
		}

		public override void TownNPCAttackStrength(ref int damage, ref float knockback) 
		{
			damage = 50;
			knockback = 4f;
		}

		public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown) 
		{
			cooldown = 20;
			randExtraCooldown = 10;
		}

		public override void TownNPCAttackProj(ref int projType, ref int attackDelay) 
		{			
			projType = ProjectileID.MoonlordBullet;
			attackDelay = 1;
		}

		public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset) 
		{
			multiplier = 12f;
			randomOffset = 2f;
		}
	}
}
