using MartyAdventure.LibGDX.Input;

namespace BadLogic.Gdx
{
	public interface IInputProcessor
	{
		public bool KeyDown(Keys key);

		public bool KeyUp(Keys key);

		public bool KeyTyped(char character);

		public bool TouchDown(int screenX, int screenY, int pointer, int button);

		public bool TouchUp(int screenX, int screenY, int pointer, int button);

		public bool TouchDragged(int screenX, int screenY, int pointer);

		public bool MouseMoved(int screenX, int screenY);

		public bool Scrolled(float amountX, float amountY);
	}
}
