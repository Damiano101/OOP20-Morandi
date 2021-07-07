using System;
using System.Collections.Generic;
using System.Text;

namespace BadLogic.Gdx.Files
{
	public class FileHandle
	{
		/// <summary>
		/// Only for the dummy LibGDX internal usage.
		/// </summary>
		internal string Path { get; set; }

		public FileHandle(string path)
		{
			Path = path;
		}

		public string Extension
		{
			get => Path.Substring(Path.LastIndexOf('.') + 1);
		}

	}
}
