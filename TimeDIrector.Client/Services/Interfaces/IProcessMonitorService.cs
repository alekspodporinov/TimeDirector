﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeDIrector.Client.Models.Entity;

namespace TimeDIrector.Client.Services.Interfaces
{
	public interface IProcessMonitorService
	{
		void Initialize(Dictionary<string, QualityTime> dictionary);
	}
}
