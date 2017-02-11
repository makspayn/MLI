using System;
using System.Collections.Generic;
using MLI.Data;
using MLI.Method;
using MLI.Services;
using NLog;

namespace MLI.Machine
{
	public class Machine
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		public static Machine instance;
		private int execUnitCount;
		private int unifUnitCount;
		private KnowledgeBase knowledgeBase;
		private WorkSupervisor workSupervisor;
		private List<ExecUnit> execUnits;
		private ControlUnit controlUnit;

		public Machine(List<Sequence> facts, List<Sequence> rules, List<Sequence> conclusions)
		{
			execUnitCount = SettingsService.ExecUnitCount;
			unifUnitCount = SettingsService.UnifUnitCount;
			BuildKnowledgeBase(facts, rules, conclusions);
			BuildWorkSupervisor();
			BuildExecUnits();
			BuildControlUnit();
			ResolveDependencies();
			instance = this;
			logger.Info($"Создана машина {execUnitCount}x{unifUnitCount}");
		}

		public static Machine GetInstance()
		{
			if (instance != null) return instance;
			logger.Error("Машина еще не создана!");
			throw new Exception("Машина еще не создана!");
		}

		private void BuildKnowledgeBase(List<Sequence> facts, List<Sequence> rules, List<Sequence> conclusions)
		{
			logger.Debug("Создание базы знаний");
			knowledgeBase = new KnowledgeBase();
			knowledgeBase.SetFacts(facts);
			knowledgeBase.SetRules(rules);
			knowledgeBase.SetConclusions(conclusions);
		}

		private void BuildWorkSupervisor()
		{
			logger.Debug("Создание диспетчера работы машины");
			workSupervisor = new WorkSupervisor();
		}

		private void BuildExecUnits()
		{
			logger.Debug("Создание исполнительных блоков");
			execUnits = new List<ExecUnit>();
			for (int i = 0; i < execUnitCount; i++)
			{
				execUnits.Add(new ExecUnit(i + 1, unifUnitCount));
			}
		}

		private void BuildControlUnit()
		{
			logger.Debug("Создание блока управления");
			controlUnit = new ControlUnit();
		}

		private void ResolveDependencies()
		{
			logger.Debug("Разрешение зависимостей");
			workSupervisor.SetExecUnits(execUnits);
			workSupervisor.SetControlUnit(controlUnit);
			foreach (ExecUnit execUnit in execUnits)
			{
				execUnit.SetWorkSupervisor(workSupervisor);
			}
			controlUnit.SetWorkSupervisor(workSupervisor);
		}

		public void Run()
		{
			logger.Info("Машина запущена");
			Process mainProcess = new MainProcess();
			workSupervisor.AddMessage(
				new Message(mainProcess, Message.MessageType.Create), execUnits[0]);
		}

		public void Stop()
		{
			logger.Info("Машина остановлена");
		}
	}
}