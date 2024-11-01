﻿// Copyright (C) 2018 - 2024 Tony's Studio. All rights reserved.

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Tonisoft.AspExtensions.Module;

public class BaseController<TController> : Controller where TController : Controller
{
    protected readonly ILogger<TController> _logger;
    protected readonly IMapper _mapper;

    protected BaseController(IMapper mapper, ILogger<TController> logger)
    {
        _mapper = mapper;
        _logger = logger;
    }
}