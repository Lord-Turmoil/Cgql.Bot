﻿using Cgql.Bot.Model.Database;
using Cgql.Bot.Model.Dto;

namespace Cgql.Bot.Server.Services;

public interface IWebhookService
{
    public void AddTask(ScanTask task);
}