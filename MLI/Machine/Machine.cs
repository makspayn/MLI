using System;
using System.Collections.Generic;
using System.Diagnostics;
using MLI.Data;
using MLI.Forms;
using MLI.Method;
using MLI.Services;
using Process = MLI.Method.Process;

namespace MLI.Machine
{
	public class Machine
	{
		private static Machine instance;
		private int unitResourceCount;
		private int execUnitWeight;
		private KnowledgeBase knowledgeBase;
		private Supervisor workMachineSupervisor;
		private List<ProcessUnit> execUnits = new List<ProcessUnit>();
		private List<ProcessUnit> unifUnits = new List<ProcessUnit>();
		private ControlUnit controlUnit;
		private ReconfigurationUnit reconfigurationUnit;
		private Stopwatch machineWatch;

		public Machine(List<Sequence> facts, List<Sequence> rules, List<Sequence> conclusions)
		{
			unitResourceCount = SettingsService.UnitResourceCount;
			execUnitWeight = SettingsService.ExecUnitWeight;
			machineWatch = new Stopwatch();
			BuildKnowledgeBase(facts, rules, conclusions);
			BuildWorkMachineSupervisor();
			BuildControlUnit();
			BuildReconfigurationUnit();
			ResolveDependencies();
			instance = this;
			LogService.Info($"Создана машина с количеством ресурсов: {unitResourceCount}");
		}

		public static Machine GetInstance()
		{
			if (instance != null) return instance;
			LogService.Error("Машина еще не создана!");
			throw new Exception("Машина еще не создана!");
		}

		private void BuildKnowledgeBase(List<Sequence> facts, List<Sequence> rules, List<Sequence> conclusions)
		{
			LogService.Debug("Создание базы знаний");
			knowledgeBase = new KnowledgeBase();
			knowledgeBase.SetFacts(facts);
			knowledgeBase.SetRules(rules);
			knowledgeBase.SetConclusions(conclusions);
		}

		private void BuildWorkMachineSupervisor()
		{
			LogService.Debug("Создание диспетчера работы машины");
			workMachineSupervisor = new Supervisor();
		}
		
		private void BuildControlUnit()
		{
			LogService.Debug("Создание блока управления");
			controlUnit = new ControlUnit("CU", 1);
		}

		private void BuildReconfigurationUnit()
		{
			LogService.Debug("Создание блока реконфигурации и вычислительных ресурсов");
			reconfigurationUnit = new ReconfigurationUnit(unitResourceCount, execUnitWeight);
			int maxExecUnitCount = unitResourceCount / execUnitWeight;
			int maxUnifUnitCount = maxExecUnitCount * execUnitWeight;
			for (int i = 0; i < maxUnifUnitCount; i++)
			{
				unifUnits.Add(new UnifUnit("UU", i + 1));
			}
			for (int i = 0; i < maxExecUnitCount; i++)
			{
				execUnits.Add(new ExecUnit("EU", i + 1, reconfigurationUnit, unifUnits));
			}
		}

		private void ResolveDependencies()
		{
			LogService.Debug("Разрешение зависимостей");
			workMachineSupervisor.SetProcessUnits(execUnits);
			workMachineSupervisor.SetControlUnit(controlUnit);
			workMachineSupervisor.SetReconfigurationUnit(reconfigurationUnit);
			controlUnit.SetSupervisor(workMachineSupervisor);
		}

		public void Run()
		{
			StatisticsService.Clear();
			machineWatch.Start();
			LogService.Info("Машина запущена");
			Process mainProcess = new MainProcess(null, 0, knowledgeBase.facts, knowledgeBase.rules, knowledgeBase.conclusions[0]);
			workMachineSupervisor.AddMessage(new Message(mainProcess, Message.MessageType.Create), null);
		}

		public void Stop()
		{
			workMachineSupervisor.CompleteWork();
			CompleteWork("Логический вывод остановлен");
		}

		public void CompleteWork(string message)
		{
			machineWatch.Stop();
			LogService.Info("Машина завершила работу");
			LogService.Info("Сортировка статистики");
			StatisticsService.PrepareStatistics();
			LogService.Info("Сортировка статистики завершена");
			CompleteEvent machineEvent = new CompleteEvent();
			machineEvent.machineCompleteEvent += MainForm.GetInstance().MachineCompleteEventHandler;
			machineEvent.OnMachineCompleteEvent($"{message}\n" +
			    $"Время работы: {machineWatch.ElapsedMilliseconds} мс");
		}

		private delegate void MachineCompleteEvent(string message);

		private class CompleteEvent
		{
			public event MachineCompleteEvent machineCompleteEvent;

			public void OnMachineCompleteEvent(string message)
			{
				machineCompleteEvent(message);
			}
		}
	}
}