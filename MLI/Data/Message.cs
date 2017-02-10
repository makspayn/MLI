using MLI.Method;

namespace MLI.Data
{
	public class Message
	{
		public enum MessageType
		{
			Create, End
		}

		private MessageType type;
		private Process process;
		private Process parentProcess;

		private Message()
		{
			
		}

		public MessageType GetMessageTypeType() => type;

		public Process GetProcess() => process;

		public Process GetParentProcess() => parentProcess;

		public static Message GetCreateMessage(Process process, Process parentProcess)
		{
			return new Message
			{
				type = MessageType.Create,
				process = process,
				parentProcess = parentProcess
			};
		}

		public static Message GetEndMessage(Process process)
		{
			return new Message
			{
				type = MessageType.End,
				process = process
			};
		}
	}
}