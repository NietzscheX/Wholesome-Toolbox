using robotManager.FiniteStateMachine;
using System;

namespace WholesomeToolbox
{
    /// <summary>
    /// States related methods
    /// </summary>
    public class WTState
    {
        /// <summary>
        /// Adds a state to the FSM with the same prio of the specified state. 
        /// Does not remove the original state.
        /// Moves states so that no 2 states have the same prio.
        /// ex : WTState.AddState(engine, waitOnTaxiState, "FlightMaster: Take taxi");
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="state"></param>
        /// <param name="inPlaceOf"></param>
        public static void AddState(Engine engine, State state, string inPlaceOf)
        {
            if (engine != null && !engine.States.Exists(s => s.DisplayName == state.DisplayName))
            {
                try
                {
                    State stateToReplace = engine.States.Find(s => s.DisplayName == inPlaceOf);

                    if (stateToReplace == null)
                    {
                        WTLogger.LogError($"Couldn't find state {inPlaceOf}");
                        return;
                    }

                    int priorityToSet = stateToReplace.Priority;

                    // Move all superior states one slot up
                    foreach (State s in engine.States)
                    {
                        if (s.Priority >= priorityToSet)
                            s.Priority++;
                    }

                    state.Priority = priorityToSet;
                    engine.AddState(state);
                    engine.States.Sort();
                }
                catch (Exception ex)
                {
                    WTLogger.LogError("AddState Error : {0}" + ex.ToString());
                }
            }
        }

        /// <summary>
        /// Removes a state from the FSM
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="stateToRemove"></param>
        public static void RemoveState(Engine engine, string stateToRemove)
        {
            bool stateExists = engine.States.Exists(s => s.DisplayName == stateToRemove);
            if (stateExists && engine != null && engine.States.Count > 5)
            {
                try
                {
                    State state = engine.States.Find(s => s.DisplayName == stateToRemove);
                    engine.States.Remove(state);
                    engine.States.Sort();
                }
                catch (Exception ex)
                {
                    WTLogger.LogError("Erreur : {0}" + ex.ToString());
                }
            }
        }
    }
}
