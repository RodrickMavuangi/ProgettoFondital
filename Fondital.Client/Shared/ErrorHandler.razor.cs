using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using System;

namespace Fondital.Client.Shared
{
    public partial class ErrorHandler
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public void ProcessEx(Exception ex, string component)
        {
            Console.WriteLine("Exception - Component: {Component} Type: {Type} Message: {Message}", component, ex.GetType(), ex.Message);
            Logger.LogError("Exception - Component: {Component} Type: {Type} Message: {Message}", component, ex.GetType(), ex.Message);
        }

        public void ProcessNullEx(NullReferenceException ex, string component)
        {
            Logger.LogError("NullReferenceException - Component: {Component} Type: {Type} Message: {Message}", component, ex.GetType(), ex.Message);
        }

        //eccetera
    }
}