using MartyAdventure.View.Entity;


namespace MartyAdventure.Controller
{
	/// <summary>
	/// An entity whose movement and direction can be controlled.
	/// </summary>
	public interface IControllableEntity
	{
		/// <summary>
		///  Calculate the entity's position for the next update.
		/// </summary>
		/// <param name="direction">The direction the entity is moved to.</param>
		/// <param name="delta">The delta time.</param>
		void CalculateNextPosition(EntityDirection direction, float delta);

		/// <summary>
		/// Set the entity's state.
		/// </summary>
		/// <param name="state">The state to set.</param>
		void SetState(EntityState state);

		/// <summary>
		/// Set the entity's direction.
		/// </summary>
		/// <param name="direction">The direction to set.</param>
		void SetDirection(EntityDirection direction);
	}
}
