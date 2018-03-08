// <copyright file="ExampleWorkflow.cs" company="">
// Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author></author>
// <date>3/8/2018 12:15:53 PM</date>
// <summary>Implements the ExampleWorkflow Workflow Activity.</summary>
namespace TFGM.D365.smartCXP.Workflows
{
	using System;
	using System.Activities;
	using System.ServiceModel;
	using Microsoft.Xrm.Sdk;
	using Microsoft.Xrm.Sdk.Workflow;

	public sealed class ExampleWorkflow : CodeActivity
	{
		/// <summary>
		/// Executes the workflow activity.
		/// </summary>
		/// <param name="executionContext">The execution context.</param>
		protected override void Execute(CodeActivityContext executionContext)
		{
			// Create the tracing service
			ITracingService tracingService = executionContext.GetExtension<ITracingService>();

			if (tracingService == null)
			{
				throw new InvalidPluginExecutionException("Failed to retrieve tracing service.");
			}

			tracingService.Trace("Entered ExampleWorkflow.Execute(), Activity Instance Id: {0}, Workflow Instance Id: {1}",
				executionContext.ActivityInstanceId,
				executionContext.WorkflowInstanceId);

			// Create the context
			IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();

			if (context == null)
			{
				throw new InvalidPluginExecutionException("Failed to retrieve workflow context.");
			}

			tracingService.Trace("ExampleWorkflow.Execute(), Correlation Id: {0}, Initiating User: {1}",
				context.CorrelationId,
				context.InitiatingUserId);

			IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
			IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

			try
			{
				// TODO: Implement your custom Workflow business logic.
			}
			catch (FaultException<OrganizationServiceFault> e)
			{
				tracingService.Trace("Exception: {0}", e.ToString());

				// Handle the exception.
				throw;
			}

			tracingService.Trace("Exiting ExampleWorkflow.Execute(), Correlation Id: {0}", context.CorrelationId);
		}
	}
}