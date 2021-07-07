using System;
using BadLogic.Gdx.Maps.Tiled;

namespace BadLogic.Gdx.Assets
{
	/// <summary>
	/// A dummy asset manager to implement code around.
	/// </summary>
	public class AssetManager
	{
		public void SetLoader(Type type, TmxMapLoader loader)
		{

		}

		public void Load<T>(AssetDescriptor<T> descriptor)
		{

		}

		public bool IsLoaded<T>(AssetDescriptor<T> descriptor)
		{
			return true;
		}

		public bool IsLoaded(string path)
		{
			return true;
		}

		public bool Contains(string path)
		{
			return true;
		}

		public bool Contains(string path, Type type)
		{
			return true;
		}

		public bool Update()
		{
			return true;
		}

		public T FinishLoadingAsset<T>(AssetDescriptor<T> assetDescriptor)
		{
			return Get(assetDescriptor);
		}

		public T Get<T>(AssetDescriptor<T> descriptor)
		{
			return Activator.CreateInstance<T>();
		}

		public void Unload(string path)
		{

		}

		public float GetProgress()
		{
			return 0.0f;
		}

		public int GetQueuedAssets()
		{
			return 0;
		}
	}
}
