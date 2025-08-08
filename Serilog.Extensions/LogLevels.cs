﻿using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Serilog.Extensions;

public interface ILogLevels
{
	LoggingLevelSwitch DefaultLevelSwitch { get; }
	Dictionary<string, LoggingLevelSwitch> LoggingLevels { get; }
	LoggerConfiguration GetConfiguration();
}

/// <summary>
/// Define logging levels for different namespaces that can be toggled at runtime
/// </summary>
public abstract class LogLevels(LogEventLevel defaultMinLevel = LogEventLevel.Information) : ILogLevels
{
	public abstract Dictionary<string, LoggingLevelSwitch> LoggingLevels { get; }

	public LogEventLevel DefaultLevel { get; } = defaultMinLevel;

	/// <summary>
	/// Switch for controlling the default minimum log level at runtime
	/// </summary>
	public LoggingLevelSwitch DefaultLevelSwitch { get; } = new(defaultMinLevel);

	public LoggerConfiguration GetConfiguration()
	{
		var loggerConfig = new LoggerConfiguration()
			.MinimumLevel.ControlledBy(DefaultLevelSwitch);

		foreach (var kp in LoggingLevels)
		{
			loggerConfig = loggerConfig.MinimumLevel.Override(kp.Key, kp.Value);
		}

		return loggerConfig;
	}
}