using System.Collections.Generic;
using MLI.Method;

namespace MLI.Machine
{
	public class ExecUnit
	{
		private int unifUnitCount;
		private List<UnifUnit> unifUnits;
		private bool busy;

		public ExecUnit(int unifUnitCount)
		{
			this.unifUnitCount = unifUnitCount;
			busy = false;
			BuildUnifUnits();
		}

		private void BuildUnifUnits()
		{
			unifUnits = new List<UnifUnit>();
			for (int i = 0; i < unifUnitCount; i++)
			{
				unifUnits.Add(new UnifUnit());
			}
		}

		public void SetBusyFlag(bool busy)
		{
			this.busy = busy;
		}

		public bool IsBusy()
		{
			return busy;
		}

		public void RunProcess(IProcess process)
		{
			process.Run();
		}
	}
}