using System;
using System.IO;
using BadLogic.Gdx.Files;

namespace BadLogic.Gdx.Assets
{
	public class AssetDescriptor<T>
	{
		private readonly FileHandle fileHandle;

		public AssetDescriptor(FileHandle fileHandle, Type type)
		{
			this.fileHandle = fileHandle;
		}

		public string AssetPath {
			get => fileHandle.Path;
		}
	}
}
