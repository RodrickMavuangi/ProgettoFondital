﻿using Fondital.Shared.Enums;
using Fondital.Shared.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fondital.Client.Shared
{
    public partial class AddVoceCostoDialog
    {
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnSave { get; set; }
        protected VoceCosto NuovaVoceCosto { get; set; } = new VoceCosto();
        protected bool isSubmitting = false;
        protected string ErrorMessage = "";

        protected async Task SalvaVoceCosto()
        {
            isSubmitting = true;
            ErrorMessage = "";

            try
            {
                await httpClient.CreateVoceCosto(NuovaVoceCosto);
                isSubmitting = false;
                await OnSave.InvokeAsync();
            }
            catch (Exception ex)
            {
                isSubmitting = false;
                ErrorMessage = localizer[ex.Message];
            }
        }
    }
}
