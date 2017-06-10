using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using TimeDIrector.Client.Autofac;
using TimeDIrector.Client.Components.Helpers;
using TimeDIrector.Client.Models.Entity;
using TimeDIrector.Client.Models.Repository;
using TimeDIrector.Client.Services.Interfaces;
//using ClientProcess = TimeDirector.Client.Model.ClientProcess;

namespace TimeDIrector.Client.Services
{
	class ProcessMonitorService : IProcessMonitorService
	{
		private static readonly object Lock = new object();
		private List<ClientProcess> _clientProcesses;
		private Dictionary<string, QualityTime> _processNameQualityTime;
		private Timer _checkProcessTimer;
		private Timer _sendProcess;

		public ProcessMonitorService()
		{
			_clientProcesses = new List<ClientProcess>();
			
			_checkProcessTimer = new Timer(OnCheckProcess, null, new TimeSpan(0, 0, 5), new TimeSpan(0, 1, 0));
			_sendProcess = new Timer(SaveProcess, null, new TimeSpan(0, 0, 0), new TimeSpan(0, 2, 30));
		}

		
		private void SaveProcess(object state)
		{
			lock (Lock)
			{
				using (var scope1 = Injection.BeginLifetimeScope())
				using (var clientProcessRepository = Injection.Resolve<IClientProcessRepository>())
				using (var qualityTimeRepository = Injection.Resolve<IQualityTimeRepository>())
				{
					Debug.WriteLine("Process in data base: " + clientProcessRepository.GetList().Count() + "(old)");
					foreach (var process in _clientProcesses)
					  {
						var processUpdate = clientProcessRepository.Get(process);
						var qualityTime = qualityTimeRepository.Get(process.QualityTime);
						if (processUpdate == null)
						{
						
							
							//clientProcessRepository.Save();
							if (qualityTime != null)
							{
								//qualityTime.ClientProcesses.Add(process);
								//qualityTimeRepository.Update(qualityTime);
								//qualityTimeRepository.Save();
								process.QualityTime = qualityTime;
							}
							clientProcessRepository.Create(process);
						}
						else
						{
							processUpdate.ProcessTime += process.ProcessTime;
							if (processUpdate.QualityTime.Id != qualityTime.Id)
								process.QualityTime = qualityTime;
						}
						clientProcessRepository.Save();
					}
					_clientProcesses.Clear();
					Debug.WriteLine("Process in data base: " + clientProcessRepository.GetList().Count() + "(new)");
				}
			}
		}

		private void OnCheckProcess(object state) //TODO:Сделать завязку на состояния программы в зависимости от врнмени простоя
		{
			lock (Lock)//блок что бы не один другой поток не смог изменить елементы которые находятся в блоке Lock
			{
				if (LastInputHelper.GetLastInputTime() > 30)//если пользователь ничего не делал больше 30 сек то выход(потом будет все записываться в специальное поле pause)
					return;

				var currentProcess = ProcessHelper.GetForegroundProcess();//получаем активный процесс
				if (currentProcess == null)//если вдруг(ШИЗА) =)
					return;

				QualityTime qualityTime;//объект который в себе содержит enum который определяет продуктивное, непродуктивное, нейтральное время
				if (!_processNameQualityTime.TryGetValue(currentProcess.ProcessName, out qualityTime))//TryGetValue служит для проучения value по key
					qualityTime = new QualityTime {ProcessTime = TypeOfQualityTime.NeutralTime};//если для такого процесса нет качества времени, то мы создаем качество времени "нейтральное"

				//_processNameQualityTime dictionary с названием процесса и качеством времени для него

				if (_clientProcesses.Any(a => a.Name == currentProcess.ProcessName && a.Title == currentProcess.MainWindowTitle))//проверка на то есть ли такой процесс в массиве(что бы добавить время)
				  {// _clientProcesses массив с объектами(объект сосотоит из активного времени, качества времени, названия процесса и пару служебных полей)

					//достаем этот процесс из массива
					var processUpdate = _clientProcesses.First(f => f.Name == currentProcess.ProcessName && f.Title == currentProcess.MainWindowTitle);
					//проверяем - вдруг качество процесса изменилось
					if (processUpdate.QualityTime.ProcessTime != qualityTime.ProcessTime)
						processUpdate.QualityTime.ProcessTime = qualityTime.ProcessTime;//если изменилось, то меняем качество для данного процесса

					processUpdate.ProcessTime += TimeSpan.FromMinutes(1);//добавляем минуту(чекаем просто каждую минуту какой процесс активен)
					//TODO:Debug - это сделано что бы отслеживать выполнение программы, лог грубо говоря
					var debugProcess = _clientProcesses.First(f => f.Name == currentProcess.ProcessName &&
					                                               f.Title == currentProcess.MainWindowTitle);
					Debug.WriteLine("Print: " + DateTime.Now.ToString("T"));
					Debug.WriteLine("Edit:\t" + "|id: " + debugProcess.ProcessId + " |name: " + debugProcess.Name + " |title: " +
					                debugProcess.Title + " |time: " + debugProcess.Time + "| Total: " +
					                _clientProcesses.Count);
					//
				}
				else
				{
					var debugProcess = new ClientProcess//если такого процесса нет в массиве, то мы создаем объект
					{
						ProcessId = currentProcess.Id,//id процесса в системе
						Name = currentProcess.ProcessName,//имя
						Title = currentProcess.MainWindowTitle,//тайтл окна
						QualityTime = qualityTime,//полезность времени, созданый объект выше
						ProcessTime = new TimeSpan(0, 1, 0)//добавляем первую минуту
					};

					_clientProcesses.Add(debugProcess);//добавляем процесс в лист(массив)
					//TODO:Debug - опять отладочная хня - LOG...
					Debug.WriteLine("Print: " + DateTime.Now.ToString("T"));
					Debug.WriteLine("Add new:\t" + "|id: " + debugProcess.ProcessId + " |name: " + debugProcess.Name + " |title: " +
					                debugProcess.Title + " |time: " + debugProcess.Time);
				}
			}
		}

		public void Initialize(Dictionary<string, QualityTime> dictionary)
		{
			_processNameQualityTime = dictionary;
		}
	}
}
