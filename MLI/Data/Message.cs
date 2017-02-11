using MLI.Method;

namespace MLI.Data
{
	public class Message
	{
		public enum MessageType
		{
			Create, End
		}

		public MessageType Type { get; }
		public Process Process { get; }
		public Process ParentProcess { get; }

		public Message(Process process, MessageType type)
		{
			Type = type;
			Process = process;
			ParentProcess = process.GetParentProcess();
		}
	}
}