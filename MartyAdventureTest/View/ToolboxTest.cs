using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MartyAdventure.View;

namespace MartyAdventureTest.View
{
	[TestClass]
	public class ToolboxTest
	{
		private const string BADLOGIC_TEXTURE_PATH = "assets/tests/badlogic.png";
		private const string TMX_MAP_PATH = "assets/Level/Map/map1.tmx";

		private void LoadBlocking<R>(string path, Func<string, R> get)
		{
			R res = get(path);
			Assert.IsNotNull(res);

			Toolbox.UnloadAsset(path);
		}

		private void LoadPreloaded<R>(string path, Action<string> queue, Func<string, R> get)
		{
			queue(path);

			// Load the whole thing
			while (!Toolbox.IsAssetLoaded(path))
			{
				Toolbox.UpdateAssetLoading();
			}

			LoadBlocking(path, get);
		}

		[TestMethod]
		[Timeout(1000)]
		public void LoadTextureBlocking()
		{
			LoadBlocking(BADLOGIC_TEXTURE_PATH, Toolbox.GetTexture);
		}

		[TestMethod]
		[Timeout(1000)]
		public void LoadTexturePreloaded()
		{
			LoadPreloaded(BADLOGIC_TEXTURE_PATH, Toolbox.QueueTexture, Toolbox.GetTexture);
		}

		[TestMethod]
		[Timeout(1000)]
		public void LoadTmxMapBlocking()
		{
			LoadBlocking(TMX_MAP_PATH, Toolbox.GetMap);
		}

		[TestMethod]
		[Timeout(1000)]
		public void LoadTmxMapPreloaded()
		{
			LoadPreloaded(TMX_MAP_PATH, Toolbox.QueueMap, Toolbox.GetMap);
		}

		[TestMethod]
		[Timeout(1000)]
		public void LoadBlockingWrongTexture()
		{
			Assert.ThrowsException<ArgumentException>(() => LoadBlocking(TMX_MAP_PATH, Toolbox.GetTexture));
			Toolbox.UnloadAsset(TMX_MAP_PATH);
		}

		[TestMethod]
		[Timeout(1000)]
		public void LoadPreloadedWrongTexture()
		{
			Assert.ThrowsException<ArgumentException>(() => LoadPreloaded(TMX_MAP_PATH, Toolbox.QueueTexture, Toolbox.GetTexture));
			Toolbox.UnloadAsset(TMX_MAP_PATH);
		}

		[TestMethod]
		[Timeout(1000)]
		public void LoadBlockingWrongMap()
		{
			Assert.ThrowsException<ArgumentException>(() => LoadBlocking(BADLOGIC_TEXTURE_PATH, Toolbox.GetMap));
			Toolbox.UnloadAsset(BADLOGIC_TEXTURE_PATH);
		}

		[TestMethod]
		[Timeout(1000)]
		public void LoadPreloadedWrongMap()
		{
			Assert.ThrowsException<ArgumentException>(() => LoadPreloaded(BADLOGIC_TEXTURE_PATH, Toolbox.QueueMap, Toolbox.GetMap));
			Toolbox.UnloadAsset(BADLOGIC_TEXTURE_PATH);
		}
	}
}
