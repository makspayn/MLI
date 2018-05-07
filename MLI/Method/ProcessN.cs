﻿using System.Collections.Generic;
using System.Linq;
using MLI.Data;
using MLI.Machine;
using MLI.Services;

namespace MLI.Method
{
	public class ProcessN : Process
	{
		public enum ProcessNStatus
		{
			ZeroRest, RestExists, NoRest
		}
		
		private List<Sequence> facts;
		private Sequence ruleSequence;
		private Sequence conclusionSequence;
		private Sequence rest;
		private ProcessNStatus processNStatus;

		public ProcessN(Process parentProcess, int index, List<Sequence> facts, Sequence ruleSequence, Sequence conclusionSequence) : base(parentProcess, index)
		{
			infoLevel = LogService.InfoLevel.ProcessN;
			reentry = false;
			name = "N";
			inputData = $"правило {ruleSequence}";
			this.facts = facts;
			this.ruleSequence = ruleSequence;
			this.conclusionSequence = conclusionSequence;
			LogService.Debug(LogService.InfoLevel.ProcessN, $"[{GetFullName()}]: создан процесс");
		}

		protected override void FirstRun()
		{
			Log("процесс запущен");
			Log(inputData);
			runTime += processUnit.RunCommand(CommandId.CreateMessage);
			runTime += processUnit.RunCommand(CommandId.AddMessageToQueue);
			childProcesses.Add(new ProcessM(this, ++childProcessCount, ruleSequence, new List<Sequence>() { conclusionSequence }, false));
			status = Status.Progress;
			reentry = true;
			Log("ожидание завершения дочерних процессов");
		}

		protected override void ReRun()
		{
			Log("процесс повторно запущен");
			List<Process> newChildProcesses = new List<Process>();
			runTime += processUnit.RunCommand(CommandId.AnalyzeRestsMatrix, childProcesses.Cast<ProcessM>().Sum(childProcess => childProcess.GetRests().Count));
			if (childProcesses.Cast<ProcessM>()
				.All(childProcess => childProcess.GetProcessMStatus() != ProcessM.ProcessMStatus.ZeroRest))
			{
				foreach (ProcessM childProcess in childProcesses.Cast<ProcessM>())
				{
					if (childProcess.GetProcessMStatus() == ProcessM.ProcessMStatus.OnesMatrix)
					{
						if (childProcessCount == 1)
						{
							processNStatus = ProcessNStatus.NoRest;
							break;
						}
						runTime += processUnit.RunCommand(CommandId.FormRest, childProcess.GetRests().Count);
						switch (FormRest(childProcess.GetRests()))
						{
							case Sequence.SequenceState.Sequence:
								processNStatus = ProcessNStatus.RestExists;
								break;
							case Sequence.SequenceState.One:
								processNStatus = ProcessNStatus.NoRest;
								break;
							case Sequence.SequenceState.Zero:
								processNStatus = ProcessNStatus.ZeroRest;
								break;
						}
						if (processNStatus == ProcessNStatus.NoRest || processNStatus == ProcessNStatus.ZeroRest)
						{
							rest = null;
							newChildProcesses.Clear();
							break;
						}
					}
					else
					{
						newChildProcesses.AddRange(childProcess.GetRests().Select(rest => new ProcessM(this, ++childProcessCount, rest, facts, true)));
					}
				}
			}
			else
			{
				rest = null;
				processNStatus = ProcessNStatus.ZeroRest;
			}
			childProcesses.Clear();
			childProcesses.AddRange(newChildProcesses);
			if (childProcesses.Count != 0)
			{
				runTime += childProcesses.Count * processUnit.RunCommand(CommandId.CreateMessage);
				runTime += childProcesses.Count * processUnit.RunCommand(CommandId.AddMessageToQueue);
				status = Status.Progress;
				Log("ожидание завершения дочерних процессов");
			}
			else
			{
				runTime += processUnit.RunCommand(CommandId.CreateMessage);
				runTime += processUnit.RunCommand(CommandId.AddMessageToQueue);
				runTime += processUnit.RunCommand(CommandId.WriteMemory);
				PrintStatus();
				status = Status.Complete;
				Log("процесс завершен");
			}
		}

		private Sequence.SequenceState FormRest(List<Sequence> rests)
		{
			List<Sequence> fullRests = new List<Sequence>(rests);
			if (rest != null)
			{
				fullRests.Add(rest);
			}
			runTime += processUnit.RunCommand(CommandId.MinimizeSequence, fullRests.Sum(rest => rest.GetDisjuncts().Count));
			rest = Minimizer.Minimize(Sequence.Multiply(fullRests));
			return Sequence.GetSequenceState(rest);
		}

		private void PrintStatus()
		{
			switch (processNStatus)
			{
				case ProcessNStatus.ZeroRest:
					statusData = "получен нулевой остаток";
					Log(statusData);
					break;
				case ProcessNStatus.RestExists:
					statusData = "получен конечный остаток";
					resultData = $"Остаток: {rest?.GetContent()}";
					Log($"{statusData}: {rest?.GetContent()}");
					break;
				case ProcessNStatus.NoRest:
					statusData = "конечного остатка не получено";
					Log(statusData);
					break;
			}
		}

		public ProcessNStatus GetProcessNStatus()
		{
			return processNStatus;
		}

		public Sequence GetRest()
		{
			return rest;
		}
	}
}