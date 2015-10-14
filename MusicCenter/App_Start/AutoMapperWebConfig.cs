using AutoMapper;
using MusicCenter.Common.ViewModels;
using MusicCenter.Dal.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicCenter.App_Start
{
    public class AutoMapperWebConfig
    {
        public static void Configure()
        {
            ConfigureUserMapping();
            //...
        }

        private static void ConfigureUserMapping()
        {
            Mapper.CreateMap<Users, UserRegisterViewModel>();
        } 
    }
}