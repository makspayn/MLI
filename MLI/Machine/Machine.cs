﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using MLI.Data;
using MLI.Forms;
using MLI.Method;
using MLI.Services;
using NLog;
using Process = MLI.Method.Process;

namespace MLI.Machine
{
	public class Machine
	{
		private static Logger logger = LogManager.GetCurrentClassLogger();
		private static Machine instance;
		private int execUnitCount;
		private int unifUnitCount;
		private KnowledgeBase knowledgeBase;
		private Supervisor workMachineSupervisor;
		private List<ProcessUnit> execUnits;
		private ControlUnit controlUnit;
		private Stopwatch machineWatch;

		public Machine(List<Sequence> facts, List<Sequence> rules, List<Sequence> conclusions)
		{
			execUnitCount = SettingsService.ExecUnitCount;
			unifUnitCount = SettingsService.UnifUnitCount;
			machineWatch = new Stopwatch();
			BuildKnowledgeBase(facts, rules, conclusions);
			BuildWorkMachineSupervisor();
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

		private void BuildWorkMachineSupervisor()
		{
			logger.Debug("Создание диспетчера работы машины");
			workMachineSupervisor = new Supervisor();
		}

		private void BuildExecUnits()
		{
			logger.Debug("Создание исполнительных блоков");
			execUnits = new List<ProcessUnit>();
			for (int i = 0; i < execUnitCount; i++)
			{
				execUnits.Add(new ExecUnit($"EU №{i + 1}", unifUnitCount));
			}
		}

		private void BuildControlUnit()
		{
			logger.Debug("Создание блока управления");
			controlUnit = new ControlUnit("CU №1");
		}

		private void ResolveDependencies()
		{
			logger.Debug("Разрешение зависимостей");
			workMachineSupervisor.SetProcessUnits(execUnits);
			workMachineSupervisor.SetControlUnit(controlUnit);
			foreach (ProcessUnit execUnit in execUnits)
			{
				execUnit.SetSupervisor(workMachineSupervisor);
			}
			controlUnit.SetSupervisor(workMachineSupervisor);
		}

		public void Run()
		{
			machineWatch.Start();
			logger.Info("Машина запущена");
			Process mainProcess = new MainProcess();
			workMachineSupervisor.AddMessage(
				new Message(mainProcess, Message.MessageType.Create), execUnits[0]);
		}

		public void Stop()
		{
			workMachineSupervisor.CompleteWork();
			CompleteWork("Логический вывод остановлен");
		}

		public void CompleteWork(string message)
		{
			machineWatch.Stop();
			logger.Info("Машина завершила работу");
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