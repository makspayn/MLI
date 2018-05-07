using System;

namespace MLI.Machine
{
    public class ReconfigurationUnit
    {
        private int unitResourceCount;
        private int execUnitWeight;
        private object resourceSync = new object();
        
        public ReconfigurationUnit(int unitResourceCount, int execUnitWeight)
        {
            this.unitResourceCount = unitResourceCount;
            this.execUnitWeight = execUnitWeight;
        }

        public bool CanGetResource(ProcessUnit processUnit)
        {
            return GetUnitResource(processUnit, processUnit is ExecUnit ? execUnitWeight : 1);
        }
        
        public void ReturnResource(ProcessUnit processUnit)
        {
            ReturnUnitResource(processUnit is ExecUnit ? execUnitWeight : 1);
        }
        
        private bool GetUnitResource(ProcessUnit processUnit, int unitWeight)
        {
            lock (resourceSync)
            {
                if (unitResourceCount - unitWeight < 0) return false;
                if (processUnit.IsBusy()) return false;
                unitResourceCount -= unitWeight;
                return true;
            }
        }

        private void ReturnUnitResource(int unitWeight)
        {
            lock (resourceSync)
            {
                unitResourceCount += unitWeight;
            }
        }
    }
}