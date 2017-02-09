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
		private IProcess process;
		private IProcess parentProcess;

		private Message()
		{
			
		}

		public MessageType GetMessageTypeType() => type;

		public static Message GetCreateMessage(IProcess process, IProcess parentProcess)
		{
			return new Message
			{
				type = MessageType.Create,
				process = process,
				parentProcess = parentProcess
			};
		}

		public static Message GetEndMessage(IProcess process)
		{
			return new Message
			{
				type = MessageType.End,
				process = process
			};
		}
	}
}