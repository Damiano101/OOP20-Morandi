using System;

using BadLogic.Gdx.Assets;
using BadLogic.Gdx.Files;
using BadLogic.Gdx.Graphics;
using BadLogic.Gdx.Graphics.G2d;
using BadLogic.Gdx.Maps.Tiled;
using BadLogic.Gdx.Scenes.Scene2d.UI;

namespace MartyAdventure.View
{
	public static class Toolbox
	{
		private const string TEXTURE_EXTENSION = "png";
		private const string MAP_EXTENSION = "tmx";
		private const string ALTLAS_EXTENSION = "atlas";
		private const string SKIN_EXTENSION = "json";

		private static readonly AssetManager assetManager;

		static Toolbox()
		{
			assetManager = new AssetManager();
			assetManager.SetLoader(typeof(TmxMapLoader), new TmxMapLoader());
		}

		private static AssetDescriptor<Texture> GetTextureAssetDescriptor(string path)
		{
			return new AssetDescriptor<Texture>(GetHandle(path, TEXTURE_EXTENSION), typeof(Texture));
		}

		private static AssetDescriptor<TiledMap> GetMapAssetDescriptor(string path)
		{
			return new AssetDescriptor<TiledMap>(GetHandle(path, MAP_EXTENSION), typeof(TiledMap));
		}

		private static AssetDescriptor<TextureAtlas> GetAtlasAssetDescriptor(string path)
		{
			return new AssetDescriptor<TextureAtlas>(GetHandle(path, ALTLAS_EXTENSION), typeof(TextureAtlas));
		}

		private static AssetDescriptor<Skin> GetSkinAssetDescriptor(string path)
		{
			return new AssetDescriptor<Skin>(GetHandle(path, SKIN_EXTENSION), typeof(Skin));
		}

		/// <summary>
		/// Verifies that the path isn't empty and tries to verify the file type from the extension.
		/// </summary>
		/// <param name="path">The path of the file to handle.</param>
		/// <param name="expectedExtension">The extension of the file.</param>
		/// <returns>A file handle to the asset to load.</returns>
		private static FileHandle GetHandle(string path, string expectedExtension)
		{
			if (string.IsNullOrWhiteSpace(path))
			{
				throw new ArgumentException("Invalid empty asset path");
			}

			FileHandle fh = new FileHandle(path);
			if (string.IsNullOrWhiteSpace(fh.Extension) || fh.Extension.Equals(expectedExtension, StringComparison.OrdinalIgnoreCase))
			{
				return fh;
			}
			else
			{
				throw new ArgumentException("Expected '" + expectedExtension + "' got '" + fh.Extension + "'");
			}
		}

		private static T GetAsset<T>(AssetDescriptor<T> assetDescriptor)
		{
			if (!assetManager.IsLoaded(assetDescriptor))
			{
				// Check if the asset is known by the asset manager.
				if (!assetManager.Contains(assetDescriptor.AssetPath, assetDescriptor.GetType()))
				{
					// Queue the asset for loading.
					assetManager.Load(assetDescriptor);
				}

				// Block and load the asset if it's not loaded yet.
				return assetManager.FinishLoadingAsset(assetDescriptor);
			}
			return assetManager.Get(assetDescriptor);
		}

		/// <summary>
		/// Progress asset loading.
		/// </summary>
		/// <returns><c>true</c> if all the assets have been loaded.</returns>
		public static bool UpdateAssetLoading()
		{
			return assetManager.Update();
		}

		/// <summary>
		/// Unloads the asset if the reference count has reached 0.
		/// </summary>
		/// <param name="filePath">the path of the assets.</param>
		public static void UnloadAsset(string filePath)
		{
			if (assetManager.Contains(filePath))
			{
				assetManager.Unload(filePath);
			}
		}

		/// <summary>
		/// Get the percentage of assets loaded so far.
		/// </summary>
		/// <returns>A value between 0.0 and 1.0.</returns>
		public static float LoadCompletion()
		{
			return assetManager.GetProgress();
		}

		/// <summary>
		/// The number of assets in queue for loading.
		/// </summary>
		/// <returns>The number of queued assets.</returns>
		public static int QueuedAssetCount()
		{
			return assetManager.GetQueuedAssets();
		}

		/// <summary>
		/// Check if an asset is loaded.
		/// </summary>
		/// <param name="path">the path of the file.</param>
		/// <returns><c>true</c>if the asset at the path has been fully loaded.</returns>
		public static bool IsAssetLoaded(string path)
		{
			return assetManager.IsLoaded(path);
		}

		/// <summary>
		/// Queue a map for loading.
		/// </summary>
		/// <param name="path">The path to the asset.</param>
		public static void QueueMap(string path)
		{
			assetManager.Load(GetMapAssetDescriptor(path));
		}

		/// <summary>
		/// Queue an atlas for loading.
		/// </summary>
		/// <param name="path">The path to the asset.</param>
		public static void QueueAtlas(string path)
		{
			assetManager.Load(GetAtlasAssetDescriptor(path));
		}

		/// <summary>
		/// Queue a skin for loading.
		/// </summary>
		/// <param name="path">The path to the asset.</param>
		public static void QueueSkin(string path)
		{
			assetManager.Load(GetSkinAssetDescriptor(path));
		}

		/// <summary>
		/// Queue a texture for loading.
		/// </summary>
		/// <param name="path">The path to the asset.</param>
		public static void QueueTexture(string path)
		{
			assetManager.Load(GetTextureAssetDescriptor(path));
		}

		/// <summary>
		/// Get the map at the path. Block if the asset hasn't been fully loaded yet.
		/// </summary>
		/// <param name="path">The path of the asset.</param>
		/// <returns>The asset at the given path.</returns>
		public static TiledMap GetMap(string path)
		{
			return assetManager.Get(GetMapAssetDescriptor(path));
		}

		/// <summary>
		/// Get the texture at the path. Block if the asset hasn't been fully loaded yet.
		/// </summary>
		/// <param name="path">The path of the asset.</param>
		/// <returns>The asset at the given path.</returns>
		public static Texture GetTexture(string path)
		{
			return assetManager.Get(GetTextureAssetDescriptor(path));
		}

		/// <summary>
		/// Get the atlas at the path. Block if the asset hasn't been fully loaded yet.
		/// </summary>
		/// <param name="path">The path of the asset.</param>
		/// <returns>The asset at the given path.</returns>
		public static TextureAtlas GetAtlas(string path)
		{
			return assetManager.Get(GetAtlasAssetDescriptor(path));
		}

		/// <summary>
		/// Get the skin at the path. Block if the asset hasn't been fully loaded yet.
		/// </summary>
		/// <param name="path">The path of the asset.</param>
		/// <returns>The asset at the given path.</returns>
		public static Skin GetSkin(string path)
		{
			return assetManager.Get(GetSkinAssetDescriptor(path));
		}
	}
}
