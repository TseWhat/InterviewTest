using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FundsLibrary.InterviewTest.Web.Models.Errors
{
    public class ErrorModel
    {
        public string ErrorHeader { get; set; }
        public string ErrorMessage { get; set; }
        public string ExceptionMesage { get; set; }

        public ErrorModel() { }

        public ErrorModel(string exceptionMessage)
        {
            ExceptionMesage = exceptionMessage;
        }
    }
}