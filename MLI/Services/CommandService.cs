using System;

namespace MLI.Services
{
	public enum Command
	{
		UnificationConstantAndConstant,
		UnificationConstantAndVariable,
		UnificationConstantAndFunctor,
		UnificationVariableAndVariable,
		UnificationVariableAndFunctor,
		UnificationFunctorAndFunctor,
		CreateMessage,
		AddMessageToQueue,
		DeleteMessageFromQueue,
		FormPredicatePair,
		FormRests,
		FormRest,
		FormConclusion,
		MinimizeSequence,
		CreateRestsMatrix,
		AnalyzeRestsMatrix,
		CanInference,
		CreateFrame,
		AddFrameToList,
		DeleteFrameFromList,
		CreateProcess,
		ReadMemory,
		WriteMemory
	}

	public static class CommandService
	{
		public static int RunCommand(Command command, params object[] param)
		{
			int time = 0;
			switch (command)
			{
				case Command.UnificationConstantAndConstant:
					time = 1;
					break;
				case Command.UnificationConstantAndVariable:
					time = 3;
					break;
				case Command.UnificationConstantAndFunctor:
					time = 1;
					break;
				case Command.UnificationVariableAndVariable:
					time = 3;
					break;
				case Command.UnificationVariableAndFunctor:
					time = (bool)param[0] ? 5 : 10;
					break;
				case Command.UnificationFunctorAndFunctor:
					time = 15;
					break;
				case Command.CreateMessage:
					time = 10;
					break;
				case Command.AddMessageToQueue:
					time = 2;
					break;
				case Command.DeleteMessageFromQueue:
					time = 2;
					break;
				case Command.CanInference:
					time = 3 * (int)param[0] * (int)param[1] * (int)param[2];
					break;
				case Command.FormPredicatePair:
					time = 2 * (int)param[0] * (int)param[1];
					break;
				case Command.MinimizeSequence:
					time = 4 * (int)param[0];
					break;
				case Command.FormRest:
					time = 2 * (int)param[0];
					break;
				case Command.FormConclusion:
					time = 3 * (int)param[0];
					break;
				case Command.CreateRestsMatrix:
					time = 5 * (int)param[0];
					break;
				case Command.AnalyzeRestsMatrix:
					time = 1;
					break;
				case Command.ReadMemory:
					time = 10;
					break;
				case Command.WriteMemory:
					time = 15;
					break;
				case Command.CreateFrame:
					time = 7;
					break;
				case Command.AddFrameToList:
					time = 3;
					break;
				case Command.DeleteFrameFromList:
					time = 3;
					break;
				case Command.CreateProcess:
					time = 5;
					break;
				case Command.FormRests:
					time = 2 * (int)param[0];
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(command), command, null);
			}
			return time;
		}
	}
}