﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.APIDomain.Interface
{
	public interface IAPIDataService
	{
		public Task<IDictionary<string, string>> getAPIData();
	}
}
