using MLI.Data;
using MLI.Method;

namespace MLI.Machine
{
	public class UnifUnit : ProcessUnit
	{
		private int parentNumber;

		public UnifUnit(string name, int number, ProcessUnit parent) : base(name, number)
		{
			id += $" ({parent.GetId()})";
			parentNumber = parent.GetNumber();
		}

		protected override void FormMessages(Process process)
		{
			supervisor.AddMessage(
					new Message(process, Message.MessageType.End), this);
		}

		public int GetParentNumber() => parentNumber;
	}
}