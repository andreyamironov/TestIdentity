using AMir.Interface.Data;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TestIdentity.Identity.ViewModels
{
    public class ClientDetailsViewModel : IdentityServer4.Models.Client
    {
        [ReadOnly(true)]
        public int Id { get; set; }
    }
}
