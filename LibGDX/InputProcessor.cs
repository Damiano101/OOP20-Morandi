using MartyAdventure.LibGDX.Input;

namespace BadLogic.Gdx
{
	public interface IInputProcessor
	{
		bool KeyDown(Keys key);

		bool KeyUp(Keys key);

		bool KeyTyped(char character);

		bool TouchDown(int screenX, int screenY, int pointer, int button);

		bool TouchUp(int screenX, int screenY, int pointer, int button);

		bool TouchDragged(int screenX, int screenY, int pointer);

		bool MouseMoved(int screenX, int screenY);

		bool Scrolled(float amountX, float amountY);
	}
}
