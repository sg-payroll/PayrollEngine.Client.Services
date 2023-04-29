﻿using System;
using PayrollEngine.Client.Model;

namespace PayrollEngine.Client.Scripting.Runtime.Api;

/// <summary>Runtime for the case function</summary>
public abstract class CaseRuntime : PayrollRuntime, ICaseRuntime
{
    /// <summary>The case</summary>
    protected Case Case { get; }

    /// <summary>Initializes a new instance of the <see cref="CaseRuntime"/> class</summary>
    /// <param name="httpClient">The Payroll http client</param>
    /// <param name="calendar">The calendar</param>
    /// <param name="tenantId">The tenant id</param>
    /// <param name="userId">The user id</param>
    /// <param name="payrollId">The payroll id</param>
    /// <param name="case">The runtime case</param>
    /// <param name="employeeId">The employee id</param>
    protected CaseRuntime(PayrollHttpClient httpClient, ScriptingCalendar calendar, int tenantId,
        int userId, int payrollId, Case @case, int? employeeId = null) :
        base(httpClient, calendar, tenantId, userId, payrollId, employeeId)
    {
        Case = @case ?? throw new ArgumentNullException(nameof(@case));
    }

    /// <inheritdoc />
    public string CaseName => Case.Name;

    /// <inheritdoc />
    public int CaseType => (int)Case.CaseType;
        
    /// <summary>The log owner, the source identifier</summary>
    protected override string LogOwner => CaseName;

    /// <inheritdoc />
    public object GetCaseAttribute(string attributeName) =>
        Case.Attributes?.GetValue<object>(attributeName);

    /// <inheritdoc />
    public string InvokeWebhook(string requestOperation, string requestMessage = null)
    {
        throw new NotSupportedException("Webhooks are not supported through the client services");
    }
}