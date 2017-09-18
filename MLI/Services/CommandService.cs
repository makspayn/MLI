using System;
using System.Collections.Generic;
using System.Linq;
using MLI.Data;

namespace MLI.Services
{
	public static class CommandService
	{
		private static Dictionary<CommandId, Command> commands = new Dictionary<CommandId, Command>();

		public static void InitCommands()
		{
			commands.Add(CommandId.UnificationConstantAndConstant, new Command(CommandId.UnificationConstantAndConstant, "Унификация двух констант", 1));
			commands.Add(CommandId.UnificationConstantAndVariable, new Command(CommandId.UnificationConstantAndVariable, "Унификация константы и переменной", 3));
			commands.Add(CommandId.UnificationConstantAndFunctor, new Command(CommandId.UnificationConstantAndFunctor, "Унификация константы и функтора", 1));
			commands.Add(CommandId.UnificationVariableAndVariable, new Command(CommandId.UnificationVariableAndVariable, "Унификация двух переменных", 3));
			commands.Add(CommandId.UnificationVariableAndFunctor, new Command(CommandId.UnificationVariableAndFunctor, "Унификация переменной и функтора", 5));
			commands.Add(CommandId.UnificationFunctorAndFunctor, new Command(CommandId.UnificationFunctorAndFunctor, "Унификация двух функторов", 15));
			commands.Add(CommandId.CreateMessage, new Command(CommandId.CreateMessage, "Формирование сообщения", 10));
			commands.Add(CommandId.AddMessageToQueue, new Command(CommandId.AddMessageToQueue, "Добавление сообщения в очередь", 2));
			commands.Add(CommandId.DeleteMessageFromQueue, new Command(CommandId.DeleteMessageFromQueue, "Удаление сообщения из очереди", 2));
			commands.Add(CommandId.FormPredicatePair, new Command(CommandId.FormPredicatePair, "Составление пар предикатов", 2));
			commands.Add(CommandId.FormRests, new Command(CommandId.FormRests, "Формирование конъюнкции остатков", 2));
			commands.Add(CommandId.FormRest, new Command(CommandId.FormRest, "Формирование конечного остатка", 2));
			commands.Add(CommandId.FormConclusion, new Command(CommandId.FormConclusion, "Формирование выводимого правила", 3));
			commands.Add(CommandId.MinimizeSequence, new Command(CommandId.MinimizeSequence, "Минимизация секвенции", 4));
			commands.Add(CommandId.CreateRestsMatrix, new Command(CommandId.CreateRestsMatrix, "Создание матрицы остатков", 5));
			commands.Add(CommandId.AnalyzeRestsMatrix, new Command(CommandId.AnalyzeRestsMatrix, "Анализ матрицы остатков", 1));
			commands.Add(CommandId.CanInference, new Command(CommandId.CanInference, "Проверка осуществимости вывода", 3));
			commands.Add(CommandId.CreateFrame, new Command(CommandId.CreateFrame, "Создание фрейма процесса", 7));
			commands.Add(CommandId.AddFrameToList, new Command(CommandId.AddFrameToList, "Добавление фрейма в список", 3));
			commands.Add(CommandId.DeleteFrameFromList, new Command(CommandId.DeleteFrameFromList, "Удаление фрейма процесса", 3));
			commands.Add(CommandId.CreateProcess, new Command(CommandId.CreateProcess, "Создание процесса", 5));
			commands.Add(CommandId.ReadMemory, new Command(CommandId.ReadMemory, "Чтение памяти", 10));
			commands.Add(CommandId.WriteMemory, new Command(CommandId.WriteMemory, "Запись в память", 15));
		}

		public static int RunCommand(CommandId commandId, params object[] param)
		{
			int time;
			Command command;
			if (commands.TryGetValue(commandId, out command))
			{
				time = command.time;
				switch (commandId)
				{
					case CommandId.UnificationVariableAndFunctor:
						time = (bool) param[0] ? time : time * 2;
						break;
					case CommandId.CanInference:
						time = time * (int) param[0] * (int) param[1] * (int) param[2];
						break;
					case CommandId.FormPredicatePair:
						time = time * (int) param[0] * (int) param[1];
						break;
					case CommandId.MinimizeSequence:
						time = time * (int) param[0];
						break;
					case CommandId.FormRest:
						time = time * (int) param[0];
						break;
					case CommandId.FormConclusion:
						time = time * (int) param[0];
						break;
					case CommandId.CreateRestsMatrix:
						time = time * (int) param[0];
						break;
					case CommandId.FormRests:
						time = time * (int) param[0];
						break;
				}
			}
			else
			{
				throw new ArgumentOutOfRangeException(nameof(commandId), commandId, null);
			}
			return time;
		}

		public static List<Command> GetCommandList()
		{
			return commands.Values.ToList();
		}

		public static void SetCommandList(List<Command> commandList)
		{
			foreach (Command command in commandList)
			{
				if (commands.ContainsKey(command.id))
				{
					commands.Remove(command.id);
				}
				commands.Add(command.id, command);
			}
		}
	}
}