namespace MLI.Data
{
	public enum CommandId
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

	public class Command
	{
		public CommandId id { get; }
		public string title { get; }
		public int time { set; get; }

		public Command(CommandId id, string title, int time)
		{
			this.id = id;
			this.title = title;
			this.time = time;
		}
	}
}