using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using MartyAdventure.Controller;
using MartyAdventure.View.Entity;
using MartyAdventure.LibGDX.Input;

namespace MartyAdventureTest.Controller
{
	[TestClass]
	public class PlayerInputProcessorTest
	{
		private class DummyControllableEntity : IControllableEntity
		{
			public EntityDirection direction;
			public EntityState state;

			public void CalculateNextPosition(EntityDirection dir, float delta)
			{
				direction = dir;
			}

			public void SetDirection(EntityDirection dir)
			{
				direction = dir;
			}

			public void SetState(EntityState sts)
			{
				state = sts;
			}
		}

		private const float DUMMY_DELTA = 0.1f;

		private DummyControllableEntity GetDummy(PlayerInputProcessor inputProcessor)
		{
			return (DummyControllableEntity)inputProcessor.GetPlayer();
		}

		private void AssertDirection(EntityDirection direction, PlayerInputProcessor inputProcessor)
		{
			Assert.AreEqual(direction, GetDummy(inputProcessor).direction);
		}

		private void AssertDirectionNot(EntityDirection direction, PlayerInputProcessor inputProcessor)
		{
			Assert.AreNotEqual(direction, GetDummy(inputProcessor).direction);
		}

		private void AssertState(EntityState state, PlayerInputProcessor inputProcessor)
		{
			Assert.AreEqual(state, GetDummy(inputProcessor).state);
		}

		/// <summary>
		/// Get a clean player input processor.
		/// </summary>
		/// <returns>The input processor.</returns>
		private PlayerInputProcessor GetInputProcessor()
		{
			PlayerInputProcessor inputProcessor = PlayerInputProcessor.GetPlayerInputProcessor();
			inputProcessor.ResetState();
			inputProcessor.SetPlayer(new DummyControllableEntity(), true);
			inputProcessor.Update(DUMMY_DELTA);
			return inputProcessor;
		}

		/// <summary>
		/// Tests that the direction is correctly updated after each key press and release.
		/// </summary>
		/// <param name="key">The key pressed.</param>
		/// <param name="direction">Direction of the entity.</param>
		/// <param name="differentKey">Check different.</param>
		private void KeyInput(Keys key, EntityDirection direction, Keys differentKey)
		{
			PlayerInputProcessor inputProcessor = GetInputProcessor();

			// The direction mustn't be set before the update
			// (Unless it's the default direction).
			Assert.IsTrue(inputProcessor.KeyDown(key));
			if (direction != PlayerInputProcessor.DEFAULT_DIRECTION)
			{
				AssertDirectionNot(direction, inputProcessor);
			}

			// The direction must be set after the update.
			inputProcessor.Update(DUMMY_DELTA);
			AssertDirection(direction, inputProcessor);

			// The direction must still be set after a key up.
			Assert.IsTrue(inputProcessor.KeyUp(key));
			AssertDirection(direction, inputProcessor);

			// Set a different direction
			Assert.IsTrue(inputProcessor.KeyDown(differentKey));
			// The direction must then be unset after the update.
			inputProcessor.Update(DUMMY_DELTA);
			AssertDirectionNot(direction, inputProcessor);
		}

		/// <summary>
		/// Test that a precedent vertical direction key press is kept after horizontal keys are also pressed.
		/// </summary>
		/// <param name="keycode">The key pressed.</param>
		/// <param name="EntityDirection">The direction to check.</param>
		private void VerticalInputPriority(Keys key, EntityDirection direction)
		{
			PlayerInputProcessor inputProcessor = GetInputProcessor();
			Assert.IsTrue(inputProcessor.KeyDown(key));
			Assert.IsTrue(inputProcessor.KeyDown(Keys.D));
			Assert.IsTrue(inputProcessor.KeyDown(Keys.A));
			inputProcessor.Update(DUMMY_DELTA);

			AssertDirection(direction, inputProcessor);
		}

		/// <summary>
		/// Test that the input is correctly detected, set and unset.
		/// </summary>
		[TestMethod]
		public void TestKeyInput()
		{
			KeyInput(Keys.W, EntityDirection.Up, Keys.S);
			KeyInput(Keys.S, EntityDirection.Down, Keys.W);
			KeyInput(Keys.A, EntityDirection.Left, Keys.D);
			KeyInput(Keys.D, EntityDirection.Right, Keys.A);
		}

		/// <summary>
		/// Test that vertical input is given priority to horizontal inputs.
		/// </summary>
		[TestMethod]
		public void TestVerticalPriorityInput()
		{
			VerticalInputPriority(Keys.W, EntityDirection.Up);
			VerticalInputPriority(Keys.S, EntityDirection.Down);
		}

		/// <summary>
		/// Test that the state is correctly set and unset.
		/// </summary>
		[TestMethod]
		public void TestStatus()
		{
			PlayerInputProcessor inputProcessor = GetInputProcessor();
			// By default, the player should be idle
			AssertState(EntityState.Idle, inputProcessor);

			// The walking state mustn't be set before the update.
			Assert.IsTrue(inputProcessor.KeyDown(Keys.W));
			AssertState(EntityState.Idle, inputProcessor);

			// The walking state must be set after the update.
			inputProcessor.Update(DUMMY_DELTA);
			AssertState(EntityState.Walking, inputProcessor);

			// The walking state must still be set after a key up.
			Assert.IsTrue(inputProcessor.KeyUp(Keys.W));
			AssertState(EntityState.Walking, inputProcessor);

			// The state must then be set to idle again after the update.
			inputProcessor.Update(DUMMY_DELTA);
			AssertState(EntityState.Idle, inputProcessor);
		}
	}
}
