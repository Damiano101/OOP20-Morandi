using System.Collections.Generic;

using BadLogic.Gdx;

using MartyAdventure.LibGDX.Input;
using MartyAdventure.View.Entity;

namespace MartyAdventure.Controller
{
	/// <summary>
	/// Handles the player inpuut, submitting it to a given entity.
	/// </summary>
	public class PlayerInputProcessor : IInputProcessor
	{
		/*
		 * Creating this ordered array to impose a stable preference in the entity's
		 * direction, otherwise at each run the hashcode of the EntityDirection's
		 * singletons may change, resulting in an inconsistent orientation across runs
		 * when the player presses more than a single key.
		 *
		 * Using a readonly instead of const since C# doesn't consider the initial assignment constant.
		 */
		private static readonly EntityDirection[] orderedDirections = { EntityDirection.Up, EntityDirection.Down,
			EntityDirection.Left, EntityDirection.Right
		};

		private static PlayerInputProcessor instance;

		// Using a readonly instead of const since C# doesn't consider the initial assignment constant.
		public static readonly EntityDirection DEFAULT_DIRECTION = orderedDirections[0];

		private IDictionary<EntityDirection, bool> directionsMap;
		private IControllableEntity player;

		/// <summary>
		/// Get the player input processor singleton.
		/// There may be 1 and only 1 player input processor to prevent multiple
		/// instances catching each's other's keycodes or sending multiple updates to the
		/// same player
		/// </summary>
		/// <returns>The input processor.</returns>
		public static PlayerInputProcessor GetPlayerInputProcessor()
		{
			if (instance == null)
			{
				instance = new PlayerInputProcessor();
			}
			return instance;
		}

		private PlayerInputProcessor()
		{
			ResetState();
		}

		private EntityDirection? MatchKeycode(Keys key)
		{
			return key switch
			{
				Keys.A => EntityDirection.Left,
				Keys.D => EntityDirection.Right,
				Keys.W => EntityDirection.Up,
				Keys.S => EntityDirection.Down,
				_ => null,
			};
		}

		/// <summary>
		/// Sets the directions state if the key is recognized.
		/// </summary>
		/// <param name="key">The key to change the state of.</param>
		/// <param name="state">The new key state.</param>
		/// <returns><c>true</c> if the key was recognized.</returns>
		private bool SetDirection(Keys key, bool state)
		{
			EntityDirection? dir = MatchKeycode(key);
			if (dir.HasValue)
			{
				directionsMap[dir.Value] = state;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Get the current player.
		/// </summary>
		/// <returns>The current player instance.</returns>
		public IControllableEntity GetPlayer()
		{
			return player;
		}

		/// <summary>
		/// Sets the player entity to send inputs to.
		/// </summary>
		/// <param name="playerEntity">The player entity.</param>
		/// <param name="setDefaults">If <c>true</c> sets the player with the default values.</param>
		public void SetPlayer(IControllableEntity playerEntity, bool setDefaults)
		{
			player = playerEntity;
			if (setDefaults)
			{
				player.SetDirection(DEFAULT_DIRECTION);
				player.SetState(EntityState.Idle);
			}
		}

		/// <summary>
		/// Resets the internal state. May be used after the processor has been
		/// temporarily disabled or paused.
		/// </summary>
		public void ResetState()
		{
			directionsMap = new Dictionary<EntityDirection, bool>();
			foreach (EntityDirection dir in typeof(EntityDirection).GetEnumValues())
			{
				directionsMap.Add(dir, false);
			}
		}

		/// <summary>
		/// Update the player entity state and set the next position.
		/// </summary>
		/// <param name="delta">Time since last update.</param>
		public void Update(float delta)
		{
			foreach (EntityDirection dir in orderedDirections)
			{
				if (directionsMap[dir])
				{
					player.SetState(EntityState.Walking);
					player.SetDirection(dir);
					player.CalculateNextPosition(dir, delta);
					return;
				}
			}
			player.SetState(EntityState.Idle);
		}

		public bool KeyDown(Keys key)
		{
			// Casting like this make cause Keys to have an unmapped value, but SetDirection handles it.
			return SetDirection(key, true);
		}

		public bool KeyUp(Keys key)
		{
			// Casting it like this make cause Keys to have an unmapped value, but SetDirection handles it.
			return SetDirection(key, false);
		}

		public bool KeyTyped(char character)
		{
			// Unused
			return false;
		}

		public bool TouchDown(int screenX, int screenY, int pointer, int button)
		{
			// Unused
			return false;
		}

		public bool TouchUp(int screenX, int screenY, int pointer, int button)
		{
			// Unused
			return false;
		}

		public bool TouchDragged(int screenX, int screenY, int pointer)
		{
			// Unused
			return false;
		}

		public bool MouseMoved(int screenX, int screenY)
		{
			// Unused
			return false;
		}

		public bool Scrolled(float amountX, float amountY)
		{
			// Unused
			return false;
		}
	}
}
