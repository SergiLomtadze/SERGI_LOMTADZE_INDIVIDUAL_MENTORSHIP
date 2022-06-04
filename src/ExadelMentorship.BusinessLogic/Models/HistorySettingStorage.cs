﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExadelMentorship.BusinessLogic.Models
{
    public class HistorySettingStorage
    {
        public HistorySettingDetails[] HistorySettings { get; set; }
    }
    public class HistorySettingDetails
    {
        public string City { get; set; }
        public string ExecutionTime { get; set; }
    }
}
